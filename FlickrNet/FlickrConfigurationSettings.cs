#if !(MONOTOUCH || WindowsCE || SILVERLIGHT)
using System;
using System.Collections.Specialized;
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
    ///     &lt;flickrNet 
    /// apiKey="1234567890abc"    // optional
    /// secret="2134123"        // optional
    /// token="234234"            // optional
    /// cacheSize="1234"        // optional, in bytes (defaults to 50 * 1024 * 1024 = 50MB)
    /// cacheTimeout="[d.]HH:mm:ss"    // optional, defaults to 1 day (1.00:00:00) - day component is optional
    /// // e.g. 10 minutes = 00:10:00 or 1 hour = 01:00:00 or 2 days, 12 hours = 2.12:00:00
    /// &gt;
    /// &lt;proxy        // proxy element is optional, but if included the attributes below are mandatory as mentioned
    /// ipaddress="127.0.0.1"    // mandatory
    /// port="8000"                // mandatory
    /// username="username"        // optional, but must have password if included
    /// password="password"        // optional, see username
    /// domain="domain"            // optional, used for Microsoft authenticated proxy servers
    /// /&gt;
    /// &lt;/flickrNet&gt;
    /// </code></p>
    /// </remarks>
    internal class FlickrConfigurationSettings
    {
        private string apiKey;
        private string apiSecret;
        private string apiToken;
        private int cacheSize;
        private TimeSpan cacheTimeout = TimeSpan.MinValue;
        private string proxyAddress;
        private int proxyPort;
        private bool proxyDefined;
        private string proxyUsername;
        private string proxyPassword;
        private string proxyDomain;
        private string cacheLocation;
        private bool cacheDisabled;
        private SupportedService service;

        /// <summary>
        /// Loads FlickrConfigurationSettings with the settings in the config file.
        /// </summary>
        /// <param name="configNode">XmlNode containing the configuration settings.</param>
        public FlickrConfigurationSettings(XmlNode configNode)
        {
            if (configNode == null) throw new ArgumentNullException("configNode");

            foreach (XmlAttribute attribute in configNode.Attributes)
            {
                switch (attribute.Name)
                {
                    case "apiKey":
                        apiKey = attribute.Value;
                        break;
                    case "secret":
                        apiSecret = attribute.Value;
                        break;
                    case "token":
                        apiToken = attribute.Value;
                        break;
                    case "cacheDisabled":
                        try
                        {
                            cacheDisabled = bool.Parse(attribute.Value);
                            break;
                        }
                        catch (FormatException ex)
                        {
                            throw new System.Configuration.ConfigurationErrorsException("cacheDisbled should be \"true\" or \"false\"", ex, configNode);
                        }
                    case "cacheSize":
                        try
                        {
                            cacheSize = int.Parse(attribute.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                            break;
                        }
                        catch (FormatException ex)
                        {
                            throw new System.Configuration.ConfigurationErrorsException("cacheSize should be integer value", ex, configNode);
                        }
                    case "cacheTimeout":
                        try
                        {
                            cacheTimeout = TimeSpan.Parse(attribute.Value);
                            break;
                        }
                        catch (FormatException ex)
                        {
                            throw new System.Configuration.ConfigurationErrorsException("cacheTimeout should be TimeSpan value ([d:]HH:mm:ss)", ex, configNode);
                        }
                    case "cacheLocation":
                        cacheLocation = attribute.Value;
                        break;

                    case "service":
                        try
                        {
                            service = (SupportedService)Enum.Parse(typeof(SupportedService), attribute.Value, true);
                            break;
                        }
                        catch (ArgumentException ex)
                        {
                            throw new System.Configuration.ConfigurationErrorsException("service must be one of the supported services (See SupportedServices enum)", ex, configNode);
                        }

                    default:
                        throw new System.Configuration.ConfigurationErrorsException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "Unknown attribute '{0}' in flickrNet node", attribute.Name), configNode);
                }
            }

            foreach (XmlNode node in configNode.ChildNodes)
            {
                switch (node.Name)
                {
                    case "proxy":
                        ProcessProxyNode(node, configNode);
                        break;
                    default:
                        throw new System.Configuration.ConfigurationErrorsException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "Unknown node '{0}' in flickrNet node", node.Name), configNode);
                }
            }
        }

        // ProcessProxyNode - Constructor Helper Method
        private void ProcessProxyNode(XmlNode proxy, XmlNode configNode)
        {
            if (proxy.ChildNodes.Count > 0)
                throw new System.Configuration.ConfigurationErrorsException("proxy element does not support child elements");

            proxyDefined = true;
            foreach (XmlAttribute attribute in proxy.Attributes)
            {

                switch (attribute.Name)
                {
                    case "ipaddress":
                        proxyAddress = attribute.Value;
                        break;
                    case "port":
                        try
                        {
                            proxyPort = int.Parse(attribute.Value, System.Globalization.CultureInfo.InvariantCulture);
                        }
                        catch (FormatException ex)
                        {
                            throw new System.Configuration.ConfigurationErrorsException("proxy port should be integer value", ex, configNode);
                        }
                        break;
                    case "username":
                        proxyUsername = attribute.Value;
                        break;
                    case "password":
                        proxyPassword = attribute.Value;
                        break;
                    case "domain":
                        proxyDomain = attribute.Value;
                        break;
                    default:
                        throw new System.Configuration.ConfigurationErrorsException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "Unknown attribute '{0}' in flickrNet/proxy node", attribute.Name), configNode);
                }
            }

            if (proxyAddress == null)
                throw new System.Configuration.ConfigurationErrorsException("proxy ipaddress is mandatory if you specify the proxy element");
            if (proxyPort == 0)
                throw new System.Configuration.ConfigurationErrorsException("proxy port is mandatory if you specify the proxy element");
            if (proxyUsername != null && proxyPassword == null)
                throw new System.Configuration.ConfigurationErrorsException("proxy password must be specified if proxy username is specified");
            if (proxyUsername == null && proxyPassword != null)
                throw new System.Configuration.ConfigurationErrorsException("proxy username must be specified if proxy password is specified");
            if (proxyDomain != null && proxyUsername == null)
                throw new System.Configuration.ConfigurationErrorsException("proxy username/password must be specified if proxy domain is specified");
        }

        /// <summary>
        /// API key. Null if not present. Optional.
        /// </summary>
        public string ApiKey
        {
            get { return apiKey; }
        }

        /// <summary>
        /// Shared Secret. Null if not present. Optional.
        /// </summary>
        public string SharedSecret
        {
            get { return apiSecret; }
        }

        /// <summary>
        /// API token. Null if not present. Optional.
        /// </summary>
        public string ApiToken
        {
            get { return apiToken; }
        }

        /// <summary>
        /// Cache size in bytes. 0 if not present. Optional.
        /// </summary>
        public bool CacheDisabled
        {
            get { return cacheDisabled; }
        }

        /// <summary>
        /// Cache size in bytes. 0 if not present. Optional.
        /// </summary>
        public int CacheSize
        {
            get { return cacheSize; }
        }

        /// <summary>
        /// Cache timeout. Equals TimeSpan.MinValue is not present. Optional.
        /// </summary>
        public TimeSpan CacheTimeout
        {
            get { return cacheTimeout; }
        }

        public string CacheLocation
        {
            get { return cacheLocation; }
        }

        public SupportedService Service
        {
            get { return service; }
        }

        /// <summary>
        /// If the proxy is defined in the configuration section.
        /// </summary>
        public bool IsProxyDefined
        {
            get { return proxyDefined; }
        }

        /// <summary>
        /// If <see cref="IsProxyDefined"/> is true then this is mandatory.
        /// </summary>
        public string ProxyIPAddress
        {
            get { return proxyAddress; }
        }

        /// <summary>
        /// If <see cref="IsProxyDefined"/> is true then this is mandatory.
        /// </summary>
        public int ProxyPort
        {
            get { return proxyPort; }
        }

        /// <summary>
        /// The username for the proxy. Optional.
        /// </summary>
        public string ProxyUsername
        {
            get { return proxyUsername; }
        }

        /// <summary>
        /// The password for the proxy. Optional.
        /// </summary>
        public string ProxyPassword
        {
            get { return proxyPassword; }
        }

        /// <summary>
        /// The domain for the proxy. Optional.
        /// </summary>
        public string ProxyDomain
        {
            get { return proxyDomain; }
        }
    }
}
#endif