using System;
using NUnit.Framework;
using FlickrNet;

namespace FlickrNet45.Tests
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
            var contacts = AuthInstance.ContactsGetList();

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
            ContactCollection contacts = AuthInstance.ContactsGetList(null, 0, 0);

            Assert.IsNotNull(contacts, "Contacts should not be null.");
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void ContactsGetListFilteredTest()
        {
            var contacts = AuthInstance.ContactsGetList("friends");

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
            var contacts = AuthInstance.ContactsGetList(2, 20);

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
            ContactCollection contacts = AuthInstance.ContactsGetPublicList(Data.UserId);

            Assert.IsNotNull(contacts, "Contacts should not be null.");

            Assert.AreNotEqual(0, contacts.Total, "Total should not be zero.");
            Assert.AreNotEqual(0, contacts.PerPage, "PerPage should not be zero.");
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void ContactsGetRecentlyUpdatedTest()
        {
            ContactCollection contacts = AuthInstance.ContactsGetListRecentlyUploaded(DateTime.Now.AddDays(-1));

            Assert.IsNotNull(contacts, "Contacts should not be null.");
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void ContactsGetTaggingSuggestions()
        {
            var contacts = AuthInstance.ContactsGetTaggingSuggestions();

            Assert.IsNotNull(contacts);
        }

    }
}
