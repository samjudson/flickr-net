using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PhotosCommentsGetListTests
    /// </summary>
    [TestClass]
    public class PhotosCommentsGetListTests
    {
        public PhotosCommentsGetListTests()
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
        public void PhotosCommentsGetListBasicTest()
        {
            Flickr f = TestData.GetInstance();

            PhotoCommentCollection comments = f.PhotosCommentsGetList("3546335765");

            Assert.IsNotNull(comments, "PhotoCommentCollection should not be null.");

            Assert.AreEqual(1, comments.Count, "Count should be one.");

            Assert.AreEqual("ian1001", comments[0].AuthorUserName);
            Assert.AreEqual("Sam lucky you NYCis so cool can't wait to go again it's my fav city along with San fran", comments[0].CommentHtml);


        }
    }
}
