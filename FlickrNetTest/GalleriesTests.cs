using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for GalleriesTests
    /// </summary>
    [TestClass]
    public class GalleriesTests
    {
        public GalleriesTests()
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
        public void GalleriesGetListUserIdTest()
        {
            Flickr f = TestData.GetInstance();

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

        [TestMethod]
        public void GalleriesGetListForPhotoTest()
        {
            string photoId = "2891347068";

            var galleries = TestData.GetInstance().GalleriesGetListForPhoto(photoId);

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

        [TestMethod]
        public void GalleriesGetPhotos()
        {
            // Dogs + Tennis Balls
            // http://www.flickr.com/photos/lesliescarter/galleries/72157622656415345
            string galleryId = "13834290-72157622656415345";

            Flickr f = TestData.GetInstance();

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

        [TestMethod]
        public void GalleriesEditPhotosTest()
        {
            Flickr.FlushCache();
            Flickr.CacheDisabled = true;

            Flickr f = TestData.GetAuthInstance();

            string galleryId = "78188-72157622589312064";

            var gallery = f.GalleriesGetInfo(galleryId);

            Console.WriteLine("GalleryUrl = " + gallery.GalleryUrl);

            var photos = f.GalleriesGetPhotos(galleryId);

            List<string> photoIds = new List<string>();

            foreach (var photo in photos) photoIds.Add(photo.PhotoId);

            f.GalleriesEditPhotos(galleryId, gallery.PrimaryPhotoId, photoIds);

            var photos2 = f.GalleriesGetPhotos(gallery.GalleryId);

            Assert.AreEqual(photos.Count, photos2.Count);

            for (int i = 0; i < photos.Count; i++)
            {
                Assert.AreEqual(photos[i].PhotoId, photos2[i].PhotoId);
            }
        }

        [TestMethod]
        public void GalleriesEditMetaTest()
        {
            Flickr.FlushCache();
            Flickr.CacheDisabled = true;

            Flickr f = TestData.GetAuthInstance();

            string galleryId = "78188-72157622589312064";

            string title = "Great Entrances to Hell";
            string description = "A guide to what makes a great photo for the Entrances to Hell group: <a href=\"http://www.flickr.com/groups/entrancetohell\">www.flickr.com/groups/entrancetohell</a>\n\n";
            description += DateTime.Now.ToString();

            f.GalleriesEditMeta(galleryId, title, description);

            Gallery gallery = f.GalleriesGetInfo(galleryId);

            Assert.AreEqual(title, gallery.Title);
            Assert.AreEqual(description, gallery.Description);
        }

        [TestMethod]
        public void GalleriesEditPhotoTest()
        {
            Flickr.FlushCache();
            Flickr.CacheDisabled = true;

            string photoId = "486875512";
            string galleryId = "78188-72157622589312064";

            string comment = "You don't get much better than this for the best Entrance to Hell.\n\n" + DateTime.Now.ToString();

            Flickr f = TestData.GetAuthInstance();
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

        [TestMethod]
        public void GalleriesEditComplexTest()
        {
            Flickr.FlushCache();
            Flickr.CacheDisabled = true;

            string primaryPhotoId = "486875512";
            string comment = "You don't get much better than this for the best Entrance to Hell.\n\n" + DateTime.Now.ToString();
            string galleryId = "78188-72157622589312064";


            Flickr f = TestData.GetAuthInstance();

            // Get photos
            var photos = f.GalleriesGetPhotos(galleryId);

            List<string> photoIds = new List<string>();
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
