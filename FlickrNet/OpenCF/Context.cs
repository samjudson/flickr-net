//==========================================================================================
//
//		OpenNETCF.Windows.Forms.Context
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
using System.Text;

namespace FlickrNet.Security.Cryptography.NativeMethods
{
    internal class Context
	{
		//static constructor
		static Context()
		{
			FindWoteInfo();
		}

		private static void FindWoteInfo()
		{
			try	
			{
				ProviderInfo [] pia = Prov.EnumProviders();
				isCryptoApi = true;
				foreach(ProviderInfo pi in pia)
				{
					if(pi.name == ProvName.MS_ENHANCED_PROV)
						isEnhanced = true;
					if(pi.type == ProvType.DSS_DH)
						isDsa = true;
				}
				if(isEnhanced == false)
					provName = ProvName.MS_DEF_PROV; //dont default to enhanced anymore

			}
			catch(MissingMethodException) //mme
			{
				//dll or method is missing
				//properties default to false;
			}
			//all other exceptions bubble up
		}

		private static bool isCryptoApi = false;
		public static bool IsCryptoApi
		{
			get
			{
				return isCryptoApi;
			}
		}

		private static bool isEnhanced = false;
		public static bool IsEnhanced
		{
			get
			{
				return isEnhanced;
			}
		}

		private static bool isDsa = false;
		public static bool IsDsa
		{
			get
			{
				return isDsa;
			}
		}

		private static IntPtr prov = IntPtr.Zero;
		public static IntPtr Provider
		{
			get{return prov;}
			set{prov=value;}
		}

		private static ProvType provType = ProvType.RSA_FULL;
		public static ProvType ProviderType
		{
			get{return provType;}
			set{provType=value;}
		}

		private static string provName = ProvName.MS_ENHANCED_PROV;
		public static string ProviderName
		{
			get{return provName;}
			set{provName=value;}
		}

		private static string container = "bNbContainer";
		public static string KeyContainer
		{
			get{return container;}
			set{container=value;}
		}

		public static void ResetKeySet()
		{
			IntPtr prov = AcquireContext(container, provName, provType, ContextFlag.DELETEKEYSET);
			Context.ReleaseContext(prov);
			prov = AcquireContext(container, provName, provType, ContextFlag.NEWKEYSET);
			Context.ReleaseContext(prov);
		}

		/// <summary>
		/// MissingMethodException. call AcquireContext instead
		/// </summary>
		public static IntPtr CpAcquireContext(string container, ContextFlag flag)
		{
			IntPtr prov;
			StringBuilder sb = new StringBuilder(container);
			byte[] vTable = new byte[0]; //VTableProvStruc with callbacks
			bool retVal = Crypto.CPAcquireContext(out prov, sb, (uint) flag, vTable);
			ErrCode ec = Error.HandleRetVal(retVal);
			return prov;
		}

		public static IntPtr AcquireContext()
		{
			return AcquireContext(container, provName, provType, ContextFlag.NONE);
		}

		public static IntPtr AcquireContext(string container)
		{
			return AcquireContext(container, provName, provType, ContextFlag.NONE);
		}

		public static IntPtr AcquireContext(ProvType provType)
		{
			return AcquireContext(null, null, provType, ContextFlag.NONE);
		}

		public static IntPtr AcquireContext(string provName, ProvType provType)
		{
			return AcquireContext(null, provName, provType, ContextFlag.NONE);
		}

		public static IntPtr AcquireContext(string provName, ProvType provType, ContextFlag conFlag)
		{
			return AcquireContext(null, provName, provType, conFlag);
		}

		public static IntPtr AcquireContext(string conName, string provName, ProvType provType)
		{
			return AcquireContext(conName, provName, provType, ContextFlag.NONE);
		}

		public static IntPtr AcquireContext(string conName, string provName, ProvType provType, ContextFlag conFlag)
		{
			IntPtr hProv;
			bool retVal = Crypto.CryptAcquireContext(out hProv, conName, provName, (uint) provType, (uint) conFlag);
			ErrCode ec = Error.HandleRetVal(retVal, ErrCode.NTE_BAD_KEYSET);
			if(ec == ErrCode.NTE_BAD_KEYSET) //try creating a new key container
			{
				retVal = Crypto.CryptAcquireContext(out hProv, conName, provName, (uint) provType, (uint) ContextFlag.NEWKEYSET);
				ec = Error.HandleRetVal(retVal);
			}
			if(hProv == IntPtr.Zero)
				throw new Exception("bNb.Sec: " + ec.ToString());
			return hProv;
		}

		public static void ReleaseContext(IntPtr prov)
		{
			uint reserved = 0;
			if(prov != IntPtr.Zero)
			{
				bool retVal = Crypto.CryptReleaseContext(prov, reserved);
				ErrCode ec = Error.HandleRetVal(retVal); //dont exception
			}
		}

		/// <summary>
		/// INVALID_PARAMETER. no need to ever call this
		/// </summary>
		public static void ContextAddRef(IntPtr prov)
		{
			uint reserved = 0;
			uint flags = 0;
			bool retVal = Crypto.CryptContextAddRef(prov, ref reserved, flags);
			ErrCode ec = Error.HandleRetVal(retVal);
		}
	}
}
