using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PeopleTests
    /// </summary>
    [TestClass]
    public class PeopleTests
    {
        public PeopleTests()
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
        public void PeopleFindByUsername()
        {
            Flickr f = TestData.GetAuthInstance();

            FoundUser user = f.PeopleFindByUserName("Sam Judson");

            Assert.AreEqual("41888973@N00", user.UserId);
            Assert.AreEqual("Sam Judson", user.UserName);
        }

        [TestMethod]
        public void PeopleFindByEmail()
        {
            Flickr f = TestData.GetAuthInstance();

            FoundUser user = f.PeopleFindByEmail("samjudson@gmail.com");

            Assert.AreEqual("41888973@N00", user.UserId);
            Assert.AreEqual("Sam Judson", user.UserName);
        }

        [TestMethod]
        public void UrlsLookupUserTest1()
        {
            Flickr f = TestData.GetAuthInstance();

            FoundUser user = f.UrlsLookupUser("http://www.flickr.com/photos/samjudson");

            Assert.AreEqual("41888973@N00", user.UserId);
            Assert.AreEqual("Sam Judson", user.UserName);
        }

        [TestMethod]
        public void UrlsLookupGroup()
        {
            string groupUrl = "http://www.flickr.com/groups/angels_of_the_north/";

            Flickr f = TestData.GetAuthInstance();

            string groupId = f.UrlsLookupGroup(groupUrl);

            Assert.AreEqual("71585219@N00", groupId);
        }

        [TestMethod]
        public void PeopleGetPublicGroupsBasicTest()
        {
            Flickr f = TestData.GetAuthInstance();

            PublicGroupInfoCollection groups = f.PeopleGetPublicGroups(TestData.TestUserId);

            Assert.AreNotEqual(0, groups.Count, "PublicGroupInfoCollection.Count should not be zero.");

            foreach(PublicGroupInfo group in groups)
            {
                Assert.IsNotNull(group.GroupId, "GroupId should not be null.");
                Assert.IsNotNull(group.GroupName, "GroupName should not be null.");
            }
        }

        [TestMethod]
        public void PeopleGetUploadStatusBasicTest()
        {
            var u = TestData.GetAuthInstance().PeopleGetUploadStatus();

            Assert.IsNotNull(u);
            Assert.IsNotNull(u.UserId);
            Assert.IsNotNull(u.UserName);
            Assert.AreNotEqual(0, u.FileSizeMax);
        }
    }
}
