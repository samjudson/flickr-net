using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using FlickrNet;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace FlickrNetTest.Async
{
    [TestFixture]
    public class StatsAsyncTests
    {
        [Test]
        public void StatsGetCollectionDomainsAsyncTest()
        {
            Flickr f = TestData.GetAuthInstance();

            DateTime d = DateTime.Today.AddDays(-7);

            var w = new AsyncSubject<FlickrResult<StatDomainCollection>>();
            f.StatsGetCollectionDomainsAsync(d, 1, 25, r => { w.OnNext(r); w.OnCompleted(); });

            var result = w.Next().First();
            Assert.IsFalse(result.HasError);
        }

        [Test]
        public void StatsGetPhotoDomainsAsyncTest()
        {
            Flickr f = TestData.GetAuthInstance();

            DateTime d = DateTime.Today.AddDays(-7);

            var w = new AsyncSubject<FlickrResult<StatDomainCollection>>();
            f.StatsGetPhotoDomainsAsync(d, 1, 25, r => { w.OnNext(r); w.OnCompleted(); });

            var result = w.Next().First();
            Assert.IsFalse(result.HasError);
        }

        [Test]
        public void StatsGetPhotostreamDomainsAsyncTest()
        {
            Flickr f = TestData.GetAuthInstance();

            DateTime d = DateTime.Today.AddDays(-7);

            var w = new AsyncSubject<FlickrResult<StatDomainCollection>>();
            f.StatsGetPhotostreamDomainsAsync(d, 1, 25, r => { w.OnNext(r); w.OnCompleted(); });

            var result = w.Next().First();
            Assert.IsFalse(result.HasError);
        }

        [Test]
        public void StatsGetPhotosetDomainsAsyncTest()
        {
            Flickr f = TestData.GetAuthInstance();

            DateTime d = DateTime.Today.AddDays(-7);

            var w = new AsyncSubject<FlickrResult<StatDomainCollection>>();
            f.StatsGetPhotosetDomainsAsync(d, 1, 25, r => { w.OnNext(r); w.OnCompleted(); });

            var result = w.Next().First();
            Assert.IsFalse(result.HasError);
        }


        [Test]
        public void StatsGetCollectionStatsAsyncTest()
        {
            Flickr f = TestData.GetAuthInstance();

            var collection = f.CollectionsGetTree().First();

            DateTime d = DateTime.Today.AddDays(-7);

            var w = new AsyncSubject<FlickrResult<Stats>>();
            f.StatsGetCollectionStatsAsync(d, collection.CollectionId, r => { w.OnNext(r); w.OnCompleted(); });

            var result = w.Next().First();
            Assert.IsFalse(result.HasError);

        }

        [Test]
        public void StatsGetPhotoStatsAsyncTest()
        {
            Flickr.CacheDisabled = true;

            Flickr f = TestData.GetAuthInstance();

            DateTime d = DateTime.Today.AddDays(-7);

            var w = new AsyncSubject<FlickrResult<Stats>>();
            f.StatsGetPhotoStatsAsync(d, "7176125763", r => { w.OnNext(r); w.OnCompleted(); });

            var result = w.Next().First();
            if (result.HasError) throw result.Error;

            Assert.IsFalse(result.HasError);
        }

        [Test]
        public void StatsGetPhotostreamStatsAsyncTest()
        {
            Flickr f = TestData.GetAuthInstance();

            DateTime d = DateTime.Today.AddDays(-7);

            var w = new AsyncSubject<FlickrResult<Stats>>();
            f.StatsGetPhotostreamStatsAsync(d, r => { w.OnNext(r); w.OnCompleted(); });

            var result = w.Next().First();
            Assert.IsFalse(result.HasError);

            Assert.IsTrue(result.Result.Views > 0, "Views should be greater than 0");
        }
    }
}
