class StudentsController < ApplicationController
  include StudentHelper

  def index
    @q = Student.ransack(params[:q])
    @students = @q.result
  end

  def show
    @student = Student.includes(:addresses).find(params[:id])
    session[:student_id] = @student.id
  end

  def edit
    @student = Student.find(params[:id])
  end

  def new
    unless can_new_student_be_admitted?
      flash[:error] = 'No students can be admitted for the currently selected school year.'
      redirect_to action: :index
    end

    @student = Student.new
    @student.enrollments << StudentEnrollment.new
    @student.addresses << StudentAddress.new do |sa|
      sa.address = Address.new
    end

    populate_select_lists
    @type = 'slow'
  end

  def create
    @student = Student.new(student_params)

    if @student.save
      redirect_to @student
    else
      populate_select_lists
      render 'new', :type => @type
    end
  end

  private

  def build_new_student
    @student = Student.new
    if @student.nil?
      render :action
    end
  end

  def populate_select_lists
    @grades = SchoolYear.find(session['school_year_id']).grades
    @admission_codes = EnrollmentCode.where(:is_admission => true)

    if params[:type].nil? || get_user_pref == 'quick'
      @type = 'quick'
    else
      @type = 'slow'
    end
  end

  def student_params
    params.require(:student).permit(:id, :student_id, :first_name, :middle_name, :last_name, :suffix, :date_of_birth, :sex,
                                    { :enrollments_attributes => [:id, :student_id, :school_year_grade_id, :admission_date, :admission_code_id, :withdraw_date, :withdraw_code_id, :next_school_id] },
                                    { :addresses_attributes => [:id, :student_id, :address_id, :is_mailing, :is_residential, :is_bus_pickup, :is_bus_drop_off,
                                                                        { :address_attributes => [:id, :street, :city, :state, :zip_code, :phone_number] } ] })
  end

  def get_user_pref
    #todo: load preference
    'slow'
  end

  def find_student
    @student = Student.find(params[:id])
  rescue
    render_404
  end

end
