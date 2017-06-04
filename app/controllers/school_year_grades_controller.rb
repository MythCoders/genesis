class SchoolYearGradesController < ControlPanelController
  def index
  end

  def show
  end

  def new
    @model = SchoolYearGrade.new(:school_id => params[:school_id])
  end

  def edit
  end

  def create
  end

  def update
  end
end
