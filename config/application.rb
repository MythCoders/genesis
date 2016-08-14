require_relative 'boot'
require 'rails/all'
Bundler.require(*Rails.groups)

module GeneSISApp
  class Application < Rails::Application
    config.autoload_paths << Rails.root.join('lib')
    config.filter_parameters += [:password]
    config.assets.version = '0.3'
    config.log_level = Rails.env.production? ? :info : :debug
  end
end
