class Grade < ApplicationRecord

  has_and_belongs_to_many :schools
  has_many :enrollments

end
