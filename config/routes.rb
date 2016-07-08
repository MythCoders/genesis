Rails.application.routes.draw do

  namespace :admin do
    get 'home/index'
    get 'home/show'
  end

  resources :schools, path: 'admin/schools'
  resources :districts, path: 'admin/districts'

  resources :students

  root 'public#index'
end
