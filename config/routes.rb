Rails.application.routes.draw do

  root :to => 'home#index', :as => 'home'

  devise_for :users, :controllers=> { :sessions => 'sessions' }

  resources :announcements
  resources :calendars
  resources :courses
  resources :districts
  resources :grades
  resources :roles
  resources :settings

  resources :schools do
    resources :school_years
  end

  resources :students
  resources :users

end
