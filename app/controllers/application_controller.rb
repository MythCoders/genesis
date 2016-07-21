class ApplicationController < ActionController::Base
  layout 'base'

  protect_from_forgery with: :exception

  before_action :authenticate_user!
  before_action :check_user_profile
  before_action :populate_session_variables


  def populate_session_variables
    if session['schools'].nil?
      #TODO: Change this to schools user has access to
      session['schools'] = School.all
    end

    if session['districts'].nil?
      session['districts'] = District.all
    end

    if session['school_years'].nil?
      session['school_years'] = SchoolYear.all
    end
  end

  def check_user_profile
    if user_signed_in?
      if current_user.first_name.nil?
        flash[:info] = 'Configure your profile!'
      end
    end
  end

end
