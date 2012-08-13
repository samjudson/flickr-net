using System;
using System.Collections.Generic;
using System.Text;
using FlickrNet;

namespace FlickrNetTest
{
    public static class TestData
    {
        public const string ApiKey = "dbc316af64fb77dae9140de64262da0a";
        public const string SharedSecret = "0781969a058a2745";

        // http://www.flickr.com/photos/samjudson/3547139066 - Apple Store
        public const string PhotoId = "3547139066";
        // http://www.flickr.com/photos/samjudson/5890800 - Grey Street
        public const string FavouritedPhotoId = "5890800";

        // FLOWERS
        public const string GroupId = "13378274@N00";

        // Test user is Sam Judson (i.e. Me)
        public const string TestUserId = "41888973@N00";

        public const string TestImageBase64 = "R0lGODlhDwAPAKECAAAAzMzM/////wAAACwAAAAADwAPAAACIISPeQHsrZ5ModrLlN48CXF8m2iQ3YmmKqVlRtW4MLwWACH+H09wdGltaXplZCBieSBVbGVhZCBTbWFydFNhdmVyIQAAOw==";

        public static byte[] TestImageBytes
        {
            get
            {
                return Convert.FromBase64String(TestImageBase64);
            }
        }

        public const string FlickrNetTestGroupId = "1368041@N20";

        public static string AuthToken
        {
            get { return GetRegistryKey("AuthToken"); }
            set { SetRegistryKey("AuthToken", value); }
        }

        public static string RequestToken
        {
            get { return GetRegistryKey("RequestToken"); }
            set { SetRegistryKey("RequestToken", value); }
        }

        public static string RequestTokenSecret
        {
            get { return GetRegistryKey("RequestTokenSecret"); }
            set { SetRegistryKey("RequestTokenSecret", value); }
        }

        public static string AccessToken
        {
            get { return GetRegistryKey("AccessToken"); }
            set { SetRegistryKey("AccessToken", value); }
        }

        public static string AccessTokenSecret
        {
            get { return GetRegistryKey("AccessTokenSecret"); }
            set { SetRegistryKey("AccessTokenSecret", value); }
        }

        private static void SetRegistryKey(string name, string value)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\FlickrNetTest", true);
            if (key == null)
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"SOFTWARE\FlickrNetTest");
            key.SetValue(name, value);
        }

        private static string GetRegistryKey(string name)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\FlickrNetTest", true);
            if (key != null && key.GetValue(name) != null)
                return key.GetValue(name).ToString();
            else
                return null;
        }



        public static Flickr GetInstance()
        {
            var f = new Flickr(ApiKey);
            f.InstanceCacheDisabled = true;
            return f;
        }

        public static Flickr GetSignedInstance()
        {
            var f = new Flickr(ApiKey, SharedSecret);
            f.InstanceCacheDisabled = true;
            return f;
        }

        public static Flickr GetAuthInstance()
        {
            Flickr f = new Flickr(ApiKey, SharedSecret);
            f.InstanceCacheDisabled = true;
            f.OAuthAccessToken = AccessToken;
            f.OAuthAccessTokenSecret = AccessTokenSecret;
            return f;
        }

        public static Flickr GetOldSignedInstance()
        {
            Flickr f = new Flickr("3dce465686fd9144c157cb5157bd0e78", "aea31b62c6714269");
            f.InstanceCacheDisabled = true;
            return f;
        }

        public static Flickr GetOldAuthInstance()
        {
            Flickr f = new Flickr("3dce465686fd9144c157cb5157bd0e78", "aea31b62c6714269", AuthToken);
            f.InstanceCacheDisabled = true;
            return f;
        }

    }
}
