class StudentRelationshipsController < ApplicationController
  def show
    get
  end

  def new
    @relationship = StudentRelationship.new
    @relationship.student_address = StudentAddress.includes(:student).find(params[:address_id])
  end

  def edit
    get
  end

  def create
    @relationship = StudentRelationship.new(relationship_params)

    if @relationship.save
      redirect_to @relationship.student_address.student
    else
      render 'new'
    end
  end

  def update
    get

    if @relationship.update(relationship_params)
      redirect_to @relationship.student_address.student
    else
      render 'edit'
    end
  end

  def destroy
    get
  end

  private

  def get
    @relationship = StudentRelationship.includes(:person, student_address: [:student, :address]).find(params[:id])
  end

  def relationship_params
    params.require(:student_relationships).permit(:id, :student_address_id, :person_id, :has_custody, :is_emergency, :relation, :contact_order,
                                                  { :person_attributes => [:id, :first_name, :middle_name, :last_name, :email_address] },
                                                  { :student_addresses_attributes => [:id, :student_id, :address_id, :is_mailing, :is_residential, :is_bus_pickup, :is_bus_drop_off,
                                                                                      { :address_attributes => [:id, :street, :city, :state, :zip_code, :phone_number] } ] })
  end
end