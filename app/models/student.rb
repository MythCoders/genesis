class Student < ApplicationRecord

  has_many :student_enrollments, :autosave => true
  has_many :student_addresses
  has_many :student_notes
  has_many :student_relationships, through: :student_addresses

  accepts_nested_attributes_for :student_enrollments
  accepts_nested_attributes_for :student_addresses

  before_create :assign_student_id
  validates :first_name, presence: true, length: {maximum: 30}
  validates :middle_name, length: {maximum: 30}
  validates :last_name, presence: true, length: {maximum: 30}
  validates :suffix, length: {maximum: 5}
  validates :sex, length: {maximum: 1}

  def primary_address
    self.primary_address_id.nil? ? nil : StudentAddress.find(self.primary_address_id).address
  end

  def any_active_medical_alerts?
    #TODO: medical alerts
    self.student_id == 1010
  end

  def current_grade(format = 'long')
    if self.student_enrollments.any?
      current_enrollments = self.enrollments.where('admission_date <= ? and withdraw_date is null or withdraw_date >= ?', Date.today, Date.today)
      if current_enrollments.count == 1
        grade = current_enrollments.first.school_year_grade.grade
      else
        logger.warn('Student with multiple active enrollments ?', self.id)
        return format == 'long' ? 'MULTIPLE SCHOOLS!' : '!?!'
      end
      format == 'long' ? grade.title : grade.short_name
    else
      format == 'long' ? 'Not Registered' : 'NR'
    end
  end

  def is_registered?
    current_grade('short') == 'NR' ? false : true
  end

  private

  def assign_student_id
    if self.student_id.nil?
      if Student.count == 0
        self.student_id = 1000
      else
        self.student_id = (Student.maximum(:student_id)) + 1
      end
    end
  end

end
