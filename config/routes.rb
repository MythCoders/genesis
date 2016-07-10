Rails.application.routes.draw do

  resources :settings
  resources :schools
  resources :districts
  resources :students

  root 'public#index'
end
