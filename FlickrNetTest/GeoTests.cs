using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for GeoTests
    /// </summary>
    [TestClass]
    public class GeoTests
    {
        public GeoTests()
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
        public void PhotosGeoGetPermsBasicTest()
        {
            GeoPermissions perms = TestData.GetAuthInstance().PhotosGeoGetPerms(TestData.PhotoId);

            Assert.IsNotNull(perms);
            Assert.AreEqual(TestData.PhotoId, perms.PhotoId);
            Assert.IsTrue(perms.IsPublic, "IsPublic should be true.");
        }

        [TestMethod]
        public void PhotosGetWithGeoDataBasicTest()
        {
            PhotoCollection photos = TestData.GetAuthInstance().PhotosGetWithGeoData();

            Assert.IsNotNull(photos);
            Assert.AreNotEqual(0, photos.Count);
            Assert.AreNotEqual(0, photos.Total);
            Assert.AreEqual(1, photos.Page);
            Assert.AreNotEqual(0, photos.PerPage);
            Assert.AreNotEqual(0, photos.Pages);

            foreach (var p in photos)
            {
                Assert.IsNotNull(p.PhotoId);
            }

        }
    }
}
