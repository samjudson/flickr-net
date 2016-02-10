using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using FlickrNet;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using Shouldly;

namespace FlickrNetTest.Async
{
    [TestFixture]
    [Category("AccessTokenRequired")]
    public class StatsAsyncTests : BaseTest
    {
        [Test]
        public void StatsGetCollectionDomainsAsyncTest()
        {
            Flickr f = AuthInstance;

            DateTime d = DateTime.Today.AddDays(-7);

            var w = new AsyncSubject<FlickrResult<StatDomainCollection>>();
            f.StatsGetCollectionDomainsAsync(d, 1, 25, r => { w.OnNext(r); w.OnCompleted(); });

            var result = w.Next().First();
            Assert.IsFalse(result.HasError);
        }

        [Test]
        public void StatsGetPhotoDomainsAsyncTest()
        {
            Flickr f = AuthInstance;

            DateTime d = DateTime.Today.AddDays(-7);

            var w = new AsyncSubject<FlickrResult<StatDomainCollection>>();
            f.StatsGetPhotoDomainsAsync(d, 1, 25, r => { w.OnNext(r); w.OnCompleted(); });

            var result = w.Next().First();
            Assert.IsFalse(result.HasError);
        }

        [Test]
        public void StatsGetPhotostreamDomainsAsyncTest()
        {
            Flickr f = AuthInstance;

            DateTime d = DateTime.Today.AddDays(-7);

            var w = new AsyncSubject<FlickrResult<StatDomainCollection>>();
            f.StatsGetPhotostreamDomainsAsync(d, 1, 25, r => { w.OnNext(r); w.OnCompleted(); });

            var result = w.Next().First();
            Assert.IsFalse(result.HasError);
        }

        [Test]
        public void StatsGetPhotosetDomainsAsyncTest()
        {
            Flickr f = AuthInstance;

            DateTime d = DateTime.Today.AddDays(-7);

            var w = new AsyncSubject<FlickrResult<StatDomainCollection>>();
            f.StatsGetPhotosetDomainsAsync(d, 1, 25, r => { w.OnNext(r); w.OnCompleted(); });

            var result = w.Next().First();
            Assert.IsFalse(result.HasError);
        }


        [Test]
        public void StatsGetCollectionStatsAsyncTest()
        {
            Flickr f = AuthInstance;

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

            Flickr f = AuthInstance;

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
            Flickr f = AuthInstance;

            var range = Enumerable.Range(7, 5);
            var list = new List<Stats>();

            foreach(var i in range)
            {
                var d = DateTime.Today.AddDays(-i);

                var w = new AsyncSubject<FlickrResult<Stats>>();
                f.StatsGetPhotostreamStatsAsync(d, r => { w.OnNext(r); w.OnCompleted(); });

                var result = w.Next().First();

                result.HasError.ShouldBe(false);
                result.Result.ShouldNotBe(null);

                list.Add(result.Result);
            }

            list.Count.ShouldBe(5);
            list.ShouldContain(s => s.Views > 0);
        }
    }
}
