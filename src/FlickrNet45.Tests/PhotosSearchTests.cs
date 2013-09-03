using FlickrNet;
using NUnit.Framework;

namespace FlickrNet45.Tests
{
    [TestFixture]
    public class PhotosSearchTests : BaseTest
    {
        [Test]
        public void ShouldReturnSimpleSearchResults()
        {
            var o = new PhotoSearchOptions { Tags = "colorful" };

            var photos = Instance.PhotosSearch(o);
            Assert.IsNotNull(photos, "Photos should not be null");
            Assert.IsNotEmpty(photos, "Photos should not be empty");
        }

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