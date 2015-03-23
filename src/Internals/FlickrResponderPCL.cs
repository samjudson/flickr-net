using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FlickrNet.Internals
{
    internal static partial class FlickrResponder
    {
        internal static async Task<string> DownloadDataAsync(string method, string baseUrl, string data, string contentType, string authHeader)
        {
            var client = new HttpClient();

            if (!String.IsNullOrEmpty(authHeader)) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", authHeader.Replace("OAuth ", ""));

            if (method == "POST")
            {
                var content = new StringContent(data);
                if (!String.IsNullOrEmpty(contentType))  content.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
                var response = await client.PostAsync(new Uri(baseUrl), content);
                return await response.Content.ReadAsStringAsync();
            }

            return await client.GetStringAsync(new Uri(baseUrl));
        }

        internal static async Task<string> UploadDataAsync(string url, byte[] data, string contentTypeHeader, string oauthHeader)
        {
            var client = new HttpClient();

            if (!String.IsNullOrEmpty(oauthHeader)) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", oauthHeader.Replace("OAuth ", ""));

            var content = new ByteArrayContent(data);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse(contentTypeHeader);
            var response = await client.PostAsync(new Uri(url), content);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
