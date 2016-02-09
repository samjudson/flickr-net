using System;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.Threading;
using System.Collections.Generic;
#if SILVERLIGHT
using System.Linq;
#endif

#pragma warning disable CS0618 // Type or member is obsolete

namespace FlickrNet
{
    /// <summary>
    /// The main Flickr class.
    /// </summary>
    /// <remarks>
    /// Create an instance of this class and then call its methods to perform methods on Flickr.
    /// </remarks>
    /// <example>
    /// <code>
    /// FlickrNet.Flickr flickr = new FlickrNet.Flickr();
    /// User user = flickr.PeopleFindByEmail("cal@iamcal.com");
    /// Console.WriteLine("User Id is " + u.UserId);
    /// </code>
    /// </example>
    // [System.Net.WebPermission(System.Security.Permissions.SecurityAction.Demand, ConnectPattern="http://www.flickr.com/.*")]
    public partial class Flickr
    {

        /// <summary>
        /// 
        /// </summary>
        /// <summary>
        /// UploadProgressHandler is fired during a synchronous upload process to signify that 
        /// a segment of uploading has been completed. This is approximately 50 bytes. The total
        /// uploaded is recorded in the <see cref="UploadProgressEventArgs"/> class.
        /// </summary>
        public event EventHandler<UploadProgressEventArgs> OnUploadProgress;

#if !(MONOTOUCH || WindowsCE || SILVERLIGHT)
        private static bool isServiceSet;
#endif
        private static SupportedService defaultService = SupportedService.Flickr;

        private SupportedService service = SupportedService.Flickr;

        /// <summary>
        /// The base URL for all Flickr REST method calls.
        /// </summary>
        public Uri BaseUri
        {
            get { return baseUri[(int)service]; }
        }

        private readonly Uri[] baseUri = { 
                                                     new Uri("https://api.flickr.com/services/rest/"), 
                                                     new Uri("http://beta.zooomr.com/bluenote/api/rest"),
                                                     new Uri("http://www.23hq.com/services/rest/")
        };

        private string UploadUrl
        {
            get { return uploadUrl[(int)service]; }
        }
        private static string[] uploadUrl = {
                                                              "https://up.flickr.com/services/upload/",
                                                              "http://beta.zooomr.com/bluenote/api/upload",
                                                              "http://www.23hq.com/services/upload/"
        };

        private string ReplaceUrl
        {
            get { return replaceUrl[(int)service]; }
        }
        private static string[] replaceUrl = {
                                                               "https://up.flickr.com/services/replace/",
                                                               "http://beta.zooomr.com/bluenote/api/replace",
                                                               "http://www.23hq.com/services/replace/"
        };

        private string AuthUrl
        {
            get { return authUrl[(int)service]; }
        }
        private static string[] authUrl = {
                                                            "https://www.flickr.com/services/auth/",
                                                            "http://beta.zooomr.com/auth/",
                                                            "http://www.23hq.com/services/auth/"
        };

        private string apiKey;
        private string apiToken;
        private string sharedSecret;
        // private int timeout = 100000;  // original value
        private int timeout = 3600000;  // (Andrew Keil) Changed to 1 hour in milliseconds to avoid timeout issues when uploading picture & videos

        private string lastRequest;
        private string lastResponse;

#if !SILVERLIGHT
        private WebProxy proxy; 
#endif

        /// <summary>
        /// Get or set the API Key to be used by all calls. API key is mandatory for all 
        /// calls to Flickr.
        /// </summary>
        public string ApiKey
        {
            get { return apiKey; }
            set
            {
                apiKey = value == null || value.Length == 0 ? null : value;
            }
        }

        /// <summary>
        /// API shared secret is required for all calls that require signing, which includes
        /// all methods that require authentication, as well as the actual flickr.auth.* calls.
        /// </summary>
        public string ApiSecret
        {
            get { return sharedSecret; }
            set
            {
                sharedSecret = value == null || value.Length == 0 ? null : value;
            }
        }

