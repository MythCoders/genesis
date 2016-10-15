class StudentRelationshipContactOrder < ActiveRecord::Migration[5.0]
  def change
    add_column :student_relationships, :contact_order, :integer
    add_column :students, :primary_address_id, :integer, null: true
    add_foreign_key :students, :student_addresses, column: 'primary_address_id'
  end
end