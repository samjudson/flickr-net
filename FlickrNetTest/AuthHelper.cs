using System;
using System.Collections.Generic;
using System.Text;
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
        //[Ignore]
        public void Auth()
        {
            Flickr f = new Flickr(TestData.ApiKey, TestData.SharedSecret);
            string frob = f.AuthGetFrob();
            string url = f.AuthCalcUrl(frob, AuthLevel.Delete);

            System.Diagnostics.Process.Start(url);

            // Auth flickr in next 30 seconds

            System.Threading.Thread.Sleep(1000 * 30);

            Auth auth = f.AuthGetToken(frob);

            TestData.AuthToken = auth.Token;

            Console.WriteLine(TestData.AuthToken);
        }
    }
}
