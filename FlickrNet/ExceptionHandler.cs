using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// A handler that is used to generate an exception from the response sent back by Flickr.
    /// </summary>
    public static class ExceptionHandler
    {
        /// <summary>
        /// Creates a <see cref="FlickrApiException"/> from the response sent back from Flickr.
        /// </summary>
        /// <param name="reader">The <see cref="XmlReader"/> containing the response from Flickr.</param>
        /// <returns>The <see cref="FlickrApiException"/> created from the information returned by Flickr.</returns>
        public static Exception CreateResponseException(XmlReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            reader.MoveToElement();

            if (!reader.ReadToDescendant("err"))
                throw new System.Xml.XmlException("No error element found in XML");

            int code = 0;
            string msg = null;

            while (reader.MoveToNextAttribute())
            {
                if (reader.LocalName == "code")
                {
                    try
                    {
                        code = int.Parse(reader.Value, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo);
                    }
                    catch (FormatException)
                    {
                        throw new FlickrException("Invalid value found in code attribute. Value '" + code + "' is not an integer");
                    }
                    continue;
                }
                if (reader.LocalName == "msg")
                {
                    msg = reader.Value;
                    continue;
                }
            }

            return new FlickrApiException(code, msg);
        }
    }
}