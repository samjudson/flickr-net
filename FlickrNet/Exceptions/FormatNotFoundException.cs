using System;

using System.Collections.Generic;
using System.Text;

namespace FlickrNet.Exceptions
{
    /// <summary>
    /// The specified format (e.g. json) was not found.
    /// </summary>
    /// <remarks>
    /// The FlickrNet library only uses one format, so you should not experience this error.
    /// </remarks>
    public sealed class FormatNotFoundException : FlickrApiException
    {
        internal FormatNotFoundException(string message)
            : base(111, message)
        {
        }

    }
}
