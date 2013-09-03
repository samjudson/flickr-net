using System;
using FlickrNet;
using NUnit.Framework;

namespace FlickrNet45.Tests
{
    public abstract class BaseTest
    {
        protected Flickr Instance;
        protected Flickr AuthInstance;

        protected readonly TestData Data = new TestData();

        [SetUp]
        public void CreateFlickrInstance()
        {
            Instance = new Flickr(Data.ApiKey, Data.SharedSecret);
            AuthInstance = new Flickr(Data.ApiKey, Data.SharedSecret)
                               {
                                   OAuthAccessToken = Data.AccessToken,
                                   OAuthAccessTokenSecret = Data.AccessTokenSecret
                               };
        }
    }

    public class TestData
    {
        public string ApiKey = "dbc316af64fb77dae9140de64262da0a";
        public string SharedSecret = "0781969a058a2745";

        public string UserId = "41888973@N00";

        public string RequestToken
        {
            get { return GetRegistryKey("RequestToken"); }
            set { SetRegistryKey("RequestToken", value); }
        }

        public string RequestTokenSecret
        {
            get { return GetRegistryKey("RequestTokenSecret"); }
            set { SetRegistryKey("RequestTokenSecret", value); }
        }

        public string AccessToken
        {
            get { return GetRegistryKey("AccessToken"); }
            set { SetRegistryKey("AccessToken", value); }
        }

        public string AccessTokenSecret
        {
            get { return GetRegistryKey("AccessTokenSecret"); }
            set { SetRegistryKey("AccessTokenSecret", value); }
        }

        private static void SetRegistryKey(string name, string value)
        {
            var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\FlickrNetTest", true) ??
                      Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"SOFTWARE\FlickrNetTest");
            if (key != null) key.SetValue(name, value);
        }

        private static string GetRegistryKey(string name)
        {
            var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\FlickrNetTest", true);
            if (key != null && key.GetValue(name) != null)
                return key.GetValue(name).ToString();
            return null;
        }

    }
}