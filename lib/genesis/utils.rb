module Genesis
  module UTILS

    class << self

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

      def format_address(address_line_1, address_line_2, city, state, zip_code, zip_code_four = '', format = 1)
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

      def format_address_with__html_link(address_line_1, address_line_2, city, state, zip_code, zip_code_four = '', format = 1)
        formatted_address = format_address(address_line_1, address_line_2, city, state, zip_code, zip_code_four, format)
        parameters = format == 2 ? formatted_address : format_address(address_line_1, address_line_2, city, state, zip_code, zip_code_four, 2)

        "<a href=\"http://bing.com/maps?q=#{parameters}\" target=\"_blank\">#{formatted_address}</a>"
      end

    end

  end
end