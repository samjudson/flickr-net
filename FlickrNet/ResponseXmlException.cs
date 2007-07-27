using System;

namespace FlickrNet
{
	/// <summary>
	/// Exception thrown when an error parsing the returned XML.
	/// </summary>
	public class ResponseXmlException : FlickrException
	{
		internal ResponseXmlException(string message) : base(message)
		{
		}

		internal ResponseXmlException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
