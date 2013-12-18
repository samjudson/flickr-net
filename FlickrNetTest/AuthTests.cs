using System;
using System.Text;
using System.Collections.Generic;

using NUnit.Framework;
using FlickrNet;
using System.Xml;
using System.IO;
using System.Linq;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for AuthTests
    /// </summary>
    [TestFixture]
    public class AuthTests
    {
        [Test]
        public void AuthGetFrobTest()
        {
            string frob = TestData.GetOldSignedInstance().AuthGetFrob();

            Assert.IsNotNull(frob, "frob should not be null.");
            Assert.AreNotEqual("", frob, "Frob should not be zero length string.");
        }

        [Test]
        [Ignore]
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
        [ExpectedException(typeof(SignatureRequiredException))]
        public void AuthGetFrobSignRequiredTest()
        {
            string frob = TestData.GetInstance().AuthGetFrob();
        }

        [Test]
        public void AuthCalcUrlTest()
        {
            string frob = "abcdefgh";

            string url = TestData.GetOldSignedInstance().AuthCalcUrl(frob, AuthLevel.Read);

            Assert.IsNotNull(url, "url should not be null.");
        }

        [Test]
        [ExpectedException(typeof(SignatureRequiredException))]
        public void AuthCalcUrlSignRequiredTest()
        {
            string frob = "abcdefgh";

            string url = TestData.GetInstance().AuthCalcUrl(frob, AuthLevel.Read);
        }

        [Test]
        [Ignore]
        // Test method for old auth no longer needed.
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
        [Ignore]
        // Test for old auth, which no longer is used.
        public void AuthCheckTokenCurrentTest()
        {
            Flickr f = TestData.GetOldAuthInstance();

            Auth auth = f.AuthCheckToken();

            Assert.IsNotNull(auth, "Auth should not be null.");
            Assert.AreEqual(f.AuthToken, auth.Token);
        }

        [Test]
        [ExpectedException(typeof(SignatureRequiredException))]
        public void AuthCheckTokenSignRequiredTest()
        {
            string token = "abcdefgh";

            TestData.GetInstance().AuthCheckToken(token);
        }

        [Test]
        [ExpectedException(typeof(FlickrNet.Exceptions.LoginFailedInvalidTokenException))]
        public void AuthCheckTokenInvalidTokenTest()
        {
            string token = "abcdefgh";

            TestData.GetOldSignedInstance().AuthCheckToken(token);
        }

        [Test]
        public void AuthClassBasicTest()
        {
            string authResponse = "<auth><token>TheToken</token><perms>delete</perms><user nsid=\"41888973@N00\" username=\"Sam Judson\" fullname=\"Sam Judson\" /></auth>";

            XmlTextReader reader = new XmlTextReader(new StringReader(authResponse));
            reader.Read();

            Auth auth = new Auth();
            IFlickrParsable parsable = auth as IFlickrParsable;

            parsable.Load(reader);

            Assert.AreEqual("TheToken", auth.Token);
            Assert.AreEqual(AuthLevel.Delete, auth.Permissions);
            Assert.AreEqual("41888973@N00", auth.User.UserId);
            Assert.AreEqual("Sam Judson", auth.User.UserName);
            Assert.AreEqual("Sam Judson", auth.User.FullName);

        }
    }
}
