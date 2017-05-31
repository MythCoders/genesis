class StudentAddressesController < ApplicationController

  def index
    @student = Student.includes(:student_addresses).find(params[:student_id])
  end

end