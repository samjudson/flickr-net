using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for GroupsPoolsGetGroupsTests
    /// </summary>
    [TestClass]
    public class GroupsPoolsTests
    {
        public GroupsPoolsTests()
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
        public void GroupsPoolGetPhotosDateAddedTest()
        {
            Flickr f = TestData.GetInstance();

            PhotoCollection photos = f.GroupsPoolsGetPhotos(TestData.GroupId);

            Assert.IsNotNull(photos, "Photos should not be null");
            Assert.IsTrue(photos.Count > 0, "Should be more than 0 photos returned");

            foreach (Photo p in photos)
            {
                Assert.AreNotEqual(default(DateTime), p.DateUploaded, "DateAdded should not be default value");
                Assert.IsTrue(p.DateUploaded < DateTime.Now, "DateAdded should be in the past");
            }

        }

        [TestMethod]
        public void GroupsPoolsGetGroupsBasicTest()
        {
            MemberGroupInfoCollection groups = TestData.GetAuthInstance().GroupsPoolsGetGroups();

            Assert.IsNotNull(groups, "MemberGroupInfoCollection should not be null.");
            Assert.AreNotEqual(0, groups.Count, "MemberGroupInfoCollection.Count should not be zero.");
        }
    }
}
