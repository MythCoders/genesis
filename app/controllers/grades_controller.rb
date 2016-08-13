class GradesController < ApplicationController
  def index
    @model = Grade.order(:sort_order).all
  end

  def edit
    @grade = Grade.find(params[:id])
    @grades = Grade.order(:sort_order).all
  end

  def new
    @grade = Grade.new
    @grades = Grade.order(:sort_order).all
  end

  def create
    @grade = Grade.new(grade_params)

    if @grade.save
      flash[:success] = 'Your record was created successfully!'
      index
    else
      @grades = Grade.order(:sort_order).all
      render 'new'
    end
  end

  def update
    @grade = Grade.find(params[:id])

    if @grade.update(grade_params)
      flash[:success] = ''
      index
    else
      render 'edit'
    end
  end

  private

  def grade_params
    params.require(:grade).permit(:id, :title, :short_name, :sort_order, :next_grade_id, :previous_grade_id)
  end
end
