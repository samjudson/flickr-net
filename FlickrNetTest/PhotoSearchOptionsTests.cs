
using NUnit.Framework;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PhotoSearchOptionsTests
    /// </summary>
    [TestFixture]
    public class PhotoSearchOptionsTests : BaseTest
    {
        [Test]
        public void PhotoSearchOptionsCalculateSlideshowUrlBasicTest()
        {
            var o = new PhotoSearchOptions {Text = "kittens", InGallery = true};

            var url = o.CalculateSlideshowUrl();

            Assert.IsNotNull(url);

            const string expected = "https://www.flickr.com/show.gne?api_method=flickr.photos.search&method_params=text|kittens;in_gallery|1";

            Assert.AreEqual(expected, url);

        }

        [Test]
        public void PhotoSearchExtrasViews()
        {
            var o = new PhotoSearchOptions {Tags = "kittens", Extras = PhotoSearchExtras.Views};

            var photos = Instance.PhotosSearch(o);

            foreach (var photo in photos)
            {
                Assert.IsTrue(photo.Views.HasValue);
            }
        }
    }
}
