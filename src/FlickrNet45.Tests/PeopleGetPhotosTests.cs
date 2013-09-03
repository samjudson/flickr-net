using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FlickrNet45.Tests
{
    [TestFixture]
    public class PeopleGetPhotosTests : BaseTest
    {
        [Test]
        public async void ShouldReturnReturnPhotosForAuthenticatedUserAsync()
        {
            var photos = await AuthInstance.PeopleGetPhotosAsync(Data.UserId);

            Assert.IsNotNull(photos);
            Assert.IsNotEmpty(photos);

            Assert.IsTrue(photos.All(p => p.UserId == Data.UserId));
        }

        [Test]
        public void ShouldReturnReturnPhotosForAuthenticatedUser()
        {
            var photos = AuthInstance.PeopleGetPhotos(Data.UserId);

            Assert.IsNotNull(photos);
            Assert.IsNotEmpty(photos);

            Assert.IsTrue(photos.All(p => p.UserId == Data.UserId));
        }
    }
}
