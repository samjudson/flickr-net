using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for ContactsTests
    /// </summary>
    [TestClass]
    public class ContactsTests
    {
        public ContactsTests()
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
        public void ContactsGetListTestBasicTest()
        {
            Flickr f = TestData.GetAuthInstance();
            var contacts = f.ContactsGetList();

            Assert.IsNotNull(contacts);

            foreach (var contact in contacts)
            {
                Assert.IsNotNull(contact.UserId, "UserId should not be null.");
                Assert.IsNotNull(contact.UserName, "UserName should not be null.");
                Assert.IsNotNull(contact.BuddyIconUrl, "BuddyIconUrl should not be null.");
            }
        }

        [TestMethod]
        public void ContactsGetListFullParamTest()
        {
            Flickr f = TestData.GetAuthInstance();

            ContactCollection contacts = f.ContactsGetList(null, 0, 0);

            Assert.IsNotNull(contacts, "Contacts should not be null.");
        }

        [TestMethod]
        public void ContactsGetListFilteredTest()
        {
            Flickr f = TestData.GetAuthInstance();
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

        [TestMethod]
        public void ContactsGetListPagedTest()
        {
            Flickr f = TestData.GetAuthInstance();
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

        [TestMethod]
        public void ContactsGetPublicListTest()
        {
            Flickr f = TestData.GetInstance();

            ContactCollection contacts = f.ContactsGetPublicList(TestData.TestUserId);

            Assert.IsNotNull(contacts, "Contacts should not be null.");

            Assert.AreNotEqual(0, contacts.Total, "Total should not be zero.");
            Assert.AreNotEqual(0, contacts.PerPage, "PerPage should not be zero.");
        }

        [TestMethod]
        public void ContactsGetRecentlyUpdatedTest()
        {
            Flickr f = TestData.GetAuthInstance();

            ContactCollection contacts = f.ContactsGetListRecentlyUploaded(DateTime.Now.AddDays(-1), null);

            Assert.IsNotNull(contacts, "Contacts should not be null.");
        }

        [TestMethod]
        public void ContactsGetTaggingSuggestions()
        {
            Flickr f = TestData.GetAuthInstance();

            var contacts = f.ContactsGetTaggingSuggestions();

            Assert.IsNotNull(contacts);
        }

    }
}
