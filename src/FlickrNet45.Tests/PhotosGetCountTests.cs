using System;
using System.Collections.Generic;
using NUnit.Framework;
using FlickrNet;

namespace FlickrNet45.Tests
{
    /// <summary>
    /// Summary description for PhotosGetCountTests
    /// </summary>
    [TestFixture]
    public class PhotosGetCountTests : BaseTest
    {
        [Test]
        [Category("AccessTokenRequired")]
        public void PhotosGetCountTakenTest()
        {
            List<DateTime> dates = new List<DateTime>();
            DateTime date1 = new DateTime(2009, 1, 12);
            DateTime date2 = new DateTime(2009, 9, 12);
            DateTime date3 = new DateTime(2009, 12, 12);

            dates.Add(date1);
            dates.Add(date2);
            dates.Add(date3);

            PhotoCountCollection counts = AuthInstance.PhotosGetCounts(dates.ToArray());

            Assert.IsNotNull(counts, "PhotoCounts should not be null.");
            Assert.AreEqual(2, counts.Count, "PhotoCounts.Count should be two.");

            Assert.AreEqual(date1, counts[0].FromDate, "FromDate should be 12th January.");
            Assert.AreEqual(date2, counts[0].ToDate, "ToDate should be 12th July.");
            Assert.AreEqual(date2, counts[1].FromDate, "FromDate should be 12th July.");
            Assert.AreEqual(date3, counts[1].ToDate, "ToDate should be 12th December.");

        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PhotosGetCountUloadTest()
        {
            var dates = new List<DateTime>();
            var date1 = new DateTime(2009, 7, 12);
            var date2 = new DateTime(2009, 9, 12);
            var date3 = new DateTime(2009, 12, 12);

            dates.Add(date1);
            dates.Add(date2);
            dates.Add(date3);

            PhotoCountCollection counts = AuthInstance.PhotosGetCounts(dates);

            Assert.IsNotNull(counts, "PhotoCounts should not be null.");
            Assert.AreEqual(2, counts.Count, "PhotoCounts.Count should be two.");

            Assert.AreEqual(date1, counts[0].FromDate, "FromDate should be 12th July.");
            Assert.AreEqual(date2, counts[0].ToDate, "ToDate should be 12th September.");
            Assert.AreEqual(date2, counts[1].FromDate, "FromDate should be 12th September.");
            Assert.AreEqual(date3, counts[1].ToDate, "ToDate should be 12th December.");

        }
    }
}
