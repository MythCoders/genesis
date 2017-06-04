class AnnouncementsController < ControlPanelController
  def index
    @announcements = Announcement.all
  end

  def new
    @announcement = Announcement.new
  end

  def edit
    get
  end

  def show
    get
  end

  def create
    @announcement = Announcement.new(announcement_params)

    if @announcement.save
      redirect_to @announcement
    else
      render 'new'
    end
  end

  def update
    get

    if @announcement.update(announcement_params)
      redirect_to @announcement
    else
      render 'edit'
    end
  end

  def destroy
    get
    @announcement.destroy
    redirect_to announcements_path
  end

  private

  def get
    @announcement = Announcement.find(params[:id])
  end

  def announcement_params
    params.require(:announcement).permit(:id, :title, :body, :start_date, :end_date, :is_flagged)
  end
end
