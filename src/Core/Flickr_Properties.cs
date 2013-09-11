using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrNet
{
    public partial class Flickr
    {
        public string OAuthAccessToken { get; set; }
        public string OAuthAccessTokenSecret { get; set; }
        public static bool CacheDisabled { get; set; }
        public bool InstanceCacheDisabled { get; set; }
        public static string CacheLocation { get; set; }

        private const string BaseApiUrl = "http://api.flickr.com/services/rest";
        private const string UploadUrl = "http://up.flickr.com/services/upload/";

        public string ApiKey { get; set; }
        public string SharedSecret { get; set; }

        public string LastRequest { get; private set; }
        public string LastResponse { get; private set; }

    }
}
