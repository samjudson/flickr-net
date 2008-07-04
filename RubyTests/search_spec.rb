require 'setup'

describe 'Simple searches' do

	before :each do
		@o = FlickrNet::PhotoSearchOptions.new
		@o.PerPage = 10
	end
	
	it 'check user id' do
	
		@o.UserId = @test_data[:user_id]
		ps = @flickrApi.PhotosSearch @o
		
		ps.PhotoCollection.should_not == nil
		ps.PhotoCollection.Length.should_not == 0
		ps.PhotoCollection.each do |p|
			p.UserId.to_str.should == @test_data[:user_id]
		end
	end
	
	it 'check no extras' do
	
		@o.UserId = @test_data[:user_id]
		ps = @flickrApi.PhotosSearch @o
		
		ps.PhotoCollection.should_not == nil
		ps.PhotoCollection.Length.should_not == 0
		ps.PhotoCollection.each do |p|
			p.CleanTags.should == nil
			p.OwnerName.should == nil
			p.MachineTags.should == nil
			p.OriginalFormat.should == nil
			p.IconServer.should == nil
			p.Latitude.should == System::Decimal.new(0)
			p.Longitude.should == System::Decimal.new(0)
			p.Accuracy.should == FlickrNet::GeoAccuracy.None
			p.DateTaken.should == System::DateTime.MinValue
			p.DateAdded.should == System::DateTime.MinValue
			p.DateUploaded.should == System::DateTime.MinValue
			p.LastUpdated.should == System::DateTime.MinValue
			p.OriginalHeight.should == -1
			p.OriginalWidth.should == -1
			p.License.should == nil
			p.Views.should == -1
			p.Media.should == nil
			p.MediaStatus.should == nil
		end
	end

	it 'check extras' do
		@o.UserId = @test_data[:user_id]
		@o.Extras = FlickrNet::PhotoSearchExtras.All
		ps = @flickrApi.PhotosSearch @o

		ps.PhotoCollection.should_not == nil
		ps.PhotoCollection.Length.should_not == 0
		ps.PhotoCollection.each do |p|
			p.CleanTags.should_not == nil
			p.OwnerName.should_not == nil
			p.MachineTags.should_not == nil
			p.OriginalFormat.should_not == nil
			p.IconServer.should_not == nil
			p.DateTaken.should_not == System::DateTime.MinValue
			p.DateUploaded.should_not == System::DateTime.MinValue
			p.LastUpdated.should_not == System::DateTime.MinValue
			p.OriginalHeight.should_not == -1
			p.OriginalWidth.should_not == -1
			p.License.should_not == nil
			p.Views.should_not == -1
			p.Media.should_not == nil
			p.MediaStatus.should_not == nil
		end
	end

	it 'check locaility info extras' do
		@o.UserId = @test_data[:user_id]
		@o.Extras = FlickrNet::PhotoSearchExtras.Geo
		@o.BoundaryBox = FlickrNet::BoundaryBox.World
		ps = @flickrApi.PhotosSearch @o

		ps.PhotoCollection.should_not == nil
		ps.PhotoCollection.Length.should_not == 0
		ps.PhotoCollection.each do |p|
			p.Latitude.should_not == System::Decimal.new(0)
			p.Longitude.should_not == System::Decimal.new(0)
			p.Accuracy.should_not == FlickrNet::GeoAccuracy.None
		end
	end
	
	it 'check original' do
		@o.UserId = @test_data[:user_id]
		@o.Extras = FlickrNet::PhotoSearchExtras.OriginalDimensions
		ps = @flickrApi.PhotosSearch @o

		ps.PhotoCollection.should_not == nil
		ps.PhotoCollection.Length.should_not == 0
		ps.PhotoCollection.each do |p|	
			p.OriginalHeight.should_not == -1
			p.OriginalWidth.should_not == -1
		end
	end
	
	it 'check owner name' do
		@o.UserId = @test_data[:user_id]
		@o.Extras = FlickrNet::PhotoSearchExtras.OwnerName
		ps = @flickrApi.PhotosSearch @o

		ps.PhotoCollection.should_not == nil
		ps.PhotoCollection.Length.should_not == 0
		ps.PhotoCollection.each do |p|
			p.OwnerName.to_str.should == @test_data[:user_name]
		end
	end
	
	it 'check icon server' do
		@o.UserId = @test_data[:user_id]
		@o.Extras = FlickrNet::PhotoSearchExtras.IconServer
		ps = @flickrApi.PhotosSearch @o

		ps.PhotoCollection.should_not == nil
		ps.PhotoCollection.Length.should_not == 0
		ps.PhotoCollection.each do |p|
			p.IconServer.should_not == nil
		end
	end
	
	it 'check date taken' do
		@o.UserId = @test_data[:user_id]
		@o.Extras = FlickrNet::PhotoSearchExtras.DateTaken
		ps = @flickrApi.PhotosSearch @o

		ps.PhotoCollection.should_not == nil
		ps.PhotoCollection.Length.should_not == 0
		ps.PhotoCollection.each do |p|
			p.DateTaken.should_not == System::DateTime.MinValue
		end
	end
	
	it 'check date uploaded' do
		@o.UserId = @test_data[:user_id]
		@o.Extras = FlickrNet::PhotoSearchExtras.DateUploaded
		ps = @flickrApi.PhotosSearch @o

		ps.PhotoCollection.should_not == nil
		ps.PhotoCollection.Length.should_not == 0
		ps.PhotoCollection.each do |p|
			p.DateUploaded.should_not == System::DateTime.MinValue
		end
	end

	it 'check last updated' do
		@o.UserId = @test_data[:user_id]
		@o.Extras = FlickrNet::PhotoSearchExtras.LastUpdated
		ps = @flickrApi.PhotosSearch @o

		ps.PhotoCollection.should_not == nil
		ps.PhotoCollection.Length.should_not == 0
		ps.PhotoCollection.each do |p|
			p.LastUpdated.should_not == System::DateTime.MinValue
		end
	end
	
	it 'check tag search' do
	
		@o.Tags = @test_data[:tag]
		@o.Extras = FlickrNet::PhotoSearchExtras.Tags
		
		ps = @flickrApi.PhotosSearch @o
		
		ps.PhotoCollection.should_not == nil
		ps.PhotoCollection.Length.should_not == 0
		ps.PhotoCollection.each do |p|
			p.CleanTags.should_not == nil
			p.CleanTags.split.index @test_data[:tag] .should_not == nil
		end
	end
	
	it 'check machine tag search' do
	
		@o.MachineTags = @test_data[:machine_tag]
		@o.Extras = FlickrNet::PhotoSearchExtras.MachineTags
		
		ps = @flickrApi.PhotosSearch @o
		
		ps.PhotoCollection.should_not == nil
		ps.PhotoCollection.Length.should_not == 0
		ps.PhotoCollection.each do |p|
			p.MachineTags.should_not == nil
		end

	end

end

