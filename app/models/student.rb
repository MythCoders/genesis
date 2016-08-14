class Student < ApplicationRecord
  include ApplicationHelper

  has_many :enrollments, :autosave => true
  accepts_nested_attributes_for :enrollments, allow_destroy: false

  before_create :assign_student_id

  validates :first_name, presence: true, length: {maximum: 30}
  validates :middle_name, length: {maximum: 30}
  validates :last_name, presence: true, length: {maximum: 30}
  validates :suffix, length: {maximum: 5}
  validates :sex, length: {maximum: 1}

  def full_name(format = 1)
    format_person_name(self.first_name, self.middle_name, self.last_name, self.suffix, format)
  end

  def current_grade(format = 'long')
    if self.enrollments.any?
      current_enrollments = self.enrollments.where('admission_date <= ? and withdraw_date is null or withdraw_date >= ?', Date.today, Date.today)
      if current_enrollments.count == 1
        grade = current_enrollments.first.school_year_grade.grade
      else
        logger.warn('Student with multiple active enrollments ?', self.id)
        return 'long' ? 'MULTIPLE SCHOOLS!' : '!?!'
      end
      format == 'long' ? grade.title : grade.short_name
    else
      format == 'long' ? 'Not Registered' : 'NR'
    end
  end

  def is_registered
    current_grade('short') == 'NR' ? true : false
  end

  private

  def assign_student_id
    if self.student_id.nil?
      if Student.count == 0
        self.student_id = 1000
      else
        self.student_id = (Student.maximum(self.student_id)) + 1
      end
    end
  end

end
