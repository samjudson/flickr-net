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

        public FlickrApiException(int code, string message)
        {
            _code = code;
            _message = message;
        }

		internal FlickrApiException(ResponseError error)
		{
			_code = error.Code;
			_message = error.Message;
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
