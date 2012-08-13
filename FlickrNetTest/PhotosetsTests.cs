using System;
using System.IO;
using System.Linq;
using FlickrNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for FlickrPhotosetsGetList
    /// </summary>
    [TestClass]
    public class PhotosetsTests
    {
        public PhotosetsTests()
        {
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
        public void GetContextTest()
        {
            var f = TestData.GetInstance();
            string photosetId = "72157594532130119";

            var photos = f.PhotosetsGetPhotos(photosetId);

            var firstPhoto = photos.First();
            var lastPhoto = photos.Last();

            var context1 = f.PhotosetsGetContext(firstPhoto.PhotoId, photosetId);

            Assert.IsNotNull(context1, "Context should not be null.");
            Assert.IsNull(context1.PreviousPhoto, "PreviousPhoto should be null for first photo.");
            Assert.IsNotNull(context1.NextPhoto, "NextPhoto should not be null.");

            if (firstPhoto.PhotoId != lastPhoto.PhotoId)
            {
                Assert.AreEqual(photos[1].PhotoId, context1.NextPhoto.PhotoId, "NextPhoto should be the second photo in photoset.");
            }

            var context2 = f.PhotosetsGetContext(lastPhoto.PhotoId, photosetId);

            Assert.IsNotNull(context2, "Last photo context should not be null.");
            Assert.IsNotNull(context2.PreviousPhoto, "PreviousPhoto should not be null for first photo.");
            Assert.IsNull(context2.NextPhoto, "NextPhoto should be null.");

            if (firstPhoto.PhotoId != lastPhoto.PhotoId)
            {
                Assert.AreEqual(photos[photos.Count - 2].PhotoId, context2.PreviousPhoto.PhotoId, "PreviousPhoto should be the last but one photo in photoset.");
            }
        }


        [TestMethod]
        public void PhotosetsGetInfoBasicTest()
        {
            string photosetId = "72157594532130119";

            var p = TestData.GetInstance().PhotosetsGetInfo(photosetId);

            Assert.IsNotNull(p);
            Assert.AreEqual(photosetId, p.PhotosetId);
            Assert.AreEqual("Places: Derwent Walk, Gateshead", p.Title);
            Assert.AreEqual("It's near work, so I go quite a bit...", p.Description);
        }

        [TestMethod]
        public void PhotosetsGetListBasicTest()
        {
            PhotosetCollection photosets = TestData.GetInstance().PhotosetsGetList(TestData.TestUserId);

            Assert.IsTrue(photosets.Count > 0, "Should be at least one photoset");
            Assert.IsTrue(photosets.Count > 100, "Should be greater than 100 photosets. (" + photosets.Count + " returned)");

            foreach (Photoset set in photosets)
            {
                Assert.IsNotNull(set.OwnerId, "OwnerId should not be null");
                Assert.IsTrue(set.NumberOfPhotos > 0, "NumberOfPhotos should be greater than zero");
                Assert.IsNotNull(set.Title, "Title should not be null");
                Assert.IsNotNull(set.Description, "Description should not be null");
                Assert.AreEqual(TestData.TestUserId, set.OwnerId);
            }
        }

        [TestMethod]
        public void PhotosetsGetListWebUrlTest()
        {
            PhotosetCollection photosets = TestData.GetInstance().PhotosetsGetList(TestData.TestUserId);

            Assert.IsTrue(photosets.Count > 0, "Should be at least one photoset");

            foreach (Photoset set in photosets)
            {
                Assert.IsNotNull(set.Url);
                string expectedUrl = "http://www.flickr.com/photos/" + TestData.TestUserId + "/sets/" + set.PhotosetId + "/";
                Assert.AreEqual<string>(expectedUrl, set.Url);
            }
        }

        [TestMethod]
        public void PhotosetsCreateAddPhotosTest()
        {
            Flickr f = TestData.GetAuthInstance();

            byte[] imageBytes = TestData.TestImageBytes;
            Stream s = new MemoryStream(imageBytes);

            string title = "Test Title";
            string title2 = "New Test Title";
            string desc = "Test Description\nSecond Line";
            string desc2 = "New Test Description";
            string tags = "testtag1,testtag2";

            s.Position = 0;
            // Upload photo once
            string photoId1 = f.UploadPicture(s, "Test.jpg", title, desc, tags, false, false, false, ContentType.Other, SafetyLevel.Safe, HiddenFromSearch.Visible);
            Console.WriteLine("Photo 1 created: " + photoId1);

            s.Position = 0;
            // Upload photo a second time
            string photoId2 = f.UploadPicture(s, "Test.jpg", title, desc, tags, false, false, false, ContentType.Other, SafetyLevel.Safe, HiddenFromSearch.Visible);
            Console.WriteLine("Photo 2 created: " + photoId2);

            // Creat photoset
            Photoset photoset = f.PhotosetsCreate("Test photoset", photoId1);
            Console.WriteLine("Photoset created: " + photoset.PhotosetId);

            try
            {
                // Add second photo to photoset.
                f.PhotosetsAddPhoto(photoset.PhotosetId, photoId2);

                // Remove second photo from photoset
                f.PhotosetsRemovePhoto(photoset.PhotosetId, photoId2);

                f.PhotosetsEditMeta(photoset.PhotosetId, title2, desc2);

                photoset = f.PhotosetsGetInfo(photoset.PhotosetId);

                Assert.AreEqual(title2, photoset.Title, "New Title should be set.");
                Assert.AreEqual(desc2, photoset.Description, "New description should be set");

                f.PhotosetsEditPhotos(photoset.PhotosetId, photoId1, new string[] { photoId2, photoId1 });

                f.PhotosetsRemovePhoto(photoset.PhotosetId, photoId2);
            }
            finally
            {
                // Delete photoset completely
                f.PhotosetsDelete(photoset.PhotosetId);

                // Delete both photos.
                f.PhotosDelete(photoId1);
                f.PhotosDelete(photoId2);
            }
        }

        [TestMethod]
        public void PhotosetsGetInfoEncodingCorrect()
        {
            Flickr f = TestData.GetInstance();

            Photoset pset = f.PhotosetsGetInfo("72157627650627399");

            Assert.AreEqual("Sítio em Arujá - 14/08/2011", pset.Title);
        }
    }
}
