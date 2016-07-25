require_relative 'navigation_item.rb'

class Sidebar

  attr_accessor :dave
  attr_accessor :show_image, :image_path
  attr_accessor :sections
  attr_accessor :bottom_navigation

  def initialize
    self.dave = []
    self.sections = []
    self.bottom_navigation = []
  end

end