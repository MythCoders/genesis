module Genesis
  module INFO

    VER_MAJOR = 0
    VER_MINOR = 1
    VER_REVISION = 0

    class << self
      def app_name; 'GeneSIS' end
      def version; "#{VER_MAJOR}.#{VER_MINOR}.#{VER_REVISION}" end
      def versioned_name; "#{app_name} #{version}" end
    end

  end
end