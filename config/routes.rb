Rails.application.routes.draw do

  devise_for :users
  resources :settings
  resources :schools
  resources :districts
  resources :students
  resources :users

  root 'home#index'
end
