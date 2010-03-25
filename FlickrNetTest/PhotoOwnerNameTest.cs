using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PhotoOwnerNameTest
    /// </summary>
    [TestClass]
    public class PhotoOwnerNameTest
    {
        public PhotoOwnerNameTest()
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
        public void PhotosSearchOwnerNameTest()
        {
            PhotoSearchOptions o = new PhotoSearchOptions();

            o.UserId = TestData.TestUserId;
            o.PerPage = 10;
            o.Extras = PhotoSearchExtras.OwnerName;

            Flickr f = TestData.GetInstance();
            PhotoCollection photos = f.PhotosSearch(o);

            Assert.IsNotNull(photos[0].OwnerName);
           
        }

        [TestMethod]
        public void PhotosGetContactsPublicPhotosOwnerNameTest()
        {
            Flickr f = TestData.GetInstance();
            PhotoCollection photos = f.PhotosGetContactsPublicPhotos(TestData.TestUserId, PhotoSearchExtras.OwnerName);

            Assert.IsNotNull(photos[0].OwnerName);
        }

    }
}
