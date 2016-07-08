class CreateGrades < ActiveRecord::Migration[5.0]
  def change
    create_table :grades do |t|
      t.string :name, null: false
      t.integer :next_grade_id, :references => "grades", null: true
      t.integer :previous_grade_id, :references => "grades", null: true

      t.timestamps
    end
  end
end
