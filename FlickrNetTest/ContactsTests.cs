using System;

using NUnit.Framework;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for ContactsTests
    /// </summary>
    [TestFixture]
    public class ContactsTests : BaseTest
    {
        
        [Test]
        [Category("AccessTokenRequired")]
        public void ContactsGetListTestBasicTest()
        {
            Flickr f = AuthInstance;
            var contacts = f.ContactsGetList();

            Assert.IsNotNull(contacts);

            foreach (var contact in contacts)
            {
                Assert.IsNotNull(contact.UserId, "UserId should not be null.");
                Assert.IsNotNull(contact.UserName, "UserName should not be null.");
                Assert.IsNotNull(contact.BuddyIconUrl, "BuddyIconUrl should not be null.");
            }
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void ContactsGetListFullParamTest()
        {
            Flickr f = AuthInstance;

            ContactCollection contacts = f.ContactsGetList(null, 0, 0);

            Assert.IsNotNull(contacts, "Contacts should not be null.");
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void ContactsGetListFilteredTest()
        {
            Flickr f = AuthInstance;
            var contacts = f.ContactsGetList("friends");

            Assert.IsNotNull(contacts);

            foreach (var contact in contacts)
            {
                Assert.IsNotNull(contact.UserId, "UserId should not be null.");
                Assert.IsNotNull(contact.UserName, "UserName should not be null.");
                Assert.IsNotNull(contact.BuddyIconUrl, "BuddyIconUrl should not be null.");
                Assert.IsNotNull(contact.IsFriend, "IsFriend should not be null.");
                Assert.IsTrue(contact.IsFriend.Value);
            }
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void ContactsGetListPagedTest()
        {
            Flickr f = AuthInstance;
            var contacts = f.ContactsGetList(2, 20);

            Assert.IsNotNull(contacts);
            Assert.AreEqual(2, contacts.Page);
            Assert.AreEqual(20, contacts.PerPage);
            Assert.AreEqual(20, contacts.Count);

            foreach (var contact in contacts)
            {
                Assert.IsNotNull(contact.UserId, "UserId should not be null.");
                Assert.IsNotNull(contact.UserName, "UserName should not be null.");
                Assert.IsNotNull(contact.BuddyIconUrl, "BuddyIconUrl should not be null.");
            }
        }

        [Test]
        public void ContactsGetPublicListTest()
        {
            Flickr f = Instance;

            ContactCollection contacts = f.ContactsGetPublicList(TestData.TestUserId);

            Assert.IsNotNull(contacts, "Contacts should not be null.");

            Assert.AreNotEqual(0, contacts.Total, "Total should not be zero.");
            Assert.AreNotEqual(0, contacts.PerPage, "PerPage should not be zero.");
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void ContactsGetRecentlyUpdatedTest()
        {
            Flickr f = AuthInstance;

            ContactCollection contacts = f.ContactsGetListRecentlyUploaded(DateTime.Now.AddDays(-1), null);

            Assert.IsNotNull(contacts, "Contacts should not be null.");
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void ContactsGetTaggingSuggestions()
        {
            Flickr f = AuthInstance;

            var contacts = f.ContactsGetTaggingSuggestions();

            Assert.IsNotNull(contacts);
        }

    }
}
