using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet.Exceptions
{
    /// <summary>
    /// Error 97: Missing signature exception.
    /// </summary>
    /// <remarks>
    /// The call required signing but no signature was sent.
    /// </remarks>
    public class MissingSignatureException : FlickrApiException
    {
        internal MissingSignatureException(string message)
            : base(97, message)
        {
        }
    }
}
