using System;
using System.Linq;
using NUnit.Framework;
using FlickrNet;
using System.Threading;

namespace FlickrNetTest
{
    [TestFixture]
    public class GroupsDiscussTests : BaseTest
    {
        
        [Test]
        [Category("AccessTokenRequired")]
        public void GroupsDiscussRepliesAddTest()
        {
            var topicId = "72157630982877126";
            var message = "Test message reply\n" + DateTime.Now.ToString("o");
            var newMessage = "New Message reply\n" + DateTime.Now.ToString("o");

            TopicReply reply = null;
            TopicReplyCollection topicReplies;
            try
            {
                AuthInstance.GroupsDiscussRepliesAdd(topicId, message);

                Thread.Sleep(1000);

                topicReplies = AuthInstance.GroupsDiscussRepliesGetList(topicId, 1, 100);

                reply = topicReplies.FirstOrDefault(r => r.Message == message);

                Assert.IsNotNull(reply, "Cannot find matching message.");

                AuthInstance.GroupsDiscussRepliesEdit(topicId, reply.ReplyId, newMessage);

                var reply2 = AuthInstance.GroupsDiscussRepliesGetInfo(topicId, reply.ReplyId);

                Assert.AreEqual(newMessage, reply2.Message, "Message should have been updated.");

            }
            finally
            {
                if (reply != null)
                {
                    AuthInstance.GroupsDiscussRepliesDelete(topicId, reply.ReplyId);
                    topicReplies = AuthInstance.GroupsDiscussRepliesGetList(topicId, 1, 100);
                    var reply3 = topicReplies.FirstOrDefault(r => r.ReplyId == reply.ReplyId);
                    Assert.IsNull(reply3, "Reply should not exist anymore.");
                }
            }

        }

        [Test]
        [Category("AccessTokenRequired")]
        public void GroupsDiscussRepliesGetListTest()
        {
            var topics = AuthInstance.GroupsDiscussTopicsGetList(TestData.GroupId, 1, 100);

            Assert.IsNotNull(topics, "Topics should not be null.");

            Assert.AreNotEqual(0, topics.Count, "Should be more than one topics return.");

            var firstTopic = topics.First(t => t.RepliesCount > 0);

            var replies = AuthInstance.GroupsDiscussRepliesGetList(firstTopic.TopicId, 1, 10);
            Assert.AreEqual(firstTopic.TopicId, replies.TopicId, "TopicId's should be the same.");
            Assert.AreEqual(firstTopic.Subject, replies.Subject, "Subject's should be the same.");
            Assert.AreEqual(firstTopic.Message, replies.Message, "Message's should be the same.");
            Assert.AreEqual(firstTopic.DateCreated, replies.DateCreated, "DateCreated's should be the same.");
            Assert.AreEqual(firstTopic.DateLastPost, replies.DateLastPost, "DateLastPost's should be the same.");

            Assert.IsNotNull(replies, "Replies should not be null.");

            var firstReply = replies.First();

            Assert.IsNotNull(firstReply.ReplyId, "ReplyId should not be null.");

            var reply = AuthInstance.GroupsDiscussRepliesGetInfo(firstTopic.TopicId, firstReply.ReplyId);
            Assert.IsNotNull(reply, "Reply should not be null.");
            Assert.AreEqual(firstReply.Message, reply.Message, "TopicReply.Message should be the same.");
        }

        [Test]
        [Ignore("Got this working, now ignore as there is no way to delete topics!")] 
        [Category("AccessTokenRequired")]
        public void GroupsDiscussTopicsAddTest()
        {
            var groupId = TestData.FlickrNetTestGroupId;

            var subject = "Test subject line: " + DateTime.Now.ToString("o");
            var message = "Subject message line.";

            AuthInstance.GroupsDiscussTopicsAdd(groupId, subject, message);

            var topics = AuthInstance.GroupsDiscussTopicsGetList(groupId, 1, 5);

            var topic = topics.SingleOrDefault(t => t.Subject == subject);

            Assert.IsNotNull(topic, "Unable to find topic with matching subject line.");

            Assert.AreEqual(message, topic.Message, "Message should be the same.");
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void GroupsDiscussTopicsGetListTest()
        {
            var topics = AuthInstance.GroupsDiscussTopicsGetList(TestData.GroupId, 1, 10);

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

            var secondTopic = AuthInstance.GroupsDiscussTopicsGetInfo(firstTopic.TopicId);
            Assert.AreEqual(firstTopic.TopicId, secondTopic.TopicId, "TopicId's should be the same.");
            Assert.AreEqual(firstTopic.Subject, secondTopic.Subject, "Subject's should be the same.");
            Assert.AreEqual(firstTopic.Message, secondTopic.Message, "Message's should be the same.");
            Assert.AreEqual(firstTopic.DateCreated, secondTopic.DateCreated, "DateCreated's should be the same.");
            Assert.AreEqual(firstTopic.DateLastPost, secondTopic.DateLastPost, "DateLastPost's should be the same.");

        }

        [Test]
        [Category("AccessTokenRequired")]
        public void GroupsDiscussTopicsGetListEditableTest()
        {
            var groupId = "51035612836@N01"; // Flickr API group

            var topics = AuthInstance.GroupsDiscussTopicsGetList(groupId, 1, 20);

            Assert.AreNotEqual(0, topics.Count);

            foreach (var topic in topics)
            {
                Assert.IsTrue(topic.CanEdit, "CanEdit should be true.");
                if (!topic.IsLocked)
                    Assert.IsTrue(topic.CanReply, "CanReply should be true.");
                Assert.IsTrue(topic.CanDelete, "CanDelete should be true.");
            }
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void GroupsDiscussTopicsGetInfoStickyTest()
        {
            const string topicId = "72157630982967152";
            var topic = AuthInstance.GroupsDiscussTopicsGetInfo(topicId);

            Assert.IsTrue(topic.IsSticky, "This topic should be marked as sticky.");
            Assert.IsFalse(topic.IsLocked, "This topic should not be marked as locked.");

            // topic.CanReply should be true, but for some reason isn't, so we cannot test it.
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void GroupsDiscussTopicsGetInfoLockedTest()
        {
            const string topicId = "72157630982969782";

            var topic = AuthInstance.GroupsDiscussTopicsGetInfo(topicId);

            Assert.IsTrue(topic.IsLocked, "This topic should be marked as locked.");
            Assert.IsFalse(topic.IsSticky, "This topic should not be marked as sticky.");

            Assert.IsFalse(topic.CanReply, "CanReply should be false as the topic is locked.");
        }

    }
}
