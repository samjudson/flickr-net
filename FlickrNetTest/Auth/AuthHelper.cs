using System;
using FlickrNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlickrNetTest
{
    [TestClass]
    public class AuthHelper
    {
        /// <summary>
        /// This method will authenticate the current user, and then store the AuthToken in the 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void AuthHelperMethod()
        {
            Flickr f = TestData.GetOldSignedInstance();

            string frob = f.AuthGetFrob();

            Assert.IsNotNull(frob, "Frob should not be null.");

            string url = f.AuthCalcUrl(frob, AuthLevel.Delete);

            Assert.IsNotNull(url, "url should not be null.");

            System.Diagnostics.Process.Start(url);

            // Auth flickr in next 30 seconds

            System.Threading.Thread.Sleep(1000 * 30);

            Auth auth = f.AuthGetToken(frob);

            TestData.AuthToken = auth.Token;

            Console.WriteLine(TestData.AuthToken);
        }
    }
}
