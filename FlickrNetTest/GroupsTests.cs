using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for GroupsBrowseTests
    /// </summary>
    [TestClass]
    public class GroupsTests
    {
        public GroupsTests()
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
        public void GroupsBrowseBasicTest()
        {
            Flickr f = TestData.GetAuthInstance();
            GroupCategory cat = f.GroupsBrowse();

            Assert.IsNotNull(cat, "GroupCategory should not be null.");
            Assert.AreEqual("/", cat.CategoryName, "CategoryName should be '/'.");
            Assert.AreEqual("/", cat.Path, "Path should be '/'.");
            Assert.AreEqual("", cat.PathIds, "PathIds should be empty string.");
            Assert.AreEqual(0, cat.Subcategories.Count, "No sub categories should be returned.");
            Assert.AreEqual(0, cat.Groups.Count, "No groups should be returned.");
        }

        [TestMethod]
        public void GroupsSearchBasicTest()
        {
            Flickr f = TestData.GetInstance();

            GroupSearchResultCollection results = f.GroupsSearch("Buses");

            Assert.IsNotNull(results, "GroupSearchResults should not be null.");
            Assert.AreNotEqual(0, results.Count, "Count should not be zero.");
            Assert.AreNotEqual(0, results.Total, "Total should not be zero.");
            Assert.AreNotEqual(0, results.PerPage, "PerPage should not be zero.");
            Assert.AreEqual(1, results.Page, "Page should be 1.");
            //Assert.AreEqual(Math.Min(results.Total, results.PerPage), results.Count, "Count should be minimum of Total and PerPage.");

            foreach (GroupSearchResult result in results)
            {
                Assert.IsNotNull(result.GroupId, "GroupId should not be null.");
                Assert.IsNotNull(result.GroupName, "GroupName should not be null.");
            }
        }

        [TestMethod]
        public void GroupsGetInfoBasicTest()
        {
            Flickr f = TestData.GetInstance();

            GroupFullInfo info = f.GroupsGetInfo(TestData.GroupId);

            Assert.IsNotNull(info, "GroupFullInfo should not be null");
            Assert.AreEqual(TestData.GroupId, info.GroupId);
            Assert.AreEqual("FLOWERS", info.GroupName);

            Assert.AreEqual("3304", info.IconServer);
            Assert.AreEqual("4", info.IconFarm);

            Assert.AreEqual<string>("http://farm4.staticflickr.com/3304/buddyicons/13378274@N00.jpg", info.GroupIconUrl);

            Assert.AreEqual(3, info.ThrottleInfo.Count);
            Assert.AreEqual(GroupThrottleMode.PerDay, info.ThrottleInfo.Mode);

            Assert.IsTrue(info.Restrictions.PhotosAccepted, "PhotosAccepted should be true.");
            Assert.IsFalse(info.Restrictions.VideosAccepted, "VideosAccepted should be false.");
        }

        [TestMethod]
        public void GroupsGetInfoNoGroupIconTest()
        {
            string groupId = "562176@N20";
            Flickr f = TestData.GetInstance();

            GroupFullInfo info = f.GroupsGetInfo(groupId);

            Assert.IsNotNull(info, "GroupFullInfo should not be null");
            Assert.AreEqual("0", info.IconServer, "Icon Server should be zero");
            Assert.AreEqual<string>("http://www.flickr.com/images/buddyicon.jpg", info.GroupIconUrl);

        }

        [TestMethod]
        public void GroupsMembersGetListBasicTest()
        {
            var ms = TestData.GetAuthInstance().GroupsMembersGetList(TestData.GroupId);

            Assert.IsNotNull(ms);
            Assert.AreNotEqual(0, ms.Count, "Count should not be zero.");
            Assert.AreNotEqual(0, ms.Total, "Total should not be zero.");
            Assert.AreEqual(1, ms.Page, "Page should be one.");
            Assert.AreNotEqual(0, ms.PerPage, "PerPage should not be zero.");
            Assert.AreNotEqual(0, ms.Pages, "Pages should not be zero.");

        }
    }
}
