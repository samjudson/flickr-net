using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PhotosSearchInterestingTests
    /// </summary>
    [TestClass]
    public class PhotosSearchInterestingTests
    {
        public PhotosSearchInterestingTests()
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
        public void TestSearchInterestingnessBasic()
        {
            Flickr f = TestData.GetInstance();
            PhotoSearchOptions o = new PhotoSearchOptions();
            o.SortOrder = PhotoSearchSortOrder.InterestingnessDesc;
            o.Tags = "colorful";
            o.PerPage = 500;

            Photos ps = f.PhotosSearch(o);

            Assert.IsNotNull(ps, "Photos should not be null");
            Assert.AreEqual(500, ps.PerPage, "PhotosPerPage should be 500");
            Assert.AreEqual(500, ps.Count, "Count should be 500 as well");
        }
    }
}
