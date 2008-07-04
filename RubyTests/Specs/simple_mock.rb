# A simple Mock framework that uses only method_missing, alias_method,
# remove_method, and respond_to?

if !$simple_mock_loaded
  $simple_mock_loaded = true

  class Object
    def singleton
      class << self; self; end
    end

    alias_method :__ms_respond_to?, :respond_to?
    alias_method :__ms_method_missing, :method_missing

    def respond_to?(sym)
      unless @should_receive_method_list.nil?
        if @should_receive_method_list.include? sym.to_sym
          # Record the call to respond_to? 
          if @should_receive_method_list.include? :respond_to?
            Mock.report(self, :respond_to?, sym.to_sym)
          end
          return true
        end
        methods = Mock.get_method_missing_methods(self, :respond_to?)
        if methods.include? sym.to_sym
          return Mock.report(self, :respond_to?, sym.to_sym)
        end
      end
      __ms_respond_to?(sym)
    end

    def method_missing(sym, *args, &block)
      unless @should_receive_method_list.nil?
        if @should_receive_method_list.include?(sym.to_sym)
          return Mock.report(self, sym.to_sym, *args, &block)
        end

        if @should_receive_method_list.include?(:method_missing)
          methods = Mock.get_method_missing_methods(self, :method_missing)
          if methods.include? sym.to_sym
            # Parameters must include symbol name and rest of arguments
            rargs = [sym, *args]
            return Mock.report(self, :method_missing, *rargs, &block)
          end
        end
      end
      __ms_method_missing(sym, *args, &block)
    end

    def should_receive(sym, info = {:with => :any, :block => :any, :count => 1})
      if @should_receive_method_list.nil?
        @should_receive_method_list = [sym.to_sym]
      else
        @should_receive_method_list << sym.to_sym
      end

      unless sym.to_sym == :respond_to? || sym.to_sym == :method_missing
        if self.__ms_respond_to? sym.to_sym
          singleton.send :alias_method, :"__ms_#{sym}", sym.to_sym

          # Undefine the method from the singleton class so that we handle via method_missing
          singleton.send :undef_method, sym

          Mock.set_objects self, sym, :single_overridden
        else
          Mock.set_objects self, sym, :single_new
        end
      else
        Mock.set_objects self, sym, :single_overridden
      end

      info[:with]   = info[:with] || :any
      info[:block]  = info[:block] || :any
      info[:count]  = info[:count] || 1

      Mock.set_expect self, sym, info 
    end

    # Same as should_receive except that :count is 0
    def should_not_receive(sym, info = {:with => :any, :block => :any, :count => 0})
      info[:count] = 0
      should_receive sym, info
    end
  end

  module Mock
    # TODO: Today, we don't hash List<T> correctly (this is what an IronRuby array 
    # is today) so this class is a workaround placeholder 

    class Storage
      def initialize
        @hash = {}
      end

      def [](key)
        raise 'expects a tuple of size 2' if key.length != 2
        obj, sym = key.first, key.last
        return nil if @hash[obj].nil?
        result = @hash[obj][sym]
        result
      end

      def []=(key, value)
        raise 'expects a tuple of size 2' if key.length != 2
        obj, sym = key.first, key.last
        @hash[obj] = {} if @hash[obj].nil?
        @hash[obj][sym] = value
      end

      def each
        @hash.each do |obj, hash|
          unless hash[obj].nil?
            hash[obj].each do |sym, value|
              yield [obj, sym], value
            end
          end
        end
      end
    end

    def self.reset()
      @expects = Storage.new
      @objects = []
    end

    def self.set_expect(obj, sym, info)
      if @expects[[obj, sym]]
        if tmp = @expects[[obj, sym]].find { |i| i[:with] == info[:with] }
          @expects[[obj, sym]].delete(tmp)
        end

        @expects[[obj, sym]] << info
      else 
        @expects[[obj, sym]] = [ info ]
      end
    end

    def self.set_objects(obj, sym, type = nil)
      @objects << [obj, sym, type]
    end

    # Verify to correct number of calls
    def self.verify()
      @expects.each do |k, expects|
        expects.each do |info|
          obj, sym = k[0], k[1]

          if info[:count] != :never && info[:count] != :any
            if info[:count] > 0
              raise Exception.new("Method #{sym} with #{info[:with].inspect} and block #{info[:block].inspect} called too FEW times on object #{obj.inspect}")
            end
          end

        end
      end
    end

    # Clean up any methods we set up
    def self.cleanup()
      @objects.each do |info|
        obj, sym, type = info[0], info[1], info[2]

        hidden_name = "__ms_" + sym.to_s

        # Revert the object back to original if possible
        case type
        when :all_instances
          next

        when :single_new
          #          meta = class << obj; self; end
          #          meta.send :remove_method, sym.to_sym

        when :single_overridden
          meta = class << obj; self; end
        meta.module_eval { alias_method(sym.to_sym, hidden_name.to_sym) }
      end
    end
  end  

  def self.get_method_missing_methods(obj, sym)
    methods = []
    info = @expects[[obj, sym]]
    return methods if info.nil?
    info.each { |info| methods << info[:with].first unless info[:with] == :any }
    methods
  end

  # Invoked by a replaced method in an object somewhere.
  # Verifies that the method is called as expected with
  # the exception that calling the method too few times
  # is not detected until #verify_expects! gets called
  # which by default happens at the end of a #specify
  def self.report(obj, sym, *args, &block)
    info = @expects[[obj, sym]].find do |info| 
      info[:with] == :any || info[:with] == args 
    end
    return unless info

    unless info[:block] == :any
      if block
        unless info[:block]
          return
        end
      end
    end

    unless info[:count] == :any
      if info[:count] == :never
        raise Exception.new("Method #{sym} with #{info[:with].inspect} and block #{info[:block].inspect} should NOT be called on object #{obj.inspect}")
      end

      info[:count] = info[:count] - 1

      if info[:count] < 0
        raise Exception.new("Method #{sym} with #{info[:with].inspect} and block #{info[:block].inspect} called too MANY times on object #{obj.inspect}")
      end
    end

    return info[:returning]
  end
end

# Start up
Mock.reset

end
