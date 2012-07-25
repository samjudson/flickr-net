using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    [TestClass]
    public class AuthTests
    {
        public AuthTests()
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
        public void AuthGetFrobTest()
        {
            string frob = TestData.GetOldSignedInstance().AuthGetFrob();

            Assert.IsNotNull(frob, "frob should not be null.");
            Assert.AreNotEqual("", frob, "Frob should not be zero length string.");
        }

        [TestMethod]
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

        [TestMethod]
        [ExpectedException(typeof(SignatureRequiredException))]
        public void AuthGetFrobSignRequiredTest()
        {
            string frob = TestData.GetInstance().AuthGetFrob();
        }

        [TestMethod]
        public void AuthCalcUrlTest()
        {
            string frob = "abcdefgh";

            string url = TestData.GetOldSignedInstance().AuthCalcUrl(frob, AuthLevel.Read);

            Assert.IsNotNull(url, "url should not be null.");
        }

        [TestMethod]
        [ExpectedException(typeof(SignatureRequiredException))]
        public void AuthCalcUrlSignRequiredTest()
        {
            string frob = "abcdefgh";

            string url = TestData.GetInstance().AuthCalcUrl(frob, AuthLevel.Read);
        }

        [TestMethod]
        public void AuthCheckTokenBasicTest()
        {
            Flickr f = TestData.GetOldAuthInstance();

            string authToken = f.AuthToken;

            Assert.IsNotNull(authToken, "authToken should not be null.");

            Auth auth = f.AuthCheckToken(authToken);

            Assert.IsNotNull(auth, "Auth should not be null.");
            Assert.AreEqual(authToken, auth.Token);
        }

        [TestMethod]
        public void AuthCheckTokenCurrentTest()
        {
            Flickr f = TestData.GetOldAuthInstance();

            Auth auth = f.AuthCheckToken();

            Assert.IsNotNull(auth, "Auth should not be null.");
            Assert.AreEqual(f.AuthToken, auth.Token);
        }

        [TestMethod]
        [ExpectedException(typeof(SignatureRequiredException))]
        public void AuthCheckTokenSignRequiredTest()
        {
            string token = "abcdefgh";

            TestData.GetInstance().AuthCheckToken(token);
        }

        [TestMethod]
        [ExpectedException(typeof(FlickrNet.Exceptions.LoginFailedInvalidTokenException))]
        public void AuthCheckTokenInvalidTokenTest()
        {
            string token = "abcdefgh";

            TestData.GetOldSignedInstance().AuthCheckToken(token);
        }

        [TestMethod]
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
