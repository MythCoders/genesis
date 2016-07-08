module GeneSIS
  module INFO

    SYSTEM_NAME = 'GeneSIS 1'

    VER_MAJOR = 0
    VER_MINOR = 1
    VER_REVISION = 0

    def version
      [VER_MAJOR, VER_MINOR, VER_REVISION].compact.join('.')
    end

  end
end