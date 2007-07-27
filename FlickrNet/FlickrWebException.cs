using System;

namespace FlickrNet
{
	/// <summary>
	/// Exception thrown when a communication error occurs with a web call.
	/// </summary>
	public class FlickrWebException : FlickrException
	{
		internal FlickrWebException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
