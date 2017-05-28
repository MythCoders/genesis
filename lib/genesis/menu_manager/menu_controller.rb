module Genesis
  module MenuManager

    module MenuController

      def self.included(base)
        base.class_attribute :main_menu
        base.main_menu = true

        base.extend(ClassMethods)
      end

      def menu_items
        self.class.menu_items
      end

      def current_menu(project)
        if project && !project.new_record?
          :project_menu
        elsif self.class.main_menu
          :application_menu
        end
      end

      # Returns the menu item name according to the current action
      def current_menu_item
        @current_menu_item ||= menu_items[controller_name.to_sym][:actions][action_name.to_sym] ||
            menu_items[controller_name.to_sym][:default]
      end

      # Redirects user to the menu item
      # Returns false if user is not authorized
      def redirect_to_menu_item(name)
        redirect_to_project_menu_item(nil, name)
      end

      # Redirects user to the menu item of the given project
      # Returns false if user is not authorized
      def redirect_to_project_menu_item(project, name)
        menu = project.nil? ? :application_menu : :project_menu
        item = Genesis::MenuManager.items(menu).detect {|i| i.name.to_s == name.to_s}
        if item && item.allowed?(User.current, project)
          url = item.url
          url = {item.param => project}.merge(url) if project
          redirect_to url
          return true
        end
        false
      end

      module ClassMethods
        @@menu_items = Hash.new {|hash, key| hash[key] = {:default => key, :actions => {}}}
        mattr_accessor :menu_items

        # Set the menu item name for a controller or specific actions
        # Examples:
        #   * menu_item :tickets # => sets the menu name to :tickets for the whole controller
        #   * menu_item :tickets, :only => :list # => sets the menu name to :tickets for the 'list' action only
        #   * menu_item :tickets, :only => [:list, :show] # => sets the menu name to :tickets for 2 actions only
        #
        # The default menu item name for a controller is controller_name by default
        # Eg. the default menu item name for ProjectsController is :projects
        def menu_item(id, options = {})
          if actions == options[:only]
            actions = [] << actions unless actions.is_a?(Array)
            actions.each {|a| menu_items[controller_name.to_sym][:actions][a.to_sym] = id}
          else
            menu_items[controller_name.to_sym][:default] = id
          end
        end
      end

    end

  end
end