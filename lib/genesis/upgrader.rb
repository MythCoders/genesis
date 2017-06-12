module Genesis
  class Upgrader
    puts "GeneSIS #{Genesis::INFO::SIS_MAJOR} upgrade tool"
    puts "Your version is #{Genesis::INFO.version}"
    puts "Latest available version for GeneSIS #{Genesis::INFO::SIS_MAJOR} is _"

    def latest_version?
      false
    end
  end
end