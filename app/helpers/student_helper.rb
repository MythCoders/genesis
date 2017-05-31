module StudentHelper

  def student_tabs
    tabs = [{:name => 't1', :partial => 'students/info/info', :label => 'General Information'},
            {:name => 't2', :partial => 'students/info/custom', :label => 'Tab'}
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

  def can_new_student_be_admitted?
    return_value = true

    unless SchoolYear.find(session['school_year_id']).grades.any?
      return_value = false
    end

    unless EnrollmentCode.where(:is_admission => true).any?
      return_value = false
    end

    return_value
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
