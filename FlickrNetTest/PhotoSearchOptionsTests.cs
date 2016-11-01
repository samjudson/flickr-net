
using NUnit.Framework;
using FlickrNet;
using System.Collections.Generic;

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

        [Test]
        public void StylesNotAddedToParameters_WhenItIsNotSet()
        {
            var o = new PhotoSearchOptions();
            var parameters = new Dictionary<string, string>();

            o.AddToDictionary(parameters);

            Assert.IsFalse(parameters.ContainsKey("styles"));
        }

        [Test]
        public void StylesNotAddedToParameters_WhenItIsEmpty()
        {
            var o = new PhotoSearchOptions { Styles = new List<Style>() };
            var parameters = new Dictionary<string, string>();

            o.AddToDictionary(parameters);

            Assert.IsFalse(parameters.ContainsKey("styles"));
        }

        [TestCase(Style.BlackAndWhite)]
        [TestCase(Style.BlackAndWhite, Style.DepthOfField)]
        [TestCase(Style.BlackAndWhite, Style.DepthOfField, Style.Minimalism)]
        [TestCase(Style.BlackAndWhite, Style.DepthOfField, Style.Minimalism, Style.Pattern)]
        public void StylesAddedToParameters_WhenItIsNotNullOrEmpty(params Style[] styles)
        {
            var o = new PhotoSearchOptions { Styles = new List<Style>(styles) };
            var parameters = new Dictionary<string, string>();

            o.AddToDictionary(parameters);

            Assert.IsTrue(parameters.ContainsKey("styles"));
        }
    }
}
