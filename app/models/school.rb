class School < ApplicationRecord

  belongs_to :district
  has_many :school_years
  has_and_belongs_to_many :report_card_grade_scale
  has_and_belongs_to_many :grade

  validates :district_id, :presence => true
  validates :name, :presence => true, length: {maximum: 30}
  validates :short_name, :presence => true, length: {maximum: 5}
  validates :address, length: {maximum: 30}
  validates :city, length: {maximum: 30}
  validates :state, length: {maximum: 2}
  validates :zip_code, length: {maximum: 9}
  validates :phone_number, length: {maximum: 10}
  validates :principals_name, length: {maximum: 50}

end
