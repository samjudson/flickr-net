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
    public class TagsTests
    {
        Flickr f = TestData.GetAuthInstance();

        public TagsTests()
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
            var tags = f.TagsGetListUserRaw();

            Assert.AreNotEqual(0, tags.Count, "There should be one or more raw tags returned");

            foreach (RawTag tag in tags)
            {
                Assert.IsNotNull(tag.CleanTag, "Clean tag should not be null");
                Assert.IsTrue(tag.CleanTag.Length > 0, "Clean tag should not be empty string");
                Assert.IsTrue(tag.RawTags.Count > 0, "Should be one or more raw tag for each clean tag");
            }
        }

        [TestMethod]
        public void TagsGetListUserPopularBasicTest()
        {
            TagCollection tags = TestData.GetAuthInstance().TagsGetListUserPopular();

            Assert.IsNotNull(tags, "TagCollection should not be null.");
            Assert.AreNotEqual(0, tags.Count, "TagCollection.Count should not be zero.");

            foreach (Tag tag in tags)
            {
                Assert.IsNotNull(tag.TagName, "Tag.TagName should not be null.");
                Assert.AreNotEqual(0, tag.Count, "Tag.Count should not be zero.");
            }
        }

        [TestMethod]
        public void TagsGetListUserBasicTest()
        {
            TagCollection tags = TestData.GetAuthInstance().TagsGetListUser();

            Assert.IsNotNull(tags, "TagCollection should not be null.");
            Assert.AreNotEqual(0, tags.Count, "TagCollection.Count should not be zero.");

            foreach (Tag tag in tags)
            {
                Assert.IsNotNull(tag.TagName, "Tag.TagName should not be null.");
                Assert.AreEqual(0, tag.Count, "Tag.Count should be zero. Not ser for this method.");
            }
        }

        [TestMethod]
        public void TagsGetListPhotoBasicTest()
        {
            var tags = TestData.GetInstance().TagsGetListPhoto(TestData.PhotoId);

            Assert.IsNotNull(tags, "tags should not be null.");
            Assert.AreNotEqual(0, tags.Count, "Length should be greater than zero.");

            foreach (var tag in tags)
            {
                Assert.IsNotNull(tag.TagId, "TagId should not be null.");
                Assert.IsNotNull(tag.TagText, "TagText should not be null.");
                Assert.IsNotNull(tag.Raw, "Raw should not be null.");
                Assert.IsNotNull(tag.IsMachineTag, "IsMachineTag should not be null.");
            }

        }

    }
}
