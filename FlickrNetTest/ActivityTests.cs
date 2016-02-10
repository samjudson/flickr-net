using System;

using NUnit.Framework;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for ActivityTests
    /// </summary>
    [TestFixture]
    public class ActivityTests : BaseTest
    {

        [Test]
        [Category("AccessTokenRequired")]
        public void ActivityUserCommentsBasicTest()
        {
            ActivityItemCollection activity = AuthInstance.ActivityUserComments(0, 0);

            Assert.IsNotNull(activity, "ActivityItemCollection should not be null.");

            foreach (ActivityItem item in activity)
            {
                Assert.IsNotNull(item.Id, "Id should not be null.");
            }
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void ActivityUserPhotosBasicTest()
        {
            ActivityItemCollection activity = AuthInstance.ActivityUserPhotos(20, "d");

            Assert.IsNotNull(activity, "ActivityItemCollection should not be null.");

            foreach (ActivityItem item in activity)
            {
                Assert.IsNotNull(item.Id, "Id should not be null.");
            }
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void ActivityUserPhotosBasicTests()
        {
            int days = 50;

            // Get last 10 days activity.
            ActivityItemCollection items = AuthInstance.ActivityUserPhotos(days, "d");

            Assert.IsNotNull(items, "ActivityItemCollection should not be null.");

            Assert.AreNotEqual(0, items.Count, "ActivityItemCollection should not be zero.");

            foreach (ActivityItem item in items)
            {
                Assert.AreNotEqual(ActivityItemType.Unknown, item.ItemType, "ItemType should not be 'Unknown'.");
                Assert.IsNotNull(item.Id, "Id should not be null.");

                Assert.AreNotEqual(0, item.Events.Count, "Events.Count should not be zero.");

                foreach (ActivityEvent e in item.Events)
                {
                    Assert.AreNotEqual(ActivityEventType.Unknown, e.EventType, "EventType should not be 'Unknown'.");
                    Assert.IsTrue(e.DateAdded > DateTime.Today.AddDays(-days), "DateAdded should be within the last " + days + " days");

                    // For Gallery events the comment is optional.
                    if (e.EventType != ActivityEventType.Gallery)
                    {
                        if (e.EventType == ActivityEventType.Note || e.EventType == ActivityEventType.Comment || e.EventType == ActivityEventType.Tag)
                            Assert.IsNotNull(e.Value, "Value should not be null for a non-favorite event.");
                        else
                            Assert.IsNull(e.Value, "Value should be null for an event of type " + e.EventType);
                    }

                    if (e.EventType == ActivityEventType.Comment)
                        Assert.IsNotNull(e.CommentId, "CommentId should not be null for a comment event.");
                    else
                        Assert.IsNull(e.CommentId, "CommentId should be null for non-comment events.");

                    if (e.EventType == ActivityEventType.Gallery)
                        Assert.IsNotNull(e.GalleryId, "GalleryId should not be null for a gallery event.");
                    else
                        Assert.IsNull(e.GalleryId, "GalleryId should be null for non-gallery events.");
                }
            }
        }
    }
}