        /// <summary>
        /// The authentication token is required for all calls that require authentication.
        /// A <see cref="FlickrApiException"/> will be raised by Flickr if the authentication token is
        /// not set when required.
        /// </summary>
        /// <remarks>
        /// It should be noted that some methods will work without the authentication token, but
        /// will return different results if used with them (such as group pool requests, 
        /// and results which include private pictures the authenticated user is allowed to see
        /// (their own, or others).
        /// </remarks>
        [Obsolete("Use OAuthToken and OAuthTokenSecret now.")]
        public string AuthToken
        {
            get { return apiToken; }
            set
            {
                apiToken = value == null || value.Length == 0 ? null : value;
            }
        }

        /// <summary>
        /// OAuth Access Token. Needed for authenticated access using OAuth to Flickr.
        /// </summary>
        public string OAuthAccessToken { get; set; }

        /// <summary>
        /// OAuth Access Token Secret. Needed for authenticated access using OAuth to Flickr.
        /// </summary>
        public string OAuthAccessTokenSecret { get; set; }


        /// <summary>
        /// Gets or sets whether the cache should be disabled. Use only in extreme cases where you are sure you
        /// don't want any caching.
        /// </summary>
        public static bool CacheDisabled
        {
            get { return Cache.CacheDisabled; }
            set { Cache.CacheDisabled = value; }
        }

        /// <summary>
        /// Override if the cache is disabled for this particular instance of <see cref="Flickr"/>.
        /// </summary>
        public bool InstanceCacheDisabled { get; set; }

        /// <summary>
        /// All GET calls to Flickr are cached by the Flickr.Net API. Set the <see cref="CacheTimeout"/>
        /// to determine how long these calls should be cached (make this as long as possible!)
        /// </summary>
        public static TimeSpan CacheTimeout
        {
            get { return Cache.CacheTimeout; }
            set { Cache.CacheTimeout = value; }
        }

        /// <summary>
        /// Sets or gets the location to store the Cache files.
        /// </summary>
        public static string CacheLocation
        {
            get { return Cache.CacheLocation; }
            set { Cache.CacheLocation = value; }
        }

        /// <summary>
        /// <see cref="CacheSizeLimit"/> is the cache file size in bytes for downloaded
        /// pictures. The default is 50MB (or 50 * 1024 * 1025 in bytes).
        /// </summary>
        public static long CacheSizeLimit
        {
            get { return Cache.CacheSizeLimit; }
            set { Cache.CacheSizeLimit = value; }
        }

        /// <summary>
        /// The default service to use for new Flickr instances
        /// </summary>
        public static SupportedService DefaultService
        {
            get
            {
#if !(MONOTOUCH || WindowsCE || SILVERLIGHT)
                if (!isServiceSet && FlickrConfigurationManager.Settings != null)
                {
                    defaultService = FlickrConfigurationManager.Settings.Service;
                    isServiceSet = true;
                }
#endif
                return defaultService;
            }
            set
            {
                defaultService = value;
#if !(MONOTOUCH || WindowsCE || SILVERLIGHT)
                isServiceSet = true;
#endif
            }
        }

        /// <summary>
        /// The current service that the Flickr API is using.
        /// </summary>
        public SupportedService CurrentService
        {
            get
            {
                return service;
            }
            set
            {
                service = value;
#if !(MONOTOUCH || WindowsCE || SILVERLIGHT)
                ServicePointManager.Expect100Continue &= service != SupportedService.Zooomr;
#endif
            }
        }

        /// <summary>
        /// Internal timeout for all web requests in milliseconds. Defaults to 30 seconds.
        /// </summary>
        public int HttpTimeout
        {
            get { return timeout; }
            set { timeout = value; }
        }

        /// <summary>
        /// Checks to see if a shared secret and an api token are stored in the object.
        /// Does not check if these values are valid values.
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return sharedSecret != null && apiToken != null;
            }
        }

        /// <summary>
        /// Returns the raw XML returned from the last response.
        /// Only set it the response was not returned from cache.
        /// </summary>
        public string LastResponse
        {
            get { return lastResponse; }
        }

        /// <summary>
        /// Returns the last URL requested. Includes API signing.
        /// </summary>
        public string LastRequest
        {
            get { return lastRequest; }
        }

#if !SILVERLIGHT
        /// <summary>
        /// You can set the <see cref="WebProxy"/> or alter its properties.
        /// It defaults to your internet explorer proxy settings.
        /// </summary>

        public WebProxy Proxy { get { return proxy; } set { proxy = value; } }
