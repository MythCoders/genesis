class SettingsController < ApplicationController
  def index
    edit
    render :action => 'edit'
  end

  def edit
    if params[:tab] == 'grade'
      @grades = Grade.all
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
