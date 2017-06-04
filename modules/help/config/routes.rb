Rails.application.routes.draw do
  get 'help' => 'help#index', :as => 'help'
end
