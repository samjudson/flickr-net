using System;
using System.Linq;
using NUnit.Framework;
using FlickrNet;

namespace FlickrNet45.Tests
{
    /// <summary>
    /// Summary description for FavouritesGetPublicListTests
    /// </summary>
    [TestFixture]
    public class FavoritesTests : BaseTest
    {
        [Test]
        public void FavoritesGetPublicListBasicTest()
        {
            const string userId = "77788903@N00";

            var p = Instance.FavoritesGetPublicList(userId);

            Assert.IsNotNull(p, "PhotoCollection should not be null instance.");
            Assert.AreNotEqual(0, p.Count, "PhotoCollection.Count should be greater than zero.");
        }

        [Test]
        public void FavouritesGetPublicListWithDates()
        {
            var allFavourites = Instance.FavoritesGetPublicList(Data.UserId);

            var firstFiveFavourites = allFavourites.OrderBy(p => p.DateFavorited).Take(5).ToList();

            var minDate = firstFiveFavourites.Min(p => p.DateFavorited).GetValueOrDefault();
            var maxDate = firstFiveFavourites.Max(p => p.DateFavorited).GetValueOrDefault();

            var subsetOfFavourites = Instance.FavoritesGetPublicList(Data.UserId, minDate, maxDate,
                                                                     PhotoSearchExtras.None, 0, 0);

            Assert.AreEqual(5, subsetOfFavourites.Count, "Should be 5 favourites in subset");
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void FavoritesGetListBasicTest()
        {
            var photos = AuthInstance.FavoritesGetList();
            Assert.IsNotNull(photos, "PhotoCollection should not be null instance.");
            Assert.AreNotEqual(0, photos.Count, "PhotoCollection.Count should be greater than zero.");
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void FavoritesGetListFullParamTest()
        {
            var photos = AuthInstance.FavoritesGetList(Data.UserId, DateTime.Now.AddYears(-1), DateTime.Now, PhotoSearchExtras.All, 1, 10);
            Assert.IsNotNull(photos, "PhotoCollection should not be null.");

            Assert.IsTrue(photos.Count > 0, "Count should be greater than zero.");

        }

        [Test]
        [Category("AccessTokenRequired")]
        public void FavoritesGetListPartialParamTest()
        {
            PhotoCollection photos = AuthInstance.FavoritesGetList(Data.UserId, 2, 20);
            Assert.IsNotNull(photos, "PhotoCollection should not be null instance.");
            Assert.AreNotEqual(0, photos.Count, "PhotoCollection.Count should be greater than zero.");
            Assert.AreEqual(2, photos.Page);
            Assert.AreEqual(20, photos.PerPage);
            Assert.AreEqual(20, photos.Count);
        }

        [Test]
        public void FavoritesGetContext()
        {
            const string photoId = "2502963121";
            const string userId = "41888973@N00";

            var context = Instance.FavoritesGetContext(photoId, userId);

            Assert.IsNotNull(context);
            Assert.AreNotEqual(0, context.Count, "Count should be greater than zero");
            Assert.AreEqual(1, context.PreviousPhotos.Count, "Should be 1 previous photo.");
            Assert.AreEqual(1, context.NextPhotos.Count, "Should be 1 next photo.");
        }

        [Test]
        public void FavoritesGetContextMorePrevious()
        {
            const string photoId = "2502963121";
            const string userId = "41888973@N00";

            var context = Instance.FavoritesGetContext(photoId, userId, 3, 4, PhotoSearchExtras.Description);

            Assert.IsNotNull(context);
            Assert.AreNotEqual(0, context.Count, "Count should be greater than zero");
            Assert.AreEqual(3, context.PreviousPhotos.Count, "Should be 3 previous photo.");
            Assert.AreEqual(4, context.NextPhotos.Count, "Should be 4 next photo.");

            context = Instance.FavoritesGetContext(photoId, userId, 1, 2, PhotoSearchExtras.Description);
            Assert.AreEqual(1, context.PreviousPhotos.Count, "Should be 1 previous photo.");
            Assert.AreEqual(2, context.NextPhotos.Count, "Should be 2 next photo.");
        }

    }
}
