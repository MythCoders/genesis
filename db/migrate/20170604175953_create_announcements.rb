class CreateAnnouncements < ActiveRecord::Migration[5.1]
  def change
    create_table :announcements do |t|
      t.string :title, :null => false, :length => 50
      t.string :body, :null => false
      t.datetime :start_date, :null => false
      t.datetime :end_date
      t.boolean :is_flagged, :null => false, :default => false
      t.timestamps
    end
  end
end
