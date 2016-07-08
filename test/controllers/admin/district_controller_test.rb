require 'test_helper'

class Admin::DistrictControllerTest < ActionDispatch::IntegrationTest
  test "should get index" do
    get admin_district_index_url
    assert_response :success
  end

  test "should get show" do
    get admin_district_show_url
    assert_response :success
  end

  test "should get new" do
    get admin_district_new_url
    assert_response :success
  end

end
