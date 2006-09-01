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
		#region Private Variables
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
		private SupportedService _service;
		#endregion

		#region FlickrConfigurationSettings Constructor
		/// <summary>
		/// Loads FlickrConfigurationSettings with the settings in the config file.
		/// </summary>
		/// <param name="configNode">XmlNode containing the configuration settings.</param>
		public FlickrConfigurationSettings(XmlNode configNode)
		{
			if( configNode == null ) throw new ArgumentNullException("configNode");

			foreach(XmlAttribute attribute in configNode.Attributes)
			{
				switch(attribute.Name)
				{
					case "apiKey":
						_apiKey = attribute.Value;
						break;
					case "secret":
						_apiSecret = attribute.Value;
						break;
					case "token":
						_apiToken = attribute.Value;
						break;
					case "cacheDisabled":
						try
						{
							_cacheDisabled = bool.Parse(attribute.Value);
							break;
						}
						catch(FormatException ex) 
						{
							throw new System.Configuration.ConfigurationException("cacheDisbled should be \"true\" or \"false\"", ex, configNode);
						}
					case "cacheSize":
						try
						{
							_cacheSize = int.Parse(attribute.Value);
							break;
						}
						catch(FormatException ex) 
						{
							throw new System.Configuration.ConfigurationException("cacheSize should be integer value", ex, configNode);
						}
					case "cacheTimeout":
						try
						{
							_cacheTimeout = TimeSpan.Parse(attribute.Value);
							break;
						}
						catch(FormatException ex) 
						{
							throw new System.Configuration.ConfigurationException("cacheTimeout should be TimeSpan value ([d:]hh:mm:ss)", ex, configNode);
						}
					case "cacheLocation":
						_cacheLocation = attribute.Value;
						break;

					case "service":
						try
						{
							_service = (SupportedService)Enum.Parse(typeof(SupportedService), attribute.Value, true);
							break;
						}
						catch(ArgumentException ex)
						{
							throw new System.Configuration.ConfigurationException("service must be one of the supported services (See SupportedServices enum)", ex, configNode);
						}

					default:
						throw new System.Configuration.ConfigurationException(String.Format("Unknown attribute '{0}' in flickrNet node", attribute.Name), configNode);
				}
			}

			foreach(XmlNode node in configNode.ChildNodes)
			{
				switch(node.Name)
				{
					case "proxy":
						ProcessProxyNode(node, configNode);
						break;
					default:
						throw new System.Configuration.ConfigurationException(String.Format("Unknown node '{0}' in flickrNet node", node.Name), configNode);
				}
			}
		}
		#endregion

		#region ProcessProxyNode - Constructor Helper Method
		private void ProcessProxyNode(XmlNode proxy, XmlNode configNode)
		{
			if( proxy.ChildNodes.Count > 0 )
				throw new System.Configuration.ConfigurationException("proxy element does not support child elements");

			_proxyDefined = true;
			foreach(XmlAttribute attribute in proxy.Attributes)
			{

				switch(attribute.Name)
				{
					case "ipaddress":
						_proxyAddress = attribute.Value;
						break;
					case "port":
						try
						{
							_proxyPort = int.Parse(attribute.Value);
						}
						catch(FormatException ex) 
						{
							throw new System.Configuration.ConfigurationException("proxy port should be integer value", ex, configNode);
						}
						break;
					case "username":
						_proxyUsername = attribute.Value;
						break;
					case "password":
						_proxyPassword = attribute.Value;
						break;
					case "domain":
						_proxyDomain = attribute.Value;
						break;
					default:
						throw new System.Configuration.ConfigurationException(String.Format("Unknown attribute '{0}' in flickrNet/proxy node", attribute.Name), configNode);
				}
			}

			if( _proxyAddress == null )
				throw new System.Configuration.ConfigurationException("proxy ipaddress is mandatory if you specify the proxy element");
			if( _proxyPort == 0 )
				throw new System.Configuration.ConfigurationException("proxy port is mandatory if you specify the proxy element");
			if( _proxyUsername != null && _proxyPassword == null )
				throw new System.Configuration.ConfigurationException("proxy password must be specified if proxy username is specified");
			if( _proxyUsername == null && _proxyPassword != null )
				throw new System.Configuration.ConfigurationException("proxy username must be specified if proxy password is specified");
			if( _proxyDomain != null && _proxyUsername == null )
				throw new System.Configuration.ConfigurationException("proxy username/password must be specified if proxy domain is specified");
		}
		#endregion

		#region Public Properties
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

		public SupportedService Service
		{
			get { return _service; }
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
		#endregion

	}
}
