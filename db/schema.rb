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

ActiveRecord::Schema.define(version: 20160824000941) do

  # These are extensions that must be enabled in order to support this database
  enable_extension "plpgsql"

  create_table "addresses", force: :cascade do |t|
    t.string "street",       limit: 50, null: false
    t.string "city",         limit: 30, null: false
    t.string "state",        limit: 2
    t.string "zip_code",     limit: 9,  null: false
    t.string "phone_number", limit: 10
  end

  create_table "attendance", force: :cascade do |t|
    t.integer  "attendance_calendar_day_id"
    t.integer  "attendance_code_id",         null: false
    t.integer  "student_id"
    t.string   "comment"
    t.integer  "minutes_present"
    t.datetime "created_at",                 null: false
    t.datetime "updated_at",                 null: false
  end

  create_table "attendance_calendar_days", force: :cascade do |t|
    t.integer  "attendance_calendar_id", null: false
    t.date     "school_date",            null: false
    t.string   "block"
    t.integer  "minutes"
    t.datetime "created_at",             null: false
    t.datetime "updated_at",             null: false
  end

  create_table "attendance_calendars", force: :cascade do |t|
    t.integer  "school_year_id", null: false
    t.string   "title",          null: false
    t.boolean  "is_default",     null: false
    t.datetime "created_at",     null: false
    t.datetime "updated_at",     null: false
  end

  create_table "attendance_codes", force: :cascade do |t|
    t.string   "title",                null: false
    t.string   "short_name",           null: false
    t.integer  "sort_order"
    t.boolean  "usable_by_office",     null: false
    t.boolean  "usable_by_teachers",   null: false
    t.boolean  "default_for_teachers", null: false
    t.boolean  "is_default_absent",    null: false
    t.datetime "created_at",           null: false
    t.datetime "updated_at",           null: false
  end

  create_table "course_categories", force: :cascade do |t|
    t.string   "title"
    t.string   "short_name"
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
  end

  create_table "courses", force: :cascade do |t|
    t.string   "title"
    t.string   "short_name"
    t.integer  "course_category_id"
    t.string   "description"
    t.datetime "created_at",         null: false
    t.datetime "updated_at",         null: false
  end

  create_table "custom_field_enumerations", force: :cascade do |t|
    t.integer  "custom_field_id",                            null: false
    t.string   "name",            limit: 255,                null: false
    t.boolean  "active",                      default: true, null: false
    t.integer  "position",                    default: 1,    null: false
    t.datetime "created_at",                                 null: false
    t.datetime "updated_at",                                 null: false
  end

  create_table "custom_fields", force: :cascade do |t|
    t.string   "type",            limit: 30,  default: "",    null: false
    t.string   "name",            limit: 30,  default: "",    null: false
    t.string   "field_format",    limit: 30,  default: "",    null: false
    t.text     "possible_values"
    t.string   "regexp",          limit: 255, default: ""
    t.integer  "min_length"
    t.integer  "max_length"
    t.boolean  "is_required",                 default: false, null: false
    t.boolean  "is_for_all",                  default: false, null: false
    t.boolean  "is_filter",                   default: false, null: false
    t.integer  "position"
    t.boolean  "searchable",                  default: false
    t.text     "default_value"
    t.boolean  "editable",                    default: true
    t.boolean  "visible",                     default: true,  null: false
    t.boolean  "multiple",                    default: false
    t.text     "format_store"
    t.text     "description"
    t.datetime "created_at",                                  null: false
    t.datetime "updated_at",                                  null: false
  end

  create_table "districts", force: :cascade do |t|
    t.string   "name",                 limit: 50, null: false
    t.string   "short_name",           limit: 5,  null: false
    t.string   "address",              limit: 50
    t.string   "city",                 limit: 30
    t.string   "state",                limit: 2
    t.string   "zip_code",             limit: 9
    t.string   "phone_number",         limit: 10
    t.string   "superintendents_name", limit: 50
    t.datetime "created_at",                      null: false
    t.datetime "updated_at",                      null: false
  end

  create_table "enrollment_codes", force: :cascade do |t|
    t.string   "title",                        null: false
    t.string   "short_name",                   null: false
    t.boolean  "is_admission", default: false, null: false
    t.boolean  "is_active",    default: false, null: false
    t.datetime "created_at",                   null: false
    t.datetime "updated_at",                   null: false
  end

  create_table "enrollments", force: :cascade do |t|
    t.integer  "student_id",           null: false
    t.integer  "school_year_grade_id", null: false
    t.date     "admission_date",       null: false
    t.integer  "admission_code_id",    null: false
    t.date     "withdraw_date"
    t.integer  "withdraw_code_id"
    t.integer  "next_school_id"
    t.datetime "created_at",           null: false
    t.datetime "updated_at",           null: false
  end

  create_table "grades", force: :cascade do |t|
    t.string   "title"
    t.string   "short_name"
    t.integer  "sort_order"
    t.integer  "next_grade_id"
    t.integer  "previous_grade_id"
    t.datetime "created_at",        null: false
    t.datetime "updated_at",        null: false
  end

  create_table "people", force: :cascade do |t|
    t.string   "first_name",    limit: 30
    t.string   "middle_name",   limit: 30
    t.string   "last_name",     limit: 30
    t.string   "email_address", limit: 60
    t.datetime "created_at",               null: false
    t.datetime "updated_at",               null: false
  end

  create_table "report_card_comments", force: :cascade do |t|
    t.integer "school_year_id", null: false
    t.string  "text"
  end

  create_table "report_card_grade_scales", force: :cascade do |t|
    t.string   "name",                        null: false
    t.text     "description"
    t.boolean  "is_active",   default: false, null: false
    t.datetime "created_at",                  null: false
    t.datetime "updated_at",                  null: false
  end

  create_table "report_card_grades", force: :cascade do |t|
    t.string   "title",                      null: false
    t.string   "description"
    t.decimal  "gpa_value",                  null: false
    t.decimal  "score_cut_off"
    t.decimal  "weighted_gpa_scale"
    t.integer  "report_card_grade_scale_id", null: false
    t.datetime "created_at",                 null: false
    t.datetime "updated_at",                 null: false
  end

  create_table "roles", force: :cascade do |t|
    t.string  "title",      null: false
    t.string  "short_name", null: false
    t.integer "sort_order"
    t.integer "base_type"
  end

  create_table "school_periods", force: :cascade do |t|
    t.integer  "school_year_id",   null: false
    t.string   "title",            null: false
    t.string   "short_name",       null: false
    t.integer  "sort_order"
    t.string   "block"
    t.boolean  "takes_attendance"
    t.time     "start_time"
    t.time     "end_time"
    t.time     "start_time_u"
    t.time     "end_time_u"
    t.time     "start_time_m"
    t.time     "end_time_m"
    t.time     "start_time_t"
    t.time     "end_time_t"
    t.time     "start_time_w"
    t.time     "end_time_w"
    t.time     "start_time_r"
    t.time     "end_time_r"
    t.time     "start_time_f"
    t.time     "end_time_f"
    t.time     "start_time_s"
    t.time     "end_time_s"
    t.datetime "created_at",       null: false
    t.datetime "updated_at",       null: false
  end

  create_table "school_progress_periods", force: :cascade do |t|
    t.integer  "school_quarter_id", null: false
    t.string   "title",             null: false
    t.string   "short_name",        null: false
    t.integer  "sort_order"
    t.date     "start_date"
    t.date     "end_date"
    t.date     "grade_start_date"
    t.date     "grade_end_date"
    t.date     "reg_start_date"
    t.date     "reg_end_date"
    t.datetime "created_at",        null: false
    t.datetime "updated_at",        null: false
  end

  create_table "school_quarters", force: :cascade do |t|
    t.integer  "school_semester_id", null: false
    t.string   "title",              null: false
    t.string   "short_name",         null: false
    t.integer  "sort_order"
    t.date     "start_date"
    t.date     "end_date"
    t.date     "grade_start_date"
    t.date     "grade_end_date"
    t.date     "reg_start_date"
    t.date     "reg_end_date"
    t.datetime "created_at",         null: false
    t.datetime "updated_at",         null: false
  end

  create_table "school_semesters", force: :cascade do |t|
    t.integer  "school_year_id",   null: false
    t.string   "title",            null: false
    t.string   "short_name",       null: false
    t.integer  "sort_order"
    t.date     "start_date"
    t.date     "end_date"
    t.date     "grade_start_date"
    t.date     "grade_end_date"
    t.date     "reg_start_date"
    t.date     "reg_end_date"
    t.datetime "created_at",       null: false
    t.datetime "updated_at",       null: false
  end

  create_table "school_year_grades", force: :cascade do |t|
    t.integer  "school_year_id"
    t.integer  "grade_id"
    t.datetime "created_at",     null: false
    t.datetime "updated_at",     null: false
  end

  create_table "school_year_mark_scales", force: :cascade do |t|
    t.integer  "school_year_id"
    t.integer  "mark_scale_id"
    t.boolean  "is_default",     default: true
    t.datetime "created_at",                    null: false
    t.datetime "updated_at",                    null: false
  end

  create_table "school_years", force: :cascade do |t|
    t.integer  "school_id",        null: false
    t.string   "title",            null: false
    t.string   "short_name",       null: false
    t.integer  "year",             null: false
    t.date     "start_date"
    t.date     "end_date"
    t.date     "grade_start_date"
    t.date     "grade_end_date"
    t.date     "reg_start_date"
    t.date     "reg_end_date"
    t.datetime "created_at",       null: false
    t.datetime "updated_at",       null: false
  end

  create_table "schools", force: :cascade do |t|
    t.string   "name",            limit: 50, null: false
    t.string   "short_name",      limit: 5,  null: false
    t.string   "address",         limit: 50
    t.string   "city",            limit: 30
    t.string   "state",           limit: 2
    t.string   "zip_code",        limit: 9
    t.string   "phone_number",    limit: 10
    t.string   "principals_name", limit: 50
    t.integer  "district_id"
    t.datetime "created_at",                 null: false
    t.datetime "updated_at",                 null: false
  end

  create_table "settings", force: :cascade do |t|
    t.string   "key",        null: false
    t.string   "value",      null: false
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
    t.index ["key"], name: "index_settings_on_key", using: :btree
  end

  create_table "student_addresses", force: :cascade do |t|
    t.integer  "student_id",      null: false
    t.integer  "address_id",      null: false
    t.boolean  "is_mailing"
    t.boolean  "is_residential"
    t.boolean  "is_bus_pickup"
    t.boolean  "is_bus_drop_off"
    t.datetime "created_at",      null: false
    t.datetime "updated_at",      null: false
  end

  create_table "student_medical_alerts", force: :cascade do |t|
    t.integer  "student_id", null: false
    t.string   "title",      null: false
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
  end

  create_table "student_medical_visits", force: :cascade do |t|
    t.integer  "student_id", null: false
    t.datetime "time_in",    null: false
    t.datetime "time_out"
    t.string   "reason"
    t.string   "result"
    t.string   "comment"
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
  end

  create_table "student_notes", force: :cascade do |t|
    t.integer  "student_id",             null: false
    t.string   "title",      limit: 100, null: false
    t.string   "body"
    t.boolean  "is_flagged"
    t.datetime "created_at",             null: false
    t.datetime "updated_at",             null: false
  end

  create_table "student_relationships", force: :cascade do |t|
    t.integer  "student_address_id", null: false
    t.integer  "person_id",          null: false
    t.boolean  "has_custody"
    t.boolean  "is_emergency"
    t.string   "relation"
    t.datetime "created_at",         null: false
    t.datetime "updated_at",         null: false
    t.integer  "contact_order"
  end

  create_table "students", force: :cascade do |t|
    t.integer  "student_id"
    t.string   "first_name",         limit: 30, null: false
    t.string   "middle_name",        limit: 30
    t.string   "last_name",          limit: 30, null: false
    t.string   "suffix",             limit: 5
    t.string   "sex",                limit: 1
    t.date     "date_of_birth"
    t.datetime "created_at",                    null: false
    t.datetime "updated_at",                    null: false
    t.integer  "primary_address_id"
  end

  create_table "users", force: :cascade do |t|
    t.string   "first_name"
    t.string   "last_name"
    t.string   "middle_name"
    t.string   "phone_number"
    t.integer  "roles"
    t.string   "email",                  default: "", null: false
    t.string   "encrypted_password",     default: "", null: false
    t.string   "reset_password_token"
    t.datetime "reset_password_sent_at"
    t.datetime "remember_created_at"
    t.integer  "sign_in_count",          default: 0,  null: false
    t.datetime "current_sign_in_at"
    t.datetime "last_sign_in_at"
    t.inet     "current_sign_in_ip"
    t.inet     "last_sign_in_ip"
    t.string   "confirmation_token"
    t.datetime "confirmed_at"
    t.datetime "confirmation_sent_at"
    t.string   "unconfirmed_email"
    t.integer  "failed_attempts",        default: 0,  null: false
    t.datetime "locked_at"
    t.datetime "created_at",                          null: false
    t.datetime "updated_at",                          null: false
    t.index ["confirmation_token"], name: "index_users_on_confirmation_token", unique: true, using: :btree
    t.index ["email"], name: "index_users_on_email", unique: true, using: :btree
    t.index ["reset_password_token"], name: "index_users_on_reset_password_token", unique: true, using: :btree
  end

  add_foreign_key "student_addresses", "addresses"
  add_foreign_key "student_addresses", "students"
  add_foreign_key "student_medical_alerts", "students"
  add_foreign_key "student_medical_visits", "students"
  add_foreign_key "student_notes", "students"
  add_foreign_key "student_relationships", "people"
  add_foreign_key "student_relationships", "student_addresses"
  add_foreign_key "students", "student_addresses", column: "primary_address_id"
end
