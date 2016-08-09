module StudentHelper

  def student_tabs
    tabs = [{:name => 't1', :partial => 'students/info', :label => 'General Information'},
            {:name => 't2', :partial => 'students/address', :label => 'Address & Contacts'},
            {:name => 't3', :partial => 'students/custom', :label => 'Medical'},
            {:name => 't4', :partial => 'students/notes', :label => 'Notes'},
            {:name => 't5', :partial => 'students/info', :label => '?'}
    ]
  end

  def path_to_student_photo
    if @student.nil?
      #TODO: get path to image for student
    end
    image_path '/images/emptystudent.svg'
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

  def can_new_student_be_admitted
    true
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
