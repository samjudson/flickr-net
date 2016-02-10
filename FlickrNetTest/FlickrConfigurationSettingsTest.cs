using FlickrNet;
using NUnit.Framework;
using System;
using System.Xml;

namespace FlickrNetTest
{
    
    
    /// <summary>
    ///This is a test class for FlickrConfigurationSettingsTest and is intended
    ///to contain all FlickrConfigurationSettingsTest Unit Tests
    ///</summary>
    [TestFixture]
    public class FlickrConfigurationSettingsTest : BaseTest
    {

        /// <summary>
        ///A test for FlickrConfigurationSettings Constructor
        ///</summary>
        [Test]
        public void FlickrConfigurationSettingsConstructorTest()
        {
            const string xml = "<flickrNet apiKey=\"apikey\" secret=\"secret\" token=\"thetoken\" " +
                               "cacheDisabled=\"true\" cacheSize=\"1024\" cacheTimeout=\"01:00:00\" " +
                               "cacheLocation=\"testlocation\" service=\"flickr\">"
                               + "<proxy ipaddress=\"localhost\" port=\"8800\" username=\"testusername\" " +
                               "password=\"testpassword\" domain=\"testdomain\"/>"
                               + "</flickrNet>";
            var doc = new XmlDocument();
            doc.LoadXml(xml);

            var configNode = doc.SelectSingleNode("flickrNet");
            var target = new FlickrConfigurationSettings(configNode);

            Assert.AreEqual("apikey", target.ApiKey);
            Assert.AreEqual("secret", target.SharedSecret);
            Assert.AreEqual("thetoken", target.ApiToken);
            Assert.IsTrue(target.CacheDisabled);
            Assert.AreEqual(1024, target.CacheSize);
            Assert.AreEqual(new TimeSpan(1, 0, 0), target.CacheTimeout);
            Assert.AreEqual("testlocation", target.CacheLocation);

            Assert.IsTrue(target.IsProxyDefined, "IsProxyDefined should be true");
            Assert.AreEqual("localhost", target.ProxyIPAddress);
            Assert.AreEqual(8800, target.ProxyPort);
            Assert.AreEqual("testusername", target.ProxyUsername);
            Assert.AreEqual("testpassword", target.ProxyPassword);
            Assert.AreEqual("testdomain", target.ProxyDomain);
        }
    }
}
