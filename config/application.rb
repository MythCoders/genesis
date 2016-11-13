require_relative 'boot'
require 'rails/all'
Bundler.require(*Rails.groups)

module GeneSISApp
  class Application < Rails::Application

    config.encoding = 'utf-8'

    config.autoload_paths << Rails.root.join('lib')

    # Configure Logging
    config.filter_parameters += [:password]
    config.log_level = Rails.env.production? ? :info : :debug

    # Enable the asset pipeline
    config.assets.enabled = true
    config.assets.version = '0.4'
    #config.app_generators.stylesheet_engine :less
  end
end
