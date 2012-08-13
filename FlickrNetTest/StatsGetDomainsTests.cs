using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for StatsGetCollectionDomainsTests
    /// </summary>
    [TestClass]
    public class StatsGetDomainsTests
    {
        public StatsGetDomainsTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private string collectionId = "78188-72157600072356354";
        private string photoId = "5890800";
        private string photosetId = "1493109";

        [TestMethod]
        public void StatsGetCollectionDomainsBasicTest()
        {
            Flickr f = TestData.GetAuthInstance();

            var domains = f.StatsGetCollectionDomains(DateTime.Today.AddDays(-2));

            Assert.IsNotNull(domains, "StatDomains should not be null.");
            Assert.AreEqual(domains.Total, domains.Count, "StatDomains.Count should be the same as StatDomains.Total");

            // Overloads
            domains = f.StatsGetCollectionDomains(DateTime.Today.AddDays(-2), collectionId);
            Assert.IsNotNull(domains);

            domains = f.StatsGetCollectionDomains(DateTime.Today.AddDays(-2), 1, 10);
            Assert.IsNotNull(domains);

            domains = f.StatsGetCollectionDomains(DateTime.Today.AddDays(-2), collectionId, 1, 10);
            Assert.IsNotNull(domains);
        }

        [TestMethod]
        public void StatsGetCollectionStatsTest()
        {
            Flickr f = TestData.GetAuthInstance();

            Stats stats = f.StatsGetCollectionStats(DateTime.Today.AddDays(-2), collectionId);

            Assert.IsNotNull(stats, "Stats should not be null.");
        }

        [TestMethod]
        public void StatsGetPhotoDomainsTests()
        {
            Flickr f = TestData.GetAuthInstance();

            var domains = f.StatsGetPhotoDomains(DateTime.Today.AddDays(-2));
            Assert.IsNotNull(domains, "StatDomains should not be null.");
            Assert.AreNotEqual(0, domains.Count, "StatDomains.Count should not be zero.");

            foreach (StatDomain domain in domains)
            {
                Assert.IsNotNull(domain.Name, "StatDomain.Name should not be null.");
                Assert.AreNotEqual(0, domain.Views, "StatDomain.Views should not be zero.");
            }

            // Overloads
            domains = f.StatsGetPhotoDomains(DateTime.Today.AddDays(-2), photoId);
            Assert.IsNotNull(domains, "StatDomains should not be null.");

            domains = f.StatsGetPhotoDomains(DateTime.Today.AddDays(-2), photoId, 1, 10);
            Assert.IsNotNull(domains, "StatDomains should not be null.");

            domains = f.StatsGetPhotoDomains(DateTime.Today.AddDays(-2), 1, 10);
            Assert.IsNotNull(domains, "StatDomains should not be null.");

        }

        [TestMethod]
        public void StatsGetPhotoStatsTest()
        {
            Flickr f = TestData.GetAuthInstance();

            var stats = f.StatsGetPhotoStats(DateTime.Today.AddDays(-5), photoId);

            Assert.IsNotNull(stats);
        }

        [TestMethod]
        public void StatsGetPhotosetDomainsBasic()
        {
            Flickr f = TestData.GetAuthInstance();

            var domains = f.StatsGetPhotosetDomains(DateTime.Today.AddDays(-2));
            Assert.IsNotNull(domains, "StatDomains should not be null.");

            foreach (StatDomain domain in domains)
            {
                Assert.IsNotNull(domain.Name, "StatDomain.Name should not be null.");
                Assert.AreNotEqual(0, domain.Views, "StatDomain.Views should not be zero.");
            }

            // Overloads
            domains = f.StatsGetPhotosetDomains(DateTime.Today.AddDays(-2), 1, 10);
            Assert.IsNotNull(domains, "StatDomains should not be null.");

            domains = f.StatsGetPhotosetDomains(DateTime.Today.AddDays(-2), photosetId);
            Assert.IsNotNull(domains, "StatDomains should not be null.");

            domains = f.StatsGetPhotosetDomains(DateTime.Today.AddDays(-2), photosetId, 1, 10);
            Assert.IsNotNull(domains, "StatDomains should not be null.");


        }

        [TestMethod]
        public void StatsGetPhotosetStatsTest()
        {
            Flickr f = TestData.GetAuthInstance();

            var stats = f.StatsGetPhotosetStats(DateTime.Today.AddDays(-5), photosetId);

            Assert.IsNotNull(stats);
        }

        [TestMethod]
        public void StatsGetPhotostreamDomainsBasic()
        {
            Flickr f = TestData.GetAuthInstance();

            var domains = f.StatsGetPhotostreamDomains(DateTime.Today.AddDays(-2));
            Assert.IsNotNull(domains, "StatDomains should not be null.");

            foreach (StatDomain domain in domains)
            {
                Assert.IsNotNull(domain.Name, "StatDomain.Name should not be null.");
                Assert.AreNotEqual(0, domain.Views, "StatDomain.Views should not be zero.");
            }

            // Overload
            domains = f.StatsGetPhotostreamDomains(DateTime.Today.AddDays(-2), 1, 10);
            Assert.IsNotNull(domains, "StatDomains should not be null.");
        }

        [TestMethod]
        public void StatsGetPhotostreamStatsTest()
        {
            Flickr f = TestData.GetAuthInstance();

            var stats = f.StatsGetPhotostreamStats(DateTime.Today.AddDays(-5));

            Assert.IsNotNull(stats);
        }


    }
}
