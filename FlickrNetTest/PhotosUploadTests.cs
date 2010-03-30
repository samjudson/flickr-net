using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;
using System.IO;
using System.Net;

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

                Uri url = sizes[sizes.Count - 1].Source;
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
    }
}
