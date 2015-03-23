using FlickrNet;

namespace FlickrNetPCL.Tests
{
    public abstract class TestBase
    {
        private Flickr _instance;
        private Flickr _authInstance;
        private Flickr _ignoreInstance;

        protected readonly TestData Data = new TestData();

        private Flickr GetInstance()
        {
            return new Flickr(Data.ApiKey, Data.SharedSecret);
        }

        private Flickr GetAuthInstance()
        {
            return new Flickr(Data.ApiKey, Data.SharedSecret)
                   {
                       OAuthAccessToken = Data.AccessToken,
                       OAuthAccessTokenSecret = Data.AccessTokenSecret
                   };
        }

        protected Flickr Instance
        {
            get { return _instance ?? (_instance = GetInstance()); }
        }

        protected Flickr AuthInstance
        {
            get { return _authInstance ?? (_authInstance = GetAuthInstance()); }
        }

        protected Flickr IgnoreInstance
        {
            get { return _ignoreInstance ?? (_ignoreInstance = GetAuthInstance()); }
        }

        protected bool InstanceUsed
        {
            get { return _instance != null; }
        }

        protected bool AuthInstanceUsed
        {
            get { return _authInstance != null; }
        }
    }
}