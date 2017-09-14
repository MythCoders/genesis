require 'genesis/il8n'
require 'genesis/menu_manager/base'
require 'genesis/module'

# Populate required application settings
ApplicationSetting.populate('sis_core_name', 'dev', 'string', 'core')
ApplicationSetting.populate('sis_core_is_enabled', 'true', 'boolean', 'core')
ApplicationSetting.populate('sis_core_display_flagged_announcements_on_login', 'true', 'boolean', 'core')

# Populate menus
Genesis::MenuManager.map :sidebar do |menu|
  menu.push :students, { :controller => 'students', :action => 'index' }, :icon => 'users'
  menu.push :staff, { :controller => 'staff_members', :action => 'index' }, :icon => 'building'
  menu.push :grades, { :controller => 'home', :action => 'grades' }, :icon => 'file-zip-o'
  menu.push :reports, { :controller => 'reports', :action => 'index' }, :icon => 'bar-chart'

  menu.push :administration, nil, :caption => 'Administration', :icon => 'cog', :html => { 'data-toggle': 'collapse' }
  menu.push :announcements, { :controller => 'announcements', :action => 'index' }, :parent => :administration, :caption => 'Announcements'
  menu.push :districts, { :controller => 'districts', :action => 'index' }, :parent => :administration, :caption => 'Districts'
  menu.push :grades, { :controller => 'grades', :action => 'index' }, :parent => :administration, :caption => 'Grade Levels'
  menu.push :schools, { :controller => 'schools', :action => 'index' }, :parent => :administration, :caption => 'Schools'
  menu.push :settings, { :controller => 'application_settings', :action => 'index' }, :parent => :administration, :caption => 'Settings'

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