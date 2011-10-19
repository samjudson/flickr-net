using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for OAuthTests
    /// </summary>
    [TestClass]
    public class OAuthTests
    {
        public OAuthTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        [Ignore]
        public void OAuthGetRequestTokenBasicTest()
        {
            Flickr f = TestData.GetSignedInstance();

            string callback = "oob";

            OAuthRequestToken requestToken = f.OAuthGetRequestToken(callback);

            Assert.IsNotNull(requestToken);
            Assert.IsNotNull(requestToken.Token, "Token should not be null.");
            Assert.IsNotNull(requestToken.TokenSecret, "TokenSecret should not be null.");

            System.Diagnostics.Process.Start(f.OAuthCalculateAuthorizationUrl(requestToken.Token, AuthLevel.Delete));

            Console.WriteLine("token = " + requestToken.Token);
            Console.WriteLine("token secret = " + requestToken.TokenSecret);

            TestData.RequestToken = requestToken.Token;
            TestData.RequestTokenSecret = requestToken.TokenSecret;
        }

        [TestMethod]
        [Ignore]
        public void OAuthGetAccessTokenBasicTest()
        {
            Flickr f = TestData.GetSignedInstance();

            OAuthRequestToken requestToken = new OAuthRequestToken();
            requestToken.Token = TestData.RequestToken;
            requestToken.TokenSecret = TestData.RequestTokenSecret;
            string verifier = "672-475-377";

            OAuthAccessToken accessToken = f.OAuthGetAccessToken(requestToken, verifier);

            Console.WriteLine("access token = " + accessToken.Token);
            Console.WriteLine("access token secret = " + accessToken.TokenSecret);

            TestData.AccessToken = accessToken.Token;
            TestData.AccessTokenSecret = accessToken.TokenSecret;
        }

        [TestMethod]
        public void OAuthPeopleGetPhotosBasicTest()
        {
            Flickr f = TestData.GetAuthInstance();

            PhotoCollection photos = f.PeopleGetPhotos("me");
        }

        [TestMethod]
        [ExpectedException(typeof(OAuthException))]
        public void OAuthInvalidAccessTokenTest()
        {
            Flickr.CacheDisabled = true;

            Flickr f = TestData.GetInstance();
            f.ApiSecret = "asdasd";
            f.OAuthGetRequestToken("oob");
        }
    }
}
