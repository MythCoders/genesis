class StudentRelationship < ApplicationRecord

  belongs_to :person
  belongs_to :student_address

end