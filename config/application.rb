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

    # setup bower components folder for lookup
    config.assets.paths << Rails.root.join('vendor', 'assets', 'bower_components')
    # fonts
    config.assets.precompile << /\.(?:svg|eot|woff|ttf)$/
    # images
    config.assets.precompile << /\.(?:png|jpg)$/
    # precompile vendor assets
    config.assets.precompile += %w( base.js )
    config.assets.precompile += %w( base.css )
    # precompile themes
    config.assets.precompile += ['angle/themes/theme-a.css',
                                 'angle/themes/theme-b.css',
                                 'angle/themes/theme-c.css',
                                 'angle/themes/theme-d.css',
                                 'angle/themes/theme-e.css',
                                 'angle/themes/theme-f.css',
                                 'angle/themes/theme-g.css',
                                 'angle/themes/theme-h.css']
  end
end
