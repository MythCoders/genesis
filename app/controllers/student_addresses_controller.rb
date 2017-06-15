class StudentAddressesController < ApplicationController

  def index
    @student = Student.includes(addresses: [:student_relationships, :address]).find(params[:student_id])
  end

  def show
    get
  end

  def edit
    get
  end

  def new
    @address = StudentAddress.new
    @address.address = Address.new
    @address.student = Student.find(params[:student_id])
  end

  def create
    @address = StudentAddress.new(address_params)

    if @address.save
      redirect_to @address
    else
      render 'new'
    end
  end

  def update
    get

    if @address.update(address_params)
      redirect_to @address
    else
      render 'edit'
    end
  end

  private

  def get
    @address = StudentAddress.includes(:address, :student).find(params[:id])
  end

  def address_params
    params.require(:address).permit(:id, :student_id, :address_id, :is_mailing, :is_residential, :is_bus_pickup, :is_bus_drop_off,
                                    { :address_attributes => [:id, :street, :city, :state, :zip_code, :phone_number] })
  end

end