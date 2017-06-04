require 'genesis'

Genesis::Module.register :state_reporting_oh do
  name 'State Reporting - OH'
  author 'MythCoders'
  author_url 'http://mythcoders.com'
  url '' if respond_to?(:url)
  description 'This is a state reporting module for GeneSIS designed for Ohio'
  version '0.1.0'
  #requires_genesis :version_or_higher => '0.0.0'

  #menu :sidebar, :courses, { :controller => 'students', :action => 'index' }, :icon => 'university', :after => :staff
end

ApplicationSetting.populate('sis_oh_is_enabled', 'true', 'boolean', 'state_reporting_oh')