using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlickrNet
{
    public partial class Flickr
    {
        public Profile ProfileGetProfile(string userId)
        {
            var parameters = new Dictionary<string, string> {
                { "method", "flickr.profile.getProfile" },
                { "user_id", userId }
            };

            return GetResponseCache<Profile>(parameters);
        }
    }
}
