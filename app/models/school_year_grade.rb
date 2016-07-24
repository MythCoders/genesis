class SchoolYearGrade < ApplicationRecord

  belongs_to :school_year
  belongs_to :grade
  has_many :enrollments

end