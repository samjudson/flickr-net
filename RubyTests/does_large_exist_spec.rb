require 'setup'

describe 'Does Large Exist' do

	before :each do
		@o = FlickrNet::PhotoSearchOptions.new
		@o.PerPage = 10
		@o.UserId = @test_data[:user_id]
		@o.Extras = FlickrNet::PhotoSearchExtras.All
	end

	it 'is large must return true' do

		@o.Tags = 'largeexists'
		
		ps = @flickrAuth.PhotosSearch @o
		
		ps.PhotoCollection.each do |p|
			p.DoesLargeExist.should == true
			
			s = @flickrAuth.DownloadPictureWithoutRedirect(p.LargeUrl)
			
			s.should_not == nil
			
			s.Close

		end
		
		
	end

	it 'is large must return false' do

		@o.Tags = 'largedoesntexist'
		
		ps = @flickrAuth.PhotosSearch @o
		i = 1
		
		ps.PhotoCollection.each do |p|
			p.DoesLargeExist.should == false
			
			s = @flickrAuth.DownloadPictureWithoutRedirect(p.LargeUrl)
			
			s.should == nil
		end
		
		
	end
	
end
