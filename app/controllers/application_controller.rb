class ApplicationController < ActionController::Base
  layout :determine_layout

  protect_from_forgery with: :exception

  before_action :authenticate_user!
  before_action :check_params
  before_action :populate_session_variables

  include Genesis::MenuManager::MenuController
  helper Genesis::MenuManager::MenuHelper

  def determine_layout
    module_name = self.class.to_s.split('::').first
    return (module_name.eql?('Devise') ? 'pages' : 'application')
  end

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

  def render_404(options={})
    render_error({:message => :notice_file_not_found, :status => 404}.merge(options))
    return false
  end

end
