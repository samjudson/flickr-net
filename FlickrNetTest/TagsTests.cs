
using NUnit.Framework;
using FlickrNet;
using Shouldly;

namespace FlickrNetTest
{
    [TestFixture]
    public class TagsTests : BaseTest
    {
        public TagsTests()
        {
            Flickr.CacheDisabled = true;
        }

        [Test]
        public void TagsGetListUserRawAuthenticationTest()
        {
            Flickr f = Instance;
            Should.Throw<SignatureRequiredException>(() => f.TagsGetListUserRaw());
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void TagsGetListUserRawBasicTest()
        {
            var tags = AuthInstance.TagsGetListUserRaw();

            Assert.AreNotEqual(0, tags.Count, "There should be one or more raw tags returned");

            foreach (RawTag tag in tags)
            {
                Assert.IsNotNull(tag.CleanTag, "Clean tag should not be null");
                Assert.IsTrue(tag.CleanTag.Length > 0, "Clean tag should not be empty string");
                Assert.IsTrue(tag.RawTags.Count > 0, "Should be one or more raw tag for each clean tag");
            }
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void TagsGetListUserPopularBasicTest()
        {
            TagCollection tags = AuthInstance.TagsGetListUserPopular();

            Assert.IsNotNull(tags, "TagCollection should not be null.");
            Assert.AreNotEqual(0, tags.Count, "TagCollection.Count should not be zero.");

            foreach (Tag tag in tags)
            {
                Assert.IsNotNull(tag.TagName, "Tag.TagName should not be null.");
                Assert.AreNotEqual(0, tag.Count, "Tag.Count should not be zero.");
            }
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void TagsGetListUserBasicTest()
        {
            TagCollection tags = AuthInstance.TagsGetListUser();

            Assert.IsNotNull(tags, "TagCollection should not be null.");
            Assert.AreNotEqual(0, tags.Count, "TagCollection.Count should not be zero.");

            foreach (Tag tag in tags)
            {
                Assert.IsNotNull(tag.TagName, "Tag.TagName should not be null.");
                Assert.AreEqual(0, tag.Count, "Tag.Count should be zero. Not ser for this method.");
            }
        }

        [Test]
        public void TagsGetListPhotoBasicTest()
        {
            var tags = Instance.TagsGetListPhoto(TestData.PhotoId);

            Assert.IsNotNull(tags, "tags should not be null.");
            Assert.AreNotEqual(0, tags.Count, "Length should be greater than zero.");

            foreach (var tag in tags)
            {
                Assert.IsNotNull(tag.TagId, "TagId should not be null.");
                Assert.IsNotNull(tag.TagText, "TagText should not be null.");
                Assert.IsNotNull(tag.Raw, "Raw should not be null.");
                Assert.IsNotNull(tag.IsMachineTag, "IsMachineTag should not be null.");
            }

        }

        [Test]
        public void TagsGetClustersNewcastleTest()
        {
            var col = Instance.TagsGetClusters("newcastle");

            Assert.IsNotNull(col);

            Assert.AreEqual(4, col.Count, "Count should be four.");
            Assert.AreEqual(col.TotalClusters, col.Count);
            Assert.AreEqual("newcastle", col.SourceTag);

            Assert.AreEqual("water-ocean-clouds", col[0].ClusterId);

            foreach (var c in col)
            {
                Assert.AreNotEqual(0, c.TotalTags, "TotalTags should not be zero.");
                Assert.IsNotNull(c.Tags, "Tags should not be null.");
                Assert.IsTrue(c.Tags.Count >= 3);
                Assert.IsNotNull(c.ClusterId);
            }
        }

        [Test]
        public void TagsGetClusterPhotosNewcastleTest()
        {
            Flickr f = Instance;
            var col = f.TagsGetClusters("newcastle");

            foreach (var c in col)
            {
                var ps = f.TagsGetClusterPhotos(c);
                Assert.IsNotNull(ps);
                Assert.AreNotEqual(0, ps.Count);
            }
        }

        [Test]
        public void TagsGetHotListTest()
        {
            var col = Instance.TagsGetHotList();

            Assert.AreNotEqual(0, col.Count, "Count should not be zero.");

            foreach (var c in col)
            {
                Assert.IsNotNull(c);
                Assert.IsNotNull(c.Tag);
                Assert.AreNotEqual(0, c.Score);
            }
        }

        [Test]
        public void TagsGetListUserTest()
        {
            var col = Instance.TagsGetListUser(TestData.TestUserId);
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void TagsGetMostFrequentlyUsedTest()
        {
            Flickr f = AuthInstance;

            var tags = f.TagsGetMostFrequentlyUsed();

            Assert.IsNotNull(tags);

            Assert.AreNotEqual(0, tags.Count);
        }
    }
}
