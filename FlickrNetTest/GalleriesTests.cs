using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for GalleriesTests
    /// </summary>
    [TestClass]
    public class GalleriesTests
    {
        public GalleriesTests()
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
        public void GalleriesGetListUserIdTest()
        {
            Flickr f = TestData.GetInstance();

            GalleryCollection galleries = f.GalleriesGetList(TestData.TestUserId);

            Assert.IsNotNull(galleries, "GalleryCollection should not be null.");
            Assert.AreNotEqual(0, galleries.Count, "Count should not be zero.");

            foreach (var g in galleries)
            {
                Assert.IsNotNull(g);
                Assert.IsNotNull(g.Title, "Title should not be null.");
                Assert.IsNotNull(g.GalleryId, "GalleryId should not be null.");
                Assert.IsNotNull(g.GalleryUrl, "GalleryUrl should not be null.");
            }
        }

        [TestMethod]
        public void GalleriesGetListForPhotoTest()
        {
            string photoId = "3992605178";

            var galleries = TestData.GetInstance().GalleriesGetListForPhoto(photoId);

            Assert.IsNotNull(galleries, "GalleryCollection should not be null.");
            Assert.AreNotEqual(0, galleries.Count, "Count should not be zero.");

            foreach (var g in galleries)
            {
                Assert.IsNotNull(g);
                Assert.IsNotNull(g.Title, "Title should not be null.");
                Assert.IsNotNull(g.GalleryId, "GalleryId should not be null.");
                Assert.IsNotNull(g.GalleryUrl, "GalleryUrl should not be null.");
            }
        }
    }
}
