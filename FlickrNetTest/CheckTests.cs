using FlickrNet;
using NUnit.Framework;

#pragma warning disable CS0618 // Type or member is obsolete

namespace FlickrNetTest
{
    [TestFixture]
    public class CheckTests : BaseTest
    {
        [Test]
        [ExpectedException(typeof(ApiKeyRequiredException))]
        public void CheckApiKeyThrowsExceptionWhenNotPresent()
        {
            var f = new Flickr();

            f.CheckApiKey();
        }

        [Test]
        public void CheckApiKeyDoesNotThrowWhenPresent()
        {
            var f = new Flickr();
            f.ApiKey = "X";

            Assert.DoesNotThrow(f.CheckApiKey);
        }

        [Test]
        [ExpectedException(typeof(SignatureRequiredException))]
        public void CheckSignatureKeyThrowsExceptionWhenSecretNotPresent()
        {
            var f = new Flickr();
            f.ApiKey = "X";
            f.CheckSigned();
        }

        [Test]
        public void CheckSignatureKeyDoesntThrowWhenSecretPresent()
        {
            var f = new Flickr();
            f.ApiKey = "X";
            f.ApiSecret = "Y";

            Assert.DoesNotThrow(f.CheckSigned);
        }

        [Test]
        [ExpectedException(typeof(AuthenticationRequiredException))]
        public void CheckRequestAuthenticationThrowsExceptionWhenNothingPresent()
        {
            var f = new Flickr();
            f.ApiKey = "X";
            f.ApiSecret = "Y";

            f.CheckRequiresAuthentication();
        }

        [Test]
        public void CheckRequestAuthenticationDoesNotThrowWhenAuthTokenPresent()
        {
            var f = new Flickr();
            f.ApiKey = "X";
            f.ApiSecret = "Y";

            f.AuthToken = "Z";

            Assert.DoesNotThrow(f.CheckRequiresAuthentication);
        }

        [Test]
        public void CheckRequestAuthenticationDoesNotThrowWhenOAuthTokenPresent()
        {
            var f = new Flickr();
            f.ApiKey = "X";
            f.ApiSecret = "Y";

            f.OAuthAccessToken = "Z1";
            f.OAuthAccessTokenSecret = "Z2";

            Assert.DoesNotThrow(f.CheckRequiresAuthentication);
        }
    }
}
