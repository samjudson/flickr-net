
using NUnit.Framework;
using FlickrNet;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for PandaGetListTest
    /// </summary>
    [TestFixture]
    public class PandaTest : BaseTest
    {
        [Test]
        public void PandaGetListBasicTest()
        {
            string[] pandas = Instance.PandaGetList();

            Assert.IsNotNull(pandas, "Should return string array");
            Assert.IsTrue(pandas.Length > 0, "Should not return empty array");

            Assert.AreEqual("ling ling", pandas[0]);
            Assert.AreEqual("hsing hsing", pandas[1]);
            Assert.AreEqual("wang wang", pandas[2]);
        }

        [Test]
        public void PandaGetPhotosLingLingTest()
        {
            var photos = Instance.PandaGetPhotos("ling ling");

            Assert.IsNotNull(photos, "PandaPhotos should not be null.");
            Assert.AreEqual(photos.Count, photos.Total, "PandaPhotos.Count should equal PandaPhotos.Total.");
            Assert.AreEqual("ling ling", photos.PandaName, "PandaPhotos.Panda should be 'ling ling'");
        }
    }
}
