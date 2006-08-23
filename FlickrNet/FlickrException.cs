using System;

namespace FlickrNet
{
	/// <summary>
	/// A FlickrException, thrown when a connection to Flickr fails.
	/// </summary>
	[Serializable]
	public class FlickrException : Exception
	{
		private int code;
		private string msg = "";

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
		/// Creates a new exception with the given the code and verbose message string
		/// </summary>
		/// <param name="code">The code of the error. 100 is Invalid Api Key and 99 is User not logged in. Others are method specific.</param>
		/// <param name="verbose">The verbose description of the error.</param>
		public FlickrException(int code, string verbose)
		{
			this.code = code;
			msg = verbose;
		}

		/// <summary>
		/// Creates a new exception from the <see cref="ResponseError"/> class.
		/// </summary>
		/// <param name="error">An instance of the <see cref="ResponseError"/> class.</param>
		public FlickrException(ResponseError error)
		{
			code = error.Code;
			msg = error.Message;
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
