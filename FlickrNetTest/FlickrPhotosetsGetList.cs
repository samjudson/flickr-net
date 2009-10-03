using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for FlickrPhotosetsGetList
    /// </summary>
    [TestClass]
    public class FlickrPhotosetsGetList
    {
        Flickr f = new Flickr(TestData.ApiKey);

        public FlickrPhotosetsGetList()
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
            Photosets photosets = f.PhotosetsGetList(TestData.TestUserId);

            Assert.IsTrue(photosets.PhotosetCollection.Length > 0, "Should be at least one photoset");

            foreach (Photoset set in photosets.PhotosetCollection)
            {
                Assert.IsTrue(set.NumberOfPhotos > 0, "NumberOfPhotos should be greater than zero");
                Assert.IsNotNull(set.Title, "Title should not be null");
                Assert.IsNotNull(set.Description, "Description should not be null");

                Assert.AreEqual(0, set.Count);
                Assert.AreEqual(0, set.PhotoCollection.Length);
            }
        }
    }
}
