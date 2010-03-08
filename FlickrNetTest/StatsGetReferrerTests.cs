using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for StatsGetReferrerTests
    /// </summary>
    [TestClass]
    public class StatsGetReferrerTests
    {
        public StatsGetReferrerTests()
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
        public void StatsGetPhotoReferrersBasicTest()
        {
            string domain = "flickr.com";

            Flickr f = TestData.GetAuthInstance();

            StatReferrers referrers = f.StatsGetPhotoReferrers(DateTime.Today.AddDays(-1), domain, null, 0, 0);

            Assert.IsNotNull(referrers, "StatReferrers should not be null.");

            Assert.AreNotEqual(0, referrers.Total, "StatReferrers.Total should not be zero.");

            Assert.AreEqual(referrers.Count, Math.Min(referrers.Total, referrers.PerPage), "Count should either be equal to Total or PerPage.");

            Assert.AreEqual(domain, referrers.DomainName, "StatReferrers.Domain should be the same as the searched for domain.");

            foreach (StatReferrer referrer in referrers)
            {
                Assert.IsNotNull(referrer.Url, "StatReferrer.Url should not be null.");
                Assert.AreNotEqual(0, referrer.Views, "StatReferrer.Views should be greater than zero.");
            }
        }

        [TestMethod]
        public void StatsGetPhotosetsReferrersBasicTest()
        {
            string domain = "flickr.com";

            Flickr f = TestData.GetAuthInstance();

            StatReferrers referrers = f.StatsGetPhotosetReferrers(DateTime.Today.AddDays(-1), domain, null, 0, 0);

            Assert.IsNotNull(referrers, "StatReferrers should not be null.");

            Assert.AreNotEqual(0, referrers.Total, "StatReferrers.Total should not be zero.");

            Assert.AreEqual(referrers.Count, Math.Min(referrers.Total, referrers.PerPage), "Count should either be equal to Total or PerPage.");

            Assert.AreEqual(domain, referrers.DomainName, "StatReferrers.Domain should be the same as the searched for domain.");

            foreach (StatReferrer referrer in referrers)
            {
                Assert.IsNotNull(referrer.Url, "StatReferrer.Url should not be null.");
                Assert.AreNotEqual(0, referrer.Views, "StatReferrer.Views should be greater than zero.");
            }
        }

        [TestMethod]
        public void StatsGetPhotostreamReferrersBasicTest()
        {
            string domain = "flickr.com";

            Flickr f = TestData.GetAuthInstance();

            StatReferrers referrers = f.StatsGetPhotostreamReferrers(DateTime.Today.AddDays(-1), domain, 0, 0);

            Assert.IsNotNull(referrers, "StatReferrers should not be null.");

            Assert.AreNotEqual(0, referrers.Total, "StatReferrers.Total should not be zero.");

            Assert.AreEqual(referrers.Count, Math.Min(referrers.Total, referrers.PerPage), "Count should either be equal to Total or PerPage.");

            Assert.AreEqual(domain, referrers.DomainName, "StatReferrers.Domain should be the same as the searched for domain.");

            foreach (StatReferrer referrer in referrers)
            {
                Assert.IsNotNull(referrer.Url, "StatReferrer.Url should not be null.");
                Assert.AreNotEqual(0, referrer.Views, "StatReferrer.Views should be greater than zero.");
            }
        }

        [TestMethod]
        public void StatsGetCollectionReferrersBasicTest()
        {
            string domain = "flickr.com";

            Flickr f = TestData.GetAuthInstance();

            StatReferrers referrers = f.StatsGetCollectionReferrers(DateTime.Today.AddDays(-1), domain, null, 0, 0);

            Assert.IsNotNull(referrers, "StatReferrers should not be null.");

            Assert.AreEqual(referrers.Count, Math.Min(referrers.Total, referrers.PerPage), "Count should either be equal to Total or PerPage.");

            if (referrers.Total == 0 && referrers.Pages == 0) return;

            Assert.AreEqual(domain, referrers.DomainName, "StatReferrers.Domain should be the same as the searched for domain.");

            foreach (StatReferrer referrer in referrers)
            {
                Assert.IsNotNull(referrer.Url, "StatReferrer.Url should not be null.");
                Assert.AreNotEqual(0, referrer.Views, "StatReferrer.Views should be greater than zero.");
            }
        }

    }
}
