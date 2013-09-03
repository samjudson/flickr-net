using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlickrNet;
using NUnit.Framework;

namespace FlickrNet45.Tests
{
    [TestFixture]
    public class PhotosSearchAsyncTests : BaseTest
    {
        [Test]
        public async void ShouldReturnSimpleSearchResults()
        {
            var o = new PhotoSearchOptions {Tags = "colorful"};

            var photos = await Instance.PhotosSearchAsync(o);
            Assert.IsNotNull(photos, "Photos should not be null");
            Assert.IsNotEmpty(photos, "Photos should not be empty");
        }
    }
}
