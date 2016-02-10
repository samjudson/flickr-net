using System;

using NUnit.Framework;
using FlickrNet;

namespace FlickrNetTest
{
    [TestFixture]
    [Category("AccessTokenRequired")]
    public class StatsGetPopularPhotosTests : BaseTest
    {
        [Test]
        public void StatsGetPopularPhotosBasic()
        {
            PopularPhotoCollection photos = AuthInstance.StatsGetPopularPhotos(DateTime.MinValue, PopularitySort.None, 0, 0);

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

        [Test]
        public void StatsGetPopularPhotosNoParamsTest()
        {
            Flickr f = AuthInstance;

            PopularPhotoCollection photos = f.StatsGetPopularPhotos();

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

        [Test]
        public void StatsGetPopularPhotosOtherTest()
        {
            var lastWeek = DateTime.Today.AddDays(-7);

            var photos = AuthInstance.StatsGetPopularPhotos(lastWeek);
            Assert.IsNotNull(photos, "PopularPhotos should not be null.");

            photos = AuthInstance.StatsGetPopularPhotos(PopularitySort.Favorites);
            Assert.IsNotNull(photos, "PopularPhotos should not be null.");

            photos = AuthInstance.StatsGetPopularPhotos(lastWeek, 1, 10);
            Assert.IsNotNull(photos, "PopularPhotos should not be null.");
            Assert.AreEqual(10, photos.Count, "Date search popular photos should return 10 photos.");

            photos = AuthInstance.StatsGetPopularPhotos(PopularitySort.Favorites, 1, 10);
            Assert.IsNotNull(photos, "PopularPhotos should not be null.");
            Assert.AreEqual(10, photos.Count, "Favorite search popular photos should return 10 photos.");

        }
    }
}
