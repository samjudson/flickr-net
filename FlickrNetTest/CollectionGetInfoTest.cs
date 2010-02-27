using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for CollectionGetInfoTest
    /// </summary>
    [TestClass]
    public class CollectionGetInfoTest
    {
        public CollectionGetInfoTest()
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
        public void TestCollectionGetInfoBasic()
        {
            string id = "78188-72157618817175751";

            Flickr f = TestData.GetAuthInstance();

            CollectionInfo info = f.CollectionsGetInfo(id);

            Assert.AreEqual(id, info.CollectionId, "CollectionId should be correct.");
            Assert.AreEqual(1, info.ChildCount, "ChildCount should be 1.");
            Assert.AreEqual("Global Collection", info.Title, "Title should be 'Global Collection'.");
            Assert.AreEqual("3629", info.Server, "Server should be 3629.");

            Assert.AreEqual(12, info.IconPhotos.Length, "IconPhotos.Length should be 12.");

            Assert.AreEqual("Tires", info.IconPhotos[0].Title, "The first IconPhoto Title should be 'Tires'.");
        }
    }
}
