using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for FlickrPhotosGetContactsPhotos
    /// </summary>
    [TestClass]
    public class PhotosGetContactsPhotos
    {
        Flickr f = TestData.GetAuthInstance();

        public PhotosGetContactsPhotos()
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
        [ExpectedException(typeof(SignatureRequiredException))]
        public void PhotosGetContactsPhotosSignatureRequiredTest()
        {
            Flickr f = TestData.GetInstance();
            f.PhotosGetContactsPhotos();
        }

        [TestMethod]
        public void PhotosGetContactsPhotosBasicTest()
        {
            PhotoCollection photos = f.PhotosGetContactsPhotos(10);

            Console.WriteLine(f.LastResponse);

            Assert.IsTrue(photos.Count > 0, "Should return some photos");
            Assert.AreEqual(10, photos.Count, "Should return 10 photos");

        }

        [TestMethod]
        public void PhotosGetContactsPhotosExtrasTest()
        {
            PhotoCollection photos = f.PhotosGetContactsPhotos(10, false, false, false, PhotoSearchExtras.All);

            Console.WriteLine(f.LastResponse);

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
