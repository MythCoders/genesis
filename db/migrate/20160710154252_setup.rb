class Setup < ActiveRecord::Migration[5.0]
  def change

    create_table :districts do |t|
      t.string :name, :null => false, :limit => 50
      t.string :address, :limit => 50
      t.string :city, :limit => 30
      t.string :state, :limit => 2
      t.string :zip_code, :limit => 9
      t.string :phone_number, :limit => 10
      t.timestamps
    end

    create_table :schools do |t|
      t.string :name, :null => false, :limit => 50
      t.string :address, :limit => 50
      t.string :city, :limit => 30
      t.string :state, :limit => 2
      t.string :zip_code, :limit => 9
      t.string :phone_number, :limit => 10
      t.references :districts, :foreign_key => true, :column => 'district_id'
      t.timestamps
    end

    create_table :school_years do |t|
      t.string :title, :null => false, :length => 50
      t.string :short_name, :null => false, :length => 5
      t.integer :year, :null => false
      t.date :start_date
      t.date :end_date
      t.date :grade_start_date
      t.date :grade_end_date
      t.date :reg_start_date
      t.date :reg_end_date
      t.references :schools, :foreign_key => true
      t.timestamps
    end

    create_table :grades do |t|
      t.string :title, :length => 100
      t.string :short_name, :length => 10
      #t.integer :next_grade_id, :references => :grades, :null => true
      #t.integer :previous_grade_id, :references => :grades, :null => true
      t.references :grades, :column => 'next_grade_id', :foreign_key => true, :null => true
      t.references :grades, :column => 'previous_grade_id', :foreign_key => true, :null => true
      t.timestamps
    end

    create_table :students do |t|
      t.integer :student_id
      t.string :first_name, :null => false, :limit => 30
      t.string :middle_name, :limit => 50
      t.string :last_name, :null => false, :limit => 30
      t.string :suffix, :limit => 5
      t.string :sex, :limit => 1
      t.date :date_of_birth
      t.timestamps
    end

    create_table :enrollment_codes do |t|
      t.string :title, :length => 100
      t.string :short_name, :length => 10
      t.integer :type
      t.timestamps
    end

    create_table :enrollments do |t|
      t.references :students, :foreign_key => true
      t.references :school_years, :foreign_key => true
      t.references :grades, :foreign_key => true
      t.date :start_date
      t.date :end_date
      t.integer :add_code_id, :references => :enrollment_codes
      t.integer :drop_code_id, :references => :enrollment_codes
      t.integer :next_school_id, :references => :schools
      #calendar
      t.timestamps
    end

    create_table :mark_scales do |t|
      t.string :name, :null => false, :length => 25
      t.text :description, :length => 250
      t.boolean :is_active, :null => false, :default => false
      t.timestamps
    end

    create_table :marks do |t|
      t.string :title, :null => false, :length => 3
      t.string :description, :length => 250
      t.integer :gpa_value, :null => false
      t.integer :breakoff, :null => false
      t.integer :weighted_gpa_value
      t.references :mark_scales, :foreign_key => true
      t.timestamps
    end

    create_table :settings do |t|
      t.string :key, :null => false, :unique => true, :index => true
      t.string :value, :null => false
      t.timestamps
    end

  end
end
