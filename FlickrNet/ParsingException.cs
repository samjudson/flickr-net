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
        /// Initializes a new instance of the <see cref="ParsingException"/> class.
        /// </summary>
        public ParsingException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParsingException"/> class with a specified error message.
        /// </summary>
        /// <param name="message"></param>
        public ParsingException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParsingException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ParsingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

#if !WindowsCE
        /// <summary>
        /// Initializes a new instance of the <see cref="ParsingException"/> class with serialized data.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ParsingException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}
