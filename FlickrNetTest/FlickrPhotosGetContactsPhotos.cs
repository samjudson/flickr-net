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
    public class FlickrPhotosGetContactsPhotos
    {
        Flickr f = new Flickr(TestData.ApiKey, TestData.SharedSecret, TestData.AuthToken);

        public FlickrPhotosGetContactsPhotos()
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
        public void TestMethod1()
        {
            Photos photos = f.PhotosGetContactsPhotos(10);

            Console.WriteLine(f.LastResponse);

            Assert.IsTrue(photos.Count > 0, "Should return some photos");
            Assert.AreEqual(10, photos.Count, "Should return 10 photos");

        }

        [TestMethod]
        public void TestMethodExtras()
        {
            Photos photos = f.PhotosGetContactsPhotos(10, false, false, false, PhotoSearchExtras.All);

            Console.WriteLine(f.LastResponse);

            Assert.IsTrue(photos.Count > 0, "Should return some photos");
            Assert.AreEqual(10, photos.Count, "Should return 10 photos");
        }
    }
}
