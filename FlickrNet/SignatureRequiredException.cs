using System;

namespace FlickrNet
{
	/// <summary>
	/// Thrown when a method requires a valid signature but no shared secret has been supplied.
	/// </summary>
	public class SignatureRequiredException : FlickrException
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="SignatureRequiredException"/> class.
        /// </summary>
        public SignatureRequiredException()
            : base("Method requires signing but no shared secret supplied.")
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="SignatureRequiredException"/> class with a specified error message.
        /// </summary>
        /// <param name="message"></param>
        public SignatureRequiredException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignatureRequiredException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public SignatureRequiredException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignatureRequiredException"/> class with serialized data.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected SignatureRequiredException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }

	}
}
