class StudentsController < ApplicationController
  def index
    @q = Student.ransack(params[:q])
    @students = @q.result
  end

  def show
    @student = Student.find(params[:id])
  end

  def new
    if params[:type].nil? || get_user_pref == 'quick'
      @type = 'quick'
    else
      @type = 'slow'
    end

    @student = Student.new
    @student.enrollments << Enrollment.new

    @grades = SchoolYearGrade.where(school_year_id: 1)
    @admission_codes = EnrollmentCode.where(:is_admission => true)
  end

  def create
    @student = Student.new(student_params)

    if @student.save
      redirect_to @student
    else
      render 'new', :type => @type
    end
  end

  private

  def student_params
    params.require(:student).permit(:id, :student_id, :first_name, :middle_name, :last_name, :suffix, :date_of_birth, :sex, {:enrollments_attributes => [:id, :student_id, :school_year_grade_id, :admission_date, :admission_code_id, :withdraw_date, :withdraw_code_id, :next_school_id]})
  end

  def get_user_pref
    'quick'
  end

end
