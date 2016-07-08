require 'test_helper'

class Admin::SchoolControllerTest < ActionDispatch::IntegrationTest
  test "should get index" do
    get admin_school_index_url
    assert_response :success
  end

  test "should get show" do
    get admin_school_show_url
    assert_response :success
  end

  test "should get new" do
    get admin_school_new_url
    assert_response :success
  end

end
