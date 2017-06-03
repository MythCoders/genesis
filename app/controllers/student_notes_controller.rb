class StudentNotesController < ApplicationController

  prepend_view_path 'app/views/students/notes'

  def index
    @student = Student.includes(:notes).find(params[:student_id])
  end

end