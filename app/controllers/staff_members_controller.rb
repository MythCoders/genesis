class StaffMembersController < ApplicationController

  def index
    @q = StaffMember.ransack(params[:q])
    @staff = @q.result
  end

end