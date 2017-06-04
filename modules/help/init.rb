require 'genesis'

Genesis::Module.register :help do
  name 'Help'
  author 'MythCoders'
  author_url 'http://mythcoders.com'
  url '' if respond_to?(:url)
  description 'This is a help module for GeneSIS'
  version '0.1.0'
  #requires_genesis :version_or_higher => '0.0.0'

  menu :sidebar, :help, { :controller => 'help', :action => 'index' }, :icon => 'info-circle', :after => :reports
end

ApplicationSetting.populate('sis_help_is_enabled', 'true', 'boolean', 'help')