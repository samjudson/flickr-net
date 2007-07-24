using System;

namespace FlickrNet
{
	/// <summary>
	/// Summary description for SafeNativeMethods.
	/// </summary>
#if !WindowsCE
	[System.Security.SuppressUnmanagedCodeSecurity()]
#endif
    internal class SafeNativeMethods 
	{
		private SafeNativeMethods()
		{
		}

		internal static int GetErrorCode(System.IO.IOException ioe)
		{
#if !WindowsCE
			return System.Runtime.InteropServices.Marshal.GetHRForException(ioe) & 0xFFFF;
#else
            return 0;
#endif
		}
	}
}
