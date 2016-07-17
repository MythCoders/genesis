class ReportCardGradeScale < ApplicationRecord

  has_many :report_card_grades, :dependent => :delete_all
  #TODO: has_and_belongs_to_many :schools

end