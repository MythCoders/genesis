class CreateNotifications < ActiveRecord::Migration[5.1]
  def change
    create_table :notifications do |t|
      t.string :title, :null => false
      t.string :body
      t.integer :user_id
      t.boolean :has_been_read

      t.timestamps
    end

    add_foreign_key :notifications, :users, column: 'user_id'
  end
end