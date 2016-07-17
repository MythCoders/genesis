class Schools < ActiveRecord::Migration[5.0]
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
      t.string :principals_name, :limit => 50
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
    end
  end
end
