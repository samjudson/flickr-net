using System;
using System.Security.Permissions;

namespace FlickrNet
{
	/// <summary>
	/// Exception thrown when the Flickr API returned a specifi error code.
	/// </summary>
    [Serializable]
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

#if !WindowsCE
        /// <summary>
        /// Initializes a new instance of the <see cref="FlickrApiException"/> class with serialized data.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected FlickrApiException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            if (info == null) throw new ArgumentNullException("info");

            _code = info.GetInt32("Code");
            _message = info.GetString("Verbose");
        }
#endif

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

#if !WindowsCE
        /// <summary>
        /// When overridden in a derived class, sets the <see cref="System.Runtime.Serialization.SerializationInfo"/> with information about the exception.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");

            info.AddValue("Code", _code);
            info.AddValue("Verbose", _message);

            base.GetObjectData(info, context);
        }
#endif
	}
}
