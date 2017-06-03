require 'genesis/il8n'
require 'genesis/menu_manager/base'
require 'genesis/module'

Genesis::MenuManager.map :sidebar do |menu|
  menu.push :students, { :controller => 'students', :action => 'index' }, :icon => 'users'
  menu.push :staff, { :controller => 'staff_members', :action => 'index' }, :icon => 'building'
  menu.push :reports, { :controller => 'students', :action => 'index' }, :icon => 'file-excel-o'
  menu.push :help, { :controller => 'students', :action => 'index' }, :icon => 'info-circle'
  menu.push :administration, nil, :caption => 'Administration', :icon => 'cogs'
  menu.push :control_panel, { :controller => 'settings', :action => 'index' }, :parent => :administration

  # menu.push :attendance, { :controller => 'students', :action => 'index' }, :icon => 'calendar-check-o'
  # menu.push :grades, { :controller => 'students', :action => 'index' }, :icon => 'question-circle'
  # menu.push :assessments, { :controller => 'students', :action => 'index' }, :icon => 'puzzle-piece'
  # menu.push :discipline, { :controller => 'students', :action => 'index' }, :icon => 'gavel'
  # menu.push :food_service, { :controller => 'students', :action => 'index' }, :icon => 'cutlery'
  # menu.push :transportation, { :controller => 'students', :action => 'index' }, :icon => 'bus'
  # menu.push :finances, { :controller => 'students', :action => 'index' }, :icon => 'money'
  # menu.push :sports, { :controller => 'students', :action => 'index' }, :icon => 'soccer-ball-o'
  # menu.push :communications, { :controller => 'students', :action => 'index' }, :icon => 'comments'
  # menu.push :medical, { :controller => 'students', :action => 'index' }, :icon => 'hospital-o'
  # menu.push :eLearning, { :controller => 'students', :action => 'index' }, :icon => 'cloud'
  # menu.push :library, { :controller => 'students', :action => 'index' }, :icon => 'book'
end