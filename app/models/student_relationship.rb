class StudentRelationship < ApplicationRecord

  default_scope { order(:contact_order) }

  belongs_to :student_address
  belongs_to :person
end