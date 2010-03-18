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
            System.Security.Permissions.SecurityPermission permission = new System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode);
            permission.Demand();

			return System.Runtime.InteropServices.Marshal.GetHRForException(ioe) & 0xFFFF;
#else
            return 0;
#endif
		}
	}
}
