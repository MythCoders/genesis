class HomeController < ApplicationController
  def index
    session[:first_time] ||= Time.now
  end

  def update_school
    if request.post?
      session['school_id'] = params[:school_id]
      session['school_year_id'] = params[:school_year_id]
    end
  end
end
