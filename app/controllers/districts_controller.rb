class DistrictsController < ApplicationController
  def index
    @model = District.all
  end

  def show
    @district = District.find(params[:id])
  end

  def new
    @model = District.new
  end

  def create
    @district = District.new(district_params)

    if @district.save
      redirect_to @district
    else
      render 'new'
    end
  end

  def edit
    @district = District.find(params[:id])
  end

  def update
    @district = District.find(params[:id])

    if @district.update(district_params)
      redirect_to @district
    else
      render 'edit'
    end
  end

  def destroy
    @district = District.find(params[:id])
    @district.destroy

    redirect_to district_path
  end

  private

  def district_params
    params.require(:district).permit(:id, :name, :short_name, :address, :city, :state, :zip_code, :phone_number, :superintendents_name)
  end
end
