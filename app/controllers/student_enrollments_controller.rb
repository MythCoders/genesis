class StudentEnrollmentsController < ApplicationController

  def index
    @student = Student.includes(:student_enrollments).find(params[:student_id])
  end

end