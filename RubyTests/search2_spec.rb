require 'setup'

describe 'More searches' do

	before :each do
		@o = FlickrNet::PhotoSearchOptions.new
		@o.PerPage = 10
	end

	it 'does safe search' do
		begin
			@o.SafeSearch = FlickrNet::SafetyLevel.Safe
			@o.Tags = @test_data[:tag]

			ps = @flickrApi.PhotosSearch @o

			ps.PhotoCollection.should_not == nil
			ps.PhotoCollection.Length.should_not == 0
		rescue Exception
			puts @flickrApi.LastRequest
		end

	end
	
	it 'does content search' do
		begin
			@o.ContentType = FlickrNet::ContentTypeSearch.PhotosOnly
			@o.Tags = @test_data[:tag]

			ps = @flickrApi.PhotosSearch @o

			ps.PhotoCollection.should_not == nil
			ps.PhotoCollection.Length.should_not == 0
		rescue Exception
			puts @flickrApi.LastRequest
		end
	end
	
	it 'do lat/lon search' do
		@o.UserId = @test_data[:user_id]
		@o.Latitude = 55.0
		@o.Longitude = -1.6
		@o.Radius = 20.0
		@o.RadiusUnits = FlickrNet::RadiusUnits.Miles
		@o.Extras = FlickrNet::PhotoSearchExtras.Geo
		
		ps = @flickrApi.PhotosSearch @o

		ps.PhotoCollection.should_not == nil
		ps.PhotoCollection.Length.should_not == 0
		ps.PhotoCollection.each do |p|
			p.Latitude.should_not == System::Decimal.new(0)
			p.Longitude.should_not == System::Decimal.new(0)
			p.Accuracy.should_not == FlickrNet::GeoAccuracy.None
		end
		
	end
	
	it 'page results' do
		@o.UserId = @test_data[:user_id]
		@o.Page = 1
		
		ps1 = @flickrApi.PhotosSearch @o
		
		@o.Page = 2
		
		ps2 = @flickrApi.PhotosSearch @o
		
		ps1.should_not == nil
		ps2.should_not == nil
		ps1.PhotoCollection.should_not == nil
		ps2.PhotoCollection.should_not == nil
		ps1.PhotoCollection.Length.should_not == 0
		ps2.PhotoCollection.Length.should_not == 0
		
		ps1.PhotoCollection[0].PhotoId.should_not == ps2.PhotoCollection[0].PhotoId
	end
	
end
