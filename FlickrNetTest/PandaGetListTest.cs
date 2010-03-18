using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PandaGetListTest
    /// </summary>
    [TestClass]
    public class PandaGetListTest
    {
        public PandaGetListTest()
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
        public void TestPandaGetList()
        {
            Flickr f = TestData.GetInstance();

            string[] pandas = f.PandaGetList();

            Assert.IsNotNull(pandas, "Should return string array");
            Assert.IsTrue(pandas.Length > 0, "Should not return empty array");

            Assert.AreEqual("ling ling", pandas[0]);
            Assert.AreEqual("hsing hsing", pandas[1]);
            Assert.AreEqual("wang wang", pandas[2]);
        }

        [TestMethod]
        public void TestPandaGetPhotos()
        {
            Flickr f = TestData.GetInstance();
            PandaPhotoCollection photos = null;

            try
            {
                photos = f.PandaGetPhotos("ling ling");
            }
            finally
            {
                Console.WriteLine(f.LastRequest);
                Console.WriteLine(f.LastResponse);
            }

            Assert.IsNotNull(photos, "PandaPhotos should not be null.");
            Assert.AreEqual(photos.Count, photos.Total, "PandaPhotos.Count should equal PandaPhotos.Total.");
            Assert.AreEqual("ling ling", photos.PandaName, "PandaPhotos.Panda should be 'ling ling'");
        }
    }
}
