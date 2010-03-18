using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for BlogTests
    /// </summary>
    [TestClass]
    public class BlogTests
    {
        public BlogTests()
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
        public void BlogsGetListTest()
        {
            Flickr f = TestData.GetAuthInstance();

            BlogCollection blogs = f.BlogsGetList();

            Assert.IsNotNull(blogs, "Blogs should not be null.");

            foreach (Blog blog in blogs)
            {
                Assert.IsNotNull(blog.BlogId, "BlogId should not be null.");
                Assert.IsNotNull(blog.NeedsPassword, "NeedsPassword should not be null.");
                Assert.IsNotNull(blog.BlogName, "BlogName should not be null.");
                Assert.IsNotNull(blog.BlogUrl, "BlogUrl should not be null.");
                Assert.IsNotNull(blog.Service, "Service should not be null.");
            }
        }

        [TestMethod]
        public void BlogGetServicesTest()
        {
            Flickr f = TestData.GetInstance();

            BlogServiceCollection services = f.BlogsGetServices();

            Assert.IsNotNull(services, "BlogServices should not be null.");
            Assert.AreNotEqual(0, services.Count, "BlogServices.Count should not be zero.");

            foreach (BlogService service in services)
            {
                Assert.IsNotNull(service.Id, "BlogService.Id should not be null.");
                Assert.IsNotNull(service.Name, "BlogService.Name should not be null.");
            }

            Assert.AreEqual("beta.blogger.com", services[0].Id, "First ID should be beta.blogger.com.");
            Assert.AreEqual("Blogger", services[0].Name, "First Name should be beta.blogger.com.");

        }
    }
}
