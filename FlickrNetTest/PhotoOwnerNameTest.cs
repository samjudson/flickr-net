using System;
using System.Text;
using System.Collections.Generic;

using NUnit.Framework;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PhotoOwnerNameTest
    /// </summary>
    [TestFixture]
    public class PhotoOwnerNameTest
    {
        [Test]
        public void PhotosSearchOwnerNameTest()
        {
            var o = new PhotoSearchOptions();

            o.UserId = TestData.TestUserId;
            o.PerPage = 10;
            o.Extras = PhotoSearchExtras.OwnerName;

            Flickr f = TestData.GetInstance();
            PhotoCollection photos = f.PhotosSearch(o);

            Assert.IsNotNull(photos[0].OwnerName);
           
        }

        [Test]
        public void PhotosGetContactsPublicPhotosOwnerNameTest()
        {
            Flickr f = TestData.GetInstance();
            PhotoCollection photos = f.PhotosGetContactsPublicPhotos(TestData.TestUserId, PhotoSearchExtras.OwnerName);

            Assert.IsNotNull(photos[0].OwnerName);
        }

    }
}
