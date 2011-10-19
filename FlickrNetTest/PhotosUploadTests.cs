using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;
using System.IO;
using System.Net;
using System.Linq;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PhotosUploadTests
    /// </summary>
    [TestClass]
    public class PhotosUploadTests
    {
        public PhotosUploadTests()
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
        [Ignore]
        public void UploadPictureAsyncBasicTest()
        {
            Flickr f = TestData.GetAuthInstance();

            var w = new AsyncSubject<FlickrResult<string>>();

            byte[] imageBytes = TestData.TestImageBytes;
            Stream s = new MemoryStream(imageBytes);
            s.Position = 0;

            string title = "Test Title";
            string desc = "Test Description\nSecond Line";
            string tags = "testtag1,testtag2";

            f.UploadPictureAsync(s, "Test.jpg", title, desc, tags, false, false, false, ContentType.Other, SafetyLevel.Safe, HiddenFromSearch.Visible,
                r => { w.OnNext(r); w.OnCompleted(); });

            var result = w.Next().First();

            if (result.HasError)
            {
                throw result.Error;
            }

            Assert.IsNotNull(result.Result);
            Console.WriteLine(result.Result);

            // Clean up photo
            f.PhotosDelete(result.Result);
        }

        [TestMethod]
        public void UploadPictureBasicTest()
        {
            Flickr f = TestData.GetAuthInstance();

            byte[] imageBytes = TestData.TestImageBytes;
            Stream s = new MemoryStream(imageBytes);
            s.Position = 0;

            string title = "Test Title";
            string desc = "Test Description\nSecond Line";
            string tags = "testtag1,testtag2";
            string photoId = f.UploadPicture(s, "Test.jpg", title, desc, tags, false, false, false, ContentType.Other, SafetyLevel.Safe, HiddenFromSearch.Visible);

            try
            {
                PhotoInfo info = f.PhotosGetInfo(photoId);

                Assert.AreEqual(title, info.Title);
                Assert.AreEqual(desc, info.Description);
                Assert.AreEqual(2, info.Tags.Count);
                Assert.AreEqual("testtag1", info.Tags[0].Raw);
                Assert.AreEqual("testtag2", info.Tags[1].Raw);

                Assert.IsFalse(info.IsPublic);
                Assert.IsFalse(info.IsFamily);
                Assert.IsFalse(info.IsFriend);

                SizeCollection sizes = f.PhotosGetSizes(photoId);

                string url = sizes[sizes.Count - 1].Source;
                using (WebClient client = new WebClient())
                {
                    byte[] downloadBytes = client.DownloadData(url);
                    string downloadBase64 = Convert.ToBase64String(downloadBytes);

                    Assert.AreEqual(TestData.TestImageBase64, downloadBase64);
                }
            }
            finally
            {
                f.PhotosDelete(photoId);
            }
        }

        [TestMethod]
        public void UploadPictureFromUrl()
        {
            string url = "http://www.google.co.uk/intl/en_com/images/srpr/logo1w.png";
            Flickr f = TestData.GetAuthInstance();

            using (WebClient client = new WebClient())
            {
                using (Stream s = client.OpenRead(url))
                {
                    string photoId = f.UploadPicture(s, "google.png", "Google Image", "Google", "", false, false, false, ContentType.Photo, SafetyLevel.None, HiddenFromSearch.None);
                    f.PhotosDelete(photoId);
                }
            }
        }

        [Ignore]
        [TestMethod]
        public void UploadPictureVideoTests()
        {
            // Samples downloaded from http://support.apple.com/kb/HT1425
            // sample_mpeg2.m2v does not upload
            string[] filenames = new string[] { "sample_mpeg4.mp4", "sample_sorenson.mov", "sample_iTunes.mov", "sample_iPod.m4v", "sample.3gp", "sample_3GPP2.3g2" };
            // Copy files to this directory.
            string directory = @"Z:\Code Projects\FlickrNet\Samples\";

            foreach (string file in filenames)
            {
                try
                {
                    using (Stream s = new FileStream(Path.Combine(directory, file), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        Flickr f = TestData.GetAuthInstance();
                        string photoId = f.UploadPicture(s, file, "Video Upload Test", file, "video, test", false, false, false, ContentType.Other, SafetyLevel.Safe, HiddenFromSearch.None);
                        f.PhotosDelete(photoId);
                    }
                }
                catch (Exception ex)
                {
                    Assert.Fail("Failed during upload of " + file + " with exception: " + ex.ToString());
                }
            }
        }
    }
}
