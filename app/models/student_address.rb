class StudentAddress < ApplicationRecord

  belongs_to :student
  belongs_to :address
  has_many :student_relationships

  accepts_nested_attributes_for :address
end