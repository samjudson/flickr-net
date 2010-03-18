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
        /// Default constructor.
        /// </summary>
		public FlickrException()
		{
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
		public FlickrException(string message) : base(message)
		{
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
		public FlickrException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
