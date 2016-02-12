using System;

using NUnit.Framework;
using FlickrNet;
using Shouldly;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PeopleTests
    /// </summary>
    [TestFixture]
    public class PeopleTests : BaseTest
    {
        [Test]
        public void PeopleGetPhotosOfBasicTest()
        {
            PeoplePhotoCollection p = Instance.PeopleGetPhotosOf(TestData.TestUserId);

            Assert.IsNotNull(p, "PeoplePhotos should not be null.");
            Assert.AreNotEqual(0, p.Count, "PeoplePhotos.Count should be greater than zero.");
            Assert.IsTrue(p.PerPage >= p.Count, "PerPage should be the same or greater than the number of photos returned.");
        }

        [Test]
        public void PeopleGetPhotosOfAuthRequired()
        {
            Should.Throw<SignatureRequiredException>(() => Instance.PeopleGetPhotosOf());
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PeopleGetPhotosOfMe()
        {
            PeoplePhotoCollection p = AuthInstance.PeopleGetPhotosOf();

            Assert.IsNotNull(p, "PeoplePhotos should not be null.");
            Assert.AreNotEqual(0, p.Count, "PeoplePhotos.Count should be greater than zero.");
            Assert.IsTrue(p.PerPage >= p.Count, "PerPage should be the same or greater than the number of photos returned.");
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PeopleGetPhotosBasicTest()
        {
            PhotoCollection photos = AuthInstance.PeopleGetPhotos();

            Assert.IsNotNull(photos);
            Assert.AreNotEqual(0, photos.Count, "Count should not be zero.");
            Assert.IsTrue(photos.Total > 1000, "Total should be greater than 1000.");
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PeopleGetPhotosFullParamTest()
        {
            PhotoCollection photos = AuthInstance.PeopleGetPhotos(TestData.TestUserId, SafetyLevel.Safe, new DateTime(2010, 1, 1),
                                                       new DateTime(2012, 1, 1), new DateTime(2010, 1, 1),
                                                       new DateTime(2012, 1, 1), ContentTypeSearch.All,
                                                       PrivacyFilter.PublicPhotos, PhotoSearchExtras.All, 1, 20);

            Assert.IsNotNull(photos);
            Assert.AreEqual(20, photos.Count, "Count should be twenty.");
        }

        [Test]
        public void PeopleGetInfoBasicTestUnauth()
        {
            Flickr f = Instance;
            Person p = f.PeopleGetInfo(TestData.TestUserId);

            Assert.AreEqual("Sam Judson", p.UserName);
            Assert.AreEqual("Sam Judson", p.RealName);
            Assert.AreEqual("samjudson", p.PathAlias);
            Assert.IsTrue(p.IsPro, "IsPro should be true.");
            Assert.AreEqual("Newcastle, UK", p.Location);
            Assert.AreEqual("+00:00", p.TimeZoneOffset);
            Assert.AreEqual("GMT: Dublin, Edinburgh, Lisbon, London", p.TimeZoneLabel);
            Assert.IsNotNull(p.Description, "Description should not be null.");
            Assert.IsTrue(p.Description.Length > 0, "Description should not be empty");
        }

        [Test]
        public void PeopleGetInfoGenderNoAuthTest()
        {
            Flickr f = Instance;
            Person p = f.PeopleGetInfo("10973297@N00");

            Assert.IsNotNull(p, "Person object should be returned");
            Assert.IsNull(p.Gender, "Gender should be null as not authenticated.");

            Assert.IsNull(p.IsReverseContact, "IsReverseContact should not be null.");
            Assert.IsNull(p.IsContact, "IsContact should be null.");
            Assert.IsNull(p.IsIgnored, "IsIgnored should be null.");
            Assert.IsNull(p.IsFriend, "IsFriend should be null.");
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PeopleGetInfoGenderTest()
        {
            Flickr f = AuthInstance;
            Person p = f.PeopleGetInfo("10973297@N00");

            Assert.IsNotNull(p, "Person object should be returned");
            Assert.AreEqual("F", p.Gender, "Gender of M should be returned");

            Assert.IsNotNull(p.IsReverseContact, "IsReverseContact should not be null.");
            Assert.IsNotNull(p.IsContact, "IsContact should not be null.");
            Assert.IsNotNull(p.IsIgnored, "IsIgnored should not be null.");
            Assert.IsNotNull(p.IsFriend, "IsFriend should not be null.");

            Assert.IsNotNull(p.PhotosSummary, "PhotosSummary should not be null.");
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PeopleGetInfoBuddyIconTest()
        {
            Flickr f = AuthInstance;
            Person p = f.PeopleGetInfo(TestData.TestUserId);
            Assert.IsTrue(p.BuddyIconUrl.Contains(".staticflickr.com/"), "Buddy icon doesn't contain correct details.");
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PeopleGetInfoSelfTest()
        {
            Flickr f = AuthInstance;

            Person p = f.PeopleGetInfo(TestData.TestUserId);

            Assert.IsNotNull(p.MailboxSha1Hash, "MailboxSha1Hash should not be null.");
            Assert.IsNotNull(p.PhotosSummary, "PhotosSummary should not be null.");
            Assert.AreNotEqual(0, p.PhotosSummary.Views, "PhotosSummary.Views should not be zero.");

        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PeopleGetGroupsTest()
        {
            Flickr f = AuthInstance;

            var groups = f.PeopleGetGroups(TestData.TestUserId);

            Assert.IsNotNull(groups);
            Assert.AreNotEqual(0, groups.Count);
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PeopleGetLimitsTest()
        {
            var f = AuthInstance;

            var limits = f.PeopleGetLimits();

            Assert.IsNotNull(limits);

            Assert.AreEqual(0, limits.MaximumDisplayPixels);
            Assert.AreEqual(209715200, limits.MaximumPhotoUpload);
            Assert.AreEqual(1073741824, limits.MaximumVideoUpload);
            Assert.AreEqual(180, limits.MaximumVideoDuration);
            
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PeopleFindByUsername()
        {
            Flickr f = AuthInstance;

            FoundUser user = f.PeopleFindByUserName("Sam Judson");

            Assert.AreEqual("41888973@N00", user.UserId);
            Assert.AreEqual("Sam Judson", user.UserName);
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PeopleFindByEmail()
        {
            Flickr f = AuthInstance;

            FoundUser user = f.PeopleFindByEmail("samjudson@gmail.com");

            Assert.AreEqual("41888973@N00", user.UserId);
            Assert.AreEqual("Sam Judson", user.UserName);
        }

        [Test]
        public void PeopleGetPublicPhotosBasicTest()
        {
            var f = Instance;
            var photos = f.PeopleGetPublicPhotos(TestData.TestUserId, 1, 100, SafetyLevel.None, PhotoSearchExtras.OriginalDimensions);

            Assert.IsNotNull(photos);
            Assert.AreNotEqual(0, photos.Count);

            foreach (var photo in photos)
            {
                Assert.IsNotNull(photo.PhotoId, "PhotoId should not be null.");
                Assert.AreEqual(TestData.TestUserId, photo.UserId);
                Assert.AreNotEqual(0, photo.OriginalWidth, "OriginalWidth should not be zero.");
                Assert.AreNotEqual(0, photo.OriginalHeight, "OriginalHeight should not be zero.");
            }
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PeopleGetPublicGroupsBasicTest()
        {
            Flickr f = AuthInstance;

            GroupInfoCollection groups = f.PeopleGetPublicGroups(TestData.TestUserId);

            Assert.AreNotEqual(0, groups.Count, "PublicGroupInfoCollection.Count should not be zero.");

            foreach(GroupInfo group in groups)
            {
                Assert.IsNotNull(group.GroupId, "GroupId should not be null.");
                Assert.IsNotNull(group.GroupName, "GroupName should not be null.");
            }
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PeopleGetUploadStatusBasicTest()
        {
            var u = AuthInstance.PeopleGetUploadStatus();

            Assert.IsNotNull(u);
            Assert.IsNotNull(u.UserId);
            Assert.IsNotNull(u.UserName);
            Assert.AreNotEqual(0, u.FileSizeMax);
        }

        [Test]
        public void PeopleGetInfoBlankDate()
        {
            var p = Instance.PeopleGetInfo("18387778@N00");
        }

        [Test]
        public void PeopleGetInfoZeroDate()
        {
            var p = Instance.PeopleGetInfo("47963952@N03");
        }

        [Test]
        public void PeopleGetInfoInternationalCharacters()
        {
            var p = Instance.PeopleGetInfo("24754141@N08");

            Assert.AreEqual("24754141@N08", p.UserId, "UserId should match.");
            Assert.AreEqual("Pierre Hsiu 脩丕政", p.RealName, "RealName should match");
        }
    }
}
