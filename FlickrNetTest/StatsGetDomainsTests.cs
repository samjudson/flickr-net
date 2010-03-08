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
        public void StatsGetCollectionDomainsBasicTest()
        {
            Flickr f = TestData.GetAuthInstance();

            StatDomains domains = f.StatsGetCollectionDomains(DateTime.Today.AddDays(-2));

            Assert.IsNotNull(domains, "StatDomains should not be null.");
            Assert.AreEqual(domains.Total, domains.Count, "StatDomains.Count should be the same as StatDomains.Total");
        }

        [TestMethod]
        public void StatsGetPhotoDomainsBasic()
        {
            Flickr f = TestData.GetAuthInstance();

            StatDomains domains = f.StatsGetPhotoDomains(DateTime.Today.AddDays(-2));

            Assert.IsNotNull(domains, "StatDomains should not be null.");
            Assert.AreNotEqual(0, domains.Count, "StatDomains.Count should not be zero.");

            foreach (StatDomain domain in domains)
            {
                Assert.IsNotNull(domain.Name, "StatDomain.Name should not be null.");
                Assert.AreNotEqual(0, domain.Views, "StatDomain.Views should not be zero.");
            }
        }

    }
}
