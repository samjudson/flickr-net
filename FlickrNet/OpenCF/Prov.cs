//==========================================================================================
//
//		OpenNETCF.Windows.Forms.Prov
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
using System.Collections;

namespace FlickrNet.Security.Cryptography.NativeMethods
{
    internal class Prov
	{
		public static void SetProvParam(IntPtr prov, ProvParamSet param, byte [] data)
		{
			uint flags = 0;
			//Microsoft CSPs do not support the PP_CLIENT_HWND or PP_KEYSET_SEC_DESCR flags. 
			bool retVal = Crypto.CryptSetProvParam(prov, (uint)param, data, flags);
			ErrCode ec = Error.HandleRetVal(retVal);
		}

		public static PROV_ENUMALGS_EX [] GetProvAlgsEx(IntPtr prov)
		{
			ArrayList retAl = GetProvParam(prov, ProvParamEnum.ALGS_EX, 148);
			ArrayList al = new ArrayList();
			foreach(byte [] ba in retAl)
			{
				PROV_ENUMALGS_EX pe = new PROV_ENUMALGS_EX();
				pe.aiAlgid = BitConverter.ToUInt32(ba, 0);
				pe.dwDefaultLen = BitConverter.ToUInt32(ba, 4);
				pe.dwMinLen = BitConverter.ToUInt32(ba, 8);
				pe.dwMaxLen = BitConverter.ToUInt32(ba, 12);
				pe.dwProtocols = BitConverter.ToUInt32(ba, 16);
				pe.dwNameLen = BitConverter.ToUInt32(ba, 20);
				int nameLen = (int) (pe.dwNameLen * 2) -1 ; //nullTerm
				if(nameLen > 0)
					pe.szName = Encoding.Unicode.GetString(ba, 24, nameLen);
				pe.dwLongNameLen = BitConverter.ToUInt32(ba, 44);
				nameLen = (int) (pe.dwLongNameLen * 2) - 1; //nullTerm
				if(nameLen > 0)
				{
					nameLen = Math.Min(80, nameLen);
					pe.szLongName = Encoding.Unicode.GetString(ba, 48, nameLen);
				}
				if(pe.aiAlgid != 0)
					al.Add(pe);
			}
			PROV_ENUMALGS_EX [] pea = new PROV_ENUMALGS_EX[al.Count];
			al.CopyTo(0, pea, 0, pea.Length);
			return pea;
		}

		public static PROV_ENUMALGS [] GetProvAlgs(IntPtr prov)
		{
			ArrayList retAl = GetProvParam(prov, ProvParamEnum.ALGS, 52);
			ArrayList al = new ArrayList();
			foreach(byte [] ba in retAl)
			{
				PROV_ENUMALGS pe = new PROV_ENUMALGS();
				pe.aiAlgid = BitConverter.ToUInt32(ba, 0);
				pe.dwBitLen = BitConverter.ToUInt32(ba, 4);
				pe.dwNameLen = BitConverter.ToUInt32(ba, 8);
				int nameLen = (int) (pe.dwNameLen * 2) - 1; //nullTerm
				if(nameLen > 0)
					pe.szName = Encoding.Unicode.GetString(ba, 12, nameLen);
				if(pe.aiAlgid != 0)
					al.Add(pe);
			}
			PROV_ENUMALGS [] pea = new PROV_ENUMALGS[al.Count];
			al.CopyTo(0, pea, 0, pea.Length);
			return pea;
		}

		public static ArrayList GetProvParam(IntPtr prov, ProvParamEnum param, uint dataLen)
		{
			ArrayList al = new ArrayList();
			byte[] data = null;
			uint flags = Const.CRYPT_FIRST;
			while ( true )
			{
				data = new byte[dataLen];
				bool retVal = Crypto.CryptGetProvParam(prov, (uint) param, data, ref dataLen, flags);
				ErrCode [] eca = new ErrCode[]{ErrCode.NO_MORE_ITEMS, ErrCode.MORE_DATA};
				ErrCode ec = Error.HandleRetVal(retVal, eca);
				if ( !retVal )
				{
					if ( ec == ErrCode.NO_MORE_ITEMS )
						break;
					if ( ec != ErrCode.MORE_DATA )
						break;
				}
				flags = 0;
				al.Add(data);
			}
			return al;
		}

