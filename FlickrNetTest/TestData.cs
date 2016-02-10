using System;
using FlickrNet;

namespace FlickrNetTest
{
    public static class TestData
    {
        public const string ApiKey = "dbc316af64fb77dae9140de64262da0a";
        public const string SharedSecret = "0781969a058a2745";

        // https://www.flickr.com/photos/samjudson/3547139066 - Apple Store
        public const string PhotoId = "3547139066";
        // https://www.flickr.com/photos/samjudson/5890800 - Grey Street
        public const string FavouritedPhotoId = "5890800";

        // FLOWERS
        public const string GroupId = "53837206@N00";

        // Test user is Sam Judson (i.e. Me)
        public const string TestUserId = "41888973@N00";

        public const string TestImageBase64 = "/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDAAYEBQYFBAYGBQYHBwYIChAKCgkJChQODwwQFxQYGBcUFhYaHSUfGhsjHBYWICwgIyYnKSopGR8tMC0oMCUoKSj/2wBDAQcHBwoIChMKChMoGhYaKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCgoKCj/wAARCAAgACADASIAAhEBAxEB/8QAGQAAAwEBAQAAAAAAAAAAAAAAAQYHCAME/8QAMRAAAQQBAgMFBQkAAAAAAAAAAQIDBBEFABITFCEGBzFR0UFVcZOUFSMkUmGEkaHw/8QAFgEBAQEAAAAAAAAAAAAAAAAAAgEE/8QAHBEAAgMBAAMAAAAAAAAAAAAAAQIAAwQSERNB/9oADAMBAAIRAxEAPwDT/Os2QOKaJSSlpZFg0eoGjzrX5Xvkr9NSzvhyGTgsYj7KnSYhW/J4nAcKNwChV141Z/nSTBzHaByuLmckf3C/XW2nC1qdgiY7NYrfgiaI55nye+Sv00OeY3JFuAqISLaUOpND2eeoajOy2R+IzM8nyElwn+jpp7A5heSyDqOZmPIRsJ461KF8RFEWT+upbjetSx+RV6ldgoh70HorK8VzkSZKQVytqYoBUDvT1Ng9NIgyEELKHMXlwk9UANXYoePT46ucvEGUoCSzj5CUrWpHGZ3lO42av/dNec9mo/u/D/SDRr1vWoVfkNuX2MW8yJSMth4rZW7i8qlF7bKBQ8vZpz7uH2Hsm4Y8OTFoJsP1avvEeFaeD2Yje78P9KNdouDEVwKjMY9i1JKiyzsJAINdPhpW7HsXkiSrJ6268z//2Q==";

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

        static void SetRegistryKey(string name, string value)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\FlickrNetTest", true);
            if (key == null)
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"SOFTWARE\FlickrNetTest");
            key.SetValue(name, value);
        }

        static string GetRegistryKey(string name)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\FlickrNetTest", true);
            if (key != null && key.GetValue(name) != null)
                return key.GetValue(name).ToString();
            else
                return null;
        }

        public static Flickr GetInstance()
        {
            return new Flickr(ApiKey) { InstanceCacheDisabled = true };
        }

        public static Flickr GetSignedInstance()
        {
            return new Flickr(ApiKey, SharedSecret) {InstanceCacheDisabled = true};
        }

        public static Flickr GetAuthInstance()
        {
            return new Flickr(ApiKey, SharedSecret)
                           {
                               InstanceCacheDisabled = true,
                               OAuthAccessToken = AccessToken,
                               OAuthAccessTokenSecret = AccessTokenSecret
                           };
        }

        public static Flickr GetOldSignedInstance()
        {
            return new Flickr("3dce465686fd9144c157cb5157bd0e78", "aea31b62c6714269") {InstanceCacheDisabled = true};
        }

        public static Flickr GetOldAuthInstance()
        {
            return new Flickr("3dce465686fd9144c157cb5157bd0e78", "aea31b62c6714269", AuthToken)
                           {
                               InstanceCacheDisabled
                                   = true
                           };
        }

    }
}
