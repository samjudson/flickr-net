using System;
using FlickrNet;
using NUnit.Framework;

namespace FlickrNet45.Tests
{
    /// <summary>
    /// Summary description for PhotosGetFavouritesTests
    /// </summary>
    [TestFixture]
    public class PhotosGetFavouritesTests : BaseTest
    {
        [Test]
        public void PhotosGetFavoritesNoFavourites()
        {
            // No favourites
            PhotoFavoriteCollection favs = Instance.PhotosGetFavorites(Data.PhotoId);

            Assert.AreEqual(0, favs.Count, "Should have no favourites");

        }

        [Test]
        public void PhotosGetFavoritesHasFavourites()
        {
            PhotoFavoriteCollection favs = Instance.PhotosGetFavorites(Data.FavouritedPhotoId, 1, 500);

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

        [Test]
        public void PhotosGetFavoritesPaging()
        {
            PhotoFavoriteCollection favs = Instance.PhotosGetFavorites(Data.FavouritedPhotoId, 1, 10);

            Assert.AreEqual(10, favs.Count, "PhotoFavourites.Count should be 10.");
            Assert.AreEqual(10, favs.PerPage, "PhotoFavourites.PerPage should be 10");
            Assert.AreEqual(1, favs.Page, "PhotoFavourites.Page should be 1.");
            Assert.IsTrue(favs.Total > 100, "PhotoFavourites.Total should be greater than 100.");
            Assert.IsTrue(favs.Pages > 10, "PhotoFavourites.Pages should be greater than 10.");
        }

        [Test]
        public void PhotosGetFavoritesPagingTwo()
        {
            PhotoFavoriteCollection favs = Instance.PhotosGetFavorites(Data.FavouritedPhotoId, 2, 10);

            Assert.AreEqual(10, favs.Count, "PhotoFavourites.Count should be 10.");
            Assert.AreEqual(10, favs.PerPage, "PhotoFavourites.PerPage should be 10");
            Assert.AreEqual(2, favs.Page, "PhotoFavourites.Page should be 2.");
        }
    }
}
