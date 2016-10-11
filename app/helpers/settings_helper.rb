module SettingsHelper
  def settings_tabs
    tabs = [{:name => 'grades', :partial => 'settings/grades', :label => 'Grades'},
            {:name => 'enrollcodes', :partial => 'settings/enrollcodes', :label => 'Enrollment Codes'},
            {:name => 'markscales', :partial => 'settings/markscales', :label => 'Report Card Mark Scales'},
            {:name => 'system', :partial => 'settings/system', :label => 'System Settings'}
    ]
  end
end
