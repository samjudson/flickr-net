using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PeopleGetPhotosOfTests
    /// </summary>
    [TestClass]
    public class PeopleGetPhotosOfTests
    {
        public PeopleGetPhotosOfTests()
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
        public void PeopleGetPhotosOfBasicTest()
        {
            Flickr f = TestData.GetInstance();

            PeoplePhotos p = f.PeopleGetPhotosOf(TestData.TestUserId);

            Assert.IsNotNull(p, "PeoplePhotos should not be null.");
            Assert.AreNotEqual(0, p.Count, "PeoplePhotos.Count should be greater than zero.");
            Assert.IsTrue(p.PerPage >= p.Count, "PerPage should be the same or greater than the number of photos returned.");
        }

        [TestMethod()]
        [ExpectedException(typeof(SignatureRequiredException))]
        public void PeopleGetPhotosOfAuthRequired()
        {
            Flickr f = TestData.GetInstance();

            PeoplePhotos p = f.PeopleGetPhotosOf();
        }

        [TestMethod()]
        public void PeopleGetPhotosOfMe()
        {
            Flickr f = TestData.GetAuthInstance();

            try
            {

                PeoplePhotos p = f.PeopleGetPhotosOf();

                Assert.IsNotNull(p, "PeoplePhotos should not be null.");
                Assert.AreNotEqual(0, p.Count, "PeoplePhotos.Count should be greater than zero.");
                Assert.IsTrue(p.PerPage >= p.Count, "PerPage should be the same or greater than the number of photos returned.");
            }
            finally
            {
                Console.WriteLine(f.LastRequest);
                Console.WriteLine(f.LastResponse);
            }
        }
    }
}
