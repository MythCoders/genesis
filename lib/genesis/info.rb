module Genesis
  module INFO

    SIS_MAJOR = 0
    SIS_MINOR = 1
    SIS_PATCH = 4
    SIS_COMMIT = '45ef44dad9e111e1120b79661098d2666ccfe44c'
    SIS_BUILD = SIS_COMMIT[0,8]
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
        s = "#{app_name} Instance Configuration\n"
        s << [
            [nil,nil],
            ['Version', version],
            ['Build', SIS_BUILD],
            ['Commit', SIS_COMMIT],
            ['Branch', "#{SIS_RELEASE_BRANCH}"],
            [nil,nil],
            ['Environment', Rails.env],
            ['Platform', "#{RUBY_PLATFORM}"],
            ['Database', ActiveRecord::Base.connection.adapter_name],
            [nil,nil],
            ['Ruby version', "#{RUBY_VERSION}-p#{RUBY_PATCHLEVEL} (#{RUBY_RELEASE_DATE})"],
            ['Rails version', Rails::VERSION::STRING],
            ['Bundler version', Bundler::VERSION]
        ].map {|info| '  %-25s %s' % info}.join("\n") + "\n"
      end
      def installed_modules
        s = "Installed Modules\n"
        plugins = Genesis::Module.all
        if plugins.any?
          s << plugins.map {|mod| "  %-25s %s" % [mod.id.to_s, mod.version.to_s]}.join("\n")
        else
          s << '  no modules installed'
        end
        # s << [
        #     [nil,nil],
        #     ['Admissions', '0.0.0'],
        #     ['Assessment', '0.0.0'],
        #     ['Attendance', '0.0.0'],
        #     ['Communications', '0.0.0'],
        #     ['Courses', '0.0.0'],
        #     ['Data Warehouse', '0.0.0'],
        #     ['Discipline', '0.0.0'],
        #     ['eLearning', '0.0.0'],
        #     ['Finances', '0.0.0'],
        #     ['Food Service', '0.0.0'],
        #     ['Housing', '0.0.0'],
        #     ['Library', '0.0.0'],
        #     ['Medical', '0.0.0'],
        #     ['Parent Portal', '0.0.0'],
        #     ['Report Designer', '0.0.0'],
        #     ['Sports', '0.0.0'],
        #     ['State Reporting - OH', '0.0.0'],
        #     ['Student Portal', '0.0.0'],
        #     ['Transportation', '0.0.0'],
        # ].map {|info| '  %-25s %s' % info}.join("\n") + "\n"
      end
    end

  end
end
