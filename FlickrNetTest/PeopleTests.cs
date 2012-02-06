using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PeopleTests
    /// </summary>
    [TestClass]
    public class PeopleTests
    {
        public PeopleTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void PeopleGetPhotosOfBasicTest()
        {
            Flickr f = TestData.GetInstance();

            PeoplePhotoCollection p = f.PeopleGetPhotosOf(TestData.TestUserId);

            Assert.IsNotNull(p, "PeoplePhotos should not be null.");
            Assert.AreNotEqual(0, p.Count, "PeoplePhotos.Count should be greater than zero.");
            Assert.IsTrue(p.PerPage >= p.Count, "PerPage should be the same or greater than the number of photos returned.");
        }

        [TestMethod()]
        [ExpectedException(typeof(SignatureRequiredException))]
        public void PeopleGetPhotosOfAuthRequired()
        {
            Flickr f = TestData.GetInstance();

            PeoplePhotoCollection p = f.PeopleGetPhotosOf();
        }

        [TestMethod()]
        public void PeopleGetPhotosOfMe()
        {
            Flickr f = TestData.GetAuthInstance();

            PeoplePhotoCollection p = f.PeopleGetPhotosOf();

            Assert.IsNotNull(p, "PeoplePhotos should not be null.");
            Assert.AreNotEqual(0, p.Count, "PeoplePhotos.Count should be greater than zero.");
            Assert.IsTrue(p.PerPage >= p.Count, "PerPage should be the same or greater than the number of photos returned.");
        }

        [TestMethod]
        public void PeopleGetPhotosBasicTest()
        {
            Flickr f = TestData.GetAuthInstance();

            PhotoCollection photos = f.PeopleGetPhotos();

            Assert.IsNotNull(photos);
            Assert.AreNotEqual(0, photos.Count, "Count should not be zero.");
            Assert.IsTrue(photos.Total > 1000, "Total should be greater than 1000.");
        }

        [TestMethod]
        public void PeopleGetPhotosFullParamTest()
        {
            Flickr f = TestData.GetAuthInstance();

            PhotoCollection photos = f.PeopleGetPhotos(TestData.TestUserId, SafetyLevel.Safe, DateTime.Today.AddYears(-2), DateTime.Today, DateTime.Today.AddYears(-2), DateTime.Today, ContentTypeSearch.All, PrivacyFilter.PublicPhotos, PhotoSearchExtras.All, 1, 20);

            Assert.IsNotNull(photos);
            Assert.AreEqual(20, photos.Count, "Count should be twenty.");
        }

        [TestMethod]
        public void PeopleGetInfoBasicTestUnauth()
        {
            Flickr f = TestData.GetInstance();
            Person p = f.PeopleGetInfo("10973297@N00");

            Assert.AreEqual("Miss Aniela", p.UserName);
            Assert.IsNull(p.RealName, "RealName should be null.");
            Assert.AreEqual("ndybisz", p.PathAlias);
            Assert.IsTrue(p.IsPro, "IsPro should be true.");
            Assert.AreEqual("London, UK", p.Location);
            Assert.AreEqual("+00:00", p.TimeZoneOffset);
            Assert.AreEqual("GMT: Dublin, Edinburgh, Lisbon, London", p.TimeZoneLabel);
            Assert.IsNotNull(p.Description, "Description should not be null.");
            Assert.IsTrue(p.Description.Length > 0, "Description should not be empty");
        }

        [TestMethod]
        public void PeopleGetInfoGenderNoAuthTest()
        {
            Flickr f = TestData.GetInstance();
            Person p = f.PeopleGetInfo("10973297@N00");

            Assert.IsNotNull(p, "Person object should be returned");
            Assert.IsNull(p.Gender, "Gender should be null as not authenticated.");

            Assert.IsNull(p.IsReverseContact, "IsReverseContact should not be null.");
            Assert.IsNull(p.IsContact, "IsContact should be null.");
            Assert.IsNull(p.IsIgnored, "IsIgnored should be null.");
            Assert.IsNull(p.IsFriend, "IsFriend should be null.");
        }

        [TestMethod]
        public void PeopleGetInfoGenderTest()
        {
            Flickr f = TestData.GetAuthInstance();
            Person p = f.PeopleGetInfo("10973297@N00");

            Assert.IsNotNull(p, "Person object should be returned");
            Assert.AreEqual("F", p.Gender, "Gender of M should be returned");

            Assert.IsNotNull(p.IsReverseContact, "IsReverseContact should not be null.");
            Assert.IsNotNull(p.IsContact, "IsContact should not be null.");
            Assert.IsNotNull(p.IsIgnored, "IsIgnored should not be null.");
            Assert.IsNotNull(p.IsFriend, "IsFriend should not be null.");

            Assert.IsNotNull(p.PhotosSummary, "PhotosSummary should not be null.");
        }

        [TestMethod]
        public void PeopleGetInfoBuddyIconTest()
        {
            Flickr f = TestData.GetAuthInstance();
            Person p = f.PeopleGetInfo(TestData.TestUserId);
            Assert.IsTrue(p.BuddyIconUrl.Contains(".staticflickr.com/"), "Buddy icon doesn't contain correct details.");
        }

        [TestMethod]
        public void PeopleGetInfoSelfTest()
        {
            Flickr f = TestData.GetAuthInstance();

            Person p = f.PeopleGetInfo(TestData.TestUserId);

            Assert.IsNotNull(p.MailboxSha1Hash, "MailboxSha1Hash should not be null.");
            Assert.IsNotNull(p.PhotosSummary, "PhotosSummary should not be null.");
            Assert.AreNotEqual(0, p.PhotosSummary.Views, "PhotosSummary.Views should not be zero.");

        }

        [TestMethod]
        public void PeopleFindByUsername()
        {
            Flickr f = TestData.GetAuthInstance();

            FoundUser user = f.PeopleFindByUserName("Sam Judson");

            Assert.AreEqual("41888973@N00", user.UserId);
            Assert.AreEqual("Sam Judson", user.UserName);
        }

        [TestMethod]
        public void PeopleFindByEmail()
        {
            Flickr f = TestData.GetAuthInstance();

            FoundUser user = f.PeopleFindByEmail("samjudson@gmail.com");

            Assert.AreEqual("41888973@N00", user.UserId);
            Assert.AreEqual("Sam Judson", user.UserName);
        }

        [TestMethod]
        public void PeopleGetPublicPhotosBasicTest()
        {
            var photos = TestData.GetInstance().PeopleGetPublicPhotos(TestData.TestUserId);

            Assert.IsNotNull(photos);
            Assert.AreNotEqual(0, photos.Count);

            foreach (var photo in photos)
            {
                Assert.IsNotNull(photo.PhotoId);
                Assert.AreEqual<string>(TestData.TestUserId, photo.UserId);
            }
        }

        [TestMethod]
        public void PeopleGetPublicGroupsBasicTest()
        {
            Flickr f = TestData.GetAuthInstance();

            PublicGroupInfoCollection groups = f.PeopleGetPublicGroups(TestData.TestUserId);

            Assert.AreNotEqual(0, groups.Count, "PublicGroupInfoCollection.Count should not be zero.");

            foreach(PublicGroupInfo group in groups)
            {
                Assert.IsNotNull(group.GroupId, "GroupId should not be null.");
                Assert.IsNotNull(group.GroupName, "GroupName should not be null.");
            }
        }

        [TestMethod]
        public void PeopleGetUploadStatusBasicTest()
        {
            var u = TestData.GetAuthInstance().PeopleGetUploadStatus();

            Assert.IsNotNull(u);
            Assert.IsNotNull(u.UserId);
            Assert.IsNotNull(u.UserName);
            Assert.AreNotEqual(0, u.FileSizeMax);
        }
    }
}
