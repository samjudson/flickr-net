using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;
using System.IO;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for CacheTests
    /// </summary>
    [TestClass]
    public class CacheTests
    {
        public CacheTests()
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
        public void CacheLocationTest()
        {
            string origLocation = Flickr.CacheLocation;

            Console.WriteLine(origLocation);

            string newLocation = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            Flickr.CacheLocation = newLocation;

            Assert.AreEqual(Flickr.CacheLocation, newLocation);

            Flickr.CacheLocation = origLocation;

            Assert.AreEqual(Flickr.CacheLocation, origLocation);

        }

        [TestMethod]
        public void CacheHitTest()
        {
            Directory.Delete(Flickr.CacheLocation, true);

            Flickr f = TestData.GetInstance();
            Flickr.FlushCache();
            f.InstanceCacheDisabled = false;

            f.PeopleGetPublicPhotos(TestData.TestUserId);

            string lastUrl = f.LastRequest;

            ICacheItem item = Cache.Responses.Get(lastUrl, TimeSpan.MaxValue, false);

            Assert.IsNotNull(item, "Cache should now contain the item.");
            Assert.IsInstanceOfType(item, typeof(ResponseCacheItem));

            ResponseCacheItem response = item as ResponseCacheItem;

            Assert.IsNotNull(response.Url, "Url should not be null.");
            Assert.AreEqual(lastUrl, response.Url.AbsoluteUri, "Url should match the url requested from the cache.");
        }
    }
}
