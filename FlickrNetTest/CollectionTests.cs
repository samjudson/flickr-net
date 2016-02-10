
using NUnit.Framework;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for CollectionGetTreeTest
    /// </summary>
    [TestFixture]
    public class CollectionTests : BaseTest
    {
        
        [Test]
        [Category("AccessTokenRequired")]
        public void CollectionGetInfoBasicTest()
        {
            string id = "78188-72157618817175751";

            Flickr f = AuthInstance;

            CollectionInfo info = f.CollectionsGetInfo(id);

            Assert.AreEqual(id, info.CollectionId, "CollectionId should be correct.");
            Assert.AreEqual(1, info.ChildCount, "ChildCount should be 1.");
            Assert.AreEqual("Global Collection", info.Title, "Title should be 'Global Collection'.");
            Assert.AreEqual("My global collection.", info.Description, "Description should be set correctly.");
            Assert.AreEqual("3629", info.Server, "Server should be 3629.");

            Assert.AreEqual(12, info.IconPhotos.Count, "IconPhotos.Length should be 12.");

            Assert.AreEqual("Tires", info.IconPhotos[0].Title, "The first IconPhoto Title should be 'Tires'.");
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void CollectionGetTreeRootTest()
        {
            Flickr f = AuthInstance;
            CollectionCollection tree = f.CollectionsGetTree();

            Assert.IsNotNull(tree, "CollectionList should not be null.");
            Assert.AreNotEqual(0, tree.Count, "CollectionList.Count should not be zero.");

            foreach (Collection coll in tree)
            {
                Assert.IsNotNull(coll.CollectionId, "CollectionId should not be null.");
                Assert.IsNotNull(coll.Title, "Title should not be null.");
                Assert.IsNotNull(coll.Description, "Description should not be null.");
                Assert.IsNotNull(coll.IconSmall, "IconSmall should not be null.");
                Assert.IsNotNull(coll.IconLarge, "IconLarge should not be null.");

                Assert.AreNotEqual(0, coll.Sets.Count + coll.Collections.Count, "Should be either some sets or some collections.");

                foreach (CollectionSet set in coll.Sets)
                {
                    Assert.IsNotNull(set.SetId, "SetId should not be null.");
                }
            }
        }

        [Test]
        public void CollectionGetTreeRootForSpecificUser()
        {
            Flickr f = Instance;
            CollectionCollection tree = f.CollectionsGetTree(null, TestData.TestUserId);

            Assert.IsNotNull(tree, "CollectionList should not be null.");
            Assert.AreNotEqual(0, tree.Count, "CollectionList.Count should not be zero.");

            foreach (Collection coll in tree)
            {
                Assert.IsNotNull(coll.CollectionId, "CollectionId should not be null.");
                Assert.IsNotNull(coll.Title, "Title should not be null.");
                Assert.IsNotNull(coll.Description, "Description should not be null.");
                Assert.IsNotNull(coll.IconSmall, "IconSmall should not be null.");
                Assert.IsNotNull(coll.IconLarge, "IconLarge should not be null.");

                Assert.AreNotEqual(0, coll.Sets.Count + coll.Collections.Count, "Should be either some sets or some collections.");

                foreach (CollectionSet set in coll.Sets)
                {
                    Assert.IsNotNull(set.SetId, "SetId should not be null.");
                }
            }
        }

        [Test]
        public void CollectionGetSubTreeForSpecificUser()
        {
            string id = "78188-72157618817175751";
            Flickr f = Instance;
            CollectionCollection tree = f.CollectionsGetTree(id, TestData.TestUserId);

            Assert.IsNotNull(tree, "CollectionList should not be null.");
            Assert.AreNotEqual(0, tree.Count, "CollectionList.Count should not be zero.");

            foreach (Collection coll in tree)
            {
                Assert.IsNotNull(coll.CollectionId, "CollectionId should not be null.");
                Assert.IsNotNull(coll.Title, "Title should not be null.");
                Assert.IsNotNull(coll.Description, "Description should not be null.");
                Assert.IsNotNull(coll.IconSmall, "IconSmall should not be null.");
                Assert.IsNotNull(coll.IconLarge, "IconLarge should not be null.");

                Assert.AreNotEqual(0, coll.Sets.Count + coll.Collections.Count, "Should be either some sets or some collections.");

                foreach (CollectionSet set in coll.Sets)
                {
                    Assert.IsNotNull(set.SetId, "SetId should not be null.");
                }
            }
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void CollectionsEditMetaTest()
        {
            string id = "78188-72157618817175751";

            Flickr.CacheDisabled = true;
            Flickr f = AuthInstance;

            CollectionInfo info = f.CollectionsGetInfo(id);

            f.CollectionsEditMeta(id, info.Title, info.Description + "TEST");

            var info2 = f.CollectionsGetInfo(id);

            Assert.AreNotEqual(info.Description, info2.Description);

            // Revert description
            f.CollectionsEditMeta(id, info.Title, info.Description);

        }

        [Test]
        [Category("AccessTokenRequired")]
        public void CollectionsEmptyCollection()
        {
            Flickr f = AuthInstance;

            // Get global collection
            CollectionCollection collections = f.CollectionsGetTree("78188-72157618817175751", null);

            Assert.IsNotNull(collections);
            Assert.IsTrue(collections.Count > 0, "Global collection should be greater than zero.");

            var col = collections[0];

            Assert.AreEqual("Global Collection", col.Title, "Global Collection title should be correct.");

            Assert.IsNotNull(col.Collections, "Child collections property should not be null.");
            Assert.IsTrue(col.Collections.Count > 0, "Global collection should have child collections.");

            var subsol = col.Collections[0];

            Assert.IsNotNull(subsol.Collections, "Child collection Collections property should ne null.");
            Assert.AreEqual(0, subsol.Collections.Count, "Child collection should not have and sub collections.");

        }
    }
}
