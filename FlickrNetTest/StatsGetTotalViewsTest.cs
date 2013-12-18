using System;
using NUnit.Framework;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for StatsGetTotalViewsTest
    /// </summary>
    [TestFixture]
    [Category("AccessTokenRequired")]
    public class StatsGetTotalViewsTest : BaseTest
    {
        [Test]
        public void StatsGetTotalViewsBasicTest()
        {
            StatViews views = AuthInstance.StatsGetTotalViews();

            Assert.IsNotNull(views, "StatViews should not be null.");
            Assert.AreNotEqual(0, views.TotalViews, "TotalViews should be greater than zero.");
            Assert.AreNotEqual(0, views.PhotostreamViews, "PhotostreamViews should be greater than zero.");
            Assert.AreNotEqual(0, views.PhotoViews, "PhotoViews should be greater than zero.");
        }

        [Test]
        public void StatGetCsvFilesTest()
        {
            CsvFileCollection col = AuthInstance.StatsGetCsvFiles();

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
