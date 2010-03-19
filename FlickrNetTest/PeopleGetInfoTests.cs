using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PersonGetInfoTests
    /// </summary>
    [TestClass]
    public class PeopleGetInfoTests
    {
        public PeopleGetInfoTests()
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
        public void PersonGetInfoGenderNoAuthTest()
        {
            Flickr f = TestData.GetInstance();
            Person p = f.PeopleGetInfo("10973297@N00");

            Console.WriteLine(f.LastResponse);
            Assert.IsNotNull(p, "Person object should be returned");
            Assert.IsNull(p.Gender, "Gender should be null as not authenticated.");

            Assert.IsNull(p.IsReverseContact, "IsReverseContact should not be null.");
            Assert.IsNull(p.IsContact, "IsContact should be null.");
            Assert.IsNull(p.IsIgnored, "IsIgnored should be null.");
            Assert.IsNull(p.IsFriend, "IsFriend should be null.");
        }

        [TestMethod]
        public void PersonGetInfoGenderTest()
        {
            Flickr f = TestData.GetAuthInstance();
            Person p = f.PeopleGetInfo("10973297@N00");

            Console.WriteLine(f.LastResponse);
            Assert.IsNotNull(p, "Person object should be returned");
            Assert.AreEqual("F", p.Gender, "Gender of M should be returned");
        
            Assert.IsNotNull(p.IsReverseContact, "IsReverseContact should not be null.");
            Assert.IsNotNull(p.IsContact, "IsContact should not be null.");
            Assert.IsNotNull(p.IsIgnored, "IsIgnored should not be null.");
            Assert.IsNotNull(p.IsFriend, "IsFriend should not be null.");

            Assert.IsNotNull(p.PhotosSummary, "PhotosSummary should not be null.");
        }

        [TestMethod]
        public void PersonGetInfoSelfTest()
        {
            Flickr f = TestData.GetAuthInstance();

            Auth a = f.AuthCheckToken(f.AuthToken);

            Person p = f.PeopleGetInfo(a.User.UserId);

            Assert.IsNotNull(p.MailboxSha1Hash, "MailboxSha1Hash should not be null.");
            Assert.IsNotNull(p.PhotosSummary, "PhotosSummary should not be null.");
            Assert.AreNotEqual(0, p.PhotosSummary.Views, "PhotosSummary.Views should not be zero.");

        }
    }
}
