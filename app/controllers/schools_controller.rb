class SchoolsController < ApplicationController
  def index
    @model = School.includes(:district).all
  end

  def show
    flash[:info] = 'Works?'
    @model = School.find(params[:id])
  end

  def new
    @model = School.new
    @districts = District.order(:name).select('id, name, short_name')
  end

  def create
    @model = School.new(school_params)

    if @model.save
      redirect_to @model
    else
      render 'new'
    end
  end

  def edit
    @model = School.find(params[:id])
    @districts = District.order(:name).select('id, name, short_name')
  end

  def update
    @model = School.find(params[:id])

    if @model.update(school_params)
      redirect_to @model
    else
      render 'edit'
    end
  end

  def destroy
    @model = School.find(params[:id])
    @model.destroy

    redirect_to school_path
  end

  private

  def school_params
    params.require(:model).permit(:id, :name, :short_name, :district_id, :address, :city, :state, :zip_code, :phone_number, :principals_name)
  end
end
