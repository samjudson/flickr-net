using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet.Exceptions
{
    /// <summary>
    /// No photo with the photo ID supplied to the method could be found.
    /// </summary>
    /// <remarks>
    /// This could mean the photo does not exist, or that you do not have permission to view the photo.
    /// </remarks>
    public class PhotoNotFoundException : FlickrApiException
    {
        internal PhotoNotFoundException(int code, string message)
            : base(code, message)
        {
        }
    }
}
