using NUnit.Framework;

namespace FlickrNet45.Tests
{
    [TestFixture]
    public class PhotosetCommentsGetListTests: BaseTest
    {
        [Test]
        public void PhotosetsCommentsGetListBasicTest()
        {
            
            var comments = Instance.PhotosetsCommentsGetList("1335934");

            Assert.IsNotNull(comments);

            Assert.AreEqual(2, comments.Count);

            Assert.AreEqual("Superchou", comments[0].AuthorUserName);
            Assert.AreEqual("LOL... I had no idea this set existed... what a great afternoon we had :)", comments[0].CommentHtml);
        }
    }
}
