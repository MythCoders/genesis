class AttendanceTables < ActiveRecord::Migration[5.0]
  def change
    create_table :attendance_calendars do |t|
      t.integer :school_year_id, :null => false
      t.string :title, :null => false, :length => 30
      t.boolean :is_default, :null=> false
      t.timestamps
    end

    create_table :attendance_calendar_days do |t|
      t.integer :attendance_calendar_id, :null => false
      t.date :school_date, :null => false
      t.string :block
      t.integer :minutes
      t.timestamps
      t.foreign_key :attendance_calendars, column: :attendance_calendar_id
    end

    create_table :attendance_codes do |t|
      t.string :title, :null => false, :length => 30
      t.string :short_name, :null => false, :length => 5
      t.integer :sort_order
      t.boolean :usable_by_office, :null => false
      t.boolean :usable_by_teachers, :null => false
      t.boolean :default_for_teachers, :null => false
      t.boolean :is_default_absent, :null => false
      t.timestamps
    end

    create_table :attendance do |t|
      t.integer :attendance_calendar_day_id
      t.integer :attendance_code_id, :null => false
      t.integer :student_id #change this later?
      t.string :comment, :length => 500
      t.integer :minutes_present
      t.timestamps
      t.foreign_key :attendance_calendar_days, column: :attendance_calendar_day_id
      t.foreign_key :attendance_codes, column: :attendance_code_id
    end
  end
end
