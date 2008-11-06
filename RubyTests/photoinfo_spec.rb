require 'setup'

describe 'Simple searches' do

	before :each do
		@o = FlickrNet::PhotoSearchOptions.new
		@o.UserId = @test_data[:user_id];
		@o.PerPage = 1
	end
	
	it 'check basics' do
	
		ps = @flickrApi.PhotosSearch @o
		
		ps.PhotoCollection.should_not == nil
		ps.PhotoCollection.Length.should_not == 0
		ps.PhotoCollection.each do |p|
			p.UserId.to_str.should == @test_data[:user_id]
			p.PhotoId.to_str.should_not == nil
		end
	end
	
	it 'usage' do
	
		ps = @flickrApi.PhotosSearch @o
		
		ps.PhotoCollection.should_not == nil
		ps.PhotoCollection.Length.should_not == 0
		ps.PhotoCollection.each do |p|
			pi = @flickrApi.PhotosGetInfo(p.PhotoId)
			pi.Usage.should_not == nil
			pi.Usage.CanBlog.should == 0
			pi.Usage.CanDownload.should == 1
			pi.Usage.CanPrint.should == 0
		end

	end

	it 'usage auth' do
	
		ps = @flickrAuth.PhotosSearch @o
		
		ps.PhotoCollection.should_not == nil
		ps.PhotoCollection.Length.should_not == 0
		ps.PhotoCollection.each do |p|
			pi = @flickrAuth.PhotosGetInfo(p.PhotoId)
			pi.Usage.should_not == nil
			pi.Usage.CanBlog.should == 1
			pi.Usage.CanDownload.should == 1
			pi.Usage.CanPrint.should == 1
		end

	end

end

