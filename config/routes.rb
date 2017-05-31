Rails.application.routes.draw do

  root :to => 'home#index', :as => 'home'

  devise_for :users, :path_prefix => 'security'

  resources :announcements
  #resources :calendars
  resources :districts
  resources :grades
  resources :roles
  resources :settings

  resources :schools do
    resources :school_years
  end

  resources :students do
    resources :student_addresses, :path => 'addresses', :as => 'addresses'
    resources :student_notes, :path => 'notes', :as => 'notes'
  end

  resources :student_relationships
  resources :users

end
