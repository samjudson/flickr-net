using System;
using System.Collections.Specialized ;
using System.Xml;

namespace FlickrNet
{
	/// <summary>
	/// Configuration settings for the Flickr.Net API Library.
	/// </summary>
	/// <remarks>
	/// <p>First, register the configuration section in the configSections section:</p>
	/// <p><code>&lt;configSections&gt;
	/// &lt;section name="flickrNet" type="FlickrNet.FlickrConfigurationManager,FlickrNet"/&gt;
	/// &lt;/configSections&gt;</code>
	/// </p>
	/// <p>
	/// Next, include the following config section:
	/// </p>
	/// <p><code>
	/// 	&lt;flickrNet 
	/// apiKey="1234567890abc"	// optional
	/// secret="2134123"		// optional
	/// token="234234"			// optional
	/// cacheSize="1234"		// optional, in bytes (defaults to 50 * 1024 * 1024 = 50MB)
	/// cacheTimeout="[d.]hh:mm:ss"	// optional, defaults to 1 day (1.00:00:00) - day component is optional
	/// // e.g. 10 minutes = 00:10:00 or 1 hour = 01:00:00 or 2 days, 12 hours = 2.12:00:00
	/// &gt;
	/// &lt;proxy		// proxy element is optional, but if included the attributes below are mandatory as mentioned
	/// ipaddress="127.0.0.1"	// mandatory
	/// port="8000"				// mandatory
	/// username="username"		// optional, but must have password if included
	/// password="password"		// optional, see username
	/// domain="domain"			// optional, used for Microsoft authenticated proxy servers
	/// /&gt;
	/// &lt;/flickrNet&gt;
	/// </code></p>
	/// </remarks>
	internal class FlickrConfigurationSettings
	{
		private string _apiKey;
		private string _apiSecret;
		private string _apiToken;
		private int _cacheSize;
		private TimeSpan _cacheTimeout = TimeSpan.MinValue;
		private string _proxyAddress;
		private int _proxyPort;
		private bool _proxyDefined;
		private string _proxyUsername;
		private string _proxyPassword;
		private string _proxyDomain;
		private string _cacheLocation;
		private bool _cacheDisabled;

		/// <summary>
		/// Loads FlickrConfigurationSettings with the settings in the config file.
		/// </summary>
		/// <param name="configNode">XmlNode containing the configuration settings.</param>
		public FlickrConfigurationSettings(XmlNode configNode)
		{
			if( configNode == null ) throw new ArgumentNullException("configNode");

			if( configNode.Attributes["apiKey"] != null ) _apiKey = configNode.Attributes["apiKey"].Value;
			if( configNode.Attributes["secret"] != null ) _apiSecret = configNode.Attributes["secret"].Value;
			if( configNode.Attributes["token"] != null ) _apiToken = configNode.Attributes["token"].Value;
			if( configNode.Attributes["cacheDisabled"] != null )
			{
				try
				{
					_cacheDisabled = bool.Parse(configNode.Attributes["cacheDisabled"].Value);
				}
				catch(FormatException) {}
			}
			if( configNode.Attributes["cacheSize"] != null )
			{
				try
				{
					_cacheSize = int.Parse(configNode.Attributes["cacheSize"].Value);
				}
				catch(FormatException) {}
			}
			if( configNode.Attributes["cacheTimeout"] != null )
			{
				try
				{
					_cacheTimeout = TimeSpan.Parse(configNode.Attributes["cacheTimeout"].Value);
				}
				catch(FormatException) {}
			}

			if( configNode.Attributes["cacheLocation"] != null )
			{
				try
				{
					_cacheLocation = (configNode.Attributes["cacheLocation"].Value);
				}
				catch(FormatException) {}
			}
			else
			{
				try
				{
					_cacheLocation = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				}
				catch(System.Security.SecurityException){ }
			}

			XmlNode proxy = configNode.SelectSingleNode("proxy");
			if( proxy != null ) 
			{
				_proxyDefined = true;
				_proxyAddress = proxy.Attributes["ipaddress"].Value;
				_proxyPort = int.Parse(proxy.Attributes["port"].Value);
				if( proxy.Attributes["username"] != null ) _proxyUsername = proxy.Attributes["username"].Value;
				if( proxy.Attributes["password"] != null ) _proxyPassword = proxy.Attributes["password"].Value;
				if( proxy.Attributes["domain"] != null ) _proxyDomain = proxy.Attributes["domain"].Value;
			}
		}

		/// <summary>
		/// API key. Null if not present. Optional.
		/// </summary>
		public string ApiKey
		{
			get { return _apiKey; }
		}

		/// <summary>
		/// Shared Secret. Null if not present. Optional.
		/// </summary>
		public string SharedSecret
		{
			get { return _apiSecret; }
		}
		
		/// <summary>
		/// API token. Null if not present. Optional.
		/// </summary>
		public string ApiToken
		{
			get { return _apiToken; }
		}

		/// <summary>
		/// Cache size in bytes. 0 if not present. Optional.
		/// </summary>
		public bool CacheDisabled
		{
			get { return _cacheDisabled; }
		}

		/// <summary>
		/// Cache size in bytes. 0 if not present. Optional.
		/// </summary>
		public int CacheSize
		{
			get { return _cacheSize; }
		}

		/// <summary>
		/// Cache timeout. Equals TimeSpan.MinValue is not present. Optional.
		/// </summary>
		public TimeSpan CacheTimeout
		{
			get { return _cacheTimeout; }
		}

		public string CacheLocation
		{
			get { return _cacheLocation; }
		}

		/// <summary>
		/// If the proxy is defined in the configuration section.
		/// </summary>
		public bool IsProxyDefined
		{
			get { return _proxyDefined; }
		}

		/// <summary>
		/// If <see cref="IsProxyDefined"/> is true then this is mandatory.
		/// </summary>
		public string ProxyIPAddress
		{
			get { return _proxyAddress; }
		}

		/// <summary>
		/// If <see cref="IsProxyDefined"/> is true then this is mandatory.
		/// </summary>
		public int ProxyPort
		{
			get { return _proxyPort; }
		}

		/// <summary>
		/// The username for the proxy. Optional.
		/// </summary>
		public string ProxyUsername
		{
			get { return _proxyUsername; }
		}

		/// <summary>
		/// The password for the proxy. Optional.
		/// </summary>
		public string ProxyPassword
		{
			get { return _proxyPassword; }
		}

		/// <summary>
		/// The domain for the proxy. Optional.
		/// </summary>
		public string ProxyDomain
		{
			get { return _proxyDomain; }
		}

	}
}
