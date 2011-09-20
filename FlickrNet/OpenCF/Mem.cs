//==========================================================================================
//
//		OpenNETCF.Windows.Forms.Mem
//		Copyright (c) 2003, OpenNETCF.org
//
//		This library is free software; you can redistribute it and/or modify it under 
//		the terms of the OpenNETCF.org Shared Source License.
//
//		This library is distributed in the hope that it will be useful, but 
//		WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or 
//		FITNESS FOR A PARTICULAR PURPOSE. See the OpenNETCF.org Shared Source License 
//		for more details.
//
//		You should have received a copy of the OpenNETCF.org Shared Source License 
//		along with this library; if not, email licensing@opennetcf.org to request a copy.
//
//		If you wish to contact the OpenNETCF Advisory Board to discuss licensing, please 
//		email licensing@opennetcf.org.
//
//		For general enquiries, email enquiries@opennetcf.org or visit our website at:
//		http://www.opennetcf.org
//
//		!!! A HUGE thank-you goes out to Casey Chesnut for supplying this class library !!!
//      !!! You can contact Casey at http://www.brains-n-brawn.com                      !!!
//
//==========================================================================================
using System;
using System.Runtime.InteropServices;

namespace FlickrNet.Security.Cryptography.NativeMethods
{
	//http://smartdevices.microsoftdev.com/Learn/Articles/500.aspx#netcfadvinterop_topic3
    internal class Mem
	{
		/// <summary>
		/// The CryptMemAlloc function allocates memory for a buffer. 
		/// It is used by all Crypt32.lib functions that return allocated buffers.
		/// </summary>
		/// <param name="cbSize">Number of bytes to be allocated. </param>
		/// <returns>Returns a pointer to the buffer allocated. 
		/// If the function fails, NULL is returned. </returns>
		//LPVOID WINAPI CryptMemAlloc(ULONG cbSize);
		[DllImport("crypt32.dll", EntryPoint="CryptMemAlloc", SetLastError=true)]
		public static extern IntPtr CryptMemAlloc(int cbSize);
		
		/// <summary>
		/// The CryptMemFree function frees memory allocated by 
		/// CryptMemAlloc or CryptMemRealloc.
		/// </summary>
		/// <param name="pv">Pointer to the buffer to be freed. </param>
		//void WINAPI CryptMemFree(LPVOID pv);
		[DllImport("crypt32.dll", EntryPoint="CryptMemFree", SetLastError=true)]
		public static extern void CryptMemFree(IntPtr pv);
		
		/// <summary>
		/// The CryptMemRealloc function frees the memory currently allocated for a buffer 
		/// and allocates memory for a new buffer.
		/// </summary>
		/// <param name="pv">Pointer to a currently allocated buffer. </param>
		/// <param name="cbSize">Number of bytes to be allocated. </param>
		/// <returns>Returns a pointer to the buffer allocated. 
		/// If the function fails, NULL is returned. </returns>
		//LPVOID WINAPI CryptMemRealloc(LPVOID pv, ULONG cbSize);
		[DllImport("crypt32.dll", EntryPoint="CryptMemRealloc", SetLastError=true)]
		public static extern IntPtr CryptMemRealloc(IntPtr pv, int cbSize);

		private const uint LMEM_FIXED = 0x0000;
		private const uint LMEM_MOVEABLE = 0x0002;
		private const uint LMEM_ZEROINIT = 0x0040;
		private const uint LPTR = (LMEM_FIXED | LMEM_ZEROINIT);

		[DllImport("coredll.dll", EntryPoint="LocalAlloc", SetLastError=true)]
		private static extern IntPtr LocalAllocCe(uint uFlags, int uBytes);
		[DllImport("kernel32.dll", EntryPoint="LocalAlloc", SetLastError=true)]
		private static extern IntPtr LocalAllocXp(uint uFlags, int uBytes);
		private static IntPtr LocalAlloc(uint uFlags, int uBytes)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return LocalAllocCe(uFlags, uBytes);
			else
				return LocalAllocXp(uFlags, uBytes);
		}

		[DllImport("coredll.dll", EntryPoint="LocalFree", SetLastError=true)]
		private static extern IntPtr LocalFreeCe(IntPtr hMem);
		[DllImport("kernel32.dll", EntryPoint="LocalFree", SetLastError=true)]
		private static extern IntPtr LocalFreeXp(IntPtr hMem);
		private static IntPtr LocalFree(IntPtr hMem)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return LocalFreeCe(hMem);
			else
				return LocalFreeXp(hMem);
		}

		[DllImport("coredll.dll", EntryPoint="LocalReAlloc", SetLastError=true)]
		private static extern IntPtr LocalReAllocCe(IntPtr hMem, uint uBytes, uint fuFlags);
		[DllImport("kernel32.dll", EntryPoint="LocalReAlloc", SetLastError=true)]
		private static extern IntPtr LocalReAllocXp(IntPtr hMem, uint uBytes, uint fuFlags);
		private static IntPtr LocalReAlloc(IntPtr hMem, uint uBytes, uint fuFlags)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return LocalReAllocCe(hMem, uBytes, fuFlags);
			else
				return LocalReAllocXp(hMem, uBytes, fuFlags);
		}

		public static IntPtr ReAllocHLocal(IntPtr pv, uint cb)
		{
			IntPtr newMem = LocalReAlloc(pv, cb, LMEM_MOVEABLE);
			if(newMem == IntPtr.Zero)
				throw new OutOfMemoryException("unmanaged memory wasnt re-allocated");
			return newMem;
		}

		public static IntPtr AllocHGlobal(int size)
		{
			IntPtr hglobal = LocalAlloc(LPTR, size);
			if(hglobal == IntPtr.Zero)
				throw new OutOfMemoryException("unmanaged memory wasnt allocated");
			return hglobal;
		}

		public static void FreeHGlobal(IntPtr hglobal)
		{
			IntPtr retVal = LocalFree(hglobal);
			if(retVal != IntPtr.Zero)
				throw new Exception("unmanaged memory wasnt freed");
		}
	}
}
