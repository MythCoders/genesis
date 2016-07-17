class Enrollment < ApplicationRecord

  belongs_to :student
  belongs_to :school_year
  belongs_to :grade
  belongs_to :admission_code, class: 'EnrollmentCode'
  belongs_to :withdraw_code, class: 'EnrollmentCode'

  validates :admission_date, date: true, presence: true
  validates :withdraw_date, date: true

  validates_presence_of :admission_code
  validates_presence_of :withdraw_code, :if => :is_withdraw_code_required?

  def is_withdraw_code_required?
    !withdraw_date.nil?
  end

end