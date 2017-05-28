require 'genesis/il8n'
require 'genesis/menu_manager'
require 'genesis/module'

Genesis::MenuManager.map :sidebar do |menu|
  menu.push :students, { :controller => 'students', :action => 'index' }, :icon => 'fa-users'


end