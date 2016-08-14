class ApplicationController < ActionController::Base
  layout 'base'

  protect_from_forgery with: :exception

  before_action :authenticate_user!
  before_action :check_params
  before_action :populate_session_variables

  def check_params
    if params.has_key?(:frc_clr_ses)
      flash[:success] = 'Session cleared'
      reset_session
    end
    if params.has_key?(:frc_sudo_mode)
      flash[:success] = 'Sudo'
    end
  end

  def populate_session_variables

    if session['school_id'].nil? or session['school_id'] == 0

      if School.any?
        session['school_id'] = School.first.id
      else
        session['school_id'] = 0
      end
    end

    if session['school_id'] != 0
      yrs = SchoolYear.order(:year).where(:school_id => session['school_id'])

      if yrs.any?
        session['school_year_id'] = yrs.first.id
      else
        session['school_year_id'] = 0
      end
    end

  end

end
