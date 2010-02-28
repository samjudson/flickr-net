using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PlacesForUserTests
    /// </summary>
    [TestClass]
    public class PlacesForUserTests
    {
        public PlacesForUserTests()
        {
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
        [ExpectedException(typeof(SignatureRequiredException))]
        public void TestGetPlacesAuthenticationRequired()
        {
            Flickr f = TestData.GetInstance();
            f.PlacesPlacesForUser();
        }

        [TestMethod]
        public void TestPlacesForUserContinent()
        {
            Flickr f = TestData.GetAuthInstance();
            Places places = f.PlacesPlacesForUser();

            Console.WriteLine(f.LastResponse);

            foreach (Place place in places)
            {
                Assert.IsNotNull(place.PlaceId, "PlaceId should not be null.");
                Assert.IsNotNull(place.WoeId, "WoeId should not be null.");
                Assert.IsNotNull(place.Description, "Description should not be null.");
                Assert.AreEqual(PlaceType.Continent, place.PlaceType, "PlaceType should be continent.");
            }

            Assert.AreEqual("lkyV7jSbBZTkl7Wkqg", places[0].PlaceId);
            Assert.AreEqual("Europe", places[0].Description);
            Assert.AreEqual("6AQKCGmbBZQfMthgkA", places[1].PlaceId);
            Assert.AreEqual("North America", places[1].Description);
        }

        [TestMethod]
        public void TestPlacesForUserRegions()
        {
            Flickr f = TestData.GetAuthInstance();

            // Test place ID of 'lkyV7jSbBZTkl7Wkqg' is Europe
            Places p = f.PlacesPlacesForUser(PlaceType.Region, null, "lkyV7jSbBZTkl7Wkqg");

            foreach (Place place in p)
            {
                Assert.IsNotNull(place.PlaceId, "PlaceId should not be null.");
                Assert.IsNotNull(place.WoeId, "WoeId should not be null.");
                Assert.IsNotNull(place.Description, "Description should not be null.");
                Assert.IsNotNull(place.PlaceUrl, "PlaceUrl should not be null");
                Assert.AreEqual(PlaceType.Region, place.PlaceType, "PlaceType should be Region.");
            }
        }
    }
}
