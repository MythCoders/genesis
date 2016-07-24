class Enrollment < ApplicationRecord

  belongs_to :student
  belongs_to :school_year
  belongs_to :school_year_grade
  belongs_to :admission_code, :class_name => 'EnrollmentCode'
  belongs_to :withdraw_code, :class_name => 'EnrollmentCode'

  validates :admission_date, presence: true

  validates_presence_of :admission_code
  validates_presence_of :withdraw_code, :if => :is_withdraw_code_required?

  def is_withdraw_code_required?
    !withdraw_date.nil?
  end

end