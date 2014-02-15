using NUnit.Framework;
using FlickrNet;

namespace FlickrNet45.Tests
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
            PhotoSearchOptions o = new PhotoSearchOptions();

            o.UserId = Data.UserId;
            o.PerPage = 10;
            o.Extras = PhotoSearchExtras.OwnerName;

            PhotoCollection photos = Instance.PhotosSearch(o);

            Assert.IsNotNull(photos[0].OwnerName);
           
        }

        [Test]
        public void PhotosGetContactsPublicPhotosOwnerNameTest()
        {
            PhotoCollection photos = Instance.PhotosGetContactsPublicPhotos(Data.UserId, PhotoSearchExtras.OwnerName);

            Assert.IsNotNull(photos[0].OwnerName);
        }

    }
}
