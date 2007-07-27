using System;

namespace FlickrNet
{
	/// <summary>
	/// Exception thrown when the Flickr API returned a specifi error code.
	/// </summary>
	public class FlickrApiException : FlickrException
	{
		private int code;
		private string msg = "";

		internal FlickrApiException(ResponseError error)
		{
			code = error.Code;
			msg = error.Message;
		}

		/// <summary>
		/// Get the code of the Flickr error.
		/// </summary>
		public int Code
		{
			get { return code; }
		}

		/// <summary>
		/// Gets the verbose message returned by Flickr.
		/// </summary>
		public string Verbose
		{
			get { return msg; }
		}
		
		/// <summary>
		/// Overrides the message to return custom error message.
		/// </summary>
		public override string Message
		{
			get
			{
				return msg + " (" + code + ")";
			}
		}
	}
}
