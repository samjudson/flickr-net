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


        public static string AuthToken
        {
            get
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\FlickrNetTest", true);
                if (key != null && key.GetValue("AuthToken") != null)
                    return key.GetValue("AuthToken").ToString();
                else
                    return null;
            }
            set
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\FlickrNetTest", true);
                if (key == null)
                    key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"SOFTWARE\FlickrNetTest");
                key.SetValue("AuthToken", value);
            }
        }

        public static Flickr GetInstance()
        {
            return new Flickr(ApiKey);
        }


        internal static Flickr GetAuthInstance()
        {
            return new Flickr(ApiKey, SharedSecret, AuthToken);
        }
    }
}
