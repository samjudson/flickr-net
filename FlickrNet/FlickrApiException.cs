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
        /// Constructor to create a FlickrApiException from the error code and message supplied by Flickr.
        /// </summary>
        /// <param name="code">The error code supplied by Flickr.</param>
        /// <param name="message">The error message supplied by Flickr.</param>
        public FlickrApiException(int code, string message)
        {
            _code = code;
            _message = message;
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
