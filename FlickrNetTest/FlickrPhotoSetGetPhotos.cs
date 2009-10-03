using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for FlickrPhotoSetGetPhotos
    /// </summary>
    [TestClass]
    public class FlickrPhotoSetGetPhotos
    {
        Flickr f = new Flickr(TestData.ApiKey);

        public FlickrPhotoSetGetPhotos()
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
            Photoset set = f.PhotosetsGetPhotos("72157618515066456", PhotoSearchExtras.All, PrivacyFilter.None, 1, 10);

            Console.WriteLine(f.LastResponse);

            Assert.AreEqual(8, set.NumberOfPhotos, "NumberOfPhotos should be 8.");
            Assert.AreEqual(8, set.PhotoCollection.Length, "Should be 8 photos returned.");

        }
    }
}
