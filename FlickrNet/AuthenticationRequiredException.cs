using System;

namespace FlickrNet
{
	/// <summary>
	/// Exception thrown when method requires authentication but no authentication token is supplied.
	/// </summary>
    [Serializable]
	public class AuthenticationRequiredException : FlickrException
	{
        /// <summary>
        /// Default constructor.
        /// </summary>
		public AuthenticationRequiredException() : base("Method requires authentication but no token supplied.")
		{
		}
	}
}
