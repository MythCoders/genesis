class SchoolYearsController < ApplicationController
  def new
    @school_year = SchoolYear.new(:school_id => params[:school_id])
  end

  def edit
    @school_year = SchoolYear.find(params[:id])
  end

  def create
    @school_year = SchoolYear.new(school_year_params)

    if @school_year.save
      redirect_to school_path(@school_year.school)
    else
      render 'new'
    end
  end

  def update
    @school_year = SchoolYear.find(params[:id])

    if @school_year.update(school_year_params)
      redirect_to school_path(@school_year.school)
    else
      render 'edit'
    end

  end

  private

  def school_year_params
    params.require(:school_year).permit(:id, :year, :title, :short_name, :school_id, :start_date, :end_date, :reg_start_date, :reg_end_date, :grade_start_date, :grade_end_date)
  end
end
