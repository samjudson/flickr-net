using System;

namespace FlickrNet
{
	/// <summary>
	/// Exception thrown when a communication error occurs with a web call.
	/// </summary>
    [Serializable]
    public class FlickrWebException : FlickrException
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="FlickrWebException"/> class.
        /// </summary>
        public FlickrWebException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlickrWebException"/> class with a specified error message.
        /// </summary>
        /// <param name="message"></param>
        public FlickrWebException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlickrWebException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public FlickrWebException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

	}
}
