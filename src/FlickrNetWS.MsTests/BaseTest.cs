using FlickrNet;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace FlickrNetWS.MsTests
{
    public abstract class BaseTest
    {
        protected Flickr Instance;
        protected Flickr AuthInstance;

        protected readonly TestData Data = new TestData();

        [TestInitialize]
        public void CreateFlickrInstance()
        {
            Instance = new Flickr(Data.ApiKey, Data.SharedSecret);
            AuthInstance = new Flickr(Data.ApiKey, Data.SharedSecret)
                               {
                                   OAuthAccessToken = Data.AccessToken,
                                   OAuthAccessTokenSecret = Data.AccessTokenSecret
                               };
        }
    }
}