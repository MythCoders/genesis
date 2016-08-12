class AnnouncementsController < ApplicationController
  def index
    @model = Announcement.all
  end

  def new
    @model = Announcement.new
  end

  def edit
    @model = Announcement.find(params[:id])
  end

  def show
    @model = Announcement.find(params[:id])
  end

  def create

  end

  def update

  end

  private

  def announcement_params
    params.require(:model).permit(:id, :title)
  end
end
