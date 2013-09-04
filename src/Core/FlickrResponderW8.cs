using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FlickrNet
{
    internal static partial class FlickrResponder
    {
        internal static async Task<string> DownloadDataAsync(string method, string baseUrl, string data, string contentType, string authHeader)
        {
            var client = new HttpClient();
            //if (!String.IsNullOrEmpty(contentType)) client.DefaultRequestHeaders.Add("Content-Type", contentType);
            if (!String.IsNullOrEmpty(authHeader)) client.DefaultRequestHeaders.Add("Authorization", authHeader);

            if (method == "POST")
            {
                var content = new StringContent(data);
                content.Headers.ContentType.MediaType = contentType;
                var postResponse = await client.PostAsync(new Uri(baseUrl), content);
                return await postResponse.Content.ReadAsStringAsync();
            }

            var getResponse = await client.GetAsync(new Uri(baseUrl));
            return await getResponse.Content.ReadAsStringAsync();
        }

    }
}
