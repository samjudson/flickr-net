using System;
using System.Text;
using System.Collections.Generic;

using NUnit.Framework;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for InterestingnessGetListTests
    /// </summary>
    [TestFixture]
    public class InterestingnessTests
    {
        
        [Test]
        public void InterestingnessGetListTestsBasicTest()
        {
            DateTime date = DateTime.Today.AddDays(-2);
            DateTime datePlusOne = date.AddDays(1);

            PhotoCollection photos = TestData.GetInstance().InterestingnessGetList(date, PhotoSearchExtras.All, 1, 100);

            Assert.IsNotNull(photos, "Photos should not be null.");

            Assert.IsTrue(photos.Count > 50 && photos.Count <= 100, "Count should be at least 50, but not more than 100.");
        }
    }
}
