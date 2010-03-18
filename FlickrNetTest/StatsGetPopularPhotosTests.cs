using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for StatsGetPopularPhotosTests
    /// </summary>
    [TestClass]
    public class StatsGetPopularPhotosTests
    {
        public StatsGetPopularPhotosTests()
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
        public void SatsGetPopularPhotosBasic()
        {
            Flickr f = TestData.GetAuthInstance();

            PopularPhotoCollection photos = f.StatsGetPopularPhotos(DateTime.MinValue, PopularitySort.None, 0, 0);

            Assert.IsNotNull(photos, "PopularPhotos should not be null.");

            Assert.AreNotEqual(0, photos.Total, "PopularPhotos.Total should not be zero.");
            Assert.AreNotEqual(0, photos.Count, "PopularPhotos.Count should not be zero.");
            Assert.AreEqual(photos.Count, Math.Min(photos.Total, photos.PerPage), "PopularPhotos.Count should equal either PopularPhotos.Total or PopularPhotos.PerPage.");

            foreach (Photo p in photos)
            {
                Assert.IsNotNull(p.PhotoId, "Photo.PhotoId should not be null.");
            }

            foreach (PopularPhoto p in photos)
            {
                Assert.IsNotNull(p.PhotoId, "PopularPhoto.PhotoId should not be null.");
                Assert.AreNotEqual(0, p.StatViews, "PopularPhoto.StatViews should not be zero.");
            }
        }
    }
}
