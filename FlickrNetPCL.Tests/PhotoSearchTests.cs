using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FlickrNet;
using NUnit.Framework;

namespace FlickrNetPCL.Tests
{
    [TestFixture]
    public class PhotoSearchTests : TestBase
    {

        [Test]
        public async void SearchForTagShouldReturnResults()
        {
            var o = new PhotoSearchOptions {Tags = "colorful"};
            var result = await Instance.PhotosSearchAsync(o);

            Assert.NotNull(result);
            Assert.AreNotEqual(0, result.Count);
        }

        [Test]
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

                Assert.False(info.IsPublic);
                Assert.False(info.IsFamily);
                Assert.False(info.IsFriend);

                var sizes = await AuthInstance.PhotosGetSizesAsync(photoId);

                var url = sizes[sizes.Count - 1].Source;
                using (var client = new HttpClient())
                {
                    var downloadBytes = await client.GetByteArrayAsync(url);
                    var downloadBase64 = Convert.ToBase64String(downloadBytes);

                    Assert.AreEqual(TestData.TestImageBase64, downloadBase64);
                }
            }
            finally
            {
                AuthInstance.PhotosDeleteAsync(photoId);
            }

        }
    }

    public class TestData
    {
        public string ApiKey = "dbc316af64fb77dae9140de64262da0a";
        public string SharedSecret = "0781969a058a2745";

        public string UserId = "41888973@N00";
        public string PhotosetId = "72157627145038616";

        // http://www.flickr.com/photos/samjudson/3547139066 - Apple Store
        public string PhotoId = "3547139066";
        // http://www.flickr.com/photos/samjudson/5890800 - Grey Street
        public string FavouritedPhotoId = "5890800";
        // FLOWERS
        public string GroupId = "53837206@N00";
        public string FlickrNetTestGroupId = "1368041@N20";

        public string RequestToken
        {
            get { return GetRegistryKey("RequestToken"); }
            set { SetRegistryKey("RequestToken", value); }
        }

        public string RequestTokenSecret
        {
            get { return GetRegistryKey("RequestTokenSecret"); }
            set { SetRegistryKey("RequestTokenSecret", value); }
        }

        public string AccessToken
        {
            get { return GetRegistryKey("AccessToken"); }
            set { SetRegistryKey("AccessToken", value); }
        }

        public string AccessTokenSecret
        {
            get { return GetRegistryKey("AccessTokenSecret"); }
            set { SetRegistryKey("AccessTokenSecret", value); }
        }

        public const string TestImageBase64 = "/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDAAYEBQYFBAYGBQYHBwYIChAKCgkJChQODwwQFxQYGBcUFhYaHSUfGhsjHBYWICwgIyYnKSopGR8tMC0oMCUoKSj/2wBDAQcHBwoIChMKChMoGhYaKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCj/wAARCAAgACADASIAAhEBAxEB/8QAGQAAAwEBAQAAAAAAAAAAAAAAAQYHCAME/8QAMRAAAQQBAgMFBQkAAAAAAAAAAQIDBBEFABITFCEGBzFR0UFVcZOUFSMkUmGEkaHw/8QAFgEBAQEAAAAAAAAAAAAAAAAAAgEE/8QAHBEAAgMBAAMAAAAAAAAAAAAAAQIAAwQSERNB/9oADAMBAAIRAxEAPwDT/Os2QOKaJSSlpZFg0eoGjzrX5Xvkr9NSzvhyGTgsYj7KnSYhW/J4nAcKNwChV141Z/nSTBzHaByuLmckf3C/XW2nC1qdgiY7NYrfgiaI55nye+Sv00OeY3JFuAqISLaUOpND2eeoajOy2R+IzM8nyElwn+jpp7A5heSyDqOZmPIRsJ461KF8RFEWT+upbjetSx+RV6ldgoh70HorK8VzkSZKQVytqYoBUDvT1Ng9NIgyEELKHMXlwk9UANXYoePT46ucvEGUoCSzj5CUrWpHGZ3lO42av/dNec9mo/u/D/SDRr1vWoVfkNuX2MW8yJSMth4rZW7i8qlF7bKBQ8vZpz7uH2Hsm4Y8OTFoJsP1avvEeFaeD2Yje78P9KNdouDEVwKjMY9i1JKiyzsJAINdPhpW7HsXkiSrJ6268z//2Q==";

        public static byte[] TestImageBytes
        {
            get
            {
                return Convert.FromBase64String(TestImageBase64);
            }
        }

        private static void SetRegistryKey(string name, string value)
        {
            var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\FlickrNetTest", true) ??
                      Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"SOFTWARE\FlickrNetTest");
            if (key != null) key.SetValue(name, value);
        }

        private static string GetRegistryKey(string name)
        {
            var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\FlickrNetTest", true);
            if (key != null && key.GetValue(name) != null)
                return key.GetValue(name).ToString();
            return null;
        }

    }
}
