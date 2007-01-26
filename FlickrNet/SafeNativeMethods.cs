using System;

namespace FlickrNet
{
	/// <summary>
	/// Summary description for SafeNativeMethods.
	/// </summary>
	[System.Security.SuppressUnmanagedCodeSecurity()]
	internal class SafeNativeMethods 
	{
		private SafeNativeMethods()
		{
		}

		internal static int GetErrorCode(System.IO.IOException ioe)
		{
			return System.Runtime.InteropServices.Marshal.GetHRForException(ioe) & 0xFFFF;
		}
	}
}
