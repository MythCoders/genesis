Rails.application.routes.draw do
  namespace :admin do
    get 'home/index'
  end

  namespace :admin do
    get 'home/show'
  end

  resources :student


  root 'public#index'
end
