module Genesis
  module MenuManager

    class MenuNode
      include Enumerable
      attr_accessor :parent
      attr_reader :last_items_count, :name

      def initialize(name, content = nil)
        @name = name
        @children = []
        @last_items_count = 0
      end

      def children
        if block_given?
          @children.each {|child| yield child}
        else
          @children
        end
      end

      # Returns the number of descendants + 1
      def size
        @children.inject(1) {|sum, node| sum + node.size}
      end

      def each(&block)
        yield self
        children { |child| child.each(&block) }
      end

      # Adds a child at first position
      def prepend(child)
        add_at(child, 0)
      end

      # Adds a child at given position
      def add_at(child, position)
        raise 'Child already added' if find {|node| node.name == child.name}

        @children = @children.insert(position, child)
        child.parent = self
        child
      end

      # Adds a child as last child
      def add_last(child)
        add_at(child, -1)
        @last_items_count += 1
        child
      end

      # Adds a child
      def add(child)
        position = @children.size - @last_items_count
        add_at(child, position)
      end
      alias :<< :add

      # Removes a child
      def remove!(child)
        @children.delete(child)
        @last_items_count -= +1 if child && child.last
        child.parent = nil
        child
      end

      # Returns the position for this node in it's parent
      def position
        self.parent.children.index(self)
      end

      # Returns the root for this node
      def root
        root = self
        root = root.parent while root.parent
        root
      end
    end

  end
end