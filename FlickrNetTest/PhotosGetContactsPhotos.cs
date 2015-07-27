using System;
using FlickrNet;
using NUnit.Framework;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for FlickrPhotosGetContactsPhotos
    /// </summary>
    [TestFixture]
    [Category("AccessTokenRequired")]
    public class PhotosGetContactsPhotos
    {
        readonly Flickr f = TestData.GetAuthInstance();

        public PhotosGetContactsPhotos()
        {
            Flickr.CacheDisabled = true;
        }

        [Test]
        [ExpectedException(typeof(SignatureRequiredException))]
        public void PhotosGetContactsPhotosSignatureRequiredTest()
        {
            Flickr f = TestData.GetInstance();
            f.PhotosGetContactsPhotos();
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PhotosGetContactsPhotosIncorrectCountTest()
        {
            Flickr f = TestData.GetAuthInstance();
            f.PhotosGetContactsPhotos(51);
        }

        [Test]
        public void PhotosGetContactsPhotosBasicTest()
        {
            PhotoCollection photos = f.PhotosGetContactsPhotos(10);

            Assert.IsTrue(photos.Count > 0, "Should return some photos");
            Assert.AreEqual(10, photos.Count, "Should return 10 photos");

        }

        [Test]
        public void PhotosGetContactsPhotosExtrasTest()
        {
            PhotoCollection photos = f.PhotosGetContactsPhotos(10, false, false, false, PhotoSearchExtras.All);

            Assert.IsTrue(photos.Count > 0, "Should return some photos");
            Assert.AreEqual(10, photos.Count, "Should return 10 photos");

            foreach (Photo p in photos)
            {
                Assert.IsNotNull(p.OwnerName, "OwnerName should not be null");
                Assert.AreNotEqual(default(DateTime), p.DateTaken, "DateTaken should not be default DateTime");
            }
        }
    }
}
