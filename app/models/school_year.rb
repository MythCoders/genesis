class SchoolYear < ApplicationRecord

  belongs_to :school
  belongs_to :report_card_grade_scale

end