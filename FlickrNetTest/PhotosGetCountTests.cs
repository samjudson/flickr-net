using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PhotosGetCountTests
    /// </summary>
    [TestClass]
    public class PhotosGetCountTests
    {
        public PhotosGetCountTests()
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
        public void PhotosGetCountTakenTest()
        {
            Flickr f = TestData.GetAuthInstance();

            List<DateTime> dates = new List<DateTime>();
            DateTime date1 = new DateTime(2009, 1, 12);
            DateTime date2 = new DateTime(2009, 9, 12);
            DateTime date3 = new DateTime(2009, 12, 12);

            dates.Add(date2);
            dates.Add(date1);
            dates.Add(date3);

            PhotoCountCollection counts = f.PhotosGetCounts(dates.ToArray(), true);

            Assert.IsNotNull(counts, "PhotoCounts should not be null.");
            Assert.AreEqual(2, counts.Count, "PhotoCounts.Count should be two.");

            Console.WriteLine(f.LastResponse);

            Assert.AreEqual(date1, counts[0].FromDate, "FromDate should be 12th January.");
            Assert.AreEqual(date2, counts[0].ToDate, "ToDate should be 12th July.");
            Assert.AreEqual(date2, counts[1].FromDate, "FromDate should be 12th July.");
            Assert.AreEqual(date3, counts[1].ToDate, "ToDate should be 12th December.");

        }

        [TestMethod]
        public void PhotosGetCountUloadTest()
        {
            Flickr f = TestData.GetAuthInstance();

            List<DateTime> dates = new List<DateTime>();
            DateTime date1 = new DateTime(2009, 7, 12);
            DateTime date2 = new DateTime(2009, 9, 12);
            DateTime date3 = new DateTime(2009, 12, 12);

            dates.Add(date2);
            dates.Add(date1);
            dates.Add(date3);

            PhotoCountCollection counts = f.PhotosGetCounts(dates.ToArray(), false);

            Assert.IsNotNull(counts, "PhotoCounts should not be null.");
            Assert.AreEqual(2, counts.Count, "PhotoCounts.Count should be two.");

            Assert.AreEqual(date1, counts[0].FromDate, "FromDate should be 12th July.");
            Assert.AreEqual(date2, counts[0].ToDate, "ToDate should be 12th September.");
            Assert.AreEqual(date2, counts[1].FromDate, "FromDate should be 12th September.");
            Assert.AreEqual(date3, counts[1].ToDate, "ToDate should be 12th December.");

        }
    }
}
