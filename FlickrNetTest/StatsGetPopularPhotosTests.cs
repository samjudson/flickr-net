using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    [TestClass]
    public class StatsGetPopularPhotosTests
    {
        public StatsGetPopularPhotosTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        [TestMethod]
        public void StatsGetPopularPhotosBasic()
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

        [TestMethod]
        public void StatsGetPopularPhotosNoParamsTest()
        {
            Flickr f = TestData.GetAuthInstance();

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

        [TestMethod]
        public void StatsGetPopularPhotosOtherTest()
        {
            var lastWeek = DateTime.Today.AddDays(-7);

            Flickr f = TestData.GetAuthInstance();

            var photos = f.StatsGetPopularPhotos(lastWeek);
            Assert.IsNotNull(photos, "PopularPhotos should not be null.");

            photos = f.StatsGetPopularPhotos(PopularitySort.Favorites);
            Assert.IsNotNull(photos, "PopularPhotos should not be null.");

            photos = f.StatsGetPopularPhotos(lastWeek, 1, 10);
            Assert.IsNotNull(photos, "PopularPhotos should not be null.");
            Assert.AreEqual(10, photos.Count, "Date search popular photos should return 10 photos.");

            photos = f.StatsGetPopularPhotos(PopularitySort.Favorites, 1, 10);
            Assert.IsNotNull(photos, "PopularPhotos should not be null.");
            Assert.AreEqual(10, photos.Count, "Favorite search popular photos should return 10 photos.");

        }
    }
}
