class Person < ApplicationRecord

  has_many :student_addresses, through: :student_relationships
  has_many :student_relationships

end