Rails.application.routes.draw do

  devise_for :users

  resources :announcements
  resources :calendars
  resources :districts
  resources :roles
  resources :settings
  resources :schools
  resources :students
  resources :users

  root 'home#index'
end
