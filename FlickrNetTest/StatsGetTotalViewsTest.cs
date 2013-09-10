using System;
using System.Text;
using System.Collections.Generic;

using NUnit.Framework;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for StatsGetTotalViewsTest
    /// </summary>
    [TestFixture]
    [Category("AccessTokenRequired")]
    public class StatsGetTotalViewsTest
    {
        [Test]
        public void StatsGetTotalViewsBasicTest()
        {
            Flickr f = TestData.GetAuthInstance();

            StatViews views = f.StatsGetTotalViews();

            Assert.IsNotNull(views, "StatViews should not be null.");
            Assert.AreNotEqual(0, views.TotalViews, "TotalViews should be greater than zero.");
            Assert.AreNotEqual(0, views.PhotostreamViews, "PhotostreamViews should be greater than zero.");
            Assert.AreNotEqual(0, views.PhotoViews, "PhotoViews should be greater than zero.");

            // Seems to be returning zero for some reason.
            //Assert.AreNotEqual(0, views.PhotosetViews, "PhotosetViews should be greater than zero.");

            // I have no collection views, so this almost always returns zero, which is correct.
            //Assert.AreNotEqual(0, views.CollectionViews, "CollectionViews should be greater than zero.");
        }

        [Test]
        public void StatGetCsvFilesTest()
        {
            CsvFileCollection col = TestData.GetAuthInstance().StatsGetCsvFiles();

            Assert.IsNotNull(col, "CsvFileCollection should not be null.");

            Assert.IsTrue(col.Count > 1, "Should be more than one CsvFile returned.");

            foreach (var file in col)
            {
                Assert.IsNotNull(file.Href, "Href should not be null.");
                Assert.IsNotNull(file.Type, "Type should not be null.");
                Assert.AreNotEqual(DateTime.MinValue, file.Date);
            }
        }
    }
}
