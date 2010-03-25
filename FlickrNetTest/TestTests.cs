using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;
using System.Xml;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for TestTests
    /// </summary>
    [TestClass]
    public class TestTests
    {
        public TestTests()
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
        public void TestGenericGroupSearch()
        {
            Flickr f = TestData.GetInstance();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("text", "Flowers");
            UnknownResponse response = f.TestGeneric("flickr.groups.search", parameters);

            Assert.IsNotNull(response, "UnknownResponse should not be null.");
            Assert.IsNotNull(response.ResponseXml, "ResponseXml should not be null.");

        }

        [TestMethod]
        [ExpectedException(typeof(FlickrWebException))]
        public void TestGenericTestNull()
        {
            Flickr f = TestData.GetAuthInstance();

            UnknownResponse response = f.TestGeneric("flickr.test.null", null);

            Assert.IsNotNull(response, "UnknownResponse should not be null.");
            Assert.IsNotNull(response.ResponseXml, "ResponseXml should not be null.");
        }

        [TestMethod]
        public void TestEcho()
        {
            Flickr f = TestData.GetInstance();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("test1", "testvalue");

            Dictionary<string, string> returns = f.TestEcho(parameters);

            Assert.IsNotNull(returns);

            Assert.AreEqual(3, returns.Count);

            Assert.AreEqual("flickr.test.echo", returns["method"]);
            Assert.AreEqual("testvalue", returns["test1"]);
            Assert.AreEqual(f.ApiKey, returns["api_key"]);

        }
    }
}
