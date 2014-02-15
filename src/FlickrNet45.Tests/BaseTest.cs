using System;
using System.Collections.Generic;
using FlickrNet;
using NUnit.Framework;

namespace FlickrNet45.Tests
{
    public abstract class BaseTest
    {
        private Flickr _instance;
        private Flickr _authInstance;
        private Flickr _ignoreInstance;
        private Dictionary<string, string> _errorLog;

        protected readonly TestData Data = new TestData();

        private Flickr GetInstance()
        {
            return new Flickr(Data.ApiKey, Data.SharedSecret);
        }

        private Flickr GetAuthInstance()
        {
            return new Flickr(Data.ApiKey, Data.SharedSecret)
            {
                OAuthAccessToken = Data.AccessToken,
                OAuthAccessTokenSecret = Data.AccessTokenSecret
            };
        }

        protected Flickr Instance
        {
            get { return _instance ?? (_instance = GetInstance()); }
        }

        protected Flickr AuthInstance
        {
            get { return _authInstance ?? (_authInstance = GetAuthInstance()); }
        }

        protected Flickr IgnoreInstance
        {
            get { return _ignoreInstance ?? (_ignoreInstance = GetAuthInstance()); }
        }

        protected bool InstanceUsed
        {
            get { return _instance != null; }
        }

        protected bool AuthInstanceUsed
        {
            get { return _authInstance != null; }
        }

        [SetUp]
        public void InitialiseLoggingAndFlickr()
        {
            _instance = null;
            _authInstance = null;
            _errorLog = new Dictionary<string, string>();
        }

        protected void LogOnError(string key, string information)
        {
            _errorLog.Add(key, information);
        }

        [TearDown]
        public void ErrorLogging()
        {
            if (TestContext.CurrentContext.Result.Status != TestStatus.Failed) return;

            if (InstanceUsed)
            {
                Console.WriteLine("LastRequest: " + _instance.LastRequest);
                Console.WriteLine(_instance.LastResponse);
            }
            if (AuthInstanceUsed)
            {
                Console.WriteLine("LastRequest (Auth): " + _authInstance.LastRequest);
                Console.WriteLine(_authInstance.LastResponse);
            }

            foreach (var line in _errorLog)
            {
                Console.WriteLine(line.Key + ": " + line.Value);
            }
        }
    }

    public class TestData
    {
        public string ApiKey = "dbc316af64fb77dae9140de64262da0a";
        public string SharedSecret = "0781969a058a2745";

        public string UserId = "41888973@N00";
        public string PhotosetId = "72157627145038616";

        // http://www.flickr.com/photos/samjudson/3547139066 - Apple Store
        public string PhotoId = "3547139066";
        // http://www.flickr.com/photos/samjudson/5890800 - Grey Street
        public string FavouritedPhotoId = "5890800";
        // FLOWERS
        public string GroupId = "13378274@N00";
        public string FlickrNetTestGroupId = "1368041@N20";

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

        public const string TestImageBase64 = "R0lGODlhDwAPAKECAAAAzMzM/////wAAACwAAAAADwAPAAACIISPeQHsrZ5ModrLlN48CXF8m2iQ3YmmKqVlRtW4MLwWACH+H09wdGltaXplZCBieSBVbGVhZCBTbWFydFNhdmVyIQAAOw==";

        public static byte[] TestImageBytes
        {
            get
            {
                return Convert.FromBase64String(TestImageBase64);
            }
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