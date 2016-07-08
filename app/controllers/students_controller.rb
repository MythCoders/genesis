class StudentsController < ApplicationController
  def index
    @model = Student.all
  end

  def show
    
  end
end
