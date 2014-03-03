using System;
using System.Text;
using System.Collections.Generic;

using NUnit.Framework;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for UrlsTests
    /// </summary>
    [TestFixture]
    public class UrlsTests
    {
        [Test]
        [Category("AccessTokenRequired")]
        public void UrlsLookupUserTest1()
        {
            Flickr f = TestData.GetAuthInstance();

            FoundUser user = f.UrlsLookupUser("https://www.flickr.com/photos/samjudson");

            Assert.AreEqual("41888973@N00", user.UserId);
            Assert.AreEqual("Sam Judson", user.UserName);
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void UrlsLookupGroup()
        {
            string groupUrl = "https://www.flickr.com/groups/angels_of_the_north/";

            Flickr f = TestData.GetAuthInstance();

            string groupId = f.UrlsLookupGroup(groupUrl);

            Assert.AreEqual("71585219@N00", groupId);
        }

        [Test]
        public void UrlsLookupGalleryTest()
        {
            string galleryUrl = "https://www.flickr.com/photos/samjudson/galleries/72157622589312064";

            Flickr f = TestData.GetInstance();

            Gallery gallery = f.UrlsLookupGallery(galleryUrl);

            Assert.AreEqual(galleryUrl, gallery.GalleryUrl);

        }

        [Test]
        public void UrlsGetUserPhotosTest()
        {
            string url = TestData.GetInstance().UrlsGetUserPhotos(TestData.TestUserId);

            Assert.AreEqual("https://www.flickr.com/photos/samjudson/", url);
        }

        [Test]
        public void UrlsGetUserProfileTest()
        {
            string url = TestData.GetInstance().UrlsGetUserProfile(TestData.TestUserId);

            Assert.AreEqual("https://www.flickr.com/people/samjudson/", url);
        }

        [Test]
        public void UrlsGetGroupTest()
        {
            string url = TestData.GetInstance().UrlsGetGroup(TestData.GroupId);

            Assert.AreEqual("https://www.flickr.com/groups/florus/", url);
        }



    }
}
