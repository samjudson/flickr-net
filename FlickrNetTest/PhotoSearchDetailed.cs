using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for FlickrPhotoSearchDetailed
    /// </summary>
    [TestClass]
    public class PhotoSearchDetailed
    {
        Flickr f = new Flickr(TestData.ApiKey);

        public PhotoSearchDetailed()
        {
            Flickr.CacheDisabled = true;
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
        public void TestPhotoSearchDetailed()
        {
            PhotoSearchOptions o = new PhotoSearchOptions();
            o.Tags = "applestore";
            o.UserId = "41888973@N00";
            //o.Text = "Apple Store";
            o.PerPage = 10;
            o.Extras = PhotoSearchExtras.All;
            Photos photos = f.PhotosSearch(o);

            Assert.AreEqual(10, photos.PhotosPerPage);
            Assert.AreEqual(1, photos.Page);

            Console.WriteLine(f.LastResponse);

            Assert.AreEqual("3547139066", photos[0].PhotoId);
            Assert.AreEqual("41888973@N00", photos[0].UserId);
            Assert.AreEqual("38560d3e1d", photos[0].Secret);
            Assert.AreEqual("3311", photos[0].Server);
            Assert.AreEqual("Apple Store!", photos[0].Title);
            Assert.AreEqual("4", photos[0].Farm);
            Assert.AreEqual(false, photos[0].IsFamily);
            Assert.AreEqual(true, photos[0].IsPublic);
            Assert.AreEqual(false, photos[0].IsFriend);

            DateTime dateTaken = new DateTime(2009, 5, 19, 22, 21, 46);
            DateTime dateUploaded = new DateTime(2009, 5, 19, 21, 21, 46);
            Assert.IsTrue(photos[0].LastUpdated > dateTaken, "Last updated date was not correct.");
            Assert.AreEqual(dateTaken, photos[0].DateTaken, "Date taken date was not correct.");
            Assert.AreEqual(dateUploaded, photos[0].DateUploaded, "Date uploaded date was not correct.");

            Assert.AreEqual("jpg", photos[0].OriginalFormat, "OriginalFormat should be JPG");
            Assert.AreEqual("WanNUqqcBJTVQXvHIw", photos[0].PlaceId, "PlaceID not set correctly.");

            foreach (Photo photo in photos)
            {
                Assert.IsNotNull(photo.PhotoId);
                Assert.IsTrue(photo.IsPublic);
                Assert.IsFalse(photo.IsFamily);
                Assert.IsFalse(photo.IsFriend);
            }
        }

        [TestMethod]
        public void TestTags()
        {
            PhotoSearchOptions o = new PhotoSearchOptions();
            o.PerPage = 10;
            o.Tags = "Test";
            o.Extras = PhotoSearchExtras.Tags;

            Photos photos = f.PhotosSearch(o);

            Assert.IsTrue(photos.TotalPhotos > 0);
            Assert.IsTrue(photos.Pages > 0);
            Assert.AreEqual(10, photos.PhotosPerPage);
            Assert.AreEqual(1, photos.Page);

            foreach (Photo photo in photos)
            {
                Assert.IsTrue(photo.Tags.Count > 0, "Should be some tags");
                Assert.IsTrue(photo.Tags.Contains("test"), "At least one should be 'test'");
            }
        }

        [TestMethod]
        public void TestPhotoSearchPerPage()
        {
            PhotoSearchOptions o = new PhotoSearchOptions();
            o.Tags = "microsoft";
            o.Licenses.Add(LicenseType.AttributionCC);
            o.Licenses.Add(LicenseType.AttributionNoDerivsCC);
            o.Licenses.Add(LicenseType.AttributionNonCommercialCC);
            o.Licenses.Add(LicenseType.AttributionNonCommercialNoDerivsCC);
            o.Licenses.Add(LicenseType.AttributionNonCommercialShareAlikeCC);
            o.Licenses.Add(LicenseType.AttributionShareAlikeCC);

            o.MinUploadDate = DateTime.Today.AddDays(-2);
            o.MaxUploadDate = DateTime.Today.AddDays(-1);

            o.PerPage = 1;

            Photos photos = f.PhotosSearch(o);

            int totalPhotos1 = photos.TotalPhotos;

            o.PerPage = 10;

            photos = f.PhotosSearch(o);

            int totalPhotos2 = photos.TotalPhotos;

            o.PerPage = 100;

            photos = f.PhotosSearch(o);

            int totalPhotos3 = photos.TotalPhotos;

            Assert.AreEqual(totalPhotos1, totalPhotos2, "Total Photos 1 & 2 should be equal");
            Assert.AreEqual(totalPhotos2, totalPhotos3, "Total Photos 1 & 2 should be equal");
        }


    }
}
