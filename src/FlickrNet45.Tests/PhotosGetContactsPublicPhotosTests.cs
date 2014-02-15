using FlickrNet;
using NUnit.Framework;

namespace FlickrNet45.Tests
{
    
    [TestFixture]
    public class PhotosGetContactsPublicPhotosTests : BaseTest
    {

        [Test()]
        public void PhotosGetContactsPublicPhotosUserIdExtrasTest()
        {
            string userId = Data.UserId;
            PhotoSearchExtras extras = PhotoSearchExtras.All;
            var photos = Instance.PhotosGetContactsPublicPhotos(userId, extras);

            Assert.IsNotNull(photos);
            Assert.AreNotEqual(0, photos.Count, "Should have returned more than 0 photos");
        }

        [Test]
        public void PhotosGetContactsPublicPhotosAllParamsTest()
        {
            var userId = Data.UserId;

            const int count = 4;
            const bool justFriends = true; 
            const bool singlePhoto = true; 
            const bool includeSelf = false;
            const PhotoSearchExtras extras = PhotoSearchExtras.None;

            var photos = Instance.PhotosGetContactsPublicPhotos(userId, count, justFriends, singlePhoto, includeSelf, extras);

            Assert.IsNotNull(photos);
            Assert.AreNotEqual(0, photos.Count, "Should have returned more than 0 photos");
        }

        [Test]
        public void PhotosGetContactsPublicPhotosExceptExtrasTest()
        {
            var userId = Data.UserId;

            const int count = 4; 
            const bool justFriends = true; 
            const bool singlePhoto = true; 
            const bool includeSelf = false;

            var photos = Instance.PhotosGetContactsPublicPhotos(userId, count, justFriends, singlePhoto, includeSelf);

            Assert.IsNotNull(photos);
            Assert.AreNotEqual(0, photos.Count, "Should have returned more than 0 photos");
        }

        [Test]
        public void PhotosGetContactsPublicPhotosUserIdTest()
        {
            string userId = Data.UserId;

            var photos = Instance.PhotosGetContactsPublicPhotos(userId);

            Assert.IsNotNull(photos);
            Assert.AreNotEqual(0, photos.Count, "Should have returned more than 0 photos");
        }

        [Test]
        public void PhotosGetContactsPublicPhotosUserIdCountExtrasTest()
        {
            string userId = Data.UserId;

            int count = 5; 
            PhotoSearchExtras extras = PhotoSearchExtras.None;

            var photos = Instance.PhotosGetContactsPublicPhotos(userId, count, extras);

            Assert.IsNotNull(photos);
            Assert.AreNotEqual(0, photos.Count, "Should have returned more than 0 photos");
        }

        [Test]
        public void PhotosGetContactsPublicPhotosUserIdCountTest()
        {
            string userId = Data.UserId;

            int count = 5;

            var photos = Instance.PhotosGetContactsPublicPhotos(userId, count);

            Assert.IsNotNull(photos);
            Assert.AreNotEqual(0, photos.Count, "Should have returned more than 0 photos");
        }
    }
}
