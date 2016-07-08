class CreateDistricts < ActiveRecord::Migration[5.0]
  def change
    create_table :districts do |t|
      t.string :name, null: false, length: 50
      t.string :address, length: 50
      t.string :city, length: 30
      t.string :state, length: 2
      t.string :zipcode, length: 9
      t.string :phone_number, length: 10

      t.timestamps
    end
  end
end
