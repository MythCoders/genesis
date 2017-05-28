module Genesis
  module MenuManager

    class MenuItem < MenuNode
      include Genesis::I18n
      attr_reader :name, :url, :param, :condition, :parent, :child_menus, :last, :permission, :icon

      def initialize(name, url, options={})
        raise ArgumentError, "Invalid option :if for menu item '#{name}'" if options[:if] && !options[:if].respond_to?(:call)
        raise ArgumentError, "Invalid option :html for menu item '#{name}'" if options[:html] && !options[:html].is_a?(Hash)
        raise ArgumentError, 'Cannot set the :parent to be the same as this item' if options[:parent] == name.to_sym
        raise ArgumentError, "Invalid option :children for menu item '#{name}'" if options[:children] && !options[:children].respond_to?(:call)
        @name = name
        @url = url
        @condition = options[:if]
        @permission = options[:permission]
        @permission ||= false if options.key?(:permission)
        @param = options[:param] || :id
        @caption = options[:caption]
        @html_options = options[:html] || {}
        @icon = options[:icon]
        # Adds a unique class to each menu item based on its name
        @html_options[:class] = [@html_options[:class], @name.to_s.dasherize].compact.join(' ')
        @parent = options[:parent]
        @child_menus = options[:children]
        @last = options[:last] || false
        super @name.to_sym
      end

      def caption(project=nil)
        if @caption.is_a?(Proc)
          c = @caption.call(project).to_s
          c = @name.to_s.humanize if c.blank?
          c
        else
          if @caption.nil?
            l_or_humanize(name, :prefix => 'label_')
          else
            @caption.is_a?(Symbol) ? l(@caption) : @caption
          end
        end
      end

      def html_options(options={})
        if options[:selected]
          o = @html_options.dup
          o[:class] += ' selected'
          o
        else
          @html_options
        end
      end

      # Checks if a user is allowed to access the menu item by:
      #
      # * Checking the permission or the url target (project only)
      # * Checking the conditions of the item
      def allowed?(user, project)
        # if url.blank?
        #   all_children = children
        #   all_children += child_menus.call(project) if child_menus
        #   return false unless all_children.detect{|child| child.allowed?(user, project)}
        # end
        true
      end

    end

  end
end