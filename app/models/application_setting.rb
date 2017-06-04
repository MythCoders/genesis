class ApplicationSetting < ApplicationRecord

  VALID_VALUE_TYPES = ['string', 'integer', 'date', 'datetime', 'boolean']

  class << self

    def get_all
      ApplicationSetting.order('key')
    end

    def get(key)
      ApplicationSetting.where(:key => key).first
    end

    def get_value(key)
      get(key).value
    end

    def get_value_as(key)
      setting = get(key)
      setting.value as setting.value_type
    end

    def set(key, value)
      setting = get(key)
      raise StandardError 'UNSET APPLICATION SETTING. PLEASE RESTART GENESIS!' if setting.nil?
      #TODO: validate against the setting type
      setting.update(value: value)
    end

    def remove(key)
      #TODO: Remove ApplicationSetting
    end

    def populate(key, value, value_type, module_name)
      setting = get(key)
      if setting.nil?
        raise 'Unknown value_type for ApplicationSetting' unless VALID_VALUE_TYPES.include?(value_type)

        setting = ApplicationSetting.new(:key => key, :value => value, :value_type => value_type, :module_name => module_name)
        setting.save

        Rails.logger.info "Added a new application setting, #{key}, for the #{module_name} module"
      else
        raise Genesis::ModuleCompatibilityError.new("An application setting with that key is already in use by another module: #{setting.module_name}") if setting.module_name != module_name
      end
      ApplicationSetting.define(key)
    end

    def define(key)
      src = <<-END_SRC
      def self.#{key}
        ApplicationSetting.get_value('#{key}')
      end
      def self.#{key}?
        ApplicationSetting.get_value_as('#{key}')
      end
      def self.#{key}=(value)
        ApplicationSetting.set('#{key}', value)
      end
      END_SRC
      class_eval src, __FILE__, __LINE__
    end

  end

end