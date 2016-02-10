using System;

using NUnit.Framework;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PhotosCommentsGetListTests
    /// </summary>
    [TestFixture]
    public class PhotosCommentsTests : BaseTest
    {
        [Test]
        public void PhotosCommentsGetListBasicTest()
        {
            Flickr f = Instance;

            PhotoCommentCollection comments = f.PhotosCommentsGetList("3546335765");

            Assert.IsNotNull(comments, "PhotoCommentCollection should not be null.");

            Assert.AreEqual(1, comments.Count, "Count should be one.");

            Assert.AreEqual("ian1001", comments[0].AuthorUserName);
            Assert.AreEqual("Sam lucky you NYCis so cool can't wait to go again it's my fav city along with San fran", comments[0].CommentHtml);
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PhotosCommentsGetRecentForContactsBasicTest()
        {
            Flickr f = AuthInstance;

            var photos = f.PhotosCommentsGetRecentForContacts();
            Assert.IsNotNull(photos, "PhotoCollection should not be null.");
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PhotosCommentsGetRecentForContactsFullParamTest()
        {
            Flickr f = AuthInstance;

            var photos = f.PhotosCommentsGetRecentForContacts(DateTime.Now.AddHours(-1), PhotoSearchExtras.All, 1, 20);
            Assert.IsNotNull(photos, "PhotoCollection should not be null.");
            Assert.AreEqual(20, photos.PerPage);
        }
    }
}
