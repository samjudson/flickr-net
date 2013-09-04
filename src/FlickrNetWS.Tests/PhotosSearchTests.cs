using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlickrNet;
using NUnit.Framework;

namespace FlickrNetWS.Tests
{
    [TestFixture]
    public class PhotosSearchTests : BaseTest
    {
        [Test]
        public async void ShouldReturnSimpleSearchResultsAsync()
        {
            var o = new PhotoSearchOptions { Tags = "colorful" };

            var photos = await Instance.PhotosSearchAsync(o);
            Assert.IsNotNull(photos, "Photos should not be null");
            Assert.IsNotEmpty(photos, "Photos should not be empty");
        }

    }
}
