class SchoolsController < ApplicationController
  def index
    @schools = School.includes(:district).all
  end

  def show
    @school = School.includes(:school_years).order('school_years.year asc').find(params[:id])
  end

  def new
    @school = School.new
    populate_districts
  end

  def create
    @school = School.new(school_params)

    if @school.save
      redirect_to @school
    else
      populate_districts
      render 'new'
    end
  end

  def edit
    @school = School.find(params[:id])
    populate_districts
  end

  def update
    @school = School.find(params[:id])

    if @school.update(school_params)
      redirect_to @school
    else
      populate_districts
      render 'edit'
    end
  end

  def destroy
    @school = School.find(params[:id])
    @school.destroy

    redirect_to school_path
  end

  private

  def populate_districts
    @districts = District.order(:name).select('id, name, short_name')
  end

  def school_params
    params.require(:school).permit(:id, :name, :short_name, :district_id, :address, :city, :state, :zip_code, :phone_number, :principals_name)
  end
end
