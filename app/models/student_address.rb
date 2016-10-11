class StudentAddress < ApplicationRecord

  has_many :persons, through: :student_relationships
  has_many :student_relationships

end



