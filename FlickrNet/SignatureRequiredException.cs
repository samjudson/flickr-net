using System;

namespace FlickrNet
{
	/// <summary>
	/// Thrown when a method requires a valid signature but no shared secret has been supplied.
	/// </summary>
	public class SignatureRequiredException : FlickrException
	{
		internal SignatureRequiredException() : base("Method requires signing but no shared secret supplied.")
		{
		}
	}
}
