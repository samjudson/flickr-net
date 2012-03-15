using System;

using System.Collections.Generic;
using System.Text;

namespace FlickrNet.Exceptions
{
    /// <summary>
    /// The method name supplied was not recognised by Flickr.
    /// </summary>
    /// <remarks>
    /// While using the FlickrNet library you should not encounter this error, 
    /// unless Flickr removes a particular method from the API.
    /// </remarks>
    public sealed class MethodNotFoundException : FlickrApiException
    {
        internal MethodNotFoundException(string message)
            : base(111, message)
        {
        }

    }
}
