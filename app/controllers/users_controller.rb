class UsersController < ControlPanelController
  def index
    @model = User.all
  end

  def new
  end

  def edit
  end

  def show
  end
end
