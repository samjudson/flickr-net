using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet.Exceptions
{
    /// <summary>
    /// No photoset with the photoset ID supplied to the method could be found.
    /// </summary>
    /// <remarks>
    /// This could mean the photoset does not exist, or that you do not have permission to view the photoset.
    /// </remarks>
    public class PhotosetNotFoundException : FlickrApiException
    {
        internal PhotosetNotFoundException(int code, string message)
            : base(code, message)
        {
        }
    }
}
