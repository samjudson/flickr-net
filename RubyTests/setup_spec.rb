require 'setup'

describe 'setup tests' do
	it 'settings work' do
		@settings[:api_key].should == 'dbc316af64fb77dae9140de64262da0a'
		@settings[:api_secret].should == '0781969a058a2745'
		@settings[:auth_token].should == '107943-d3eb4c0b6b08a79a'
	end

	it 'test data work' do
		@test_data[:user_id].should_not == nil
	end
end
