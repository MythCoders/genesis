module StudentHelper

  def path_to_student_photo
    image_path '/images/emptystudent.svg'
  end

  def dave

  end

  def student_sidebar
    sidebar = Sidebar.new
    sidebar.show_image = true
    sidebar.image_path = path_to_student_photo
    sidebar.bottom_navigation = nil

    sidebar.sections = []
    sidebar.sections << navigation_links
    sidebar.sections << action_links
  end

  private

  def navigation_links
    links = []
    links << NavigationItem('Dude', root_path)
  end

  def action_links
    links = []
    links << NavigationItem('Dave', root_path)
  end
end
