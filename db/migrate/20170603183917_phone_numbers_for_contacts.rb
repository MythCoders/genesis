class PhoneNumbersForContacts < ActiveRecord::Migration[5.1]
  def change
    create_table :people_phone_numbers do |t|
      t.integer :person_id
      t.string :phone_number, :null => false, :length => 10
      t.string :number_type, :null => false, :length => 20
      t.timestamps
    end

    add_column :student_addresses, :home_phone, :string, :null => true, :length => 10
    add_column :people, :primary_phone_number_id, :integer, :null => true
    add_foreign_key :people, :people_phone_numbers, column: 'primary_phone_number_id'
    add_foreign_key :people_phone_numbers, :people, column: 'person_id'

  end
end
