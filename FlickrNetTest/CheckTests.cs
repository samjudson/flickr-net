using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlickrNet;
using NUnit.Framework;

namespace FlickrNetTest
{
    [TestFixture]
    public class CheckTests : BaseTest
    {
        [Test]
        [ExpectedException(typeof(ApiKeyRequiredException))]
        public void CheckApiKeyThrowsExceptionWhenNotPresent()
        {
            Flickr f = new Flickr();

            f.CheckApiKey();
        }

        [Test]
        public void CheckApiKeyDoesNotThrowWhenPresent()
        {
            Flickr f = new Flickr();
            f.ApiKey = "X";

            Assert.DoesNotThrow(f.CheckApiKey);
        }

        [Test]
        [ExpectedException(typeof(SignatureRequiredException))]
        public void CheckSignatureKeyThrowsExceptionWhenSecretNotPresent()
        {
            Flickr f = new Flickr();
            f.ApiKey = "X";
            f.CheckSigned();
        }

        [Test]
        public void CheckSignatureKeyDoesntThrowWhenSecretPresent()
        {
            Flickr f = new Flickr();
            f.ApiKey = "X";
            f.ApiSecret = "Y";

            Assert.DoesNotThrow(f.CheckSigned);
        }

        [Test]
        [ExpectedException(typeof(AuthenticationRequiredException))]
        public void CheckRequestAuthenticationThrowsExceptionWhenNothingPresent()
        {
            Flickr f = new Flickr();
            f.ApiKey = "X";
            f.ApiSecret = "Y";

            f.CheckRequiresAuthentication();
        }

        [Test]
        public void CheckRequestAuthenticationDoesNotThrowWhenAuthTokenPresent()
        {
            Flickr f = new Flickr();
            f.ApiKey = "X";
            f.ApiSecret = "Y";

            f.AuthToken = "Z";

            Assert.DoesNotThrow(f.CheckRequiresAuthentication);
        }

        [Test]
        public void CheckRequestAuthenticationDoesNotThrowWhenOAuthTokenPresent()
        {
            Flickr f = new Flickr();
            f.ApiKey = "X";
            f.ApiSecret = "Y";

            f.OAuthAccessToken = "Z1";
            f.OAuthAccessTokenSecret = "Z2";

            Assert.DoesNotThrow(f.CheckRequiresAuthentication);
        }
    }
}
