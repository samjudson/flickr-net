using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FlickrNet;
using FlickrNetWS.MsTests;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace FlickrNetWS.MsTests
{
    [TestClass]
    public class UploadAsyncTests : BaseTest
    {
        [TestMethod]
        public void ShouldUploadImageAsync()
        {
            var imageBytes = TestData.TestImageBytes;
            Stream s = new MemoryStream(imageBytes);
            s.Position = 0;

            const string title = "Test Title";
            const string desc = "Test Description\nSecond Line";
            const string tags = "testtag1,testtag2";
            var photoId = AuthInstance.UploadPicture(s, "Test.jpg", title, desc, tags, false, false, false, ContentType.Other, SafetyLevel.Safe, HiddenFromSearch.Visible).Result;

            try
            {
                PhotoInfo info = AuthInstance.PhotosGetInfoAsync(photoId).Result;

                Assert.AreEqual(title, info.Title);
                Assert.AreEqual(desc, info.Description);
                Assert.AreEqual(2, info.Tags.Count);
                Assert.AreEqual("testtag1", info.Tags[0].Raw);
                Assert.AreEqual("testtag2", info.Tags[1].Raw);

                Assert.IsFalse(info.IsPublic);
                Assert.IsFalse(info.IsFamily);
                Assert.IsFalse(info.IsFriend);

                var sizes = AuthInstance.PhotosGetSizesAsync(photoId).Result;

                var url = sizes[sizes.Count - 1].Source;
                using (var client = new HttpClient())
                {
                    var downloadBytes = client.GetByteArrayAsync(url).Result;
                    var downloadBase64 = Convert.ToBase64String(downloadBytes);

                    Assert.AreEqual(TestData.TestImageBase64, downloadBase64);
                }
            }
            finally
            {
                AuthInstance.PhotosDeleteAsync(photoId).RunSynchronously();
            }


        }
    }
}
