class CreateCourse < ActiveRecord::Migration[5.0]
  def change
    create_table :course_categories do |t|
      t.string :title, :length => 30
      t.string :short_name, :length => 10
      t.timestamps
    end

    create_table :courses do |t|
      t.string :title, :length => 30
      t.string :short_name, :length => 10
      t.integer :course_category_id, :null => false
      t.string :description, :length => 250
      t.timestamps
      t.foreign_key :course_categories, column: :course_category_id
    end
  end
end
