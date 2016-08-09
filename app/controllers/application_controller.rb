class ApplicationController < ActionController::Base
  layout 'base'

  protect_from_forgery with: :exception

  before_action :authenticate_user!
  before_action :check_user_profile
  before_action :populate_session_variables

  def populate_session_variables

    #TODO: Change logic to reflect schools/years current users has access to
    if session['districts'].nil?
      session['districts'] = District.all
    end

    if session['schools'].nil?
      session['schools'] = School.all
    end

    if session['school_id'].nil?
      if School.any?
        if session['schools'].nil?
          session['schools'] = School.all
        end
        session['school_id'] = session['schools'].first()[:id]
      else
        session['school_id'] = 0
      end
    end

    if session['school_years'].nil?
      session['school_years'] = SchoolYear.where(school_id: session['school_id'])
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
