require 'genesis'

Genesis::Module.register :housing do
  name 'Housing'
  author 'MythCoders'
  author_url 'http://mythcoders.com'
  url '' if respond_to?(:url)
  description 'This is a course module for GeneSIS'
  version '0.1.0'
  #requires_genesis :version_or_higher => '0.0.0'

  #menu :sidebar, :housing, { :controller => 'students', :action => 'index' }, :icon => 'university', :after => :staff
end

ApplicationSetting.populate('sis_housing_is_enabled', 'true', 'boolean', 'housing')