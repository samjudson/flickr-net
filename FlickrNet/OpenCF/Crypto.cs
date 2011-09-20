//==========================================================================================
//
//		OpenNETCF.Windows.Forms.Crypto
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
using System.Runtime.InteropServices;

namespace FlickrNet.Security.Cryptography.NativeMethods
{
	internal class Crypto
	{
		public const string coredll = "coredll.dll";
		public const string advapi32 = "advapi32.dll";
		public const string crypt32 = "crypt32.dll";

		///<summary>
		///This function fills a buffer with random bytes. You can use this function when 
		///Cryptography Services features are not available on your platform.
		///</summary>
		/// <remarks>
		/// worked on the SmartPhone
		/// </remarks>
		public static bool CeGenRandom(int dwLen, byte[] pbBuffer)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CeGenRandomCe(dwLen, pbBuffer);
			else
			{
                Random r = new Random();
				//Random r = new Random(EnvironmentEx.TickCount);
				r.NextBytes(pbBuffer);
				return true;
			}
		}
		//BOOL CeGenRandom(DWORD dwLen, BYTE* pbBuffer);
		[DllImport(coredll, EntryPoint="CeGenRandom", SetLastError=true)] 
		private static extern bool CeGenRandomCe(int dwLen, byte[] pbBuffer); 

