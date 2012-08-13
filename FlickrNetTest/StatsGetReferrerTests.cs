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

        private string collectionId = "78188-72157600072356354";
        private string photoId = "5890800";
        private string photosetId = "1493109";
        private DateTime lastWeek = DateTime.Today.AddDays(-7);


        [TestMethod]
        public void StatsGetPhotoReferrersBasicTest()
        {
            string domain = "flickr.com";

            Flickr f = TestData.GetAuthInstance();

            StatReferrerCollection referrers = f.StatsGetPhotoReferrers(lastWeek, domain, 1, 10);

            Assert.IsNotNull(referrers, "StatReferrers should not be null.");

            Assert.AreNotEqual(0, referrers.Total, "StatReferrers.Total should not be zero.");

            Assert.AreEqual(referrers.Count, Math.Min(referrers.Total, referrers.PerPage), "Count should either be equal to Total or PerPage.");

            Assert.AreEqual(domain, referrers.DomainName, "StatReferrers.Domain should be the same as the searched for domain.");

            foreach (StatReferrer referrer in referrers)
            {
                Assert.IsNotNull(referrer.Url, "StatReferrer.Url should not be null.");
                Assert.AreNotEqual(0, referrer.Views, "StatReferrer.Views should be greater than zero.");
            }

            // Overloads
            referrers = f.StatsGetPhotoReferrers(lastWeek, domain);
            Assert.IsNotNull(referrers);

            referrers = f.StatsGetPhotoReferrers(lastWeek, domain, photoId);
            Assert.IsNotNull(referrers);

            referrers = f.StatsGetPhotoReferrers(lastWeek, domain, photoId, 1, 10);
            Assert.IsNotNull(referrers);

        }

        [TestMethod]
        public void StatsGetPhotosetsReferrersBasicTest()
        {
            string domain = "flickr.com";

            Flickr f = TestData.GetAuthInstance();

            StatReferrerCollection referrers = f.StatsGetPhotosetReferrers(lastWeek, domain, 1, 10);

            Assert.IsNotNull(referrers, "StatReferrers should not be null.");

            // I often get 0 referrers for a particular given date. As this method only works for the previous 28 days I cannot pick a fixed date.
            //Assert.AreNotEqual(0, referrers.Total, "StatReferrers.Total should not be zero.");

            Assert.AreEqual(referrers.Count, Math.Min(referrers.Total, referrers.PerPage), "Count should either be equal to Total or PerPage.");

            if (referrers.Total == 0) return;

            Assert.AreEqual(domain, referrers.DomainName, "StatReferrers.Domain should be the same as the searched for domain.");

            foreach (StatReferrer referrer in referrers)
            {
                Assert.IsNotNull(referrer.Url, "StatReferrer.Url should not be null.");
                Assert.AreNotEqual(0, referrer.Views, "StatReferrer.Views should be greater than zero.");
            }

            // Overloads
            referrers = f.StatsGetPhotosetReferrers(lastWeek, domain);
            Assert.IsNotNull(referrers);

            referrers = f.StatsGetPhotosetReferrers(lastWeek, domain, photosetId);
            Assert.IsNotNull(referrers);

            referrers = f.StatsGetPhotosetReferrers(lastWeek, domain, photosetId, 1, 10);
            Assert.IsNotNull(referrers);

        }

        [TestMethod]
        public void StatsGetPhotostreamReferrersBasicTest()
        {
            string domain = "flickr.com";

            Flickr f = TestData.GetAuthInstance();

            StatReferrerCollection referrers = f.StatsGetPhotostreamReferrers(lastWeek, domain, 1, 10);

            Assert.IsNotNull(referrers, "StatReferrers should not be null.");

            // I often get 0 referrers for a particular given date. As this method only works for the previous 28 days I cannot pick a fixed date.
            //Assert.AreNotEqual(0, referrers.Total, "StatReferrers.Total should not be zero.");

            Assert.AreEqual(referrers.Count, Math.Min(referrers.Total, referrers.PerPage), "Count should either be equal to Total or PerPage.");

            if (referrers.Total == 0) return;

            Assert.AreEqual(domain, referrers.DomainName, "StatReferrers.Domain should be the same as the searched for domain.");

            foreach (StatReferrer referrer in referrers)
            {
                Assert.IsNotNull(referrer.Url, "StatReferrer.Url should not be null.");
                Assert.AreNotEqual(0, referrer.Views, "StatReferrer.Views should be greater than zero.");
            }

            // Overloads
            referrers = f.StatsGetPhotostreamReferrers(lastWeek, domain);
            Assert.IsNotNull(referrers);
        }

        [TestMethod]
        public void StatsGetCollectionReferrersBasicTest()
        {
            string domain = "flickr.com";

            Flickr f = TestData.GetAuthInstance();

            var referrers = f.StatsGetCollectionReferrers(lastWeek, domain, 1, 10);

            Assert.IsNotNull(referrers, "StatReferrers should not be null.");

            Assert.AreEqual(referrers.Count, Math.Min(referrers.Total, referrers.PerPage), "Count should either be equal to Total or PerPage.");

            if (referrers.Total == 0 && referrers.Pages == 0) return;

            Assert.AreEqual(domain, referrers.DomainName, "StatReferrers.Domain should be the same as the searched for domain.");

            foreach (StatReferrer referrer in referrers)
            {
                Assert.IsNotNull(referrer.Url, "StatReferrer.Url should not be null.");
                Assert.AreNotEqual(0, referrer.Views, "StatReferrer.Views should be greater than zero.");
            }
            
            // Overloads
            referrers = f.StatsGetCollectionReferrers(lastWeek, domain);
            Assert.IsNotNull(referrers);

            referrers = f.StatsGetCollectionReferrers(lastWeek, domain, collectionId);
            Assert.IsNotNull(referrers);

            referrers = f.StatsGetCollectionReferrers(lastWeek, domain, collectionId, 1, 10);
            Assert.IsNotNull(referrers);
        }

    }
}
