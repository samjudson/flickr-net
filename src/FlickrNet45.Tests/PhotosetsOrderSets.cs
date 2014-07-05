using System;
using System.Collections.Generic;
using System.Linq;
using FlickrNet;
using NUnit.Framework;

namespace FlickrNet45.Tests
{
    /// <summary>
    /// Summary description for PhotosetsOrderSets
    /// </summary>
    [TestFixture]
    public class PhotosetsOrderSets : BaseTest
    {
        [Test]
        [Category("AccessTokenRequired")]
        public void PhotosetsOrderSetsArrayTest()
        {
            PhotosetCollection mySets = AuthInstance.PhotosetsGetList();

            AuthInstance.PhotosetsOrderSets(mySets.Select(myset => myset.PhotosetId));

        }
    }
}
