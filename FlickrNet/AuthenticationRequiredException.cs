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
        /// Initializes a new instance of the <see cref="AuthenticationRequiredException"/> class.
        /// </summary>
		public AuthenticationRequiredException() : base("Method requires authentication but no token supplied.")
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationRequiredException"/> class with a specified error message.
        /// </summary>
        /// <param name="message"></param>
        public AuthenticationRequiredException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationRequiredException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public AuthenticationRequiredException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

	}
}
