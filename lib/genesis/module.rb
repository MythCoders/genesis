module Genesis #:nodoc:

  class ModuleCompatibilityError < StandardError; end
  class ModuleNotFound < StandardError; end
  class ModuleRequirementError < StandardError; end

  class Module

    cattr_accessor :directory
    self.directory = File.join(Rails.root, 'modules')

    cattr_accessor :public_directory
    self.public_directory = File.join(Rails.root, 'public', 'module_assets')

    @registered_modules = {}
    @used_partials = {}

    class << self
      attr_reader :registered_modules
      private :new

      def def_field(*names)
        class_eval do
          names.each do |name|
            define_method(name) do |*args|
              args.empty? ? instance_variable_get("@#{name}") : instance_variable_set("@#{name}", *args)
            end
          end
        end
      end
    end
    def_field :name, :description, :url, :author, :author_url, :version, :settings, :directory
    attr_reader :id

    def self.register(id, &block)
      p = new(id)
      p.instance_eval(&block)

      # Set a default name if it was not provided during registration
      p.name(id.to_s.humanize) if p.name.nil?
      # Set a default directory if it was not provided during registration
      p.directory(File.join(self.directory, id.to_s)) if p.directory.nil?

      # Adds module locales if any
      # YAML translation files should be found under <module>/config/locales/
      Rails.application.config.i18n.load_path += Dir.glob(File.join(p.directory, 'config', 'locales', '*.yml'))

      # Prepends the app/views directory of the module to the view path
      view_path = File.join(p.directory, 'app', 'views')
      if File.directory?(view_path)
        ActionController::Base.prepend_view_path(view_path)
        ActionMailer::Base.prepend_view_path(view_path)
      end

      # Adds the app/{controllers,helpers,models} directories of the module to the autoload path
      Dir.glob File.expand_path(File.join(p.directory, 'app', '{controllers,helpers,models}')) do |dir|
        ActiveSupport::Dependencies.autoload_paths += [dir]
      end

      # Defines module setting if present
      if p.settings
        Setting.define_module_setting p
      end

      # Warn for potential settings[:partial] collisions
      if p.configurable?
        partial = p.settings[:partial]
        if @used_partials[partial]
          Rails.logger.warn "WARNING: settings partial '#{partial}' is declared in '#{p.id}' module but it is already used by module '#{@used_partials[partial]}'. Only one settings view will be used. You may want to contact those modules authors to fix this."
        end
        @used_partials[partial] = p.id
      end

      registered_modules[id] = p
    end

    def initialize(id)
      @id = id.to_sym
    end

    def public_directory
      File.join(self.class.public_directory, id.to_s)
    end

    def to_param
      id
    end

    def assets_directory
      File.join(directory, 'assets')
    end

    def <=>(plugin)
      self.id.to_s <=> plugin.id.to_s
    end

    def self.all
      registered_modules.values.sort
    end

    # Finds a module by its id
    # Returns a PluginNotFound exception if the module doesn't exist
    def self.find(id)
      registered_modules[id.to_sym] || raise(ModuleNotFound)
    end

    # Clears the registered modules hash
    # It doesn't unload installed modules
    def self.clear
      @registered_modules = {}
    end

    # Removes a module from the registered modules
    # It doesn't unload the module
    def self.unregister(id)
      @registered_modules.delete(id)
    end

    # Checks if a module is installed
    #
    # @param [String] id name of the module
    def self.installed?(id)
      @registered_modules[id.to_sym].present?
    end

    def self.load
      Dir.glob(File.join(self.directory, '*')).sort.each do |directory|
        if File.directory?(directory)
          lib = File.join(directory, 'lib')
          if File.directory?(lib)
            $:.unshift lib
            ActiveSupport::Dependencies.autoload_paths += [lib]
          end
          initializer = File.join(directory, 'init.rb')
          if File.file?(initializer)
            require initializer
          end
        end
      end
    end

    # Sets a requirement on GeneSIS version
    # Raises a ModuleRequirementError exception if the requirement is not met
    def requires_genesis(arg)
      arg = { :version_or_higher => arg } unless arg.is_a?(Hash)
      arg.assert_valid_keys(:version, :version_or_higher)

      current = Genesis::VERSION.to_a
      arg.each do |k, req|
        case k
          when :version_or_higher
            raise ArgumentError.new(":version_or_higher accepts a version string only") unless req.is_a?(String)
            unless compare_versions(req, current) <= 0
              raise ModuleRequirementError.new("#{id} module requires GeneSIS #{req} or higher but current is #{current.join('.')}")
            end
          when :version
            req = [req] if req.is_a?(String)
            if req.is_a?(Array)
              unless req.detect {|ver| compare_versions(ver, current) == 0}
                raise ModuleRequirementError.new("#{id} module requires one the following GeneSIS versions: #{req.join(', ')} but current is #{current.join('.')}")
              end
            elsif req.is_a?(Range)
              unless compare_versions(req.first, current) <= 0 && compare_versions(req.last, current) >= 0
                raise ModuleRequirementError.new("#{id} module requires a Redmine version between #{req.first} and #{req.last} but current is #{current.join('.')}")
              end
            else
              raise ArgumentError.new(":version option accepts a version string, an array or a range of versions")
            end
        end
      end
      true
    end

    # Sets a requirement on a GeneSIS module version
    # Raises a ModuleRequirementError exception if the requirement is not met
    def requires_genesis_module(module_name, arg)
      arg = { :version_or_higher => arg } unless arg.is_a?(Hash)
      arg.assert_valid_keys(:version, :version_or_higher)

      mod = Plugin.find(module_name)
      current = mod.version.split('.').collect(&:to_i)

      arg.each do |k, v|
        v = [] << v unless v.is_a?(Array)
        versions = v.collect {|s| s.split('.').collect(&:to_i)}
        case k
          when :version_or_higher
            raise ArgumentError.new("wrong number of versions (#{versions.size} for 1)") unless versions.size == 1
            unless (current <=> versions.first) >= 0
              raise ModuleRequirementError.new("#{id} module requires the #{plugin_name} module #{v} or higher but current is #{current.join('.')}")
            end
          when :version
            unless versions.include?(current.slice(0,3))
              raise ModuleRequirementError.new("#{id} module requires one the following versions of #{plugin_name}: #{v.join(', ')} but current is #{current.join('.')}")
            end
        end
      end
      true
    end

    # Adds an item to the given +menu+.
    # The +id+ parameter (equals to the project id) is automatically added to the url.
    #   menu :project_menu, :plugin_example, { :controller => 'example', :action => 'say_hello' }, :caption => 'Sample'
    #
    # +name+ parameter can be: :top_menu, :account_menu, :application_menu or :project_menu
    #
    def menu(menu, item, url, options={})
      Genesis::MenuManager.map(menu).push(item, url, options)
    end
    alias :add_menu_item :menu

    # Removes +item+ from the given +menu+.
    def delete_menu_item(menu, item)
      Genesis::MenuManager.map(menu).delete(item)
    end

    # Returns +true+ if the module can be configured.
    def configurable?
      settings && settings.is_a?(Hash) && !settings[:partial].blank?
    end

    def mirror_assets
      source = assets_directory
      destination = public_directory
      return unless File.directory?(source)

      source_files = Dir[source + '/**/*']
      source_dirs = source_files.select { |d| File.directory?(d) }
      source_files -= source_dirs

      unless source_files.empty?
        base_target_dir = File.join(destination, File.dirname(source_files.first).gsub(source, ''))
        begin
          FileUtils.mkdir_p(base_target_dir)
        rescue Exception => e
          raise "Could not create directory #{base_target_dir}: " + e.message
        end
      end

      source_dirs.each do |dir|
        # strip down these paths so we have simple, relative paths we can
        # add to the destination
        target_dir = File.join(destination, dir.gsub(source, ''))
        begin
          FileUtils.mkdir_p(target_dir)
        rescue Exception => e
          raise "Could not create directory #{target_dir}: " + e.message
        end
      end

      source_files.each do |file|
        begin
          target = File.join(destination, file.gsub(source, ''))
          unless File.exist?(target) && FileUtils.identical?(file, target)
            FileUtils.cp(file, target)
          end
        rescue Exception => e
          raise "Could not copy #{file} to #{target}: " + e.message
        end
      end
    end

    # Mirrors assets from one or all plugins to public/plugin_assets
    def self.mirror_assets(name=nil)
      if name.present?
        find(name).mirror_assets
      else
        all.each do |plugin|
          plugin.mirror_assets
        end
      end
    end

    # The directory containing this module's migrations (<tt>module/db/migrate</tt>)
    def migration_directory
      File.join(directory, 'db', 'migrate')
    end

    # Returns the version number of the latest migration for this module. Returns
    # nil if this module has no migrations.
    def latest_migration
      migrations.last
    end

    # Returns the version numbers of all migrations for this module.
    def migrations
      migrations = Dir[migration_directory+'/*.rb']
      migrations.map { |p| File.basename(p).match(/0*(\d+)\_/)[1].to_i }.sort
    end

    # Migrate this module to the given version
    def migrate(version = nil)
      puts "Migrating #{id} (#{name})..."
      Genesis::Module::Migrator.migrate_plugin(self, version)
    end

    # Migrates all plugins or a single module to a given version
    # Exemples:
    #   Plugin.migrate
    #   Plugin.migrate('sample_plugin')
    #   Plugin.migrate('sample_plugin', 1)
    #
    def self.migrate(name=nil, version=nil)
      if name.present?
        find(name).migrate(version)
      else
        all.each do |plugin|
          plugin.migrate
        end
      end
    end

    class Migrator < ActiveRecord::Migrator
      # We need to be able to set the 'current' module being migrated.
      cattr_accessor :current_plugin

      class << self
        # Runs the migrations from a module, up (or down) to the version given
        def migrate_plugin(plugin, version)
          self.current_plugin = plugin
          return if current_version(plugin) == version
          migrate(plugin.migration_directory, version)
        end

        def current_version(plugin=current_plugin)
          # Delete migrations that don't match .. to_i will work because the number comes first
          ::ActiveRecord::Base.connection.select_values(
              "SELECT version FROM #{schema_migrations_table_name}"
          ).delete_if{ |v| v.match(/-#{plugin.id}$/) == nil }.map(&:to_i).max || 0
        end
      end

      def migrated
        sm_table = self.class.schema_migrations_table_name
        ::ActiveRecord::Base.connection.select_values(
            "SELECT version FROM #{sm_table}"
        ).delete_if{ |v| v.match(/-#{current_plugin.id}$/) == nil }.map(&:to_i).sort
      end

      def record_version_state_after_migrating(version)
        super(version.to_s + '-' + current_plugin.id.to_s)
      end
    end

    private

    def compare_versions(requirement, current)
      requirement = requirement.split('.').collect(&:to_i)
      requirement <=> current.slice(0, requirement.size)
    end

  end

end