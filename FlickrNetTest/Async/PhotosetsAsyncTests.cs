using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace FlickrNetTest.Async
{
    [TestClass]
    public class PhotosetsAsyncTests
    {
        [TestMethod]
        public void PhotosetsGetContextAsyncTest()
        {
            Flickr f = TestData.GetInstance();

            var photosetId = "72157626420254033"; // Beamish
            var photos = f.PhotosetsGetPhotos(photosetId, 1, 100);
            var firstPhoto = photos.First();
            var lastPhoto = photos.Last();

            var w = new AsyncSubject<FlickrResult<Context>>();

            f.PhotosetsGetContextAsync(firstPhoto.PhotoId, photosetId, r => { w.OnNext(r); w.OnCompleted(); });
            var result = w.Next().First();

            Assert.IsFalse(result.HasError);

            var context = result.Result;

            Assert.IsNull(context.PreviousPhoto, "As this is the first photo the previous photo should be null.");
            Assert.IsNotNull(context.NextPhoto, "As this is the first photo the next photo should not be null.");

            w = new AsyncSubject<FlickrResult<Context>>();

            f.PhotosetsGetContextAsync(lastPhoto.PhotoId, photosetId, r => { w.OnNext(r); w.OnCompleted(); });
            result = w.Next().First();

            Assert.IsFalse(result.HasError);

            context = result.Result;

            Assert.IsNull(context.NextPhoto, "As this is the last photo the next photo should be null.");
            Assert.IsNotNull(context.PreviousPhoto, "As this is the last photo the previous photo should not be null.");
        }

        [TestMethod]
        public void PhotosetsGetInfoAsyncTest()
        {
            Flickr f = TestData.GetInstance();

            var photoset = f.PhotosetsGetList(TestData.TestUserId).First();

            var w = new AsyncSubject<FlickrResult<Photoset>>();

            f.PhotosetsGetInfoAsync(photoset.PhotosetId, r => { w.OnNext(r); w.OnCompleted(); });
            var result = w.Next().First();
        }

        [TestMethod]
        public void PhotosetsGeneralAsyncTest()
        {
            Flickr f = TestData.GetAuthInstance();

            var photoId1 = "7519320006"; // Tree/Write/Wall
            var photoId2 = "7176125763"; // Rainbow Rose

            var w = new AsyncSubject<FlickrResult<Photoset>>();
            f.PhotosetsCreateAsync("Test Photoset", photoId1, r => { w.OnNext(r); w.OnCompleted(); });

            var photosetResult = w.Next().First();
            Assert.IsFalse(photosetResult.HasError);
            var photoset = photosetResult.Result;


            try
            {
                var w2 = new AsyncSubject<FlickrResult<NoResponse>>();
                f.PhotosetsEditMetaAsync(photoset.PhotosetId, "New Title", "New Description", r => { w2.OnNext(r); w2.OnCompleted(); });
                var noResponseResult = w2.Next().First();
                Assert.IsFalse(noResponseResult.HasError);

                var w3 = new AsyncSubject<FlickrResult<NoResponse>>();
                f.PhotosetsAddPhotoAsync(photoset.PhotosetId, photoId2, r => { w3.OnNext(r); w3.OnCompleted(); });

                noResponseResult = w3.Next().First();
                Assert.IsFalse(noResponseResult.HasError);
            }
            finally
            {
                var w4 = new AsyncSubject<FlickrResult<NoResponse>>();
                // Clean up and delete photoset
                f.PhotosetsDeleteAsync(photoset.PhotosetId, r => { w4.OnNext(r); w4.OnCompleted(); });
                var noResponseResult = w4.Next().First();
            }

        }

        [TestMethod]
        public void PhotosetsGetPhotosAsyncTest()
        {
            Flickr f = TestData.GetInstance();

            var photoset = f.PhotosetsGetList(TestData.TestUserId).First();

            var w = new AsyncSubject<FlickrResult<PhotosetPhotoCollection>>();

            f.PhotosetsGetPhotosAsync(photoset.PhotosetId, PhotoSearchExtras.All, PrivacyFilter.PublicPhotos, 1, 50, MediaType.All, r => { w.OnNext(r); w.OnCompleted(); });
            var result = w.Next().First();

            Assert.IsFalse(result.HasError);
            
        }
    }
}
