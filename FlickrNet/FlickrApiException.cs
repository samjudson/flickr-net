using System;

namespace FlickrNet
{
	/// <summary>
	/// Exception thrown when the Flickr API returned a specifi error code.
	/// </summary>
	public class FlickrApiException : FlickrException
	{
		private int _code;
		private string _message = String.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlickrApiException"/> class with a specific code and message.
        /// </summary>
        /// <remarks>
        /// The code and message returned from Flickr are used to generate the exceptions message.
        /// </remarks>
        /// <param name="code">The error code supplied by Flickr.</param>
        /// <param name="message">The error message supplied by Flickr.</param>
        public FlickrApiException(int code, string message) : base()
        {
            _code = code;
            _message = message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlickrApiException"/> class.
        /// </summary>
        public FlickrApiException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlickrApiException"/> class with a specified error message.
        /// </summary>
        /// <param name="message"></param>
        public FlickrApiException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlickrApiException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public FlickrApiException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlickrApiException"/> class with serialized data.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected FlickrApiException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }

		/// <summary>
		/// Get the code of the Flickr error.
		/// </summary>
		public int Code
		{
			get { return _code; }
		}

		/// <summary>
		/// Gets the verbose message returned by Flickr.
		/// </summary>
		public string Verbose
		{
			get { return _message; }
		}
		
		/// <summary>
		/// Overrides the message to return custom error message.
		/// </summary>
		public override string Message
		{
			get
			{
				return _message + " (" + _code + ")";
			}
		}
	}
}
