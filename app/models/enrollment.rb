class Enrollment < ApplicationRecord

  belongs_to :student
  #belongs_to :school
  belongs_to :school_year_grade
  belongs_to :admission_code, :class_name => 'EnrollmentCode'
  belongs_to :withdraw_code, :class_name => 'EnrollmentCode'

  before_create :verify_no_date_overlap

  validates :admission_date, presence: true
  validates_presence_of :admission_code
  validates_presence_of :withdraw_code, :if => :is_withdraw_code_required?

  def verify_no_date_overlap

  end

  def is_withdraw_code_required?
    !withdraw_date.nil?
  end

end