class Announcement < ApplicationRecord

  scope :active, -> { where('start_date <= ? and ((end_date is null) or (end_date >= ?))', DateTime.now.at_beginning_of_day, DateTime.now.at_beginning_of_day) }
  scope :active_flagged, -> { where('start_date <= ? and ((end_date is null) or (end_date >= ?)) and is_flagged = true', DateTime.now.at_beginning_of_day, DateTime.now.at_beginning_of_day) }

  validates :title, presence: true, length: {maximum: 50}
  validates :body, presence: true
  validates :start_date, presence: true

end