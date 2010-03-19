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
        public void PlacesFindBasicTest()
        {
            var places = TestData.GetInstance().PlacesFind("Newcastle");

            Assert.IsNotNull(places);
            Assert.AreNotEqual(0, places.Count);
        }

        [TestMethod]
        public void PlacesFindNewcastleTest()
        {
            var places = TestData.GetInstance().PlacesFind("Newcastle upon Tyne");

            Assert.IsNotNull(places);
            Assert.AreEqual(1, places.Count);

            Console.WriteLine(places[0].Latitude);
            Console.WriteLine(places[0].Longitude);
        }

        [TestMethod]
        public void PlacesFindByLatLongNewcastleTest()
        {
            decimal lat = 54.977M;
            decimal lon = -1.612M;

            var place = TestData.GetInstance().PlacesFindByLatLon(lat, lon);

            Assert.IsNotNull(place);
            Assert.AreEqual("Haymarket, Newcastle upon Tyne, England, GB, United Kingdom", place.Description);
        }

        [TestMethod]
        [ExpectedException(typeof(SignatureRequiredException))]
        public void PlacesPlacesForUserAuthenticationRequiredTest()
        {
            Flickr f = TestData.GetInstance();
            f.PlacesPlacesForUser();
        }

        [TestMethod]
        public void PlacesPlacesForUserHasContinentsTest()
        {
            Flickr f = TestData.GetAuthInstance();
            PlaceCollection places = f.PlacesPlacesForUser();

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
        public void PlacesPlacesForUserContinentHasRegionsTest()
        {
            Flickr f = TestData.GetAuthInstance();

            // Test place ID of 'lkyV7jSbBZTkl7Wkqg' is Europe
            PlaceCollection p = f.PlacesPlacesForUser(PlaceType.Region, null, "lkyV7jSbBZTkl7Wkqg");

            foreach (Place place in p)
            {
                Assert.IsNotNull(place.PlaceId, "PlaceId should not be null.");
                Assert.IsNotNull(place.WoeId, "WoeId should not be null.");
                Assert.IsNotNull(place.Description, "Description should not be null.");
                Assert.IsNotNull(place.PlaceUrl, "PlaceUrl should not be null");
                Assert.AreEqual(PlaceType.Region, place.PlaceType, "PlaceType should be Region.");
            }
        }

        [TestMethod]
        public void PlacesGetInfoBasicTest()
        {
            var f = TestData.GetInstance();
            var placeId = "IEcHLFCaAZwoKQ";
            PlaceInfo p = f.PlacesGetInfo(placeId, null);

            Console.WriteLine(f.LastResponse);
            Assert.IsNotNull(p);
            Assert.AreEqual(placeId, p.PlaceId);
            Assert.AreEqual("30079", p.WoeId);
            Assert.AreEqual(PlaceType.Locality, p.PlaceType);
            Assert.AreEqual("Newcastle upon Tyne, England, United Kingdom", p.Description);

            Assert.IsTrue(p.HasShapeData);
            Assert.IsNotNull(p.ShapeData);
            Assert.AreEqual(0.00015, p.ShapeData.Alpha);
            Assert.AreEqual(1, p.ShapeData.PolyLines.Count);
            Assert.AreEqual(91, p.ShapeData.PolyLines[0].Count);
            Assert.AreEqual(55.018703460693, p.ShapeData.PolyLines[0][90].X);
            Assert.AreEqual(-1.6715459823608, p.ShapeData.PolyLines[0][90].Y);
        }
    }
}
