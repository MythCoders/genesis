class AddSecurity < ActiveRecord::Migration[5.0]
  def change
    create_table :roles, force: :cascade do |t|
      t.string :name, limit: 30, default: '', null: false
      t.integer :position, limit: 4
      t.boolean :assignable, default: true
      t.integer :builtin, limit: 4, default: 0, null: false
      t.text :permissions, limit: 65535
      t.string :issues_visibility, limit: 30, default: 'default', null: false
      t.string :users_visibility, limit: 30, default: 'all', null: false
      t.string :time_entries_visibility, limit: 30, default: 'all', null: false
      t.boolean :all_roles_managed, default: true, null: false
      t.text :settings, limit: 65535
      t.timestamps
    end

    create_table :users, force: :cascade do |t|
      t.string :username, limit: 255, default: '', null: false
      t.string :hashed_password, limit: 40, default: '', null: false
      t.string :first_name, limit: 30, default: '', null: false
      t.string :last_name, limit: 255, default: '', null: false
      t.boolean :admin, default: false, null: false
      t.integer :status, limit: 4, default: 1, null: false
      t.datetime :last_login_on
      t.string :language, limit: 5, default: ''
      t.string :type, limit: 255
      t.string :identity_url, limit: 255
      t.string :mail_notification, limit: 255, default: '', null: false
      t.string :salt, limit: 64
      t.boolean :must_change_passwd, default: false, null: false
      t.datetime :passwd_changed_on
      t.timestamps
    end
  end
end
