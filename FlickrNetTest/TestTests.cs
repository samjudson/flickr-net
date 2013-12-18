using System;
using System.Text;
using System.Collections.Generic;

using NUnit.Framework;
using FlickrNet;
using System.Xml;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for TestTests
    /// </summary>
    [TestFixture]
    public class TestTests
    {
        [Test]
        public void TestGenericGroupSearch()
        {
            Flickr f = TestData.GetInstance();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("text", "Flowers");
            UnknownResponse response = f.TestGeneric("flickr.groups.search", parameters);

            Assert.IsNotNull(response, "UnknownResponse should not be null.");
            Assert.IsNotNull(response.ResponseXml, "ResponseXml should not be null.");

        }

        [Test]
        [Category("AccessTokenRequired")]
        public void TestGenericTestNull()
        {
            Flickr f = TestData.GetAuthInstance();

            UnknownResponse response = f.TestGeneric("flickr.test.null", null);

            Assert.IsNotNull(response, "UnknownResponse should not be null.");
            Assert.IsNotNull(response.ResponseXml, "ResponseXml should not be null.");
        }

        [Test]
        public void TestEcho()
        {
            Flickr f = TestData.GetInstance();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("test1", "testvalue");

            Dictionary<string, string> returns = f.TestEcho(parameters);

            Assert.IsNotNull(returns);

            // Was 3, now 11 because of extra oauth parameter used by default.
            Assert.AreEqual(11, returns.Count);

            Assert.AreEqual("flickr.test.echo", returns["method"]);
            Assert.AreEqual("testvalue", returns["test1"]);

        }
    }
}
