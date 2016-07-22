class StudentsController < ApplicationController
  def index
    @q = Student.ransack(params[:q])
    @students = @q.result
  end

  def show
    
  end
end
