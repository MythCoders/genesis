class PublicController < ApplicationController
  def index
    flash[:system_alert] = 'This is a system alert!'
  end
end
