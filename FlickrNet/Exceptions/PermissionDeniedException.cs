using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet.Exceptions
{
    /// <summary>
    /// Error: Permission Denied.
    /// </summary>
    /// <remarks>
    /// The owner of the photo does not want to share the data wih you.
    /// </remarks>
    public class PermissionDeniedException : FlickrApiException
    {
        internal PermissionDeniedException(int code, string message)
            : base(code, message)
        {
        }
    }
}
