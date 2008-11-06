require 'setup'

describe 'Place Info' do

	before :each do
		@o = FlickrNet::PhotoSearchOptions.new
		@o.PerPage = 10
	end

	it 'find by lat/lon' do
		# Search for newcastle
		place = @flickrApi.PlacesFindByLatLon 55.0, -1.6
		place.PlaceId.should_not == nil
		place.WoeId.should_not == nil
		
		place.PlaceId.to_str.should == 'IEcHLFCaAZwoKQ'
		place.WoeId.to_str.should == '30079'
		place.PlaceUrl.to_str.should == '/United+Kingdom/England/Newcastle+upon+Tyne'
		
		place.Latitude.should == System::Decimal.new(54.977)
		place.Longitude.should == System::Decimal.new(-1.612)
	end
	
	it 'resolve place id' do
		# Search for newcastle
		location = @flickrApi.PlacesGetInfo 'IEcHLFCaAZwoKQ', nil
		
		location.should_not == nil
		location.PlaceId.to_str.should == 'IEcHLFCaAZwoKQ'
		location.WoeId.to_str.should == '30079'
		
		location.Locality.should_not == nil
		location.Locality.PlaceId.to_str.should == 'IEcHLFCaAZwoKQ'
		location.Locality.Description.to_str.should == 'Newcastle upon Tyne'
		location.Locality.Latitude.should == System::Decimal.new(54.977)
		location.Locality.Longitude.should == System::Decimal.new(-1.612)

		location.County.should_not == nil
		location.County.PlaceId.to_str.should == '5La1sJqYA5qNBtPTrA'
		location.County.Description.to_str.should == 'Tyne and Wear'
		location.County.Latitude.should == System::Decimal.new(54.939)
		location.County.Longitude.should == System::Decimal.new(-1.600)

		location.Region.should_not == nil
		location.Region.PlaceId.to_str.should == 'pn4MsiGbBZlXeplyXg'
		location.Region.Description.to_str.should == 'England'
		location.Region.Latitude.should == System::Decimal.new(52.883)
		location.Region.Longitude.should == System::Decimal.new(-1.976)

		location.Country.should_not == nil
		location.Country.PlaceId.to_str.should == 'DevLebebApj4RVbtaQ'
		location.Country.Description.to_str.should == 'United Kingdom'
		location.Country.Latitude.should == System::Decimal.new(54.313)
		location.Country.Longitude.should == System::Decimal.new(-2.232)
	end 
	
	it 'resolve WOE id' do
		# Search for newcastle
		location = @flickrApi.PlacesResolvePlaceId nil, '30079'

		location.should_not == nil
		location.PlaceId.to_str.should == 'IEcHLFCaAZwoKQ'
		location.WoeId.to_str.should == '30079'
		
		location.Locality.should_not == nil
		location.Locality.PlaceId.to_str.should == 'IEcHLFCaAZwoKQ'
		location.Locality.Description.to_str.should == 'Newcastle upon Tyne'

		location.County.should_not == nil
		location.County.PlaceId.to_str.should == '5La1sJqYA5qNBtPTrA'

		location.Region.should_not == nil
		location.Region.PlaceId.to_str.should == 'pn4MsiGbBZlXeplyXg'

		location.Country.should_not == nil
		location.Country.PlaceId.to_str.should == 'DevLebebApj4RVbtaQ'
	end 
	
	it 'resolve place URL' do
		# Search for newcastle
		location = @flickrApi.PlacesGetInfoByUrl '/United+Kingdom/England/Newcastle+upon+Tyne'

		location.should_not == nil
		location.PlaceId.to_str.should == 'IEcHLFCaAZwoKQ'
		location.WoeId.to_str.should == '30079'
		
		location.Locality.should_not == nil
		location.Locality.PlaceId.to_str.should == 'IEcHLFCaAZwoKQ'
		location.Locality.Description.to_str.should == 'Newcastle upon Tyne'

		location.County.should_not == nil
		location.County.PlaceId.to_str.should == '5La1sJqYA5qNBtPTrA'

		location.Region.should_not == nil
		location.Region.PlaceId.to_str.should == 'pn4MsiGbBZlXeplyXg'

		location.Country.should_not == nil
		location.Country.PlaceId.to_str.should == 'DevLebebApj4RVbtaQ'
	end 
	
end
