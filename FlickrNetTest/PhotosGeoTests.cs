using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PhotosGeoTests
    /// </summary>
    [TestClass]
    public class PhotosGeoTests
    {
        public PhotosGeoTests()
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
        public void PhotosGeoPhotosForLocationBasicTest()
        {
            Flickr f = TestData.GetAuthInstance();
            PhotoSearchOptions o = new PhotoSearchOptions();
            o.UserId = TestData.TestUserId;
            o.HasGeo = true;
            o.PerPage = 1;
            o.Extras = PhotoSearchExtras.Geo;

            var photos = f.PhotosSearch(o);
            var photo = photos[0];

            var photos2 = f.PhotosGeoPhotosForLocation(photo.Latitude, photo.Longitude, photo.Accuracy, PhotoSearchExtras.All, 0, 0);

            Assert.IsNotNull(photos2, "PhotosGeoPhotosForLocation should not return null.");
            Assert.IsTrue(photos2.Count > 0, "Should return one or more photos.");

            foreach (var p in photos2)
            {
                Assert.IsNotNull(p.PhotoId);
                Assert.AreNotEqual(0, p.Longitude);
                Assert.AreNotEqual(0, p.Latitude);
            }

        }
    }
}
