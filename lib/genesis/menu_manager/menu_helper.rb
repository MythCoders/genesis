module Genesis
  module MenuManager

    module MenuHelper
      include ApplicationHelper

      # Returns the current menu item name
      def current_menu_item
        controller.current_menu_item
      end

      # Renders the application main menu
      def render_main_menu(project)
        if menu_name = controller.current_menu(project)
          render_menu(menu_name, project)
        end
      end

      def display_main_menu?(project)
        menu_name = controller.current_menu(project)
        menu_name.present? && Genesis::MenuManager.items(menu_name).children.present?
      end

      def render_menu(menu, project=nil)
        links = []
        menu_items_for(menu, project) do |node|
          links << render_menu_node(node, project)
        end
        links.empty? ? nil : links.join.html_safe
      end

      def render_menu_node(node, project=nil)
        if node.children.present? || !node.child_menus.nil?
          render_menu_node_with_children(node, project)
        else
          caption, url, selected = extract_node_details(node, project)
          content_tag('li', render_single_menu_node(node, caption, url, selected),
                             :class => (is_active?(node)))
        end
      end

      def is_active?(node)
        unless node.url.blank?
          'active' if controller?(node.url[:controller])
          '' if action?(node.url[:action])
        end
      end

      def render_menu_node_with_children(node, project=nil)
        caption, url, selected = extract_node_details(node, project)

        html = [].tap do |html|
          html << '<li class=\'d\'>'
          # Parent
          html << render_single_menu_node(node, caption, url, selected)

          # Standard children
          standard_children_list = ''.html_safe.tap do |child_html|
            node.children.each do |child|
              child_html << render_menu_node(child, project) if allowed_node?(child, User.current, project)
            end
          end

          html << content_tag(:ul, standard_children_list, :class => 'nav sidebar-subnav collapse', :id => "##{node.name}") unless standard_children_list.empty?

          # Unattached children
          unattached_children_list = render_unattached_children_menu(node, project)
          html << content_tag(:ul, unattached_children_list, :class => 'nav sidebar-subnav unattached') unless unattached_children_list.blank?

          html << '</li>'
        end
        return html.join("\n").html_safe
      end

      # Returns a list of unattached children menu items
      def render_unattached_children_menu(node, project)
        return nil unless node.child_menus

        ''.html_safe.tap do |child_html|
          unattached_children = node.child_menus.call(project)
          # Tree nodes support #each so we need to do object detection
          if unattached_children.is_a? Array
            unattached_children.each do |child|
              child_html << content_tag(:li, render_unattached_menu_item(child, project)) if allowed_node?(child, User.current, project)
            end
          else
            raise MenuError, ':child_menus must be an array of MenuItems'
          end
        end
      end

      def render_single_menu_node(item, caption, url, selected)
        options = item.html_options(:selected => selected, )

        # virtual nodes are only there for their children to be displayed in the menu
        # and should not do anything on click, except if otherwise defined elsewhere
        if url.blank?
          url = '#'
          options.reverse_merge!(:onclick => 'return false;')
        end

        if item.icon.blank?
          link_to(caption, url, options)
        else
          link_to(url, options) do
            rv = content_tag :em do
              content_tag(:i, nil, class: "fa fa-#{item.icon}")
            end
            rv << content_tag(:span, h(caption))
          end
        end
      end

      def render_unattached_menu_item(menu_item, project)
        raise MenuError, ':child_menus must be an array of MenuItems' unless menu_item.is_a? MenuItem

        if menu_item.allowed?(User.current, project)
          link_to(menu_item.caption, menu_item.url, menu_item.html_options)
        end
      end

      def menu_items_for(menu, project=nil)
        items = []
        Genesis::MenuManager.items(menu).root.children.each do |node|
          if node.allowed?(User.current, project)
            if block_given?
              yield node
            else
              items << node  # TODO: not used?
            end
          end
        end
        return block_given? ? nil : items
      end

      def extract_node_details(node, project=nil)
        item = node
        url = case item.url
                when Hash
                  project.nil? ? item.url : {item.param => project}.merge(item.url)
                when Symbol
                  if project
                    send(item.url, project)
                  else
                    send(item.url)
                  end
                else
                  item.url
              end
        caption = item.caption(project)
        return [caption, url, (current_menu_item == item.name)]
      end

      # See MenuItem#allowed?
      def allowed_node?(node, user, project)
        #raise MenuError, ':child_menus must be an array of MenuItems' unless node.is_a? Genesis::MenuManager::MenuItem
        node.allowed?(user, project)
      end
    end

  end
end