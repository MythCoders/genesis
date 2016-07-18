class ApplicationController < ActionController::Base
  layout 'base'

  protect_from_forgery with: :exception

  before_action :authenticate_user!
  before_action :check_user_profile
  before_action :get_districts

  def get_districts
    @schools = School.select('id, name, short_name')
    @districts = District.select('id, name, short_name')
  end

  def check_user_profile
    if user_signed_in?
      if current_user.first_name.nil?
        flash[:info] = 'Configure your profile!'
      end
    end
  end

end
