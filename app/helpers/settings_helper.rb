module SettingsHelper
  def settings_tabs
    tabs = [{:name => 'district', :partial => 'admin/settings/district', :label => 'Districts'},
            {:name => 'schools', :partial => 'admin/settings/school', :label => 'Schools'},
            {:name => 'system', :partial => 'admin/settings/system', :label => 'System'}
    ]
  end
end
