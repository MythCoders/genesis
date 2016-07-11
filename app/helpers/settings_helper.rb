module SettingsHelper
  def settings_tabs
    tabs = [{:name => 'district', :partial => 'settings/district', :label => 'Districts'},
            {:name => 'schools', :partial => 'settings/school', :label => 'Schools'},
            {:name => 'grades', :partial => 'settings/grades', :label => 'Grades'},
            {:name => 'enrollcodes', :partial => 'settings/enrollcodes', :label => 'Enrollment Codes'},
            {:name => 'markscales', :partial => 'settings/markscales', :label => 'Report Card Mark Scales'},
            {:name => 'system', :partial => 'settings/system', :label => 'System'}
    ]
  end

  def sample_tabs
    tabs = [{:name => 't1', :partial => 'settings/district', :label => 'Tab #1'},
            {:name => 't2', :partial => 'settings/grades', :label => 'Tab #2'}
    ]
  end

  def settings_nav_menu
    tabs = [
        {:type => 'heading', :label => 'General'},
        {:type => 'link', :name => 'district', :label => 'Districts', :partial => 'settings/district'},
        {:type => 'link', :name => 'school', :label => 'Schools', :partial => 'settings/school'},
        {:type => 'link', :name => 'grade', :label => 'Grades', :partial => 'settings/grades'},
        {:type => 'heading', :label => 'Codes'},
        {:type => 'link', :name => 'enrollcodes', :label => 'Enrollment Codes', :partial => 'settings/enrollcodes'},
        {:type => 'link', :name => 'markscales', :label => 'Report Card Mark Scales', :partial => 'settings/markscales'},
        {:type => 'link', :name => 'system', :label => 'System', :partial => 'settings/system'}
    ]
  end
end
