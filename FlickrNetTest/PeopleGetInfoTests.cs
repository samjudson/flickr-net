using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PersonGetInfoTests
    /// </summary>
    [TestClass]
    public class PeopleGetInfoTests
    {
        public PeopleGetInfoTests()
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
        public void TestPersonGetInfoGenderNoAuth()
        {
            Flickr f = TestData.GetInstance();
            Person p = f.PeopleGetInfo("10973297@N00");

            Console.WriteLine(f.LastResponse);
            Assert.IsNotNull(p, "Person object should be returned");
            Assert.IsNull(p.Gender, "Gender should be null as not authenticated.");
        }

        [TestMethod]
        public void TestPersonGetInfoGender()
        {
            Flickr f = TestData.GetAuthInstance();
            Person p = f.PeopleGetInfo("10973297@N00");

            Console.WriteLine(f.LastResponse);
            Assert.IsNotNull(p, "Person object should be returned");
            Assert.AreEqual("F", p.Gender, "Gender of M should be returned");
        }
    }
}
