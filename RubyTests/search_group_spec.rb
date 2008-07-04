require 'setup'

describe 'Place Info' do

	it 'test group search' do
		o = FlickrNet::PhotoSearchOptions.new
		o.GroupId = '13378274@N00'
		o.PerPage = 10
		o.SortOrder = FlickrNet::PhotoSearchSortOrder.DatePostedAsc
		
		ps = @flickrApi.PhotosSearch o
		ps.should_not == nil
		
		ps.PhotoCollection.should_not == nil
		ps.PhotoCollection.Length.should_not == 0
		
		p = ps.PhotoCollection[0]
		
		p.PhotoId.to_str.should == '256591001'
		
	end
	
end