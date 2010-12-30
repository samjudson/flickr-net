using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for ActivityTests
    /// </summary>
    [TestClass]
    public class ActivityTests
    {
        public ActivityTests()
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
        public void ActivityUserCommentsBasicTest()
        {
            var f = TestData.GetAuthInstance();
            ActivityItemCollection activity = f.ActivityUserComments(0, 0);

            Assert.IsNotNull(activity, "ActivityItemCollection should not be null.");

            foreach (ActivityItem item in activity)
            {
                Assert.IsNotNull(item.Id, "Id should not be null.");
            }
        }

        [TestMethod]
        public void ActivityUserPhotosBasicTest()
        {
            var f = TestData.GetAuthInstance();
            ActivityItemCollection activity = f.ActivityUserPhotos(20, "d");

            Assert.IsNotNull(activity, "ActivityItemCollection should not be null.");

            foreach (ActivityItem item in activity)
            {
                Assert.IsNotNull(item.Id, "Id should not be null.");
            }
        }

        [TestMethod]
        public void ActivityUserPhotosBasicTests()
        {
            Flickr f = TestData.GetAuthInstance();

            // Get last 10 days activity.
            ActivityItemCollection items = f.ActivityUserPhotos(20, "d");

            Console.WriteLine(f.LastResponse);

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
                    Assert.IsTrue(e.DateAdded > DateTime.Today.AddDays(-21), "DateAdded should be within the last 20 days");

                    // For Gallery events the comment is optional.
                    if (e.EventType != ActivityEventType.Gallery)
                    {
                        if (e.EventType == ActivityEventType.Favorite)
                            Assert.IsNull(e.Value, "Value should be null for a favorite event.");
                        else
                            Assert.IsNotNull(e.Value, "Value should not be null for a non-favorite event.");
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
