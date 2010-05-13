using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for UrlsTests
    /// </summary>
    [TestClass]
    public class UrlsTests
    {
        public UrlsTests()
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
        public void UrlsLookupGalleryTest()
        {
            string galleryUrl = "http://www.flickr.com/photos/samjudson/galleries/72157622589312064";

            Flickr f = TestData.GetInstance();

            Gallery gallery = f.UrlsLookupGallery(galleryUrl);

            Assert.AreEqual<string>(galleryUrl, gallery.GalleryUrl);

        }

        [TestMethod]
        public void UrlsGetUserPhotosTest()
        {
            string url = TestData.GetInstance().UrlsGetUserPhotos(TestData.TestUserId);

            Assert.AreEqual<string>("http://www.flickr.com/photos/samjudson/", url);
        }

        [TestMethod]
        public void UrlsGetUserProfileTest()
        {
            string url = TestData.GetInstance().UrlsGetUserProfile(TestData.TestUserId);

            Assert.AreEqual<string>("http://www.flickr.com/people/samjudson/", url);
        }

        [TestMethod]
        public void UrlsGetGroupTest()
        {
            string url = TestData.GetInstance().UrlsGetGroup(TestData.GroupId);

            Assert.AreEqual<string>("http://www.flickr.com/groups/florus/", url);
        }



    }
}
