using System;
using System.Text;
using System.Collections.Generic;

using NUnit.Framework;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PrefsTest
    /// </summary>
    [TestFixture]
    [Category("AccessTokenRequired")]
    public class PrefsTests
    {
        [Test]
        public void PrefsGetContentTypeTest()
        {
            var s = TestData.GetAuthInstance().PrefsGetContentType();

            Assert.IsNotNull(s);
            Assert.AreNotEqual(ContentType.None, s);
        }

        [Test]
        public void PrefsGetGeoPermsTest()
        {
            var p = TestData.GetAuthInstance().PrefsGetGeoPerms();

            Assert.IsNotNull(p);
            Assert.IsTrue(p.ImportGeoExif);
            Assert.AreEqual(GeoPermissionType.Public, p.GeoPermissions);
        }

        [Test]
        public void PrefsGetHiddenTest()
        {
            var s = TestData.GetAuthInstance().PrefsGetHidden();

            Assert.IsNotNull(s);
            Assert.AreNotEqual(HiddenFromSearch.None, s);
        }

        [Test]
        public void PrefsGetPrivacyTest()
        {
            var p = TestData.GetAuthInstance().PrefsGetPrivacy();

            Assert.IsNotNull(p);
            Assert.AreEqual(PrivacyFilter.PublicPhotos, p);
        }

        [Test]
        public void PrefsGetSafetyLevelTest()
        {
            var s = TestData.GetAuthInstance().PrefsGetSafetyLevel();

            Assert.IsNotNull(s);
            Assert.AreEqual(SafetyLevel.Safe, s);
        }


    }
}
