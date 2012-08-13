using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace FlickrNetTest.Async
{
    [TestClass]
    public class PhotosAsyncTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Flickr f = TestData.GetInstance();

        }

        [TestMethod]
        public void PhotosGetContactsPublicPhotosAsyncTest()
        {
            Flickr f = TestData.GetInstance();

            var w = new AsyncSubject<FlickrResult<PhotoCollection>>();
            f.PhotosGetContactsPublicPhotosAsync(TestData.TestUserId, 50, true, true, true, PhotoSearchExtras.All, r => { w.OnNext(r); w.OnCompleted(); });
            var result = w.Next().First();

            Assert.IsFalse(result.HasError);
            Assert.IsNotNull(result.Result);

            Assert.IsTrue(result.Result.Count > 0, "Should return some photos.");
        }

        [TestMethod]
        public void PhotosGetCountsAsyncTest()
        {
            DateTime date1 = DateTime.Today.AddMonths(-12);
            DateTime date2 = DateTime.Today.AddMonths(-6);
            DateTime date3 = DateTime.Today;

            DateTime[] uploadDates = new DateTime[] { date1, date2, date3 };

            Flickr f = TestData.GetAuthInstance();

            var w = new AsyncSubject<FlickrResult<PhotoCountCollection>>();
            f.PhotosGetCountsAsync(uploadDates, false, r => { w.OnNext(r); w.OnCompleted(); });
            var result = w.Next().First();

            Assert.IsFalse(result.HasError);

            var counts = result.Result;

            Assert.AreEqual(2, counts.Count, "Should be two counts returned.");

            var count1 = counts[0];

            Assert.AreEqual(date1, count1.FromDate);
            Assert.AreEqual(date2, count1.ToDate);

            var count2 = counts[1];
            Assert.AreEqual(date2, count2.FromDate);
            Assert.AreEqual(date3, count2.ToDate);

        }

        [TestMethod]
        public void PhotosGetExifAsyncTest()
        {
            Flickr f = TestData.GetInstance();

            var w = new AsyncSubject<FlickrResult<ExifTagCollection>>();
            f.PhotosGetExifAsync(TestData.PhotoId, r => { w.OnNext(r); w.OnCompleted(); });
            var result = w.Next().First();

            Assert.IsFalse(result.HasError);

        }

        [TestMethod]
        public void PhotosGetRecentAsyncTest()
        {
            Flickr f = TestData.GetInstance();
            var w = new AsyncSubject<FlickrResult<PhotoCollection>>();
            f.PhotosGetRecentAsync(1, 50, PhotoSearchExtras.All, r => { w.OnNext(r); w.OnCompleted(); });
            var result = w.Next().First();

            Assert.IsFalse(result.HasError);
            Assert.IsNotNull(result.Result);

            Assert.IsTrue(result.Result.Count > 0, "Should return some photos.");

        }


    }
}
