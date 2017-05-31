class CreateCustomFields < ActiveRecord::Migration[5.0]
  def change

    create_table :custom_field_enumerations, force: :cascade do |t|
      t.integer 'custom_field_id', limit: 4, null: false
      t.string 'name', limit: 255, null: false
      t.boolean 'active', default: true, null: false
      t.integer 'position', limit: 4, default: 1, null: false
      t.timestamps
    end

    create_table :custom_fields, force: :cascade do |t|
      t.string 'type', limit: 30, default: '', null: false
      t.string 'name', limit: 30, default: '', null: false
      t.string 'field_format', limit: 30, default: '', null: false
      t.text 'possible_values', limit: 65535
      t.string 'regexp', limit: 255, default: ''
      t.integer 'min_length', limit: 4
      t.integer 'max_length', limit: 4
      t.boolean 'is_required', default: false, null: false
      t.boolean 'is_for_all', default: false, null: false
      t.boolean 'is_filter', default: false, null: false
      t.integer 'position', limit: 4
      t.boolean 'searchable', default: false
      t.text 'default_value', limit: 65535
      t.boolean 'editable', default: true
      t.boolean 'visible', default: true, null: false
      t.boolean 'multiple', default: false
      t.text 'format_store', limit: 65535
      t.text 'description', limit: 65535
      t.timestamps
    end

  end
end