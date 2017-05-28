require_relative 'mapper'

module Genesis
  module MenuManager

    class MenuError < StandardError; end #:nodoc;

    class << self
      def map(menu_name)
        @items ||= {}
        mapper = Mapper.new(menu_name.to_sym, @items)
        if block_given?
          yield mapper
        else
          mapper
        end
      end

      def items(menu_name)
        @items[menu_name.to_sym] || MenuNode.new(:root, {})
      end
    end

  end
end