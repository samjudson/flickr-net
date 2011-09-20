//==========================================================================================
//
//		OpenNETCF.Windows.Forms.Const
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
    internal class Const
	{
		// Algorithm classes
		public const uint ALG_CLASS_ANY = (0);
		public const uint ALG_CLASS_SIGNATURE = (1 << 13);
		public const uint ALG_CLASS_MSG_ENCRYPT = (2 << 13);
		public const uint ALG_CLASS_DATA_ENCRYPT = (3 << 13);
		public const uint ALG_CLASS_HASH = (4 << 13);
		public const uint ALG_CLASS_KEY_EXCHANGE = (5 << 13);
		public const uint ALG_CLASS_ALL = (7 << 13);

		// Algorithm types
		public const uint ALG_TYPE_ANY = (0);
		public const uint ALG_TYPE_DSS = (1 << 9);
		public const uint ALG_TYPE_RSA = (2 << 9);
		public const uint ALG_TYPE_BLOCK = (3 << 9);
		public const uint ALG_TYPE_STREAM = (4 << 9);
		public const uint ALG_TYPE_DH = (5 << 9);
		public const uint ALG_TYPE_SECURECHANNEL = (6 << 9);

		// Generic sub-ids
		public const uint ALG_SID_ANY = (0);
		// Some RSA sub-ids
		public const uint ALG_SID_RSA_ANY = 0;
		public const uint ALG_SID_RSA_PKCS = 1;
		public const uint ALG_SID_RSA_MSATWORK = 2;
		public const uint ALG_SID_RSA_ENTRUST = 3;
		public const uint ALG_SID_RSA_PGP = 4;
		// Some DSS sub-ids
		public const uint ALG_SID_DSS_ANY = 0;
		public const uint ALG_SID_DSS_PKCS = 1;
		public const uint ALG_SID_DSS_DMS = 2;
		// Block cipher sub ids
		// DES sub_ids
		public const uint ALG_SID_DES = 1;
		public const uint ALG_SID_3DES = 3;
		public const uint ALG_SID_DESX = 4;
		public const uint ALG_SID_IDEA = 5;
		public const uint ALG_SID_CAST = 6;
		public const uint ALG_SID_SAFERSK64 = 7;
		public const uint ALG_SID_SAFERSK128 = 8;
		public const uint ALG_SID_3DES_112 = 9;
		public const uint ALG_SID_CYLINK_MEK = 12;
		public const uint ALG_SID_RC5 = 13;
		public const uint ALG_SID_AES_128 = 14;
		public const uint ALG_SID_AES_192 = 15;
		public const uint ALG_SID_AES_256 = 16;
		public const uint ALG_SID_AES = 17;
		// Fortezza sub-ids
		public const uint ALG_SID_SKIPJACK = 10;
		public const uint ALG_SID_TEK = 11;
		// RC2 sub-ids
		public const uint ALG_SID_RC2 = 2;
		// Stream cipher sub-ids
		public const uint ALG_SID_RC4 = 1;
		public const uint ALG_SID_SEAL = 2;
		// Diffie-Hellman sub-ids
		public const uint ALG_SID_DH_SANDF = 1;
		public const uint ALG_SID_DH_EPHEM = 2;
		public const uint ALG_SID_AGREED_KEY_ANY = 3;
		public const uint ALG_SID_KEA = 4;
		// Hash sub ids
		public const uint ALG_SID_MD2 = 1;
		public const uint ALG_SID_MD4 = 2;
		public const uint ALG_SID_MD5 = 3;
		public const uint ALG_SID_SHA = 4;
		public const uint ALG_SID_SHA1 = 4;
		public const uint ALG_SID_MAC = 5;
		public const uint ALG_SID_RIPEMD = 6;
		public const uint ALG_SID_RIPEMD160 = 7;
		public const uint ALG_SID_SSL3SHAMD5 = 8;
		public const uint ALG_SID_HMAC = 9;
		public const uint ALG_SID_TLS1PRF = 10;
		public const uint ALG_SID_HASH_REPLACE_OWF = 11;
		public const uint ALG_SID_SHA_256 = 12;
		public const uint ALG_SID_SHA_384 = 13;
		public const uint ALG_SID_SHA_512 = 14;
		// secure channel sub ids
		public const uint ALG_SID_SSL3_MASTER = 1;
		public const uint ALG_SID_SCHANNEL_MASTER_HASH = 2;
		public const uint ALG_SID_SCHANNEL_MAC_KEY = 3;
		public const uint ALG_SID_PCT1_MASTER = 4;
		public const uint ALG_SID_SSL2_MASTER = 5;
		public const uint ALG_SID_TLS1_MASTER = 6;
		public const uint ALG_SID_SCHANNEL_ENC_KEY = 7;
		// Our silly example sub-id
		public const uint ALG_SID_EXAMPLE = 80;

		// algorithm identifier definitions
		public const uint CALG_MD2 = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_MD2);
		public const uint CALG_MD4 = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_MD4);
		public const uint CALG_MD5 = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_MD5); //32771
		public const uint CALG_SHA = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_SHA);
		public const uint CALG_SHA1 = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_SHA1);
		public const uint CALG_MAC = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_MAC);
		public const uint CALG_RSA_SIGN = (ALG_CLASS_SIGNATURE | ALG_TYPE_RSA | ALG_SID_RSA_ANY);
		public const uint CALG_DSS_SIGN = (ALG_CLASS_SIGNATURE | ALG_TYPE_DSS | ALG_SID_DSS_ANY);
		public const uint CALG_NO_SIGN = (ALG_CLASS_SIGNATURE | ALG_TYPE_ANY | ALG_SID_ANY);
		public const uint CALG_RSA_KEYX = (ALG_CLASS_KEY_EXCHANGE|ALG_TYPE_RSA|ALG_SID_RSA_ANY);
		public const uint CALG_DES = (ALG_CLASS_DATA_ENCRYPT|ALG_TYPE_BLOCK|ALG_SID_DES); //26113
		public const uint CALG_3DES_112 = (ALG_CLASS_DATA_ENCRYPT|ALG_TYPE_BLOCK|ALG_SID_3DES_112);
		public const uint CALG_3DES = (ALG_CLASS_DATA_ENCRYPT|ALG_TYPE_BLOCK|ALG_SID_3DES);
		public const uint CALG_DESX = (ALG_CLASS_DATA_ENCRYPT|ALG_TYPE_BLOCK|ALG_SID_DESX);
		public const uint CALG_RC2 = (ALG_CLASS_DATA_ENCRYPT|ALG_TYPE_BLOCK|ALG_SID_RC2);
		public const uint CALG_RC4 = (ALG_CLASS_DATA_ENCRYPT|ALG_TYPE_STREAM|ALG_SID_RC4); //26625
		public const uint CALG_SEAL = (ALG_CLASS_DATA_ENCRYPT|ALG_TYPE_STREAM|ALG_SID_SEAL);
		public const uint CALG_DH_SF = (ALG_CLASS_KEY_EXCHANGE|ALG_TYPE_DH|ALG_SID_DH_SANDF);
		public const uint CALG_DH_EPHEM = (ALG_CLASS_KEY_EXCHANGE|ALG_TYPE_DH|ALG_SID_DH_EPHEM);
		public const uint CALG_AGREEDKEY_ANY = (ALG_CLASS_KEY_EXCHANGE|ALG_TYPE_DH|ALG_SID_AGREED_KEY_ANY);
		public const uint CALG_KEA_KEYX = (ALG_CLASS_KEY_EXCHANGE|ALG_TYPE_DH|ALG_SID_KEA);
		public const uint CALG_HUGHES_MD5 = (ALG_CLASS_KEY_EXCHANGE|ALG_TYPE_ANY|ALG_SID_MD5);
		public const uint CALG_SKIPJACK = (ALG_CLASS_DATA_ENCRYPT|ALG_TYPE_BLOCK|ALG_SID_SKIPJACK);
		public const uint CALG_TEK = (ALG_CLASS_DATA_ENCRYPT|ALG_TYPE_BLOCK|ALG_SID_TEK);
		public const uint CALG_CYLINK_MEK = (ALG_CLASS_DATA_ENCRYPT|ALG_TYPE_BLOCK|ALG_SID_CYLINK_MEK);
		public const uint CALG_SSL3_SHAMD5 = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_SSL3SHAMD5);
		public const uint CALG_SSL3_MASTER = (ALG_CLASS_MSG_ENCRYPT|ALG_TYPE_SECURECHANNEL|ALG_SID_SSL3_MASTER);
		public const uint CALG_SCHANNEL_MASTER_HASH = (ALG_CLASS_MSG_ENCRYPT|ALG_TYPE_SECURECHANNEL|ALG_SID_SCHANNEL_MASTER_HASH);
		public const uint CALG_SCHANNEL_MAC_KEY = (ALG_CLASS_MSG_ENCRYPT|ALG_TYPE_SECURECHANNEL|ALG_SID_SCHANNEL_MAC_KEY);
		public const uint CALG_SCHANNEL_ENC_KEY = (ALG_CLASS_MSG_ENCRYPT|ALG_TYPE_SECURECHANNEL|ALG_SID_SCHANNEL_ENC_KEY);
		public const uint CALG_PCT1_MASTER = (ALG_CLASS_MSG_ENCRYPT|ALG_TYPE_SECURECHANNEL|ALG_SID_PCT1_MASTER);
		public const uint CALG_SSL2_MASTER = (ALG_CLASS_MSG_ENCRYPT|ALG_TYPE_SECURECHANNEL|ALG_SID_SSL2_MASTER);
		public const uint CALG_TLS1_MASTER = (ALG_CLASS_MSG_ENCRYPT|ALG_TYPE_SECURECHANNEL|ALG_SID_TLS1_MASTER);
		public const uint CALG_RC5 = (ALG_CLASS_DATA_ENCRYPT|ALG_TYPE_BLOCK|ALG_SID_RC5);
		public const uint CALG_HMAC = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_HMAC);
		public const uint CALG_TLS1PRF = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_TLS1PRF);
		public const uint CALG_HASH_REPLACE_OWF = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_HASH_REPLACE_OWF);
		public const uint CALG_AES_128 = (ALG_CLASS_DATA_ENCRYPT|ALG_TYPE_BLOCK|ALG_SID_AES_128);
		public const uint CALG_AES_192 = (ALG_CLASS_DATA_ENCRYPT|ALG_TYPE_BLOCK|ALG_SID_AES_192);
		public const uint CALG_AES_256 = (ALG_CLASS_DATA_ENCRYPT|ALG_TYPE_BLOCK|ALG_SID_AES_256);
		public const uint CALG_AES = (ALG_CLASS_DATA_ENCRYPT|ALG_TYPE_BLOCK|ALG_SID_AES);
		public const uint CALG_SHA_256 = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_SHA_256);
		public const uint CALG_SHA_384 = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_SHA_384);
		public const uint CALG_SHA_512 = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_SHA_512);

		// KP_MODE
		public const uint CRYPT_MODE_CBCI= 6;       // ANSI CBC Interleaved
		public const uint CRYPT_MODE_CFBP = 7;       // ANSI CFB Pipelined
		public const uint CRYPT_MODE_OFBP = 8;       // ANSI OFB Pipelined
		public const uint CRYPT_MODE_CBCOFM = 9;       // ANSI CBC + OF Masking
		public const uint CRYPT_MODE_CBCOFMI = 10;      // ANSI CBC + OFM Interleaved

		// resource number for signatures in the CSP
		public const uint SIGNATURE_RESOURCE_NUMBER	= 0x29A;

		// dwFlag definitions for CryptGenKey
		public const uint CRYPT_EXPORTABLE = 0x00000001;
		public const uint CRYPT_USER_PROTECTED = 0x00000002;
		public const uint CRYPT_CREATE_SALT	= 0x00000004;
		public const uint CRYPT_UPDATE_KEY = 0x00000008;
		public const uint CRYPT_NO_SALT = 0x00000010;
		public const uint CRYPT_PREGEN = 0x00000040;
		public const uint CRYPT_RECIPIENT = 0x00000010;
		public const uint CRYPT_INITIATOR = 0x00000040;
		public const uint CRYPT_ONLINE = 0x00000080;
		public const uint CRYPT_SF = 0x00000100;
		public const uint CRYPT_CREATE_IV = 0x00000200;
		public const uint CRYPT_KEK	= 0x00000400;
		public const uint CRYPT_DATA_KEY = 0x00000800;
		public const uint CRYPT_VOLATILE = 0x00001000;
		public const uint CRYPT_SGCKEY = 0x00002000;
		public const uint CRYPT_ARCHIVABLE = 0x00004000;

		public const uint RSA1024BIT_KEY = 0x04000000;

		// dwFlags definitions for CryptDeriveKey
		public const uint CRYPT_SERVER	= 0x00000400;

		public const uint KEY_LENGTH_MASK = 0xFFFF0000;

		// dwFlag definitions for CryptExportKey
		public const uint CRYPT_Y_ONLY = 0x00000001;
		public const uint CRYPT_SSL2_FALLBACK = 0x00000002;
		public const uint CRYPT_DESTROYKEY = 0x00000004;
		public const uint CRYPT_OAEP = 0x00000040;  // used with RSA encryptions/decryptions
		// CryptExportKey, CryptImportKey,
		// CryptEncrypt and CryptDecrypt

		public const uint CRYPT_BLOB_VER3 = 0x00000080;  // export version 3 of a blob type
		public const uint CRYPT_IPSEC_HMAC_KEY = 0x00000100;  // CryptImportKey only

		// dwFlags definitions for CryptDecrypt
		//  See also CRYPT_OAEP, above.
		//  Note, the following flag is not supported for CryptEncrypt
		public const uint CRYPT_DECRYPT_RSA_NO_PADDING_CHECK = 0x00000020;

		// dwFlags definitions for CryptCreateHash
		public const uint CRYPT_SECRETDIGEST = 0x00000001;

		// dwFlags definitions for CryptHashData
		public const uint CRYPT_OWF_REPL_LM_HASH = 0x00000001;  // this is only for the OWF replacement CSP

		// dwFlags definitions for CryptHashSessionKey
		public const uint CRYPT_LITTLE_ENDIAN = 0x00000001;

		// dwFlags definitions for CryptSignHash and CryptVerifySignature
		public const uint CRYPT_NOHASHOID = 0x00000001;
		public const uint CRYPT_TYPE2_FORMAT = 0x00000002;
		public const uint CRYPT_X931_FORMAT = 0x00000004;

		// exported key blob definitions
		public const uint SIMPLEBLOB = 0x1;
		public const uint PUBLICKEYBLOB = 0x6;
		public const uint PRIVATEKEYBLOB = 0x7;
		public const uint PLAINTEXTKEYBLOB = 0x8;
		public const uint OPAQUEKEYBLOB = 0x9;
		public const uint PUBLICKEYBLOBEX = 0xA;
		public const uint SYMMETRICWRAPKEYBLOB = 0xB;

		public const uint AT_KEYEXCHANGE = 1;
		public const uint AT_SIGNATURE = 2;

		public const uint CRYPT_USERDATA = 1;

		// dwParam
		public const uint KP_IV = 1;       // Initialization vector
		public const uint KP_SALT = 2;       // Salt value
		public const uint KP_PADDING = 3;       // Padding values
		public const uint KP_MODE = 4;       // Mode of the cipher
		public const uint KP_MODE_BITS = 5;       // Number of bits to feedback
		public const uint KP_PERMISSIONS = 6;       // Key permissions DWORD
		public const uint KP_ALGID = 7;       // Key algorithm
		public const uint KP_BLOCKLEN = 8;       // Block size of the cipher
		public const uint KP_KEYLEN = 9;       // Length of key in bits
		public const uint KP_SALT_EX = 10;      // Length of salt in bytes
		public const uint KP_P = 11;      // DSS/Diffie-Hellman P value
		public const uint KP_G = 12;      // DSS/Diffie-Hellman G value
		public const uint KP_Q = 13;      // DSS Q value
		public const uint KP_X = 14;      // Diffie-Hellman X value
		public const uint KP_Y = 15;      // Y value
		public const uint KP_RA = 16;      // Fortezza RA value
		public const uint KP_RB = 17;      // Fortezza RB value
		public const uint KP_INFO = 18;      // for putting information into an RSA envelope
		public const uint KP_EFFECTIVE_KEYLEN = 19;      // setting and getting RC2 effective key length
		public const uint KP_SCHANNEL_ALG = 20;      // for setting the Secure Channel algorithms
		public const uint KP_CLIENT_RANDOM = 21;      // for setting the Secure Channel client random data
		public const uint KP_SERVER_RANDOM = 22;      // for setting the Secure Channel server random data
		public const uint KP_RP = 23;
		public const uint KP_PRECOMP_MD5 = 24;
		public const uint KP_PRECOMP_SHA = 25;
		public const uint KP_CERTIFICATE = 26;      // for setting Secure Channel certificate data (PCT1)
		public const uint KP_CLEAR_KEY = 27;      // for setting Secure Channel clear key data (PCT1)
		public const uint KP_PUB_EX_LEN = 28;
		public const uint KP_PUB_EX_VAL = 29;
		public const uint KP_KEYVAL = 30;
		public const uint KP_ADMIN_PIN = 31;
		public const uint KP_KEYEXCHANGE_PIN = 32;
		public const uint KP_SIGNATURE_PIN = 33;
		public const uint KP_PREHASH = 34;
		public const uint KP_ROUNDS = 35;
		public const uint KP_OAEP_PARAMS = 36;      // for setting OAEP params on RSA keys
		public const uint KP_CMS_KEY_INFO = 37;
		public const uint KP_CMS_DH_KEY_INFO = 38;
		public const uint KP_PUB_PARAMS = 39;      // for setting public parameters
		public const uint KP_VERIFY_PARAMS = 40;      // for verifying DSA and DH parameters
		public const uint KP_HIGHEST_VERSION = 41;      // for TLS protocol version setting
		public const uint KP_GET_USE_COUNT = 42;      // for use with PP_CRYPT_COUNT_KEY_USE contexts

		// KP_PADDING
		public const uint PKCS5_PADDING = 1;       // PKCS 5 (sec 6.2) padding method
		public const uint RANDOM_PADDING = 2;
		public const uint ZERO_PADDING = 3;

		// KP_MODE
		public const uint CRYPT_MODE_CBC = 1;       // Cipher block chaining
		public const uint CRYPT_MODE_ECB = 2;       // Electronic code book
		public const uint CRYPT_MODE_OFB = 3;       // Output feedback mode
		public const uint CRYPT_MODE_CFB = 4;       // Cipher feedback mode
		public const uint CRYPT_MODE_CTS = 5;       // Ciphertext stealing mode

		// KP_PERMISSIONS
		public const uint CRYPT_ENCRYPT = 0x0001;  // Allow encryption
		public const uint CRYPT_DECRYPT = 0x0002;  // Allow decryption
		public const uint CRYPT_EXPORT = 0x0004;  // Allow key to be exported
		public const uint CRYPT_READ = 0x0008;  // Allow parameters to be read
		public const uint CRYPT_WRITE = 0x0010;  // Allow parameters to be set
		public const uint CRYPT_MAC = 0x0020;  // Allow MACs to be used with key
		public const uint CRYPT_EXPORT_KEY = 0x0040;  // Allow key to be used for exporting keys
		public const uint CRYPT_IMPORT_KEY = 0x0080;  // Allow key to be used for importing keys
		public const uint CRYPT_ARCHIVE = 0x0100;  // Allow key to be exported at creation only

		public const uint HP_ALGID = 0x0001;  // Hash algorithm
		public const uint HP_HASHVAL = 0x0002;  // Hash value
		public const uint HP_HASHSIZE = 0x0004;  // Hash value size
		public const uint HP_HMAC_INFO = 0x0005;  // information for creating an HMAC
		public const uint HP_TLS1PRF_LABEL = 0x0006;  // label for TLS1 PRF
		public const uint HP_TLS1PRF_SEED = 0x0007;  // seed for TLS1 PRF

		public const uint CRYPT_FAILED = 0; //FALSE
		public const uint CRYPT_SUCCEED = 1; //TRUE

		public const uint CRYPT_FIRST = 1;
		public const uint CRYPT_NEXT = 2;
		public const uint CRYPT_SGC_ENUM = 4;

		public const uint CRYPT_IMPL_HARDWARE = 1;
		public const uint CRYPT_IMPL_SOFTWARE = 2;
		public const uint CRYPT_IMPL_MIXED = 3;
		public const uint CRYPT_IMPL_UNKNOWN = 4;
		public const uint CRYPT_IMPL_REMOVABLE = 8;

		// key storage flags
		public const uint CRYPT_SEC_DESCR = 0x00000001;
		public const uint CRYPT_PSTORE = 0x00000002;
		public const uint CRYPT_UI_PROMPT = 0x00000004;

		// protocol flags
		public const uint CRYPT_FLAG_PCT1 = 0x0001;
		public const uint CRYPT_FLAG_SSL2 = 0x0002;
		public const uint CRYPT_FLAG_SSL3 = 0x0004;
		public const uint CRYPT_FLAG_TLS1 = 0x0008;
		public const uint CRYPT_FLAG_IPSEC = 0x0010;
		public const uint CRYPT_FLAG_SIGNING = 0x0020;

		// SGC flags
		public const uint CRYPT_SGC = 0x0001;
		public const uint CRYPT_FASTSGC = 0x0002;

		//
		// CryptSetProvParam
		//
		public const uint PP_CLIENT_HWND = 1;
		public const uint PP_CONTEXT_INFO = 11;
		public const uint PP_KEYEXCHANGE_KEYSIZE = 12;
		public const uint PP_SIGNATURE_KEYSIZE = 13;
		public const uint PP_KEYEXCHANGE_ALG = 14;
		public const uint PP_SIGNATURE_ALG = 15;
		public const uint PP_DELETEKEY = 24;

		//max for CSP and container names
		public const uint MAXUIDLEN = 64;

		// Exponentiation Offload Reg Location
		public const string EXPO_OFFLOAD_REG_VALUE = "ExpoOffload";
		public const string EXPO_OFFLOAD_FUNC_NAME = "OffloadModExpo";

		//
		// Registry key in which the following private key-related
		// values are created.
		//
		public const string szKEY_CRYPTOAPI_PRIVATE_KEY_OPTIONS = "Software\\Policies\\Microsoft\\Cryptography" ;

		//
		// Registry value for controlling Data Protection API (DPAPI) UI settings.
		//
		public const string szFORCE_KEY_PROTECTION = "ForceKeyProtection";

		public const uint dwFORCE_KEY_PROTECTION_DISABLED = 0x0;
		public const uint dwFORCE_KEY_PROTECTION_USER_SELECT = 0x1;
		public const uint dwFORCE_KEY_PROTECTION_HIGH = 0x2;
 
		//
		// Registry values for enabling and controlling the caching (and timeout)
		// of private keys.  This feature is useful only for UI-protected private
		// keys.
		//
		// Note that in Windows 2000 and later, private keys, once read from storage,
		// are cached in the associated HCRYPTPROV structure for subsequent use.
		//
		// In .NET Server and XP SP1, new key caching behavior is available.  Keys
		// that have been read from storage and cached may now be considered "stale"
		// if a period of time has elapsed since the key was last used.  This forces
		// the key to be re-read from storage (which will make the DPAPI UI appear 
		// again).
		//
		// To enable the new behavior, create the registry DWORD value 
		// szKEY_CACHE_ENABLED and set it to 1.  The registry DWORD value
		// szKEY_CACHE_SECONDS must also be created and set to the number of seconds
		// that a cached private key may still be considered usable.  
		//
		public const string szKEY_CACHE_ENABLED = "CachePrivateKeys";
		public const string szKEY_CACHE_SECONDS = "PrivateKeyLifetimeSeconds";

		public const uint CUR_BLOB_VERSION = 2;
			
		// uses of algortihms for SCHANNEL_ALG structure
		public const uint SCHANNEL_MAC_KEY = 0x00000000;
		public const uint SCHANNEL_ENC_KEY = 0x00000001;

		// uses of dwFlags SCHANNEL_ALG structure
		public const uint INTERNATIONAL_USAGE = 0x00000001;

		public const string MS_DEF_PROV = "Microsoft Base Cryptographic Provider v1.0";
		public const string MS_ENHANCED_PROV = "Microsoft Enhanced Cryptographic Provider v1.0";
		public const string MS_STRONG_PROV = "Microsoft Strong Cryptographic Provider";
		public const string MS_DEF_RSA_SIG_PROV = "Microsoft RSA Signature Cryptographic Provider";
		public const string MS_DEF_RSA_SCHANNEL_PROV = "Microsoft RSA SChannel Cryptographic Provider";
		public const string MS_DEF_DSS_PROV = "Microsoft Base DSS Cryptographic Provider";
		public const string MS_DEF_DSS_DH_PROV = "Microsoft Base DSS and Diffie-Hellman Cryptographic Provider";
		public const string MS_ENH_DSS_DH_PROV = "Microsoft Enhanced DSS and Diffie-Hellman Cryptographic Provider";
		public const string MS_DEF_DH_SCHANNEL_PROV = "Microsoft DH SChannel Cryptographic Provider";
		public const string MS_SCARD_PROV = "Microsoft Base Smart Card Crypto Provider";
		public const string MS_ENH_RSA_AES_PROV = "Microsoft Enhanced RSA and AES Cryptographic Provider";

			// CryptGetProvParam
		public const uint PP_ENUMALGS = 1;
		public const uint PP_ENUMCONTAINERS = 2;
		public const uint PP_IMPTYPE = 3;
		public const uint PP_NAME = 4;
		public const uint PP_VERSION = 5;
		public const uint PP_CONTAINER = 6;
		public const uint PP_CHANGE_PASSWORD = 7;
		public const uint PP_KEYSET_SEC_DESCR = 8; // get/set security descriptor of keyset
		public const uint PP_CERTCHAIN = 9; // for retrieving certificates from tokens
		public const uint PP_KEY_TYPE_SUBTYPE = 10;
		public const uint PP_PROVTYPE = 16;
		public const uint PP_KEYSTORAGE = 17;
		public const uint PP_APPLI_CERT = 18;
		public const uint PP_SYM_KEYSIZE = 19;
		public const uint PP_SESSION_KEYSIZE = 20;
		public const uint PP_UI_PROMPT = 21;
		public const uint PP_ENUMALGS_EX = 22;
		public const uint PP_ENUMMANDROOTS = 25;
		public const uint PP_ENUMELECTROOTS = 26;
		public const uint PP_KEYSET_TYPE = 27;
		public const uint PP_ADMIN_PIN = 31;
		public const uint PP_KEYEXCHANGE_PIN = 32;
		public const uint PP_SIGNATURE_PIN = 33;
		public const uint PP_SIG_KEYSIZE_INC = 34;
		public const uint PP_KEYX_KEYSIZE_INC = 35;
		public const uint PP_UNIQUE_CONTAINER = 36;
		public const uint PP_SGC_INFO = 37;
		public const uint PP_USE_HARDWARE_RNG = 38;
		public const uint PP_KEYSPEC = 39;
		public const uint PP_ENUMEX_SIGNING_PROT = 40;
		public const uint PP_CRYPT_COUNT_KEY_USE = 41;

		// dwFlags definitions for CryptAcquireContext
		public const uint CRYPT_VERIFYCONTEXT = 0xF0000000;
		public const uint CRYPT_NEWKEYSET = 0x00000008;
		public const uint CRYPT_DELETEKEYSET = 0x00000010;
		public const uint CRYPT_MACHINE_KEYSET = 0x00000020;
		public const uint CRYPT_SILENT = 0x00000040;

		// dwFlag definitions for CryptSetProviderEx and CryptGetDefaultProvider
		public const uint CRYPT_MACHINE_DEFAULT = 0x00000001;
		public const uint CRYPT_USER_DEFAULT = 0x00000002;
		public const uint CRYPT_DELETE_DEFAULT = 0x00000004;

		public const uint PROV_RSA_FULL = 1;
		public const uint PROV_RSA_SIG = 2;
		public const uint PROV_DSS = 3;
		public const uint PROV_FORTEZZA = 4;
		public const uint PROV_MS_EXCHANGE = 5;
		public const uint PROV_SSL = 6;
		public const uint PROV_RSA_SCHANNEL = 12;
		public const uint PROV_DSS_DH = 13;
		public const uint PROV_EC_ECDSA_SIG = 14;
		public const uint PROV_EC_ECNRA_SIG = 15;
		public const uint PROV_EC_ECDSA_FULL = 16;
		public const uint PROV_EC_ECNRA_FULL = 17;
		public const uint PROV_DH_SCHANNEL = 18;
		public const uint PROV_SPYRUS_LYNKS = 20;
		public const uint PROV_RNG = 21;
		public const uint PROV_INTEL_SEC = 22;
		public const uint PROV_REPLACE_OWF = 23;
		public const uint PROV_RSA_AES = 24;

		// base provider action
		//{ 0xdf9d8cd0, 0x1501, 0x11d1, {0x8c, 0x7a, 0x00, 0xc0, 0x4f, 0xc2, 0x97, 0xeb} }
		//public const uint CRYPTPROTECT_DEFAULT_PROVIDER_INTERNAL = {0x8c, 0x7a, 0x00, 0xc0, 0x4f, 0xc2, 0x97, 0xeb}; 
		//public const uint CRYPTPROTECT_DEFAULT_PROVIDER = { 0xdf9d8cd0, 0x1501, 0x11d1, CRYPTPROTECT_DEFAULT_PROVIDER_INTERNAL };
		// CryptProtect PromptStruct dwPromtFlags
		// prompt on unprotect
		public const uint CRYPTPROTECT_PROMPT_ON_UNPROTECT = 0x1;  // 1<<0
		// prompt on protect
		public const uint CRYPTPROTECT_PROMPT_ON_PROTECT = 0x2;  // 1<<1
		public const uint CRYPTPROTECT_PROMPT_RESERVED = 0x04; // reserved, do not use.
		// default to strong variant UI protection (user supplied password currently).
		public const uint CRYPTPROTECT_PROMPT_STRONG = 0x08; // 1<<3
		// require strong variant UI protection (user supplied password currently).
		public const uint CRYPTPROTECT_PROMPT_REQUIRE_STRONG = 0x10; // 1<<4
		// CryptProtectData and CryptUnprotectData dwFlags
		// for remote-access situations where ui is not an option
		// if UI was specified on protect or unprotect operation, the call
		// will fail and GetLastError() will indicate ERROR_PASSWORD_RESTRICTION
		public const uint CRYPTPROTECT_UI_FORBIDDEN = 0x1;
		// per machine protected data -- any user on machine where CryptProtectData
		// took place may CryptUnprotectData
		public const uint CRYPTPROTECT_LOCAL_MACHINE = 0x4;
		// force credential synchronize during CryptProtectData()
		// Synchronize is only operation that occurs during this operation
		public const uint CRYPTPROTECT_CRED_SYNC = 0x8;
		// Generate an Audit on protect and unprotect operations
		public const uint CRYPTPROTECT_AUDIT = 0x10;
		// Protect data with a non-recoverable key
		public const uint CRYPTPROTECT_NO_RECOVERY = 0x20;
		// Verify the protection of a protected blob
		public const uint CRYPTPROTECT_VERIFY_PROTECTION = 0x40;
		// Regenerate the local machine protection
		public const uint CRYPTPROTECT_CRED_REGENERATE = 0x80;
		// Only allow decryption from system (trusted) processes (Windows CE)
		public const uint CRYPTPROTECT_SYSTEM = 0x20000000;
		// flags reserved for system use
		public const uint CRYPTPROTECT_FIRST_RESERVED_FLAGVAL = 0x0FFFFFFF;
		public const uint CRYPTPROTECT_LAST_RESERVED_FLAGVAL = 0xFFFFFFFF;

		//NOT FROM WinCrypt.h
		//public const int MAX_KEYBLOB = 1024;
		//public const uint MAX_HASH = 64;
	}
}
