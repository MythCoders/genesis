class Attendance < ActiveRecord::Base

  belongs_to :attendance_calendar_day
  belongs_to :attendance_code
  belongs_to :student

end