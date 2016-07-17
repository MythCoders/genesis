class SchoolYear < ApplicationRecord

  belongs_to :school
  has_and_belongs_to_many :grades
  has_and_belongs_to_many :report_card_grade_scales
  has_many :school_semesters
  has_many :enrollments
  has_many :school_periods

end