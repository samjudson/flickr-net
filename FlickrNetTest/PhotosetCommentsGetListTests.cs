
using NUnit.Framework;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PhotosetCommentsGetListTests
    /// </summary>
    [TestFixture]
    public class PhotosetCommentsGetListTests : BaseTest
    {
       [Test]
        public void PhotosetsCommentsGetListBasicTest()
        {
            Flickr f = Instance;

            PhotosetCommentCollection comments = f.PhotosetsCommentsGetList("1335934");

            Assert.IsNotNull(comments);

            Assert.AreEqual(2, comments.Count);

            Assert.AreEqual("Superchou", comments[0].AuthorUserName);
            Assert.AreEqual("LOL... I had no idea this set existed... what a great afternoon we had :)", comments[0].CommentHtml);
        }
    }
}
