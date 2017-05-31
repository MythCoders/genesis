class CreateSupportingStudentTables < ActiveRecord::Migration[5.0]
  def change
    create_table :addresses do |t|
      t.string :street, :null => false, :limit => 50
      t.string :city, :null => false, :limit => 30
      t.string :state, :limit => 2
      t.string :zip_code, :null => false, :limit => 9
      t.string :phone_number, :limit => 10
    end

    create_table :student_addresses do |t|
      t.integer :student_id, :null => false
      t.integer :address_id, :null => false
      t.boolean :is_mailing
      t.boolean :is_residential
      t.boolean :is_bus_pickup
      t.boolean :is_bus_drop_off
      t.timestamps
      t.foreign_key :students, :column => :student_id
      t.foreign_key :addresses, :column => :address_id
    end

    add_column :students, :primary_address_id, :integer, null: true
    add_foreign_key :students, :student_addresses, column: 'primary_address_id'

    create_table :people do |t|
      t.string :first_name, :limit => 30
      t.string :middle_name, :limit => 30
      t.string :last_name, :limit => 30
      t.string :email_address, :limit => 60
      t.timestamps
    end

    create_table :student_relationships do |t|
      t.integer :student_address_id, :null => false
      t.integer :person_id, :null => false
      t.boolean :has_custody
      t.boolean :is_emergency
      t.string :relation
      t.integer :contact_order
      t.timestamps
      t.foreign_key :student_addresses, :column => :student_address_id
      t.foreign_key :people, :column => :person_id
    end

    create_table :student_notes do |t|
      t.integer :student_id, :null => false
      t.string :title, :null => false, :limit => 100
      t.string :body
      t.boolean :is_flagged
      t.timestamps
      t.foreign_key :students, :column => :student_id
    end
  end
end