#endif

        /// <summary>
        /// Clears the cache completely.
        /// </summary>
        public static void FlushCache()
        {
            Cache.FlushCache();
        }

        /// <summary>
        /// Clears the cache for a particular URL.
        /// </summary>
        /// <param name="url">The URL to remove from the cache.</param>
        /// <remarks>
        /// The URL can either be an image URL for a downloaded picture, or
        /// a request URL (see <see cref="LastRequest"/> for getting the last URL).
        /// </remarks>
        public static void FlushCache(string url)
        {
            FlushCache(new Uri(url));
        }

        /// <summary>
        /// Clears the cache for a particular URL.
        /// </summary>
        /// <param name="url">The URL to remove from the cache.</param>
        /// <remarks>
        /// The URL can either be an image URL for a downloaded picture, or
        /// a request URL (see <see cref="LastRequest"/> for getting the last URL).
        /// </remarks>
        public static void FlushCache(Uri url)
        {
            Cache.FlushCache(url);
        }

        /// <summary>
        /// Constructor loads configuration settings from app.config or web.config file if they exist.
        /// </summary>
        public Flickr()
        {
            InstanceCacheDisabled = CacheDisabled;
            CurrentService = DefaultService;

#if !(MONOTOUCH || WindowsCE || SILVERLIGHT)

            var settings = FlickrConfigurationManager.Settings;
            if (settings == null) return;

            if (settings.CacheSize != 0) CacheSizeLimit = settings.CacheSize;
            if (settings.CacheTimeout != TimeSpan.MinValue) CacheTimeout = settings.CacheTimeout;
            ApiKey = settings.ApiKey;
            AuthToken = settings.ApiToken;
            ApiSecret = settings.SharedSecret;

            if (!settings.IsProxyDefined) return;

            Proxy = new WebProxy {Address = new Uri("http://" + settings.ProxyIPAddress + ":" + settings.ProxyPort)};

            if (string.IsNullOrEmpty(settings.ProxyUsername)) return;

            var creds = new NetworkCredential
                        {
                            UserName = settings.ProxyUsername,
                            Password = settings.ProxyPassword,
                            Domain = settings.ProxyDomain
                        };
            Proxy.Credentials = creds;

#endif
        }

        /// <summary>
        /// Create a new instance of the <see cref="Flickr"/> class with no authentication.
        /// </summary>
        /// <param name="apiKey">Your Flickr API Key.</param>
        public Flickr(string apiKey)
            : this(apiKey, null, null)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Flickr"/> class with an API key and a Shared Secret.
        /// This is only useful really useful for calling the Auth functions as all other
        /// authenticationed methods also require the API Token.
        /// </summary>
        /// <param name="apiKey">Your Flickr API Key.</param>
        /// <param name="sharedSecret">Your Flickr Shared Secret.</param>
        public Flickr(string apiKey, string sharedSecret)
            : this(apiKey, sharedSecret, null)
        {
        }

        /// <summary>
        /// Create a new instance of the <see cref="Flickr"/> class with the email address and password given
        /// </summary>
        /// <param name="apiKey">Your Flickr API Key</param>
        /// <param name="sharedSecret">Your FLickr Shared Secret.</param>
        /// <param name="token">The token for the user who has been authenticated.</param>
        public Flickr(string apiKey, string sharedSecret, string token)
            : this()
        {
            ApiKey = apiKey;
            ApiSecret = sharedSecret;
            AuthToken = token;
        }

        internal void CheckApiKey()
        {
            if (string.IsNullOrEmpty(ApiKey))
                throw new ApiKeyRequiredException();
        }

        internal void CheckSigned()
        {
            CheckApiKey();

            if (string.IsNullOrEmpty(ApiSecret))
                throw new SignatureRequiredException();
        }

        internal void CheckRequiresAuthentication()
        {
            CheckSigned();

            if (!string.IsNullOrEmpty(OAuthAccessToken) && !string.IsNullOrEmpty(OAuthAccessTokenSecret))
            {
                return;
            }

            if (!string.IsNullOrEmpty(AuthToken))
            {
                return;
            }

            throw new AuthenticationRequiredException();
        }

        /// <summary>
        /// Calculates the Flickr method cal URL based on the passed in parameters, and also generates the signature if required.
        /// </summary>
        /// <param name="parameters">A Dictionary containing a list of parameters to add to the method call.</param>
        /// <param name="includeSignature">Boolean use to decide whether to generate the api call signature as well.</param>
        /// <returns>The <see cref="Uri"/> for the method call.</returns>
        public string CalculateUri(Dictionary<string, string> parameters, bool includeSignature)
        {
            if (includeSignature)
            {
                string signature = CalculateAuthSignature(parameters);
                parameters.Add("api_sig", signature);
            }

            var url = new StringBuilder();
            url.Append("?");
            foreach (KeyValuePair<string, string> pair in parameters)
            {
                var escapedValue = UtilityMethods.EscapeDataString(pair.Value ?? "");
                url.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "{0}={1}&", pair.Key, escapedValue);
            }

            return BaseUri.AbsoluteUri + url.ToString();
        }

        private string CalculateAuthSignature(Dictionary<string, string> parameters)
        {
#if !SILVERLIGHT
            var sorted = new SortedList<string, string>();
            foreach (var pair in parameters) { sorted.Add(pair.Key, pair.Value); }
#else
            var sorted = parameters.OrderBy(p => p.Key);
#endif

            var sb = new StringBuilder(ApiSecret);
            foreach (var pair in sorted)
            {
                sb.Append(pair.Key);
                sb.Append(pair.Value);
            }
            return UtilityMethods.MD5Hash(sb.ToString());
        }

        private static Stream ConvertNonSeekableStreamToByteArray(Stream nonSeekableStream)
        {
            if (nonSeekableStream.CanSeek)
            {
                nonSeekableStream.Position = 0;
                return nonSeekableStream;
            }

            return nonSeekableStream;

            //var ms = new MemoryStream();
            //var buffer = new byte[1024];
            //int bytes;
            //while ((bytes = nonSeekableStream.Read(buffer, 0, buffer.Length)) > 0)
            //{
            //    ms.Write(buffer, 0, bytes);
            //}
            //ms.Position = 0;
            //return ms;
        }

        private StreamCollection CreateUploadData(Stream imageStream, string fileName, Dictionary<string, string> parameters, string boundary)
        {
            var oAuth = parameters.ContainsKey("oauth_consumer_key");

            var keys = new string[parameters.Keys.Count];
            parameters.Keys.CopyTo(keys, 0);
            Array.Sort(keys);

            var hashStringBuilder = new StringBuilder(sharedSecret, 2 * 1024);
            var ms1 = new MemoryStream();
            var contentStringBuilder = new StreamWriter(ms1, new UTF8Encoding(false));

            foreach (var key in keys)
            {

#if !SILVERLIGHT
                // Silverlight < 5 doesn't support modification of the Authorization header, so all data must be sent in post body.
                if (key.StartsWith("oauth", StringComparison.Ordinal)) continue;
#endif
                hashStringBuilder.Append(key);
                hashStringBuilder.Append(parameters[key]);
                contentStringBuilder.Write("--" + boundary + "\r\n");
                contentStringBuilder.Write("Content-Disposition: form-data; name=\"" + key + "\"\r\n");
                contentStringBuilder.Write("\r\n");
                contentStringBuilder.Write(parameters[key] + "\r\n");
            }

            if (!oAuth)
            {
                contentStringBuilder.Write("--" + boundary + "\r\n");
                contentStringBuilder.Write("Content-Disposition: form-data; name=\"api_sig\"\r\n");
                contentStringBuilder.Write("\r\n");
                contentStringBuilder.Write(UtilityMethods.MD5Hash(hashStringBuilder.ToString()) + "\r\n");
            }

            // Photo
            contentStringBuilder.Write("--" + boundary + "\r\n");
            contentStringBuilder.Write("Content-Disposition: form-data; name=\"photo\"; filename=\"" + Path.GetFileName(fileName) + "\"\r\n");
            contentStringBuilder.Write("Content-Type: image/jpeg\r\n");
            contentStringBuilder.Write("\r\n");

            contentStringBuilder.Flush();

            var photoContents = ConvertNonSeekableStreamToByteArray(imageStream);

            var ms2 = new MemoryStream();
            var postFooterWriter = new StreamWriter(ms2, new UTF8Encoding(false));
            postFooterWriter.Write("\r\n--" + boundary + "--\r\n");
            postFooterWriter.Flush();

            var collection = new StreamCollection(new[] { ms1, photoContents, ms2 });

            return collection;
        }

        internal class StreamCollection : IDisposable
        {
            public List<Stream> Streams { get; private set; }

            public StreamCollection(IEnumerable<Stream> streams)
            {
                Streams = new List<Stream>(streams);
            }

            public void ResetPosition()
            {
                Streams.ForEach(s => { if (s.CanSeek) { s.Position = 0; } });
            }

            public long? Length
            {
                get
                {
                    long l = 0;
                    foreach (var s in Streams)
                    {
                        if (!s.CanSeek) return null;

                        l += s.Length;
                    }
                    return l;
                }
            }

            public EventHandler<UploadProgressEventArgs> UploadProgress;

            public void CopyTo(Stream stream, int bufferSize = 1024*16)
            {
                ResetPosition();

                var buffer = new byte[bufferSize];
                var l = Length;
                var soFar = 0;

                foreach (var s in Streams)
                {
                    int read;
                    while(0 < (read = s.Read(buffer, 0, buffer.Length)))
                    {
                        soFar += read;
                        stream.Write(buffer, 0, read);
                        if( UploadProgress != null)
                            UploadProgress(this, new UploadProgressEventArgs { BytesSent = soFar, TotalBytesToSend = l.GetValueOrDefault(-1) });
                    }
                    stream.Flush();
                }
                stream.Flush();
            }

            public void Dispose()
            {
                Streams.ForEach(s =>
                                    {
                                        if( s != null)
                                            s.Dispose();
                                    });
            }
        }

    }

    internal static class FlickrExtensions
    {
        public static void Load(this IFlickrParsable item, string originalXml)
        {
            try
            {
                var reader = XmlReader.Create(new StringReader(originalXml), new XmlReaderSettings
                {
                    IgnoreWhitespace = true
                });

                if (!reader.ReadToDescendant("rsp"))
                {
                    throw new Exception("Unable to find response element 'rsp' in Flickr response");
                }
                while (reader.MoveToNextAttribute())
                {
                    if (reader.LocalName == "stat" && reader.Value == "fail")
                        throw ExceptionHandler.CreateResponseException(reader);
                }

                reader.MoveToElement();
                reader.Read();

                item.Load(reader);
            }
            catch (XmlException)
            {
                var newReader = XmlReader.Create(new StringReader(SanitizeXmlString(originalXml)), new XmlReaderSettings
                {
                    IgnoreWhitespace = true
                });

                if (!newReader.ReadToDescendant("rsp"))
                {
                    throw new Exception("Unable to find response element 'rsp' in Flickr response");
                }

                while (newReader.MoveToNextAttribute())
                {
                    if (newReader.LocalName == "stat" && newReader.Value == "fail")
                        throw ExceptionHandler.CreateResponseException(newReader);
                }

                newReader.MoveToElement();
                newReader.Read();

                item.Load(newReader);
            }
        }

        private static string SanitizeXmlString(string xml)
        {
            if (xml == null)
            {
                throw new ArgumentNullException("xml");
            }

            var buffer = new System.Text.StringBuilder(xml.Length);

            foreach (char c in xml)
            {
                if (IsLegalXmlChar(c))
                {
                    buffer.Append(c);
                }
            }

            return buffer.ToString();
        }

        /// <summary>
        /// Whether a given character is allowed by XML 1.0.
        /// </summary>
        private static bool IsLegalXmlChar(int character)
        {
            return
            (
                 character == 0x9 /* == '\t' == 9   */          ||
                 character == 0xA /* == '\n' == 10  */          ||
                 character == 0xD /* == '\r' == 13  */          ||
                (character >= 0x20 && character <= 0xD7FF) ||
                (character >= 0xE000 && character <= 0xFFFD) ||
                (character >= 0x10000 && character <= 0x10FFFF)
            );
        }
    }
}
