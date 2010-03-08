using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for StatsGetTotalViewsTest
    /// </summary>
    [TestClass]
    public class StatsGetTotalViewsTest
    {
        public StatsGetTotalViewsTest()
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
        public void StatsGetTotalViewsBasicTest()
        {
            Flickr f = TestData.GetAuthInstance();

            StatViews views = f.StatsGetTotalViews();

            Assert.IsNotNull(views, "StatViews should not be null.");
            Assert.AreNotEqual(0, views.TotalViews, "TotalViews should be greater than zero.");
            Assert.AreNotEqual(0, views.PhotosetViews, "PhotosetViews should be greater than zero.");
            Assert.AreNotEqual(0, views.PhotostreamViews, "PhotostreamViews should be greater than zero.");
            Assert.AreNotEqual(0, views.PhotoViews, "PhotoViews should be greater than zero.");
            // I have no collection views, so this almost always returns zero, which is correct.
            //Assert.AreNotEqual(0, views.CollectionViews, "CollectionViews should be greater than zero.");
        }
    }
}
