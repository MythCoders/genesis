class DistrictsController < ControlPanelController
  def index
    @model = District.all
  end

  def show
    get
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
    get
  end

  def update
    get

    if @district.update(district_params)
      redirect_to @district
    else
      render 'edit'
    end
  end

  def destroy
    get
    @district.destroy
    redirect_to districts_path
  end

  private

  def get
    @district = District.find(params[:id])
  end

  def district_params
    params.require(:district).permit(:id, :name, :short_name, :address, :city, :state, :zip_code, :phone_number, :superintendents_name)
  end
end
