using System;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.Threading;
using System.Collections.Generic;

namespace FlickrNet
{
	/// <summary>
	/// The main Flickr class.
	/// </summary>
	/// <remarks>
	/// Create an instance of this class and then call its methods to perform methods on Flickr.
	/// </remarks>
	/// <example>
	/// <code>FlickrNet.Flickr flickr = new FlickrNet.Flickr();
	/// User user = flickr.PeopleFindByEmail("cal@iamcal.com");
	/// Console.WriteLine("User Id is " + u.UserId);</code>
	/// </example>
	//[System.Net.WebPermission(System.Security.Permissions.SecurityAction.Demand, ConnectPattern="http://www.flickr.com/.*")]
	public partial class Flickr
	{

		#region [ Upload Event and Delegate ]
		/// <summary>
		/// 
		/// </summary>
		public delegate void UploadProgressEventHandler(object sender, UploadProgressEventArgs e);

		/// <summary>
		/// UploadProgressHandler is fired during a synchronous upload process to signify that 
		/// a segment of uploading has been completed. This is approximately 50 bytes. The total
		/// uploaded is recorded in the <see cref="UploadProgressEventArgs"/> class.
		/// </summary>
		public event UploadProgressEventHandler OnUploadProgress;
		#endregion

		#region [ Private Variables ]
#if !WindowsCE
		private static bool _isServiceSet;
#endif
		private static SupportedService _defaultService = SupportedService.Flickr;

		private SupportedService _service = SupportedService.Flickr;

		private string BaseUrl
		{
			get { return _baseUrl[(int)_service]; }
		}

		private string[] _baseUrl = new string[] { 
													 "http://api.flickr.com/services/rest/", 
													 "http://beta.zooomr.com/bluenote/api/rest",
													 "http://www.23hq.com/services/rest/"};

		private string UploadUrl
		{
			get { return _uploadUrl[(int)_service]; }
		}
		private static string[] _uploadUrl = new string[] {
															  "http://api.flickr.com/services/upload/",
															  "http://beta.zooomr.com/bluenote/api/upload",
															  "http://www.23hq.com/services/upload/"};

		private string ReplaceUrl
		{
			get { return _replaceUrl[(int)_service]; }
		}
		private static string[] _replaceUrl = new string[] {
															   "http://api.flickr.com/services/replace/",
															   "http://beta.zooomr.com/bluenote/api/replace",
															   "http://www.23hq.com/services/replace/"};

		private string AuthUrl
		{
			get { return _authUrl[(int)_service]; }
		}
		private static string[] _authUrl = new string[] {
															"http://www.flickr.com/services/auth/",
															"http://beta.zooomr.com/auth/",
															"http://www.23hq.com/services/auth/"};

		private string _apiKey;
		private string _apiToken;
		private string _sharedSecret;
		private int _timeout = 30000;
		private const string UserAgent = "Mozilla/4.0 FlickrNet API (compatible; MSIE 6.0; Windows NT 5.1)";
		private string _lastRequest;
		private string _lastResponse;

		private WebProxy _proxy;// = WebProxy.GetDefaultProxy();

		// Static serializers
		private static XmlSerializer _uploaderSerializer = new XmlSerializer(typeof(FlickrNet.UploadResponse));

		#endregion

		#region [ Public Properties ]
		/// <summary>
		/// Get or set the API Key to be used by all calls. API key is mandatory for all 
		/// calls to Flickr.
		/// </summary>
		public string ApiKey 
		{ 
			get { return _apiKey; } 
			set { _apiKey = (value==null||value.Length==0?null:value); }
		}

		/// <summary>
		/// API shared secret is required for all calls that require signing, which includes
		/// all methods that require authentication, as well as the actual flickr.auth.* calls.
		/// </summary>
		public string ApiSecret
		{
			get { return _sharedSecret; }
			set { _sharedSecret = (value==null||value.Length==0?null:value); }
		}

