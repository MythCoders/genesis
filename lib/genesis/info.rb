module Genesis
  module INFO

    VER_MAJOR = 0
    VER_MINOR = 1
    VER_REVISION = 3

    VER_BRANCH = 'master'
    VER_BUILD = 173

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
        blank = "-------------------------"
        s = "System Information\n"
        s << [
            [nil,nil],
            ["#{app_name} version", "#{version}-#{VER_BUILD}"],
            ['Ruby version', "#{RUBY_VERSION}-p#{RUBY_PATCHLEVEL} (#{RUBY_RELEASE_DATE}) [#{RUBY_PLATFORM}]"],
            ['Rails version', Rails::VERSION::STRING],
            ['Rails environment', Rails.env],
            ['Database adapter', ActiveRecord::Base.connection.adapter_name],
            ['Database version', "?"]
        ].map {|info| '  %-20s %s' % info}.join("\n") + "\n"
      end
    end

  end
end