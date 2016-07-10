class ApplicationController < ActionController::Base
  layout 'base'

  protect_from_forgery with: :exception
end
