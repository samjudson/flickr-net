using System;

namespace FlickrNet
{
	/// <summary>
	/// Generic Flickr.Net Exception.
	/// </summary>
	[Serializable]
	public class FlickrException : Exception
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="FlickrException"/> class.
        /// </summary>
		public FlickrException()
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="FlickrException"/> class with a specified error message.
        /// </summary>
        /// <param name="message"></param>
		public FlickrException(string message) : base(message)
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="FlickrException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
		public FlickrException(string message, Exception innerException) : base(message, innerException)
		{
		}

#if !WindowsCE
        /// <summary>
        /// Initializes a new instance of the <see cref="FlickrException"/> class with serialized data.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected FlickrException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
#endif
	}
}
