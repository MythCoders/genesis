class DistrictsController < ApplicationController
  def index
    @model = District.all
  end

  def show
    @model = District.find(params[:id])
  end

  def new
    @model = District.new
  end

  def create
    @model = District.new(district_params)

    if @model.save
      redirect_to @model
    else
      render 'new'
    end
  end

  def edit
    @model = District.find(params[:id])
  end

  def update
    @model = District.find(params[:id])

    if @model.update(district_params)
      redirect_to @model
    else
      render 'edit'
    end
  end

  def destroy
    @model = District.find(params[:id])
    @model.destroy

    redirect_to district_path
  end

  private

  def district_params
    params.require(:model).permit(:id, :name, :short_name, :address, :city, :state, :zip_code, :phone_number, :superintendents_name)
  end
end
