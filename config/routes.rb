Rails.application.routes.draw do

  devise_for :users

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

  root 'home#index'
end
