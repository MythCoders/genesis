class Grade < ApplicationRecord

  has_and_belongs_to_many :schools
  has_many :enrollments

  validates :title, length: 30
  validates :short_name, length: 10
  validates :sort_order, presence: true

end
