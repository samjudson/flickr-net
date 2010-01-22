using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for GroupPoolGetPhotosTests
    /// </summary>
    [TestClass]
    public class GroupPoolGetPhotosTests
    {
        public GroupPoolGetPhotosTests()
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
        public void TestGroupPoolDateAdded()
        {
            Flickr f = TestData.GetInstance();

            Photos photos = f.GroupPoolGetPhotos(TestData.GroupId);

            Assert.IsNotNull(photos, "Photos should not be null");
            Assert.IsTrue(photos.Count > 0, "Should be more than 0 photos returned");

            foreach (Photo p in photos)
            {
                Assert.AreNotEqual(default(DateTime), p.DateAdded, "DateAdded should not be default value");
                Assert.IsTrue(p.DateAdded < DateTime.Now, "DateAdded should be in the past");
            }

        }
    }
}
