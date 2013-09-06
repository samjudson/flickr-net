namespace FlickrNetWS.MsTests
{
    public class TestData
    {
        public string ApiKey = "dbc316af64fb77dae9140de64262da0a";
        public string SharedSecret = "0781969a058a2745";
        public string AccessToken
        {
            get { return GetSetting("AccessToken"); }
            set { SetSetting("AccessToken", value); }
        } 

        public string AccessTokenSecret 
        {
            get { return GetSetting("AccessTokenSecret"); }
            set { SetSetting("AccessTokenSecret", value); }
        } 

        public string UserId = "41888973@N00";

        private static string GetSetting(string key)
        {
            var settings = Windows.Storage.ApplicationData.Current;
            return settings.LocalSettings.Values[key] as string;
        }

        private static void SetSetting(string key, string value)
        {
            var settings = Windows.Storage.ApplicationData.Current;
            settings.LocalSettings.Values[key] = value;
        }
    }
}