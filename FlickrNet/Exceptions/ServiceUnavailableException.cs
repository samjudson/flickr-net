using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet.Exceptions
{
    /// <summary>
    /// Error: 105: Service currently unavailable
    /// </summary>
    /// <remarks>
    /// The requested service is temporarily unavailable.
    /// </remarks>
    public class ServiceUnavailableException : FlickrApiException
    {
        internal ServiceUnavailableException(string message)
            : base(105, message)
        {
        }
    }
}
