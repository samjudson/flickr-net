using NUnit.Framework;
using FlickrNet;

namespace FlickrNet45.Tests
{
    /// <summary>
    /// Summary description for GroupsBrowseTests
    /// </summary>
    [TestFixture]
    public class GroupsTests : BaseTest
    {
        [Test]
        [Category("AccessTokenRequired")]
        public void GroupsBrowseBasicTest()
        {
            GroupCategory cat = AuthInstance.GroupsBrowse();

            Assert.IsNotNull(cat, "GroupCategory should not be null.");
            Assert.AreEqual("/", cat.CategoryName, "CategoryName should be '/'.");
            Assert.AreEqual("/", cat.Path, "Path should be '/'.");
            Assert.AreEqual("", cat.PathIds, "PathIds should be empty string.");
            Assert.AreEqual(0, cat.Subcategories.Count, "No sub categories should be returned.");
            Assert.AreEqual(0, cat.Groups.Count, "No groups should be returned.");
        }

        [Test]
        public void GroupsSearchBasicTest()
        {
            GroupSearchResultCollection results = Instance.GroupsSearch("Buses");

            Assert.IsNotNull(results, "GroupSearchResults should not be null.");
            Assert.AreNotEqual(0, results.Count, "Count should not be zero.");
            Assert.AreNotEqual(0, results.Total, "Total should not be zero.");
            Assert.AreNotEqual(0, results.PerPage, "PerPage should not be zero.");
            Assert.AreEqual(1, results.Page, "Page should be 1.");

            foreach (GroupSearchResult result in results)
            {
                Assert.IsNotNull(result.GroupId, "GroupId should not be null.");
                Assert.IsNotNull(result.GroupName, "GroupName should not be null.");
            }
        }

        [Test]
        public void GroupsGetInfoBasicTest()
        {
            GroupFullInfo info = Instance.GroupsGetInfo(Data.GroupId);

            Assert.IsNotNull(info, "GroupFullInfo should not be null");
            Assert.AreEqual(Data.GroupId, info.GroupId);
            Assert.AreEqual("FLOWERS", info.GroupName);

            Assert.AreEqual("7368", info.IconServer);
            Assert.AreEqual("8", info.IconFarm);

            Assert.AreEqual("http://farm8.staticflickr.com/7368/buddyicons/13378274@N00.jpg", info.GroupIconUrl);

            Assert.AreEqual(3, info.ThrottleInfo.Count);
            Assert.AreEqual(GroupThrottleMode.PerDay, info.ThrottleInfo.Mode);

            Assert.IsTrue(info.Restrictions.PhotosAccepted, "PhotosAccepted should be true.");
            Assert.IsFalse(info.Restrictions.VideosAccepted, "VideosAccepted should be false.");
        }

        [Test]
        public void GroupsGetInfoNoGroupIconTest()
        {
            string groupId = "562176@N20";

            GroupFullInfo info = Instance.GroupsGetInfo(groupId);

            Assert.IsNotNull(info, "GroupFullInfo should not be null");
            Assert.AreEqual("0", info.IconServer, "Icon Server should be zero");
            Assert.AreEqual("http://www.flickr.com/images/buddyicon.jpg", info.GroupIconUrl);

        }

        [Test]
        [Category("AccessTokenRequired")]
        public void GroupsMembersGetListBasicTest()
        {
            var ms = AuthInstance.GroupsMembersGetList(Data.GroupId, 1, 10);

            Assert.IsNotNull(ms);
            Assert.AreNotEqual(0, ms.Count, "Count should not be zero.");
            Assert.AreNotEqual(0, ms.Total, "Total should not be zero.");
            Assert.AreEqual(1, ms.Page, "Page should be one.");
            Assert.AreNotEqual(0, ms.PerPage, "PerPage should not be zero.");
            Assert.AreNotEqual(0, ms.Pages, "Pages should not be zero.");

        }
    }
}
