# This file is auto-generated from the current state of the database. Instead
# of editing this file, please use the migrations feature of Active Record to
# incrementally modify your database, and then regenerate this schema definition.
#
# Note that this schema.rb definition is the authoritative source for your
# database schema. If you need to create the application database on another
# system, you should be using db:schema:load, not running all the migrations
# from scratch. The latter is a flawed and unsustainable approach (the more migrations
# you'll amass, the slower it'll run and the greater likelihood for issues).
#
# It's strongly recommended that you check this file into your version control system.

ActiveRecord::Schema.define(version: 20170604175953) do

  # These are extensions that must be enabled in order to support this database
  enable_extension "plpgsql"

  create_table "addresses", id: :serial, force: :cascade do |t|
    t.string "street", limit: 50, null: false
    t.string "city", limit: 30, null: false
    t.string "state", limit: 2
    t.string "zip_code", limit: 9, null: false
    t.string "phone_number", limit: 10
  end

  create_table "announcements", force: :cascade do |t|
    t.string "title", null: false
    t.string "body", null: false
    t.datetime "start_date", null: false
    t.datetime "end_date"
    t.boolean "is_flagged", default: false, null: false
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
  end

  create_table "application_settings", force: :cascade do |t|
    t.string "key", null: false
    t.string "value"
    t.string "value_type", null: false
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
    t.string "module_name", limit: 25, null: false
  end

  create_table "custom_field_enumerations", id: :serial, force: :cascade do |t|
    t.integer "custom_field_id", null: false
    t.string "name", limit: 255, null: false
    t.boolean "active", default: true, null: false
    t.integer "position", default: 1, null: false
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
  end

  create_table "custom_fields", id: :serial, force: :cascade do |t|
    t.string "type", limit: 30, default: "", null: false
    t.string "name", limit: 30, default: "", null: false
    t.string "field_format", limit: 30, default: "", null: false
    t.text "possible_values"
    t.string "regexp", limit: 255, default: ""
    t.integer "min_length"
    t.integer "max_length"
    t.boolean "is_required", default: false, null: false
    t.boolean "is_for_all", default: false, null: false
    t.boolean "is_filter", default: false, null: false
    t.integer "position"
    t.boolean "searchable", default: false
    t.text "default_value"
    t.boolean "editable", default: true
    t.boolean "visible", default: true, null: false
    t.boolean "multiple", default: false
    t.text "format_store"
    t.text "description"
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
  end

  create_table "districts", id: :serial, force: :cascade do |t|
    t.string "name", limit: 50, null: false
    t.string "short_name", limit: 5, null: false
    t.string "address", limit: 50
    t.string "city", limit: 30
    t.string "state", limit: 2
    t.string "zip_code", limit: 9
    t.string "phone_number", limit: 10
    t.string "superintendents_name", limit: 50
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
  end

  create_table "enrollment_codes", id: :serial, force: :cascade do |t|
    t.string "title", null: false
    t.string "short_name", null: false
    t.boolean "is_admission", default: true, null: false
    t.boolean "is_withdraw", default: false, null: false
    t.boolean "is_active", default: false, null: false
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
  end

  create_table "grades", id: :serial, force: :cascade do |t|
    t.string "title"
    t.string "short_name"
    t.integer "sort_order"
    t.integer "next_grade_id"
    t.integer "previous_grade_id"
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
  end

  create_table "people", id: :serial, force: :cascade do |t|
    t.string "first_name", limit: 30
    t.string "middle_name", limit: 30
    t.string "last_name", limit: 30
    t.string "email_address", limit: 60
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
    t.integer "primary_phone_number_id"
  end

  create_table "people_phone_numbers", force: :cascade do |t|
    t.integer "person_id"
    t.string "phone_number", null: false
    t.string "number_type", null: false
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
  end

  create_table "roles", id: :serial, force: :cascade do |t|
    t.string "title", null: false
    t.string "short_name", null: false
    t.boolean "is_active", default: false, null: false
    t.integer "sort_order"
    t.integer "base_type"
  end

  create_table "school_periods", id: :serial, force: :cascade do |t|
    t.integer "school_year_id", null: false
    t.string "title", null: false
    t.string "short_name", null: false
    t.integer "sort_order"
    t.string "block"
    t.boolean "takes_attendance"
    t.time "start_time"
    t.time "end_time"
    t.time "start_time_u"
    t.time "end_time_u"
    t.time "start_time_m"
    t.time "end_time_m"
    t.time "start_time_t"
    t.time "end_time_t"
    t.time "start_time_w"
    t.time "end_time_w"
    t.time "start_time_r"
    t.time "end_time_r"
    t.time "start_time_f"
    t.time "end_time_f"
    t.time "start_time_s"
    t.time "end_time_s"
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
  end

  create_table "school_progress_periods", id: :serial, force: :cascade do |t|
    t.integer "school_quarter_id", null: false
    t.string "title", null: false
    t.string "short_name", null: false
    t.integer "sort_order"
    t.date "start_date"
    t.date "end_date"
    t.date "grade_start_date"
    t.date "grade_end_date"
    t.date "reg_start_date"
    t.date "reg_end_date"
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
  end

  create_table "school_quarters", id: :serial, force: :cascade do |t|
    t.integer "school_semester_id", null: false
    t.string "title", null: false
    t.string "short_name", null: false
    t.integer "sort_order"
    t.date "start_date"
    t.date "end_date"
    t.date "grade_start_date"
    t.date "grade_end_date"
    t.date "reg_start_date"
    t.date "reg_end_date"
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
  end

  create_table "school_semesters", id: :serial, force: :cascade do |t|
    t.integer "school_year_id", null: false
    t.string "title", null: false
    t.string "short_name", null: false
    t.integer "sort_order"
    t.date "start_date"
    t.date "end_date"
    t.date "grade_start_date"
    t.date "grade_end_date"
    t.date "reg_start_date"
    t.date "reg_end_date"
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
  end

  create_table "school_year_grade_scales", id: :serial, force: :cascade do |t|
    t.integer "school_year_id"
    t.integer "report_card_grade_scale_id"
    t.boolean "is_default", default: true
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
  end

  create_table "school_year_grades", id: :serial, force: :cascade do |t|
    t.integer "school_year_id"
    t.integer "grade_id"
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
  end

  create_table "school_years", id: :serial, force: :cascade do |t|
    t.integer "school_id", null: false
    t.string "title", null: false
    t.string "short_name", null: false
    t.integer "year", null: false
    t.date "start_date"
    t.date "end_date"
    t.date "grade_start_date"
    t.date "grade_end_date"
    t.date "reg_start_date"
    t.date "reg_end_date"
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
  end

  create_table "schools", id: :serial, force: :cascade do |t|
    t.string "name", limit: 50, null: false
    t.string "short_name", limit: 5, null: false
    t.string "address", limit: 50
    t.string "city", limit: 30
    t.string "state", limit: 2
    t.string "zip_code", limit: 9
    t.string "phone_number", limit: 10
    t.string "principals_name", limit: 50
    t.integer "district_id"
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
  end

  create_table "staff_member_employments", id: :serial, force: :cascade do |t|
    t.integer "staff_member_id", null: false
    t.integer "school_id", null: false
    t.date "start_date", null: false
    t.date "end_date"
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
  end

  create_table "staff_members", id: :serial, force: :cascade do |t|
    t.integer "staff_member_number"
    t.string "first_name", limit: 30, null: false
    t.string "middle_name", limit: 30
    t.string "last_name", limit: 30, null: false
    t.string "suffix", limit: 5
    t.string "sex", limit: 1
    t.date "date_of_birth"
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
  end

  create_table "student_addresses", id: :serial, force: :cascade do |t|
    t.integer "student_id", null: false
    t.integer "address_id", null: false
    t.boolean "is_mailing"
    t.boolean "is_residential"
    t.boolean "is_bus_pickup"
    t.boolean "is_bus_drop_off"
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
    t.string "home_phone"
  end

  create_table "student_enrollments", id: :serial, force: :cascade do |t|
    t.integer "student_id", null: false
    t.integer "school_year_grade_id", null: false
    t.integer "admission_code_id", null: false
    t.date "admission_date", null: false
    t.integer "withdraw_code_id"
    t.date "withdraw_date"
    t.integer "next_school_id"
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
  end

  create_table "student_notes", id: :serial, force: :cascade do |t|
    t.integer "student_id", null: false
    t.string "title", limit: 100, null: false
    t.string "body"
    t.boolean "is_flagged"
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
  end

  create_table "student_relationships", id: :serial, force: :cascade do |t|
    t.integer "student_address_id", null: false
    t.integer "person_id", null: false
    t.boolean "has_custody"
    t.boolean "is_emergency"
    t.string "relation"
    t.integer "contact_order"
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
  end

  create_table "students", id: :serial, force: :cascade do |t|
    t.integer "student_number"
    t.string "first_name", limit: 30, null: false
    t.string "middle_name", limit: 30
    t.string "last_name", limit: 30, null: false
    t.string "suffix", limit: 5
    t.string "sex", limit: 1
    t.date "date_of_birth"
    t.integer "primary_address_id"
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
  end

  create_table "users", id: :serial, force: :cascade do |t|
    t.string "email", default: "", null: false
    t.string "encrypted_password", default: "", null: false
    t.integer "student_id"
    t.integer "staff_member_id"
    t.integer "person_id"
    t.integer "roles"
    t.string "reset_password_token"
    t.datetime "reset_password_sent_at"
    t.datetime "remember_created_at"
    t.integer "sign_in_count", default: 0, null: false
    t.datetime "current_sign_in_at"
    t.datetime "last_sign_in_at"
    t.inet "current_sign_in_ip"
    t.inet "last_sign_in_ip"
    t.string "confirmation_token"
    t.datetime "confirmed_at"
    t.datetime "confirmation_sent_at"
    t.string "unconfirmed_email"
    t.integer "failed_attempts", default: 0, null: false
    t.datetime "locked_at"
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
    t.index ["confirmation_token"], name: "index_users_on_confirmation_token", unique: true
    t.index ["email"], name: "index_users_on_email", unique: true
    t.index ["reset_password_token"], name: "index_users_on_reset_password_token", unique: true
  end

  add_foreign_key "grades", "grades", column: "next_grade_id"
  add_foreign_key "grades", "grades", column: "previous_grade_id"
  add_foreign_key "people", "people_phone_numbers", column: "primary_phone_number_id"
  add_foreign_key "school_periods", "school_years"
  add_foreign_key "school_progress_periods", "school_quarters"
  add_foreign_key "school_quarters", "school_semesters"
  add_foreign_key "school_semesters", "school_years"
  add_foreign_key "school_year_grade_scales", "school_years"
  add_foreign_key "school_year_grades", "grades"
  add_foreign_key "school_year_grades", "school_years"
  add_foreign_key "school_years", "schools"
  add_foreign_key "schools", "districts"
  add_foreign_key "staff_member_employments", "schools"
  add_foreign_key "staff_member_employments", "staff_members"
  add_foreign_key "student_addresses", "addresses"
  add_foreign_key "student_addresses", "students"
  add_foreign_key "student_enrollments", "enrollment_codes", column: "admission_code_id"
  add_foreign_key "student_enrollments", "enrollment_codes", column: "withdraw_code_id"
  add_foreign_key "student_enrollments", "school_year_grades"
  add_foreign_key "student_enrollments", "schools", column: "next_school_id"
  add_foreign_key "student_enrollments", "students"
  add_foreign_key "student_notes", "students"
  add_foreign_key "student_relationships", "people"
  add_foreign_key "student_relationships", "student_addresses"
  add_foreign_key "students", "student_addresses", column: "primary_address_id"
end
