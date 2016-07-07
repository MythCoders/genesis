class StudentController < ApplicationController
  def index
    @model = Student.all
  end
end
