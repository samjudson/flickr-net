using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;
using System.Xml;
using System.IO;

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
