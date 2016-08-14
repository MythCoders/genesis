module Genesis
  module INFO

    VER_MAJOR = 0
    VER_MINOR = 1
    VER_REVISION = 150

    VER_BRANCH = 'master'
    VER_BUILD = 'd6f60b0'

    class << self
      def app_name
        'GeneSIS'
      end
      def version
        "#{VER_MAJOR}.#{VER_MINOR}.#{VER_REVISION}"
      end
      def versioned_name
        "#{app_name} #{version}"
      end
      def environment
        s = "Instance Information:\n"
        s << [
            ["#{app_name} version", "#{version}-#{VER_BUILD}"],
            ['Ruby version', "#{RUBY_VERSION}-p#{RUBY_PATCHLEVEL} (#{RUBY_RELEASE_DATE}) [#{RUBY_PLATFORM}]"],
            ['Rails version', Rails::VERSION::STRING],
            ['Environment', Rails.env],
            ['Database adapter', ActiveRecord::Base.connection.adapter_name],
            ['Branch', VER_BRANCH]
        ].map {|info| '  %-20s %s' % info}.join("\n") + "\n"
      end
    end

  end
end