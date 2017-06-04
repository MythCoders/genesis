module Genesis
  module Themes
    extend self

    # Theme ID used when no `default_theme` configuration setting is provided.
    APPLICATION_DEFAULT = 1

    # Struct class representing a single Theme
    Theme = Struct.new(:id, :name, :css_class)

    # All available Themes
    THEMES = [
        Theme.new(1, 'Blue w/ Light', 'blue_light'),
        Theme.new(2, 'Green w/ Light', 'green_light'),
        Theme.new(3, 'Purple w/ Light',    'purple_light'),
        Theme.new(4, 'Red w/ Light',    'red_purple'),
        Theme.new(5, 'Blue w/ Dark', 'blue_dark'),
        Theme.new(6, 'Green w/ Dark', 'green_dark'),
        Theme.new(7, 'Purple w/ Dark',    'purple_dark'),
        Theme.new(8, 'Red w/ Dark',    'red_dark')
    ].freeze

    # Convenience method to get a space-separated String of all the theme
    # classes that might be applied to the `body` element
    #
    # Returns a String
    def body_classes
      THEMES.collect(&:css_class).uniq.join(' ')
    end

    # Get a Theme by its ID
    #
    # If the ID is invalid, returns the default Theme.
    #
    # id - Integer ID
    #
    # Returns a Theme
    def by_id(id)
      THEMES.detect { |t| t.id == id } || default
    end

    # Returns the number of defined Themes
    def count
      THEMES.size
    end

    # Get the default Theme
    #
    # Returns a Theme
    def default
      by_id(default_id)
    end

    # Iterate through each Theme
    #
    # Yields the Theme object
    def each(&block)
      THEMES.each(&block)
    end

    # Get the Theme for the specified user, or the default
    #
    # user - User record
    #
    # Returns a Theme
    def for_user(user)
      if user
        by_id(user.theme_id)
      else
        default
      end
    end

    private

    def default_id
      id = Gitlab.config.gitlab.default_theme.to_i

      # Prevent an invalid configuration setting from causing an infinite loop
      if id < THEMES.first.id || id > THEMES.last.id
        APPLICATION_DEFAULT
      else
        id
      end
    end
  end
end