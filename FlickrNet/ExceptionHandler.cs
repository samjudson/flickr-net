using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using FlickrNet.Exceptions;

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

            return CreateException(code, msg);
        }

        private static FlickrApiException CreateException(int code, string message)
        {
            switch (code)
            {
                case 96:
                    return new InvalidSignatureException(message);
                case 97:
                    return new MissingSignatureException(message);
                case 98:
                    return new LoginFailedInvalidTokenException(message);
                case 99:
                    return new UserNotLoggedInInsufficientPermissionsException(message);
                case 100:
                    return new InvalidApiKeyException(message);
                case 105:
                    return new ServiceUnavailableException(message);
                case 111:
                    return new FormatNotFoundException(message);
                case 112:
                    return new MethodNotFoundException(message);
                case 116:
                    return new BadUrlFoundException(message);
                case 114: // Soap Error
                case 115: // XML-RPC error
                    return new FlickrApiException(code, message);
                default:
                    return CreateExceptionFromMessage(code, message);
            }

        }

        private static FlickrApiException CreateExceptionFromMessage(int code, string message)
        {
            switch (message)
            {
                case "Photo not found":
                case "Photo not found.":
                    return new PhotoNotFoundException(code, message);
                case "Photoset not found":
                case "Photoset not found.":
                    return new PhotosetNotFoundException(code, message);
                case "Permission Denied":
                    return new PermissionDeniedException(code, message);
                case "User not found":
                case "User not found.":
                    return new UserNotFoundException(code, message);
            }

            return new FlickrApiException(code, message);
        }

    }
}