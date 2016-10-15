module SchoolYearHelper
  def can_grade_be_edited(grade)
    if @school_year.has_started?
      return false
    end
    #todo check to see if students have been admitted into that grade
    true
  end
end
