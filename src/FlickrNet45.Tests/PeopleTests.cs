using System;
using FlickrNet.Exceptions;
using NUnit.Framework;
using FlickrNet;

namespace FlickrNet45.Tests
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
            PeoplePhotoCollection p = Instance.PeopleGetPhotosOf(Data.UserId);

            Assert.IsNotNull(p, "PeoplePhotos should not be null.");
            Assert.AreNotEqual(0, p.Count, "PeoplePhotos.Count should be greater than zero.");
            Assert.IsTrue(p.PerPage >= p.Count, "PerPage should be the same or greater than the number of photos returned.");
        }

        [Test()]
        [ExpectedException(typeof(UserNotFoundException))]
        public void PeopleGetPhotosOfAuthRequired()
        {
            PeoplePhotoCollection p = Instance.PeopleGetPhotosOf();
        }

        [Test()]
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
            PhotoCollection photos = AuthInstance.PeopleGetPhotos(Data.UserId, SafetyLevel.Safe, new DateTime(2010, 1, 1),
                                                       new DateTime(2012, 1, 1), new DateTime(2010, 1, 1),
                                                       new DateTime(2012, 1, 1), ContentTypeSearch.All,
                                                       PrivacyFilter.PublicPhotos, PhotoSearchExtras.All, 1, 20);

            Assert.IsNotNull(photos);
            Assert.AreEqual(20, photos.Count, "Count should be twenty.");
        }

        [Test]
        public void PeopleGetInfoBasicTestUnauth()
        {
            Person p = Instance.PeopleGetInfo(Data.UserId);

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
            Person p = Instance.PeopleGetInfo("10973297@N00");

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
            Person p = AuthInstance.PeopleGetInfo("10973297@N00");

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
            Person p = AuthInstance.PeopleGetInfo(Data.UserId);
            Assert.IsTrue(p.BuddyIconUrl.Contains(".staticflickr.com/"), "Buddy icon doesn't contain correct details.");
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PeopleGetInfoSelfTest()
        {
            Person p = AuthInstance.PeopleGetInfo(Data.UserId);

            Assert.IsNotNull(p.MailboxSha1Hash, "MailboxSha1Hash should not be null.");
            Assert.IsNotNull(p.PhotosSummary, "PhotosSummary should not be null.");
            Assert.AreNotEqual(0, p.PhotosSummary.Views, "PhotosSummary.Views should not be zero.");

        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PeopleGetGroupsTest()
        {
            var groups = AuthInstance.PeopleGetGroups(Data.UserId);

            Assert.IsNotNull(groups);
            Assert.AreNotEqual(0, groups.Count);
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PeopleGetLimitsTest()
        {
            var limits = AuthInstance.PeopleGetLimits();

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
            FoundUser user = AuthInstance.PeopleFindByUserName("Sam Judson");

            Assert.AreEqual("Sam Judson", user.UserName);
            Assert.AreEqual("41888973@N00", user.UserId);
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PeopleFindByEmail()
        {
            FoundUser user = AuthInstance.PeopleFindByEmail("samjudson@gmail.com");

            Assert.AreEqual("41888973@N00", user.UserId);
            Assert.AreEqual("Sam Judson", user.UserName);
        }

        [Test]
        public void PeopleGetPublicPhotosBasicTest()
        {
            var photos = Instance.PeopleGetPublicPhotos(Data.UserId);

            Assert.IsNotNull(photos);
            Assert.AreNotEqual(0, photos.Count);

            foreach (var photo in photos)
            {
                Assert.IsNotNull(photo.PhotoId);
                Assert.AreEqual(Data.UserId, photo.UserId);
            }
        }

        [Test]
        public void PeopleGetPublicGroupsBasicTest()
        {
            GroupInfoCollection groups = Instance.PeopleGetPublicGroups(Data.UserId);

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
