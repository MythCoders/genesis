class CreateSettings < ActiveRecord::Migration[5.0]
  def change
    create_table :settings do |t|
      t.string :key, null: false, unique: true, index: true
      t.string :value, null: false

      t.timestamps
    end
  end
end
