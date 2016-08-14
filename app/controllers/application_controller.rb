class ApplicationController < ActionController::Base
  layout 'base'

  protect_from_forgery with: :exception

  before_action :authenticate_user!
  before_action :check_user_profile
  before_action :populate_session_variables
  
  def populate_session_variables

    #reset_session

    if session['school_id'].nil? or session['school_id'] == 0

      if School.any?
        session['school_id'] = School.first.id
      else
        session['school_id'] = 0
      end

    else
      yrs = SchoolYear.order(:year).where(:school_id => session['school_id'])

      if yrs.any?
        session['school_year_id'] = yrs.first.id
      else
        session['school_year_id'] = 0
      end

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
