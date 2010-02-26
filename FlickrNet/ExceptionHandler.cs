using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    public static class ExceptionHandler
    {
        public static Exception CreateResponseException(System.Xml.XmlReader reader)
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
                    if (!int.TryParse(reader.Value, out code))
                        throw new FlickrException("Invalid 'code' found attribute found in error message. Code is not an integer");
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