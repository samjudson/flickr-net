using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// The exception thrown when an error occurred while trying to parse the response from Flickr. 
    /// </summary>
    /// <remarks>
    /// Usually because an unexpected element or attribute was encountered.
    /// </remarks>
    [Serializable]
    public class ParsingException : FlickrException
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ParsingException()
		{
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
		public ParsingException(string message) : base(message)
		{
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ParsingException(string message, Exception innerException)
            : base(message, innerException)
		{
		}

    }
}
