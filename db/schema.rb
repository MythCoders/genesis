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

ActiveRecord::Schema.define(version: 20160707213555) do

  # These are extensions that must be enabled in order to support this database
  enable_extension "plpgsql"

  create_table "districts", force: :cascade do |t|
    t.string   "name",         null: false
    t.string   "address"
    t.string   "city"
    t.string   "state"
    t.string   "zipcode"
    t.string   "phone_number"
    t.datetime "created_at",   null: false
    t.datetime "updated_at",   null: false
  end

  create_table "grades", force: :cascade do |t|
    t.string   "name",              null: false
    t.integer  "next_grade_id"
    t.integer  "previous_grade_id"
    t.datetime "created_at",        null: false
    t.datetime "updated_at",        null: false
  end

  create_table "schools", force: :cascade do |t|
    t.string   "name",         null: false
    t.string   "address"
    t.string   "city"
    t.string   "state"
    t.string   "zipcode"
    t.string   "phone_number"
    t.integer  "district_id"
    t.datetime "created_at",   null: false
    t.datetime "updated_at",   null: false
    t.index ["district_id"], name: "index_schools_on_district_id", using: :btree
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
    t.string   "first_name",    null: false
    t.string   "middle_name"
    t.string   "last_name",     null: false
    t.string   "suffix"
    t.string   "sex"
    t.date     "date_of_birth"
    t.datetime "created_at",    null: false
    t.datetime "updated_at",    null: false
  end

  add_foreign_key "schools", "districts"
end
