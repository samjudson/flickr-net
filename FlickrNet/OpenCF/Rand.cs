//==========================================================================================
//
//		FlickrNet.Security.Cryptography.NativeMethods.Rand
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

namespace FlickrNet.Security.Cryptography.NativeMethods
{
    internal class Rand
	{
		/// <summary>
		/// if crypto is not available
		/// </summary>
		/// <remarks>not on PPC 2002 device</remarks>
		public static byte [] CeGenRandom(int length)
		{
			byte [] baTemp = new byte[length];
			bool retVal = Crypto.CeGenRandom(baTemp.Length, baTemp);
			ErrCode ec = Error.HandleRetVal(retVal);
			return baTemp;
		}

		/// <summary>
		/// not seeded
		/// </summary>
		public static byte [] GetRandomBytes(int length)
		{
			byte[] randomBuf = new byte[length];
			return GetRandomBytes(randomBuf);
		}

		/// <summary>
		/// seeded, dont have to specify a provider
		/// </summary>
		public static byte [] GetRandomBytes(byte[] seed)
		{
			IntPtr prov = Context.AcquireContext(ProvType.RSA_FULL);
			bool retVal = Crypto.CryptGenRandom(prov, seed.Length, seed);
			ErrCode ec = Error.HandleRetVal(retVal);
			Context.ReleaseContext(prov);
			return seed;
		}

		public static byte [] GetNonZeroBytes(byte[] seed)
		{
			byte [] buffer = GetRandomBytes(seed);
			for(int i=0; i<buffer.Length; i++)
			{
				if(buffer[i] == 0)
					buffer[i] = 1;
			}
			return buffer;
		}
	}
}
