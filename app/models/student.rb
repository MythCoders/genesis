class Student < ApplicationRecord
  include ApplicationHelper

  has_many :enrollments

  validates :first_name, presence: true, length: 30
  validates :middle_name, length: 30
  validates :last_name, presence: true, length: 30
  validates :suffix, length: 5
  validates :sex, length: 1
  validates :date_of_birth, date: true


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
