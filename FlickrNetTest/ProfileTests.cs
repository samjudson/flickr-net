using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlickrNetTest
{
    [TestFixture]
    public class ProfileTests : BaseTest
    {
        [Test]
        public void GetDefaultProfile()
        {
            var profile = Instance.ProfileGetProfile(TestData.TestUserId);

            profile.UserId.ShouldBe(TestData.TestUserId);
        }
    }
}
