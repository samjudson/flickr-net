using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FlickrNetWS.Tests
{
    [TestFixture]
    public class PeopleGetPhotosTests : BaseTest
    {
        [Test]
        public async void ShouldReturnReturnPublicPhotosForUserAsync()
        {
            var photos = await Instance.PeopleGetPublicPhotosAsync(Data.UserId);

            Assert.IsNotNull(photos);
            Assert.IsNotEmpty(photos);

            Assert.IsTrue(photos.All(p => p.UserId == Data.UserId));
        }

    }
}
