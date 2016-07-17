class EnrollmentCode < ApplicationRecord

  validates :title, presence: true, length: 100
  validates :short_name, presence: true, length: 10
  validates :is_admission, presence: true, bool: true
  validates :is_active, presence: true, bool: true

end