# Be sure to restart your server when you modify this file.

require 'genesis'
I18n.default_locale = 'en'
I18n.backend = Genesis::I18n::Backend.new
# Forces I18n to load available locales from the backend
I18n.config.available_locales = nil

Genesis::Module.load
Genesis::Module.mirror_assets