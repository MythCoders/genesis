class Student < ApplicationRecord

  has_many :enrollments, :class_name => 'StudentEnrollment', :autosave => true
  has_many :addresses, :class_name => 'StudentAddress'
  has_many :notes, :class_name => 'StudentNote'
  has_many :relationships, through: :student_addresses, :class_name => 'StudentRelationship'

  accepts_nested_attributes_for :enrollments
  accepts_nested_attributes_for :addresses

  before_create :assign_student_id
  validates :first_name, presence: true, length: {maximum: 30}
  validates :middle_name, length: {maximum: 30}
  validates :last_name, presence: true, length: {maximum: 30}
  validates :suffix, length: {maximum: 5}
  validates :sex, length: {maximum: 1}

  def primary_address
    self.primary_address_id.nil? ? nil : StudentAddress.find(self.primary_address_id).address
  end

  def current_grade(format = 'long')
    if self.enrollments.any?
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

  def has_alert?
    true
  end

  def has_flagged_note?
    self.notes.where(:is_flagged => true).any?
  end

  def has_medical_alert?
    true
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
