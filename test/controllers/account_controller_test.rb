require 'test_helper'

class AccountControllerTest < ActionDispatch::IntegrationTest
  test "should get login" do
    get account_login_url
    assert_response :success
  end

  test "should get logout" do
    get account_logout_url
    assert_response :success
  end

  test "should get lost_password" do
    get account_lost_password_url
    assert_response :success
  end

  test "should get password_recovery" do
    get account_password_recovery_url
    assert_response :success
  end

  test "should get register" do
    get account_register_url
    assert_response :success
  end

end
