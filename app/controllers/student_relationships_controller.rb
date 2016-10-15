class StudentRelationshipsController < ApplicationController
  def get
    @relationship = StudentRelationship.find(params[:id])
    render json: @relationship
  end

  def create
    @relationship = StudentRelationship.new(relationship_params)

    if @relationship.save
      render json: @relationship
    else
      render 'new'
    end
  end

  def update

  end

  def delete

  end

  private

  def relationship_params
    params.require(:student_relationships).permit(:id, :student_address_id, :person_id, :has_custody, :is_emergency, :relation, :contact_order,
                                                  { :person_attributes => [:id, :first_name, :middle_name, :last_name4, :email_address] },
                                                  { :student_addresses_attributes => [:id, :student_id, :address_id, :is_mailing, :is_residential, :is_bus_pickup, :is_bus_drop_off,
                                                                                     { :address_attributes => [:id, :street, :city, :state, :zip_code, :phone_number] } ] })
  end
end