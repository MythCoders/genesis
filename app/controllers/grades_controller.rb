class GradesController < ApplicationController
  def index
    @model = Grade.order(:sort_order).all
  end

  def show
    @grade = Grade.find(params[:id])
  end

  def edit
    @grade = Grade.find(params[:id])
    @grades = Grade.order(:sort_order).all
  end

  def new
    @model = Grade.new
    @grades = Grade.order(:sort_order).all
  end

  def create
    @grade = Grade.new(grade_params)

    if @grade.save
      redirect_to @grade
    else
      @grades = Grade.order(:sort_order).all
      render 'new'
    end
  end

  def update

  end

  private

  def grade_params
    params.require(:grade).permit(:id, :title, :short_name, :sort_order, :next_grade_id, :previous_grade_id)
  end
end
