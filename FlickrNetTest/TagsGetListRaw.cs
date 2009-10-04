using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for TagsGetListRaw
    /// </summary>
    [TestClass]
    public class TagsGetListRaw
    {
        Flickr f = TestData.GetAuthInstance();

        public TagsGetListRaw()
        {
            Flickr.CacheDisabled = true;
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
        public void TestGetListUserawAuthentication()
        {
            Flickr f = TestData.GetInstance();
            f.TagsGetListUserRaw();
        }

        [TestMethod]
        public void TestGetUserRawTagsBasic()
        {
            RawTag[] tags = f.TagsGetListUserRaw();

            Assert.IsTrue(tags.Length > 0, "There should be one or more raw tags returned");

            foreach (RawTag tag in tags)
            {
                Assert.IsNotNull(tag.CleanTag, "Clean tag should not be null");
                Assert.IsTrue(tag.CleanTag.Length > 0, "Clean tag should not be empty string");
                Assert.IsTrue(tag.RawTags.Length > 0, "Should be one or more raw tag for each clean tag");
            }
        }
    }
}
