class Course < ActiveRecord::Base

  belongs_to :course_category

  validates :course_category_id, presence: true

end