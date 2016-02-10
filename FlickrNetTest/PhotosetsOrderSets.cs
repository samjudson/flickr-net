using System.Linq;
using NUnit.Framework;

namespace FlickrNetTest
{
    [TestFixture]
    [Category("AccessTokenRequired")]
    public class PhotosetsOrderSets : BaseTest
    {
        [Test]
        public void PhotosetsOrderSetsStringTest()
        {
            var mySets = AuthInstance.PhotosetsGetList();

            AuthInstance.PhotosetsOrderSets(string.Join(",", mySets.Select(myset => myset.PhotosetId).ToArray()));

        }

        [Test]
        public void PhotosetsOrderSetsArrayTest()
        {
            var mySets = AuthInstance.PhotosetsGetList();

            AuthInstance.PhotosetsOrderSets(mySets.Select(myset => myset.PhotosetId).ToArray());
        }
    }
}
