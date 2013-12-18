using System;
using System.Text;
using System.Collections.Generic;

using NUnit.Framework;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PandaGetListTest
    /// </summary>
    [TestFixture]
    public class PandaTest
    {
        [Test]
        public void PandaGetListBasicTest()
        {
            Flickr f = TestData.GetInstance();

            string[] pandas = f.PandaGetList();

            Assert.IsNotNull(pandas, "Should return string array");
            Assert.IsTrue(pandas.Length > 0, "Should not return empty array");

            Assert.AreEqual("ling ling", pandas[0]);
            Assert.AreEqual("hsing hsing", pandas[1]);
            Assert.AreEqual("wang wang", pandas[2]);
        }

        [Test]
        public void PandaGetPhotosLingLingTest()
        {
            Flickr f = TestData.GetInstance();

            var photos = f.PandaGetPhotos("ling ling");

            Assert.IsNotNull(photos, "PandaPhotos should not be null.");
            Assert.AreEqual(photos.Count, photos.Total, "PandaPhotos.Count should equal PandaPhotos.Total.");
            Assert.AreEqual("ling ling", photos.PandaName, "PandaPhotos.Panda should be 'ling ling'");
        }
    }
}
