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
    public class PlacesTests
    {
        public PlacesTests()
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
            double lat = 54.977;
            double lon = -1.612;

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

        [TestMethod]
        public void PlacesGetInfoByUrlBasicTest()
        {
            var f = TestData.GetInstance();
            var placeId = "IEcHLFCaAZwoKQ";
            PlaceInfo p1 = f.PlacesGetInfo(placeId, null);
            PlaceInfo p2 = f.PlacesGetInfoByUrl(p1.PlaceUrl);

            Assert.IsNotNull(p2);
            Assert.AreEqual(p1.PlaceId, p2.PlaceId);
            Assert.AreEqual(p1.WoeId, p2.WoeId);
            Assert.AreEqual(p1.PlaceType, p2.PlaceType);
            Assert.AreEqual(p1.Description, p2.Description);

            Assert.IsNotNull(p2.PlaceFlickrUrl);
        }

        [TestMethod]
        public void PlacesGetTopPlacesListTest()
        {
            var f = TestData.GetInstance();
            var places = f.PlacesGetTopPlacesList(PlaceType.Continent);

            Assert.IsNotNull(places);
            Assert.AreNotEqual(0, places.Count);

            foreach (var p in places)
            {
                Assert.AreEqual<PlaceType>(PlaceType.Continent, p.PlaceType);
                Assert.IsNotNull(p.PlaceId);
                Assert.IsNotNull(p.WoeId);
            }
        }

        [TestMethod]
        public void PlacesGetShapeHistoryTest()
        {
            var placeId = "IEcHLFCaAZwoKQ";
            var f = TestData.GetInstance();
            var col = f.PlacesGetShapeHistory(placeId, null);

            Assert.IsNotNull(col, "ShapeDataCollection should not be null.");
            Assert.AreEqual(6, col.Count, "Count should be six.");
            Assert.AreEqual(placeId, col.PlaceId);

            Assert.AreEqual(2, col[1].PolyLines.Count, "The second shape should have two polylines.");
        }

        [TestMethod]
        public void PlacesGetTagsForPlace()
        {
            var placeId = "IEcHLFCaAZwoKQ";
            var f = TestData.GetInstance();
            var col = f.PlacesTagsForPlace(placeId, null);

            Assert.IsNotNull(col, "TagCollection should not be null.");
            Assert.AreEqual(100, col.Count, "Count should be one hundred.");

            foreach (var t in col)
            {
                Assert.AreNotEqual(0, t.Count, "Count should be greater than zero.");
                Assert.IsNotNull(t.TagName, "TagName should not be null.");
            }

        }
    }
}