		/// <summary>
		/// The API token is required for all calls that require authentication.
		/// A <see cref="FlickrApiException"/> will be raised by Flickr if the API token is
		/// not set when required.
		/// </summary>
		/// <remarks>
		/// It should be noted that some methods will work without the API token, but
		/// will return different results if used with them (such as group pool requests, 
		/// and results which include private pictures the authenticated user is allowed to see
		/// (their own, or others).
		/// </remarks>
		[Obsolete("Renamed to AuthToken to be more consistent with the Flickr API")]
		public string ApiToken 
		{
			get { return _apiToken; }
			set { _apiToken = (value==null||value.Length==0?null:value); }
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
		public string AuthToken 
		{
			get { return _apiToken; }
			set { _apiToken = (value==null||value.Length==0?null:value); }
		}

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
		/// Gets the current size of the Cache.
		/// </summary>
		public static long CacheSize
		{
			get { return Cache.CacheSize; }
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
#if !WindowsCE
				if( !_isServiceSet && FlickrConfigurationManager.Settings != null )
				{
					_defaultService = FlickrConfigurationManager.Settings.Service;
					_isServiceSet = true;
				}
#endif
				return _defaultService;
			}
			set
			{
				_defaultService = value;
#if !WindowsCE
				_isServiceSet = true;
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
				return _service; 
			}
			set 
			{
				_service = value; 
#if !WindowsCE
				if( _service == SupportedService.Zooomr ) ServicePointManager.Expect100Continue = false;
#endif
			}
		}

		/// <summary>
		/// Internal timeout for all web requests in milliseconds. Defaults to 30 seconds.
		/// </summary>
		public int HttpTimeout
		{
			get { return _timeout; } 
			set { _timeout = value; }
		}

		/// <summary>
		/// Checks to see if a shared secret and an api token are stored in the object.
		/// Does not check if these values are valid values.
		/// </summary>
		public bool IsAuthenticated
		{
			get { return (_sharedSecret != null && _apiToken != null); }
		}

		/// <summary>
		/// Returns the raw XML returned from the last response.
		/// Only set it the response was not returned from cache.
		/// </summary>
		public string LastResponse
		{
			get { return _lastResponse; }
		}

		/// <summary>
		/// Returns the last URL requested. Includes API signing.
		/// </summary>
		public string LastRequest
		{
			get { return _lastRequest; }
		}

		/// <summary>
		/// You can set the <see cref="WebProxy"/> or alter its properties.
		/// It defaults to your internet explorer proxy settings.
		/// </summary>
		public WebProxy Proxy { get { return _proxy; } set { _proxy = value; } }
		#endregion

		#region [ Cache Methods ]
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
			Cache.FlushCache(url);
		}

		/// <summary>
		/// Provides static access to the list of cached photos.
		/// </summary>
		/// <returns>An array of <see cref="PictureCacheItem"/> objects.</returns>
		public static PictureCacheItem[] GetCachePictures()
		{
			return (PictureCacheItem[]) Cache.Downloads.ToArray(typeof(PictureCacheItem));
		}
		#endregion

		#region [ Constructors ]

		/// <summary>
		/// Constructor loads configuration settings from app.config or web.config file if they exist.
		/// </summary>
		public Flickr()
		{
#if !WindowsCE
			FlickrConfigurationSettings settings = FlickrConfigurationManager.Settings;
			if( settings == null ) return;

			if( settings.CacheSize != 0 ) CacheSizeLimit = settings.CacheSize;
			if( settings.CacheTimeout != TimeSpan.MinValue ) CacheTimeout = settings.CacheTimeout;
			ApiKey = settings.ApiKey;
			AuthToken = settings.ApiToken;
			ApiSecret = settings.SharedSecret;

			if (settings.IsProxyDefined)
			{
				Proxy = new WebProxy();
				Proxy.Address = new Uri("http://" + settings.ProxyIPAddress + ":" + settings.ProxyPort);
				if( settings.ProxyUsername != null && settings.ProxyUsername.Length > 0 )
				{
					NetworkCredential creds = new NetworkCredential();
					creds.UserName = settings.ProxyUsername;
					creds.Password = settings.ProxyPassword;
					creds.Domain = settings.ProxyDomain;
					Proxy.Credentials = creds;
				}
			}
#endif

			CurrentService = DefaultService;
		}

