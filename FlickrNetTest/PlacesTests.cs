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
        public void PlacesGetChildrenWithPhotosPublicPlaceIdTest()
        {
            string placeId = "lkyV7jSbBZTkl7Wkqg"; // Europe;
            Flickr f = TestData.GetInstance();

            var places = f.PlacesGetChildrenWithPhotosPublic(placeId, null);
            Console.WriteLine(f.LastRequest);
            Console.WriteLine(f.LastResponse);

            Assert.IsNotNull(places);
            Assert.AreNotEqual(0, places.Count);

            foreach (var place in places)
            {
                Assert.AreEqual<PlaceType>(PlaceType.Country, place.PlaceType);
            }
        }

        [TestMethod]
        public void PlacesGetChildrenWithPhotosPublicWoeIdTest()
        {
            string woeId = "24865675"; // Europe;

            var places = TestData.GetInstance().PlacesGetChildrenWithPhotosPublic(null, woeId);
            Assert.IsNotNull(places);
            Assert.AreNotEqual(0, places.Count);

            foreach (var place in places)
            {
                Assert.AreEqual<PlaceType>(PlaceType.Region, place.PlaceType);
            }
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
        public void PlacesPlacesForContactsBasicTest()
        {
            var f = TestData.GetAuthInstance();
            var places = f.PlacesPlacesForContacts(PlaceType.Country, null, null, 0, ContactSearch.AllContacts, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);

            Assert.IsNotNull(places);

            Assert.AreNotEqual(0, places.Count);

            foreach (var place in places)
            {
                Assert.AreEqual<PlaceType>(PlaceType.Country, place.PlaceType);
                Assert.IsNotNull(place.PlaceId);
            }
        }

        [TestMethod]
        public void PlacesPlacesForContactsFullParamTest()
        {
            DateTime lastYear = DateTime.Today.AddYears(-1);
            DateTime today = DateTime.Today;

            var f = TestData.GetAuthInstance();
            var places = f.PlacesPlacesForContacts(PlaceType.Country, null, null, 1, ContactSearch.AllContacts, lastYear, today, lastYear, today);

            Console.WriteLine(f.LastRequest);

            Assert.IsNotNull(places);

            Assert.AreNotEqual(0, places.Count);

            foreach (var place in places)
            {
                Assert.AreEqual<PlaceType>(PlaceType.Country, place.PlaceType);
                Assert.IsNotNull(place.PlaceId);
            }
        }

        [TestMethod]
        public void PlacesPlacesForTagsBasicTest()
        {
            var f = TestData.GetAuthInstance();
            var places = f.PlacesPlacesForTags(PlaceType.Country, null, null, 0, new string[] { "newcastle" }, TagMode.AllTags, null, MachineTagMode.None, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);

            Assert.IsNotNull(places);

            Assert.AreNotEqual(0, places.Count);

            foreach (var place in places)
            {
                Assert.AreEqual<PlaceType>(PlaceType.Country, place.PlaceType);
                Assert.IsNotNull(place.PlaceId);
            }
        }

        [TestMethod]
        public void PlacesPlacesForTagsFullParamTest()
        {
            var f = TestData.GetAuthInstance();
            var places = f.PlacesPlacesForTags(PlaceType.Country, null, null, 0, new string[] { "newcastle" }, TagMode.AllTags, new string[] { "dc:author=*" }, MachineTagMode.AllTags, DateTime.Today.AddYears(-10), DateTime.Today, DateTime.Today.AddYears(-10), DateTime.Today);

            Assert.IsNotNull(places);
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

            Assert.AreEqual("IEcHLFCaAZwoKQ", p.Locality.PlaceId);
            Assert.AreEqual("5La1sJqYA5qNBtPTrA", p.County.PlaceId);
            Assert.AreEqual("pn4MsiGbBZlXeplyXg", p.Region.PlaceId);
            Assert.AreEqual("DevLebebApj4RVbtaQ", p.Country.PlaceId);

            Assert.IsTrue(p.HasShapeData);
            Assert.IsNotNull(p.ShapeData);
            Assert.AreEqual(0.00015, p.ShapeData.Alpha);
            Assert.AreEqual(1, p.ShapeData.PolyLines.Count);
            Assert.AreEqual(89, p.ShapeData.PolyLines[0].Count);
            Assert.AreEqual(55.030498504639, p.ShapeData.PolyLines[0][88].X);
            Assert.AreEqual(-1.6404060125351, p.ShapeData.PolyLines[0][88].Y);
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
            Assert.AreEqual(7, col.Count, "Count should be six.");
            Assert.AreEqual(placeId, col.PlaceId);

            Assert.AreEqual(1, col[1].PolyLines.Count, "The second shape should have one polylines.");
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

        [TestMethod]
        public void PlacesGetPlaceTypes()
        {
            var pts = TestData.GetInstance().PlacesGetPlaceTypes();
            Assert.IsNotNull(pts);
            Assert.IsTrue(pts.Count > 1, "Count should be greater than one. Count = " + pts.Count + ".");

            foreach (var kp in pts)
            {
                Assert.AreNotEqual(0, kp.Id, "Key should not be zero.");
                Assert.IsNotNull(kp.Name, "Value should not be null.");

                Assert.IsTrue(Enum.IsDefined(typeof(PlaceType), kp.Id), "PlaceType with ID " + kp.Id + " and Value '" + kp.Name + "' not defined in PlaceType enum.");
                PlaceType type = (PlaceType)kp.Id;
                Assert.AreEqual(type.ToString("G").ToLower(), kp.Name, "Name of enum should match.");
            }
        }

        [TestMethod]
        public void PlacesPlacesForBoundingBoxUsaTest()
        {
            Flickr f = TestData.GetInstance();

            var places = f.PlacesPlacesForBoundingBox(PlaceType.County, null, null, BoundaryBox.UKNewcastle);

            Assert.IsNotNull(places);
            Assert.AreNotEqual(0, places.Count);

            foreach (var place in places)
            {
                Assert.IsNotNull(place.PlaceId);
                Assert.AreEqual<PlaceType>(PlaceType.County, place.PlaceType);
            }
        }

        [TestMethod]
        public void BoundaryBoxCalculateSizesUKNewcastle()
        {
            BoundaryBox b = BoundaryBox.UKNewcastle;

            double e = b.DiagonalDistanceInMiles();
        }

        [TestMethod]
        public void BoundaryBoxCalculateSizesFrankfurtToBerlin()
        {
            BoundaryBox b = new BoundaryBox(8.68194, 50.11222, 13.29750, 52.52222);

            double e = b.DiagonalDistanceInMiles();
            Assert.IsTrue(259.9 < e && e < 260.0);
        }

    }
}
