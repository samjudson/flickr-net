require 'setup'

describe 'Constructor test' do

	it 'everything should be null' do
		f = FlickrNet::Flickr.new
		f.ApiKey.should == nil
		f.ApiSecret.should == nil
		f.AuthToken.should == nil
	end
	
	it 'api key should be set' do
		f = FlickrNet::Flickr.new @settings[:api_key]
		f.ApiKey.to_str.should == @settings[:api_key]
		f.ApiSecret.should == nil
		f.AuthToken.should == nil
	end
	
	it 'api key and secret should be set' do
		f = FlickrNet::Flickr.new @settings[:api_key], @settings[:api_secret]
		f.ApiKey.to_str.should == @settings[:api_key]
		f.ApiSecret.to_str.should == @settings[:api_secret]
		f.AuthToken.should == nil
	end
	
	it 'api key, secret and auth_token should be set' do
		f = FlickrNet::Flickr.new @settings[:api_key], @settings[:api_secret], @settings[:auth_token]
		f.ApiKey.to_str.should == @settings[:api_key]
		f.ApiSecret.to_str.should == @settings[:api_secret]
		f.AuthToken.to_str.should == @settings[:auth_token]
	end
	
end
