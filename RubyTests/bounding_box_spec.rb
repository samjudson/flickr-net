require 'setup'

describe 'Bounding Box in alternative cultures' do

	it 'test with french culture' do
	
		b = FlickrNet::BoundaryBox.UK
		d = System::Decimal.new(12.02)
		
		s1 = b.ToString
		s2 = d.ToString
		
		System::Threading::Thread.CurrentThread.CurrentCulture = System::Globalization::CultureInfo.CreateSpecificCulture("fr")
		
		s3 = b.ToString
		s4 = d.ToString

		s1.to_str.should == s3.to_str
		s2.to_str.should_not == s4.to_str
		
	end
	
end
