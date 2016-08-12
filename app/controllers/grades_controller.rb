class GradesController < ApplicationController
  def index
    @model = Grade.order(:sort_order).all
  end

  def edit
    @model = Grade.find(params[:id])
    @grades = Grade.order(:sort_order).all
  end

  def new
    @model = Grade.new
    @grades = Grade.order(:sort_order).all
  end

  def create
    @model = Grade.new(grade_params)

    if @model.save
      flash[:success] = 'Your record was created successfully!'
      redirect_to index
    else
      @grades = Grade.order(:sort_order).all
      render 'new'
    end
  end

  def update
    @model = Grade.find(params[:id])

    if @model.update(grade_params)
      flash[:success] = ''
      redirect_to index
    else
      render 'edit'
    end
  end

  private

  def grade_params
    params.require(:model).permit(:id, :title, :short_name, :sort_order, :next_grade_id, :previous_grade_id)
  end
end
