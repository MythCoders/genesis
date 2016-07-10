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

ActiveRecord::Schema.define(version: 20160710154252) do

  # These are extensions that must be enabled in order to support this database
  enable_extension "plpgsql"

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
    t.string   "title"
    t.string   "short_name"
    t.integer  "type"
    t.datetime "created_at", null: false
    t.datetime "updated_at", null: false
  end

  create_table "enrollments", force: :cascade do |t|
    t.integer  "students_id"
    t.integer  "school_years_id"
    t.integer  "grades_id"
    t.date     "start_date"
    t.date     "end_date"
    t.integer  "add_code_id"
    t.integer  "drop_code_id"
    t.integer  "next_school_id"
    t.datetime "created_at",      null: false
    t.datetime "updated_at",      null: false
    t.index ["grades_id"], name: "index_enrollments_on_grades_id", using: :btree
    t.index ["school_years_id"], name: "index_enrollments_on_school_years_id", using: :btree
    t.index ["students_id"], name: "index_enrollments_on_students_id", using: :btree
  end

  create_table "grades", force: :cascade do |t|
    t.string   "name",              null: false
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
    t.integer  "gpa_value",          null: false
    t.integer  "breakoff",           null: false
    t.integer  "credits"
    t.integer  "weighted_gpa_value"
    t.integer  "mark_scales_id"
    t.datetime "created_at",         null: false
    t.datetime "updated_at",         null: false
    t.index ["mark_scales_id"], name: "index_marks_on_mark_scales_id", using: :btree
  end

  create_table "school_years", force: :cascade do |t|
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
    t.integer  "districts_id"
    t.datetime "created_at",              null: false
    t.datetime "updated_at",              null: false
    t.index ["districts_id"], name: "index_schools_on_districts_id", using: :btree
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

  add_foreign_key "enrollments", "grades", column: "grades_id"
  add_foreign_key "enrollments", "school_years", column: "school_years_id"
  add_foreign_key "enrollments", "students", column: "students_id"
  add_foreign_key "marks", "mark_scales", column: "mark_scales_id"
  add_foreign_key "schools", "districts", column: "districts_id"
end
