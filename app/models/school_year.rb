class SchoolYear < ApplicationRecord

  belongs_to :school
  has_many :grades, :through =>  :school_year_grades
  has_many :school_semesters
  has_many :enrollments
  has_many :school_periods
  #has_and_belongs_to_many :report_card_grade_scales

end