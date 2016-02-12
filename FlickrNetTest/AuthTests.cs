using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Xml;
using FlickrNet;
using NUnit.Framework;
using System;
using Shouldly;
using FlickrNet.Exceptions;

#pragma warning disable CS0618 // Type or member is obsolete

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for AuthTests
    /// </summary>
    [TestFixture]
    public class AuthTests : BaseTest
    {
        [Test]
        public void AuthGetFrobTest()
        {
            string frob = TestData.GetOldSignedInstance().AuthGetFrob();

            Assert.IsNotNull(frob, "frob should not be null.");
            Assert.AreNotEqual("", frob, "Frob should not be zero length string.");
        }

        [Test]
        [Ignore("Calling this will invalidate the existing token so use wisely.")]
        public void AuthGetFrobAsyncTest()
        {
            var w = new AsyncSubject<FlickrResult<string>>();

            TestData.GetOldSignedInstance().AuthGetFrobAsync(r => { w.OnNext(r); w.OnCompleted(); });

            var frobResult = w.Next().First();

            Assert.IsFalse(frobResult.HasError);

            string frob = frobResult.Result;

            Assert.IsNotNull(frob, "frob should not be null.");
            Assert.AreNotEqual("", frob, "Frob should not be zero length string.");
        }

        [Test]
        public void AuthGetFrobSignRequiredTest()
        {
            Action getFrobAction = () => Instance.AuthGetFrob();
            getFrobAction.ShouldThrow<SignatureRequiredException>();
        }

        [Test]
        public void AuthCalcUrlTest()
        {
            string frob = "abcdefgh";

            string url = TestData.GetOldSignedInstance().AuthCalcUrl(frob, AuthLevel.Read);

            Assert.IsNotNull(url, "url should not be null.");
        }

        [Test]
        public void AuthCalcUrlSignRequiredTest()
        {
            string frob = "abcdefgh";

            Action calcUrlAction = () => Instance.AuthCalcUrl(frob, AuthLevel.Read);
            calcUrlAction.ShouldThrow<SignatureRequiredException>();
        }

        [Test]
        [Ignore("No longer needed. Delete in future version")]
        public void AuthCheckTokenBasicTest()
        {
            Flickr f = TestData.GetOldAuthInstance();

            string authToken = f.AuthToken;

            Assert.IsNotNull(authToken, "authToken should not be null.");

            Auth auth = f.AuthCheckToken(authToken);

            Assert.IsNotNull(auth, "Auth should not be null.");
            Assert.AreEqual(authToken, auth.Token);
        }

        [Test]
        [Ignore("No longer needed. Delete in future version")]
        public void AuthCheckTokenCurrentTest()
        {
            Flickr f = TestData.GetOldAuthInstance();

            Auth auth = f.AuthCheckToken();

            Assert.IsNotNull(auth, "Auth should not be null.");
            Assert.AreEqual(f.AuthToken, auth.Token);
        }

        [Test]
        public void AuthCheckTokenSignRequiredTest()
        {
            string token = "abcdefgh";

            Should.Throw<SignatureRequiredException>(() => Instance.AuthCheckToken(token));
        }

        [Test]
        public void AuthCheckTokenInvalidTokenTest()
        {
            string token = "abcdefgh";

            Should.Throw<LoginFailedInvalidTokenException>(() => TestData.GetOldSignedInstance().AuthCheckToken(token));
        }

        [Test]
        public void AuthClassBasicTest()
        {
            string authResponse = "<auth><token>TheToken</token><perms>delete</perms><user nsid=\"41888973@N00\" username=\"Sam Judson\" fullname=\"Sam Judson\" /></auth>";

            var reader = new XmlTextReader(new StringReader(authResponse));
            reader.Read();

            var auth = new Auth();
            var parsable = auth as IFlickrParsable;

            parsable.Load(reader);

            Assert.AreEqual("TheToken", auth.Token);
            Assert.AreEqual(AuthLevel.Delete, auth.Permissions);
            Assert.AreEqual("41888973@N00", auth.User.UserId);
            Assert.AreEqual("Sam Judson", auth.User.UserName);
            Assert.AreEqual("Sam Judson", auth.User.FullName);

        }
    }
}

#pragma warning restore CS0618 // Type or member is obsolete
