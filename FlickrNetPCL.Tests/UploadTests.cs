using FlickrNet;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FlickrNetPCL.Tests
{
    [TestFixture]
    public class UploadTests : TestBase
    {
        [Test]
        [Category("AccessTokenRequired")]
        public async void ShouldUploadSampleBinaryDataAsImage()
        {
            var imageBytes = TestData.TestImageBytes;
            Stream s = new MemoryStream(imageBytes);
            s.Position = 0;

            const string title = "Test Title";
            const string desc = "Test Description\nSecond Line";
            const string tags = "testtag1,testtag2";
            var photoId = await AuthInstance.UploadPictureAsync(s, "Test.jpg", title, desc, tags, false, false, false, ContentType.Other, SafetyLevel.Safe, HiddenFromSearch.Visible);

            try
            {
                PhotoInfo info = await AuthInstance.PhotosGetInfoAsync(photoId);

                Assert.AreEqual(title, info.Title);
                Assert.AreEqual(desc, info.Description);
                Assert.AreEqual(2, info.Tags.Count);
                Assert.AreEqual("testtag1", info.Tags[0].Raw);
                Assert.AreEqual("testtag2", info.Tags[1].Raw);

                Assert.IsFalse(info.IsPublic);
                Assert.IsFalse(info.IsFamily);
                Assert.IsFalse(info.IsFriend);

                var sizes = await AuthInstance.PhotosGetSizesAsync(photoId);

                var url = sizes[sizes.Count - 1].Source;
                using (var client = new WebClient())
                {
                    var downloadBytes = client.DownloadData(url);
                    var downloadBase64 = Convert.ToBase64String(downloadBytes);

                    Assert.AreEqual(TestData.TestImageBase64, downloadBase64);
                }
            }
            finally
            {
                await AuthInstance.PhotosDeleteAsync(photoId);
            }

        }

        [Test]
        [Category("AccessTokenRequired")]
        [ExpectedException(typeof(FlickrApiException))]
        public async void ShouldFailToUploadSampleBinaryDataAsImage()
        {
            var imageBytes = TestData.TestImageBytes;
            Stream s = new MemoryStream(imageBytes);
            s.Position = 1;

            const string title = "Test Title";
            const string desc = "Test Description\nSecond Line";
            const string tags = "testtag1,testtag2";
            var photoId = await AuthInstance.UploadPictureAsync(s, "Test.fig", title, desc, tags, false, false, false, ContentType.Other, SafetyLevel.Safe, HiddenFromSearch.Visible);

            try
            {
                PhotoInfo info = await AuthInstance.PhotosGetInfoAsync(photoId);

                Assert.AreEqual(title, info.Title);
                Assert.AreEqual(desc, info.Description);
                Assert.AreEqual(2, info.Tags.Count);
                Assert.AreEqual("testtag1", info.Tags[0].Raw);
                Assert.AreEqual("testtag2", info.Tags[1].Raw);

                Assert.IsFalse(info.IsPublic);
                Assert.IsFalse(info.IsFamily);
                Assert.IsFalse(info.IsFriend);

                var sizes = await AuthInstance.PhotosGetSizesAsync(photoId);

                var url = sizes[sizes.Count - 1].Source;
                using (var client = new WebClient())
                {
                    var downloadBytes = client.DownloadData(url);
                    var downloadBase64 = Convert.ToBase64String(downloadBytes);

                    Assert.AreEqual(TestData.TestImageBase64, downloadBase64);
                }
            }
            finally
            {
                await AuthInstance.PhotosDeleteAsync(photoId);
            }

        }
    }
}
