class CreateCoreTables < ActiveRecord::Migration[5.0]
  def change

    create_table :districts do |t|
      t.string :name, :null => false, :limit => 50
      t.string :short_name, :null => false, :limit => 5
      t.string :address, :limit => 50
      t.string :city, :limit => 30
      t.string :state, :limit => 2
      t.string :zip_code, :limit => 9
      t.string :phone_number, :limit => 10
      t.string :superintendents_name, :limit => 50
      t.timestamps
    end

    create_table :schools do |t|
      t.string :name, :null => false, :limit => 50
      t.string :short_name, :null => false, :limit => 5
      t.string :address, :limit => 50
      t.string :city, :limit => 30
      t.string :state, :limit => 2
      t.string :zip_code, :limit => 9
      t.string :phone_number, :limit => 10
      t.string :principals_name, :limit => 50
      t.integer :district_id
      t.timestamps
      t.foreign_key :districts, column: :district_id
    end

    create_table :enrollment_codes do |t|
      t.string :title, :length => 100, :null => false
      t.string :short_name, :length => 10, :null => false
      t.boolean :is_admission, :null => false, :default => true
      t.boolean :is_withdraw, :null => false, :default => false
      t.boolean :is_active, :null => false, :default => false
      t.timestamps
    end

    create_table :settings do |t|
      t.string :key, :null => false, :unique => true, :index => true
      t.string :value, :null => false
      t.timestamps
    end

    create_table :grades do |t|
      t.string :title, :length => 30
      t.string :short_name, :length => 10
      t.integer :sort_order, :null => true
      t.integer :next_grade_id, :null => true
      t.integer :previous_grade_id, :null => true
      t.timestamps
      t.foreign_key :grades, :column => :next_grade_id
      t.foreign_key :grades, :column => :previous_grade_id
    end

    create_table :staff_members do |t|
      t.integer :staff_member_number
      t.string :first_name, :null => false, :limit => 30
      t.string :middle_name, :limit => 30
      t.string :last_name, :null => false, :limit => 30
      t.string :suffix, :limit => 5
      t.string :sex, :limit => 1
      t.date :date_of_birth
      t.timestamps
    end

    create_table :students do |t|
      t.integer :student_number
      t.string :first_name, :null => false, :limit => 30
      t.string :middle_name, :limit => 30
      t.string :last_name, :null => false, :limit => 30
      t.string :suffix, :limit => 5
      t.string :sex, :limit => 1
      t.date :date_of_birth
      t.timestamps
    end

  end
end
