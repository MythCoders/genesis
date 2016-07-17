class Enrollment < ApplicationRecord

  belongs_to :student
  belongs_to :school_year
  belongs_to :grade
  belongs_to :enrollment_code
  belongs_to :enrollment_code

end