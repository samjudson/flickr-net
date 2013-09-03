using FlickrNet;
using NUnit.Framework;

namespace FlickrNet45.Tests
{
    public abstract class BaseTest
    {
        protected Flickr Instance;
        protected const string ApiKey = "dbc316af64fb77dae9140de64262da0a";
        protected const string SharedSecret = "0781969a058a2745";

        [SetUp]
        public void CreateFlickrInstance()
        {
            Instance = new Flickr(ApiKey, SharedSecret);
        }
    }
}