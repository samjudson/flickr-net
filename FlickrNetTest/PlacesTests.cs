using System;

using NUnit.Framework;
using FlickrNet;
using Shouldly;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PlacesForUserTests
    /// </summary>
    [TestFixture]
    public class PlacesTests : BaseTest
    {
        [Test]
        public void PlacesFindBasicTest()
        {
            var places = Instance.PlacesFind("Newcastle");

            Assert.IsNotNull(places);
            Assert.AreNotEqual(0, places.Count);
        }

        [Test]
        public void PlacesFindNewcastleTest()
        {
            var places = Instance.PlacesFind("Newcastle upon Tyne");

            Assert.IsNotNull(places);
            Assert.AreEqual(1, places.Count);
        }

        [Test]
        public void PlacesFindByLatLongNewcastleTest()
        {
            double lat = 54.977;
            double lon = -1.612;

            var place = Instance.PlacesFindByLatLon(lat, lon);

            Assert.IsNotNull(place);
            Assert.AreEqual("Haymarket, Newcastle upon Tyne, England, GB, United Kingdom", place.Description);
        }

        [Test]
        public void PlacesPlacesForUserAuthenticationRequiredTest()
        {
            Flickr f = Instance;
            Should.Throw<SignatureRequiredException>(() => f.PlacesPlacesForUser());
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PlacesPlacesForUserHasContinentsTest()
        {
            Flickr f = AuthInstance;
            PlaceCollection places = f.PlacesPlacesForUser();

            foreach (Place place in places)
            {
                Assert.IsNotNull(place.PlaceId, "PlaceId should not be null.");
                Assert.IsNotNull(place.WoeId, "WoeId should not be null.");
                Assert.IsNotNull(place.Description, "Description should not be null.");
                Assert.AreEqual(PlaceType.Continent, place.PlaceType, "PlaceType should be continent.");
            }

            Assert.AreEqual("6dCBhRRTVrJiB5xOrg", places[0].PlaceId);
            Assert.AreEqual("Europe", places[0].Description);
            Assert.AreEqual("l5geY0lTVrLoNkLgeQ", places[1].PlaceId);
            Assert.AreEqual("North America", places[1].Description);
        }

        [Test, Ignore("Not currently returning any records for some reason.")]
        public void PlacesGetChildrenWithPhotosPublicPlaceIdTest()
        {
            string placeId = "6dCBhRRTVrJiB5xOrg"; // Europe
            Flickr f = Instance;

            var places = f.PlacesGetChildrenWithPhotosPublic(placeId, null);
            Console.WriteLine(f.LastRequest);
            Console.WriteLine(f.LastResponse);

            Assert.IsNotNull(places);
            Assert.AreNotEqual(0, places.Count);

            foreach (var place in places)
            {
                Assert.AreEqual(PlaceType.Country, place.PlaceType);
            }
        }

        [Test, Ignore("Not currently returning any records for some reason.")]
        public void PlacesGetChildrenWithPhotosPublicWoeIdTest()
        {
            string woeId = "24865675"; // Europe

            var places = Instance.PlacesGetChildrenWithPhotosPublic(null, woeId);
            Assert.IsNotNull(places);
            Assert.AreNotEqual(0, places.Count);

            foreach (var place in places)
            {
                Assert.AreEqual(PlaceType.Country, place.PlaceType);
            }
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PlacesPlacesForUserContinentHasRegionsTest()
        {
            Flickr f = AuthInstance;

            // Test place ID of '6dCBhRRTVrJiB5xOrg' is Europe
            PlaceCollection p = f.PlacesPlacesForUser(PlaceType.Region, null, "6dCBhRRTVrJiB5xOrg");

            foreach (Place place in p)
            {
                Assert.IsNotNull(place.PlaceId, "PlaceId should not be null.");
                Assert.IsNotNull(place.WoeId, "WoeId should not be null.");
                Assert.IsNotNull(place.Description, "Description should not be null.");
                Assert.IsNotNull(place.PlaceUrl, "PlaceUrl should not be null");
                Assert.AreEqual(PlaceType.Region, place.PlaceType, "PlaceType should be Region.");
            }
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PlacesPlacesForContactsBasicTest()
        {
            var f = AuthInstance;
            var places = f.PlacesPlacesForContacts(PlaceType.Country, null, null, 0, ContactSearch.AllContacts, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);

            Assert.IsNotNull(places);

            Assert.AreNotEqual(0, places.Count);

            foreach (var place in places)
            {
                Assert.AreEqual(PlaceType.Country, place.PlaceType);
                Assert.IsNotNull(place.PlaceId);
            }
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PlacesPlacesForContactsFullParamTest()
        {
            DateTime lastYear = DateTime.Today.AddYears(-1);
            DateTime today = DateTime.Today;

            var f = AuthInstance;
            var places = f.PlacesPlacesForContacts(PlaceType.Country, null, null, 1, ContactSearch.AllContacts, lastYear, today, lastYear, today);

            Console.WriteLine(f.LastRequest);

            Assert.IsNotNull(places);

            Assert.AreNotEqual(0, places.Count);

            foreach (var place in places)
            {
                Assert.AreEqual(PlaceType.Country, place.PlaceType);
                Assert.IsNotNull(place.PlaceId);
            }
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PlacesPlacesForTagsBasicTest()
        {
            var f = AuthInstance;
            var places = f.PlacesPlacesForTags(PlaceType.Country, null, null, 0, new string[] {"newcastle"},
                                               TagMode.AllTags, null, MachineTagMode.None, DateTime.MinValue,
                                               DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);

            Assert.IsNotNull(places);

            Assert.AreNotEqual(0, places.Count);

            foreach (var place in places)
            {
                Assert.AreEqual(PlaceType.Country, place.PlaceType);
                Assert.IsNotNull(place.PlaceId);
            }
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PlacesPlacesForTagsFullParamTest()
        {
            var f = AuthInstance;
            var places = f.PlacesPlacesForTags(PlaceType.Country, null, null, 0, new string[] {"newcastle"},
                                               TagMode.AllTags, new string[] {"dc:author=*"}, MachineTagMode.AllTags,
                                               DateTime.Today.AddYears(-10), DateTime.Today,
                                               DateTime.Today.AddYears(-10), DateTime.Today);

            Assert.IsNotNull(places);
        }

        [Test]
        public void PlacesGetInfoBasicTest()
        {
            var f = Instance;
            var placeId = "X9sTR3BSUrqorQ";
            PlaceInfo p = f.PlacesGetInfo(placeId, null);

            Console.WriteLine(f.LastResponse);

            Assert.IsNotNull(p);
            Assert.AreEqual(placeId, p.PlaceId);
            Assert.AreEqual("30079", p.WoeId);
            Assert.AreEqual(PlaceType.Locality, p.PlaceType);
            Assert.AreEqual("Newcastle upon Tyne, England, United Kingdom", p.Description);

            Assert.AreEqual("X9sTR3BSUrqorQ", p.Locality.PlaceId);
            Assert.AreEqual("myqh27pQULzLWcg7Kg", p.County.PlaceId);
            Assert.AreEqual("2eIY2QFTVr_DwWZNLg", p.Region.PlaceId);
            Assert.AreEqual("cnffEpdTUb5v258BBA", p.Country.PlaceId);

            Assert.IsTrue(p.HasShapeData);
            Assert.IsNotNull(p.ShapeData);
            Assert.AreEqual(0.00015, p.ShapeData.Alpha);
            Assert.AreEqual(1, p.ShapeData.PolyLines.Count);
            Assert.AreEqual(89, p.ShapeData.PolyLines[0].Count);
            Assert.AreEqual(55.030498504639, p.ShapeData.PolyLines[0][88].X);
            Assert.AreEqual(-1.6404060125351, p.ShapeData.PolyLines[0][88].Y);
        }

        [Test]
        public void PlacesGetInfoByUrlBasicTest()
        {
            var f = Instance;
            var placeId = "X9sTR3BSUrqorQ";
            PlaceInfo p1 = f.PlacesGetInfo(placeId, null);
            PlaceInfo p2 = f.PlacesGetInfoByUrl(p1.PlaceUrl);

            Assert.IsNotNull(p2);
            Assert.AreEqual(p1.PlaceId, p2.PlaceId);
            Assert.AreEqual(p1.WoeId, p2.WoeId);
            Assert.AreEqual(p1.PlaceType, p2.PlaceType);
            Assert.AreEqual(p1.Description, p2.Description);

            Assert.IsNotNull(p2.PlaceFlickrUrl);
        }

        [Test]
        public void PlacesGetTopPlacesListTest()
        {
            var f = Instance;
            var places = f.PlacesGetTopPlacesList(PlaceType.Continent);

            Assert.IsNotNull(places);
            Assert.AreNotEqual(0, places.Count);

            foreach (var p in places)
            {
                Assert.AreEqual(PlaceType.Continent, p.PlaceType);
                Assert.IsNotNull(p.PlaceId);
                Assert.IsNotNull(p.WoeId);
            }
        }

        [Test]
        public void PlacesGetShapeHistoryTest()
        {
            var placeId = "X9sTR3BSUrqorQ";
            var f = Instance;
            var col = f.PlacesGetShapeHistory(placeId, null);

            Assert.IsNotNull(col, "ShapeDataCollection should not be null.");
            Assert.AreEqual(7, col.Count, "Count should be six.");
            Assert.AreEqual(placeId, col.PlaceId);

            Assert.AreEqual(1, col[1].PolyLines.Count, "The second shape should have one polylines.");
        }

        [Test]
        public void PlacesGetTagsForPlace()
        {
            var placeId = "X9sTR3BSUrqorQ";
            var f = Instance;
            var col = f.PlacesTagsForPlace(placeId, null);

            Assert.IsNotNull(col, "TagCollection should not be null.");
            Assert.AreEqual(100, col.Count, "Count should be one hundred.");

            foreach (var t in col)
            {
                Assert.AreNotEqual(0, t.Count, "Count should be greater than zero.");
                Assert.IsNotNull(t.TagName, "TagName should not be null.");
            }

        }

        [Test]
        public void PlacesGetPlaceTypes()
        {
            var pts = Instance.PlacesGetPlaceTypes();
            Assert.IsNotNull(pts);
            Assert.IsTrue(pts.Count > 1, "Count should be greater than one. Count = " + pts.Count + ".");

            foreach (var kp in pts)
            {
                Assert.AreNotEqual(0, kp.Id, "Key should not be zero.");
                Assert.IsNotNull(kp.Name, "Value should not be null.");

                Assert.IsTrue(Enum.IsDefined(typeof(PlaceType), kp.Id), "PlaceType with ID " + kp.Id + " and Value '" + kp.Name + "' not defined in PlaceType enum.");
                var type = (PlaceType)kp.Id;
                Assert.AreEqual(type.ToString("G").ToLower(), kp.Name, "Name of enum should match.");
            }
        }

        [Test]
        public void PlacesPlacesForBoundingBoxUsaTest()
        {
            Flickr f = Instance;

            var places = f.PlacesPlacesForBoundingBox(PlaceType.County, null, null, BoundaryBox.UKNewcastle);

            Assert.IsNotNull(places);
            Assert.AreNotEqual(0, places.Count);

            foreach (var place in places)
            {
                Assert.IsNotNull(place.PlaceId);
                Assert.AreEqual(PlaceType.County, place.PlaceType);
            }
        }

    }
}
