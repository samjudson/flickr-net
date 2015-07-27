using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for OAuthTests
    /// </summary>
    [TestFixture]
    public class OAuthTests
    {
        [Test]
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

        [Test]
        [Ignore]
        public void OAuthGetAccessTokenBasicTest()
        {
            Flickr f = TestData.GetSignedInstance();

            var requestToken = new OAuthRequestToken();
            requestToken.Token = TestData.RequestToken;
            requestToken.TokenSecret = TestData.RequestTokenSecret;
            string verifier = "736-824-579";

            OAuthAccessToken accessToken = f.OAuthGetAccessToken(requestToken, verifier);

            Console.WriteLine("access token = " + accessToken.Token);
            Console.WriteLine("access token secret = " + accessToken.TokenSecret);

            TestData.AccessToken = accessToken.Token;
            TestData.AccessTokenSecret = accessToken.TokenSecret;
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void OAuthPeopleGetPhotosBasicTest()
        {
            Flickr f = TestData.GetAuthInstance();

            PhotoCollection photos = f.PeopleGetPhotos("me");
        }

        [Test]
        [ExpectedException(typeof(OAuthException))]
        public void OAuthInvalidAccessTokenTest()
        {
            Flickr.CacheDisabled = true;

            Flickr f = TestData.GetInstance();
            f.ApiSecret = "asdasd";
            f.OAuthGetRequestToken("oob");
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void OAuthCheckTokenTest()
        {
            Flickr f = TestData.GetAuthInstance();

            Auth a = f.AuthOAuthCheckToken();

            Assert.AreEqual(a.Token, f.OAuthAccessToken);
        }

        [Test]
        public void OAuthCheckEncoding()
        {
            // Test cases taken from OAuth spec
            // http://wiki.oauth.net/w/page/12238556/TestCases
            var test = new Dictionary<string, string>()
            {
                { "abcABC123", "abcABC123" },
                { "-._~", "-._~"},
                {"%", "%25"},
                { "+", "%2B"},
                { "&=*", "%26%3D%2A"},
                { "", ""},
                { "\u000A", "%0A"},
                { "\u0020", "%20"},
                { "\u007F", "%7F"},
                { "\u0080", "%C2%80"},
                { "\u3001", "%E3%80%81"},
                { "$()", "%24%28%29"}
            };

            foreach (var pair in test)
            {
                Assert.AreEqual(pair.Value, UtilityMethods.EscapeOAuthString(pair.Key));
            }
        }
    }
}
