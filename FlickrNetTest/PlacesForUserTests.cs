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
            f.PlacesPlacesForUser(PlaceType.None);
        }

        [TestMethod]
        public void TestGetPlacesBasic()
        {
            Flickr f = TestData.GetAuthInstance();
            Places places = f.PlacesPlacesForUser(PlaceType.Continent);

            Console.WriteLine(f.LastResponse);

            foreach (Place place in places.PlacesCollection)
            {
                Assert.IsNotNull(place.PlaceId, "PlaceId should not be null");
                Assert.IsNotNull(place.Description, "Description should not be null");
                Assert.AreEqual(PlaceType.Continent, place.PlaceType, "PlaceType should be continent");
            }

            foreach (Place place in places.PlacesCollection)
            {
                Console.WriteLine(place.PlaceId + " = " + place.Description);
            }
        }
    }
}
