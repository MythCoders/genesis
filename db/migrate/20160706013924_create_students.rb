class CreateStudents < ActiveRecord::Migration[5.0]
  def change
    create_table :students do |t|
      t.integer :student_id
      t.string :first_name, null: false, length: 30
      t.string :middle_name, length: 50
      t.string :last_name, null: false, length: 30
      t.string :suffix, length: 5
      t.string :sex, length: 1
      t.date :date_of_birth

      t.timestamps
    end
  end
end
