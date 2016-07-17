class SchoolsController < ApplicationController
  def index
    if District.any?
      flash[:system_alert] = 'No districts have been configured!'
      @model = {}
    else
      @model = School.all
    end
  end

  def show
  end

  def new
  end
end
