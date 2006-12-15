using System;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

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
	public class Flickr
	{

		#region [ Upload Event and Delegate ]
		/// <summary>
		/// 
		/// </summary>
		public delegate void UploadProgressHandler(object sender, UploadProgressEventArgs e);

		/// <summary>
		/// UploadProgressHandler is fired during a synchronous upload process to signify that 
		/// a segment of uploading has been completed. This is approximately 50 bytes. The total
		/// uploaded is recorded in the <see cref="UploadProgressEventArgs"/> class.
		/// </summary>
		public event UploadProgressHandler OnUploadProgress;
		#endregion

		#region [ Private Variables ]
#if !WindowsCE
		private static bool _isServiceSet = false;
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
		private static XmlSerializer _responseSerializer = new XmlSerializer(typeof(FlickrNet.Response));
		private static XmlSerializer _uploaderSerializer = new XmlSerializer(typeof(FlickrNet.Uploader));

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
		/// A <see cref="FlickrException"/> will be raised by Flickr if the API token is
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
		/// A <see cref="FlickrException"/> will be raised by Flickr if the authentication token is
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
			get { return _service; }
			set { _service = value; }
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

		/// <summary>
		/// Check a url for existance in the cache
		/// </summary>
		/// <param name="url"></param>
		/// <returns>true if in cache</returns>
		public static bool IsUrlCached(string url)
		{
			return (Cache.Downloads[url] != null);
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
            CurrentService = DefaultService;

			if( settings.IsProxyDefined )
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
			else
			{
				// try and get default IE settings
				try
				{
					Proxy = WebProxy.GetDefaultProxy();
				}
				catch(System.Security.SecurityException)
				{
					// Capture SecurityException for when running in a Medium Trust environment.
				}
			}
#else
            CurrentService = DefaultService;
#endif
		}

		/// <summary>
		/// Create a new instance of the <see cref="Flickr"/> class with no authentication.
		/// </summary>
		/// <param name="apiKey">Your Flickr API Key.</param>
		public Flickr(string apiKey) : this(apiKey, "", "")
		{
		}

		/// <summary>
		/// Creates a new instance of the <see cref="Flickr"/> class with an API key and a Shared Secret.
		/// This is only useful really useful for calling the Auth functions as all other
		/// authenticationed methods also require the API Token.
		/// </summary>
		/// <param name="apiKey">Your Flickr API Key.</param>
		/// <param name="sharedSecret">Your Flickr Shared Secret.</param>
		public Flickr(string apiKey, string sharedSecret) : this(apiKey, sharedSecret, "")
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
		/// <summary>
		/// A private method which performs the actual HTTP web request if
		/// the details are not found within the cache.
		/// </summary>
		/// <param name="url">The URL to download.</param>
		/// <returns>A <see cref="FlickrNet.Response"/> object.</returns>
		private string DoGetResponse(string url)
		{
			HttpWebRequest req = null;
			HttpWebResponse res = null;

			// Initialise the web request
			req = (HttpWebRequest)HttpWebRequest.Create(url);
			req.Method = CurrentService==SupportedService.Zooomr?"GET":"POST";
			if( req.Method == "POST" ) req.ContentLength = 0;
			//req.Method = "POST";
			req.UserAgent = UserAgent;
			if( Proxy != null ) req.Proxy = Proxy;
			req.Timeout = HttpTimeout;
			req.KeepAlive = false;

			try
			{
				// Get response from the internet
				res = (HttpWebResponse)req.GetResponse();
			}
			catch(WebException ex)
			{
				if( ex.Status == WebExceptionStatus.ProtocolError )
				{
					HttpWebResponse res2 = (HttpWebResponse)ex.Response;
					if( res2 != null )
					{
						throw new FlickrException((int)res2.StatusCode, res2.StatusDescription);
					}
				}
				throw new FlickrException(9999, ex.Message);
			}

			string responseString = string.Empty;

			using (StreamReader sr = new StreamReader(res.GetResponseStream()))
			{
				responseString = sr.ReadToEnd();
			}

			return responseString;
		}

		/// <summary>
		/// Download a picture (or anything else actually).
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		private Stream DoDownloadPicture(string url)
		{
			HttpWebRequest req = null;
			HttpWebResponse res = null;

			try
			{
				req = (HttpWebRequest)HttpWebRequest.Create(url);
				req.UserAgent = UserAgent;
				if( Proxy != null ) req.Proxy = Proxy;
				req.Timeout = HttpTimeout;
				req.KeepAlive = false;
				res = (HttpWebResponse)req.GetResponse();
			}
			catch(WebException ex)
			{
				if( ex.Status == WebExceptionStatus.ProtocolError )
				{
					HttpWebResponse res2 = (HttpWebResponse)ex.Response;
					if( res2 != null )
					{
						throw new FlickrException((int)res2.StatusCode, res2.StatusDescription);
					}
				}
				else if( ex.Status == WebExceptionStatus.Timeout )
				{
					throw new FlickrException(301, "Request time-out");
				}
				throw new FlickrException(9999, "Picture download failed (" + ex.Message + ")");
			}

			return res.GetResponseStream();
		}
		#endregion

		#region [ GetResponse methods ]
		private Response GetResponseNoCache(Hashtable parameters)
		{
			return GetResponse(parameters, TimeSpan.MinValue);
		}

		private Response GetResponseAlwaysCache(Hashtable parameters)
		{
			return GetResponse(parameters, TimeSpan.MaxValue);
		}

		private Response GetResponseCache(Hashtable parameters)
		{
			return GetResponse(parameters, Cache.CacheTimeout);
		}

		private Response GetResponse(Hashtable parameters, TimeSpan cacheTimeout)
		{
			// Calulate URL 
            StringBuilder UrlStringBuilder = new StringBuilder(BaseUrl, 2 * 1024);
            StringBuilder HashStringBuilder = new StringBuilder(_sharedSecret, 2 * 1024);

            UrlStringBuilder.Append("?");

			parameters["api_key"] = _apiKey;

			if( _apiToken != null && _apiToken.Length > 0 )
			{
				parameters["auth_token"] = _apiToken;
			}

			string[] keys = new string[parameters.Keys.Count];
			parameters.Keys.CopyTo(keys, 0);
			Array.Sort(keys);

			foreach(string key in keys)
			{
				if( UrlStringBuilder.Length > BaseUrl.Length + 1 ) UrlStringBuilder.Append("&");
                UrlStringBuilder.Append(key);
                UrlStringBuilder.Append("=");
                UrlStringBuilder.Append(Utils.UrlEncode((string)parameters[key]));
                HashStringBuilder.Append(key);
                HashStringBuilder.Append(parameters[key]);
			}

            if (_sharedSecret != null && _sharedSecret.Length > 0) 
            {
                if (UrlStringBuilder.Length > BaseUrl.Length + 1)
                {
                    UrlStringBuilder.Append("&");
                }
                UrlStringBuilder.Append("api_sig=");
                UrlStringBuilder.Append(Md5Hash(HashStringBuilder.ToString()));
            }

			string url = UrlStringBuilder.ToString();
			_lastRequest = url;
			_lastResponse = string.Empty;

			if( CacheDisabled )
			{
				string responseXml = DoGetResponse(url);
				_lastResponse = responseXml;
				return Deserialize(responseXml);
			}
			else
			{
				ResponseCacheItem cached = (ResponseCacheItem) Cache.Responses.Get(url, cacheTimeout, true);
				if (cached != null)
				{
					System.Diagnostics.Debug.WriteLine("Cache hit");
					_lastResponse = cached.Response;
					return Deserialize(cached.Response);
				}
				else
				{
					System.Diagnostics.Debug.WriteLine("Cache miss");
					string responseXml = DoGetResponse(url);
					_lastResponse = responseXml;

					ResponseCacheItem resCache = new ResponseCacheItem();
					resCache.Response = responseXml;
					resCache.Url = url;
					resCache.CreationTime = DateTime.UtcNow;

					FlickrNet.Response response = Deserialize(responseXml);

					if( response.Status == ResponseStatus.OK )
					{
						Cache.Responses.Shrink(Math.Max(0, Cache.CacheSizeLimit - responseXml.Length));
						Cache.Responses[url] = resCache;
					}

					return response;
				}
			}
		}

		/// <summary>
		/// Converts the response string (in XML) into the <see cref="Response"/> object.
		/// </summary>
		/// <param name="responseString">The response from Flickr.</param>
		/// <returns>A <see cref="Response"/> object containing the details of the </returns>
		private static Response Deserialize(string responseString)
		{
			XmlSerializer serializer = _responseSerializer;
			try
			{
				// Deserialise the web response into the Flickr response object
				StringReader responseReader = new StringReader(responseString);
				FlickrNet.Response response = (FlickrNet.Response)serializer.Deserialize(responseReader);
				responseReader.Close();

				return response;
			}
			catch(InvalidOperationException ex)
			{
				// Serialization error occurred!
				throw new FlickrException(9998, "Invalid response received (" + ex.Message + ")");
			}
		}

		#endregion

		#region [ DownloadPicture ]
		/// <summary>
		/// Downloads the picture from a internet and transfers it to a stream object.
		/// </summary>
		/// <param name="url">The url of the image to download.</param>
		/// <returns>A <see cref="Stream"/> object containing the downloaded picture.</returns>
		/// <remarks>
		/// The method checks the download cache first to see if the picture has already 
		/// been downloaded and if so returns the cached image. Otherwise it goes to the internet for the actual 
		/// image.
		/// </remarks>
		public System.IO.Stream DownloadPicture(string url)
		{
			const int BUFFER_SIZE = 1024 * 10;

			PictureCacheItem cacheItem = (PictureCacheItem) Cache.Downloads[url];
			if (cacheItem != null)
			{
				return  new FileStream(cacheItem.filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			}

			PictureCacheItem picCache = new PictureCacheItem();
			picCache.filename = Path.Combine(Cache.CacheLocation,Guid.NewGuid().ToString());
			Stream read = DoDownloadPicture(url);
			Stream write = new FileStream(picCache.filename, FileMode.Create, FileAccess.Write, FileShare.None);

			byte[] buffer = new byte[BUFFER_SIZE];
			int bytes = 0;
			long fileSize = 0;

			while( (bytes = read.Read(buffer, 0, BUFFER_SIZE)) > 0 )
			{
				fileSize += bytes;
				write.Write(buffer, 0, bytes);
			}

			read.Close();
			write.Close();

			picCache.url = url;
			picCache.creationTime = DateTime.UtcNow;
			picCache.fileSize = fileSize;

			Cache.Downloads.Shrink(Math.Max(0, Cache.CacheSizeLimit - fileSize));
			Cache.Downloads[url] = picCache;

			return new FileStream(picCache.filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		}
		#endregion

		#region [ Auth ]
		/// <summary>
		/// Retrieve a temporary FROB from the Flickr service, to be used in redirecting the
		/// user to the Flickr web site for authentication. Only required for desktop authentication.
		/// </summary>
		/// <remarks>
		/// Pass the FROB to the <see cref="AuthCalcUrl"/> method to calculate the url.
		/// </remarks>
		/// <example>
		/// <code>
		/// string frob = flickr.AuthGetFrob();
		/// string url = flickr.AuthCalcUrl(frob, AuthLevel.Read);
		/// 
		/// // redirect the user to the url above and then wait till they have authenticated and return to the app.
		/// 
		/// Auth auth = flickr.AuthGetToken(frob);
		/// 
		/// // then store the auth.Token for later use.
		/// string token = auth.Token;
		/// </code>
		/// </example>
		/// <returns>The FROB.</returns>
		public string AuthGetFrob()
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.auth.getFrob");
			
			FlickrNet.Response response = GetResponseNoCache(parameters);
			if( response.Status == ResponseStatus.OK )
			{
				return response.AllElements[0].InnerText;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Calculates the URL to redirect the user to Flickr web site for
		/// authentication. Used by desktop application. 
		/// See <see cref="AuthGetFrob"/> for example code.
		/// </summary>
		/// <param name="frob">The FROB to be used for authentication.</param>
		/// <param name="authLevel">The <see cref="AuthLevel"/> stating the maximum authentication level your application requires.</param>
		/// <returns>The url to redirect the user to.</returns>
		public string AuthCalcUrl(string frob, AuthLevel authLevel)
		{
			if( _sharedSecret == null ) throw new FlickrException(0, "AuthGetToken requires signing. Please supply api key and secret.");

			string hash = _sharedSecret + "api_key" + _apiKey + "frob" + frob + "perms" + authLevel.ToString().ToLower();
			hash = Md5Hash(hash);
			string url = AuthUrl + "?api_key=" + _apiKey + "&perms=" + authLevel.ToString().ToLower() + "&frob=" + frob;
			url += "&api_sig=" + hash;

			return url;
		}

		/// <summary>
		/// Calculates the URL to redirect the user to Flickr web site for
		/// auehtntication. Used by Web applications. 
		/// See <see cref="AuthGetFrob"/> for example code.
		/// </summary>
		/// <remarks>
		/// The Flickr web site provides 'tiny urls' that can be used in place
		/// of this URL when you specify your return url in the API key page.
		/// It is recommended that you use these instead as they do not include
		/// your API or shared secret.
		/// </remarks>
		/// <param name="authLevel">The <see cref="AuthLevel"/> stating the maximum authentication level your application requires.</param>
		/// <returns>The url to redirect the user to.</returns>
		public string AuthCalcWebUrl(AuthLevel authLevel)
		{
			if( _sharedSecret == null ) throw new FlickrException(0, "AuthGetToken requires signing. Please supply api key and secret.");

			string hash = _sharedSecret + "api_key" + _apiKey + "perms" + authLevel.ToString().ToLower();
			hash = Md5Hash(hash);
			string url = AuthUrl + "?api_key=" + _apiKey + "&perms=" + authLevel.ToString().ToLower();
			url += "&api_sig=" + hash;

			return url;
		}

		/// <summary>
		/// After the user has authenticated your application on the flickr web site call this 
		/// method with the FROB (either stored from <see cref="AuthGetFrob"/> or returned in the URL
		/// from the Flickr web site) to get the users token.
		/// </summary>
		/// <param name="frob">The string containing the FROB.</param>
		/// <returns>A <see cref="Auth"/> object containing user and token details.</returns>
		public Auth AuthGetToken(string frob)
		{
			if( _sharedSecret == null ) throw new FlickrException(0, "AuthGetToken requires signing. Please supply api key and secret.");

			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.auth.getToken");
			parameters.Add("frob", frob);

			FlickrNet.Response response = GetResponseNoCache(parameters);
			if( response.Status == ResponseStatus.OK )
			{
				Auth auth = new Auth(response.AllElements[0]);
				return auth;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Gets the full token details for a given mini token, entered by the user following a 
		/// web based authentication.
		/// </summary>
		/// <param name="miniToken">The mini token.</param>
		/// <returns>An instance <see cref="Auth"/> class, detailing the user and their full token.</returns>
		public Auth AuthGetFullToken(string miniToken)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.auth.getFullToken");
			parameters.Add("mini_token", miniToken);
			FlickrNet.Response response = GetResponseNoCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				Auth auth = new Auth(response.AllElements[0]);
				return auth;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Checks a authentication token with the flickr service to make
		/// sure it is still valid.
		/// </summary>
		/// <param name="token">The authentication token to check.</param>
		/// <returns>The <see cref="Auth"/> object detailing the user for the token.</returns>
		public Auth AuthCheckToken(string token)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.auth.checkToken");
			parameters.Add("auth_token", token);

			FlickrNet.Response response = GetResponseNoCache(parameters);
			if( response.Status == ResponseStatus.OK )
			{
				Auth auth = new Auth(response.AllElements[0]);
				return auth;
			}
			else
			{
				throw new FlickrException(response.Error);
			}

		}
		#endregion

		#region [ UploadPicture ]
		/// <summary>
		/// Uploads a file to Flickr.
		/// </summary>
		/// <param name="filename">The filename of the file to open.</param>
		/// <returns>The id of the photo on a successful upload.</returns>
		/// <exception cref="FlickrException">Thrown when Flickr returns an error. see http://www.flickr.com/services/api/upload.api.html for more details.</exception>
		/// <remarks>Other exceptions may be thrown, see <see cref="FileStream"/> constructors for more details.</remarks>
		public string UploadPicture(string filename)
		{
			return UploadPicture(filename, null, null, null, true, false, false);
		}

		/// <summary>
		/// Uploads a file to Flickr.
		/// </summary>
		/// <param name="filename">The filename of the file to open.</param>
		/// <param name="title">The title of the photograph.</param>
		/// <returns>The id of the photo on a successful upload.</returns>
		/// <exception cref="FlickrException">Thrown when Flickr returns an error. see http://www.flickr.com/services/api/upload.api.html for more details.</exception>
		/// <remarks>Other exceptions may be thrown, see <see cref="FileStream"/> constructors for more details.</remarks>
		public string UploadPicture(string filename, string title)
		{
			return UploadPicture(filename, title, null, null, true, false, false);
		}

		/// <summary>
		/// Uploads a file to Flickr.
		/// </summary>
		/// <param name="filename">The filename of the file to open.</param>
		/// <param name="title">The title of the photograph.</param>
		/// <param name="description">The description of the photograph.</param>
		/// <returns>The id of the photo on a successful upload.</returns>
		/// <exception cref="FlickrException">Thrown when Flickr returns an error. see http://www.flickr.com/services/api/upload.api.html for more details.</exception>
		/// <remarks>Other exceptions may be thrown, see <see cref="FileStream"/> constructors for more details.</remarks>
		public string UploadPicture(string filename, string title, string description)
		{
			return UploadPicture(filename, title, description, null, true, false, false);
		}

		/// <summary>
		/// Uploads a file to Flickr.
		/// </summary>
		/// <param name="filename">The filename of the file to open.</param>
		/// <param name="title">The title of the photograph.</param>
		/// <param name="description">The description of the photograph.</param>
		/// <param name="tags">A comma seperated list of the tags to assign to the photograph.</param>
		/// <returns>The id of the photo on a successful upload.</returns>
		/// <exception cref="FlickrException">Thrown when Flickr returns an error. see http://www.flickr.com/services/api/upload.api.html for more details.</exception>
		/// <remarks>Other exceptions may be thrown, see <see cref="FileStream"/> constructors for more details.</remarks>
		public string UploadPicture(string filename, string title, string description, string tags)
		{
			Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			return UploadPicture(stream, title, description, tags, -1, -1, -1);
		}

		/// <summary>
		/// Uploads a file to Flickr.
		/// </summary>
		/// <param name="filename">The filename of the file to open.</param>
		/// <param name="title">The title of the photograph.</param>
		/// <param name="description">The description of the photograph.</param>
		/// <param name="tags">A comma seperated list of the tags to assign to the photograph.</param>
		/// <param name="isPublic">True if the photograph should be public and false if it should be private.</param>
		/// <param name="isFriend">True if the photograph should be marked as viewable by friends contacts.</param>
		/// <param name="isFamily">True if the photograph should be marked as viewable by family contacts.</param>
		/// <returns>The id of the photo on a successful upload.</returns>
		/// <exception cref="FlickrException">Thrown when Flickr returns an error. see http://www.flickr.com/services/api/upload.api.html for more details.</exception>
		/// <remarks>Other exceptions may be thrown, see <see cref="FileStream"/> constructors for more details.</remarks>
		public string UploadPicture(string filename, string title, string description, string tags, bool isPublic, bool isFamily, bool isFriend)
		{
			Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			return UploadPicture(stream, title, description, tags, isPublic?1:0, isFamily?1:0, isFriend?1:0);
		}

		/// <summary>
		/// UploadPicture method that does all the uploading work.
		/// </summary>
		/// <param name="stream">The <see cref="Stream"/> object containing the pphoto to be uploaded.</param>
		/// <param name="title">The title of the photo (optional).</param>
		/// <param name="description">The description of the photograph (optional).</param>
		/// <param name="tags">The tags for the photograph (optional).</param>
		/// <param name="isPublic">0 for private, 1 for public.</param>
		/// <param name="isFamily">1 if family, 0 is not.</param>
		/// <param name="isFriend">1 if friend, 0 if not.</param>
		/// <returns>The id of the photograph after successful uploading.</returns>
		public string UploadPicture(Stream stream, string title, string description, string tags, int isPublic, int isFamily, int isFriend)
		{
			/*
			 * 
			 * Modified UploadPicture code taken from the Flickr.Net library
			 * URL: http://workspaces.gotdotnet.com/flickrdotnet
			 * It is used under the terms of the Common Public License 1.0
			 * URL: http://www.opensource.org/licenses/cpl.php
			 * 
			 * */

			string boundary = "FLICKR_MIME_" + DateTime.Now.ToString("yyyyMMddhhmmss");

			HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(UploadUrl);
			req.UserAgent = "Mozilla/4.0 FlickrNet API (compatible; MSIE 6.0; Windows NT 5.1)";
			req.Method = "POST";
			if( Proxy != null ) req.Proxy = Proxy;
			//req.Referer = "http://www.flickr.com";
			req.KeepAlive = true;
			req.Timeout = HttpTimeout * 1000;
			req.ContentType = "multipart/form-data; boundary=" + boundary + "";
			req.Expect = "";

			StringBuilder sb = new StringBuilder();
            
			Hashtable parameters = new Hashtable();
		
			if( title != null && title.Length > 0 )
			{
				parameters.Add("title", title);
			}
			if( description != null && description.Length > 0 )
			{
				parameters.Add("description", description);
			}
			if( tags != null && tags.Length > 0 )
			{
				parameters.Add("tags", tags);
			}
			if( isPublic >= 0 )
			{
				parameters.Add("is_public", isPublic.ToString());
			}
			if( isFriend >= 0 )
			{
				parameters.Add("is_friend", isFriend.ToString());
			}
			if( isFamily >= 0 )
			{
				parameters.Add("is_family", isFamily.ToString());
			}

			parameters.Add("api_key", _apiKey);
			parameters.Add("auth_token", _apiToken);

			string[] keys = new string[parameters.Keys.Count];
			parameters.Keys.CopyTo(keys, 0);
			Array.Sort(keys);

			StringBuilder HashStringBuilder = new StringBuilder(_sharedSecret, 2 * 1024); 

			foreach(string key in keys)
			{
                HashStringBuilder.Append(key);
                HashStringBuilder.Append(parameters[key]);
				sb.Append("--" + boundary + "\r\n");
				sb.Append("Content-Disposition: form-data; name=\"" + key + "\"\r\n");
				sb.Append("\r\n");
				sb.Append(parameters[key] + "\r\n");
			}

			sb.Append("--" + boundary + "\r\n");
			sb.Append("Content-Disposition: form-data; name=\"api_sig\"\r\n");
			sb.Append("\r\n");
            sb.Append(Md5Hash(HashStringBuilder.ToString()) + "\r\n");

			// Photo
			sb.Append("--" + boundary + "\r\n");
			sb.Append("Content-Disposition: form-data; name=\"photo\"; filename=\"image.jpeg\"\r\n");
			sb.Append("Content-Type: image/jpeg\r\n");
			sb.Append("\r\n");

			UTF8Encoding encoding = new UTF8Encoding();

			byte[] postContents = encoding.GetBytes(sb.ToString());
			
			byte[] photoContents = new byte[stream.Length];
			stream.Read(photoContents, 0, photoContents.Length);
			stream.Close();

			byte[] postFooter = encoding.GetBytes("\r\n--" + boundary + "--\r\n");

			byte[] dataBuffer = new byte[postContents.Length + photoContents.Length + postFooter.Length];
			Buffer.BlockCopy(postContents, 0, dataBuffer, 0, postContents.Length);
			Buffer.BlockCopy(photoContents, 0, dataBuffer, postContents.Length, photoContents.Length);
			Buffer.BlockCopy(postFooter, 0, dataBuffer, postContents.Length + photoContents.Length, postFooter.Length);

			req.ContentLength = dataBuffer.Length;

			Stream resStream = req.GetRequestStream();

			int j = 1;
			int uploadBit = Math.Max(dataBuffer.Length / 100, 50*1024);
			int uploadSoFar = 0;

			for(int i = 0; i < dataBuffer.Length; i=i+uploadBit)
			{
				int toUpload = Math.Min(uploadBit, dataBuffer.Length - i);
				uploadSoFar += toUpload;

				resStream.Write(dataBuffer, i, toUpload);

				if( (OnUploadProgress != null) && ((j++) % 5 == 0 || uploadSoFar == dataBuffer.Length) )
				{
					OnUploadProgress(this, new UploadProgressEventArgs(i+toUpload, uploadSoFar == dataBuffer.Length));
				}
			}
			resStream.Close();

			HttpWebResponse res = (HttpWebResponse)req.GetResponse();

			XmlSerializer serializer = _uploaderSerializer;

			StreamReader sr = new StreamReader(res.GetResponseStream());
			string s= sr.ReadToEnd();
			sr.Close();

			StringReader str = new StringReader(s);

			FlickrNet.Uploader uploader = (FlickrNet.Uploader)serializer.Deserialize(str);
			
			if( uploader.Status == ResponseStatus.OK )
			{
				return uploader.PhotoId;
			}
			else
			{
				throw new FlickrException(uploader.Code, uploader.Message);
			}
		}

		/// <summary>
		/// Replace an existing photo on Flickr.
		/// </summary>
		/// <param name="filename">The filename of the photo to upload.</param>
		/// <param name="photoId">The ID of the photo to replace.</param>
		/// <returns>The id of the photograph after successful uploading.</returns>
		public string ReplacePicture(string filename, string photoId)
		{
			FileStream stream = null;
			try
			{
				stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
				return ReplacePicture(stream, photoId);
			}
			finally
			{
				if( stream != null ) stream.Close();
			}
			
		}

		/// <summary>
		/// Replace an existing photo on Flickr.
		/// </summary>
		/// <param name="stream">The <see cref="Stream"/> object containing the photo to be uploaded.</param>
		/// <param name="photoId">The ID of the photo to replace.</param>
		/// <returns>The id of the photograph after successful uploading.</returns>
		public string ReplacePicture(Stream stream, string photoId)
		{
			string boundary = "FLICKR_MIME_" + DateTime.Now.ToString("yyyyMMddhhmmss");

			HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(ReplaceUrl);
			req.UserAgent = "Mozilla/4.0 FlickrNet API (compatible; MSIE 6.0; Windows NT 5.1)";
			req.Method = "POST";
			if( Proxy != null ) req.Proxy = Proxy;
			req.Referer = "http://www.flickr.com";
			req.KeepAlive = false;
			req.Timeout = HttpTimeout * 100;
			req.ContentType = "multipart/form-data; boundary=" + boundary + "";

			StringBuilder sb = new StringBuilder();
            
			Hashtable parameters = new Hashtable();
		
			parameters.Add("photo_id", photoId);
			parameters.Add("api_key", _apiKey);
			parameters.Add("auth_token", _apiToken);

			string[] keys = new string[parameters.Keys.Count];
			parameters.Keys.CopyTo(keys, 0);
			Array.Sort(keys);

			StringBuilder HashStringBuilder = new StringBuilder(_sharedSecret, 2 * 1024); 

			foreach(string key in keys)
			{
				HashStringBuilder.Append(key);
				HashStringBuilder.Append(parameters[key]);
				sb.Append("--" + boundary + "\r\n");
				sb.Append("Content-Disposition: form-data; name=\"" + key + "\"\r\n");
				sb.Append("\r\n");
				sb.Append(parameters[key] + "\r\n");
			}

			sb.Append("--" + boundary + "\r\n");
			sb.Append("Content-Disposition: form-data; name=\"api_sig\"\r\n");
			sb.Append("\r\n");
			sb.Append(Md5Hash(HashStringBuilder.ToString()) + "\r\n");

			// Photo
			sb.Append("--" + boundary + "\r\n");
			sb.Append("Content-Disposition: form-data; name=\"photo\"; filename=\"image.jpeg\"\r\n");
			sb.Append("Content-Type: image/jpeg\r\n");
			sb.Append("\r\n");

			UTF8Encoding encoding = new UTF8Encoding();

			byte[] postContents = encoding.GetBytes(sb.ToString());
			
			byte[] photoContents = new byte[stream.Length];
			stream.Read(photoContents, 0, photoContents.Length);
			stream.Close();

			byte[] postFooter = encoding.GetBytes("\r\n--" + boundary + "--\r\n");

			byte[] dataBuffer = new byte[postContents.Length + photoContents.Length + postFooter.Length];
			Buffer.BlockCopy(postContents, 0, dataBuffer, 0, postContents.Length);
			Buffer.BlockCopy(photoContents, 0, dataBuffer, postContents.Length, photoContents.Length);
			Buffer.BlockCopy(postFooter, 0, dataBuffer, postContents.Length + photoContents.Length, postFooter.Length);

			req.ContentLength = dataBuffer.Length;

			Stream resStream = req.GetRequestStream();

			int j = 1;
			int uploadBit = Math.Max(dataBuffer.Length / 100, 50*1024);
			int uploadSoFar = 0;

			for(int i = 0; i < dataBuffer.Length; i=i+uploadBit)
			{
				int toUpload = Math.Min(uploadBit, dataBuffer.Length - i);
				uploadSoFar += toUpload;

				resStream.Write(dataBuffer, i, toUpload);

				if( (OnUploadProgress != null) && ((j++) % 5 == 0 || uploadSoFar == dataBuffer.Length) )
				{
					OnUploadProgress(this, new UploadProgressEventArgs(i+toUpload, uploadSoFar == dataBuffer.Length));
				}
			}
			resStream.Close();

			HttpWebResponse res = (HttpWebResponse)req.GetResponse();

			XmlSerializer serializer = _uploaderSerializer;

			StreamReader sr = new StreamReader(res.GetResponseStream());
			string s= sr.ReadToEnd();
			sr.Close();

			StringReader str = new StringReader(s);

			FlickrNet.Uploader uploader = (FlickrNet.Uploader)serializer.Deserialize(str);
			
			if( uploader.Status == ResponseStatus.OK )
			{
				return uploader.PhotoId;
			}
			else
			{
				throw new FlickrException(uploader.Code, uploader.Message);
			}
		}
		#endregion

		#region [ Blogs ]
		/// <summary>
		/// Gets a list of blogs that have been set up by the user.
		/// Requires authentication.
		/// </summary>
		/// <returns>A <see cref="Blogs"/> object containing the list of blogs.</returns>
		/// <remarks></remarks>
		public Blogs BlogGetList()
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.blogs.getList");
			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Blogs;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Posts a photo already uploaded to a blog.
		/// Requires authentication.
		/// </summary>
		/// <param name="blogId">The Id of the blog to post the photo too.</param>
		/// <param name="photoId">The Id of the photograph to post.</param>
		/// <param name="title">The title of the blog post.</param>
		/// <param name="description">The body of the blog post.</param>
		/// <returns>True if the operation is successful.</returns>
		public bool BlogPostPhoto(string blogId, string photoId, string title, string description)
		{
			return BlogPostPhoto(blogId, photoId, title, description, null);
		}

		/// <summary>
		/// Posts a photo already uploaded to a blog.
		/// Requires authentication.
		/// </summary>
		/// <param name="blogId">The Id of the blog to post the photo too.</param>
		/// <param name="photoId">The Id of the photograph to post.</param>
		/// <param name="title">The title of the blog post.</param>
		/// <param name="description">The body of the blog post.</param>
		/// <param name="blogPassword">The password of the blog if it is not already stored in flickr.</param>
		/// <returns>True if the operation is successful.</returns>
		public bool BlogPostPhoto(string blogId, string photoId, string title, string description, string blogPassword)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.blogs.postPhoto");
			parameters.Add("blog_id", blogId);
			parameters.Add("photo_id", photoId);
			parameters.Add("title", title);
			parameters.Add("description", description);
			if( blogPassword != null ) parameters.Add("blog_password", blogPassword);

			FlickrNet.Response response = GetResponseCache(parameters);
			
			if( response.Status == ResponseStatus.OK )
			{
				return true;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}
		#endregion

		#region [ Contacts ]
		/// <summary>
		/// Gets a list of contacts for the logged in user.
		/// Requires authentication.
		/// </summary>
		/// <returns>An instance of the <see cref="Contacts"/> class containing the list of contacts.</returns>
		public Contacts ContactsGetList()
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.contacts.getList");
			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Contacts;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Gets a list of the given users contact, or those that are publically avaiable.
		/// </summary>
		/// <param name="userId">The Id of the user who's contacts you want to return.</param>
		/// <returns>An instance of the <see cref="Contacts"/> class containing the list of contacts.</returns>
		public Contacts ContactsGetPublicList(string userId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.contacts.getPublicList");
			parameters.Add("user_id", userId);
			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Contacts;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}
		#endregion

		#region [ Favorites ]
		/// <summary>
		/// Adds a photo to the logged in favourites.
		/// Requires authentication.
		/// </summary>
		/// <param name="photoId">The id of the photograph to add.</param>
		/// <returns>True if the operation is successful.</returns>
		public bool FavoritesAdd(string photoId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.favorites.add");
			parameters.Add("photo_id", photoId);
			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return true;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Removes a photograph from the logged in users favourites.
		/// Requires authentication.
		/// </summary>
		/// <param name="photoId">The id of the photograph to remove.</param>
		/// <returns>True if the operation is successful.</returns>
		public bool FavoritesRemove(string photoId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.favorites.remove");
			parameters.Add("photo_id", photoId);
			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return true;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Get a list of the currently logger in users favourites.
		/// Requires authentication.
		/// </summary>
		/// <returns><see cref="Photos"/> instance containing a collection of <see cref="Photo"/> objects.</returns>
		public Photos FavoritesGetList()
		{
			return FavoritesGetList(null, 0, 0);
		}

		/// <summary>
		/// Get a list of the currently logger in users favourites.
		/// Requires authentication.
		/// </summary>
		/// <param name="perPage">Number of photos to include per page.</param>
		/// <param name="page">The page to download this time.</param>
		/// <returns><see cref="Photos"/> instance containing a collection of <see cref="Photo"/> objects.</returns>
		public Photos FavoritesGetList(int perPage, int page)
		{
			return FavoritesGetList(null, perPage, page);
		}

		/// <summary>
		/// Get a list of favourites for the specified user.
		/// </summary>
		/// <param name="userId">The user id of the user whose favourites you wish to retrieve.</param>
		/// <returns><see cref="Photos"/> instance containing a collection of <see cref="Photo"/> objects.</returns>
		public Photos FavoritesGetList(string userId)
		{
			return FavoritesGetList(userId, 0, 0);
		}

		/// <summary>
		/// Get a list of favourites for the specified user.
		/// </summary>
		/// <param name="userId">The user id of the user whose favourites you wish to retrieve.</param>
		/// <param name="perPage">Number of photos to include per page.</param>
		/// <param name="page">The page to download this time.</param>
		/// <returns><see cref="Photos"/> instance containing a collection of <see cref="Photo"/> objects.</returns>
		public Photos FavoritesGetList(string userId, int perPage, int page)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.favorites.getList");
			if( userId != null ) parameters.Add("user_id", userId);
			if( perPage > 0 ) parameters.Add("per_page", perPage.ToString());
			if( page > 0 ) parameters.Add("page", page.ToString());
			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Photos;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Gets the public favourites for a specified user.
		/// </summary>
		/// <remarks>This function difers from <see cref="Flickr.FavoritesGetList(string)"/> in that the user id 
		/// is not optional.</remarks>
		/// <param name="userId">The is of the user whose favourites you wish to return.</param>
		/// <returns>A <see cref="Photos"/> object containing a collection of <see cref="Photo"/> objects.</returns>
		public Photos FavoritesGetPublicList(string userId)
		{
			return FavoritesGetPublicList(userId, 0, 0);
		}
			
		/// <summary>
		/// Gets the public favourites for a specified user.
		/// </summary>
		/// <remarks>This function difers from <see cref="Flickr.FavoritesGetList(string)"/> in that the user id 
		/// is not optional.</remarks>
		/// <param name="userId">The is of the user whose favourites you wish to return.</param>
		/// <param name="perPage">The number of photos to return per page.</param>
		/// <param name="page">The specific page to return.</param>
		/// <returns>A <see cref="Photos"/> object containing a collection of <see cref="Photo"/> objects.</returns>
		public Photos FavoritesGetPublicList(string userId, int perPage, int page)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.favorites.getPublicList");
			parameters.Add("user_id", userId);
			if( perPage > 0 ) parameters.Add("per_page", perPage.ToString());
			if( page > 0 ) parameters.Add("page", page.ToString());
			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Photos;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}
		#endregion

		#region [ Groups ]
		/// <summary>
		/// Returns the top <see cref="Category"/> with a list of sub-categories and groups. 
		/// (The top category does not have any groups in it but others may).
		/// </summary>
		/// <returns>A <see cref="Category"/> instance.</returns>
		public Category GroupsBrowse()
		{
			return GroupsBrowse("0");
		}
		
		/// <summary>
		/// Returns the <see cref="Category"/> specified by the category id with a list of sub-categories and groups. 
		/// </summary>
		/// <param name="catId"></param>
		/// <returns>A <see cref="Category"/> instance.</returns>
		public Category GroupsBrowse(string catId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.groups.browse");
			parameters.Add("cat_id", catId);
			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Category;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Search the list of groups on Flickr for the text.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <returns>A list of groups matching the search criteria.</returns>
		public GroupSearchResults GroupsSearch(string text)
		{
			return GroupsSearch(text, 0, 0);
		}

		/// <summary>
		/// Search the list of groups on Flickr for the text.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <param name="page">The page of the results to return.</param>
		/// <returns>A list of groups matching the search criteria.</returns>
		public GroupSearchResults GroupsSearch(string text, int page)
		{
			return GroupsSearch(text, page, 0);
		}

		/// <summary>
		/// Search the list of groups on Flickr for the text.
		/// </summary>
		/// <param name="text">The text to search for.</param>
		/// <param name="page">The page of the results to return.</param>
		/// <param name="perPage">The number of groups to list per page.</param>
		/// <returns>A list of groups matching the search criteria.</returns>
		public GroupSearchResults GroupsSearch(string text, int page, int perPage)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.groups.search");
			parameters.Add("api_key", _apiKey);
			parameters.Add("text", text);
			if( page > 0 ) parameters.Add("page", page.ToString());
			if( perPage > 0 ) parameters.Add("per_page", perPage.ToString());

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return new GroupSearchResults(response.AllElements[0]);
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Returns a <see cref="GroupFullInfo"/> object containing details about a group.
		/// </summary>
		/// <param name="groupId">The id of the group to return.</param>
		/// <returns>The <see cref="GroupFullInfo"/> specified by the group id.</returns>
		public GroupFullInfo GroupsGetInfo(string groupId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.groups.getInfo");
			parameters.Add("api_key", _apiKey);
			parameters.Add("group_id", groupId);
			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return new GroupFullInfo(response.AllElements[0]);
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}
		#endregion

		#region [ Group Pool ]
		/// <summary>
		/// Adds a photo to a pool you have permission to add photos to.
		/// </summary>
		/// <param name="photoId">The id of one of your photos to be added.</param>
		/// <param name="groupId">The id of a group you are a member of.</param>
		/// <returns>True on a successful addition.</returns>
		public bool GroupPoolAdd(string photoId, string groupId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.groups.pools.add");
			parameters.Add("photo_id", photoId);
			parameters.Add("group_id", groupId);
			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return true;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Gets the context for a photo from within a group. This provides the
		/// id and thumbnail url for the next and previous photos in the group.
		/// </summary>
		/// <param name="photoId">The Photo ID for the photo you want the context for.</param>
		/// <param name="groupId">The group ID for the group you want the context to be relevant to.</param>
		/// <returns>The <see cref="Context"/> of the photo in the group.</returns>
		public Context GroupPoolGetContext(string photoId, string groupId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.groups.pools.getContext");
			parameters.Add("photo_id", photoId);
			parameters.Add("group_id", groupId);
			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				Context context = new Context();
				context.Count = response.ContextCount.Count;
				context.NextPhoto = response.ContextNextPhoto;
				context.PreviousPhoto = response.ContextPrevPhoto;
				return context;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Remove a picture from a group.
		/// </summary>
		/// <param name="photoId">The id of one of your pictures you wish to remove.</param>
		/// <param name="groupId">The id of the group to remove the picture from.</param>
		/// <returns>True if the photo is successfully removed.</returns>
		public bool GroupPoolRemove(string photoId, string groupId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.groups.pools.remove");
			parameters.Add("photo_id", photoId);
			parameters.Add("group_id", groupId);
			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return true;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Gets a list of 
		/// </summary>
		/// <returns></returns>
		public MemberGroupInfo[] GroupPoolGetGroups()
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.groups.pools.getGroups");
			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return MemberGroupInfo.GetMemberGroupInfo(response.AllElements[0]);
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Gets a list of photos for a given group.
		/// </summary>
		/// <param name="groupId">The group ID for the group.</param>
		/// <returns>A <see cref="Photos"/> object containing the list of photos.</returns>
		public Photos GroupPoolGetPhotos(string groupId)
		{
			return GroupPoolGetPhotos(groupId, null, null, PhotoSearchExtras.All, 0, 0);
		}

		/// <summary>
		/// Gets a list of photos for a given group.
		/// </summary>
		/// <param name="groupId">The group ID for the group.</param>
		/// <param name="tags">Space seperated list of tags that photos returned must have.</param>
		/// <returns>A <see cref="Photos"/> object containing the list of photos.</returns>
		public Photos GroupPoolGetPhotos(string groupId, string tags)
		{
			return GroupPoolGetPhotos(groupId, tags, null, PhotoSearchExtras.All, 0, 0);
		}

		/// <summary>
		/// Gets a list of photos for a given group.
		/// </summary>
		/// <param name="groupId">The group ID for the group.</param>
		/// <param name="perPage">The number of photos per page.</param>
		/// <param name="page">The page to return.</param>
		/// <returns>A <see cref="Photos"/> object containing the list of photos.</returns>
		public Photos GroupPoolGetPhotos(string groupId, int perPage, int page)
		{
			return GroupPoolGetPhotos(groupId, null, null, PhotoSearchExtras.All, perPage, page);
		}

		/// <summary>
		/// Gets a list of photos for a given group.
		/// </summary>
		/// <param name="groupId">The group ID for the group.</param>
		/// <param name="tags">Space seperated list of tags that photos returned must have.</param>
		/// <param name="perPage">The number of photos per page.</param>
		/// <param name="page">The page to return.</param>
		/// <returns>A <see cref="Photos"/> object containing the list of photos.</returns>
		public Photos GroupPoolGetPhotos(string groupId, string tags, int perPage, int page)
		{
			return GroupPoolGetPhotos(groupId, tags, null, PhotoSearchExtras.All, perPage, page);
		}

		/// <summary>
		/// Gets a list of photos for a given group.
		/// </summary>
		/// <param name="groupId">The group ID for the group.</param>
		/// <param name="tags">Space seperated list of tags that photos returned must have.
		/// Currently only supports 1 tag at a time.</param>
		/// <param name="userId">The group member to return photos for.</param>
		/// <param name="extras">The <see cref="PhotoSearchExtras"/> specifying which extras to return. All other overloads default to returning all extras.</param>
		/// <param name="perPage">The number of photos per page.</param>
		/// <param name="page">The page to return.</param>
		/// <returns>A <see cref="Photos"/> object containing the list of photos.</returns>
		public Photos GroupPoolGetPhotos(string groupId, string tags, string userId, PhotoSearchExtras extras, int perPage, int page)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.groups.pools.getPhotos");
			parameters.Add("group_id", groupId);
			if( tags != null && tags.Length > 0 )parameters.Add("tags", tags);
			if( perPage > 0 ) parameters.Add("per_page", perPage.ToString());
			if( page > 0 ) parameters.Add("page", page.ToString());
			if( userId != null && userId.Length > 0 ) parameters.Add("user_id", userId);
			if( extras != PhotoSearchExtras.None ) parameters.Add("extras", Utils.ExtrasToString(extras));

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Photos;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}
		#endregion

		#region [ Interestingness ]
		/// <summary>
		/// Gets a list of photos from the most recent interstingness list.
		/// </summary>
		/// <param name="perPage">Number of photos per page.</param>
		/// <param name="page">The page number to return.</param>
		/// <param name="extras"><see cref="PhotoSearchExtras"/> enumeration.</param>
		/// <returns><see cref="Photos"/> instance containing list of photos.</returns>
		public Photos InterestingnessGetList(PhotoSearchExtras extras, int perPage, int page)
		{
			return InterestingnessGetList(DateTime.MinValue, extras, perPage, page);
		}

		/// <summary>
		/// Gets a list of photos from the interstingness list for the specified date.
		/// </summary>
		/// <param name="date">The date to return the interestingness list for.</param>
		/// <returns><see cref="Photos"/> instance containing list of photos.</returns>
		public Photos InterestingnessGetList(DateTime date)
		{
			return InterestingnessGetList(date, PhotoSearchExtras.All, 0, 0);
		}

		/// <summary>
		/// Gets a list of photos from the most recent interstingness list.
		/// </summary>
		/// <returns><see cref="Photos"/> instance containing list of photos.</returns>
		public Photos InterestingnessGetList()
		{
			return InterestingnessGetList(DateTime.MinValue, PhotoSearchExtras.All, 0, 0);
		}

		/// <summary>
		/// Gets a list of photos from the most recent interstingness list.
		/// </summary>
		/// <param name="date">The date to return the interestingness photos for.</param>
		/// <param name="extras">The extra parameters to return along with the search results.
		/// See <see cref="PhotoSearchOptions"/> for more details.</param>
		/// <param name="perPage">The number of results to return per page.</param>
		/// <param name="page">The page of the results to return.</param>
		/// <returns></returns>
		public Photos InterestingnessGetList(DateTime date, PhotoSearchExtras extras, int perPage, int page)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.interestingness.getList");

			if( date > DateTime.MinValue ) parameters.Add("date", date.ToString("yyyy-MM-dd"));
			if( perPage > 0 ) parameters.Add("per_page", perPage.ToString());
			if( page > 0 ) parameters.Add("page", page.ToString());
			if( extras != PhotoSearchExtras.None )
				parameters.Add("extras", Utils.ExtrasToString(extras));

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Photos;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}


		#endregion

		#region [ Notes ]
		/// <summary>
		/// Add a note to a picture.
		/// </summary>
		/// <param name="photoId">The photo id to add the note to.</param>
		/// <param name="noteX">The X co-ordinate of the upper left corner of the note.</param>
		/// <param name="noteY">The Y co-ordinate of the upper left corner of the note.</param>
		/// <param name="noteWidth">The width of the note.</param>
		/// <param name="noteHeight">The height of the note.</param>
		/// <param name="noteText">The text in the note.</param>
		/// <returns></returns>
		public string NotesAdd(string photoId, int noteX, int noteY, int noteWidth, int noteHeight, string noteText)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.notes.add");
			parameters.Add("photo_id", photoId);
			parameters.Add("note_x", noteX.ToString());
			parameters.Add("note_y", noteY.ToString());
			parameters.Add("note_w", noteWidth.ToString());
			parameters.Add("note_h", noteHeight.ToString());
			parameters.Add("note_text", noteText);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				foreach(XmlElement element in response.AllElements)
				{
					return element.Attributes["id", ""].Value;
				}
				return string.Empty;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Edit and update a note.
		/// </summary>
		/// <param name="noteId">The ID of the note to update.</param>
		/// <param name="noteX">The X co-ordinate of the upper left corner of the note.</param>
		/// <param name="noteY">The Y co-ordinate of the upper left corner of the note.</param>
		/// <param name="noteWidth">The width of the note.</param>
		/// <param name="noteHeight">The height of the note.</param>
		/// <param name="noteText">The new text in the note.</param>
		public void NotesEdit(string noteId, int noteX, int noteY, int noteWidth, int noteHeight, string noteText)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.notes.edit");
			parameters.Add("note_id", noteId);
			parameters.Add("note_x", noteX.ToString());
			parameters.Add("note_y", noteY.ToString());
			parameters.Add("note_w", noteWidth.ToString());
			parameters.Add("note_h", noteHeight.ToString());
			parameters.Add("note_text", noteText);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Delete an existing note.
		/// </summary>
		/// <param name="noteId">The ID of the note.</param>
		public void NotesDelete(string noteId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.notes.delete");
			parameters.Add("note_id", noteId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}
		#endregion

		#region [ People ]
		/// <summary>
		/// Used to fid a flickr users details by specifying their email address.
		/// </summary>
		/// <param name="emailAddress">The email address to search on.</param>
		/// <returns>The <see cref="FoundUser"/> object containing the matching details.</returns>
		/// <exception cref="FlickrException">A FlickrException is raised if the email address is not found.</exception>
		public FoundUser PeopleFindByEmail(string emailAddress)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.people.findByEmail");
			parameters.Add("api_key", _apiKey);
			parameters.Add("find_email", emailAddress);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return new FoundUser(response.AllElements[0]);
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Returns a <see cref="FoundUser"/> object matching the screen name.
		/// </summary>
		/// <param name="username">The screen name or username of the user.</param>
		/// <returns>A <see cref="FoundUser"/> class containing the userId and username of the user.</returns>
		/// <exception cref="FlickrException">A FlickrException is raised if the email address is not found.</exception>
		public FoundUser PeopleFindByUsername(string username)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.people.findByUsername");
			parameters.Add("api_key", _apiKey);
			parameters.Add("username", username);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return new FoundUser(response.AllElements[0]);
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Gets the <see cref="Person"/> object for the given user id.
		/// </summary>
		/// <param name="userId">The user id to find.</param>
		/// <returns>The <see cref="Person"/> object containing the users details.</returns>
		public Person PeopleGetInfo(string userId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.people.getInfo");
			parameters.Add("api_key", _apiKey);
			parameters.Add("user_id", userId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Person;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Gets the upload status of the authenticated user.
		/// </summary>
		/// <returns>The <see cref="UserStatus"/> object containing the users details.</returns>
		public UserStatus PeopleGetUploadStatus()
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.people.getUploadStatus");

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return new UserStatus(response.AllElements[0]);
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Get a list of public groups for a user.
		/// </summary>
		/// <param name="userId">The user id to get groups for.</param>
		/// <returns>An array of <see cref="PublicGroupInfo"/> instances.</returns>
		public PublicGroupInfo[] PeopleGetPublicGroups(string userId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.people.getPublicGroups");
			parameters.Add("api_key", _apiKey);
			parameters.Add("user_id", userId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return PublicGroupInfo.GetPublicGroupInfo(response.AllElements[0]);
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Gets a users public photos. Excludes private photos.
		/// </summary>
		/// <param name="userId">The user id of the user.</param>
		/// <returns>The collection of photos contained within a <see cref="Photo"/> object.</returns>
		public Photos PeopleGetPublicPhotos(string userId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.people.getPublicPhotos");
			parameters.Add("api_key", _apiKey);
			parameters.Add("user_id", userId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Photos;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}
		#endregion

		#region [ Photos ]
		/// <summary>
		/// Add a selection of tags to a photo.
		/// </summary>
		/// <param name="photoId">The photo id of the photo.</param>
		/// <param name="tags">An array of strings containing the tags.</param>
		/// <returns>True if the tags are added successfully.</returns>
		public void PhotosAddTags(string photoId, string[] tags)
		{	
			string s = string.Join(",", tags);
			PhotosAddTags(photoId, s);
		}

		/// <summary>
		/// Add a selection of tags to a photo.
		/// </summary>
		/// <param name="photoId">The photo id of the photo.</param>
		/// <param name="tags">An string of comma delimited tags.</param>
		/// <returns>True if the tags are added successfully.</returns>
		public void PhotosAddTags(string photoId, string tags)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.addTags");
			parameters.Add("photo_id", photoId);
			parameters.Add("tags", tags);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Delete a photo from Flickr.
		/// </summary>
		/// <remarks>
		/// Requires Delete permissions. Also note, photos cannot be recovered once deleted.</remarks>
		/// <param name="photoId">The ID of the photo to delete.</param>
		public void PhotosDelete(string photoId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.delete");
			parameters.Add("photo_id", photoId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Get all the contexts (group, set and photostream 'next' and 'previous'
		/// pictures) for a photo.
		/// </summary>
		/// <param name="photoId">The photo id of the photo to get the contexts for.</param>
		/// <returns>An instance of the <see cref="AllContexts"/> class.</returns>
		public AllContexts PhotosGetAllContexts(string photoId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.getAllContexts");
			parameters.Add("photo_id", photoId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				AllContexts contexts = new AllContexts(response.AllElements);
				return contexts;
			}
			else
			{
				throw new FlickrException(response.Error);
			}

		}

		/// <summary>
		/// Gets the most recent 10 photos from your contacts.
		/// </summary>
		/// <returns>An instance of the <see cref="Photo"/> class containing the photos.</returns>
		public Photos PhotosGetContactsPhotos()
		{
			return PhotosGetContactsPhotos(0, false, false, false);
		}

		/// <summary>
		/// Gets the most recent photos from your contacts.
		/// </summary>
		/// <remarks>Returns the most recent photos from all your contact, excluding yourself.</remarks>
		/// <param name="count">The number of photos to return, from between 10 and 50.</param>
		/// <returns>An instance of the <see cref="Photo"/> class containing the photos.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Throws a <see cref="ArgumentOutOfRangeException"/> exception if the cound
		/// is not between 10 and 50, or 0.</exception>
		public Photos PhotosGetContactsPhotos(long count)
		{
			return PhotosGetContactsPhotos(count, false, false, false);
		}

		/// <summary>
		/// Gets your contacts most recent photos.
		/// </summary>
		/// <param name="count">The number of photos to return, from between 10 and 50.</param>
		/// <param name="justFriends">If true only returns photos from contacts marked as
		/// 'friends'.</param>
		/// <param name="singlePhoto">If true only returns a single photo for each of your contacts.
		/// Ignores the count if this is true.</param>
		/// <param name="includeSelf">If true includes yourself in the group of people to 
		/// return photos for.</param>
		/// <returns>An instance of the <see cref="Photo"/> class containing the photos.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Throws a <see cref="ArgumentOutOfRangeException"/> exception if the cound
		/// is not between 10 and 50, or 0.</exception>
		public Photos PhotosGetContactsPhotos(long count, bool justFriends, bool singlePhoto, bool includeSelf)
		{
			if( count != 0 && (count < 10 || count > 50) && !singlePhoto )
			{
				throw new ArgumentOutOfRangeException("count", "Count must be between 10 and 50.");
			}
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.getContactsPhotos");
			if( count > 0 && !singlePhoto ) parameters.Add("count", count.ToString());
			if( justFriends ) parameters.Add("just_friends", "1");
			if( singlePhoto ) parameters.Add("single_photo", "1");
			if( includeSelf ) parameters.Add("include_self", "1");

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Photos;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Gets the public photos for given users ID's contacts.
		/// </summary>
		/// <param name="userId">The user ID whose contacts you wish to get photos for.</param>
		/// <returns>A <see cref="Photos"/> object containing details of the photos returned.</returns>
		public Photos PhotosGetContactsPublicPhotos(string userId)
		{
			return PhotosGetContactsPublicPhotos(userId, 0, false, false, false, PhotoSearchExtras.All);
		}

		/// <summary>
		/// Gets the public photos for given users ID's contacts.
		/// </summary>
		/// <param name="userId">The user ID whose contacts you wish to get photos for.</param>
		/// <param name="extras">A list of extra details to return for each photo.</param>
		/// <returns>A <see cref="Photos"/> object containing details of the photos returned.</returns>
		public Photos PhotosGetContactsPublicPhotos(string userId, PhotoSearchExtras extras)
		{
			return PhotosGetContactsPublicPhotos(userId, 0, false, false, false, extras);
		}

		/// <summary>
		/// Gets the public photos for given users ID's contacts.
		/// </summary>
		/// <param name="userId">The user ID whose contacts you wish to get photos for.</param>
		/// <param name="count">The number of photos to return. Defaults to 10, maximum is 50.</param>
		/// <returns>A <see cref="Photos"/> object containing details of the photos returned.</returns>
		public Photos PhotosGetContactsPublicPhotos(string userId, long count)
		{
			return PhotosGetContactsPublicPhotos(userId, count, false, false, false, PhotoSearchExtras.All);
		}

		/// <summary>
		/// Gets the public photos for given users ID's contacts.
		/// </summary>
		/// <param name="userId">The user ID whose contacts you wish to get photos for.</param>
		/// <param name="count">The number of photos to return. Defaults to 10, maximum is 50.</param>
		/// <param name="extras">A list of extra details to return for each photo.</param>
		/// <returns>A <see cref="Photos"/> object containing details of the photos returned.</returns>
		public Photos PhotosGetContactsPublicPhotos(string userId, long count, PhotoSearchExtras extras)
		{
			return PhotosGetContactsPublicPhotos(userId, count, false, false, false, extras);
		}

		/// <summary>
		/// Gets the public photos for given users ID's contacts.
		/// </summary>
		/// <param name="userId">The user ID whose contacts you wish to get photos for.</param>
		/// <param name="count">The number of photos to return. Defaults to 10, maximum is 50.</param>
		/// <param name="justFriends">True to just return photos from friends and family (excluding regular contacts).</param>
		/// <param name="singlePhoto">True to return just a single photo for each contact.</param>
		/// <param name="includeSelf">True to include photos from the user ID specified as well.</param>
		/// <returns></returns>
		public Photos PhotosGetContactsPublicPhotos(string userId, long count, bool justFriends, bool singlePhoto, bool includeSelf)
		{
			return PhotosGetContactsPublicPhotos(userId, count, justFriends, singlePhoto, includeSelf, PhotoSearchExtras.All);
		}
			
		/// <summary>
		/// Gets the public photos for given users ID's contacts.
		/// </summary>
		/// <param name="userId">The user ID whose contacts you wish to get photos for.</param>
		/// <param name="count">The number of photos to return. Defaults to 10, maximum is 50.</param>
		/// <param name="justFriends">True to just return photos from friends and family (excluding regular contacts).</param>
		/// <param name="singlePhoto">True to return just a single photo for each contact.</param>
		/// <param name="includeSelf">True to include photos from the user ID specified as well.</param>
		/// <param name="extras">A list of extra details to return for each photo.</param>
		/// <returns></returns>
		public Photos PhotosGetContactsPublicPhotos(string userId, long count, bool justFriends, bool singlePhoto, bool includeSelf, PhotoSearchExtras extras)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.getContactsPublicPhotos");
			parameters.Add("api_key", _apiKey);
			parameters.Add("user_id", userId);
			if( count > 0 ) parameters.Add("count", count.ToString());
			if( justFriends ) parameters.Add("just_friends", "1");
			if( singlePhoto ) parameters.Add("single_photo", "1");
			if( includeSelf ) parameters.Add("include_self", "1");
			if( extras != PhotoSearchExtras.None ) parameters.Add("extras", Utils.ExtrasToString(extras));

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Photos;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Gets the context of the photo in the users photostream.
		/// </summary>
		/// <param name="photoId">The ID of the photo to return the context for.</param>
		/// <returns></returns>
		public Context PhotosGetContext(string photoId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.getContext");
			parameters.Add("photo_id", photoId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				Context c = new Context();
				c.Count = response.ContextCount.Count;
				c.NextPhoto = response.ContextNextPhoto;
				c.PreviousPhoto = response.ContextPrevPhoto;

				return c;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Returns count of photos between each pair of dates in the list.
		/// </summary>
		/// <remarks>If you pass in DateA, DateB and DateC it returns
		/// a list of the number of photos between DateA and DateB,
		/// followed by the number between DateB and DateC. 
		/// More parameters means more sets.</remarks>
		/// <param name="dates">Array of <see cref="DateTime"/> objects.</param>
		/// <returns><see cref="PhotoCounts"/> class instance.</returns>
		public PhotoCounts PhotosGetCounts(DateTime[] dates)
		{
			return PhotosGetCounts(dates, false);
		}

		/// <summary>
		/// Returns count of photos between each pair of dates in the list.
		/// </summary>
		/// <remarks>If you pass in DateA, DateB and DateC it returns
		/// a list of the number of photos between DateA and DateB,
		/// followed by the number between DateB and DateC. 
		/// More parameters means more sets.</remarks>
		/// <param name="dates">Array of <see cref="DateTime"/> objects.</param>
		/// <param name="taken">Boolean parameter to specify if the dates are the taken date, or uploaded date.</param>
		/// <returns><see cref="PhotoCounts"/> class instance.</returns>
		public PhotoCounts PhotosGetCounts(DateTime[] dates, bool taken)
		{
			StringBuilder s = new StringBuilder(dates.Length * 20);
			foreach(DateTime d in dates)
			{
				s.Append(Utils.DateToUnixTimestamp(d));
				s.Append(",");
			}
			if( s.Length > 0 ) s.Remove(s.Length-2,1);

			if( taken )
                return PhotosGetCounts(null, s.ToString());
			else
				return PhotosGetCounts(s.ToString(), null);
		}
		/// <summary>
		/// Returns count of photos between each pair of dates in the list.
		/// </summary>
		/// <remarks>If you pass in DateA, DateB and DateC it returns
		/// a list of the number of photos between DateA and DateB,
		/// followed by the number between DateB and DateC. 
		/// More parameters means more sets.</remarks>
		/// <param name="dates">Comma-delimited list of dates in unix timestamp format. Optional.</param>
		/// <returns><see cref="PhotoCounts"/> class instance.</returns>
		public PhotoCounts PhotosGetCounts(string dates)
		{
			return PhotosGetCounts(dates, null);
		}

		/// <summary>
		/// Returns count of photos between each pair of dates in the list.
		/// </summary>
		/// <remarks>If you pass in DateA, DateB and DateC it returns
		/// a list of the number of photos between DateA and DateB,
		/// followed by the number between DateB and DateC. 
		/// More parameters means more sets.</remarks>
		/// <param name="dates">Comma-delimited list of dates in unix timestamp format. Optional.</param>
		/// <param name="taken_dates">Comma-delimited list of dates in unix timestamp format. Optional.</param>
		/// <returns><see cref="PhotoCounts"/> class instance.</returns>
		public PhotoCounts PhotosGetCounts(string dates, string taken_dates)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.getContactsPhotos");
			if( dates != null && dates.Length > 0 ) parameters.Add("dates", dates);
			if( taken_dates != null && taken_dates.Length > 0 ) parameters.Add("taken_dates", taken_dates);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.PhotoCounts;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Gets the EXIF data for a given Photo ID.
		/// </summary>
		/// <param name="photoId">The Photo ID of the photo to return the EXIF data for.</param>
		/// <returns>An instance of the <see cref="ExifPhoto"/> class containing the EXIF data.</returns>
		public ExifPhoto PhotosGetExif(string photoId)
		{
			return PhotosGetExif(photoId, null);
		}

		/// <summary>
		/// Gets the EXIF data for a given Photo ID.
		/// </summary>
		/// <param name="photoId">The Photo ID of the photo to return the EXIF data for.</param>
		/// <param name="secret">The secret of the photo. If the secret is specified then
		/// authentication checks are bypassed.</param>
		/// <returns>An instance of the <see cref="ExifPhoto"/> class containing the EXIF data.</returns>
		public ExifPhoto PhotosGetExif(string photoId, string secret)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.getExif");
			parameters.Add("photo_id", photoId);
			if( secret != null ) parameters.Add("secret", secret);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				ExifPhoto e = new ExifPhoto(response.PhotoInfo.PhotoId,
					response.PhotoInfo.Secret,
					response.PhotoInfo.Server,
					response.PhotoInfo.ExifTagCollection);

				return e;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Get information about a photo. The calling user must have permission to view the photo.
		/// </summary>
		/// <param name="photoId">The id of the photo to fetch information for.</param>
		/// <returns>A <see cref="PhotoInfo"/> class detailing the properties of the photo.</returns>
		public PhotoInfo PhotosGetInfo(string photoId)
		{
			return PhotosGetInfo(photoId, null);
		}
		
		/// <summary>
		/// Get information about a photo. The calling user must have permission to view the photo.
		/// </summary>
		/// <param name="photoId">The id of the photo to fetch information for.</param>
		/// <param name="secret">The secret for the photo. If the correct secret is passed then permissions checking is skipped. This enables the 'sharing' of individual photos by passing around the id and secret.</param>
		/// <returns>A <see cref="PhotoInfo"/> class detailing the properties of the photo.</returns>
		public PhotoInfo PhotosGetInfo(string photoId, string secret)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.getInfo");
			parameters.Add("photo_id", photoId);
			if( secret != null ) parameters.Add("secret", secret);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.PhotoInfo;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Get permissions for a photo.
		/// </summary>
		/// <param name="photoId">The id of the photo to get permissions for.</param>
		/// <returns>An instance of the <see cref="PhotoPermissions"/> class containing the permissions of the specified photo.</returns>
		public PhotoPermissions PhotosGetPerms(string photoId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.getPerms");
			parameters.Add("photo_id", photoId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return new PhotoPermissions(response.AllElements[0]);
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Returns a list of the latest public photos uploaded to flickr.
		/// </summary>
		/// <returns>A <see cref="Photos"/> class containing the list of photos.</returns>
		public Photos PhotosGetRecent()
		{
			return PhotosGetRecent(0, 0, PhotoSearchExtras.All);
		}

		/// <summary>
		/// Returns a list of the latest public photos uploaded to flickr.
		/// </summary>
		/// <param name="extras">A comma-delimited list of extra information to fetch for each returned record.</param>
		/// <returns>A <see cref="Photos"/> class containing the list of photos.</returns>
		public Photos PhotosGetRecent(PhotoSearchExtras extras)
		{
			return PhotosGetRecent(0, 0, extras);
		}

		/// <summary>
		/// Returns a list of the latest public photos uploaded to flickr.
		/// </summary>
		/// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
		/// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
		/// <returns>A <see cref="Photos"/> class containing the list of photos.</returns>
		public Photos PhotosGetRecent(long perPage, long page)
		{
			return PhotosGetRecent(perPage, page, PhotoSearchExtras.All);
		}

		/// <summary>
		/// Returns a list of the latest public photos uploaded to flickr.
		/// </summary>
		/// <param name="extras">A comma-delimited list of extra information to fetch for each returned record.</param>
		/// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
		/// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
		/// <returns>A <see cref="Photos"/> class containing the list of photos.</returns>
		public Photos PhotosGetRecent(long perPage, long page, PhotoSearchExtras extras)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.getRecent");
			parameters.Add("api_key", _apiKey);
			if( perPage > 0 ) parameters.Add("per_page", perPage.ToString());
			if( page > 0 ) parameters.Add("page", page.ToString());
			if( extras != PhotoSearchExtras.None ) parameters.Add("extras", Utils.ExtrasToString(extras));

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Photos;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Return a list of your photos that have been recently created or which have been recently modified.
		/// Recently modified may mean that the photo's metadata (title, description, tags) 
		/// may have been changed or a comment has been added (or just modified somehow :-)
		/// </summary>
		/// <param name="minDate">The date from which modifications should be compared.</param>
		/// <param name="extras">A list of extra information to fetch for each returned record.</param>
		/// <returns>Returns a <see cref="Photos"/> instance containing the list of photos.</returns>
		public Photos PhotosRecentlyUpdated(DateTime minDate, PhotoSearchExtras extras)
		{
			return PhotosRecentlyUpdated(minDate, extras, 0, 0);
		}

		/// <summary>
		/// Return a list of your photos that have been recently created or which have been recently modified.
		/// Recently modified may mean that the photo's metadata (title, description, tags) 
		/// may have been changed or a comment has been added (or just modified somehow :-)
		/// </summary>
		/// <param name="minDate">The date from which modifications should be compared.</param>
		/// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
		/// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
		/// <returns>Returns a <see cref="Photos"/> instance containing the list of photos.</returns>
		public Photos PhotosRecentlyUpdated(DateTime minDate, int perPage, int page)
		{
			return PhotosRecentlyUpdated(minDate, PhotoSearchExtras.None, perPage, page);
		}

		/// <summary>
		/// Return a list of your photos that have been recently created or which have been recently modified.
		/// Recently modified may mean that the photo's metadata (title, description, tags) 
		/// may have been changed or a comment has been added (or just modified somehow :-)
		/// </summary>
		/// <param name="minDate">The date from which modifications should be compared.</param>
		/// <returns>Returns a <see cref="Photos"/> instance containing the list of photos.</returns>
		public Photos PhotosRecentlyUpdated(DateTime minDate)
		{
			return PhotosRecentlyUpdated(minDate, PhotoSearchExtras.None, 0, 0);
		}

		/// <summary>
		/// Return a list of your photos that have been recently created or which have been recently modified.
		/// Recently modified may mean that the photo's metadata (title, description, tags) 
		/// may have been changed or a comment has been added (or just modified somehow :-)
		/// </summary>
		/// <param name="minDate">The date from which modifications should be compared.</param>
		/// <param name="extras">A list of extra information to fetch for each returned record.</param>
		/// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
		/// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
		/// <returns>Returns a <see cref="Photos"/> instance containing the list of photos.</returns>
		public Photos PhotosRecentlyUpdated(DateTime minDate, PhotoSearchExtras extras, int perPage, int page)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.recentlyUpdated");
			parameters.Add("min_date", Utils.DateToUnixTimestamp(minDate).ToString());
			if( extras != PhotoSearchExtras.None ) parameters.Add("extras", Utils.ExtrasToString(extras));
			if( perPage > 0  ) parameters.Add("per_page", perPage.ToString());
			if( page > 0 ) parameters.Add("page", page.ToString());

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Photos;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Returns the available sizes for a photo. The calling user must have permission to view the photo.
		/// </summary>
		/// <param name="photoId">The id of the photo to fetch size information for.</param>
		/// <returns>A <see cref="Sizes"/> class whose property <see cref="Sizes.SizeCollection"/> is an array of <see cref="Size"/> objects.</returns>
		public Sizes PhotosGetSizes(string photoId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.getSizes");
			parameters.Add("photo_id", photoId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Sizes;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Returns a list of your photos with no tags.
		/// </summary>
		/// <returns>A <see cref="Photos"/> class containing the list of photos.</returns>
		public Photos PhotosGetUntagged()
		{
			return PhotosGetUntagged(0, 0, PhotoSearchExtras.All);
		}

		/// <summary>
		/// Returns a list of your photos with no tags.
		/// </summary>
		/// <param name="extras">A comma-delimited list of extra information to fetch for each returned record.</param>
		/// <returns>A <see cref="Photos"/> class containing the list of photos.</returns>
		public Photos PhotosGetUntagged(PhotoSearchExtras extras)
		{
			return PhotosGetUntagged(0, 0, extras);
		}

		/// <summary>
		/// Returns a list of your photos with no tags.
		/// </summary>
		/// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
		/// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
		/// <returns>A <see cref="Photos"/> class containing the list of photos.</returns>
		public Photos PhotosGetUntagged(int perPage, int page)
		{
			return PhotosGetUntagged(perPage, page, PhotoSearchExtras.All);
		}

		/// <summary>
		/// Returns a list of your photos with no tags.
		/// </summary>
		/// <param name="extras">A comma-delimited list of extra information to fetch for each returned record.</param>
		/// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
		/// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
		/// <returns>A <see cref="Photos"/> class containing the list of photos.</returns>
		public Photos PhotosGetUntagged(int perPage, int page, PhotoSearchExtras extras)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.getUntagged");
			if( perPage > 0 ) parameters.Add("per_page", perPage.ToString());
			if( page > 0 ) parameters.Add("page", page.ToString());
			if( extras != PhotoSearchExtras.None ) parameters.Add("extras", Utils.ExtrasToString(extras));

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Photos;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Gets a list of photos not in sets. Defaults to include all extra fields.
		/// </summary>
		/// <returns><see cref="Photos"/> instance containing list of photos.</returns>
		public Photos PhotosGetNotInSet()
		{
			return PhotosGetNotInSet(new PartialSearchOptions());
		}

		/// <summary>
		/// Gets a specific page of the list of photos which are not in sets.
		/// Defaults to include all extra fields.
		/// </summary>
		/// <param name="page">The page number to return.</param>
		/// <returns><see cref="Photos"/> instance containing list of photos.</returns>
		public Photos PhotosGetNotInSet(int page)
		{
			return PhotosGetNotInSet(0, page, PhotoSearchExtras.None);
		}

		/// <summary>
		/// Gets a specific page of the list of photos which are not in sets.
		/// Defaults to include all extra fields.
		/// </summary>
		/// <param name="perPage">Number of photos per page.</param>
		/// <param name="page">The page number to return.</param>
		/// <returns><see cref="Photos"/> instance containing list of photos.</returns>
		public Photos PhotosGetNotInSet(int perPage, int page)
		{
			return PhotosGetNotInSet(perPage, page, PhotoSearchExtras.None);
		}

		/// <summary>
		/// Gets a list of a users photos which are not in a set.
		/// </summary>
		/// <param name="perPage">Number of photos per page.</param>
		/// <param name="page">The page number to return.</param>
		/// <param name="extras"><see cref="PhotoSearchExtras"/> enumeration.</param>
		/// <returns><see cref="Photos"/> instance containing list of photos.</returns>
		public Photos PhotosGetNotInSet(int perPage, int page, PhotoSearchExtras extras)
		{
			PartialSearchOptions options = new PartialSearchOptions();
			options.PerPage = perPage;
			options.Page = page;
			options.Extras = extras;

			return PhotosGetNotInSet(options);
		}

		/// <summary>
		/// Gets a list of the authenticated users photos which are not in a set.
		/// </summary>
		/// <param name="options">A selection of options to filter/sort by.</param>
		/// <returns>A collection of photos in the <see cref="Photos"/> class.</returns>
		public Photos PhotosGetNotInSet(PartialSearchOptions options)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.getNotInSet");
			Utils.PartialOptionsIntoArray(options, parameters);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Photos;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Gets a list of all current licenses.
		/// </summary>
		/// <returns><see cref="Licenses"/> instance.</returns>
		public Licenses PhotosLicensesGetInfo()
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.licenses.getInfo");
			parameters.Add("api_key", _apiKey);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Licenses;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Remove an existing tag.
		/// </summary>
		/// <param name="tagId">The id of the tag, as returned by <see cref="Flickr.PhotosGetInfo(string)"/> or similar method.</param>
		/// <returns>True if the tag was removed.</returns>
		public bool PhotosRemoveTag(string tagId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.removeTag");
			parameters.Add("tag_id", tagId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return true;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Search for photos containing text, rather than tags.
		/// </summary>
		/// <param name="userId">The user whose photos you wish to search for.</param>
		/// <param name="text">The text you want to search for in titles and descriptions.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearchText(string userId, string text)
		{
			return PhotosSearch(userId, "", 0, text, DateTime.MinValue, DateTime.MinValue, 0, 0, 0, PhotoSearchExtras.All);
		}

		/// <summary>
		/// Search for photos containing text, rather than tags.
		/// </summary>
		/// <param name="userId">The user whose photos you wish to search for.</param>
		/// <param name="text">The text you want to search for in titles and descriptions.</param>
		/// <param name="extras">Optional extras to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearchText(string userId, string text, PhotoSearchExtras extras)
		{
			return PhotosSearch(userId, "", 0, text, DateTime.MinValue, DateTime.MinValue, 0, 0, 0, extras);
		}

		/// <summary>
		/// Search for photos containing text, rather than tags.
		/// </summary>
		/// <param name="userId">The user whose photos you wish to search for.</param>
		/// <param name="text">The text you want to search for in titles and descriptions.</param>
		/// <param name="license">The license type to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearchText(string userId, string text, int license)
		{
			return PhotosSearch(userId, "", 0, text, DateTime.MinValue, DateTime.MinValue, license, 0, 0, PhotoSearchExtras.All);
		}

		/// <summary>
		/// Search for photos containing text, rather than tags.
		/// </summary>
		/// <param name="userId">The user whose photos you wish to search for.</param>
		/// <param name="text">The text you want to search for in titles and descriptions.</param>
		/// <param name="license">The license type to return.</param>
		/// <param name="extras">Optional extras to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearchText(string userId, string text, int license, PhotoSearchExtras extras)
		{
			return PhotosSearch(userId, "", 0, text, DateTime.MinValue, DateTime.MinValue, license, 0, 0, extras);
		}

		/// <summary>
		/// Search for photos containing text, rather than tags.
		/// </summary>
		/// <param name="text">The text you want to search for in titles and descriptions.</param>
		/// <param name="extras">Optional extras to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearchText(string text, PhotoSearchExtras extras)
		{
			return PhotosSearch(null, "", 0, text, DateTime.MinValue, DateTime.MinValue, 0, 0, 0, extras);
		}

		/// <summary>
		/// Search for photos containing text, rather than tags.
		/// </summary>
		/// <param name="text">The text you want to search for in titles and descriptions.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearchText(string text)
		{
			return PhotosSearch(null, "", 0, text, DateTime.MinValue, DateTime.MinValue, 0, 0, 0, PhotoSearchExtras.All);
		}

		/// <summary>
		/// Search for photos containing text, rather than tags.
		/// </summary>
		/// <param name="text">The text you want to search for in titles and descriptions.</param>
		/// <param name="license">The license type to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearchText(string text, int license)
		{
			return PhotosSearch(null, "", 0, text, DateTime.MinValue, DateTime.MinValue, license, 0, 0, PhotoSearchExtras.All);
		}

		/// <summary>
		/// Search for photos containing text, rather than tags.
		/// </summary>
		/// <param name="text">The text you want to search for in titles and descriptions.</param>
		/// <param name="license">The license type to return.</param>
		/// <param name="extras">Optional extras to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearchText(string text, int license, PhotoSearchExtras extras)
		{
			return PhotosSearch(null, "", 0, text, DateTime.MinValue, DateTime.MinValue, license, 0, 0, extras);
		}

		/// <summary>
		/// Search for photos containing an array of tags.
		/// </summary>
		/// <param name="tags">An array of tags to search for.</param>
		/// <param name="extras">Optional extras to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string[] tags, PhotoSearchExtras extras)
		{
			return PhotosSearch(null, tags, 0, "", DateTime.MinValue, DateTime.MinValue, 0, 0, 0, extras);
		}

		/// <summary>
		/// Search for photos containing an array of tags.
		/// </summary>
		/// <param name="tags">An array of tags to search for.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string[] tags)
		{
			return PhotosSearch(null, tags, 0, "", DateTime.MinValue, DateTime.MinValue, 0, 0, 0, PhotoSearchExtras.All);
		}

		/// <summary>
		/// Search for photos containing an array of tags.
		/// </summary>
		/// <param name="tags">An array of tags to search for.</param>
		/// <param name="license">The license type to return.</param>
		/// <param name="extras">Optional extras to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string[] tags, int license, PhotoSearchExtras extras)
		{
			return PhotosSearch(null, tags, 0, "", DateTime.MinValue, DateTime.MinValue, license, 0, 0, extras);
		}

		/// <summary>
		/// Search for photos containing an array of tags.
		/// </summary>
		/// <param name="tags">An array of tags to search for.</param>
		/// <param name="license">The license type to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string[] tags, int license)
		{
			return PhotosSearch(null, tags, 0, "", DateTime.MinValue, DateTime.MinValue, license, 0, 0, PhotoSearchExtras.All);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="tags">An array of tags to search for.</param>
		/// <param name="tagMode">Match all tags, or any tag.</param>
		/// <param name="text">Text to search for in photo title or description.</param>
		/// <param name="perPage">Number of photos to return per page.</param>
		/// <param name="page">The page number to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string[] tags, TagMode tagMode, string text, int perPage, int page)
		{
			return PhotosSearch(null, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, perPage, page, PhotoSearchExtras.All);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="tags">An array of tags to search for.</param>
		/// <param name="tagMode">Match all tags, or any tag.</param>
		/// <param name="text">Text to search for in photo title or description.</param>
		/// <param name="perPage">Number of photos to return per page.</param>
		/// <param name="page">The page number to return.</param>
		/// <param name="extras">Optional extras to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string[] tags, TagMode tagMode, string text, int perPage, int page, PhotoSearchExtras extras)
		{
			return PhotosSearch(null, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, perPage, page, extras);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="tags">An array of tags to search for.</param>
		/// <param name="tagMode">Match all tags, or any tag.</param>
		/// <param name="text">Text to search for in photo title or description.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string[] tags, TagMode tagMode, string text)
		{
			return PhotosSearch(null, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, 0, 0, PhotoSearchExtras.All);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="tags">An array of tags to search for.</param>
		/// <param name="tagMode">Match all tags, or any tag.</param>
		/// <param name="text">Text to search for in photo title or description.</param>
		/// <param name="extras">Optional extras to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string[] tags, TagMode tagMode, string text, PhotoSearchExtras extras)
		{
			return PhotosSearch(null, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, 0, 0, extras);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="userId">The ID of the user to search the photos of.</param>
		/// <param name="tags">An array of tags to search for.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string userId, string[] tags)
		{
			return PhotosSearch(userId, tags, 0, "", DateTime.MinValue, DateTime.MinValue, 0, 0, 0, PhotoSearchExtras.All);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="userId">The ID of the user to search the photos of.</param>
		/// <param name="tags">An array of tags to search for.</param>
		/// <param name="extras">Optional extras to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string userId, string[] tags, PhotoSearchExtras extras)
		{
			return PhotosSearch(userId, tags, 0, "", DateTime.MinValue, DateTime.MinValue, 0, 0, 0, extras);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="userId">The ID of the user to search the photos of.</param>
		/// <param name="tags">An array of tags to search for.</param>
		/// <param name="license">The license type to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string userId, string[] tags, int license)
		{
			return PhotosSearch(userId, tags, 0, "", DateTime.MinValue, DateTime.MinValue, license, 0, 0, PhotoSearchExtras.All);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="userId">The ID of the user to search the photos of.</param>
		/// <param name="tags">An array of tags to search for.</param>
		/// <param name="license">The license type to return.</param>
		/// <param name="extras">Optional extras to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string userId, string[] tags, int license, PhotoSearchExtras extras)
		{
			return PhotosSearch(userId, tags, 0, "", DateTime.MinValue, DateTime.MinValue, license, 0, 0, extras);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="userId">The ID of the user to search the photos of.</param>
		/// <param name="tags">An array of tags to search for.</param>
		/// <param name="tagMode">Match all tags, or any tag.</param>
		/// <param name="text">Text to search for in photo title or description.</param>
		/// <param name="perPage">Number of photos to return per page.</param>
		/// <param name="page">The page number to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string userId, string[] tags, TagMode tagMode, string text, int perPage, int page)
		{
			return PhotosSearch(userId, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, perPage, page, PhotoSearchExtras.All);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="userId">The ID of the user to search the photos of.</param>
		/// <param name="tags">An array of tags to search for.</param>
		/// <param name="tagMode">Match all tags, or any tag.</param>
		/// <param name="text">Text to search for in photo title or description.</param>
		/// <param name="perPage">Number of photos to return per page.</param>
		/// <param name="page">The page number to return.</param>
		/// <param name="extras">Optional extras to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string userId, string[] tags, TagMode tagMode, string text, int perPage, int page, PhotoSearchExtras extras)
		{
			return PhotosSearch(userId, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, perPage, page, extras);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="userId">The ID of the user to search the photos of.</param>
		/// <param name="tags">An array of tags to search for.</param>
		/// <param name="tagMode">Match all tags, or any tag.</param>
		/// <param name="text">Text to search for in photo title or description.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string userId, string[] tags, TagMode tagMode, string text)
		{
			return PhotosSearch(userId, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, 0, 0, PhotoSearchExtras.All);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="userId">The ID of the user to search the photos of.</param>
		/// <param name="tags">An array of tags to search for.</param>
		/// <param name="tagMode">Match all tags, or any tag.</param>
		/// <param name="text">Text to search for in photo title or description.</param>
		/// <param name="extras">Optional extras to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string userId, string[] tags, TagMode tagMode, string text, PhotoSearchExtras extras)
		{
			return PhotosSearch(userId, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, 0, 0, extras);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="userId">The ID of the user to search the photos of.</param>
		/// <param name="tags">An array of tags to search for.</param>
		/// <param name="tagMode">Match all tags, or any tag.</param>
		/// <param name="text">Text to search for in photo title or description.</param>
		/// <param name="minUploadDate">The minimum upload date.</param>
		/// <param name="maxUploadDate">The maxmimum upload date.</param>
		/// <param name="license">The license type to return.</param>
		/// <param name="perPage">Number of photos to return per page.</param>
		/// <param name="page">The page number to return.</param>
		/// <param name="extras">Optional extras to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string userId, string[] tags, TagMode tagMode, string text, DateTime minUploadDate, DateTime maxUploadDate, int license, int perPage, int page, PhotoSearchExtras extras)
		{
			return PhotosSearch(userId, String.Join(",", tags), tagMode, text, minUploadDate, maxUploadDate, license, perPage, page, extras);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="userId">The ID of the user to search the photos of.</param>
		/// <param name="tags">An array of tags to search for.</param>
		/// <param name="tagMode">Match all tags, or any tag.</param>
		/// <param name="text">Text to search for in photo title or description.</param>
		/// <param name="minUploadDate">The minimum upload date.</param>
		/// <param name="maxUploadDate">The maxmimum upload date.</param>
		/// <param name="license">The license type to return.</param>
		/// <param name="perPage">Number of photos to return per page.</param>
		/// <param name="page">The page number to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string userId, string[] tags, TagMode tagMode, string text, DateTime minUploadDate, DateTime maxUploadDate, int license, int perPage, int page)
		{
			return PhotosSearch(userId, String.Join(",", tags), tagMode, text, minUploadDate, maxUploadDate, license, perPage, page, PhotoSearchExtras.All);
		}

		// PhotoSearch - tags versions

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="tags">A comma seperated list of tags to search for.</param>
		/// <param name="license">The license type to return.</param>
		/// <param name="extras">Optional extras to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string tags, int license, PhotoSearchExtras extras)
		{
			return PhotosSearch(null, tags, 0, "", DateTime.MinValue, DateTime.MinValue, license, 0, 0, extras);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="tags">A comma seperated list of tags to search for.</param>
		/// <param name="license">The license type to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string tags, int license)
		{
			return PhotosSearch(null, tags, 0, "", DateTime.MinValue, DateTime.MinValue, license, 0, 0, PhotoSearchExtras.All);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="tags">A comma seperated list of tags to search for.</param>
		/// <param name="tagMode">Match all tags, or any tag.</param>
		/// <param name="text">Text to search for in photo title or description.</param>
		/// <param name="perPage">Number of photos to return per page.</param>
		/// <param name="page">The page number to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string tags, TagMode tagMode, string text, int perPage, int page)
		{
			return PhotosSearch(null, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, perPage, page, PhotoSearchExtras.All);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="tags">A comma seperated list of tags to search for.</param>
		/// <param name="tagMode">Match all tags, or any tag.</param>
		/// <param name="text">Text to search for in photo title or description.</param>
		/// <param name="perPage">Number of photos to return per page.</param>
		/// <param name="page">The page number to return.</param>
		/// <param name="extras">Optional extras to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string tags, TagMode tagMode, string text, int perPage, int page, PhotoSearchExtras extras)
		{
			return PhotosSearch(null, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, perPage, page, extras);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="tags">A comma seperated list of tags to search for.</param>
		/// <param name="tagMode">Match all tags, or any tag.</param>
		/// <param name="text">Text to search for in photo title or description.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string tags, TagMode tagMode, string text)
		{
			return PhotosSearch(null, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, 0, 0, PhotoSearchExtras.All);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="tags">A comma seperated list of tags to search for.</param>
		/// <param name="tagMode">Match all tags, or any tag.</param>
		/// <param name="text">Text to search for in photo title or description.</param>
		/// <param name="extras">Optional extras to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string tags, TagMode tagMode, string text, PhotoSearchExtras extras)
		{
			return PhotosSearch(null, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, 0, 0, extras);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="userId">The ID of the user to search the photos of.</param>
		/// <param name="tags">A comma seperated list of tags to search for.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string userId, string tags)
		{
			return PhotosSearch(userId, tags, 0, "", DateTime.MinValue, DateTime.MinValue, 0, 0, 0, PhotoSearchExtras.All);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="userId">The ID of the user to search the photos of.</param>
		/// <param name="tags">A comma seperated list of tags to search for.</param>
		/// <param name="extras">Optional extras to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string userId, string tags, PhotoSearchExtras extras)
		{
			return PhotosSearch(userId, tags, 0, "", DateTime.MinValue, DateTime.MinValue, 0, 0, 0, extras);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="userId">The ID of the user to search the photos of.</param>
		/// <param name="tags">A comma seperated list of tags to search for.</param>
		/// <param name="license">The license type to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string userId, string tags, int license)
		{
			return PhotosSearch(userId, tags, 0, "", DateTime.MinValue, DateTime.MinValue, license, 0, 0, PhotoSearchExtras.All);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="userId">The ID of the user to search the photos of.</param>
		/// <param name="tags">A comma seperated list of tags to search for.</param>
		/// <param name="license">The license type to return.</param>
		/// <param name="extras">Optional extras to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string userId, string tags, int license, PhotoSearchExtras extras)
		{
			return PhotosSearch(userId, tags, 0, "", DateTime.MinValue, DateTime.MinValue, license, 0, 0, extras);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="userId">The ID of the user to search the photos of.</param>
		/// <param name="tags">A comma seperated list of tags to search for.</param>
		/// <param name="tagMode">Match all tags, or any tag.</param>
		/// <param name="text">Text to search for in photo title or description.</param>
		/// <param name="perPage">Number of photos to return per page.</param>
		/// <param name="page">The page number to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string userId, string tags, TagMode tagMode, string text, int perPage, int page)
		{
			return PhotosSearch(userId, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, perPage, page, PhotoSearchExtras.All);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="userId">The ID of the user to search the photos of.</param>
		/// <param name="tags">A comma seperated list of tags to search for.</param>
		/// <param name="tagMode">Match all tags, or any tag.</param>
		/// <param name="text">Text to search for in photo title or description.</param>
		/// <param name="perPage">Number of photos to return per page.</param>
		/// <param name="page">The page number to return.</param>
		/// <param name="extras">Optional extras to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string userId, string tags, TagMode tagMode, string text, int perPage, int page, PhotoSearchExtras extras)
		{
			return PhotosSearch(userId, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, perPage, page, extras);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="userId">The ID of the user to search the photos of.</param>
		/// <param name="tags">A comma seperated list of tags to search for.</param>
		/// <param name="tagMode">Match all tags, or any tag.</param>
		/// <param name="text">Text to search for in photo title or description.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string userId, string tags, TagMode tagMode, string text)
		{
			return PhotosSearch(userId, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, 0, 0, PhotoSearchExtras.All);
		}

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="userId">The ID of the user to search the photos of.</param>
		/// <param name="tags">A comma seperated list of tags to search for.</param>
		/// <param name="tagMode">Match all tags, or any tag.</param>
		/// <param name="text">Text to search for in photo title or description.</param>
		/// <param name="extras">Optional extras to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string userId, string tags, TagMode tagMode, string text, PhotoSearchExtras extras)
		{
			return PhotosSearch(userId, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, 0, 0, extras);
		}

		// Actual PhotoSearch function

		/// <summary>
		/// Search for photos.
		/// </summary>
		/// <param name="userId">The ID of the user to search the photos of.</param>
		/// <param name="tags">A comma seperated list of tags to search for.</param>
		/// <param name="tagMode">Match all tags, or any tag.</param>
		/// <param name="text">Text to search for in photo title or description.</param>
		/// <param name="perPage">Number of photos to return per page.</param>
		/// <param name="page">The page number to return.</param>
		/// <param name="extras">Optional extras to return.</param>
		/// <param name="minUploadDate">The minimum upload date.</param>
		/// <param name="maxUploadDate">The maxmimum upload date.</param>
		/// <param name="license">The license type to return.</param>
		/// <returns>A <see cref="Photos"/> instance.</returns>
		public Photos PhotosSearch(string userId, string tags, TagMode tagMode, string text, DateTime minUploadDate, DateTime maxUploadDate, int license, int perPage, int page, PhotoSearchExtras extras)
		{
			PhotoSearchOptions options = new PhotoSearchOptions();
			options.UserId = userId;
			options.Tags = tags;
			options.TagMode = tagMode;
			options.Text = text;
			options.MinUploadDate = minUploadDate;
			options.MaxUploadDate = maxUploadDate;
			options.AddLicense(license);
			options.PerPage = perPage;
			options.Page = page;
			options.Extras = extras;

			return PhotosSearch(options);
		}

		/// <summary>
		/// Search for a set of photos, based on the value of the <see cref="PhotoSearchOptions"/> parameters.
		/// </summary>
		/// <param name="options">The parameters to search for.</param>
		/// <returns>A collection of photos contained within a <see cref="Photos"/> object.</returns>
		public Photos PhotosSearch(PhotoSearchOptions options)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.search");
			if( options.UserId != null && options.UserId.Length > 0 ) parameters.Add("user_id", options.UserId);
			if( options.Text != null && options.Text.Length > 0 ) parameters.Add("text", options.Text);
			if( options.Tags != null && options.Tags.Length > 0 ) parameters.Add("tags", options.Tags);
			if( options.TagMode != TagMode.None ) parameters.Add("tag_mode", options.TagModeString);
			if( options.MinUploadDate != DateTime.MinValue ) parameters.Add("min_upload_date", Utils.DateToUnixTimestamp(options.MinUploadDate).ToString());
			if( options.MaxUploadDate != DateTime.MinValue ) parameters.Add("max_upload_date", Utils.DateToUnixTimestamp(options.MaxUploadDate).ToString());
			if( options.MinTakenDate != DateTime.MinValue ) parameters.Add("min_taken_date", options.MinTakenDate.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo));
			if( options.MaxTakenDate != DateTime.MinValue ) parameters.Add("max_taken_date", options.MaxTakenDate.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo));
			if( options.Licenses.Length != 0 ) 
			{
				string lic = "";
				for(int i = 0; i < options.Licenses.Length; i++)
				{
					if( i > 0 ) lic += ",";
					lic += Convert.ToString(options.Licenses[i]);
				}
				parameters.Add("license", lic);
			}
			if( options.PerPage != 0 ) parameters.Add("per_page", options.PerPage.ToString());
			if( options.Page != 0 ) parameters.Add("page", options.Page.ToString());
			if( options.Extras != PhotoSearchExtras.None ) parameters.Add("extras", options.ExtrasString);
			if( options.SortOrder != PhotoSearchSortOrder.None ) parameters.Add("sort", options.SortOrderString);
			if( options.PrivacyFilter != PrivacyFilter.None ) parameters.Add("privacy_filter", options.PrivacyFilter.ToString("d"));
			if( options.BoundaryBox.IsSet ) parameters.Add("bbox", options.BoundaryBox.ToString());
			if( options.Accuracy != GeoAccuracy.None ) parameters.Add("accuracy", options.Accuracy.ToString("d"));

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Photos;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Set the date taken for a photo.
		/// </summary>
		/// <remarks>
		/// All dates are assumed to be GMT. It is the developers responsibility to change dates to the local users 
		/// timezone.
		/// </remarks>
		/// <param name="photoId">The id of the photo to set the date taken for.</param>
		/// <param name="dateTaken">The date taken.</param>
		/// <param name="granularity">The granularity of the date taken.</param>
		/// <returns>True if the date was updated successfully.</returns>
		public bool PhotosSetDates(string photoId, DateTime dateTaken, DateGranularity granularity)
		{
			return PhotosSetDates(photoId, DateTime.MinValue, dateTaken, granularity);
		}
		
		/// <summary>
		/// Set the date the photo was posted (uploaded). This will affect the order in which photos
		/// are seen in your photostream.
		/// </summary>
		/// <remarks>
		/// All dates are assumed to be GMT. It is the developers responsibility to change dates to the local users 
		/// timezone.
		/// </remarks>
		/// <param name="photoId">The id of the photo to set the date posted.</param>
		/// <param name="datePosted">The new date to set the date posted too.</param>
		/// <returns>True if the date was updated successfully.</returns>
		public bool PhotosSetDates(string photoId, DateTime datePosted)
		{
			return PhotosSetDates(photoId, datePosted, DateTime.MinValue, DateGranularity.FullDate);
		}
		
		/// <summary>
		/// Set the date the photo was posted (uploaded) and the date the photo was taken.
		/// Changing the date posted will affect the order in which photos are seen in your photostream.
		/// </summary>
		/// <remarks>
		/// All dates are assumed to be GMT. It is the developers responsibility to change dates to the local users 
		/// timezone.
		/// </remarks>
		/// <param name="photoId">The id of the photo to set the dates.</param>
		/// <param name="datePosted">The new date to set the date posted too.</param>
		/// <param name="dateTaken">The new date to set the date taken too.</param>
		/// <param name="granularity">The granularity of the date taken.</param>
		/// <returns>True if the dates where updated successfully.</returns>
		public bool PhotosSetDates(string photoId, DateTime datePosted, DateTime dateTaken, DateGranularity granularity)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.setDates");
			parameters.Add("photo_id", photoId);
			if( datePosted != DateTime.MinValue ) parameters.Add("date_posted", Utils.DateToUnixTimestamp(datePosted).ToString());
			if( dateTaken != DateTime.MinValue ) 
			{
				parameters.Add("date_taken", dateTaken.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo));
				parameters.Add("date_taken_granularity", granularity.ToString("d"));
			}

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return true;
			}
			else
			{
				throw new FlickrException(response.Error);
			}

		}

		/// <summary>
		/// Sets the title and description of the photograph.
		/// </summary>
		/// <param name="photoId">The numerical photoId of the photograph.</param>
		/// <param name="title">The new title of the photograph.</param>
		/// <param name="description">The new description of the photograph.</param>
		/// <returns>True when the operation is successful.</returns>
		/// <exception cref="FlickrException">Thrown when the photo id cannot be found.</exception>
		public bool PhotosSetMeta(string photoId, string title, string description)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.setMeta");
			parameters.Add("photo_id", photoId);
			parameters.Add("title", title);
			parameters.Add("description", description);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return true;
			}
			else
			{
				throw new FlickrException(response.Error);
			}

		}

		/// <summary>
		/// Set the permissions on a photo.
		/// </summary>
		/// <param name="photoId">The id of the photo to update.</param>
		/// <param name="isPublic">1 if the photo is public, 0 if it is not.</param>
		/// <param name="isFriend">1 if the photo is viewable by friends, 0 if it is not.</param>
		/// <param name="isFamily">1 if the photo is viewable by family, 0 if it is not.</param>
		/// <param name="permComment">Who can add comments. See <see cref="PermissionComment"/> for more details.</param>
		/// <param name="permAddMeta">Who can add metadata (notes and tags). See <see cref="PermissionAddMeta"/> for more details.</param>
		public void PhotosSetPerms(string photoId, int isPublic, int isFriend, int isFamily, PermissionComment permComment, PermissionAddMeta permAddMeta)
		{
			PhotosSetPerms(photoId, (isPublic==1), (isFriend==1), (isFamily==1), permComment, permAddMeta);
		}

		/// <summary>
		/// Set the permissions on a photo.
		/// </summary>
		/// <param name="photoId">The id of the photo to update.</param>
		/// <param name="isPublic">True if the photo is public, False if it is not.</param>
		/// <param name="isFriend">True if the photo is viewable by friends, False if it is not.</param>
		/// <param name="isFamily">True if the photo is viewable by family, False if it is not.</param>
		/// <param name="permComment">Who can add comments. See <see cref="PermissionComment"/> for more details.</param>
		/// <param name="permAddMeta">Who can add metadata (notes and tags). See <see cref="PermissionAddMeta"/> for more details.</param>
		public void PhotosSetPerms(string photoId, bool isPublic, bool isFriend, bool isFamily, PermissionComment permComment, PermissionAddMeta permAddMeta)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.setPerms");
			parameters.Add("photo_id", photoId);
			parameters.Add("is_public", (isPublic?"1":"0"));
			parameters.Add("is_friend", (isFriend?"1":"0"));
			parameters.Add("is_family", (isFamily?"1":"0"));
			parameters.Add("perm_comment", permComment.ToString("d"));
			parameters.Add("perm_addmeta", permAddMeta.ToString("d"));

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return;
			}
			else
			{
				throw new FlickrException(response.Error);
			}

		}

		/// <summary>
		/// Set the tags for a photo.
		/// </summary>
		/// <remarks>
		/// This will remove all old tags and add these new ones specified. See <see cref="PhotosAddTags"/>
		/// to just add new tags without deleting old ones.
		/// </remarks>
		/// <param name="photoId">The id of the photo to update.</param>
		/// <param name="tags">An array of tags.</param>
		/// <returns>True if the photo was updated successfully.</returns>
		public bool PhotosSetTags(string photoId, string[] tags)
		{
			string s = string.Join(",", tags);
			return PhotosSetTags(photoId, s);
		}
			
		/// <summary>
		/// Set the tags for a photo.
		/// </summary>
		/// <remarks>
		/// This will remove all old tags and add these new ones specified. See <see cref="PhotosAddTags"/>
		/// to just add new tags without deleting old ones.
		/// </remarks>
		/// <param name="photoId">The id of the photo to update.</param>
		/// <param name="tags">An comma-seperated list of tags.</param>
		/// <returns>True if the photo was updated successfully.</returns>
		public bool PhotosSetTags(string photoId, string tags)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.setTags");
			parameters.Add("photo_id", photoId);
			parameters.Add("tags", tags);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return true;
			}
			else
			{
				throw new FlickrException(response.Error);
			}

		}
		#endregion

		#region [ Photos Comments ]
		/// <summary>
		/// Gets a list of comments for a photo.
		/// </summary>
		/// <param name="photoId">The id of the photo to return the comments for.</param>
		/// <returns>An array of <see cref="Comment"/> objects.</returns>
		public Comment[] PhotosCommentsGetList(string photoId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.comments.getList");
			parameters.Add("photo_id", photoId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return PhotoComments.GetComments(response.AllElements[0]);
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Adds a new comment to a photo.
		/// </summary>
		/// <param name="photoId">The ID of the photo to add the comment to.</param>
		/// <param name="commentText">The text of the comment. Can contain some HTML.</param>
		/// <returns>The new ID of the created comment.</returns>
		public string PhotosCommentsAddComment(string photoId, string commentText)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.comments.addComment");
			parameters.Add("photo_id", photoId);
			parameters.Add("comment_text", commentText);

			FlickrNet.Response response = GetResponseNoCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				XmlNode node = response.AllElements[0];
				if( node.Attributes.GetNamedItem("id") != null )
					return node.Attributes.GetNamedItem("id").Value;
				else
					throw new FlickrException(9001, "Comment ID not found");
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Deletes a comment from a photo.
		/// </summary>
		/// <param name="commentId">The ID of the comment to delete.</param>
		public void PhotosCommentsDeleteComment(string commentId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.comments.deleteComment");
			parameters.Add("comment_id", commentId);

			FlickrNet.Response response = GetResponseNoCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Edits a comment.
		/// </summary>
		/// <param name="commentId">The ID of the comment to edit.</param>
		/// <param name="commentText">The new text for the comment.</param>
		public void PhotosCommentsEditComment(string commentId, string commentText)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.comments.editComment");
			parameters.Add("comment_id", commentId);
			parameters.Add("comment_text", commentText);

			FlickrNet.Response response = GetResponseNoCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}
		#endregion

		#region [ Photosets ]
		/// <summary>
		/// Add a photo to a photoset.
		/// </summary>
		/// <param name="photosetId">The ID of the photoset to add the photo to.</param>
		/// <param name="photoId">The ID of the photo to add.</param>
		public void PhotosetsAddPhoto(string photosetId, string photoId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photosets.addPhoto");
			parameters.Add("photoset_id", photosetId);
			parameters.Add("photo_id", photoId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Creates a blank photoset, with a title and a primary photo (minimum requirements).
		/// </summary>
		/// <param name="title">The title of the photoset.</param>
		/// <param name="primaryPhotoId">The ID of the photo which will be the primary photo for the photoset. This photo will also be added to the set.</param>
		/// <returns>The <see cref="Photoset"/> that is created.</returns>
		public Photoset PhotosetsCreate(string title, string primaryPhotoId)
		{
			return PhotosetsCreate(title, null, primaryPhotoId);
		}

		/// <summary>
		/// Creates a blank photoset, with a title, description and a primary photo.
		/// </summary>
		/// <param name="title">The title of the photoset.</param>
		/// <param name="description">THe description of the photoset.</param>
		/// <param name="primaryPhotoId">The ID of the photo which will be the primary photo for the photoset. This photo will also be added to the set.</param>
		/// <returns>The <see cref="Photoset"/> that is created.</returns>
		public Photoset PhotosetsCreate(string title, string description, string primaryPhotoId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photosets.create");
			parameters.Add("title", title);
			parameters.Add("primary_photo_id", primaryPhotoId);
			parameters.Add("description", description);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Photoset;
			}
			else
			{
				throw new FlickrException(response.Error);
			}

		}

		/// <summary>
		/// Deletes the specified photoset.
		/// </summary>
		/// <param name="photosetId">The ID of the photoset to delete.</param>
		/// <returns>Returns true when the photoset has been deleted.</returns>
		public bool PhotosetsDelete(string photosetId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photosets.delete");
			parameters.Add("photoset_id", photosetId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return true;
			}
			else
			{
				throw new FlickrException(response.Error);
			}

		}

		/// <summary>
		/// Updates the title and description for a photoset.
		/// </summary>
		/// <param name="photosetId">The ID of the photoset to update.</param>
		/// <param name="title">The new title for the photoset.</param>
		/// <param name="description">The new description for the photoset.</param>
		/// <returns>Returns true when the photoset has been updated.</returns>
		public bool PhotosetsEditMeta(string photosetId, string title, string description)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photosets.editMeta");
			parameters.Add("photoset_id", photosetId);
			parameters.Add("title", title);
			parameters.Add("description", description);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return true;
			}
			else
			{
				throw new FlickrException(response.Error);
			}

		}

		/// <summary>
		/// Sets the photos for a photoset.
		/// </summary>
		/// <remarks>
		/// Will remove any previous photos from the photoset. 
		/// The order in thich the photoids are given is the order they will appear in the 
		/// photoset page.
		/// </remarks>
		/// <param name="photosetId">The ID of the photoset to update.</param>
		/// <param name="primaryPhotoId">The ID of the new primary photo for the photoset.</param>
		/// <param name="photoIds">An array of photo IDs.</param>
		/// <returns>Returns true when the photoset has been updated.</returns>
		public bool PhotosetsEditPhotos(string photosetId, string primaryPhotoId, string[] photoIds)
		{
			return PhotosetsEditPhotos(photosetId, primaryPhotoId, string.Join(",", photoIds));
		}


		/// <summary>
		/// Sets the photos for a photoset.
		/// </summary>
		/// <remarks>
		/// Will remove any previous photos from the photoset. 
		/// The order in thich the photoids are given is the order they will appear in the 
		/// photoset page.
		/// </remarks>
		/// <param name="photosetId">The ID of the photoset to update.</param>
		/// <param name="primaryPhotoId">The ID of the new primary photo for the photoset.</param>
		/// <param name="photoIds">An comma seperated list of photo IDs.</param>
		/// <returns>Returns true when the photoset has been updated.</returns>
		public bool PhotosetsEditPhotos(string photosetId, string primaryPhotoId, string photoIds)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photosets.editPhotos");
			parameters.Add("photoset_id", photosetId);
			parameters.Add("primary_photo_id", primaryPhotoId);
			parameters.Add("photo_ids", photoIds);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return true;
			}
			else
			{
				throw new FlickrException(response.Error);
			}

		}

		/// <summary>
		/// Gets the context of the specified photo within the photoset.
		/// </summary>
		/// <param name="photoId">The photo id of the photo in the set.</param>
		/// <param name="photosetId">The id of the set.</param>
		/// <returns><see cref="Context"/> of the specified photo.</returns>
		public Context PhotosetsGetContext(string photoId, string photosetId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photosets.getContext");
			parameters.Add("photo_id", photoId);
			parameters.Add("photoset_id", photosetId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				Context c = new Context();
				c.Count = response.ContextCount.Count;
				c.NextPhoto = response.ContextNextPhoto;
				c.PreviousPhoto = response.ContextPrevPhoto;

				return c;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Gets the information about a photoset.
		/// </summary>
		/// <param name="photosetId">The ID of the photoset to return information for.</param>
		/// <returns>A <see cref="Photoset"/> instance.</returns>
		public Photoset PhotosetsGetInfo(string photosetId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photosets.getInfo");
			parameters.Add("photoset_id", photosetId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Photoset;
			}
			else
			{
				throw new FlickrException(response.Error);
			}

		}

		/// <summary>
		/// Gets a list of the currently authenticated users photosets.
		/// </summary>
		/// <returns>A <see cref="Photosets"/> instance containing a collection of photosets.</returns>
		public Photosets PhotosetsGetList()
		{
			return PhotosetsGetList(null);
		}

		/// <summary>
		/// Gets a list of the specified users photosets.
		/// </summary>
		/// <param name="userId">The ID of the user to return the photosets of.</param>
		/// <returns>A <see cref="Photosets"/> instance containing a collection of photosets.</returns>
		public Photosets PhotosetsGetList(string userId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photosets.getList");
			if( userId != null ) parameters.Add("user_id", userId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Photosets;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Gets a collection of photos for a photoset.
		/// </summary>
		/// <param name="photosetId">The ID of the photoset to return photos for.</param>
		/// <returns>An array of <see cref="Photo"/> instances.</returns>
		public Photo[] PhotosetsGetPhotos(string photosetId)
		{
			return PhotosetsGetPhotos(photosetId, PhotoSearchExtras.All, PrivacyFilter.None);
		}

		/// <summary>
		/// Gets a collection of photos for a photoset.
		/// </summary>
		/// <param name="photosetId">The ID of the photoset to return photos for.</param>
		/// <param name="privacyFilter">The privacy filter to search on.</param>
		/// <returns>An array of <see cref="Photo"/> instances.</returns>
		public Photo[] PhotosetsGetPhotos(string photosetId, PrivacyFilter privacyFilter)
		{
			return PhotosetsGetPhotos(photosetId, PhotoSearchExtras.All, privacyFilter);
		}

		/// <summary>
		/// Gets a collection of photos for a photoset.
		/// </summary>
		/// <param name="photosetId">The ID of the photoset to return photos for.</param>
		/// <param name="extras">The extras to return for each photo.</param>
		/// <returns>An array of <see cref="Photo"/> instances.</returns>
		public Photo[] PhotosetsGetPhotos(string photosetId, PhotoSearchExtras extras)
		{
			return PhotosetsGetPhotos(photosetId, extras, PrivacyFilter.None);
		}

		/// <summary>
		/// Gets a collection of photos for a photoset.
		/// </summary>
		/// <param name="photosetId">The ID of the photoset to return photos for.</param>
		/// <param name="extras">The extras to return for each photo.</param>
		/// <param name="privacyFilter">The privacy filter to search on.</param>
		/// <returns>An array of <see cref="Photo"/> instances.</returns>
		public Photo[] PhotosetsGetPhotos(string photosetId, PhotoSearchExtras extras, PrivacyFilter privacyFilter)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photosets.getPhotos");
			parameters.Add("photoset_id", photosetId);
			if( extras != PhotoSearchExtras.None ) parameters.Add("extras", Utils.ExtrasToString(extras));
			if( privacyFilter != PrivacyFilter.None ) parameters.Add("privacy_filter", privacyFilter.ToString("d"));

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				if( response.Photoset.PhotoCollection == null ) return new Photo[0];
				if( response.Photoset.OwnerId != null && response.Photoset.OwnerId.Length > 0 )
				{
					foreach(Photo p in response.Photoset.PhotoCollection)
					{
						p.UserId = response.Photoset.OwnerId;
					}
				}
				return response.Photoset.PhotoCollection;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Changes the order of your photosets.
		/// </summary>
		/// <param name="photosetIds">An array of photoset IDs, 
		/// ordered with the set to show first, first in the list. 
		/// Any set IDs not given in the list will be set to appear at the end of the list, ordered by their IDs.</param>
		public void PhotosetsOrderSets(string[] photosetIds)
		{
			PhotosetsOrderSets(string.Join(",", photosetIds));
		}

		/// <summary>
		/// Changes the order of your photosets.
		/// </summary>
		/// <param name="photosetIds">A comma delimited list of photoset IDs, 
		/// ordered with the set to show first, first in the list. 
		/// Any set IDs not given in the list will be set to appear at the end of the list, ordered by their IDs.</param>
		public void PhotosetsOrderSets(string photosetIds)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photosets.orderSets");
			parameters.Add("photosetIds", photosetIds);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Removes a photo from a photoset.
		/// </summary>
		/// <remarks>
		/// An exception will be raised if the photo is not in the set.
		/// </remarks>
		/// <param name="photosetId">The ID of the photoset to remove the photo from.</param>
		/// <param name="photoId">The ID of the photo to remove.</param>
		public void PhotosetsRemovePhoto(string photosetId, string photoId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photosets.removePhoto");
			parameters.Add("photoset_id", photosetId);
			parameters.Add("photo_id", photoId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}
		#endregion

		#region [ Photoset Comments ]
		/// <summary>
		/// Gets a list of comments for a photoset.
		/// </summary>
		/// <param name="photosetId">The id of the photoset to return the comments for.</param>
		/// <returns>An array of <see cref="Comment"/> objects.</returns>
		public Comment[] PhotosetsCommentsGetList(string photosetId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photosets.comments.getList");
			parameters.Add("photoset_id", photosetId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return PhotoComments.GetComments(response.AllElements[0]);
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Adds a new comment to a photoset.
		/// </summary>
		/// <param name="photosetId">The ID of the photoset to add the comment to.</param>
		/// <param name="commentText">The text of the comment. Can contain some HTML.</param>
		/// <returns>The new ID of the created comment.</returns>
		public string PhotosetsCommentsAddComment(string photosetId, string commentText)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photosets.comments.addComment");
			parameters.Add("photoset_id", photosetId);
			parameters.Add("comment_text", commentText);

			FlickrNet.Response response = GetResponseNoCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				XmlNode node = response.AllElements[0];
				if( node.Attributes.GetNamedItem("id") != null )
					return node.Attributes.GetNamedItem("id").Value;
				else
					throw new FlickrException(9001, "Comment ID not found");
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Deletes a comment from a photoset.
		/// </summary>
		/// <param name="commentId">The ID of the comment to delete.</param>
		public void PhotosetsCommentsDeleteComment(string commentId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photosets.comments.deleteComment");
			parameters.Add("comment_id", commentId);

			FlickrNet.Response response = GetResponseNoCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Edits a comment.
		/// </summary>
		/// <param name="commentId">The ID of the comment to edit.</param>
		/// <param name="commentText">The new text for the comment.</param>
		public void PhotosetsCommentsEditComment(string commentId, string commentText)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photosets.comments.editComment");
			parameters.Add("comment_id", commentId);
			parameters.Add("comment_text", commentText);

			FlickrNet.Response response = GetResponseNoCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}
		#endregion

		#region [ Tags ]
		/// <summary>
		/// Get the tag list for a given photo.
		/// </summary>
		/// <param name="photoId">The id of the photo to return tags for.</param>
		/// <returns>An instance of the <see cref="PhotoInfo"/> class containing only the <see cref="PhotoInfo.Tags"/> property.</returns>
		public PhotoInfo TagsGetListPhoto(string photoId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.tags.getListPhoto");
			parameters.Add("api_key", _apiKey);
			parameters.Add("photo_id", photoId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.PhotoInfo;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Get the tag list for a given user (or the currently logged in user).
		/// </summary>
		/// <returns>An array of <see cref="Tag"/> objects.</returns>
		public Tag[] TagsGetListUser()
		{
			return TagsGetListUser(null);
		}

		/// <summary>
		/// Get the tag list for a given user (or the currently logged in user).
		/// </summary>
		/// <param name="userId">The NSID of the user to fetch the tag list for. If this argument is not specified, the currently logged in user (if any) is assumed.</param>
		/// <returns>An array of <see cref="Tag"/> objects.</returns>
		public Tag[] TagsGetListUser(string userId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.tags.getListUser");
			if( userId != null && userId.Length > 0 ) parameters.Add("user_id", userId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				XmlNodeList nodes = response.AllElements[0].SelectNodes("//tag");
				Tag[] tags = new Tag[nodes.Count];
				for(int i = 0; i < tags.Length; i++)
				{
					tags[i] = new Tag(nodes[i]);
				}
				return tags;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Get the popular tags for a given user (or the currently logged in user).
		/// </summary>
		/// <returns>An array of <see cref="Tag"/> objects.</returns>
		public Tag[] TagsGetListUserPopular()
		{
			return TagsGetListUserPopular(null, 0);
		}
			
		/// <summary>
		/// Get the popular tags for a given user (or the currently logged in user).
		/// </summary>
		/// <param name="count">Number of popular tags to return. defaults to 10 when this argument is not present.</param>
		/// <returns>An array of <see cref="Tag"/> objects.</returns>
		public Tag[] TagsGetListUserPopular(int count)
		{
			return TagsGetListUserPopular(null, count);
		}
			
		/// <summary>
		/// Get the popular tags for a given user (or the currently logged in user).
		/// </summary>
		/// <param name="userId">The NSID of the user to fetch the tag list for. If this argument is not specified, the currently logged in user (if any) is assumed.</param>
		/// <returns>An array of <see cref="Tag"/> objects.</returns>
		public Tag[] TagsGetListUserPopular(string userId)
		{
			return TagsGetListUserPopular(userId, 0);
		}
			
		/// <summary>
		/// Get the popular tags for a given user (or the currently logged in user).
		/// </summary>
		/// <param name="userId">The NSID of the user to fetch the tag list for. If this argument is not specified, the currently logged in user (if any) is assumed.</param>
		/// <param name="count">Number of popular tags to return. defaults to 10 when this argument is not present.</param>
		/// <returns>An array of <see cref="Tag"/> objects.</returns>
		public Tag[] TagsGetListUserPopular(string userId, long count)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.tags.getListUserPopular");
			if( userId != null ) parameters.Add("user_id", userId);
			if( count > 0 ) parameters.Add("count", count.ToString());

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				XmlNodeList nodes = response.AllElements[0].SelectNodes("//tag");
				Tag[] tags = new Tag[nodes.Count];
				for(int i = 0; i < tags.Length; i++)
				{
					tags[i] = new Tag(nodes[i]);
				}
				return tags;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Returns a list of tags 'related' to the given tag, based on clustered usage analysis.
		/// </summary>
		/// <param name="tag">The tag to fetch related tags for.</param>
		/// <returns>An array of <see cref="Tag"/> objects.</returns>
		public Tag[] TagsGetRelated(string tag)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.tags.getRelated");
			parameters.Add("api_key", _apiKey);
			parameters.Add("tag", tag);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				XmlNodeList nodes = response.AllElements[0].SelectNodes("//tag");
				Tag[] tags = new Tag[nodes.Count];
				for(int i = 0; i < tags.Length; i++)
				{
					tags[i] = new Tag(nodes[i]);
				}
				return tags;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		#endregion

		#region [ Transform ]

		/// <summary>
		/// Rotates a photo on Flickr.
		/// </summary>
		/// <remarks>
		/// Does not rotate the original photo.
		/// </remarks>
		/// <param name="photoId">The ID of the photo.</param>
		/// <param name="degrees">The number of degrees to rotate by. Valid values are 90, 180 and 270.</param>
		public void TransformRotate(string photoId, int degrees)
		{
			if( photoId == null ) 
				throw new ArgumentNullException("photoId");
			if( degrees != 90 && degrees != 180 && degrees != 270 )
				throw new ArgumentException("Must be 90, 180 or 270", "degrees");

			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.transform.rotate");
			parameters.Add("photo_id", photoId);
			parameters.Add("degrees", degrees.ToString("0"));

			FlickrNet.Response response = GetResponseNoCache(parameters);
			if( response.Status == ResponseStatus.OK )
			{
				return;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		#endregion

		#region	[ Geo ]
		/// <summary>
		/// Returns the location data for a give photo.
		/// </summary>
		/// <param name="photoId">The ID of the photo to return the location information for.</param>
		/// <returns>Returns null if the photo has no location information, otherwise returns the location information.</returns>
		public PhotoLocation PhotosGeoGetLocation(string photoId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.geo.getLocation");
			parameters.Add("photo_id", photoId);

			FlickrNet.Response response = GetResponseCache(parameters);
			if( response.Status == ResponseStatus.OK )
			{
				return response.PhotoInfo.Location;
			}
			else
			{
				if( response.Error.Code == 2 )
					return null;
				else
					throw new FlickrException(response.Error);
			}
		}
		/// <summary>
		/// Sets the geo location for a photo.
		/// </summary>
		/// <param name="photoId">The ID of the photo to set to location for.</param>
		/// <param name="latitude">The latitude of the geo location. A double number ranging from -180.00 to 180.00. Digits beyond 6 decimal places will be truncated.</param>
		/// <param name="longitude">The longitude of the geo location. A double number ranging from -180.00 to 180.00. Digits beyond 6 decimal places will be truncated.</param>
		public void PhotosGeoSetLocation(string photoId, double latitude, double longitude)
		{
			PhotosGeoSetLocation(photoId, latitude, longitude, GeoAccuracy.None);
		}

		/// <summary>
		/// Sets the geo location for a photo.
		/// </summary>
		/// <param name="photoId">The ID of the photo to set to location for.</param>
		/// <param name="latitude">The latitude of the geo location. A double number ranging from -180.00 to 180.00. Digits beyond 6 decimal places will be truncated.</param>
		/// <param name="longitude">The longitude of the geo location. A double number ranging from -180.00 to 180.00. Digits beyond 6 decimal places will be truncated.</param>
		/// <param name="accuracy">The accuracy of the photos geo location.</param>
		public void PhotosGeoSetLocation(string photoId, double latitude, double longitude, GeoAccuracy accuracy)
		{
			System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.geo.setLocation");
			parameters.Add("photo_id", photoId);
			parameters.Add("lat", latitude.ToString(nfi));
			parameters.Add("lon", longitude.ToString(nfi));
			if( accuracy != GeoAccuracy.None )
				parameters.Add("accuracy", ((int)accuracy).ToString());

			FlickrNet.Response response = GetResponseNoCache(parameters);
			if( response.Status == ResponseStatus.OK )
			{
				return;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Removes Location information.
		/// </summary>
		/// <param name="photoId">The photo ID of the photo to remove information from.</param>
		/// <returns>Returns true if the location information as found and removed. Returns false if no photo information was found.</returns>
		public bool PhotosGeoRemoveLocation(string photoId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.geo.removeLocation");
			parameters.Add("photo_id", photoId);

			FlickrNet.Response response = GetResponseNoCache(parameters);
			if( response.Status == ResponseStatus.OK )
			{
				return true;
			}
			else
			{
				if( response.Error.Code == 2 )
					return false;
				else
					throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Gets a list of photos that do not contain geo location information.
		/// </summary>
		/// <returns>A list of photos that do not contain location information.</returns>
		public Photos PhotosGetWithoutGeoData()
		{
			PartialSearchOptions options = new PartialSearchOptions();
			return PhotosGetWithoutGeoData(options);
		}

		/// <summary>
		/// Gets a list of photos that do not contain geo location information.
		/// </summary>
		/// <param name="options">A limited set of options are supported.</param>
		/// <returns>A list of photos that do not contain location information.</returns>
		public Photos PhotosGetWithoutGeoData(PartialSearchOptions options)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.getWithoutGeoData");
			Utils.PartialOptionsIntoArray(options, parameters);

			FlickrNet.Response response = GetResponseNoCache(parameters);
			if( response.Status == ResponseStatus.OK )
			{
				return response.Photos;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Gets a list of photos that do not contain geo location information.
		/// </summary>
		/// <param name="options">A limited set of options are supported. 
		/// Unsupported arguments are ignored. 
		/// See http://www.flickr.com/services/api/flickr.photos.getWithGeoData.html for supported properties.</param>
		/// <returns>A list of photos that do not contain location information.</returns>
		[Obsolete("Use the PartialSearchOptions instead")]
		public Photos PhotosGetWithoutGeoData(PhotoSearchOptions options)
		{
			PartialSearchOptions newOptions = new PartialSearchOptions(options);
			return PhotosGetWithoutGeoData(newOptions);
		}

		/// <summary>
		/// Gets a list of photos that contain geo location information.
		/// </summary>
		/// <remarks>
		/// Note, this method doesn't actually return the location information with the photos, 
		/// unless you specify the <see cref="PhotoSearchExtras.Geo"/> option in the <c>extras</c> parameter.
		/// </remarks>
		/// <returns>A list of photos that contain Location information.</returns>
		public Photos PhotosGetWithGeoData()
		{
			PartialSearchOptions options = new PartialSearchOptions();
			return PhotosGetWithGeoData(options);
		}

		/// <summary>
		/// Gets a list of photos that contain geo location information.
		/// </summary>
		/// <remarks>
		/// Note, this method doesn't actually return the location information with the photos, 
		/// unless you specify the <see cref="PhotoSearchExtras.Geo"/> option in the <c>extras</c> parameter.
		/// </remarks>
		/// <param name="options">A limited set of options are supported. 
		/// Unsupported arguments are ignored. 
		/// See http://www.flickr.com/services/api/flickr.photos.getWithGeoData.html for supported properties.</param>
		/// <returns>A list of photos that contain Location information.</returns>
		[Obsolete("Use the new PartialSearchOptions instead")]
		public Photos PhotosGetWithGeoData(PhotoSearchOptions options)
		{
			PartialSearchOptions newOptions = new PartialSearchOptions(options);
			return PhotosGetWithGeoData(newOptions);
		}
		
		/// <summary>
		/// Gets a list of photos that contain geo location information.
		/// </summary>
		/// <remarks>
		/// Note, this method doesn't actually return the location information with the photos, 
		/// unless you specify the <see cref="PhotoSearchExtras.Geo"/> option in the <c>extras</c> parameter.
		/// </remarks>
		/// <param name="options">The options to filter/sort the results by.</param>
		/// <returns>A list of photos that contain Location information.</returns>
		public Photos PhotosGetWithGeoData(PartialSearchOptions options)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.getWithGeoData");
			Utils.PartialOptionsIntoArray(options, parameters);

			FlickrNet.Response response = GetResponseNoCache(parameters);
			if( response.Status == ResponseStatus.OK )
			{
				return response.Photos;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Get permissions for a photo.
		/// </summary>
		/// <param name="photoId">The id of the photo to get permissions for.</param>
		/// <returns>An instance of the <see cref="PhotoPermissions"/> class containing the permissions of the specified photo.</returns>
		public GeoPermissions PhotosGeoGetPerms(string photoId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.geo.getPerms");
			parameters.Add("photo_id", photoId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return new GeoPermissions(response.AllElements[0]);
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Set the permission for who can see geotagged photos on Flickr.
		/// </summary>
		/// <param name="photoId">The ID of the photo permissions to update.</param>
		/// <param name="IsPublic"></param>
		/// <param name="IsContact"></param>
		/// <param name="IsFamily"></param>
		/// <param name="IsFriend"></param>
		public void PhotosGeoSetPerms(string photoId, bool IsPublic, bool IsContact, bool IsFamily, bool IsFriend)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photos.geo.setPerms");
			parameters.Add("photo_id", photoId);
			parameters.Add("is_public", IsPublic?"1":"0");
			parameters.Add("is_contact", IsContact?"1":"0");
			parameters.Add("is_friend", IsFriend?"1":"0");
			parameters.Add("is_family", IsFamily?"1":"0");

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}


		#endregion

		#region [ Tests ]
		/// <summary>
		/// Can be used to call unsupported methods in the Flickr API.
		/// </summary>
		/// <remarks>
		/// Use of this method is not supported. 
		/// The way the FlickrNet API Library works may mean that some methods do not return an expected result 
		/// when using this method.
		/// </remarks>
		/// <param name="method">The method name, e.g. "flickr.test.null".</param>
		/// <param name="parameters">A list of parameters. Note, api_key is added by default and is not included. Can be null.</param>
		/// <returns>An array of <see cref="XmlElement"/> instances which is the expected response.</returns>
		public XmlElement[] TestGeneric(string method, NameValueCollection parameters)
		{
			Hashtable _parameters = new Hashtable();
			if( parameters != null )
			{
				foreach(string key in parameters.AllKeys)
				{
					_parameters.Add(key, parameters[key]);
				}
			}
			_parameters.Add("method", method);

			FlickrNet.Response response = GetResponseNoCache(_parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.AllElements;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}
		/// <summary>
		/// Runs the flickr.test.echo method and returned an array of <see cref="XmlElement"/> items.
		/// </summary>
		/// <param name="echoParameter">The parameter to pass to the method.</param>
		/// <param name="echoValue">The value to pass to the method with the parameter.</param>
		/// <returns>An array of <see cref="XmlElement"/> items.</returns>
		/// <remarks>
		/// The APi Key has been removed from the returned array and will not be shown.
		/// </remarks>
		/// <example>
		/// <code>
		/// XmlElement[] elements = flickr.TestEcho("&amp;param=value");
		/// foreach(XmlElement element in elements)
		/// {
		///		if( element.Name = "method" )
		///			Console.WriteLine("Method = " + element.InnerXml);
		///		if( element.Name = "param" )
		///			Console.WriteLine("Param = " + element.InnerXml);
		/// }
		/// </code>
		/// </example>
		public XmlElement[] TestEcho(string echoParameter, string echoValue)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.test.echo");
			parameters.Add("api_key", _apiKey);
			if( echoParameter != null && echoParameter.Length > 0 )
			{
				parameters.Add(echoParameter, echoValue);
			}

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				// Remove the api_key element from the array.
				XmlElement[] elements = new XmlElement[response.AllElements.Length - 1];
				int c = 0;
				foreach(XmlElement element in response.AllElements)
				{
					if(element.Name != "api_key" )
						elements[c++] = element;
				}
				return elements;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Test the logged in state of the current Filckr object.
		/// </summary>
		/// <returns>The <see cref="FoundUser"/> object containing the username and userid of the current user.</returns>
		public FoundUser TestLogin()
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.test.login");

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return new FoundUser(response.AllElements[0]);
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}
		#endregion

		#region [ Urls ]
		/// <summary>
		/// Returns the url to a group's page.
		/// </summary>
		/// <param name="groupId">The NSID of the group to fetch the url for.</param>
		/// <returns>An instance of the <see cref="Uri"/> class containing the URL of the group page.</returns>
		public Uri UrlsGetGroup(string groupId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.urls.getGroup");
			parameters.Add("api_key", _apiKey);
			parameters.Add("group_id", groupId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				if( response.AllElements[0] != null && response.AllElements[0].Attributes["url"] != null )
					return new Uri(response.AllElements[0].Attributes["url"].Value);
				else
					return null;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Returns the url to a user's photos.
		/// </summary>
		/// <returns>An instance of the <see cref="Uri"/> class containing the URL for the users photos.</returns>
		public Uri UrlsGetUserPhotos()
		{
			return UrlsGetUserPhotos(null);
		}
		
		/// <summary>
		/// Returns the url to a user's photos.
		/// </summary>
		/// <param name="userId">The NSID of the user to fetch the url for. If omitted, the calling user is assumed.</param>
		/// <returns>The URL of the users photos.</returns>
		public Uri UrlsGetUserPhotos(string userId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.urls.getUserPhotos");
			if( userId != null && userId.Length > 0 ) parameters.Add("user_id", userId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				if( response.AllElements[0] != null && response.AllElements[0].Attributes["url"] != null )
					return new Uri(response.AllElements[0].Attributes["url"].Value);
				else
					return null;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Returns the url to a user's profile.
		/// </summary>
		/// <returns>An instance of the <see cref="Uri"/> class containing the URL for the users profile.</returns>
		public Uri UrlsGetUserProfile()
		{
			return UrlsGetUserProfile(null);
		}

		/// <summary>
		/// Returns the url to a user's profile.
		/// </summary>
		/// <param name="userId">The NSID of the user to fetch the url for. If omitted, the calling user is assumed.</param>
		/// <returns>An instance of the <see cref="Uri"/> class containing the URL for the users profile.</returns>
		public Uri UrlsGetUserProfile(string userId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.urls.getUserProfile");
			if( userId != null && userId.Length > 0 ) parameters.Add("user_id", userId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				if( response.AllElements[0] != null && response.AllElements[0].Attributes["url"] != null )
					return new Uri(response.AllElements[0].Attributes["url"].Value);
				else
					return null;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Returns a group NSID, given the url to a group's page or photo pool.
		/// </summary>
		/// <param name="urlToFind">The url to the group's page or photo pool.</param>
		/// <returns>The ID of the group at the specified URL on success, a null reference (Nothing in Visual Basic) if the group cannot be found.</returns>
		public string UrlsLookupGroup(string urlToFind)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.urls.lookupGroup");
			parameters.Add("api_key", _apiKey);
			parameters.Add("url", urlToFind);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				if( response.AllElements[0] != null && response.AllElements[0].Attributes["id"] != null )
				{
					return response.AllElements[0].Attributes["id"].Value;
				}
				else
				{
					return null;
				}
			}
			else
			{
				if( response.Error.Code == 1 )
					return null;
				else
					throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Returns a user NSID, given the url to a user's photos or profile.
		/// </summary>
		/// <param name="urlToFind">Thr url to the user's profile or photos page.</param>
		/// <returns>An instance of the <see cref="FoundUser"/> class containing the users ID and username.</returns>
		public FoundUser UrlsLookupUser(string urlToFind)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.urls.lookupUser");
			parameters.Add("api_key", _apiKey);
			parameters.Add("url", urlToFind);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return new FoundUser(response.AllElements[0]);
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}
		#endregion

		#region [ Reflection ]
		/// <summary>
		/// Gets an array of supported method names for Flickr.
		/// </summary>
		/// <remarks>
		/// Note: Not all methods might be supported by the FlickrNet Library.</remarks>
		/// <returns></returns>
		public string[] ReflectionGetMethods()
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.reflection.getMethods");
			parameters.Add("api_key", _apiKey);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return Methods.GetMethods(response.AllElements[0]);
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		/// <summary>
		/// Gets the method details for a given method.
		/// </summary>
		/// <param name="methodName">The name of the method to retrieve.</param>
		/// <returns>Returns a <see cref="Method"/> instance for the given method name.</returns>
		public Method ReflectionGetMethodInfo(string methodName)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.reflection.getMethodInfo");
			parameters.Add("api_key", _apiKey);
			parameters.Add("method_name", methodName);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Method;
			}
			else
			{
				throw new FlickrException(response.Error);
			}
		}

		#endregion

		#region [ MD5 Hash ]
		private static string Md5Hash(string unhashed)
		{
			System.Security.Cryptography.MD5CryptoServiceProvider csp = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] bytes = System.Text.Encoding.UTF8.GetBytes(unhashed);
			byte[] hashedBytes = csp.ComputeHash(bytes, 0, bytes.Length);
			return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
		}
		#endregion
	}

}
