using System;

namespace FlickrNet
{
	/// <summary>
	/// Exception thrown is no API key is supplied.
	/// </summary>
    [Serializable]
	public class ApiKeyRequiredException : FlickrException
	{
		/// <summary>
        /// Initializes a new instance of the <see cref="ApiKeyRequiredException"/> class.
		/// </summary>
		public ApiKeyRequiredException() : base("API Key is required for all method calls")
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiKeyRequiredException"/> class with a specified error message.
        /// </summary>
        /// <param name="message"></param>
        public ApiKeyRequiredException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiKeyRequiredException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ApiKeyRequiredException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

	}
}
