using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet.Exceptions
{
    /// <summary>
    /// Error: 99: User not logged in / Insufficient permissions
    /// </summary>
    /// <remarks>
    /// The method requires user authentication but the user was not logged in, 
    /// or the authenticated method call did not have the required permissions.
    /// </remarks>
    public class UserNotLoggedInInsufficientPermissionsException : FlickrApiException
    {
        internal UserNotLoggedInInsufficientPermissionsException(string message)
            : base(99, message)
        {
        }
    }
}
