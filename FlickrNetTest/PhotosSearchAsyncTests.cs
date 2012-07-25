using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;
using System.Linq;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PhotosSearchAsyncTests
    /// </summary>
    [TestClass]
    public class PhotosSearchAsyncTests
    {
        public PhotosSearchAsyncTests()
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
        public void PhotosSearchAsyncBasicTest()
        {
            Flickr f = TestData.GetInstance();

            PhotoSearchOptions o = new PhotoSearchOptions();
            o.Tags = "microsoft";

            var w = new AsyncSubject<FlickrResult<PhotoCollection>>();

            f.PhotosSearchAsync(o, r => { w.OnNext(r); w.OnCompleted(); });
            var result = w.Next().First();

            Assert.IsTrue(result.Result.Total > 0);

        }

        [TestMethod]
        public void PhotosAddTagTest()
        {
            Flickr f = TestData.GetAuthInstance();
            string photoId = "4499284951";
            string tag = "testx";

            var w = new AsyncSubject<FlickrResult<NoResponse>>();

            f.PhotosAddTagsAsync(photoId, tag, r => { w.OnNext(r); w.OnCompleted(); });

            //var result = w.Next().First();

            w.Next().First();

            //Assert.IsFalse(result.HasError);
        }

        [TestMethod]
        public void PhotosSearchAsyncShowerTest()
        {
            Flickr f = TestData.GetAuthInstance();

            PhotoSearchOptions o = new PhotoSearchOptions();
            o.UserId = "78507951@N00";
            o.Tags = "shower";
            o.SortOrder = PhotoSearchSortOrder.DatePostedDescending;
            o.PerPage = 1000;
            o.TagMode = TagMode.AllTags;
            o.Extras = PhotoSearchExtras.All;

            var w = new AsyncSubject<FlickrResult<PhotoCollection>>();

            f.PhotosSearchAsync(o, r => { w.OnNext(r); w.OnCompleted(); });
            var result = w.Next().First();

            Assert.IsTrue(result.Result.Total > 0);

        }

    }
}
