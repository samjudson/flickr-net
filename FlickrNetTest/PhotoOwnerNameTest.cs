
using NUnit.Framework;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PhotoOwnerNameTest
    /// </summary>
    [TestFixture]
    public class PhotoOwnerNameTest : BaseTest
    {
        [Test]
        public void PhotosSearchOwnerNameTest()
        {
            var o = new PhotoSearchOptions();

            o.UserId = TestData.TestUserId;
            o.PerPage = 10;
            o.Extras = PhotoSearchExtras.OwnerName;

            Flickr f = Instance;
            PhotoCollection photos = f.PhotosSearch(o);

            Assert.IsNotNull(photos[0].OwnerName);
           
        }

        [Test]
        public void PhotosGetContactsPublicPhotosOwnerNameTest()
        {
            Flickr f = Instance;
            PhotoCollection photos = f.PhotosGetContactsPublicPhotos(TestData.TestUserId, PhotoSearchExtras.OwnerName);

            Assert.IsNotNull(photos[0].OwnerName);
        }

    }
}
