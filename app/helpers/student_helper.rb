module StudentHelper

  def student_tabs
    tabs = [{:name => 't1', :partial => 'students/info/info', :label => 'General'}]
  end

  def path_to_student_photo
    if @student.nil?
      #TODO: get path to image for student
    end
    image_path '/images/emptystudent.svg'
  end

  def can_new_student_be_admitted?
    unless SchoolYear.where(:id => active_school_year_id).first.grades.any?
      false
    end
    unless EnrollmentCode.where(:is_admission => true).any?
      false
    end
    true
  end
end
