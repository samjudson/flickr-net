using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace FlickrNet
{
    /// <summary>
    /// Summary description for SafeNativeMethods.
    /// </summary>
#if !WindowsCE && !SILVERLIGHT && !NETFX_CORE
    [SuppressUnmanagedCodeSecurity]
#endif
    internal class SafeNativeMethods
    {
        private SafeNativeMethods()
        {
        }

        internal static int GetErrorCode(IOException ioe)
        {
#if !WindowsCE && !SILVERLIGHT && !NETFX_CORE
            var permission = new System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode);
            permission.Demand();

            return Marshal.GetHRForException(ioe) & 0xFFFF;
#else
            return 0;
#endif
        }
    }
}