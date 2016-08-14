class SettingsController < ApplicationController
  def index
    edit
    render :action => 'edit'
  end

  def edit
    if params[:tab] == 'grade'
      @grades = Grade.all
    end
    if params[:tab] == 'enrollcodes'
      @enrollment_codes = EnrollmentCode.all

      if @enrollment_codes.count == 0
        @enrollment_codes = EnrollmentCode.new
      end
    end
  end

  def update
    respond_to do |format|
      format.html {redirect_to index}
      format.json {head :no_content}
      format.js {render :layout => false}
    end
  end
end
