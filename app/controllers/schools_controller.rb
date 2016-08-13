class SchoolsController < ApplicationController
  def index
    @schools = School.includes(:district).all
  end

  def show
    flash[:info] = 'Works?'
    @school = School.includes(:school_years).find(params[:id])
  end

  def new
    @school = School.new
    @districts = District.order(:name).select('id, name, short_name')
  end

  def create
    @school = School.new(school_params)

    if @school.save
      redirect_to @school
    else
      @districts = District.order(:name).select('id, name, short_name')
      render 'new'
    end
  end

  def edit
    @school = School.find(params[:id])
    @districts = District.order(:name).select('id, name, short_name')
  end

  def update
    @school = School.find(params[:id])

    if @school.update(school_params)
      redirect_to @school
    else
      @districts = District.order(:name).select('id, name, short_name')
      render 'edit'
    end
  end

  def destroy
    @school = School.find(params[:id])
    @school.destroy

    redirect_to school_path
  end

  private

  def school_params
    params.require(:school).permit(:id, :name, :short_name, :district_id, :address, :city, :state, :zip_code, :phone_number, :principals_name)
  end
end
