class Grade < ApplicationRecord

  has_many :school_years, :through => :school_year_grades

  validates :title, length: {maximum: 30}
  validates :short_name, length: {maximum: 10}
  validates :sort_order, presence: true

end
