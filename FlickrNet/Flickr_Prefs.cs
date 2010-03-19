using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Gets the currently authenticated users default safety level.
        /// </summary>
        /// <returns></returns>
        public SafetyLevel PrefsGetSafetyLevel()
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.prefs.getSafetyLevel");

            UnknownResponse response = GetResponseCache<UnknownResponse>(parameters);

            System.Xml.XmlNode nav = response.GetXmlDocument().SelectSingleNode("*/@safety_level");
            if (nav == null)
                throw new ParsingException("Unable to find safety level in returned XML.");

            return (SafetyLevel)int.Parse(nav.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// Gets the currently authenticated users default hidden from search setting.
        /// </summary>
        /// <returns></returns>
        public HiddenFromSearch PrefsGetHidden()
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.prefs.getHidden");

            UnknownResponse response = GetResponseCache<UnknownResponse>(parameters);

            System.Xml.XmlNode nav = response.GetXmlDocument().SelectSingleNode("*/@hidden");
            if (nav == null)
                throw new ParsingException("Unable to find hidden preference in returned XML.");

            return (HiddenFromSearch)int.Parse(nav.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// Gets the currently authenticated users default content type.
        /// </summary>
        /// <returns></returns>
        public ContentType PrefsGetContentType()
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.prefs.getContentType");

            UnknownResponse response = GetResponseCache<UnknownResponse>(parameters);

            System.Xml.XmlNode nav = response.GetXmlDocument().SelectSingleNode("*/@content_type");
            if (nav == null)
                throw new ParsingException("Unable to find content type preference in returned XML.");

            return (ContentType)int.Parse(nav.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
        }
    }
}
