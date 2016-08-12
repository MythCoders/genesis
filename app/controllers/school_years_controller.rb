class SchoolYearsController < ApplicationController
  def index
  end

  def show
  end

  def new
    @model = SchoolYear.new(:school_id => params[:school_id])
  end

  def edit
  end

  def create
  end

  def update
  end
end
