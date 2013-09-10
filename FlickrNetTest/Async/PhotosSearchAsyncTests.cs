using System;
using System.Text;
using System.Collections.Generic;

using NUnit.Framework;
using FlickrNet;
using System.Linq;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PhotosSearchAsyncTests
    /// </summary>
    [TestFixture]
    public class PhotosSearchAsyncTests
    {

        [Test]
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

        [Test]
        [Category("AccessTokenRequired")]
        public void PhotosAddTagTest()
        {
            Flickr f = TestData.GetAuthInstance();
            string photoId = "4499284951";
            string tag = "testx";

            var w = new AsyncSubject<FlickrResult<NoResponse>>();

            f.PhotosAddTagsAsync(photoId, tag, r => { w.OnNext(r); w.OnCompleted(); });

            var result = w.Next().First();
            
        }

        [Test]
        [Category("AccessTokenRequired")]
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

        [Test]
        [Category("AccessTokenRequired")]
        public void PhotosGetContactsPhotosAsyncTest()
        {
            Flickr f = TestData.GetAuthInstance();
            var w = new AsyncSubject<FlickrResult<PhotoCollection>>();
            f.PhotosGetContactsPhotosAsync(50, true, true, true, PhotoSearchExtras.All, r => { w.OnNext(r); w.OnCompleted(); });
            var result = w.Next().First();

            Assert.IsFalse(result.HasError);
            Assert.IsNotNull(result.Result);

            Assert.IsTrue(result.Result.Count > 0, "Should return some photos.");

        }


    }
}
