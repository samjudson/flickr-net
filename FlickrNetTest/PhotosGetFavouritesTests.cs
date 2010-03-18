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
            PhotoFavoriteCollection favs = f.PhotosGetFavorites(TestData.PhotoId, 100, 1);

            Console.WriteLine(f.LastRequest);
            Console.WriteLine(f.LastResponse);

            Assert.AreEqual(0, favs.Count, "Should have no favourites");

        }

        [TestMethod]
        public void TestGetFavouritesHasFavourites()
        {
            PhotoFavoriteCollection favs = f.PhotosGetFavorites(TestData.FavouritedPhotoId, 500, 1);

            Assert.IsNotNull(favs, "PhotoFavourites instance should not be null.");

            Assert.IsTrue(favs.Count > 0, "PhotoFavourites.Count should not be zero.");

            Assert.AreEqual(50, favs.Count, "Should be 50 favourites listed (maximum returned)");

            foreach (PhotoFavorite p in favs)
            {
                Assert.IsFalse(String.IsNullOrEmpty(p.UserId), "Should have a user ID.");
                Assert.IsFalse(String.IsNullOrEmpty(p.UserName), "Should have a user name.");
                Assert.AreNotEqual(default(DateTime), p.FavoriteDate, "Favourite Date should not be default Date value");
                Assert.IsTrue(p.FavoriteDate < DateTime.Now, "Favourite Date should be in the past.");
            }
        }

        [TestMethod]
        public void TestGetFavouritesPaging()
        {
            PhotoFavoriteCollection favs = f.PhotosGetFavorites(TestData.FavouritedPhotoId, 10, 1);

            Assert.AreEqual(10, favs.Count, "PhotoFavourites.Count should be 10.");
            Assert.AreEqual(10, favs.PerPage, "PhotoFavourites.PerPage should be 10");
            Assert.AreEqual(1, favs.Page, "PhotoFavourites.Page should be 1.");
            Assert.IsTrue(favs.Total > 100, "PhotoFavourites.Total should be greater than 100.");
            Assert.IsTrue(favs.Pages > 10, "PhotoFavourites.Pages should be greater than 10.");
        }

        [TestMethod]
        public void TestGetFavouritesPagingTwo()
        {
            PhotoFavoriteCollection favs = f.PhotosGetFavorites(TestData.FavouritedPhotoId, 10, 2);

            Assert.AreEqual(10, favs.Count, "PhotoFavourites.Count should be 10.");
            Assert.AreEqual(10, favs.PerPage, "PhotoFavourites.PerPage should be 10");
            Assert.AreEqual(2, favs.Page, "PhotoFavourites.Page should be 2.");
        }
    }
}
