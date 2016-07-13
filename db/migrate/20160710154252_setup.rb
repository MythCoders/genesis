class Setup < ActiveRecord::Migration[5.0]
  def change

    create_table :enrollment_codes do |t|
      t.string :title, :length => 100, :null => false
      t.string :short_name, :length => 10, :null => false
      t.boolean :is_admission, :null => false, :default => false
      t.boolean :is_active, :null => false, :dafault => false
      t.timestamps
    end

    create_table :settings do |t|
      t.string :key, :null => false, :unique => true, :index => true
      t.string :value, :null => false
      t.timestamps
    end

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
      t.integer :district_id
      t.timestamps
    end

    create_table :school_years do |t|
      t.integer :school_id, :null => false
      t.string :title, :null => false, :length => 30
      t.string :short_name, :null => false, :length => 5
      t.integer :year, :null => false
      t.date :start_date
      t.date :end_date
      t.date :grade_start_date
      t.date :grade_end_date
      t.date :reg_start_date
      t.date :reg_end_date
      t.timestamps
    end

    create_table :grades do |t|
      t.string :title, :length => 30
      t.string :short_name, :length => 10
      t.integer :sort_order, :null => true
      t.integer :next_grade_id, :null => true
      t.integer :previous_grade_id, :null => true
      t.timestamps
    end

    create_table :mark_scales do |t|
      t.string :name, :null => false, :length => 30
      t.text :description, :length => 250
      t.boolean :is_active, :null => false, :default => false
      t.timestamps
    end

    create_table :marks do |t|
      t.string :title, :null => false, :length => 3
      t.string :description, :length => 250
      t.decimal :gpa_value, :null => false
      t.decimal :score_cut_off, :null => true
      t.decimal :weighted_gpa_scale, :null => true
      t.integer :mark_scale_id, :null => false
      t.timestamps
    end

    create_table :school_year_grades do |t|
      t.integer :school_year_id
      t.integer :grade_id
      t.timestamps
    end

    create_table :school_year_mark_scales do |t|
      t.integer :school_year_id
      t.integer :mark_scale_id
      t.boolean :is_default, :default => true
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

    create_table :enrollments do |t|
      t.integer :student_id, :null => false
      t.integer :school_year_grade_id, :null => false
      t.date :admission_date, :null => false
      t.integer :admission_code_id, :null => false
      t.date :withdraw_date
      t.integer :withdraw_code_id
      t.integer :next_school_id
      t.timestamps
    end

  end
end