		public static byte [] GetProvParam(IntPtr prov, ProvParam param)
		{
			byte[] data = new byte[0];
			uint dataLen = 0;
			uint flags = 0;
			bool retVal = Crypto.CryptGetProvParam(prov, (uint) param, data, ref dataLen, flags);
			ErrCode ec = Error.HandleRetVal(retVal, ErrCode.MORE_DATA);
			if(ec == ErrCode.MORE_DATA)
			{
				data = new byte[(int)dataLen];
				retVal = Crypto.CryptGetProvParam(prov, (uint) param, data, ref dataLen, flags);
				ec = Error.HandleRetVal(retVal);
			}
			return data;
		}

		/// <summary>
		/// INVALID_PARAMETER. call SetProvider instead
		/// </summary>
		public static void SetProviderEx(string provName, ProvType provType, ProvDefaultFlag provDefFlag)
		{
			uint reserved = 0;
			bool retVal = Crypto.CryptSetProviderEx(provName, (uint) provType, ref reserved, (uint) provDefFlag);
			ErrCode ec = Error.HandleRetVal(retVal);
		}

		public static void SetProvider(string provName, ProvType provType)
		{
			bool retVal = Crypto.CryptSetProvider(provName, (uint) provType);
			ErrCode ec = Error.HandleRetVal(retVal);
		}

		/// <summary>
		/// INVALID_PARAMETER. call Context.AcquireContext with minimal values instead
		/// </summary>
		public static string GetDefaultProvider(ProvType provType, ProvDefaultFlag provDefFlag)
		{
			uint reserved = 0;
			uint flags = (uint) provDefFlag;
			uint dataLen = 0;
			StringBuilder provName = new StringBuilder();
			bool retVal = Crypto.CryptGetDefaultProvider((uint)provType, ref reserved, flags, provName, ref dataLen);
			ErrCode ec = Error.HandleRetVal(retVal, ErrCode.MORE_DATA);
			if(ec == ErrCode.MORE_DATA)
			{
				provName = new StringBuilder((int)dataLen);
				retVal = Crypto.CryptGetDefaultProvider((uint)provType, ref reserved, flags, provName, ref dataLen);
				ec = Error.HandleRetVal(retVal);
			}
			string name = provName.ToString();
			if(name == null || name == String.Empty)
				throw new Exception(ec.ToString());
			return name;
		}

		/// <summary>
		/// works by calling EnumProviders. otherwise INVALID_PARAMETER
		/// </summary>
		public static ProvType [] EnumProviderTypes()
		{
			uint [] ia;
			ProviderInfo [] providers = EnumProviders(out ia);
			ProvType [] pta = new ProvType[ia.Length];
			for(int i=0; i<ia.Length; i++)
			{
				pta[i] = (ProvType) ia[i];
			}
			return pta;
		}

		public static ProviderInfo [] EnumProviders()
		{
			uint [] ia;
			return EnumProviders(out ia);
		}

		private static ProviderInfo [] EnumProviders(out uint [] provTypes)
		{
			ArrayList alProv = new ArrayList();
			ArrayList alProvType = new ArrayList();
			uint dwIndex = 0;
			uint dwReserved = 0;
			uint dwFlags = 0;
			uint dwProvType = 0;
			uint cbProvName = 0;
			StringBuilder sProv = new StringBuilder();
			while ( true )
			{
				bool retVal = Crypto.CryptEnumProviders(dwIndex, ref dwReserved, dwFlags, ref dwProvType, sProv, ref cbProvName);
				ErrCode [] eca = new ErrCode[]{ErrCode.NO_MORE_ITEMS, ErrCode.MORE_DATA};
				ErrCode ec = Error.HandleRetVal(retVal, eca);
				if ( !retVal )
				{					
					if ( ec == ErrCode.NO_MORE_ITEMS )
						break;
					if ( ec != ErrCode.MORE_DATA )
						break;
				}
				sProv = new System.Text.StringBuilder((int)cbProvName);
				retVal = Crypto.CryptEnumProviders(dwIndex, ref dwReserved, dwFlags, ref dwProvType, sProv, ref cbProvName);
				ec = Error.HandleRetVal(retVal);
				dwIndex = dwIndex + 1;
				cbProvName = 0;
				ProviderInfo pi = new ProviderInfo();
				pi.name = sProv.ToString().TrimEnd(new char['\0']);
				pi.type = (ProvType) dwProvType;
				alProv.Add(pi);
				if(alProvType.Contains(dwProvType) == false)
					alProvType.Add(dwProvType);
			}
			provTypes = new uint[alProvType.Count];
			alProvType.CopyTo(0, provTypes, 0, provTypes.Length);
			ProviderInfo [] pia = new ProviderInfo[alProv.Count];
			alProv.CopyTo(0, pia, 0, pia.Length);
			return pia;
		}
	}
}
