class CoreTables < ActiveRecord::Migration[5.0]
  def change

    create_table :enrollment_codes do |t|
      t.string :title, :length => 100, :null => false
      t.string :short_name, :length => 10, :null => false
      t.boolean :is_admission, :null => false, :default => false
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

    create_table :students do |t|
      t.integer :student_id
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
