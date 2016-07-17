class AccountController < ApplicationController
  helper :custom_fields

  # prevents login action to be filtered by check_if_login_required application scope filter
  #skip_before_filter :check_if_login_required, :check_password_change


  def login

  end

  def logout
  end

  def lost_password
  end

  def password_recovery
  end

  def register
  end
end
