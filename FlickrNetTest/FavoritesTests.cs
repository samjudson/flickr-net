using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for FavouritesGetPublicListTests
    /// </summary>
    [TestClass]
    public class FavoritesTests
    {
        public FavoritesTests()
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
        public void FavoritesGetPublicListBasicTest()
        {
            string userId = "77788903@N00";
            Flickr f = TestData.GetInstance();

            PhotoCollection p = f.FavoritesGetPublicList(userId);

            Assert.IsNotNull(p, "PhotoCollection should not be null instance.");
            Assert.AreNotEqual(0, p.Count, "PhotoCollection.Count should be greater than zero.");
        }

        [TestMethod]
        public void FavoritesGetListBasicTest()
        {
            Flickr f = TestData.GetAuthInstance();

            PhotoCollection photos = f.FavoritesGetList();
            Assert.IsNotNull(photos, "PhotoCollection should not be null instance.");
            Assert.AreNotEqual(0, photos.Count, "PhotoCollection.Count should be greater than zero.");
        }

        [TestMethod]
        public void FavoritesGetListFullParamTest()
        {
            Flickr f = TestData.GetAuthInstance();

            var photos = f.FavoritesGetList(TestData.TestUserId, DateTime.Now.AddYears(-1), DateTime.Now, PhotoSearchExtras.All, 1, 10);
            Assert.IsNotNull(photos, "PhotoCollection should not be null.");

            Assert.IsTrue(photos.Count > 0, "Count should be greater than zero.");

        }

        [TestMethod]
        public void FavoritesGetListPartialParamTest()
        {
            Flickr f = TestData.GetAuthInstance();

            PhotoCollection photos = f.FavoritesGetList(TestData.TestUserId, 2, 20);
            Assert.IsNotNull(photos, "PhotoCollection should not be null instance.");
            Assert.AreNotEqual(0, photos.Count, "PhotoCollection.Count should be greater than zero.");
            Assert.AreEqual(2, photos.Page);
            Assert.AreEqual(20, photos.PerPage);
            Assert.AreEqual(20, photos.Count);
        }

        [TestMethod]
        public void FavoritesGetContext()
        {
            Flickr f = TestData.GetInstance();
            string photoId = "2502963121";
            string userId = "41888973@N00";

            var context = f.FavoritesGetContext(photoId, userId);

            Assert.IsNotNull(context);
            Assert.AreNotEqual(0, context.Count, "Count should be greater than zero");
            Assert.AreEqual(1, context.PreviousPhotos.Count, "Should be 1 previous photo.");
            Assert.AreEqual(1, context.NextPhotos.Count, "Should be 1 next photo.");
        }

        [TestMethod]
        public void FavoritesGetContextMorePrevious()
        {
            Flickr f = TestData.GetInstance();
            string photoId = "2502963121";
            string userId = "41888973@N00";

            var context = f.FavoritesGetContext(photoId, userId, 3, 4, PhotoSearchExtras.Description);

            Assert.IsNotNull(context);
            Assert.AreNotEqual(0, context.Count, "Count should be greater than zero");
            Assert.AreEqual(3, context.PreviousPhotos.Count, "Should be 3 previous photo.");
            Assert.AreEqual(4, context.NextPhotos.Count, "Should be 4 next photo.");
        }

    }
}
