require 'setup'
require 'System.Xml, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, processorArchitecture=MSIL'  

def serialize(obj)

	ms = System::IO::MemoryStream.new

	xs = System::Xml::Serialization::XmlSerializer.new(obj.GetType)

	xs.Serialize(ms, obj)

	return ms

end

def deserialize(ms, t)

	xs = System::Xml::Serialization::XmlSerializer.new(t)

	return xs.Deserialize(ms)

end


describe 'Auth serialization' do

	it 'save setting' do
	
		a = FlickrNet::Auth.new
		a.Token = "test token"
		a.User = FlickrNet::FoundUser.new
		a.User.UserId = @test_data[:user_id]
		
		ms = serialize(a)
		ms.Position = 0
		
		b = deserialize(ms, a.GetType)
		
		b.should_not == nil
		b.Token.should == a.Token
		b.User.should_not == nil
		b.User.UserId.should == a.User.UserId
	
	end

end


