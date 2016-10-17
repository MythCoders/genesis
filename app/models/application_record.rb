class ApplicationRecord < ActiveRecord::Base
  include ApplicationHelper
  self.abstract_class = true

  def formatted_full_name(format = 1)
    format_person_name(self.first_name, self.middle_name, self.last_name, self.suffix, format)
  end

  def formatted_address(format = 1)
    format_address(self.street, nil, self.city, self.state, self.zip_code, nil, format)
  end

  def method_missing(sym, *args)
    msg = "Unable to find method '#{sym}' on class '#{self.class.name}'. Args were: #{args.inspect}"
    logger.warn(msg)
    puts msg
  end

end
