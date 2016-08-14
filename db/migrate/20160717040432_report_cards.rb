class ReportCards < ActiveRecord::Migration[5.0]
  def change
    create_table :report_card_comments do |t|
      t.integer :school_year_id, :null => false
      t.string :text, :length => 250
    end

    create_table :report_card_grade_scales do |t|
      t.string :name, :null => false, :length => 30
      t.text :description, :length => 250
      t.boolean :is_active, :null => false, :default => false
      t.timestamps
    end

    create_table :report_card_grades do |t|
      t.string :title, :null => false, :length => 3
      t.string :description, :length => 250
      t.decimal :gpa_value, :null => false
      t.decimal :score_cut_off, :null => true
      t.decimal :weighted_gpa_scale, :null => true
      t.integer :report_card_grade_scale_id, :null => false
      t.timestamps
      t.foreign_key :report_card_grade_scales, column: :report_card_grade_scale_id
    end
  end
end
