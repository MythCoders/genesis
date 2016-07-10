module ApplicationHelper

  def render_tabs(tabs, selected = params[:tab])
    if tabs.any?
      unless tabs.detect {|tab| tab[:name] == selected}
        selected = nil
      end
      selected ||= tabs.first[:name]
      render :partial => 'common/tabs', :locals => {:tabs => tabs, :selected_tab => selected}
    else
      content_tag 'p', l(:label_no_data), :class => 'no-data'
    end
  end

  def html_title(*args)
    if args.empty?
      title = @html_title || []
      #title << @project.name if @project
      title << Genesis::INFO.app_name #Setting.app_title unless Setting.app_title == title.last
      title.reject(&:blank?).join(' - ')
    else
      @html_title ||= []
      @html_title += args
    end
  end
end
