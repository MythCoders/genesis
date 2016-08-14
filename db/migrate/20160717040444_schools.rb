class Schools < ActiveRecord::Migration[5.0]
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
      t.foreign_key :schools, column: :school_id
    end

    create_table :school_year_grades do |t|
      t.integer :school_year_id
      t.integer :grade_id
      t.timestamps
      t.foreign_key :school_years, column: :school_year_id
      t.foreign_key :grades, column: :grade_id
    end

    create_table :school_year_grade_scales do |t|
      t.integer :school_year_id
      t.integer :report_card_grade_scale_id
      t.boolean :is_default, :default => true
      t.timestamps
      t.foreign_key :school_years, column: :school_year_id
      t.foreign_key :report_card_grade_scales, column: :report_card_grade_scale_id
    end

    create_table :school_semesters do |t|
      t.integer :school_year_id, :null => false
      t.string :title, :null => false, :length => 30
      t.string :short_name, :null => false, :length => 5
      t.integer :sort_order
      t.date :start_date
      t.date :end_date
      t.date :grade_start_date
      t.date :grade_end_date
      t.date :reg_start_date
      t.date :reg_end_date
      t.timestamps
      t.foreign_key :school_years, column: :school_year_id
    end

    create_table :school_quarters do |t|
      t.integer :school_semester_id, :null => false
      t.string :title, :null => false, :length => 30
      t.string :short_name, :null => false, :length => 5
      t.integer :sort_order
      t.date :start_date
      t.date :end_date
      t.date :grade_start_date
      t.date :grade_end_date
      t.date :reg_start_date
      t.date :reg_end_date
      t.timestamps
      t.foreign_key :school_semesters, column: :school_semester_id
    end

    create_table :school_progress_periods do |t|
      t.integer :school_quarter_id, :null => false
      t.string :title, :null => false, :length => 30
      t.string :short_name, :null => false, :length => 5
      t.integer :sort_order
      t.date :start_date
      t.date :end_date
      t.date :grade_start_date
      t.date :grade_end_date
      t.date :reg_start_date
      t.date :reg_end_date
      t.timestamps
      t.foreign_key :school_quarters, column: :school_quarter_id
    end

    create_table :school_periods do |t|
      t.integer :school_year_id, :null => false
      t.string :title, :null => false, :length => 30
      t.string :short_name, :null => false, :length => 5
      t.integer :sort_order
      t.string :block, :length => 10
      t.boolean :takes_attendance
      t.time :start_time
      t.time :end_time
      t.time :start_time_u
      t.time :end_time_u
      t.time :start_time_m
      t.time :end_time_m
      t.time :start_time_t
      t.time :end_time_t
      t.time :start_time_w
      t.time :end_time_w
      t.time :start_time_r
      t.time :end_time_r
      t.time :start_time_f
      t.time :end_time_f
      t.time :start_time_s
      t.time :end_time_s
      t.timestamps
      t.foreign_key :school_years, column: :school_year_id
    end

    create_table :enrollments do |t|
      t.integer :student_id, :null => false
      t.integer :school_year_grade_id, :null => false
      t.integer :admission_code_id, :null => false
      t.date :admission_date, :null => false
      t.integer :withdraw_code_id, :null => true
      t.date :withdraw_date
      t.integer :next_school_id
      t.timestamps

      t.foreign_key :students, :column => :student_id
      t.foreign_key :enrollment_codes, :column => :admission_code_id
      t.foreign_key :school_year_grades, :column => :school_year_grade_id
      t.foreign_key :enrollment_codes, :column => :withdraw_code_id
      t.foreign_key :schools, :column => :next_school_id
    end
  end
end
