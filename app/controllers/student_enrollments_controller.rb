class StudentEnrollmentsController < ApplicationController

  def index
    @student = Student.includes(:enrollments).find(params[:student_id])
  end

end