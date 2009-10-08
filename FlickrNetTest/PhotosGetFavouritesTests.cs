using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PhotosGetFavouritesTests
    /// </summary>
    [TestClass]
    public class PhotosGetFavouritesTests
    {
        Flickr f = TestData.GetInstance();

        public PhotosGetFavouritesTests()
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
        public void TestGetFavouritesNoFavourites()
        {
            // No favourites
            PhotoFavourite[] favs = f.PhotosGetFavorites(TestData.PhotoId, 100, 1);

            Assert.AreEqual(0, favs.Length, "Should have no favourites");

        }

        [TestMethod]
        public void TestGetFavouritesHasFavourites()
        {
            PhotoFavourite[] favs = f.PhotosGetFavorites(TestData.FavouritedPhotoId, 500, 1);

            Assert.IsNotNull(favs, "Should not be null");

            Assert.IsTrue(favs.Length > 0, "Should have favourites");

            Assert.AreEqual(50, favs.Length, "Should be 50 favourites listed (maximum returned)");

            foreach (PhotoFavourite p in favs)
            {
                Assert.IsFalse(String.IsNullOrEmpty(p.UserId), "Should have a user ID.");
                Assert.IsFalse(String.IsNullOrEmpty(p.UserName), "Should have a user name.");
                Assert.AreNotEqual(default(DateTime), p.FavoriteDate, "Favourite Date should not be default Date value");
                Assert.IsTrue(p.FavoriteDate < DateTime.Now, "Favourite Date should be in the past.");

            }

        }
    }
}
