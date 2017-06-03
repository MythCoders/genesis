class Person < ApplicationRecord

  has_many :phone_numbers, :foreign_key => 'person_id', :class_name => 'PeoplePhoneNumber'
  has_many :addresses, :through => :student_relationships, :class_name => 'StudentAddress'
  has_many :student_relationships

end