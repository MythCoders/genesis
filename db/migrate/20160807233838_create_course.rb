class CreateCourse < ActiveRecord::Migration[5.0]
  def change
    create_table :courses do |t|
      t.string :title, :length => 30
      t.string :short_name, :length => 10
      t.integer :course_category_id
      t.string :description, :length => 250
      t.timestamps
    end

    create_table :course_categories do |t|
      t.string :title, :length => 30
      t.string :short_name, :length => 10
      t.timestamps
    end

  end
end
