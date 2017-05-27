module Genesis
  module INFO

    SIS_MAJOR = 0
    SIS_MINOR = 1
    SIS_PATCH = 3
    SIS_BUILD = '173'

    SIS_RELEASE_BRANCH = 'master'

    class << self
      def app_name
        'GeneSIS'
      end
      def version
        "#{SIS_MAJOR}.#{SIS_MINOR}.#{SIS_PATCH}"
      end
      def versioned_name
        "#{app_name} #{version}"
      end
      def build_string
        "#{version} (#{SIS_BUILD})"
      end
      def environment
        s = "Instance Configuration\n"
        s << [
            [nil,nil],
            ["#{app_name} version", "#{build_string}"],
            ['Release branch', "#{SIS_RELEASE_BRANCH}"],
            ['Environment', Rails.env],
            [nil,nil],
            ['Ruby version', "#{RUBY_VERSION}-p#{RUBY_PATCHLEVEL} (#{RUBY_RELEASE_DATE})"],
            ['Rails version', Rails::VERSION::STRING],
            ['Bundler version', Bundler::VERSION],
            [nil,nil],
            ['Platform', "#{RUBY_PLATFORM}"],
            ['Database', ActiveRecord::Base.connection.adapter_name]
        ].map {|info| '  %-20s %s' % info}.join("\n") + "\n"
      end
    end

  end
end
