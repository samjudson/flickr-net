using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace FlickrNetWS.MsTests
{
    [TestClass]
    public class PeopleGetPhotosTests : BaseTest
    {
        [TestMethod]
        public void ShouldReturnReturnPhotosForAuthenticatedUserAsync()
        {
            var photos = AuthInstance.PeopleGetPhotosAsync(Data.UserId).Result;

            Assert.IsNotNull(photos);
            Assert.AreNotEqual(0, photos.Count);

            Assert.IsTrue(photos.All(p => p.UserId == Data.UserId));
        }
    }
}
