using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for GroupsGetInfoTest
    /// </summary>
    [TestClass]
    public class GroupsGetInfoTest
    {
        public GroupsGetInfoTest()
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
        public void TestBasicGroupsGetInfo()
        {
            Flickr f = TestData.GetInstance();

            GroupFullInfo info = f.GroupsGetInfo(TestData.GroupId);

            Assert.IsNotNull(info, "GroupFullInfo should not be null");
            Assert.AreEqual(TestData.GroupId, info.GroupId);
            Assert.AreEqual("FLOWERS", info.GroupName);

            Assert.AreEqual("3304", info.IconServer);
            Assert.AreEqual("4", info.IconFarm);

            Assert.AreEqual("http://farm4.static.flickr.com/3304/buddyicons/13378274@N00.jpg", info.GroupIconUrl);
        }

        [TestMethod]
        public void TestNoGroupIcon()
        {
            string groupId = "562176@N20";
            Flickr f = TestData.GetInstance();

            GroupFullInfo info = f.GroupsGetInfo(groupId);

            Assert.IsNotNull(info, "GroupFullInfo should not be null");
            Assert.AreEqual("0", info.IconServer, "Icon Server should be zero");
            Assert.AreEqual("http://www.flickr.com/images/buddyicon.jpg", info.GroupIconUrl);

        }
    }
}
