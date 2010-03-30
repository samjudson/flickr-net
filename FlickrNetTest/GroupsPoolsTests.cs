using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;
using System.IO;

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
        public void GroupsPoolsAddBasicTest()
        {
            Flickr f = TestData.GetAuthInstance();

            byte[] imageBytes = TestData.TestImageBytes;
            Stream s = new MemoryStream(imageBytes);
            s.Position = 0;

            string title = "Test Title";
            string desc = "Test Description\nSecond Line";
            string tags = "testtag1,testtag2";
            string photoId = f.UploadPicture(s, "Test.jpg", title, desc, tags, false, false, false, ContentType.Other, SafetyLevel.Safe, HiddenFromSearch.Visible);

            try
            {
                f.GroupsPoolsAdd(photoId, TestData.FlickrNetTestGroupId);
            }
            finally
            {
                f.PhotosDelete(photoId);
            }

        }

        [TestMethod]
        [ExpectedException(typeof(SignatureRequiredException))]
        public void GroupsPoolsAddNotAuthTestTest()
        {
            Flickr f = TestData.GetAuthInstance();

            byte[] imageBytes = TestData.TestImageBytes;
            Stream s = new MemoryStream(imageBytes);
            s.Position = 0;

            string title = "Test Title";
            string desc = "Test Description\nSecond Line";
            string tags = "testtag1,testtag2";
            string photoId = f.UploadPicture(s, "Test.jpg", title, desc, tags, false, false, false, ContentType.Other, SafetyLevel.Safe, HiddenFromSearch.Visible);

            try
            {
                TestData.GetInstance().GroupsPoolsAdd(photoId, TestData.FlickrNetTestGroupId);
            }
            finally
            {
                f.PhotosDelete(photoId);
            }

        }

        [TestMethod]
        public void GroupsPoolGetPhotosFullParamTest()
        {
            Flickr f = TestData.GetInstance();

            PhotoCollection photos = f.GroupsPoolsGetPhotos(TestData.GroupId, null, TestData.TestUserId, PhotoSearchExtras.All, 1, 20);

            Assert.IsNotNull(photos, "Photos should not be null");
            Assert.IsTrue(photos.Count > 0, "Should be more than 0 photos returned");
            Assert.AreEqual(20, photos.PerPage);
            Assert.AreEqual(1, photos.Page);

            foreach (Photo p in photos)
            {
                Assert.AreNotEqual(default(DateTime), p.DateAddedToGroup, "DateAddedToGroup should not be default value");
                Assert.IsTrue(p.DateAddedToGroup < DateTime.Now, "DateAddedToGroup should be in the past");
            }

        }

        [TestMethod]
        public void GroupsPoolGetPhotosDateAddedTest()
        {
            Flickr f = TestData.GetInstance();

            PhotoCollection photos = f.GroupsPoolsGetPhotos(TestData.GroupId);

            Assert.IsNotNull(photos, "Photos should not be null");
            Assert.IsTrue(photos.Count > 0, "Should be more than 0 photos returned");

            foreach (Photo p in photos)
            {
                Assert.AreNotEqual(default(DateTime), p.DateAddedToGroup, "DateAddedToGroup should not be default value");
                Assert.IsTrue(p.DateAddedToGroup < DateTime.Now, "DateAddedToGroup should be in the past");
            }

        }

        [TestMethod]
        public void GroupsPoolsGetGroupsBasicTest()
        {
            MemberGroupInfoCollection groups = TestData.GetAuthInstance().GroupsPoolsGetGroups();

            Assert.IsNotNull(groups, "MemberGroupInfoCollection should not be null.");
            Assert.AreNotEqual(0, groups.Count, "MemberGroupInfoCollection.Count should not be zero.");
            Assert.IsTrue(groups.Count > 1, "Count should be greater than one.");
        }
    }
}
