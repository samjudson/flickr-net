
using NUnit.Framework;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for GeoTests
    /// </summary>
    [TestFixture]
    public class GeoTests : BaseTest
    {
       
        [Test]
        [Category("AccessTokenRequired")]
        public void PhotosGeoGetPermsBasicTest()
        {
            GeoPermissions perms = AuthInstance.PhotosGeoGetPerms(TestData.PhotoId);

            Assert.IsNotNull(perms);
            Assert.AreEqual(TestData.PhotoId, perms.PhotoId);
            Assert.IsTrue(perms.IsPublic, "IsPublic should be true.");
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PhotosGetWithGeoDataBasicTest()
        {
            PhotoCollection photos = AuthInstance.PhotosGetWithGeoData();

            Assert.IsNotNull(photos);
            Assert.AreNotEqual(0, photos.Count);
            Assert.AreNotEqual(0, photos.Total);
            Assert.AreEqual(1, photos.Page);
            Assert.AreNotEqual(0, photos.PerPage);
            Assert.AreNotEqual(0, photos.Pages);

            foreach (var p in photos)
            {
                Assert.IsNotNull(p.PhotoId);
            }

        }
    }
}
