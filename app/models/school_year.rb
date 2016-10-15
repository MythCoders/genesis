class SchoolYear < ApplicationRecord

  belongs_to :school
  has_many :grades, through: :school_year_grades
  has_many :school_year_grades
  has_many :school_semesters
  has_many :enrollments, through: :school_year_grades
  has_many :school_periods

  accepts_nested_attributes_for :school_year_grades, allow_destroy: true

  validates :title, length: {maximum: 30}, presence: true
  validates :short_name, length: {maximum: 5}, uniqueness: true, presence: true

  def is_in_session?
    has_started? and !has_ended?
  end

  def has_started?
    if start_date.nil?
      false
    else
      start_date <= Date.today
    end
  end

  def has_ended?
    if end_date.nil? or !has_started?
      false
    else
      end_date <= Date.now
    end
  end

  def is_grade_being_taught?(grade)
    grades.where(:short_name => grade).any?
  end

end