		///<summary>
		///This function acquires a handle to the key container specified by the pszContainer 
		///parameter.
		///</summary>
		/// <remarks>
		/// does not work on smartPhone, MissingMethodException
		/// </remarks>
		public static bool CPAcquireContext(out IntPtr phProv, StringBuilder pszContainer, uint dwFlags, byte[] pVTable)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CPAcquireContextCe(out phProv, pszContainer, dwFlags, pVTable);
			else
				return CPAcquireContextXp(out phProv, pszContainer, dwFlags, pVTable);
		}
		//BOOL CPAcquireContext(HCRYPTPROV* phProv, WCHAR* pszContainer, DWORD dwFlags, PVTableProvStruc pVTable);
		[DllImport(coredll, EntryPoint="CPAcquireContext", SetLastError=true)] 
		private static extern bool CPAcquireContextCe(out IntPtr phProv, StringBuilder pszContainer, uint dwFlags, byte[] pVTable);
		[DllImport(advapi32, EntryPoint="CPAcquireContext", SetLastError=true)] 
		private static extern bool CPAcquireContextXp(out IntPtr phProv, StringBuilder pszContainer, uint dwFlags, byte[] pVTable);

		///<summary>
		///This function acquires a handle to a specific key container within a particular 
		///cryptographic service provider (CSP). This handle can be used to make calls to the 
		///selected CSP.
		///</summary>
		/// <remarks>
		/// raCrypto, mca / works on smartPhone
		/// </remarks>
		public static bool CryptAcquireContext(out IntPtr hProv, string pszContainer, string pszProvider, uint dwProvType, uint dwFlags)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptAcquireContextCe(out hProv, pszContainer, pszProvider, dwProvType, dwFlags);
			else
				return CryptAcquireContextXp(out hProv, pszContainer, pszProvider, dwProvType, dwFlags);
		}
		//1    0 0000ABCC CPAcquireContext 
		//BOOL WINAPI CryptAcquireContext(HCRYPTPROV* phProv, LPCTSTR pszContainer, LPCTSTR pszProvider, DWORD dwProvType, DWORD dwFlags);
		[DllImport(coredll, EntryPoint="CryptAcquireContext", SetLastError=true)] 
		private static extern bool CryptAcquireContextCe(out IntPtr hProv, string pszContainer, string pszProvider, uint dwProvType, uint dwFlags); 
		[DllImport(advapi32, EntryPoint="CryptAcquireContext", SetLastError=true)] 
		private static extern bool CryptAcquireContextXp(out IntPtr hProv, string pszContainer, string pszProvider, uint dwProvType, uint dwFlags); 

		///<summary>
		///This function adds one to the reference count of an HCRYPTPROV handle. 
		///</summary>
		/// <remarks>
		/// did not work on smartPhone, dont need it
		/// </remarks>
		public static bool CryptContextAddRef(IntPtr hProv, ref uint pdwReserved, uint dwFlags)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptContextAddRefCe(hProv, ref pdwReserved, dwFlags);
			else
				return CryptContextAddRefXp(hProv, ref pdwReserved, dwFlags);
		}
		//BOOL WINAPI CryptContextAddRef(HCRYPTPROV hProv, DWORD* pdwReserved, DWORD dwFlags);
		[DllImport(coredll, EntryPoint="CryptContextAddRef", SetLastError=true)] 
		private static extern bool CryptContextAddRefCe(IntPtr hProv, ref uint pdwReserved, uint dwFlags);
		[DllImport(advapi32, EntryPoint="CryptContextAddRef", SetLastError=true)] 
		private static extern bool CryptContextAddRefXp(IntPtr hProv, ref uint pdwReserved, uint dwFlags);

		///<summary>
		///This function initiates the hashing of a stream of data. It creates and returns to 
		///the calling application a handle to a cryptographic service provider (CSP) hash 
		///object. This handle is used in subsequent calls to the CryptHashData function and 
		///CryptHashSessionKey function to hash streams of data and session keys.
		///</summary>
		/// <remarks>
		/// raCrypto, mca / worked on smartPhone
		/// </remarks>
		public static bool CryptCreateHash(IntPtr hProv, uint Algid, IntPtr hKey, uint dwFlags, out IntPtr phHash)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptCreateHashCe(hProv, Algid, hKey, dwFlags, out phHash); 
			else
				return CryptCreateHashXp(hProv, Algid, hKey, dwFlags, out phHash); 
		}
		//2    1 00004E78 CPCreateHash 
		//BOOL WINAPI CryptCreateHash(HCRYPTPROV hProv, ALG_ID Algid, HCRYPTKEY hKey, DWORD dwFlags, HCRYPTHASH* phHash);
		[DllImport(coredll, EntryPoint="CryptCreateHash", SetLastError=true)] 
		private static extern bool CryptCreateHashCe(IntPtr hProv, uint Algid, IntPtr hKey, uint dwFlags, out IntPtr phHash); 
		[DllImport(advapi32, EntryPoint="CryptCreateHash", SetLastError=true)] 
		private static extern bool CryptCreateHashXp(IntPtr hProv, uint Algid, IntPtr hKey, uint dwFlags, out IntPtr phHash); 

		///<summary>
		///This function decrypts data that was previously encrypted with the CryptEncrypt 
		///function.
		///</summary>
		/// <remarks>
		/// raCrypto, mca / works on smartPhone
		/// </remarks>
		public static bool CryptDecrypt(IntPtr hKey, IntPtr hHash, bool Final, uint dwFlags, byte[] pbData, ref uint pdwDataLen)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptDecryptCe(hKey, hHash, Final, dwFlags, pbData, ref pdwDataLen);
			else
				return CryptDecryptXp(hKey, hHash, Final, dwFlags, pbData, ref pdwDataLen);
		}
		//3    2 00004CC4 CPDecrypt 
		//BOOL CRYPTFUNC CryptDecrypt(HCRYPTKEY hKey, HCRYPTHASH hHash, BOOL Final, DWORD dwFlags, BYTE *pbData, DWORD *pdwDataLen);
		[DllImport(coredll, EntryPoint="CryptDecrypt", SetLastError=true)]
		private static extern bool CryptDecryptCe(IntPtr hKey, IntPtr hHash, bool Final, uint dwFlags, byte[] pbData, ref uint pdwDataLen);
		[DllImport(advapi32, EntryPoint="CryptDecrypt", SetLastError=true)]
		private static extern bool CryptDecryptXp(IntPtr hKey, IntPtr hHash, bool Final, uint dwFlags, byte[] pbData, ref uint pdwDataLen);

		///<summary>
		///This function generates cryptographic session keys derived from base data. This 
		///function guarantees that all keys generated from the same base data are identical, 
		///provided the same cryptographic service provider (CSP) and algorithms are used. 
		///The base data can be a password or any other user data.
		///This function is the same as the CryptGenKey function, except that the generated 
		///session keys are derived from base data instead of being random. The CryptDeriveKey 
		///function can only generate session keys and cannot be used to generate 
		///public/private key pairs.
		///A handle to the session key is returned in the phKey parameter. This handle can 
		///then be used with any CryptoAPI functions that require key handles.
		///</summary>
		/// <remarks>
		/// raCrypto, mca / works on smartPhone
		/// </remarks>
		public static bool CryptDeriveKey(IntPtr hProv, uint Algid, IntPtr hBaseData, uint dwFlags, out IntPtr phKey)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptDeriveKeyCe(hProv, Algid, hBaseData, dwFlags, out phKey);
			else
				return CryptDeriveKeyXp(hProv, Algid, hBaseData, dwFlags, out phKey);
		}
		//4    3 000066D0 CPDeriveKey 
		//BOOL CRYPTFUNC CryptDeriveKey(HCRYPTPROV hProv, ALG_ID Algid, HCRYPTHASH hBaseData, DWORD dwFlags, HCRYPTKEY *phKey);
		[DllImport(coredll, EntryPoint="CryptDeriveKey", SetLastError=true)]
		private static extern bool CryptDeriveKeyCe(IntPtr hProv, uint Algid, IntPtr hBaseData, uint dwFlags, out IntPtr phKey);
		[DllImport(advapi32, EntryPoint="CryptDeriveKey", SetLastError=true)]
		private static extern bool CryptDeriveKeyXp(IntPtr hProv, uint Algid, IntPtr hBaseData, uint dwFlags, out IntPtr phKey);
		
		///<summary>
		///This function destroys the hash object referenced by the hHash parameter. Once a 
		///hash object has been destroyed, it can no longer be used and its handle is useless 
		///from then on.
		///All hash objects should be destroyed with the CryptDestroyHash function when the 
		///application is finished with them.
		///</summary>
		/// <remarks>
		/// raCrypto / worked on smartPhone
		/// </remarks>
		public static bool CryptDestroyHash(IntPtr hHash)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptDestroyHashCe(hHash); 
			else
				return CryptDestroyHashXp(hHash); 
		}
		//5    4 00005A90 CPDestroyHash 
		//BOOL CRYPTFUNC CryptDestroyHash(HCRYPTHASH hHash);
		[DllImport(coredll, EntryPoint="CryptDestroyHash", SetLastError=true)] 
		private static extern bool CryptDestroyHashCe(IntPtr hHash); 
		[DllImport(advapi32, EntryPoint="CryptDestroyHash", SetLastError=true)] 
		private static extern bool CryptDestroyHashXp(IntPtr hHash); 
		
		///<summary>
		///This function releases the handle referenced by the hKey parameter. Once a key 
		///handle has been released, it becomes invalid and cannot be used again.
		///If the handle refers to a session key, or to a public key that has been imported 
		///into the cryptographic service provider (CSP) through CryptImportKey, the 
		///CryptDestroyKey function destroys the key and frees the memory that the key 
		///occupied. Many CSPs scrub the memory where the key was held before freeing it.																																																																															  
		///On the other hand, if the handle refers to a public/private key pair obtained from 
		///the CryptGetUserKey function, the underlying key pair is not destroyed by the 
		///CryptDestroyKey function. Only the handle is destroyed.
		///</summary>
		/// <remarks>
		/// raCrypto / works on smartPhone
		/// </remarks>
		public static bool CryptDestroyKey(IntPtr hKey)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptDestroyKeyCe(hKey);
			else
				return CryptDestroyKeyXp(hKey);
		}
		//6    5 000076A8 CPDestroyKey 
		//BOOL CRYPTFUNC CryptDestroyKey(HCRYPTKEY hKey);
		[DllImport(coredll, EntryPoint="CryptDestroyKey", SetLastError=true)] 
		private static extern bool CryptDestroyKeyCe(IntPtr hKey);
		[DllImport(advapi32, EntryPoint="CryptDestroyKey", SetLastError=true)] 
		private static extern bool CryptDestroyKeyXp(IntPtr hKey);
		
		///<summary>
		///This function makes an exact copy of a hash and the state the hash is in.
		///A hash can be created in a piece-by-piece way. This function can create separate 
		///hashes of two different contents that begin with the same content. 
		///</summary>
		/// <remarks>
		/// did not work on smartPhone, dont need it
		/// </remarks>
		public static bool CryptDuplicateHash(IntPtr hHash, ref uint pdwReserved, uint dwFlags, out IntPtr phHash)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptDuplicateHashCe(hHash, ref pdwReserved, dwFlags, out phHash);
			else
				return CryptDuplicateHashXp(hHash, ref pdwReserved, dwFlags, out phHash);
		}
		//7    6 00005C00 CPDuplicateHash 
		//BOOL WINAPI CryptDuplicateHash(HCRYPTHASH hHash, DWORD* pdwReserved, DWORD dwFlags, HCRYPTHASH* phHash);
		[DllImport(coredll, EntryPoint="CryptDuplicateHash", SetLastError=true)] 
		private static extern bool CryptDuplicateHashCe(IntPtr hHash, ref uint pdwReserved, uint dwFlags, out IntPtr phHash);
		[DllImport(advapi32, EntryPoint="CryptDuplicateHash", SetLastError=true)] 
		private static extern bool CryptDuplicateHashXp(IntPtr hHash, ref uint pdwReserved, uint dwFlags, out IntPtr phHash);
		
		///<summary>
		///This function makes an exact copy of a key and the state the key is in. 
		///Some keys have an associated state, for example, an initialization vector and/or 
		///a salt value.
		///</summary>
		/// <remarks>
		/// did not work on smartPhone, dont need it
		/// </remarks>
		public static bool CryptDuplicateKey(IntPtr hKey, ref uint pdwReserved, uint dwFlags, out IntPtr phKey)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptDuplicateKeyCe(hKey, ref pdwReserved, dwFlags, out phKey);
			else
				return CryptDuplicateKeyXp(hKey, ref pdwReserved, dwFlags, out phKey);
		}
		//8    7 000091C8 CPDuplicateKey 
		//BOOL WINAPI CryptDuplicateKey(HCRYPTKEY hKey, DWORD* pdwReserved, DWORD dwFlags, HCRYPTKEY* phKey);
		[DllImport(coredll, EntryPoint="CryptDuplicateKey", SetLastError=true)] 
		private static extern bool CryptDuplicateKeyCe(IntPtr hKey, ref uint pdwReserved, uint dwFlags, out IntPtr phKey);
		[DllImport(advapi32, EntryPoint="CryptDuplicateKey", SetLastError=true)] 
		private static extern bool CryptDuplicateKeyXp(IntPtr hKey, ref uint pdwReserved, uint dwFlags, out IntPtr phKey);
		
		///<summary>
		///This function encrypts data. The key held by the cryptographic service provider 
		///(CSP) module and referenced by the hKey parameter specifies the algorithm used to 
		///encrypt the data parameter.
		///</summary>
		/// <remarks>
		/// raCrypto, mca / works on smartPhone
		/// </remarks>
		public static bool CryptEncrypt(IntPtr hKey, IntPtr hHash, bool Final, uint dwFlags, byte[] pbData, ref uint pdwDataLen, uint dwBufLen)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptEncryptCe(hKey, hHash, Final, dwFlags, pbData, ref pdwDataLen, dwBufLen);
			else
				return CryptEncryptXp(hKey, hHash, Final, dwFlags, pbData, ref pdwDataLen, dwBufLen);
		}
		//9    8 00004838 CPEncrypt 
		//BOOL CRYPTFUNC CryptEncrypt(HCRYPTKEY hKey, HCRYPTHASH hHash, BOOL Final, DWORD dwFlags, BYTE *pbData, DWORD *pdwDataLen, DWORD dwBufLen);
		[DllImport(coredll, EntryPoint="CryptEncrypt", SetLastError=true)]
		private static extern bool CryptEncryptCe(IntPtr hKey, IntPtr hHash, bool Final, uint dwFlags, byte[] pbData, ref uint pdwDataLen, uint dwBufLen);
		[DllImport(advapi32, EntryPoint="CryptEncrypt", SetLastError=true)]
		private static extern bool CryptEncryptXp(IntPtr hKey, IntPtr hHash, bool Final, uint dwFlags, byte[] pbData, ref uint pdwDataLen, uint dwBufLen);
		
		///<summary>
		///This function retrieves the first or next available cryptographic service provider 
		///(CSP). Used in a loop, this function can retrieve in sequence all of the CSPs 
		///available on a computer.
		///</summary>
		/// <remarks>
		/// works on smartPhone
		/// </remarks>
		public static bool CryptEnumProviders(uint dwIndex, ref uint pdwReserved, uint dwFlags, ref uint pdwProvType, StringBuilder pszProvName, ref uint pcbProvName)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptEnumProvidersCe(dwIndex, ref pdwReserved, dwFlags, ref pdwProvType, pszProvName, ref pcbProvName);
			else
				return CryptEnumProvidersXp(dwIndex, ref pdwReserved, dwFlags, ref pdwProvType, pszProvName, ref pcbProvName);
		}
		//BOOL WINAPI CryptEnumProviders(DWORD dwIndex, DWORD* pdwReserved, DWORD dwFlags, DWORD* pdwProvType, LPTSTR pszProvName, DWORD* pcbProvName);
		[DllImport(coredll, EntryPoint="CryptEnumProviders", SetLastError=true)]
		private static extern bool CryptEnumProvidersCe(uint dwIndex, ref uint pdwReserved, uint dwFlags, ref uint pdwProvType, StringBuilder pszProvName, ref uint pcbProvName);
		[DllImport(advapi32, EntryPoint="CryptEnumProviders", SetLastError=true)]
		private static extern bool CryptEnumProvidersXp(uint dwIndex, ref uint pdwReserved, uint dwFlags, ref uint pdwProvType, StringBuilder pszProvName, ref uint pcbProvName);
		
		///<summary>
		///This function retrieves the first or next type of cryptographic service provider 
		///(CSP) supported on the computer. Used in a loop, this function retrieves in 
		///sequence all of the CSP types available on a computer.
		///</summary>
		/// <remarks>
		/// did not work on smartPhone
		/// used return values from CryptEnumProviders instead
		/// </remarks>
		public static bool CryptEnumProviderTypes(uint dwIndex, ref uint pdwReserved, uint dwFlags, ref uint pdwProvType, StringBuilder pszTypeName, ref uint pcbTypeName)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptEnumProviderTypesCe(dwIndex, ref pdwReserved, dwFlags, ref pdwProvType, pszTypeName, ref pcbTypeName);
			else
				return CryptEnumProviderTypesXp(dwIndex, ref pdwReserved, dwFlags, ref pdwProvType, pszTypeName, ref pcbTypeName);
		}
		//BOOL WINAPI CryptEnumProviderTypes(DWORD dwIndex, DWORD* pdwReserved, DWORD dwFlags, DWORD* pdwProvType, LPTSTR pszTypeName, DWORD* pcbTypeName);
		[DllImport(coredll, EntryPoint="CryptEnumProviderTypes", SetLastError=true)]
		private static extern bool CryptEnumProviderTypesCe(uint dwIndex, ref uint pdwReserved, uint dwFlags, ref uint pdwProvType, StringBuilder pszTypeName, ref uint pcbTypeName);
		[DllImport(advapi32, EntryPoint="CryptEnumProviderTypes", SetLastError=true)]
		private static extern bool CryptEnumProviderTypesXp(uint dwIndex, ref uint pdwReserved, uint dwFlags, ref uint pdwProvType, StringBuilder pszTypeName, ref uint pcbTypeName);

		///<summary>
		///This function exports cryptographic keys from of a cryptographic service provider 
		///(CSP) in a secure manner.
		///The caller passes to the CryptImportKey function a handle to the key to be exported 
		///and gets a key binary large object (BLOB). This key BLOB can be sent over a 
		///nonsecure transport or stored in a nonsecure storage location. The key BLOB is 
		///useless until the intended recipient uses the CryptImportKey function, which 
		///imports the key into the recipient's CSP.
		///</summary>
		/// <remarks>
		/// mca / works on smartPhone
		/// </remarks>
		public static bool CryptExportKey(IntPtr hKey, IntPtr hExpKey, uint dwBlobType, uint dwFlags, byte[] pbData, ref uint pdwDataLen)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptExportKeyCe(hKey, hExpKey, dwBlobType, dwFlags, pbData, ref pdwDataLen);
			else
				return CryptExportKeyXp(hKey, hExpKey, dwBlobType, dwFlags, pbData, ref pdwDataLen);
		}
		//10    9 0000692C CPExportKey 
		//BOOL WINAPI CryptExportKey(HCRYPTKEY hKey, HCRYPTKEY hExpKey, DWORD dwBlobType, DWORD dwFlags, BYTE* pbData, DWORD* pdwDataLen);
		[DllImport(coredll, EntryPoint="CryptExportKey", SetLastError=true)]
		private static extern bool CryptExportKeyCe(IntPtr hKey, IntPtr hExpKey, uint dwBlobType, uint dwFlags, byte[] pbData, ref uint pdwDataLen);
		[DllImport(advapi32, EntryPoint="CryptExportKey", SetLastError=true)]
		private static extern bool CryptExportKeyXp(IntPtr hKey, IntPtr hExpKey, uint dwBlobType, uint dwFlags, byte[] pbData, ref uint pdwDataLen);
		
		///<summary>
		///This function generates a random cryptographic session key or a public/private 
		///key pair for use with the cryptographic service provider (CSP) module. 
		///The function returns a handle to the key in the phKey parameter. 
		///This handle can then be used as needed with any of the other CryptoAPI functions 
		///requiring a key handle. 
		///When calling this function, the application must specify the algorithm. 
		///Because this algorithm type is kept bundled with the key, the application does not 
		///need to specify the algorithm later when the actual cryptographic operations are 
		///performed.
		///</summary>
		/// <remarks>
		/// mca / works on smartPhone
		/// </remarks>
		public static bool CryptGenKey(IntPtr hProv, uint Algid, uint dwFlags, out IntPtr phKey)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptGenKeyCe(hProv, Algid, dwFlags, out phKey);
			else
				return CryptGenKeyXp(hProv, Algid, dwFlags, out phKey);
		}
		//11    A 000062BC CPGenKey 
		//BOOL WINAPI CryptGenKey(HCRYPTPROV hProv, ALG_ID Algid, DWORD dwFlags, HCRYPTKEY* phKey);
		[DllImport(coredll, EntryPoint="CryptGenKey", SetLastError=true)]
		private static extern bool CryptGenKeyCe(IntPtr hProv, uint Algid, uint dwFlags, out IntPtr phKey);
		[DllImport(advapi32, EntryPoint="CryptGenKey", SetLastError=true)]
		private static extern bool CryptGenKeyXp(IntPtr hProv, uint Algid, uint dwFlags, out IntPtr phKey);
		
		///<summary>
		///This function fills a buffer with random bytes.
		///</summary>
		/// <remarks>
		/// raPocketGuid / worked on smartPhone
		/// </remarks>
		public static bool CryptGenRandom(IntPtr hProv, int dwLen, byte[] pbBuffer)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptGenRandomCe(hProv, dwLen, pbBuffer);
			else
				return CryptGenRandomXp(hProv, dwLen, pbBuffer);
		}
		//12    B 000092A8 CPGenRandom 
		//BOOL CRYPTFUNC CrptGenRandom(HCRYPTPROV hProv, DWORD dwLen, BYTE* pbBuffer);
		[DllImport(coredll, EntryPoint="CryptGenRandom", SetLastError=true)]
		private static extern bool CryptGenRandomCe(IntPtr hProv, int dwLen, byte[] pbBuffer);
		[DllImport(advapi32, EntryPoint="CryptGenRandom", SetLastError=true)]
		private static extern bool CryptGenRandomXp(IntPtr hProv, int dwLen, byte[] pbBuffer);
		
		///<summary>
		///This function finds the default cryptographic service provider (CSP) of a specified 
		///type either for the current user or the device. The name of the default CSP for 
		///the type specified in the dwProvType parameter is returned in the pszProvName buffer.
		///</summary>
		/// <remarks>
		/// this did not work on smartPhone
		/// can just use CryptAcquireContext and pass in alot of nulls
		/// </remarks>
		public static bool CryptGetDefaultProvider(uint dwProvType, ref uint pdwReserved, uint dwFlags, StringBuilder pszProvName, ref uint pcbProvName)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptGetDefaultProviderCe(dwProvType, ref pdwReserved, dwFlags, pszProvName, ref pcbProvName);
			else
				return CryptGetDefaultProviderXp(dwProvType, ref pdwReserved, dwFlags, pszProvName, ref pcbProvName);
		}
		//BOOL WINAPI CryptGetDefaultProvider(DWORD dwProvType, DWORD* pdwReserved, DWORD dwFlags, LPTSTR pszProvName, DWORD* pcbProvName);
		[DllImport(coredll, EntryPoint="CryptGetDefaultProvider", SetLastError=true)]
		private static extern bool CryptGetDefaultProviderCe(uint dwProvType, ref uint pdwReserved, uint dwFlags, StringBuilder pszProvName, ref uint pcbProvName);
		[DllImport(advapi32, EntryPoint="CryptGetDefaultProvider", SetLastError=true)]
		private static extern bool CryptGetDefaultProviderXp(uint dwProvType, ref uint pdwReserved, uint dwFlags, StringBuilder pszProvName, ref uint pcbProvName);

		///<summary>
		///This function retrieves data that governs the operations of a hash object and 
		///retrieves the actual hash value.
		///</summary>
		/// <remarks>
		/// mca / worked on smartPhone
		/// </remarks>
		public static bool CryptGetHashParam(IntPtr hHash, uint dwParam, byte[] pbData, ref uint pdwDataLen, uint dwFlags)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptGetHashParamCe(hHash, dwParam, pbData, ref pdwDataLen, dwFlags);
			else
				return CryptGetHashParamXp(hHash, dwParam, pbData, ref pdwDataLen, dwFlags);
		}
		//13    C 00008B90 CPGetHashParam 
		//BOOL WINAPI CryptGetHashParam(HCRYPTHASH hHash, DWORD dwParam, BYTE* pbData, DWORD* pdwDataLen, DWORD dwFlags);
		[DllImport(coredll, EntryPoint="CryptGetHashParam", SetLastError=true)] 			
		private static extern bool CryptGetHashParamCe(IntPtr hHash, uint dwParam, byte[] pbData, ref uint pdwDataLen, uint dwFlags); 
		[DllImport(advapi32, EntryPoint="CryptGetHashParam", SetLastError=true)] 			
		private static extern bool CryptGetHashParamXp(IntPtr hHash, uint dwParam, byte[] pbData, ref uint pdwDataLen, uint dwFlags); 
		
		///<summary>
		///This function lets applications retrieve data that governs the operations of a key. 
		///In the Microsoft cryptographic service providers (CSPs), the base symmetric keying 
		///material is not obtainable by this or any other function.
		///</summary>
		/// <remarks>
		/// works on smartPhone
		/// </remarks>
		public static bool CryptGetKeyParam(IntPtr hKey, uint dwParam, byte[] pbData, ref uint pdwDataLen, uint dwFlags)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptGetKeyParamCe(hKey, dwParam, pbData, ref pdwDataLen, dwFlags);
			else
				return CryptGetKeyParamXp(hKey, dwParam, pbData, ref pdwDataLen, dwFlags);
		}
		//14    D 00007C2C CPGetKeyParam 
		//BOOL CRYPTFUNC CryptGetKeyParam(HCRYPTKEY hKey, DWORD dwParam, BYTE* pbData, DWORD* pdwDataLen, DWORD dwFlags);
		[DllImport(coredll, EntryPoint="CryptGetKeyParam", SetLastError=true)] 			
		private static extern bool CryptGetKeyParamCe(IntPtr hKey, uint dwParam, byte[] pbData, ref uint pdwDataLen, uint dwFlags);
		[DllImport(advapi32, EntryPoint="CryptGetKeyParam", SetLastError=true)] 			
		private static extern bool CryptGetKeyParamXp(IntPtr hKey, uint dwParam, byte[] pbData, ref uint pdwDataLen, uint dwFlags);
		
		///<summary>
		///This function retrieves parameters that govern the operations of a cryptographic 
		///service provider (CSP).
		///</summary>
		/// <remarks>
		/// works on the smartPhone
		/// </remarks>
		public static bool CryptGetProvParam(IntPtr hProv, uint dwParam, byte[] pbData, ref uint pdwDataLen, uint dwFlags)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptGetProvParamCe(hProv, dwParam, pbData, ref pdwDataLen, dwFlags);
			else
				return CryptGetProvParamXp(hProv, dwParam, pbData, ref pdwDataLen, dwFlags);
		}
		//15    E 00008130 CPGetProvParam 
		//BOOL WINAPI CryptGetProvParam(HCRYPTPROV hProv, DWORD dwParam, BYTE* pbData, DWORD* pdwDataLen, DWORD dwFlags);
		[DllImport(coredll, EntryPoint="CryptGetProvParam", SetLastError=true)] 			
		private static extern bool CryptGetProvParamCe(IntPtr hProv, uint dwParam, byte[] pbData, ref uint pdwDataLen, uint dwFlags);
		[DllImport(advapi32, EntryPoint="CryptGetProvParam", SetLastError=true)] 			
		private static extern bool CryptGetProvParamXp(IntPtr hProv, uint dwParam, byte[] pbData, ref uint pdwDataLen, uint dwFlags);
		
		///<summary>
		///This function retrieves a handle to a permanent user key pair, such as the user's 
		///signature key pair. This function also retrieves a handle to one of a user's two 
		///public/private key pairs. Only the owner of the public/private key pairs uses the 
		///function and only when the handle to a cryptographic service provider (CSP) and 
		///its associated key container is available. Use the CryptAcquireCertificatePrivateKey 
		///function if the user's certificate is available, but not the CSP handle.
		///</summary>
		/// <remarks>
		/// mca / works on smartPhone
		/// </remarks>
		public static bool CryptGetUserKey(IntPtr hProv, uint dwKeySpec, out IntPtr phUserKey)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptGetUserKeyCe(hProv, dwKeySpec, out phUserKey);
			else
				return CryptGetUserKeyXp(hProv, dwKeySpec, out phUserKey);
		}
		//16    F 000077D4 CPGetUserKey 
		//BOOL WINAPI CryptGetUserKey(HCRYPTPROV hProv, DWORD dwKeySpec, HCRYPTKEY* phUserKey);
		[DllImport(coredll, EntryPoint="CryptGetUserKey", SetLastError=true)]
		private static extern bool CryptGetUserKeyCe(IntPtr hProv, uint dwKeySpec, out IntPtr phUserKey);
		[DllImport(advapi32, EntryPoint="CryptGetUserKey", SetLastError=true)]
		private static extern bool CryptGetUserKeyXp(IntPtr hProv, uint dwKeySpec, out IntPtr phUserKey);
		
		///<summary>
		///This function adds data to a specified hash object. This function and the 
		///CryptHashSessionKey function can be called multiple times to compute the hash on 
		///long streams or on discontinuous streams.
		///Before calling this function, the CryptCreateHash function must be called to create 
		///a handle to a hash object.
		///</summary>
		/// <remarks>
		/// raCrypto, mca / worked on smartPhone
		/// </remarks>
		public static bool CryptHashData(IntPtr hHash, byte[] pbData, int dwDataLen, uint dwFlags)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptHashDataCe(hHash, pbData, dwDataLen, dwFlags); 
			else
				return CryptHashDataXp(hHash, pbData, dwDataLen, dwFlags); 
		}
		//17   10 000052DC CPHashData 
		//BOOL WINAPI CryptHashData(HCRYPTHASH hHash, BYTE* pbData, DWORD dwDataLen, DWORD dwFlags);
		[DllImport(coredll, EntryPoint="CryptHashData", SetLastError=true)] 
		private static extern bool CryptHashDataCe(IntPtr hHash, byte[] pbData, int dwDataLen, uint dwFlags); 
		[DllImport(advapi32, EntryPoint="CryptHashData", SetLastError=true)] 
		private static extern bool CryptHashDataXp(IntPtr hHash, byte[] pbData, int dwDataLen, uint dwFlags); 
		
		///<summary>
		///This function computes the cryptographic hash of a session key object. This 
		///function can be called multiple times with the same hash handle to compute the hash 
		///of multiple keys. Calls to the CryptHashSessionKey function can be interspersed 
		///with calls to the CryptHashData function.
		///Before calling this function, the CryptCreateHash function must be called to get a 
		///handle to a hash object. 
		///</summary>
		/// <remarks>
		/// works on smartPhone
		/// </remarks>
		public static bool CryptHashSessionKey(IntPtr hHash, IntPtr hKey, uint dwFlags)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptHashSessionKeyCe(hHash, hKey, dwFlags);
			else
				return CryptHashSessionKeyXp(hHash, hKey, dwFlags);
		}
		//18   11 0000577C CPHashSessionKey 
		//BOOL WINAPI CryptHashSessionKey(HCRYPTHASH hHash, HCRYPTKEY hKey, DWORD dwFlags);
		[DllImport(coredll, EntryPoint="CryptHashSessionKey", SetLastError=true)] 
		private static extern bool CryptHashSessionKeyCe(IntPtr hHash, IntPtr hKey, uint dwFlags);
		[DllImport(advapi32, EntryPoint="CryptHashSessionKey", SetLastError=true)] 
		private static extern bool CryptHashSessionKeyXp(IntPtr hHash, IntPtr hKey, uint dwFlags);
		
		///<summary>
		///This function transfers a cryptographic key from a key binary large object (BLOB) 
		///to the cryptographic service provider (CSP). This function can be used to import 
		///an Schannel session key, regular session key, public key, or public/private key 
		///pair. For all but the public key, the key or key pair is encrypted. 
		///</summary>
		/// <remarks>
		/// mca / works on smartPhone
		/// </remarks>
		public static bool CryptImportKey(IntPtr hProv, byte[] pbData, uint dwDataLen, IntPtr hPubKey, uint dwFlags, out IntPtr phKey)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptImportKeyCe(hProv, pbData, dwDataLen, hPubKey, dwFlags, out phKey);
			else
				return CryptImportKeyXp(hProv, pbData, dwDataLen, hPubKey, dwFlags, out phKey);
		}
		//19   12 00006DDC CPImportKey 
		//BOOL WINAPI CryptImportKey(HCRYPTPROV hProv, BYTE* pbData, DWORD dwDataLenHCRYPTKEY hPubKey, DWORD dwFlags, HCRYPTKEY* phKey);
		[DllImport(coredll, EntryPoint="CryptImportKey", SetLastError=true)]
		private static extern bool CryptImportKeyCe(IntPtr hProv, byte[] pbData, uint dwDataLen, IntPtr hPubKey, uint dwFlags, out IntPtr phKey);
		[DllImport(advapi32, EntryPoint="CryptImportKey", SetLastError=true)]
		private static extern bool CryptImportKeyXp(IntPtr hProv, byte[] pbData, uint dwDataLen, IntPtr hPubKey, uint dwFlags, out IntPtr phKey);
		
		///<summary>
		///This function performs encryption on the data in a DATA_BLOB structure. Typically, 
		///only a user with the same logon credentials as the encrypter can decrypt the data. 
		///In addition, the encryption and decryption usually must be done on the same 
		///computer. For information about exceptions, see the Remarks section. 
		///Note   An untrusted application can call the CryptProtectData function. The call 
		///will fail only if CRYPTPROTECT_SYSTEM is specified for the dwFlags parameter.
		///</summary>
		/// <remarks>
		/// works on smartPhone
		/// </remarks>
		public static bool CryptProtectData(ref CRYPTOAPI_BLOB pDataIn, StringBuilder szDataDescr, IntPtr pOptionalEntropy, IntPtr pvReserved, IntPtr pPromptStruct, uint dwFlags, ref CRYPTOAPI_BLOB pDataOut)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptProtectDataCe(ref pDataIn, szDataDescr, pOptionalEntropy, pvReserved, pPromptStruct, dwFlags, ref pDataOut);
			else
				return CryptProtectDataXp(ref pDataIn, szDataDescr, pOptionalEntropy, pvReserved, pPromptStruct, dwFlags, ref pDataOut);
		}
		//BOOL WINAPI CryptProtectData(DATA_BLOB* pDataIn, LPCWSTR szDataDescr, DATA_BLOB* pOptionalEntropy, PVOID pvReserved, CRYPTPROTECT_PROMPTSTRUCT* pPromptStruct, DWORD dwFlags, DATA_BLOB* pDataOut);
		[DllImport(coredll, EntryPoint="CryptProtectData", SetLastError=true)]
		private static extern bool CryptProtectDataCe(ref CRYPTOAPI_BLOB pDataIn, StringBuilder szDataDescr, IntPtr pOptionalEntropy, IntPtr pvReserved, IntPtr pPromptStruct, uint dwFlags, ref CRYPTOAPI_BLOB pDataOut);
		[DllImport(crypt32, EntryPoint="CryptProtectData", SetLastError=true)]
		private static extern bool CryptProtectDataXp(ref CRYPTOAPI_BLOB pDataIn, StringBuilder szDataDescr, IntPtr pOptionalEntropy, IntPtr pvReserved, IntPtr pPromptStruct, uint dwFlags, ref CRYPTOAPI_BLOB pDataOut);
 
		///<summary>
		///This function releases the handle to a cryptographic service provider (CSP) and 
		///the key container. At each call to this function, the reference count on the CSP 
		///is reduced by one. When the reference count reaches zero, the context is fully 
		///released and it can no longer be used by any function in the application. 
		///The application calls this function when it is finished using the CSP. After this 
		///function is called, the CSP handle specified by the hProv parameter is no longer 
		///valid; however, the function does not destroy either the key container or any key 
		///pairs.
		///</summary>
		/// <remarks>
		/// raCrypto / works on smartPhone
		/// </remarks>
		public static bool CryptReleaseContext(IntPtr hProv, uint dwFlags)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptReleaseContextCe(hProv, dwFlags);
			else
				return CryptReleaseContextXp(hProv, dwFlags);
		}
		//20   13 0000AC30 CPReleaseContext 
		//BOOL WINAPI CryptReleaseContext(HCRYPTPROV hProv, DWORD dwFlags); 
		[DllImport(coredll, EntryPoint="CryptReleaseContext", SetLastError=true)] 
		private static extern bool CryptReleaseContextCe(IntPtr hProv, uint dwFlags); 
		[DllImport(advapi32, EntryPoint="CryptReleaseContext", SetLastError=true)] 
		private static extern bool CryptReleaseContextXp(IntPtr hProv, uint dwFlags); 
		
		///<summary>
		///This function customizes the operations of a hash object. Currently, only a single 
		///parameter is defined for this function. 
		///</summary>
		/// <remarks>
		/// works on smartPhone
		/// </remarks>
		public static bool CryptSetHashParam(IntPtr hHash, uint dwParam, byte[] pbData, uint dwFlags)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptSetHashParamCe(hHash, dwParam, pbData, dwFlags);
			else
				return CryptSetHashParamXp(hHash, dwParam, pbData, dwFlags);
		}
		//21   14 000086E0 CPSetHashParam 
		//BOOL WINAPI CryptSetHashParam(HCRYPTHASH hHash, DWORD dwParam, BYTE* pbData, DWORD dwFlags);
		[DllImport(coredll, EntryPoint="CryptSetHashParam", SetLastError=true)] 
		private static extern bool CryptSetHashParamCe(IntPtr hHash, uint dwParam, byte[] pbData, uint dwFlags);
		[DllImport(advapi32, EntryPoint="CryptSetHashParam", SetLastError=true)] 
		private static extern bool CryptSetHashParamXp(IntPtr hHash, uint dwParam, byte[] pbData, uint dwFlags);
		
		///<summary>
		///This function customizes various aspects of a key's operations. The values set by 
		///this function are not persisted to memory and are used only within a single session. 
		///The Microsoft cryptographic service providers (CSPs) do not allow setting the 
		///values for key exchange or signature keys; however, custom providers may define 
		///parameters that can be set on these keys.
		///</summary>
		/// <remarks>
		/// worked on smartPhone
		/// </remarks>
		public static bool CryptSetKeyParam(IntPtr hKey, uint dwParam, byte[] pbData, uint dwFlags)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptSetKeyParamCe(hKey, dwParam, pbData, dwFlags);
			else
				return CryptSetKeyParamXp(hKey, dwParam, pbData, dwFlags);
		}
		//22   15 000078B0 CPSetKeyParam 
		//BOOL WINAPI CryptSetKeyParam(HCRYPTKEY hKey, DWORD dwParam, BYTE* pbData, DWORD dwFlags);
		[DllImport(coredll, EntryPoint="CryptSetKeyParam", SetLastError=true)] 
		private static extern bool CryptSetKeyParamCe(IntPtr hKey, uint dwParam, byte[] pbData, uint dwFlags);
		[DllImport(advapi32, EntryPoint="CryptSetKeyParam", SetLastError=true)] 
		private static extern bool CryptSetKeyParamXp(IntPtr hKey, uint dwParam, byte[] pbData, uint dwFlags);
		
		///<summary>
		///This function specifies the current user default cryptographic service provider 
		///(CSP). Typical applications do not use this function. It is intended for use solely 
		///by administrative applications. 
		///If a current user's default provider is set, that default provider is acquired by 
		///any call by that user to the CryptAcquireContext function specifying a dwProvType 
		///provider type but not a CSP name. 
		///</summary>
		/// <remarks>
		/// worked on SmartPhone
		/// </remarks>
		public static bool CryptSetProvider(string pszProvName, uint dwProvType)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptSetProviderCe(pszProvName, dwProvType);
			else
				return CryptSetProviderXp(pszProvName, dwProvType);
		}
		//BOOL CRYPTFUNC CryptSetProvider(LPCTSTR pszProvName, DWORD dwProvType);
		[DllImport(coredll, EntryPoint="CryptSetProvider", SetLastError=true)] 
		private static extern bool CryptSetProviderCe(string pszProvName, uint dwProvType);
		[DllImport(advapi32, EntryPoint="CryptSetProvider", SetLastError=true)] 
		private static extern bool CryptSetProviderXp(string pszProvName, uint dwProvType);

		///<summary>
		///This function specifies the default cryptographic service provider (CSP) for the 
		///current user or the local device.
		///If a current user's default provider is set, that default provider is acquired by 
		///any call by that user to the CryptAcquireContext function specifying a dwProvType 
		///provider type but not a CSP name.
		///If a local computer default is set, calls to the CryptAcquireContext function by a 
		///user not having a current user default set and not specifying a CSP result in the 
		///use of the local computer's default CSP.
		///Typical applications do not use this function. It is intended for use solely by 
		///administrative applications. 
		///</summary>
		/// <remarks>
		/// this did not work on smartPhone, dont need it
		/// </remarks>
		public static bool CryptSetProviderEx(string pszProvName, uint dwProvType, ref uint pdwReserved, uint dwFlags)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptSetProviderExCe(pszProvName, dwProvType, ref pdwReserved, dwFlags);
			else
				return CryptSetProviderExXp(pszProvName, dwProvType, ref pdwReserved, dwFlags);
		}
		//BOOL WINAPI CryptSetProviderEx(LPCTSTR pszProvName, DWORD dwProvType, DWORD* pdwReserved, DWORD dwFlags);
		[DllImport(coredll, EntryPoint="CryptSetProviderEx", SetLastError=true)] 
		private static extern bool CryptSetProviderExCe(string pszProvName, uint dwProvType, ref uint pdwReserved, uint dwFlags);
		[DllImport(advapi32, EntryPoint="CryptSetProviderEx", SetLastError=true)] 
		private static extern bool CryptSetProviderExXp(string pszProvName, uint dwProvType, ref uint pdwReserved, uint dwFlags);

		///<summary>
		///This function customizes the operations of a cryptographic service provider (CSP). 
		///This function is commonly used to set a security descriptor on the key container 
		///associated with a CSP to control access to the private keys in that key container.
		///</summary>
		/// <remarks>
		/// works on smartPhone
		/// </remarks>
		public static bool CryptSetProvParam(IntPtr hProv, uint dwParam, byte[] pbData, uint dwFlags)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptSetProvParamCe(hProv, dwParam, pbData, dwFlags);
			else
				return CryptSetProvParamXp(hProv, dwParam, pbData, dwFlags);
		}
		//23   16 00008078 CPSetProvParam 
		//BOOL CRYPTFUNC CryptoSetProvParam(HCRYPTPROV hProv, DWORD dwParam, BYTE* pbData, DWORD dwFlags);
		[DllImport(coredll, EntryPoint="CryptSetProvParam", SetLastError=true)] 
		private static extern bool CryptSetProvParamCe(IntPtr hProv, uint dwParam, byte[] pbData, uint dwFlags);
		[DllImport(advapi32, EntryPoint="CryptSetProvParam", SetLastError=true)] 
		private static extern bool CryptSetProvParamXp(IntPtr hProv, uint dwParam, byte[] pbData, uint dwFlags);
		
		///<summary>
		///This function signs data. Because all signature algorithms are asymmetric and 
		///therefore slow, the CryptoAPI does not let data be signed directly. Instead, you 
		///must first hash the data and then use the CryptSignHash function to sign the hash 
		///value.
		///</summary>
		/// <remarks>
		/// works on smartPhone
		/// </remarks>
		public static bool CryptSignHash(IntPtr hHash, uint dwKeySpec, string sDescription, uint dwFlags, byte[] pbSignature, ref uint pdwSigLen)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptSignHashCe(hHash, dwKeySpec, sDescription, dwFlags, pbSignature, ref pdwSigLen);
			else
				return CryptSignHashXp(hHash, dwKeySpec, sDescription, dwFlags, pbSignature, ref pdwSigLen);
		}
		//24   17 00009304 CPSignHash 
		//BOOL WINAPI CryptSignHash(HCRYPTHASH hHash, DWORD dwKeySpec, LPCTSTR sDescription, DWORD dwFlags, BYTE* pbSignature, DWORD* pdwSigLen);
		[DllImport(coredll, EntryPoint="CryptSignHash", SetLastError=true)] 
		private static extern bool CryptSignHashCe(IntPtr hHash, uint dwKeySpec, string sDescription, uint dwFlags, byte[] pbSignature, ref uint pdwSigLen);
		[DllImport(advapi32, EntryPoint="CryptSignHash", SetLastError=true)] 
		private static extern bool CryptSignHashXp(IntPtr hHash, uint dwKeySpec, string sDescription, uint dwFlags, byte[] pbSignature, ref uint pdwSigLen);
		
		///<summary>
		///This function decrypts and checks the integrity of the data in a DATA_BLOB 
		///structure. Usually, only a user with the same logon credentials as the encrypter 
		///can decrypt the data. In addition, the encryption and decryption must be done on 
		///the same computer. 
		///Note   An untrusted application can call the CryptUnprotectData function. The call 
		///will fail only if CRYPTPROTECT_SYSTEM is specified for the dwFlags parameter.
		///</summary>
		/// <remarks>
		/// works on smartPhone
		/// </remarks>
		public static bool CryptUnprotectData(ref CRYPTOAPI_BLOB pDataIn, ref IntPtr ppszDataDescr, IntPtr pOptionalEntropy, IntPtr pvReserved, IntPtr pPromptStruct, uint dwFlags, ref CRYPTOAPI_BLOB pDataOut)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptUnprotectDataCe(ref pDataIn, ref ppszDataDescr, pOptionalEntropy, pvReserved, pPromptStruct, dwFlags, ref pDataOut);
			else
				return CryptUnprotectDataXp(ref pDataIn, ref ppszDataDescr, pOptionalEntropy, pvReserved, pPromptStruct, dwFlags, ref pDataOut);
		}
		//BOOL WINAPI CryptUnprotectData(DATA_BLOB* pDataIn, LPWSTR* ppszDataDescr, DATA_BLOB* pOptionalEntropy, PVOID pvReserved, CRYPTPROTECT_PROMPTSTRUCT* pPromptStruct, DWORD dwFlags, DATA_BLOB* pDataOut);
		[DllImport(coredll, EntryPoint="CryptUnprotectData", SetLastError=true)] 
		private static extern bool CryptUnprotectDataCe(ref CRYPTOAPI_BLOB pDataIn, ref IntPtr ppszDataDescr, IntPtr pOptionalEntropy, IntPtr pvReserved, IntPtr pPromptStruct, uint dwFlags, ref CRYPTOAPI_BLOB pDataOut); 
		[DllImport(crypt32, EntryPoint="CryptUnprotectData", SetLastError=true)] 
		private static extern bool CryptUnprotectDataXp(ref CRYPTOAPI_BLOB pDataIn, ref IntPtr ppszDataDescr, IntPtr pOptionalEntropy, IntPtr pvReserved, IntPtr pPromptStruct, uint dwFlags, ref CRYPTOAPI_BLOB pDataOut);

		///<summary>
		///This function verifies the signature of a hash object.
		///Before calling this function, the CryptCreateHash function must be called to get a 
		///handle to a hash object. The CryptHashData function or CryptHashSessionKey function 
		///is then used to add the data or session keys to the hash object.
		///After the call to the CryptVerifySignature function has been completed, only the 
		///CryptDestroyHash function can be called using the hHash handle.
		///</summary>
		/// <remarks>
		/// works on smartPhone
		/// </remarks>
		public static bool CryptVerifySignature(IntPtr hHash, byte[] pbSignature, uint dwSigLen, IntPtr hPubKey, string sDescription, uint dwFlags)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return CryptVerifySignatureCe(hHash, pbSignature, dwSigLen, hPubKey, sDescription, dwFlags);
			else
				return CryptVerifySignatureXp(hHash, pbSignature, dwSigLen, hPubKey, sDescription, dwFlags);
		}
		//25   18 000097B0 CPVerifySignature 
		//BOOL WINAPI CryptVerifySignature(HCRYPTHASH hHash, BYTE* pbSignature, DWORD dwSigLen, HCRYPTKEY hPubKey, LPCTSTR sDescription, DWORD dwFlags);
		[DllImport(coredll, EntryPoint="CryptVerifySignature", SetLastError=true)] 
		private static extern bool CryptVerifySignatureCe(IntPtr hHash, byte[] pbSignature, uint dwSigLen, IntPtr hPubKey, string sDescription, uint dwFlags);
		[DllImport(advapi32, EntryPoint="CryptVerifySignature", SetLastError=true)] 
		private static extern bool CryptVerifySignatureXp(IntPtr hHash, byte[] pbSignature, uint dwSigLen, IntPtr hPubKey, string sDescription, uint dwFlags);
	}
}
