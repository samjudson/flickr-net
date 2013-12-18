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
    public class PhotosetsTests : BaseTest
    {
        [Test]
        public void ShouldGetListOfPhotosForPhotoset()
        {
            var photos = Instance.PhotosetsGetPhotos(Data.PhotosetId, PhotoSearchExtras.All);

            Assert.IsNotNull(photos);
        }
    }
}
