class Person < ApplicationRecord

  has_many :phone_numbers, :foreign_key => 'person_id', :class_name => 'PeoplePhoneNumber'
  has_many :student_addresses, :through => :student_relationships
  has_many :student_relationships

end