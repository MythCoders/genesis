class HomeController < ApplicationController
  def index
    session[:first_time] ||= Time.now
  end
end
