using System;

using NUnit.Framework;
using FlickrNet;
using System.IO;
using Shouldly;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for GroupsPoolsGetGroupsTests
    /// </summary>
    [TestFixture]
    public class GroupsPoolsTests : BaseTest
    {
       
        [Test]
        [Category("AccessTokenRequired")]
        public void GroupsPoolsAddBasicTest()
        {
            Flickr f = AuthInstance;

            byte[] imageBytes = TestData.TestImageBytes;
            var s = new MemoryStream(imageBytes);
            s.Position = 0;

            string title = "Test Title";
            string desc = "Test Description\nSecond Line";
            string tags = "testtag1,testtag2";
            string photoId = f.UploadPicture(s, "Test.jpg", title, desc, tags, false, false, false, ContentType.Other, SafetyLevel.Safe, HiddenFromSearch.Visible);

            try
            {
                f.GroupsPoolsAdd(photoId, TestData.FlickrNetTestGroupId);
            }
            finally
            {
                f.PhotosDelete(photoId);
            }

        }

        [Test]
        [Category("AccessTokenRequired")]
        public void GroupsPoolsAddNotAuthTestTest()
        {
            string photoId = "12345";

            Should.Throw<SignatureRequiredException>(() => Instance.GroupsPoolsAdd(photoId, TestData.FlickrNetTestGroupId));
        }

        [Test]
        public void GroupsPoolGetPhotosFullParamTest()
        {
            Flickr f = Instance;

            PhotoCollection photos = f.GroupsPoolsGetPhotos(TestData.GroupId, null, TestData.TestUserId, PhotoSearchExtras.All, 1, 20);

            Assert.IsNotNull(photos, "Photos should not be null");
            Assert.IsTrue(photos.Count > 0, "Should be more than 0 photos returned");
            Assert.AreEqual(20, photos.PerPage);
            Assert.AreEqual(1, photos.Page);

            foreach (Photo p in photos)
            {
                Assert.AreNotEqual(default(DateTime), p.DateAddedToGroup, "DateAddedToGroup should not be default value");
                Assert.IsTrue(p.DateAddedToGroup < DateTime.Now, "DateAddedToGroup should be in the past");
            }

        }

        [Test]
        public void GroupsPoolGetPhotosDateAddedTest()
        {
            Flickr f = Instance;

            PhotoCollection photos = f.GroupsPoolsGetPhotos(TestData.GroupId);

            Assert.IsNotNull(photos, "Photos should not be null");
            Assert.IsTrue(photos.Count > 0, "Should be more than 0 photos returned");

            foreach (Photo p in photos)
            {
                Assert.AreNotEqual(default(DateTime), p.DateAddedToGroup, "DateAddedToGroup should not be default value");
                Assert.IsTrue(p.DateAddedToGroup < DateTime.Now, "DateAddedToGroup should be in the past");
            }

        }

        [Test]
        [Category("AccessTokenRequired")]
        public void GroupsPoolsGetGroupsBasicTest()
        {
            MemberGroupInfoCollection groups = AuthInstance.GroupsPoolsGetGroups();

            Assert.IsNotNull(groups, "MemberGroupInfoCollection should not be null.");
            Assert.AreNotEqual(0, groups.Count, "MemberGroupInfoCollection.Count should not be zero.");
            Assert.IsTrue(groups.Count > 1, "Count should be greater than one.");

            Assert.AreEqual(400, groups.PerPage, "PerPage should be 400.");
            Assert.AreEqual(1, groups.Page, "Page should be 1.");
            Assert.IsTrue(groups.Total > 1, "Total chould be greater than one");
        }
    }
}
