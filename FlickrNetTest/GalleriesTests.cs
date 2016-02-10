using System;
using System.Collections.Generic;

using NUnit.Framework;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for GalleriesTests
    /// </summary>
    [TestFixture]
    public class GalleriesTests : BaseTest
    {
        
        [Test]
        public void GalleriesGetListUserIdTest()
        {
            Flickr f = Instance;

            GalleryCollection galleries = f.GalleriesGetList(TestData.TestUserId);

            Assert.IsNotNull(galleries, "GalleryCollection should not be null.");
            Assert.AreNotEqual(0, galleries.Count, "Count should not be zero.");

            foreach (var g in galleries)
            {
                Assert.IsNotNull(g);
                Assert.IsNotNull(g.Title, "Title should not be null.");
                Assert.IsNotNull(g.GalleryId, "GalleryId should not be null.");
                Assert.IsNotNull(g.GalleryUrl, "GalleryUrl should not be null.");
            }
        }

        [Test]
        public void GalleriesGetListForPhotoTest()
        {
            string photoId = "2891347068";

            var galleries = Instance.GalleriesGetListForPhoto(photoId);

            Assert.IsNotNull(galleries, "GalleryCollection should not be null.");
            Assert.AreNotEqual(0, galleries.Count, "Count should not be zero.");

            foreach (var g in galleries)
            {
                Assert.IsNotNull(g);
                Assert.IsNotNull(g.Title, "Title should not be null.");
                Assert.IsNotNull(g.GalleryId, "GalleryId should not be null.");
                Assert.IsNotNull(g.GalleryUrl, "GalleryUrl should not be null.");
            }
        }

        [Test]
        public void GalleriesGetPhotos()
        {
            // Dogs + Tennis Balls
            // https://www.flickr.com/photos/lesliescarter/galleries/72157622656415345
            string galleryId = "13834290-72157622656415345";

            Flickr f = Instance;

            GalleryPhotoCollection photos = f.GalleriesGetPhotos(galleryId, PhotoSearchExtras.All);

            Console.WriteLine(f.LastRequest);

            Assert.IsNotNull(photos);
            Assert.AreEqual(18, photos.Count, "Count should be eighteen.");

            foreach (var photo in photos)
            {
                //This gallery has a comment on each photo.
                Assert.IsNotNull(photo.Comment, "GalleryPhoto.Comment shoult not be null.");
            }
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void GalleriesEditPhotosTest()
        {
            Flickr.FlushCache();
            Flickr.CacheDisabled = true;

            Flickr f = AuthInstance;

            string galleryId = "78188-72157622589312064";

            var gallery = f.GalleriesGetInfo(galleryId);

            Console.WriteLine("GalleryUrl = " + gallery.GalleryUrl);

            var photos = f.GalleriesGetPhotos(galleryId);

            var photoIds = new List<string>();

            foreach (var photo in photos) photoIds.Add(photo.PhotoId);

            f.GalleriesEditPhotos(galleryId, gallery.PrimaryPhotoId, photoIds);

            var photos2 = f.GalleriesGetPhotos(gallery.GalleryId);

            Assert.AreEqual(photos.Count, photos2.Count);

            for (int i = 0; i < photos.Count; i++)
            {
                Assert.AreEqual(photos[i].PhotoId, photos2[i].PhotoId);
            }
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void GalleriesEditMetaTest()
        {
            Flickr.FlushCache();
            Flickr.CacheDisabled = true;

            Flickr f = AuthInstance;

            string galleryId = "78188-72157622589312064";

            string title = "Great Entrances to Hell";
            string description = "A guide to what makes a great photo for the Entrances to Hell group: " +
                                 "<a href=\"https://www.flickr.com/groups/entrancetohell\">www.flickr.com/groups/entrancetohell</a>\n\n";
            description += DateTime.Now.ToString();

            f.GalleriesEditMeta(galleryId, title, description);

            Gallery gallery = f.GalleriesGetInfo(galleryId);

            Assert.AreEqual(title, gallery.Title);
            Assert.AreEqual(description, gallery.Description);
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void GalleriesEditPhotoTest()
        {
            Flickr.FlushCache();
            Flickr.CacheDisabled = true;

            string photoId = "486875512";
            string galleryId = "78188-72157622589312064";

            string comment = "You don't get much better than this for the best Entrance to Hell.\n\n" + DateTime.Now.ToString();

            Flickr f = AuthInstance;
            f.GalleriesEditPhoto(galleryId, photoId, comment);

            var photos = f.GalleriesGetPhotos(galleryId);

            bool found = false;

            foreach (var photo in photos)
            {
                if (photo.PhotoId == photoId)
                {
                    Assert.AreEqual(comment, photo.Comment, "Comment should have been updated.");
                    found = true;
                    break;
                }
            }

            Assert.IsTrue(found, "Should have found the photo in the gallery.");
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void GalleriesEditComplexTest()
        {
            Flickr.CacheDisabled = true;
            Flickr.FlushCache();

            string primaryPhotoId = "486875512";
            string comment = "You don't get much better than this for the best Entrance to Hell.\n\n" + DateTime.Now.ToString();
            string galleryId = "78188-72157622589312064";


            Flickr f = AuthInstance;

            // Get photos
            var photos = f.GalleriesGetPhotos(galleryId);

            var photoIds = new List<string>();
            foreach (var p in photos) photoIds.Add(p.PhotoId);

            // Remove the last one.
            GalleryPhoto photo = photos[photos.Count - 1];
            photoIds.Remove(photo.PhotoId);

            // Update the gallery
            f.GalleriesEditPhotos(galleryId, primaryPhotoId, photoIds);

            // Check removed photo no longer returned.
            var photos2 = f.GalleriesGetPhotos(galleryId);

            Assert.AreEqual(photos.Count - 1, photos2.Count, "Should be one less photo.");

            bool found = false;
            foreach (var p in photos2)
            {
                if (p.PhotoId == photo.PhotoId)
                {
                    found = true; 
                    break;
                }
            }
            Assert.IsFalse(false, "Should not have found the photo in the gallery.");

            // Add photo back in
            f.GalleriesAddPhoto(galleryId, photo.PhotoId, photo.Comment);

            var photos3 = f.GalleriesGetPhotos(galleryId);
            Assert.AreEqual(photos.Count, photos3.Count, "Count should match now photo added back in.");

            found = false;
            foreach (var p in photos3)
            {
                if (p.PhotoId == photo.PhotoId)
                {
                    Assert.AreEqual(photo.Comment, p.Comment, "Comment should have been updated.");
                    found = true;
                    break;
                }
            }

            Assert.IsTrue(found, "Should have found the photo in the gallery.");
        }

    }
}
