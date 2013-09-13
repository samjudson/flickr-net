using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlickrNet;
using NUnit.Framework;

namespace FlickrNetWS.Tests
{
    public abstract class BaseTest
    {
        protected Flickr Instance;

        protected readonly TestData Data = new TestData();

        [SetUp]
        public void CreateFlickrInstance()
        {
            Instance = new Flickr(Data.ApiKey, Data.SharedSecret);
        }
    }

    public class TestData
    {
        public string ApiKey = "dbc316af64fb77dae9140de64262da0a";
        public string SharedSecret = "0781969a058a2745";
        public string UserId = "41888973@N00";

    }
}
