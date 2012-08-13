using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    [TestClass]
    public class GroupsDiscussTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Flickr.CacheDisabled = true;
        }

        [TestMethod]
        public void GroupsDiscussRepliesAddTest()
        {
            var f = TestData.GetAuthInstance();
            var topicId = "72157630982877126";
            var message = "Test message reply\r\n" + DateTime.Now.ToString("o");
            var newMessage = "New Message reply\r\n" + DateTime.Now.ToString("o");

            f.GroupsDiscussRepliesAdd(topicId, message);

            var topicReplies = f.GroupsDiscussRepliesGetList(topicId, 1, 100);

            var reply = topicReplies.FirstOrDefault(r => r.Message == message);

            Assert.IsNotNull(reply, "Cannot find matching message.");

            f.GroupsDiscussRepliesEdit(topicId, reply.ReplyId, newMessage);

            var reply2 = f.GroupsDiscussRepliesGetInfo(topicId, reply.ReplyId);

            Assert.AreEqual(newMessage, reply2.Message, "Message should have been updated.");

            f.GroupsDiscussRepliesDelete(topicId, reply.ReplyId);

            topicReplies = f.GroupsDiscussRepliesGetList(topicId, 1, 100);

            var reply3 = topicReplies.FirstOrDefault(r => r.ReplyId == reply.ReplyId);

            Assert.IsNull(reply3, "Reply should not exist anymore.");

        }

        [TestMethod]
        public void GroupsDiscussRepliesGetListTest()
        {
            var f = TestData.GetAuthInstance();

            var topics = f.GroupsDiscussTopicsGetList(TestData.GroupId, 1, 10);

            Assert.IsNotNull(topics, "Topics should not be null.");

            Assert.AreNotEqual(0, topics.Count, "Should be more than one topics return.");

            var firstTopic = topics.First();

            var replies = f.GroupsDiscussRepliesGetList(firstTopic.TopicId, 1, 10);
            Assert.AreEqual(firstTopic.TopicId, replies.TopicId, "TopicId's should be the same.");
            Assert.AreEqual(firstTopic.Subject, replies.Subject, "Subject's should be the same.");
            Assert.AreEqual(firstTopic.Message, replies.Message, "Message's should be the same.");
            Assert.AreEqual(firstTopic.DateCreated, replies.DateCreated, "DateCreated's should be the same.");
            Assert.AreEqual(firstTopic.DateLastPost, replies.DateLastPost, "DateLastPost's should be the same.");

            Assert.IsNotNull(replies, "Replies should not be null.");
            Assert.AreNotEqual(0, replies.Count, "Replies Count should be greater than zero.");

            var firstReply = replies.First();

            Assert.IsNotNull(firstReply.ReplyId, "ReplyId should not be null.");

            var reply = f.GroupsDiscussRepliesGetInfo(firstTopic.TopicId, firstReply.ReplyId);
            Assert.IsNotNull(reply, "Reply should not be null.");
            Assert.AreEqual(firstReply.Message, reply.Message, "TopicReply.Message should be the same.");
        }

        [TestMethod]
        [Ignore] // Got this working, now ignore as there is no way to delete topics!
        public void GroupsDiscussTopicsAddTest()
        {
            var f = TestData.GetAuthInstance();
            var groupId = TestData.FlickrNetTestGroupId;

            var subject = "Test subject line: " + DateTime.Now.ToString("o");
            var message = "Subject message line.";

            f.GroupsDiscussTopicsAdd(groupId, subject, message);

            var topics = f.GroupsDiscussTopicsGetList(groupId, 1, 5);

            var topic = topics.SingleOrDefault(t => t.Subject == subject);

            Assert.IsNotNull(topic, "Unable to find topic with matching subject line.");

            Assert.AreEqual(message, topic.Message, "Message should be the same.");
        }

        [TestMethod]
        public void GroupsDiscussTopicsGetListTest()
        {
            var f = TestData.GetAuthInstance();

            var topics = f.GroupsDiscussTopicsGetList(TestData.GroupId, 1, 10);

            Assert.IsNotNull(topics, "Topics should not be null.");

            Assert.AreEqual(TestData.GroupId, topics.GroupId, "GroupId should be the same.");
            Assert.AreNotEqual(0, topics.Count, "Should be more than one topics return.");
            Assert.AreEqual(10, topics.Count, "Count should be 10.");

            foreach (var topic in topics)
            {
                Assert.IsNotNull(topic.TopicId, "TopicId should not be null.");
                Assert.IsNotNull(topic.Subject, "Subject should not be null.");
                Assert.IsNotNull(topic.Message, "Message should not be null.");
            }

            var firstTopic = topics.First();

            var secondTopic = f.GroupsDiscussTopicsGetInfo(firstTopic.TopicId);
            Assert.AreEqual(firstTopic.TopicId, secondTopic.TopicId, "TopicId's should be the same.");
            Assert.AreEqual(firstTopic.Subject, secondTopic.Subject, "Subject's should be the same.");
            Assert.AreEqual(firstTopic.Message, secondTopic.Message, "Message's should be the same.");
            Assert.AreEqual(firstTopic.DateCreated, secondTopic.DateCreated, "DateCreated's should be the same.");
            Assert.AreEqual(firstTopic.DateLastPost, secondTopic.DateLastPost, "DateLastPost's should be the same.");

        }

        [TestMethod]
        public void GroupsDiscussTopicsGetListEditableTest()
        {
            var groupId = "51035612836@N01"; // Flickr API group
            var f = TestData.GetAuthInstance();

            var topics = f.GroupsDiscussTopicsGetList(groupId, 1, 20);

            Assert.AreNotEqual(0, topics.Count);

            foreach (var topic in topics)
            {
                Assert.IsTrue(topic.CanEdit, "CanEdit should be true.");
                if (!topic.IsLocked)
                    Assert.IsTrue(topic.CanReply, "CanReply should be true.");
                Assert.IsTrue(topic.CanDelete, "CanDelete should be true.");
            }
        }

        [TestMethod]
        public void GroupsDiscussTopicsGetInfoStickyTest()
        {
            var topicId = "72157630982967152";
            var f = TestData.GetAuthInstance();

            var topic = f.GroupsDiscussTopicsGetInfo(topicId);

            Assert.IsTrue(topic.IsSticky, "This topic should be marked as sticky.");
            Assert.IsFalse(topic.IsLocked, "This topic should not be marked as locked.");

            // This assert should pass, but Flickr returns 0 for can_reply, even though I am an admin of this group.
            //Assert.IsTrue(topic.CanReply, "CanReply should be true as the topic is not locked.");
        }

        [TestMethod]
        public void GroupsDiscussTopicsGetInfoLockedTest()
        {
            var topicId = "72157630982969782";
            var f = TestData.GetAuthInstance();

            var topic = f.GroupsDiscussTopicsGetInfo(topicId);

            Assert.IsTrue(topic.IsLocked, "This topic should be marked as locked.");
            Assert.IsFalse(topic.IsSticky, "This topic should not be marked as sticky.");

            Assert.IsFalse(topic.CanReply, "CanReply should be false as the topic is locked.");
        }

    }
}
