using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlickrNet;
using NUnit.Framework;

namespace FlickrNetWS.Tests
{
    [TestFixture]
    public class OAuthAccessTokenTests
    {
        [Test]
        public void ShouldParseWin8VerifierResponseCorrectly()
        {
            const string response =
                "ms-app://s-1-15-2-988245719-1757109990-2047434370-645059098-821926992-2695812667-2577404277/?oauth_token=72157635534512434-f32e5d7328c0bff0&oauth_verifier=2599eb91c9f54be4";

            var dic = UtilityMethods.StringToDictionary(response);

            Assert.AreEqual(2, dic.Count);

            Assert.IsTrue(dic.ContainsKey("oauth_token"));
            Assert.IsTrue(dic.ContainsKey("oauth_verifier"));
        }
    }
}
