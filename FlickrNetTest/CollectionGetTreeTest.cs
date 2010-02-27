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
    public class CollectionGetTreeTest
    {
        public CollectionGetTreeTest()
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
        public void CollectionGetTreeRoot()
        {
            Flickr f = TestData.GetAuthInstance();
            Collections tree = f.CollectionsGetTree();

            Assert.IsNotNull(tree, "CollectionList should not be null.");
            Assert.AreNotEqual(0, tree.Count, "CollectionList.Count should not be zero.");

            foreach (Collection coll in tree)
            {
                Assert.IsNotNull(coll.CollectionId, "CollectionId should not be null.");
                Assert.IsNotNull(coll.Title, "Title should not be null.");
                Assert.IsNotNull(coll.Description, "Description should not be null.");
                Assert.IsNotNull(coll.IconSmall, "IconSmall should not be null.");
                Assert.IsNotNull(coll.IconLarge, "IconLarge should not be null.");

                Assert.AreNotEqual(0, coll.Sets.Length + coll.Collections.Length, "Should be either some sets or some collections.");

                foreach (CollectionSet set in coll.Sets)
                {
                }
            }
        }
    }
}
