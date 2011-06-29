using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for CollectionGetTreeTest
    /// </summary>
    [TestClass]
    public class CollectionTests
    {
        public CollectionTests()
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
        public void CollectionGetInfoBasicTest()
        {
            string id = "78188-72157618817175751";

            Flickr f = TestData.GetAuthInstance();

            CollectionInfo info = f.CollectionsGetInfo(id);

            Assert.AreEqual(id, info.CollectionId, "CollectionId should be correct.");
            Assert.AreEqual(1, info.ChildCount, "ChildCount should be 1.");
            Assert.AreEqual("Global Collection", info.Title, "Title should be 'Global Collection'.");
            Assert.AreEqual("3629", info.Server, "Server should be 3629.");

            Assert.AreEqual(12, info.IconPhotos.Count, "IconPhotos.Length should be 12.");

            Assert.AreEqual("Tires", info.IconPhotos[0].Title, "The first IconPhoto Title should be 'Tires'.");
        }

        [TestMethod]
        public void CollectionGetTreeRootTest()
        {
            Flickr f = TestData.GetAuthInstance();
            CollectionCollection tree = f.CollectionsGetTree();

            Assert.IsNotNull(tree, "CollectionList should not be null.");
            Assert.AreNotEqual(0, tree.Count, "CollectionList.Count should not be zero.");
            //Assert.IsTrue(tree.Count > 1, "CollectionList.Count should be greater than 1.");

            foreach (Collection coll in tree)
            {
                Assert.IsNotNull(coll.CollectionId, "CollectionId should not be null.");
                Assert.IsNotNull(coll.Title, "Title should not be null.");
                Assert.IsNotNull(coll.Description, "Description should not be null.");
                Assert.IsNotNull(coll.IconSmall, "IconSmall should not be null.");
                Assert.IsNotNull(coll.IconLarge, "IconLarge should not be null.");

                Assert.AreNotEqual(0, coll.Sets.Count + coll.Collections.Count, "Should be either some sets or some collections.");

                foreach (CollectionSet set in coll.Sets)
                {
                    Assert.IsNotNull(set.SetId, "SetId should not be null.");
                }
            }
        }

        [TestMethod]
        public void CollectionCreateBasicTest()
        {
            Flickr f = TestData.GetAuthInstance();

            //Collection c1 = f.CollectionsCreate("Test TItle", "Test Description", null);

            //f.CollectionsEditMeta(c1.CollectionId, "Real Test Title", "Real Test Description");

            //Collection c2 = f.CollectionsCreate("Test 2", "T2", c1.CollectionId);
            //Collection c3 = f.CollectionsCreate("Test 3", "T3", c1.CollectionId);
            //Collection c4 = f.CollectionsCreate("Test 4", "T4", c1.CollectionId);
            //Collection c5 = f.CollectionsCreate("Test 5", "T5", c1.CollectionId);

            //f.CollectionsSortCollections(c1.CollectionId, new string[] { c5.CollectionId, c4.CollectionId, c3.CollectionId, c2.CollectionId });

            //f.CollectionsDelete(c1.CollectionId, true);

        }
    }
}
