Rails.application.routes.draw do

  root :to => 'home#index', :as => 'home'
  get 'help' => 'help#index', :as => 'help'
  get 'reports' => 'reports#index', :as => 'reports'

  devise_for :users, :path_prefix => 'security'

  resources :announcements
  resources :application_settings, :path => 'control_panel', :as => 'settings'
  resources :districts
  resources :grades
  resources :roles
  resources :users
  resources :settings
  resources :staff_members, :path => 'staff'

  resources :schools do
    resources :school_years
  end

  resources :students do
    resources :student_addresses, :path => 'addresses', :as => 'addresses'
    resources :student_notes, :path => 'notes', :as => 'notes'
    resources :student_enrollments, :path => 'enrollments', :as => 'enrollments'
    resources :student_relationships, :path => 'contacts', :as => 'relationships'
  end


end
