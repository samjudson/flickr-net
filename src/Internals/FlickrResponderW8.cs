using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FlickrNet.Internals
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
                var result = await postResponse.Content.ReadAsStringAsync();
                if (!postResponse.IsSuccessStatusCode)
                {
                    throw new OAuthException(result);
                }
                return result;
            }

            var getResponse = await client.GetAsync(new Uri(baseUrl));
            getResponse.EnsureSuccessStatusCode();
            return await getResponse.Content.ReadAsStringAsync();
        }

    }
}
