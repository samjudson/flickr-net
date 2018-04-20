using System;
using System.Xml;

namespace FlickrNet.Exceptions
{
    public class ConfigurationErrorsException : FlickrApiException
    {
        public ConfigurationErrorsException(string message) : base(message)
        {
        }

        public ConfigurationErrorsException(string message, XmlNode configNode) : base(message)
        {
        }

        public ConfigurationErrorsException(string message, Exception ex, XmlNode configNode) : base(message, ex)
        {
        }
    }
}
