class Student < ApplicationRecord
  include ApplicationHelper

  has_many :enrollments, :autosave => true
  accepts_nested_attributes_for :enrollments, allow_destroy: false

  before_create :before_create

  validates :first_name, presence: true, length: {maximum: 30}
  validates :middle_name, length: {maximum: 30}
  validates :last_name, presence: true, length: {maximum: 30}
  validates :suffix, length: {maximum: 5}
  validates :sex, length: {maximum: 1}

  def full_name(format = 1)
    format_person_name(self.first_name, self.middle_name, self.last_name, self.suffix, format)
  end

  def current_grade(format = 'long')
    if format == 'long'
      'Senior'
    else
      '12'
    end
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
