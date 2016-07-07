Rails.application.routes.draw do
  get 'student/index'

  root 'public#index'
end
