class SchoolYearGrade < ApplicationRecord

  belongs_to :grade
  belongs_to :school_year
  has_many :enrollments

end