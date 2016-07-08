class Student < ApplicationRecord

  private

  def before_create
    if Student.count == 0
      self.student_id = 1000
    else
      self.student_id = (Student.maximum(:student_id)) + 1
    end
  end

end
