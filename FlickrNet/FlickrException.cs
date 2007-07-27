using System;

namespace FlickrNet
{
	/// <summary>
	/// Generic Flickr.Net Exception.
	/// </summary>
	[Serializable]
	public class FlickrException : Exception
	{
		internal FlickrException()
		{
		}

		internal FlickrException(string message) : base(message)
		{
		}

		internal FlickrException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
