class Enrollment < ApplicationRecord

  belongs_to :student
  belongs_to :school_year
  belongs_to :grade

end