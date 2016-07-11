class Student < ApplicationRecord

  include ApplicationHelper

  #has_and_belongs_to_many :school_years

  def current_grade
    'Not Registered'
  end

  def full_name(format = 1)
    format_person_name(self.first_name, self.middle_name, self.last_name, self.suffix, format)
  end

  private

  def before_create
    if Student.count == 0
      self.student_id = 1000
    else
      self.student_id = (Student.maximum(:student_id)) + 1
    end
  end

end
