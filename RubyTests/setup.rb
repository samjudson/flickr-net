require 'Specs\spec_helper'
require 'mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, processorArchitecture=x86'  
require 'System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, processorArchitecture=MSIL'  
require '..\FlickrNet\bin\Debug\FlickrNet.dll'

@settings = { 
	:api_key => 'dbc316af64fb77dae9140de64262da0a', 
	:api_secret => '0781969a058a2745', 
	:auth_token => '107943-d3eb4c0b6b08a79a'}
	
@test_data = {
	:user_id => '41888973@N00',
	:user_name => 'Sam Judson',
	:tag => 'microsoft',
	:machine_tag => 'geo:lat=*'}

FlickrNet::Flickr.CacheDisabled = true;

@flickrApi = FlickrNet::Flickr.new @settings[:api_key]
@flickrShared = FlickrNet::Flickr.new @settings[:api_key], @settings[:api_secret]
@flickrAuth = FlickrNet::Flickr.new @settings[:api_key],@settings[:api_secret], @settings[:auth_token]
