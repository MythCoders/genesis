class SchoolYear < ApplicationRecord

  belongs_to :school
  belongs_to :report_card_grade_scale
  has_many :school_semesters
  has_many :enrollments
  has_many :school_periods

end