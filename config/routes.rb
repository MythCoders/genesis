Rails.application.routes.draw do

  root :to => 'home#index', :as => 'home'
  get 'grades' => 'home#grades', :as => 'grades'
  get 'help' => 'help#index', :as => 'help'
  get 'reports' => 'reports#index', :as => 'reports'

  devise_for :users, :path_prefix => 'security'

  resources :announcements, :path => 'admin/announcements'
  resources :districts, :path => 'admin/districts'
  resources :grades, :path => 'admin/grades'
  resources :roles, :path => 'admin/roles'
  resources :users, :path => 'admin/users'
  resources :application_settings, :path => 'admin/settings'
  resources :staff_members, :path => 'admin/staff'

  resources :schools, :path => 'admin/schools' do
    resources :school_years, :path => 'years'
  end

  resources :students do
    get 'delete'
    post 'delete'

    resources :student_addresses, :path => 'addresses', :as => 'addresses' do
      resources :student_relationships, :path => 'contacts', :as => 'contacts'
    end

    resources :student_notes, :path => 'notes', :as => 'notes'
    resources :student_enrollments, :path => 'enrollments', :as => 'enrollments'

  end

end
