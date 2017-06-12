class SchoolYearGrade < ApplicationRecord

  belongs_to :grade
  belongs_to :school_year
  has_many :student_enrollments

  def self.can_be_edited?
    if self.school_year.has_started?
      return false
    end
    #todo check to see if students have been admitted into that grade
    true
  end

end