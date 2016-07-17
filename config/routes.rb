Rails.application.routes.draw do

  get 'account/login'

  get 'account/logout'

  get 'account/lost_password'

  get 'account/password_recovery'

  get 'account/register'

  resources :settings
  resources :schools
  resources :districts
  resources :students

  root 'public#index'
end