		/// <summary>
		/// Create a new instance of the <see cref="Flickr"/> class with no authentication.
		/// </summary>
		/// <param name="apiKey">Your Flickr API Key.</param>
		public Flickr(string apiKey) : this(apiKey, null, null)
		{
		}

		/// <summary>
		/// Creates a new instance of the <see cref="Flickr"/> class with an API key and a Shared Secret.
		/// This is only useful really useful for calling the Auth functions as all other
		/// authenticationed methods also require the API Token.
		/// </summary>
		/// <param name="apiKey">Your Flickr API Key.</param>
		/// <param name="sharedSecret">Your Flickr Shared Secret.</param>
		public Flickr(string apiKey, string sharedSecret) : this(apiKey, sharedSecret, null)
		{
		}

		/// <summary>
		/// Create a new instance of the <see cref="Flickr"/> class with the email address and password given
		/// </summary>
		/// <param name="apiKey">Your Flickr API Key</param>
		/// <param name="sharedSecret">Your FLickr Shared Secret.</param>
		/// <param name="token">The token for the user who has been authenticated.</param>
		public Flickr(string apiKey, string sharedSecret, string token) : this()
		{
			_apiKey = apiKey;
			_sharedSecret = sharedSecret;
			_apiToken = token;
		}
		#endregion

		#region [ Private Methods ]

		private void CheckApiKey()
		{
			if( ApiKey == null || ApiKey.Length == 0 )
				throw new ApiKeyRequiredException();
		}

		private void CheckSigned()
		{
			CheckApiKey();

			if( ApiSecret == null || ApiSecret.Length == 0 )
				throw new SignatureRequiredException();
		}

		private void CheckRequiresAuthentication()
		{
			CheckApiKey();

			if( ApiSecret == null || ApiSecret.Length == 0 )
				throw new SignatureRequiredException();
			if( AuthToken == null || AuthToken.Length == 0 )
				throw new AuthenticationRequiredException();

		}

        private static readonly Uri baseUri = new Uri("http://api.flickr.com/services/rest/");

        /// <summary>
        /// The base URL for all Flickr REST method calls.
        /// </summary>
        public static Uri BaseUri
        {
            get { return baseUri; }
        }

        /// <summary>
        /// Calculates the Flickr method cal URL based on the passed in parameters, and also generates the signature if required.
        /// </summary>
        /// <param name="parameters">A Dictionary containing a list of parameters to add to the method call.</param>
        /// <param name="includeSignature">Boolean use to decide whether to generate the api call signature as well.</param>
        /// <returns>The <see cref="Uri"/> for the method call.</returns>
        public Uri CalculateUri(Dictionary<string, string> parameters, bool includeSignature)
        {
            if (includeSignature)
            {
                SortedDictionary<string, string> sorted = new SortedDictionary<string, string>();
                foreach (KeyValuePair<string, string> pair in parameters) { sorted.Add(pair.Key, pair.Value); }

                StringBuilder sb = new StringBuilder(ApiSecret);
                foreach (KeyValuePair<string, string> pair in sorted) { sb.Append(pair.Key); sb.Append(pair.Value); }

                parameters.Add("api_sig", UtilityMethods.MD5Hash(sb.ToString()));
            }

            StringBuilder url = new StringBuilder();
            url.Append("?");
            foreach (KeyValuePair<string, string> pair in parameters)
            {
                url.AppendFormat("{0}={1}&", pair.Key, Uri.EscapeDataString(pair.Value));
            }

            return new Uri(BaseUri, url.ToString());
        }


		#endregion

    }
}
