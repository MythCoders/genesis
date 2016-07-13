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

ActiveRecord::Schema.define(version: 20160712214020) do

  # These are extensions that must be enabled in order to support this database
  enable_extension "plpgsql"

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
    t.string   "name",         limit: 50, null: false
    t.string   "address",      limit: 50
    t.string   "city",         limit: 30
    t.string   "state",        limit: 2
    t.string   "zip_code",     limit: 9
    t.string   "phone_number", limit: 10
    t.datetime "created_at",              null: false
    t.datetime "updated_at",              null: false
  end

  create_table "enrollment_codes", force: :cascade do |t|
    t.string   "title",                        null: false
    t.string   "short_name",                   null: false
    t.boolean  "is_admission", default: false, null: false
    t.boolean  "is_active",                    null: false
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

  create_table "mark_scales", force: :cascade do |t|
    t.string   "name",                        null: false
    t.text     "description"
    t.boolean  "is_active",   default: false, null: false
    t.datetime "created_at",                  null: false
    t.datetime "updated_at",                  null: false
  end

  create_table "marks", force: :cascade do |t|
    t.string   "title",              null: false
    t.string   "description"
    t.decimal  "gpa_value",          null: false
    t.decimal  "score_cut_off"
    t.decimal  "weighted_gpa_scale"
    t.integer  "mark_scale_id",      null: false
    t.datetime "created_at",         null: false
    t.datetime "updated_at",         null: false
  end

  create_table "roles", force: :cascade do |t|
    t.string   "name",                    limit: 30, default: "",        null: false
    t.integer  "position"
    t.boolean  "assignable",                         default: true
    t.integer  "builtin",                            default: 0,         null: false
    t.text     "permissions"
    t.string   "issues_visibility",       limit: 30, default: "default", null: false
    t.string   "users_visibility",        limit: 30, default: "all",     null: false
    t.string   "time_entries_visibility", limit: 30, default: "all",     null: false
    t.boolean  "all_roles_managed",                  default: true,      null: false
    t.text     "settings"
    t.datetime "created_at",                                             null: false
    t.datetime "updated_at",                                             null: false
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
    t.string   "name",         limit: 50, null: false
    t.string   "address",      limit: 50
    t.string   "city",         limit: 30
    t.string   "state",        limit: 2
    t.string   "zip_code",     limit: 9
    t.string   "phone_number", limit: 10
    t.integer  "district_id"
    t.datetime "created_at",              null: false
    t.datetime "updated_at",              null: false
  end

  create_table "settings", force: :cascade do |t|
    t.string   "key",        null: false
    t.string   "value",      null: false
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
    t.index ["key"], name: "index_settings_on_key", using: :btree
  end

  create_table "students", force: :cascade do |t|
    t.integer  "student_id"
    t.string   "first_name",    limit: 30, null: false
    t.string   "middle_name",   limit: 50
    t.string   "last_name",     limit: 30, null: false
    t.string   "suffix",        limit: 5
    t.string   "sex",           limit: 1
    t.date     "date_of_birth"
    t.datetime "created_at",               null: false
    t.datetime "updated_at",               null: false
  end

  create_table "users", force: :cascade do |t|
    t.string   "username",           limit: 255, default: "",    null: false
    t.string   "hashed_password",    limit: 40,  default: "",    null: false
    t.string   "first_name",         limit: 30,  default: "",    null: false
    t.string   "last_name",          limit: 255, default: "",    null: false
    t.boolean  "admin",                          default: false, null: false
    t.integer  "status",                         default: 1,     null: false
    t.datetime "last_login_on"
    t.string   "language",           limit: 5,   default: ""
    t.string   "type",               limit: 255
    t.string   "identity_url",       limit: 255
    t.string   "mail_notification",  limit: 255, default: "",    null: false
    t.string   "salt",               limit: 64
    t.boolean  "must_change_passwd",             default: false, null: false
    t.datetime "passwd_changed_on"
    t.datetime "created_at",                                     null: false
    t.datetime "updated_at",                                     null: false
  end

end
