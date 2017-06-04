class ApplicationSettings < ActiveRecord::Migration[5.1]
  def change

    create_table :application_settings do |t|
      t.string :key, :null => false, :length => 100
      t.string :value, :null => true
      t.string :value_type, :null => false, :length => 25
      t.string :module_name, :null => false, :length => 25
      t.timestamps
    end

    drop_table :settings

  end
end
