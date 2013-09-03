using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrNet
{
    public partial class Flickr
    {
        public Flickr(string apiKey, string sharedSecret)
        {
            ApiKey = apiKey;
            SharedSecret = sharedSecret;
        }

        public Flickr(string apiKey)
        {
            ApiKey = apiKey;
        }


    }
}
