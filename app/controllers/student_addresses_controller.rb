class StudentAddressesController < ApplicationController

  def index
    @student = Student.includes(:addresses).find(params[:student_id])
  end

end