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
        public Uri CalculateUri(Dictionary<string, object> parameters, bool includeSignature)
        {
            if (includeSignature)
            {
                SortedDictionary<string, object> sorted = new SortedDictionary<string, object>();
                foreach (KeyValuePair<string, object> pair in parameters) { sorted.Add(pair.Key, pair.Value); }

                StringBuilder sb = new StringBuilder(ApiSecret);
                foreach (KeyValuePair<string, object> pair in sorted) { sb.Append(pair.Key); sb.Append(pair.Value); }

                parameters.Add("api_sig", Utils.Md5Hash(sb.ToString()));
            }

            StringBuilder url = new StringBuilder();
            url.Append("?");
            foreach (KeyValuePair<string, object> pair in parameters)
            {
                url.AppendFormat("{0}={1}&", pair.Key, Uri.EscapeDataString(pair.Value.ToString()));
            }

            return new Uri(BaseUri, url.ToString());
        }


		#endregion

		#region [ DownloadPicture ]
        /// <summary>
        /// Download a picture (or anything else actually).
        /// </summary>
        /// <param name="url"></param>
        /// <param name="redirect">Allow automatic redirections.</param>
        /// <returns></returns>
        private Stream DoDownloadPicture(string url, bool redirect)
        {
            HttpWebRequest req = null;
            HttpWebResponse res = null;

            try
            {
                req = (HttpWebRequest)HttpWebRequest.Create(url);
                req.UserAgent = UserAgent;
                if (Proxy != null) req.Proxy = Proxy;
                req.Timeout = HttpTimeout;
                req.KeepAlive = false;
                req.AllowAutoRedirect = redirect;
                res = (HttpWebResponse)req.GetResponse();
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse res2 = (HttpWebResponse)ex.Response;
                    if (res2 != null)
                    {
                        throw new FlickrWebException(String.Format("HTTP Error while downloading photo: {0}, {1}", (int)res2.StatusCode, res2.StatusDescription), ex);
                    }
                }
                else if (ex.Status == WebExceptionStatus.Timeout)
                {
                    throw new FlickrWebException("The request timed-out", ex);
                }
                throw new FlickrWebException("Picture download failed (" + ex.Message + ")", ex);
            }

            System.Diagnostics.Debug.Write("Http Status Code = " + res.StatusCode);
            if (!redirect && res.StatusCode == HttpStatusCode.Redirect) return null;

            return res.GetResponseStream();
        }
        
        /// <summary>
		/// Downloads the picture from a internet and transfers it to a stream object. Returns null if the image does not exist.
		/// </summary>
		/// <param name="url">The url of the image to download.</param>
		/// <returns>A <see cref="Stream"/> object containing the downloaded picture if the image exists, and null if it does not.</returns>
		/// <remarks>
		/// The method checks the download cache first to see if the picture has already 
		/// been downloaded and if so returns the cached image. Otherwise it goes to the internet for the actual 
		/// image.
		/// </remarks>
        [Obsolete("Use WebClient.OpenRead or WebClient.DownloadData instead.")]
        public Stream DownloadPictureWithoutRedirect(string url)
		{
            return DoDownloadPicture(url, false);
		}

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
        [Obsolete("Use WebClient.OpenRead or WebClient.DownloadData instead.")]
        public System.IO.Stream DownloadPicture(string url)
		{
            return DoDownloadPicture(url, true);
		}

		#endregion

		#region [ Activity ]
		/// <summary>
		/// Returns a list of recent activity on photos belonging to the calling user.
		/// </summary>
		/// <remarks>
		/// <b>Do not poll this method more than once an hour.</b>
		/// </remarks>
		/// <returns>An array of <see cref="ActivityItem"/> instances.</returns>
		public ActivityItem[] ActivityUserPhotos()
		{
			return ActivityUserPhotos(null);
		}

		/// <summary>
		/// Returns a list of recent activity on photos belonging to the calling user.
		/// </summary>
		/// <remarks>
		/// <b>Do not poll this method more than once an hour.</b>
		/// </remarks>
		/// <param name="timePeriod">The number of days or hours you want to get activity for.</param>
		/// <param name="timeType">'d' for days, 'h' for hours.</param>
		/// <returns>An array of <see cref="ActivityItem"/> instances.</returns>
		public ActivityItem[] ActivityUserPhotos(int timePeriod, string timeType)
		{
			if( timePeriod == 0 ) 
				throw new ArgumentOutOfRangeException("timePeriod", "Time Period should be greater than 0");

			if( timeType == null ) 
				throw new ArgumentNullException("timeType");

			if( timeType != "d" && timeType != "h" )
				throw new ArgumentOutOfRangeException("timeType", "Time type must be 'd' or 'h'");

			return ActivityUserPhotos(timePeriod + timeType);
		}

		private ActivityItem[] ActivityUserPhotos(string timeframe)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.activity.userPhotos");
			if( timeframe != null && timeframe.Length > 0 ) parameters.Add("timeframe", timeframe);
			
			FlickrNet.Response response = GetResponseCache(parameters);
			if( response.Status == ResponseStatus.OK )
			{
				XmlNodeList list = response.AllElements[0].SelectNodes("item");
				ActivityItem[] items = new ActivityItem[list.Count];
				for(int i = 0; i < items.Length; i++)
				{
					items[i] = new ActivityItem(list[i]);
				}
				return items;
			}
			else
			{
				throw new FlickrApiException(response.Error);
			}
		}

		/// <summary>
		/// Returns a list of recent activity on photos commented on by the calling user.
		/// </summary>
		/// <remarks>
		/// <b>Do not poll this method more than once an hour.</b>
		/// </remarks>
		/// <returns></returns>
		public ActivityItem[] ActivityUserComments(int page, int perPage)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.activity.userComments");
			if( page > 0 ) parameters.Add("page", page);
			if( perPage > 0 ) parameters.Add("per_page", perPage);

			FlickrNet.Response response = GetResponseCache(parameters);
			if( response.Status == ResponseStatus.OK )
			{
				XmlNodeList list = response.AllElements[0].SelectNodes("item");
				ActivityItem[] items = new ActivityItem[list.Count];
				for(int i = 0; i < items.Length; i++)
				{
					items[i] = new ActivityItem(list[i]);
				}
				return items;
			}
			else
			{
				throw new FlickrApiException(response.Error);
			}
		}
		#endregion

		#region [ UploadPicture ]
		/// <summary>
		/// Uploads a file to Flickr.
		/// </summary>
		/// <param name="filename">The filename of the file to open.</param>
		/// <returns>The id of the photo on a successful upload.</returns>
		/// <exception cref="FlickrApiException">Thrown when Flickr returns an error. see http://www.flickr.com/services/api/upload.api.html for more details.</exception>
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
		/// <exception cref="FlickrApiException">Thrown when Flickr returns an error. see http://www.flickr.com/services/api/upload.api.html for more details.</exception>
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
		/// <exception cref="FlickrApiException">Thrown when Flickr returns an error. see http://www.flickr.com/services/api/upload.api.html for more details.</exception>
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
		/// <exception cref="FlickrApiException">Thrown when Flickr returns an error. see http://www.flickr.com/services/api/upload.api.html for more details.</exception>
		/// <remarks>Other exceptions may be thrown, see <see cref="FileStream"/> constructors for more details.</remarks>
		public string UploadPicture(string filename, string title, string description, string tags)
		{
            string file = Path.GetFileName(filename);
			Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			return UploadPicture(stream, file, title, description, tags, false, false, false, ContentType.None, SafetyLevel.None, HiddenFromSearch.None);
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
		/// <exception cref="FlickrApiException">Thrown when Flickr returns an error. see http://www.flickr.com/services/api/upload.api.html for more details.</exception>
		/// <remarks>Other exceptions may be thrown, see <see cref="FileStream"/> constructors for more details.</remarks>
		public string UploadPicture(string filename, string title, string description, string tags, bool isPublic, bool isFamily, bool isFriend)
		{
            string file = Path.GetFileName(filename);
			Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			return UploadPicture(stream, file, title, description, tags, isPublic, isFamily, isFriend, ContentType.None, SafetyLevel.None, HiddenFromSearch.None);
		}

		/// <summary>
		/// UploadPicture method that does all the uploading work.
		/// </summary>
		/// <param name="stream">The <see cref="Stream"/> object containing the pphoto to be uploaded.</param>
        /// <param name="filename">The filename of the file to upload. Used as the title if title is null.</param>
		/// <param name="title">The title of the photo (optional).</param>
		/// <param name="description">The description of the photograph (optional).</param>
		/// <param name="tags">The tags for the photograph (optional).</param>
		/// <param name="isPublic">0 for private, 1 for public.</param>
		/// <param name="isFamily">1 if family, 0 is not.</param>
		/// <param name="isFriend">1 if friend, 0 if not.</param>
		/// <param name="contentType">The content type of the photo, i.e. Photo, Screenshot or Other.</param>
		/// <param name="safetyLevel">The safety level of the photo, i.e. Safe, Moderate or Restricted.</param>
		/// <param name="hiddenFromSearch">Is the photo hidden from public searches.</param>
		/// <returns>The id of the photograph after successful uploading.</returns>
        public string UploadPicture(Stream stream, string filename, string title, string description, string tags, bool isPublic, bool isFamily, bool isFriend, ContentType contentType, SafetyLevel safetyLevel, HiddenFromSearch hiddenFromSearch)
		{
			CheckRequiresAuthentication();
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
            req.Timeout = HttpTimeout;
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

			parameters.Add("is_public", isPublic?"1":"0");
			parameters.Add("is_friend", isFriend?"1":"0");
			parameters.Add("is_family", isFamily?"1":"0");

			if( safetyLevel != SafetyLevel.None )
			{
				parameters.Add("safety_level", (int)safetyLevel);
			}
			if( contentType != ContentType.None )
			{
				parameters.Add("content_type", (int)contentType);
			}
			if( hiddenFromSearch != HiddenFromSearch.None )
			{
				parameters.Add("hidden", (int)hiddenFromSearch);
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
			sb.Append(Utils.Md5Hash(HashStringBuilder.ToString()) + "\r\n");

			// Photo
			sb.Append("--" + boundary + "\r\n");
			sb.Append("Content-Disposition: form-data; name=\"photo\"; filename=\"" + filename + "\"\r\n");
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
				throw new FlickrApiException(uploader.Error);
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
			req.Timeout = HttpTimeout;
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
			sb.Append(Utils.Md5Hash(HashStringBuilder.ToString()) + "\r\n");

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
				throw new FlickrApiException(uploader.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
			}
		}

		/// <summary>
		/// Get a list of group members.
		/// </summary>
		/// <param name="groupId">The group id to get the list of members for.</param>
		/// <returns>A <see cref="Members"/> instance containing the first 100 members for the group.</returns>
		public Members GroupsMemberGetList(string groupId)
		{
			return GroupsMemberGetList(groupId, 100, 1, MemberType.NotSpecified);
		}

		/// <summary>
		/// Get a list of group members.
		/// </summary>
		/// <param name="groupId">The group id to get the list of members for.</param>
		/// <param name="perPage">The number of members to return per page (default is 100, max is 500).</param>
		/// <param name="page">The page of the results to return (default is 1).</param>
		/// <param name="memberTypes">The types of members to be returned. Can be more than one.</param>
		/// <returns>A <see cref="Members"/> instance containing the members for the group.</returns>
		public Members GroupsMemberGetList(string groupId, int perPage, int page, MemberType memberTypes)
		{
			CheckRequiresAuthentication();

			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.groups.members.getList");
			parameters.Add("api_key", _apiKey);
			parameters.Add("per_page", perPage);
			parameters.Add("page", page);
			if (memberTypes != MemberType.NotSpecified) parameters.Add("membertypes", Utils.MemberTypeToString(memberTypes));
			parameters.Add("group_id", groupId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if (response.Status == ResponseStatus.OK)
			{
				return response.Members;
			}
			else
			{
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
					throw new ResponseXmlException("Comment ID not found in response Xml.");
			}
			else
			{
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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

			FlickrNet.Response response = GetResponseNoCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return;
			}
			else
			{
				throw new FlickrApiException(response.Error);
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

			FlickrNet.Response response = GetResponseNoCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Photoset;
			}
			else
			{
				throw new FlickrApiException(response.Error);
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

			FlickrNet.Response response = GetResponseNoCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return true;
			}
			else
			{
				throw new FlickrApiException(response.Error);
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

			FlickrNet.Response response = GetResponseNoCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return true;
			}
			else
			{
				throw new FlickrApiException(response.Error);
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

			FlickrNet.Response response = GetResponseNoCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return true;
			}
			else
			{
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
                foreach (Photoset set in response.Photosets.PhotosetCollection)
                {
                    set.OwnerId = userId;
                }
				return response.Photosets;
			}
			else
			{
				throw new FlickrApiException(response.Error);
			}
		}

		/// <summary>
		/// Gets a collection of photos for a photoset.
		/// </summary>
		/// <param name="photosetId">The ID of the photoset to return photos for.</param>
		/// <returns>A <see cref="Photoset"/> object containing the list of <see cref="Photo"/> instances.</returns>
		public Photoset PhotosetsGetPhotos(string photosetId)
		{
			return PhotosetsGetPhotos(photosetId, PhotoSearchExtras.All, PrivacyFilter.None, 0, 0);
		}

		/// <summary>
		/// Gets a collection of photos for a photoset.
		/// </summary>
		/// <param name="photosetId">The ID of the photoset to return photos for.</param>
		/// <param name="page">The page to return, defaults to 1.</param>
		/// <param name="perPage">The number of photos to return per page.</param>
		/// <returns>A <see cref="Photoset"/> object containing the list of <see cref="Photo"/> instances.</returns>
		public Photoset PhotosetsGetPhotos(string photosetId, int page, int perPage)
		{
			return PhotosetsGetPhotos(photosetId, PhotoSearchExtras.All, PrivacyFilter.None, page, perPage);
		}

		/// <summary>
		/// Gets a collection of photos for a photoset.
		/// </summary>
		/// <param name="photosetId">The ID of the photoset to return photos for.</param>
		/// <param name="privacyFilter">The privacy filter to search on.</param>
		/// <returns>A <see cref="Photoset"/> object containing the list of <see cref="Photo"/> instances.</returns>
		public Photoset PhotosetsGetPhotos(string photosetId, PrivacyFilter privacyFilter)
		{
			return PhotosetsGetPhotos(photosetId, PhotoSearchExtras.All, privacyFilter, 0, 0);
		}

		/// <summary>
		/// Gets a collection of photos for a photoset.
		/// </summary>
		/// <param name="photosetId">The ID of the photoset to return photos for.</param>
		/// <param name="privacyFilter">The privacy filter to search on.</param>
		/// <param name="page">The page to return, defaults to 1.</param>
		/// <param name="perPage">The number of photos to return per page.</param>
		/// <returns>A <see cref="Photoset"/> object containing the list of <see cref="Photo"/> instances.</returns>
		public Photoset PhotosetsGetPhotos(string photosetId, PrivacyFilter privacyFilter, int page, int perPage)
		{
			return PhotosetsGetPhotos(photosetId, PhotoSearchExtras.All, privacyFilter, page, perPage);
		}

		/// <summary>
		/// Gets a collection of photos for a photoset.
		/// </summary>
		/// <param name="photosetId">The ID of the photoset to return photos for.</param>
		/// <param name="extras">The extras to return for each photo.</param>
		/// <returns>A <see cref="Photoset"/> object containing the list of <see cref="Photo"/> instances.</returns>
		public Photoset PhotosetsGetPhotos(string photosetId, PhotoSearchExtras extras)
		{
			return PhotosetsGetPhotos(photosetId, extras, PrivacyFilter.None, 0, 0);
		}

		/// <summary>
		/// Gets a collection of photos for a photoset.
		/// </summary>
		/// <param name="photosetId">The ID of the photoset to return photos for.</param>
		/// <param name="extras">The extras to return for each photo.</param>
		/// <param name="page">The page to return, defaults to 1.</param>
		/// <param name="perPage">The number of photos to return per page.</param>
		/// <returns>A <see cref="Photoset"/> object containing the list of <see cref="Photo"/> instances.</returns>
		public Photoset PhotosetsGetPhotos(string photosetId, PhotoSearchExtras extras, int page, int perPage)
		{
			return PhotosetsGetPhotos(photosetId, extras, PrivacyFilter.None, page, perPage);
		}

		/// <summary>
		/// Gets a collection of photos for a photoset.
		/// </summary>
		/// <param name="photosetId">The ID of the photoset to return photos for.</param>
		/// <param name="extras">The extras to return for each photo.</param>
		/// <param name="privacyFilter">The privacy filter to search on.</param>
		/// <returns>A <see cref="Photoset"/> object containing the list of <see cref="Photo"/> instances.</returns>
		public Photoset PhotosetsGetPhotos(string photosetId, PhotoSearchExtras extras, PrivacyFilter privacyFilter)
		{
			return PhotosetsGetPhotos(photosetId, extras, privacyFilter, 0, 0);
		}

				/// <summary>
		/// Gets a collection of photos for a photoset.
		/// </summary>
		/// <param name="photosetId">The ID of the photoset to return photos for.</param>
		/// <param name="extras">The extras to return for each photo.</param>
		/// <param name="privacyFilter">The privacy filter to search on.</param>
		/// <param name="page">The page to return, defaults to 1.</param>
		/// <param name="perPage">The number of photos to return per page.</param>
		/// <returns>An array of <see cref="Photo"/> instances.</returns>
        public Photoset PhotosetsGetPhotos(string photosetId, PhotoSearchExtras extras, PrivacyFilter privacyFilter, int page, int perPage)
        {
            return PhotosetsGetPhotos(photosetId, extras, privacyFilter, page, perPage, MediaType.None);
        }

		/// <summary>
		/// Gets a collection of photos for a photoset.
		/// </summary>
		/// <param name="photosetId">The ID of the photoset to return photos for.</param>
		/// <param name="extras">The extras to return for each photo.</param>
		/// <param name="privacyFilter">The privacy filter to search on.</param>
		/// <param name="page">The page to return, defaults to 1.</param>
		/// <param name="perPage">The number of photos to return per page.</param>
        /// <param name="media">Filter on the type of media.</param>
		/// <returns>An array of <see cref="Photo"/> instances.</returns>
		public Photoset PhotosetsGetPhotos(string photosetId, PhotoSearchExtras extras, PrivacyFilter privacyFilter, int page, int perPage, MediaType media)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.photosets.getPhotos");
			parameters.Add("photoset_id", photosetId);
			if( extras != PhotoSearchExtras.None ) parameters.Add("extras", Utils.ExtrasToString(extras));
			if( privacyFilter != PrivacyFilter.None ) parameters.Add("privacy_filter", privacyFilter.ToString("d"));
			if( page > 0 ) parameters.Add("page", page);
			if( perPage > 0 ) parameters.Add("per_page", perPage);
            if (media != MediaType.None) parameters.Add("media", (media == MediaType.All ? "all" : (media == MediaType.Photos ? "photos" : (media == MediaType.Videos ? "videos" : ""))));

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				if( response.Photoset.OwnerId != null && response.Photoset.OwnerId.Length > 0 )
				{
					foreach(Photo p in response.Photoset.PhotoCollection)
					{
						p.UserId = response.Photoset.OwnerId;
					}
				}
				return response.Photoset;
			}
			else
			{
				throw new FlickrApiException(response.Error);
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
			parameters.Add("photoset_ids", photosetIds);

			FlickrNet.Response response = GetResponseNoCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return;
			}
			else
			{
				throw new FlickrApiException(response.Error);
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

			FlickrNet.Response response = GetResponseNoCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return;
			}
			else
			{
				throw new FlickrApiException(response.Error);
			}
		}
		#endregion

		#region [ Places ]
		/// <summary>
		/// Returns a list of places which contain the query string.
		/// </summary>
		/// <param name="query">The string to search for. Must not be null.</param>
		/// <returns>An array of <see cref="Place"/> instances.</returns>
		public Place[] PlacesFind(string query)
		{
			if( query == null ) throw new ArgumentNullException("query");

			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.places.find");
			parameters.Add("query", query);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Places.PlacesCollection;
			}
			else
			{
				throw new FlickrApiException(response.Error);
			}

		}

		/// <summary>
		/// Returns a place based on the input latitude and longitude.
		/// </summary>
		/// <param name="latitude">The latitude, between -180 and 180.</param>
		/// <param name="longitude">The longitude, between -90 and 90.</param>
		/// <returns>An instance of the <see cref="Place"/> that matches the locality.</returns>
		public Place PlacesFindByLatLon(decimal latitude, decimal longitude)
		{
			return PlacesFindByLatLon(latitude, longitude, GeoAccuracy.None);
		}

		/// <summary>
		/// Returns a place based on the input latitude and longitude.
		/// </summary>
		/// <param name="latitude">The latitude, between -180 and 180.</param>
		/// <param name="longitude">The longitude, between -90 and 90.</param>
		/// <param name="accuracy">The level the locality will be for.</param>
		/// <returns>An instance of the <see cref="Place"/> that matches the locality.</returns>
		public Place PlacesFindByLatLon(decimal latitude, decimal longitude, GeoAccuracy accuracy)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.places.findByLatLon");
			parameters.Add("lat", latitude.ToString("0.000"));
			parameters.Add("lon", longitude.ToString("0.000"));
			if( accuracy != GeoAccuracy.None ) parameters.Add("accuracy", (int)accuracy);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Places.PlacesCollection[0];
			}
			else
			{
				throw new FlickrApiException(response.Error);
			}
		}

		/// <summary>
		/// Return a list of locations with public photos that are parented by a Where on Earth (WOE) or Places ID.
		/// </summary>
		/// <param name="placeId">A Flickr Places ID. (While optional, you must pass either a valid Places ID or a WOE ID.)</param>
		/// <param name="woeId">A Where On Earth (WOE) ID. (While optional, you must pass either a valid Places ID or a WOE ID.)</param>
		/// <returns>Returns an array of <see cref="Place"/> elements.</returns>
		public Place[] PlacesGetChildrenWithPhotosPublic(string placeId, string woeId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.places.getChildrenWithPhotosPublic");

			if( (placeId == null || placeId.Length == 0) && (woeId == null || woeId.Length == 0 ))
			{
				throw new FlickrException("Both placeId and woeId cannot be null or empty.");
			}

			parameters.Add("place_id", placeId);
			parameters.Add("woe_id", woeId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Places.PlacesCollection;
			}
			else
			{
				throw new FlickrApiException(response.Error);
			}
		}

		/// <summary>
		/// Get informations about a place.
		/// </summary>
		/// <param name="placeId">A Flickr Places ID. (While optional, you must pass either a valid Places ID or a WOE ID.)</param>
		/// <param name="woeId">A Where On Earth (WOE) ID. (While optional, you must pass either a valid Places ID or a WOE ID.)</param>
		/// <returns>The <see cref="Place"/> record for the place/woe ID.</returns>
		public Place PlacesGetInfo(string placeId, string woeId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.places.getInfo");

			if( (placeId == null || placeId.Length == 0) && (woeId == null || woeId.Length == 0 ) )
			{
				throw new FlickrException("Both placeId and woeId cannot be null or empty.");
			}

			parameters.Add("place_id", placeId);
			parameters.Add("woe_id", woeId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.Place;
			}
			else
			{
				throw new FlickrApiException(response.Error);
			}
		}

        public Places PlacesPlacesForUser()
        {
            return PlacesPlacesForUser(PlaceType.Continent, null, null, 0, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
        }

        public Places PlacesPlacesForUser(PlaceType placeType, string woeId, string placeId)
        {
            return PlacesPlacesForUser(placeType, woeId, placeId, 0, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
        }

        /// <summary>
        /// Gets the places of a particular type that the authenticated user has geotagged photos.
        /// </summary>
        /// <param name="placeType">The type of places to return.</param>
        /// <returns>The list of places of that type.</returns>
        public Places PlacesPlacesForUser(PlaceType placeType, string woeId, string placeId, int threshold, DateTime minUploadDate, DateTime maxUploadDate, DateTime minTakenDate, DateTime maxTakenDate)
        {
            CheckRequiresAuthentication();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("method", "flickr.places.placesForUser");

            parameters.Add("place_type_id", (int)placeType);
            if (!String.IsNullOrEmpty(woeId)) parameters.Add("woe_id", woeId);
            if (!String.IsNullOrEmpty(placeId)) parameters.Add("place_id", placeId);
            if (threshold > 0) parameters.Add("threshold", threshold);
            if (minTakenDate != DateTime.MinValue) parameters.Add("min_taken_date", Utils.DateToUnixTimestamp(minTakenDate));
            if (maxTakenDate != DateTime.MinValue) parameters.Add("max_taken_date", Utils.DateToUnixTimestamp(maxTakenDate));
            if (minUploadDate != DateTime.MinValue) parameters.Add("min_upload_date ", Utils.DateToUnixTimestamp(minUploadDate));
            if (maxUploadDate != DateTime.MinValue) parameters.Add("max_upload_date ", Utils.DateToUnixTimestamp(maxUploadDate));

            return GetResponseCache<Places>(parameters);
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
				throw new FlickrApiException(response.Error);
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
					throw new ResponseXmlException("Comment ID not found in response.");
			}
			else
			{
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
			}
		}
		#endregion

		#region [ Prefs ]
		/// <summary>
		/// Gets the currently authenticated users default safety level.
		/// </summary>
		/// <returns></returns>
		public SafetyLevel PrefsGetSafetyLevel()
		{
			CheckRequiresAuthentication();

			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.prefs.getSafetyLevel");

			Response res = GetResponseCache(parameters);
			if( res.Status == ResponseStatus.OK )
			{
				string s = res.AllElements[0].GetAttribute("safety_level");
                return (SafetyLevel)int.Parse(s, System.Globalization.CultureInfo.InvariantCulture);
			}
			else
			{
				throw new FlickrApiException(res.Error);
			}
		}

		/// <summary>
		/// Gets the currently authenticated users default hidden from search setting.
		/// </summary>
		/// <returns></returns>
		public HiddenFromSearch PrefsGetHidden()
		{
			CheckRequiresAuthentication();

			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.prefs.getHidden");

			Response res = GetResponseCache(parameters);
			if( res.Status == ResponseStatus.OK )
			{
				string s = res.AllElements[0].GetAttribute("hidden");
                return (HiddenFromSearch)int.Parse(s, System.Globalization.CultureInfo.InvariantCulture);
			}
			else
			{
				throw new FlickrApiException(res.Error);
			}
		}
		
		/// <summary>
		/// Gets the currently authenticated users default content type.
		/// </summary>
		/// <returns></returns>
		public ContentType PrefsGetContentType()
		{
			CheckRequiresAuthentication();

			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.prefs.getContentType");

			Response res = GetResponseCache(parameters);
			if( res.Status == ResponseStatus.OK )
			{
				string s = res.AllElements[0].GetAttribute("content_type");
                return (ContentType)int.Parse(s, System.Globalization.CultureInfo.InvariantCulture);
			}
			else
			{
				throw new FlickrApiException(res.Error);
			}
		}
		#endregion

		#region [ Tags ]
		/// <summary>
		/// Get the tag list for a given photo.
		/// </summary>
		/// <param name="photoId">The id of the photo to return tags for.</param>
		/// <returns>An instance of the <see cref="PhotoInfo"/> class containing only the <see cref="PhotoInfo.Tags"/> property.</returns>
		public PhotoInfoTag[] TagsGetListPhoto(string photoId)
		{
			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.tags.getListPhoto");
			parameters.Add("api_key", _apiKey);
			parameters.Add("photo_id", photoId);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return response.PhotoInfo.Tags.TagCollection;
			}
			else
			{
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
		public Tag[] TagsGetListUserPopular(string userId, int count)
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
				throw new FlickrApiException(response.Error);
			}
		}

		/// <summary>
		/// Gets a list of 'cleaned' tags and the raw values for those tags.
		/// </summary>
		/// <returns>An array of <see cref="RawTag"/> objects.</returns>
		public RawTag[] TagsGetListUserRaw()
		{
			return TagsGetListUserRaw(null);
		}

		/// <summary>
		/// Gets a list of 'cleaned' tags and the raw values for a specific tag.
		/// </summary>
		/// <param name="tag">The tag to return the raw version of.</param>
		/// <returns>An array of <see cref="RawTag"/> objects.</returns>
		public RawTag[] TagsGetListUserRaw(string tag)
		{
			CheckRequiresAuthentication();

			Hashtable parameters = new Hashtable();
			parameters.Add("method", "flickr.tags.getListUserRaw");
			if( tag != null && tag.Length > 0 ) parameters.Add("tag", tag);

			FlickrNet.Response response = GetResponseCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				XmlNodeList nodes = response.AllElements[0].SelectNodes("//tag");
				RawTag[] tags = new RawTag[nodes.Count];
				for(int i = 0; i < tags.Length; i++)
				{
					tags[i] = new RawTag(nodes[i]);
				}
				return tags;
			}
			else
			{
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
					throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
					throw new FlickrApiException(response.Error);
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

			FlickrNet.Response response = GetResponseCache(parameters);
			if( response.Status == ResponseStatus.OK )
			{
				return response.Photos;
			}
			else
			{
				throw new FlickrApiException(response.Error);
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

			FlickrNet.Response response = GetResponseCache(parameters);
			if( response.Status == ResponseStatus.OK )
			{
				return response.Photos;
			}
			else
			{
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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

			FlickrNet.Response response = GetResponseNoCache(parameters);

			if( response.Status == ResponseStatus.OK )
			{
				return;
			}
			else
			{
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
					throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
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
				throw new FlickrApiException(response.Error);
			}
		}

		#endregion


    }
}
