module StudentHelper

  def student_tabs
    tabs = [{:name => 't1', :partial => 'students/info/info', :label => 'General Information'},
            {:name => 't2', :partial => 'students/info/address', :label => 'Address & Contacts'},
            {:name => 't3', :partial => 'students/info/custom', :label => 'Medical'},
            {:name => 't4', :partial => 'students/info/notes', :label => 'Notes'},
            {:name => 't5', :partial => 'students/info/custom', :label => 'Tab'},
            {:name => 't5', :partial => 'students/info/custom', :label => 'New Tab'},
            {:name => 't5', :partial => 'students/info/custom', :label => 'Custom Information'},
            {:name => 't5', :partial => 'students/info/custom', :label => 'Hmm'}
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
    return_value = true

    if !SchoolYear.find(session['school_year_id']).grades.any?
      return_value = false
    end

    if EnrollmentCode.where(:is_admission => true).none!
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
