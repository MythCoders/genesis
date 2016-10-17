module ApplicationHelper

  FULL_NAME_FORMAT = [LAST_FIRST = 1, FIRST_LAST = 2]
  ADDRESS_FORMAT = [WITH_LINE_BREAKS = 1, ONE_LINE = 2]
  SEX = [MALE = 'M', FEMALE = 'F']
  USER_BASE_ROLES = [STUDENT = 1, PARENT = 4, TEACHER = 12, ADMIN = 64, SUPER_ADMIN = 256]

  def render_vertical_tabs(tabs, selected = params[:tab])
    if tabs.any?
      unless tabs.detect { |tab| tab[:name] == selected }
        selected = nil
      end

      if selected == nil
        if tabs[0][:type] == 'heading'
          selected = tabs[1][:name]
        else
          selected = tabs[0][:name]
        end
      end

      render :partial => 'common/vertical_tabs', :locals => {:tabs => tabs, :selected_tab => selected}
    else
      content_tag 'p', l(:label_no_data), :class => 'no-data'
    end
  end

  def render_horizontal_nav(tabs, selected = params[:tab])
    if tabs.any?
      unless tabs.detect { |tab| tab[:name] == selected }
        selected = nil
      end
      selected ||= tabs.first[:name]
      render :partial => 'common/horizontal_tabs', :locals => {:tabs => tabs, :selected_tab => selected}
    else
      content_tag 'p', l(:label_no_data), :class => 'no-data'
    end
  end

  def render_nav_bar(menu)
    render :partial => 'layouts/navbar', :locals => {:menu => menu}
  end

  def html_title(*args)
    if args.empty?
      title = @html_title || []
      title << @student.formatted_full_name  if @student
      title << @page_title if @page_title
      title << Genesis::INFO.app_name
      title.reject(&:blank?).join(' - ')
    else
      @html_title ||= []
      @html_title += args
    end
  end

  def format_person_name(first_name, middle_initial, last_name, suffix = nil, format = 2)
    return_value = nil

    if format == 1
      if !last_name.blank? || !first_name.blank?
        return_value = "#{last_name}, #{first_name}"
      end

      unless middle_initial.blank?
        return_value += " #{middle_initial}"
        if middle_initial.length == 1
          return_value += '.'
        end
      end

      unless suffix.blank?
        return_value += ", #{suffix}"

        if suffix == 'Jr' || suffix == 'Sr'
          return_value += '.'
        end
      end
    else
      return_value = "#{first_name}"

      unless middle_initial.blank?
        return_value += " #{middle_initial}"
        if middle_initial.length == 1
          return_value += '.'
        end
      end

      return_value += " #{last_name}"

      unless suffix.blank?
        return_value += ", #{suffix}"

        if suffix == 'Jr' || suffix == 'Sr'
          return_value += '.'
        end
      end
    end

    return_value

  end

  def format_address(address_line_1, address_line_2, city, state, zip_code, zip_code_four = '', format = WITH_LINE_BREAKS)
    return_value = ''
    delimiter = format == 2 ? ', ' : '<br/>'

    unless address_line_1.blank?
      return_value += address_line_1
    end

    unless address_line_2.blank?
      if return_value.length > 0
        return_value += delimiter
      end
      return_value += address_line_2
    end

    unless city.blank? && state.blank? && zip_code.blank?
      if return_value.length > 0
        return_value += delimiter
      end

      unless city.blank?
        return_value += city

        if !state.blank? && !zip_code.blank?
          return_value += ', '
        end
      end

      unless state.blank?
        return_value += state
      end

      unless zip_code.blank?
        if state != nil
          return_value += ' '
        end

        if zip_code.length == 9 && zip_code_four.blank?
          zip_code_four = zip_code[5..4]
          zip_code = zip_code[0..5]
        end

        return_value += zip_code

        unless zip_code_four.blank?
          return_value += "-#{zip_code_four}"
        end
      end
    end

    return_value
  end

  def format_address_with_html_link(address_line_1, address_line_2, city, state, zip_code, zip_code_four = '', format = 1, label = nil)
    formatted_address = format_address(address_line_1, address_line_2, city, state, zip_code, zip_code_four, format)
    parameters = format == 2 ? formatted_address : format_address(address_line_1, address_line_2, city, state, zip_code, zip_code_four, 2)

    if label.nil?
      "<a href=\"http://bing.com/maps?q=#{parameters}\" target=\"_blank\">#{formatted_address}</a>"
    else
      "<a href=\"http://bing.com/maps?q=#{parameters}\" target=\"_blank\">#{label}</a>"
    end
  end

  def display_date(value, return_if_nil = '')
    if value.nil?
      return_if_nil
    else
      value.strftime('%x')
    end
  end

  def display_thur_dates(date1, date2, separator = '-')
    if date1.nil? and date2.nil?
      ''
    else
      "#{display_date(date1, '?')} #{separator} #{display_date(date2, '?')}"
    end
  end

  # Render the error messages for the given objects
  def error_messages_for(*objects)
    objects = objects.map {|o| o.is_a?(String) ? instance_variable_get("@#{o}") : o}.compact
    errors = objects.map {|o| o.errors.full_messages}.flatten
    render_error_messages(errors)
  end

  # Renders a list of error messages
  def render_error_messages(errors)
    html = ''
    if errors.present?
      html << "<div id='errorExplanation'><ul>\n"
      errors.each do |error|
        html << "<li>#{h error}</li>\n"
      end
      html << "</ul></div>\n"
    end
    html.html_safe
  end

  def render_school_dropdown
    return '' unless session['school_id'] != 0

    active_school = School.find(session['school_id'])
    html = "<input type='hidden' value='#{active_school.id}' id='SIS.SCHOOL_ID' name='SIS.SCHOOL_ID' />"

    if School.any?

      html << '<li><div>'
      html << "<a href='#schools' aria-owns='schools' aria-haspopup='true' class='aui-dropdown2-trigger'>#{active_school.name}</a>"
      html << '<div id="schools" class="aui-style-default aui-dropdown2">'
      html << '<ul class="aui-list-truncate">'

      schools = School.order(:name).all

      schools.each do |s|
        if s.id == active_school.id
          html << "<li><a href='#' onclick=''><strong>#{s.name}</strong></a></li>"
        else
          html << "<li><a href='#' onclick=''>#{s.name}</a></li>"
        end
      end

      html << '</ul></div></div></li>'

    end

    if !active_school.nil? and active_school.school_years.any?

      active_year = SchoolYear.find(session['school_year_id'])

      html << "<input type='hidden' value='#{active_year.id}' id='SIS.SCHOOL_YEAR_ID' name='SIS.SCHOOL_YEAR_ID' />"

      html << '<li><div>'
      html << "<a href='#school-years' aria-owns='school-years' aria-haspopup='true' class='aui-dropdown2-trigger'>#{active_year.title}</a>"
      html << '<div id="school-years" class="aui-style-default aui-dropdown2">'
      html << '<ul class="aui-list-truncate">'

      years = active_school.school_years.order(:year).all

      years.each do |y|
        if y.year == active_year.year
          html << "<li><a href='#' onclick=''><strong>#{y.title}</strong></a></li>"
        else
          html << "<li><a href='#' onclick=''>#{y.title}</a></li>"
        end
      end

      html << '</ul></div></div></li>'
    end

    html
  end

  def delete_link(url, options={})
    options = {
        :method => :delete,
        :data => {:confirm => l(:text_are_you_sure)},
        :class => 'icon icon-del'
    }.merge(options)

    link_to l(:button_delete), url, options
  end

  def back_url
    url = params[:back_url]
    if url.nil? && referer = request.env['HTTP_REFERER']
      url = CGI.unescape(referer.to_s)
    end
    url
  end

  def bs_text_field(form_helper, attribute, html_class = 'form-control')
    form_helper.text_field attribute, :class => html_class
  end

end
