using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FlickrNet
{
    internal static partial class FlickrResponder
    {
        internal static async Task<string> DownloadDataAsync(string method, string baseUrl, string data, string contentType, string authHeader)
        {
            var client = new WebClient();
            if (!String.IsNullOrEmpty(contentType)) client.Headers["Content-Type"] = contentType;
            if (!String.IsNullOrEmpty(authHeader)) client.Headers["Authorization"] = authHeader;

            if (method == "POST")
            {
                return await client.UploadStringTaskAsync(new Uri(baseUrl), data);
            }

            return await client.DownloadStringTaskAsync(new Uri(baseUrl));
        }
    }
}
