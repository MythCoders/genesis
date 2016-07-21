class Grade < ApplicationRecord

  has_and_belongs_to_many :schools
  has_many :enrollments

  validates :title, length: {maximum: 30}
  validates :short_name, length: {maximum: 10}
  validates :sort_order, presence: true

end
