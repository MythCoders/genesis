require 'test_helper'

class SchoolYearGradesControllerTest < ActionDispatch::IntegrationTest
  test "should get index" do
    get school_year_grades_index_url
    assert_response :success
  end

  test "should get show" do
    get school_year_grades_show_url
    assert_response :success
  end

  test "should get new" do
    get school_year_grades_new_url
    assert_response :success
  end

  test "should get edit" do
    get school_year_grades_edit_url
    assert_response :success
  end

  test "should get create" do
    get school_year_grades_create_url
    assert_response :success
  end

  test "should get update" do
    get school_year_grades_update_url
    assert_response :success
  end

end
