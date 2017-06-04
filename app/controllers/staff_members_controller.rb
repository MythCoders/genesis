class StaffMembersController < ApplicationController

  def index
    @q = StaffMember.ransack(params[:q])
    @staff = @q.result
  end

  def new
    @staff = StaffMember.new
  end

  def edit
    get
  end

  def create
    @staff = StaffMember.new(staff_member_params)

    if @staff.save
      redirect_to @staff
    else
      render 'new'
    end
  end

  def update
    get

    if @staff.update(staff_member_params)
      redirect_to @staff
    else
      render 'edit'
    end
  end

  def destroy
    get
    @staff.destroy
    redirect_to staff_members_path
  end

  private

  def get
    @staff = StaffMember.find(params[:id])
  end

  def staff_member_params
    params.require(:staff).permit(:id)
  end

end