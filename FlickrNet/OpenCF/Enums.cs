//==========================================================================================
//
//		OpenNETCF.Windows.Forms.Enums.cs
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
    internal enum PaddingMode : uint
	{
		PKCS5 = 1,       // PKCS 5 (sec 6.2) padding method
		RANDOM = 2,
		ZERO = 3,
	}

    internal enum KeyBlob : uint
	{
		// exported key blob definitions
		SIMPLEBLOB = 0x1,
		PUBLICKEYBLOB = 0x6,
		PRIVATEKEYBLOB = 0x7,
		PLAINTEXTKEYBLOB = 0x8,
		OPAQUEKEYBLOB = 0x9,
		PUBLICKEYBLOBEX = 0xA,
		SYMMETRICWRAPKEYBLOB = 0xB,
	}

	[Flags]
    internal enum ExportKeyParam : uint
	{
		// dwFlag definitions for CryptExportKey
		Y_ONLY = 0x00000001,
		SSL2_FALLBACK = 0x00000002,
		DESTROYKEY = 0x00000004,
		OAEP = 0x00000040,  // used with RSA encryptions/decryptions
	}	

    internal enum KeyParam : uint
	{
		IV = 1,      // Initialization vector
		SALT = 2,       // Salt value
		PADDING = 3,       // Padding values
		MODE = 4,       // Mode of the cipher
		MODE_BITS = 5,       // Number of bits to feedback
		PERMISSIONS = 6,       // Key permissions DWORD
		ALGID = 7,       // Key algorithm
		BLOCKLEN = 8,       // Block size of the cipher
		KEYLEN = 9,       // Length of key in bits
		SALT_EX = 10,      // Length of salt in bytes
		P = 11,      // DSS/Diffie-Hellman P value
		G = 12,      // DSS/Diffie-Hellman G value
		Q = 13,      // DSS Q value
		X = 14,      // Diffie-Hellman X value
		Y = 15,      // Y value
		RA = 16,      // Fortezza RA value
		RB = 17,      // Fortezza RB value
		INFO = 18,      // for putting information into an RSA envelope
		EFFECTIVE_KEYLEN = 19,      // setting and getting RC2 effective key length
		SCHANNEL_ALG = 20,      // for setting the Secure Channel algorithms
		CLIENT_RANDOM = 21,      // for setting the Secure Channel client random data
		SERVER_RANDOM = 22,      // for setting the Secure Channel server random data
		RP = 23,
		PRECOMP_MD5 = 24,
		PRECOMP_SHA = 25,
		CERTIFICATE = 26,      // for setting Secure Channel certificate data (PCT1)
		CLEAR_KEY = 27,      // for setting Secure Channel clear key data (PCT1)
		PUB_EX_LEN = 28,
		PUB_EX_VAL = 29,
		KEYVAL = 30,
		ADMIN_PIN = 31,
		KEYEXCHANGE_PIN = 32,
		SIGNATURE_PIN = 33,
		PREHASH = 34,
		ROUNDS = 35,
		OAEP_PARAMS = 36,      // for setting OAEP params on RSA keys
		CMS_KEY_INFO = 37,
		CMS_DH_KEY_INFO = 38,
		PUB_PARAMS = 39,      // for setting public parameters
		VERIFY_PARAMS = 40,      // for verifying DSA and DH parameters
		HIGHEST_VERSION = 41,      // for TLS protocol version setting
		GET_USE_COUNT = 42,      // for use with PP_CRYPT_COUNT_KEY_USE contexts
	}

	[Flags]
    internal enum GenKeyParam : uint
	{
		NONE = 0,
		EXPORTABLE = 0x00000001,
		USER_PROTECTED = 0x00000002,
		CREATE_SALT	= 0x00000004,
		UPDATE_KEY = 0x00000008,
		NO_SALT = 0x00000010,
		PREGEN = 0x00000040,
		RECIPIENT = 0x00000010,
		INITIATOR = 0x00000040,
		ONLINE = 0x00000080,
		CRYPT_SF = 0x00000100,
		CREATE_IV = 0x00000200,
		KEK	= 0x00000400,
		DATA_KEY = 0x00000800,
		VOLATILE = 0x00001000,
		SGCKEY = 0x00002000,
		ARCHIVABLE = 0x00004000,
	}

	[Flags]
    internal enum ProtectPromptStruct : uint
	{
		// CryptProtect PromptStruct dwPromtFlags
		// prompt on unprotect
		ON_UNPROTECT = 0x1,  // 1<<0
		// prompt on protect
		ON_PROTECT = 0x2,  // 1<<1
		RESERVED = 0x04, // reserved, do not use.
		// default to strong variant UI protection (user supplied password currently).
		STRONG = 0x08, // 1<<3
		// require strong variant UI protection (user supplied password currently).
		REQUIRE_STRONG = 0x10, // 1<<4
	}

	[Flags]
    internal enum ProtectParam : uint
	{
		// CryptProtectData and CryptUnprotectData dwFlags
		UI_FORBIDDEN = 0x1, // for remote-access situations where ui is not an option
		// if UI was specified on protect or unprotect operation, the call
		// will fail and GetLastError() will indicate ERROR_PASSWORD_RESTRICTION
		LOCAL_MACHINE = 0x4, // per machine protected data -- any user on machine where CryptProtectData
		// took place may CryptUnprotectData
		CRED_SYNC = 0x8,// force credential synchronize during CryptProtectData()
		// Synchronize is only operation that occurs during this operation
		AUDIT = 0x10, // Generate an Audit on protect and unprotect operations
		NO_RECOVERY = 0x20, // Protect data with a non-recoverable key
		VERIFY_PROTECTION = 0x40, // Verify the protection of a protected blob
		CRED_REGENERATE = 0x80, // Regenerate the local machine protection
		SYSTEM = 0x20000000, // Only allow decryption from system (trusted) processes (Windows CE)
		FIRST_RESERVED_FLAGVAL = 0x0FFFFFFF, // flags reserved for system use
		LAST_RESERVED_FLAGVAL = 0xFFFFFFFF,
	}

	//[CLSCompliant(false)]
    internal enum KeySpec : int //mod from uint to make RSACryptoServiceProvider compliant
	{
		KEYEXCHANGE = 1,
		SIGNATURE = 2,
	}

    internal enum HashParam : uint
	{
		ALGID = 0x0001,  // Hash algorithm
		HASHVAL = 0x0002,  // Hash value
		HASHSIZE = 0x0004,  // Hash value size
		HMAC_INFO = 0x0005,  // information for creating an HMAC
		TLS1PRF_LABEL = 0x0006,  // label for TLS1 PRF
		TLS1PRF_SEED = 0x0007,  // seed for TLS1 PRF
	}

    internal enum CalgHash : uint
	{
		MD2 = (AlgClass.HASH | AlgType.ANY | AlgSid.MD2),
		MD4 = (AlgClass.HASH | AlgType.ANY | AlgSid.MD4),
		MD5 = (AlgClass.HASH | AlgType.ANY | AlgSid.MD5), //32771
		SHA = (AlgClass.HASH | AlgType.ANY | AlgSid.SHA),
		SHA1 = (AlgClass.HASH | AlgType.ANY | AlgSid.SHA1),
		MAC = (AlgClass.HASH | AlgType.ANY | AlgSid.MAC),
		SSL3_SHAMD5 = (AlgClass.HASH | AlgType.ANY | AlgSid.SSL3SHAMD5),
		HMAC = (AlgClass.HASH | AlgType.ANY | AlgSid.HMAC),
		TLS1PRF = (AlgClass.HASH | AlgType.ANY | AlgSid.TLS1PRF),
		HASH_REPLACE_OWF = (AlgClass.HASH | AlgType.ANY | AlgSid.HASH_REPLACE_OWF),
		SHA_256 = (AlgClass.HASH | AlgType.ANY | AlgSid.SHA_256),
		SHA_384 = (AlgClass.HASH | AlgType.ANY | AlgSid.SHA_384),
		SHA_512 = (AlgClass.HASH | AlgType.ANY | AlgSid.SHA_512),
	}

    internal enum CalgEncrypt : uint
	{
		DES = (AlgClass.DATA_ENCRYPT | AlgType.BLOCK | AlgSid.DES), //26113
		TRIP_DES_112 = (AlgClass.DATA_ENCRYPT | AlgType.BLOCK | AlgSid.TRIP_DES_112),
		TRIP_DES = (AlgClass.DATA_ENCRYPT | AlgType.BLOCK | AlgSid.TRIP_DES),
		DESX = (AlgClass.DATA_ENCRYPT | AlgType.BLOCK | AlgSid.DESX),
		RC2 = (AlgClass.DATA_ENCRYPT | AlgType.BLOCK | AlgSid.RC2),
		RC4 = (AlgClass.DATA_ENCRYPT | AlgType.STREAM | AlgSid.RC4), //26625
		SEAL = (AlgClass.DATA_ENCRYPT | AlgType.STREAM | AlgSid.SEAL),
		SKIPJACK = (AlgClass.DATA_ENCRYPT | AlgType.BLOCK | AlgSid.SKIPJACK),
		TEK = (AlgClass.DATA_ENCRYPT | AlgType.BLOCK | AlgSid.TEK),
		CYLINK_MEK = (AlgClass.DATA_ENCRYPT | AlgType.BLOCK | AlgSid.CYLINK_MEK),
		SSL3_MASTER = (AlgClass.MSG_ENCRYPT | AlgType.SECURECHANNEL | AlgSid.SSL3_MASTER),
		SCHANNEL_MASTER_HASH = (AlgClass.MSG_ENCRYPT | AlgType.SECURECHANNEL | AlgSid.SCHANNEL_MASTER_HASH),
		SCHANNEL_MAC_KEY = (AlgClass.MSG_ENCRYPT | AlgType.SECURECHANNEL | AlgSid.SCHANNEL_MAC_KEY),
		SCHANNEL_ENC_KEY = (AlgClass.MSG_ENCRYPT | AlgType.SECURECHANNEL | AlgSid.SCHANNEL_ENC_KEY),
		PCT1_MASTER = (AlgClass.MSG_ENCRYPT | AlgType.SECURECHANNEL | AlgSid.PCT1_MASTER),
		SSL2_MASTER = (AlgClass.MSG_ENCRYPT | AlgType.SECURECHANNEL | AlgSid.SSL2_MASTER),
		TLS1_MASTER = (AlgClass.MSG_ENCRYPT | AlgType.SECURECHANNEL | AlgSid.TLS1_MASTER),
		RC5 = (AlgClass.DATA_ENCRYPT | AlgType.BLOCK|AlgSid.RC5),
		AES_128 = (AlgClass.DATA_ENCRYPT | AlgType.BLOCK | AlgSid.AES_128),
		AES_192 = (AlgClass.DATA_ENCRYPT | AlgType.BLOCK | AlgSid.AES_192),
		AES_256 = (AlgClass.DATA_ENCRYPT | AlgType.BLOCK | AlgSid.AES_256),
		AES = (AlgClass.DATA_ENCRYPT | AlgType.BLOCK | AlgSid.AES),
	}

	internal enum CalgSign : uint
	{
		RSA_SIGN = (AlgClass.SIGNATURE | AlgType.RSA | AlgSid.RSA_ANY),
		DSS_SIGN = (AlgClass.SIGNATURE | AlgType.DSS | AlgSid.DSS_ANY),
		NO_SIGN = (AlgClass.SIGNATURE | AlgType.ANY | AlgSid.ANY),
	}

	internal enum CalgKeyEx : uint
	{
		RSA_KEYX = (AlgClass.KEY_EXCHANGE | AlgType.RSA | AlgSid.RSA_ANY),
		DH_SF = (AlgClass.KEY_EXCHANGE | AlgType.DH | AlgSid.DH_SANDF),
		DH_EPHEM = (AlgClass.KEY_EXCHANGE | AlgType.DH | AlgSid.DH_EPHEM),
		AGREEDKEY_ANY = (AlgClass.KEY_EXCHANGE | AlgType.DH | AlgSid.AGREED_KEY_ANY),
		KEA_KEYX = (AlgClass.KEY_EXCHANGE | AlgType.DH | AlgSid.KEA),
		HUGHES_MD5 = (AlgClass.KEY_EXCHANGE | AlgType.ANY | AlgSid.MD5),
	}

	internal enum Calg : uint
	{
		//key spec - for GenKey
		AT_KEYEXCHANGE = 1,
		AT_SIGNATURE = 2,
		// algorithm identifier definitions
		MD2 = (AlgClass.HASH | AlgType.ANY | AlgSid.MD2),
		MD4 = (AlgClass.HASH | AlgType.ANY | AlgSid.MD4),
		MD5 = (AlgClass.HASH | AlgType.ANY | AlgSid.MD5), //32771
		SHA = (AlgClass.HASH | AlgType.ANY | AlgSid.SHA),
		SHA1 = (AlgClass.HASH | AlgType.ANY | AlgSid.SHA1),
		MAC = (AlgClass.HASH | AlgType.ANY | AlgSid.MAC),
		RSA_SIGN = (AlgClass.SIGNATURE | AlgType.RSA | AlgSid.RSA_ANY),
		DSS_SIGN = (AlgClass.SIGNATURE | AlgType.DSS | AlgSid.DSS_ANY),
		NO_SIGN = (AlgClass.SIGNATURE | AlgType.ANY | AlgSid.ANY),
		RSA_KEYX = (AlgClass.KEY_EXCHANGE | AlgType.RSA | AlgSid.RSA_ANY),
		DES = (AlgClass.DATA_ENCRYPT | AlgType.BLOCK | AlgSid.DES), //26113
		TRIP_DES_112 = (AlgClass.DATA_ENCRYPT | AlgType.BLOCK | AlgSid.TRIP_DES_112),
		TRIP_DES = (AlgClass.DATA_ENCRYPT | AlgType.BLOCK | AlgSid.TRIP_DES),
		DESX = (AlgClass.DATA_ENCRYPT | AlgType.BLOCK | AlgSid.DESX),
		RC2 = (AlgClass.DATA_ENCRYPT | AlgType.BLOCK | AlgSid.RC2),
		RC4 = (AlgClass.DATA_ENCRYPT | AlgType.STREAM | AlgSid.RC4), //26625
		SEAL = (AlgClass.DATA_ENCRYPT | AlgType.STREAM | AlgSid.SEAL),
		DH_SF = (AlgClass.KEY_EXCHANGE | AlgType.DH | AlgSid.DH_SANDF),
		DH_EPHEM = (AlgClass.KEY_EXCHANGE | AlgType.DH | AlgSid.DH_EPHEM),
		AGREEDKEY_ANY = (AlgClass.KEY_EXCHANGE | AlgType.DH | AlgSid.AGREED_KEY_ANY),
		KEA_KEYX = (AlgClass.KEY_EXCHANGE | AlgType.DH | AlgSid.KEA),
		HUGHES_MD5 = (AlgClass.KEY_EXCHANGE | AlgType.ANY | AlgSid.MD5),
		SKIPJACK = (AlgClass.DATA_ENCRYPT | AlgType.BLOCK | AlgSid.SKIPJACK),
		TEK = (AlgClass.DATA_ENCRYPT | AlgType.BLOCK | AlgSid.TEK),
		CYLINK_MEK = (AlgClass.DATA_ENCRYPT | AlgType.BLOCK | AlgSid.CYLINK_MEK),
		SSL3_SHAMD5 = (AlgClass.HASH | AlgType.ANY | AlgSid.SSL3SHAMD5),
		SSL3_MASTER = (AlgClass.MSG_ENCRYPT | AlgType.SECURECHANNEL | AlgSid.SSL3_MASTER),
		SCHANNEL_MASTER_HASH = (AlgClass.MSG_ENCRYPT | AlgType.SECURECHANNEL | AlgSid.SCHANNEL_MASTER_HASH),
		SCHANNEL_MAC_KEY = (AlgClass.MSG_ENCRYPT | AlgType.SECURECHANNEL | AlgSid.SCHANNEL_MAC_KEY),
		SCHANNEL_ENC_KEY = (AlgClass.MSG_ENCRYPT | AlgType.SECURECHANNEL | AlgSid.SCHANNEL_ENC_KEY),
		PCT1_MASTER = (AlgClass.MSG_ENCRYPT | AlgType.SECURECHANNEL | AlgSid.PCT1_MASTER),
		SSL2_MASTER = (AlgClass.MSG_ENCRYPT | AlgType.SECURECHANNEL | AlgSid.SSL2_MASTER),
		TLS1_MASTER = (AlgClass.MSG_ENCRYPT | AlgType.SECURECHANNEL | AlgSid.TLS1_MASTER),
		RC5 = (AlgClass.DATA_ENCRYPT | AlgType.BLOCK|AlgSid.RC5),
		HMAC = (AlgClass.HASH | AlgType.ANY | AlgSid.HMAC),
		TLS1PRF = (AlgClass.HASH | AlgType.ANY | AlgSid.TLS1PRF),
		HASH_REPLACE_OWF = (AlgClass.HASH | AlgType.ANY | AlgSid.HASH_REPLACE_OWF),
		AES_128 = (AlgClass.DATA_ENCRYPT | AlgType.BLOCK | AlgSid.AES_128),
		AES_192 = (AlgClass.DATA_ENCRYPT | AlgType.BLOCK | AlgSid.AES_192),
		AES_256 = (AlgClass.DATA_ENCRYPT | AlgType.BLOCK | AlgSid.AES_256),
		AES = (AlgClass.DATA_ENCRYPT | AlgType.BLOCK | AlgSid.AES),
		SHA_256 = (AlgClass.HASH | AlgType.ANY | AlgSid.SHA_256),
		SHA_384 = (AlgClass.HASH | AlgType.ANY | AlgSid.SHA_384),
		SHA_512 = (AlgClass.HASH | AlgType.ANY | AlgSid.SHA_512),
	}

	internal class AlgSid
	{
		// Generic sub-ids
		public const uint ANY = (0);
		// Some RSA sub-ids
		public const uint RSA_ANY = 0;
		public const uint RSA_PKCS = 1;
		public const uint RSA_MSATWORK = 2;
		public const uint RSA_ENTRUST = 3;
		public const uint RSA_PGP = 4;
		// Some DSS sub-ids
		public const uint DSS_ANY = 0;
		public const uint DSS_PKCS = 1;
		public const uint DSS_DMS = 2;
		// Block cipher sub ids
		// DES sub_ids
		public const uint DES = 1;
		public const uint TRIP_DES = 3;
		public const uint DESX = 4;
		public const uint IDEA = 5;
		public const uint CAST = 6;
		public const uint SAFERSK64 = 7;
		public const uint SAFERSK128 = 8;
		public const uint TRIP_DES_112 = 9;
		public const uint CYLINK_MEK = 12;
		public const uint RC5 = 13;
		public const uint AES_128 = 14;
		public const uint AES_192 = 15;
		public const uint AES_256 = 16;
		public const uint AES = 17;
		// Fortezza sub-ids
		public const uint SKIPJACK = 10;
		public const uint TEK = 11;
		// RC2 sub-ids
		public const uint RC2 = 2;
		// Stream cipher sub-ids
		public const uint RC4 = 1;
		public const uint SEAL = 2;
		// Diffie-Hellman sub-ids
		public const uint DH_SANDF = 1;
		public const uint DH_EPHEM = 2;
		public const uint AGREED_KEY_ANY = 3;
		public const uint KEA = 4;
		// Hash sub ids
		public const uint MD2 = 1;
		public const uint MD4 = 2;
		public const uint MD5 = 3;
		public const uint SHA = 4;
		public const uint SHA1 = 4;
		public const uint MAC = 5;
		public const uint RIPEMD = 6;
		public const uint RIPEMD160 = 7;
		public const uint SSL3SHAMD5 = 8;
		public const uint HMAC = 9;
		public const uint TLS1PRF = 10;
		public const uint HASH_REPLACE_OWF = 11;
		public const uint SHA_256 = 12;
		public const uint SHA_384 = 13;
		public const uint SHA_512 = 14;
		// secure channel sub ids
		public const uint SSL3_MASTER = 1;
		public const uint SCHANNEL_MASTER_HASH = 2;
		public const uint SCHANNEL_MAC_KEY = 3;
		public const uint PCT1_MASTER = 4;
		public const uint SSL2_MASTER = 5;
		public const uint TLS1_MASTER = 6;
		public const uint SCHANNEL_ENC_KEY = 7;
		// Our silly example sub-id
		public const uint EXAMPLE = 80;
	}

	internal enum AlgClass : uint
	{
		// Algorithm classes
		ANY = (0),
		SIGNATURE = (1 << 13),
		MSG_ENCRYPT = (2 << 13),
		DATA_ENCRYPT = (3 << 13),
		HASH = (4 << 13),
		KEY_EXCHANGE = (5 << 13),
		ALL = (7 << 13),
	}

	internal enum AlgType : uint
	{
		// Algorithm types
		ANY = (0),
		DSS = (1 << 9),
		RSA = (2 << 9),
		BLOCK = (3 << 9),
		STREAM = (4 << 9),
		DH = (5 << 9),
		SECURECHANNEL = (6 << 9),
	}

	internal enum CipherMode : uint // KP_MODE
	{
		CBC = 1,       // Cipher block chaining
		ECB = 2,       // Electronic code book
		OFB = 3,       // Output feedback mode
		CFB = 4,       // Cipher feedback mode
		CTS = 5,       // Ciphertext stealing mode
		CBCI = 6,       // ANSI CBC Interleaved
		CFBP = 7,       // ANSI CFB Pipelined
		OFBP = 8,       // ANSI OFB Pipelined
		CBCOFM = 9,       // ANSI CBC + OF Masking
		CBCOFMI = 10,      // ANSI CBC + OFM Interleaved
	}

    internal class ProvName
	{
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
	}

	internal enum ProvParamEnum : uint 
	{
		// CryptGetProvParam
		ALGS = 1,
		CONTAINERS = 2,
		ALGS_EX = 22,
		MANDROOTS = 25,
		ELECTROOTS = 26,
		EX_SIGNING_PROT = 40,
	}

	internal enum ProvParamSet : uint
	{
		CLIENT_HWND = 1,
		CONTEXT_INFO = 11,
		KEYEXCHANGE_KEYSIZE = 12,
		SIGNATURE_KEYSIZE = 13,
		KEYEXCHANGE_ALG = 14,
		SIGNATURE_ALG = 15,
		DELETEKEY = 24,
	}

	internal enum ProvParam : uint 
	{
		// CryptGetProvParam
		IMPTYPE = 3,
		NAME = 4,
		VERSION = 5,
		CONTAINER = 6,
		CHANGE_PASSWORD = 7,
		KEYSET_SEC_DESCR = 8, // get/set security descriptor of keyset
		CERTCHAIN = 9, // for retrieving certificates from tokens
		KEY_TYPE_SUBTYPE = 10,
		PROVTYPE = 16,
		KEYSTORAGE = 17,
		APPLI_CERT = 18,
		SYM_KEYSIZE = 19,
		SESSION_KEYSIZE = 20,
		UI_PROMPT = 21,
		KEYSET_TYPE = 27,
		ADMIN_PIN = 31,
		KEYEXCHANGE_PIN = 32,
		SIGNATURE_PIN = 33,
		SIG_KEYSIZE_INC = 34,
		KEYX_KEYSIZE_INC = 35,
		UNIQUE_CONTAINER = 36,
		SGC_INFO = 37,
		USE_HARDWARE_RNG = 38,
		KEYSPEC = 39,
		CRYPT_COUNT_KEY_USE = 41,
	}

	[Flags]
	internal enum ContextFlag : uint
	{
		// dwFlags definitions for CryptAcquireContext
		NONE = 0,
		VERIFYCONTEXT = 0xF0000000,
		NEWKEYSET = 0x00000008,
		DELETEKEYSET = 0x00000010,
		MACHINE_KEYSET = 0x00000020,
		SILENT = 0x00000040,
	}

	[Flags]
	internal enum ProvDefaultFlag : uint
	{
		// dwFlag definitions for CryptSetProviderEx and CryptGetDefaultProvider
		MACHINE_DEFAULT = 0x00000001,
		USER_DEFAULT = 0x00000002,
		DELETE_DEFAULT = 0x00000004,
	}

	internal enum ProvType : uint
	{
		NONE = 0,
		RSA_FULL = 1,
		RSA_SIG = 2,
		DSS = 3,
		FORTEZZA = 4,
		MS_EXCHANGE = 5,
		SSL = 6,
		RSA_SCHANNEL = 12,
		DSS_DH = 13,
		EC_ECDSA_SIG = 14,
		EC_ECNRA_SIG = 15,
		EC_ECDSA_FULL = 16,
		EC_ECNRA_FULL = 17,
		DH_SCHANNEL = 18,
		SPYRUS_LYNKS = 20,
		RNG = 21,
		INTEL_SEC = 22,
		REPLACE_OWF = 23,
		RSA_AES = 24,
	}

	internal enum ErrCode : uint
	{
		//ERROR_
		SUCCESS					= 0,
		FILE_NOT_FOUND          = 2,
		INVALID_HANDLE			= 6,
		INVALID_PARAMETER		= 87,
		MORE_DATA				= 234,
		NO_MORE_ITEMS			= 259,
		MR_MID_NOT_FOUND		= 317,

		E_UNEXPECTED = 0x8000FFFF,
		E_NOTIMPL = 0x80004001,
		E_OUTOFMEMORY = 0x8007000E,
		E_INVALIDARG = 0x80070057,
		E_NOINTERFACE = 0x80004002,
		E_POINTER = 0x80004003,
		E_HANDLE = 0x80070006,
		E_ABORT = 0x80004004,
		E_FAIL = 0x80004005,
		E_ACCESSDENIED = 0x80070005,
		E_NOTIMPL_ = 0x80000001,
		E_OUTOFMEMORY_ = 0x80000002,
		E_INVALIDARG_ = 0x80000003,
		E_NOINTERFACE_ = 0x80000004,
		E_POINTER_ = 0x80000005,
		E_HANDLE_ = 0x80000006,
		E_ABORT_ = 0x80000007,
		E_FAIL_ = 0x80000008,
		E_ACCESSDENIED_ = 0x80000009,
		E_PENDING = 0x8000000A,

		CERT_E_CRITICAL = 0x800B0105,
		CERT_E_INVALID_NAME = 0x800B0114,
		CERT_E_INVALID_POLICY = 0x800B0113,
		CERT_E_ISSUERCHAINING = 0x800B0107,
		CERT_E_MALFORMED = 0x800B0108,
		CERT_E_PATHLENCONST = 0x800B0104,
		CERT_E_UNTRUSTEDCA = 0x800B0112,

		CRYPT_E_MSG_ERROR = 0x80091001,
		CRYPT_E_UNKNOWN_ALGO = 0x80091002,
		CRYPT_E_OID_FORMAT = 0x80091003,
		CRYPT_E_INVALID_MSG_TYPE = 0x80091004,
		CRYPT_E_UNEXPECTED_ENCODING = 0x80091005,
		CRYPT_E_AUTH_ATTR_MISSING = 0x80091006,
		CRYPT_E_HASH_VALUE = 0x80091007,
		CRYPT_E_INVALID_INDEX = 0x80091008,
		CRYPT_E_ALREADY_DECRYPTED = 0x80091009,
		CRYPT_E_NOT_DECRYPTED = 0x8009100A,
		CRYPT_E_RECIPIENT_NOT_FOUND = 0x8009100B,
		CRYPT_E_CONTROL_TYPE = 0x8009100C,
		CRYPT_E_ISSUER_SERIALNUMBER = 0x8009100D,
		CRYPT_E_SIGNER_NOT_FOUND = 0x8009100E,
		CRYPT_E_ATTRIBUTES_MISSING = 0x8009100F,
		CRYPT_E_STREAM_MSG_NOT_READY = 0x80091010,
		CRYPT_E_STREAM_INSUFFICIENT_DATA = 0x80091011,
		CRYPT_I_NEW_PROTECTION_REQUIRED = 0x00091012,
		CRYPT_E_BAD_LEN = 0x80092001,
		CRYPT_E_BAD_ENCODE = 0x80092002,
		CRYPT_E_FILE_ERROR = 0x80092003,
		CRYPT_E_NOT_FOUND = 0x80092004,
		CRYPT_E_EXISTS = 0x80092005,
		CRYPT_E_NO_PROVIDER = 0x80092006,
		CRYPT_E_SELF_SIGNED = 0x80092007,
		CRYPT_E_DELETED_PREV = 0x80092008,
		CRYPT_E_NO_MATCH = 0x80092009,
		CRYPT_E_UNEXPECTED_MSG_TYPE = 0x8009200A,
		CRYPT_E_NO_KEY_PROPERTY = 0x8009200B,
		CRYPT_E_NO_DECRYPT_CERT = 0x8009200C,
		CRYPT_E_BAD_MSG = 0x8009200D,
		CRYPT_E_NO_SIGNER = 0x8009200E,
		CRYPT_E_PENDING_CLOSE = 0x8009200F,
		CRYPT_E_REVOKED = 0x80092010,
		CRYPT_E_NO_REVOCATION_DLL = 0x80092011,
		CRYPT_E_NO_REVOCATION_CHECK = 0x80092012,
		CRYPT_E_REVOCATION_OFFLINE = 0x80092013,
		CRYPT_E_NOT_IN_REVOCATION_DATABASE = 0x80092014,
		CRYPT_E_INVALID_NUMERIC_STRING = 0x80092020,
		CRYPT_E_INVALID_PRINTABLE_STRING = 0x80092021,
		CRYPT_E_INVALID_IA5_STRING = 0x80092022,
		CRYPT_E_INVALID_X500_STRING = 0x80092023,
		CRYPT_E_NOT_CHAR_STRING = 0x80092024,
		CRYPT_E_FILERESIZED = 0x80092025,
		CRYPT_E_SECURITY_SETTINGS = 0x80092026,
		CRYPT_E_NO_VERIFY_USAGE_DLL = 0x80092027,
		CRYPT_E_NO_VERIFY_USAGE_CHECK = 0x80092028,
		CRYPT_E_VERIFY_USAGE_OFFLINE = 0x80092029,
		CRYPT_E_NOT_IN_CTL = 0x8009202A,
		CRYPT_E_NO_TRUSTED_SIGNER = 0x8009202B,
		CRYPT_E_MISSING_PUBKEY_PARA = 0x8009202C,
		CRYPT_E_OSS_ERROR = 0x80093000,
		CRYPT_E_ASN1_ERROR = 0x80093100,
		CRYPT_E_ASN1_INTERNAL = 0x80093101,
		CRYPT_E_ASN1_EOD = 0x80093102,
		CRYPT_E_ASN1_CORRUPT = 0x80093103,
		CRYPT_E_ASN1_LARGE = 0x80093104,
		CRYPT_E_ASN1_CONSTRAINT = 0x80093105,
		CRYPT_E_ASN1_MEMORY = 0x80093106,
		CRYPT_E_ASN1_OVERFLOW = 0x80093107,
		CRYPT_E_ASN1_BADPDU = 0x80093108,
		CRYPT_E_ASN1_BADARGS = 0x80093109,
		CRYPT_E_ASN1_BADREAL = 0x8009310A,
		CRYPT_E_ASN1_BADTAG = 0x8009310B,
		CRYPT_E_ASN1_CHOICE = 0x8009310C,
		CRYPT_E_ASN1_RULE = 0x8009310D,
		CRYPT_E_ASN1_UTF8 = 0x8009310E,
		CRYPT_E_ASN1_PDU_TYPE = 0x80093133,
		CRYPT_E_ASN1_NYI = 0x80093134,
		CRYPT_E_ASN1_EXTENDED = 0x80093201,
		CRYPT_E_ASN1_NOEOD = 0x80093202,

		NTE_BAD_UID = 2148073473, //&H80090001
		NTE_BAD_HASH = 2148073474, //&H80090002
		NTE_BAD_KEY = 2148073475, //&H80090003
		NTE_BAD_LEN = 2148073476, //&H80090004
		NTE_BAD_DATA = 2148073477, //&H80090005
		NTE_BAD_SIGNATURE = 2148073478, //&H80090006
		NTE_BAD_VER = 2148073479, //&H80090007
		NTE_BAD_ALGID = 2148073480, //&H80090008
		NTE_BAD_FLAGS = 2148073481, //&H80090009
		NTE_BAD_TYPE = 2148073482, //&H8009000A
		NTE_BAD_KEY_STATE = 2148073483, //&H8009000B
		NTE_BAD_HASH_STATE = 2148073484, //&H8009000C
		NTE_NO_KEY = 2148073485, //&H8009000D
		NTE_NO_MEMORY = 2148073486, //&H8009000E
		NTE_EXISTS = 2148073487, //&H8009000F
		NTE_PERM = 2148073488, //&H80090010
		NTE_NOT_FOUND = 2148073489, //&H80090011
		NTE_DOUBLE_ENCRYPT = 2148073490, //&H80090012
		NTE_BAD_PROVIDER = 2148073491, //&H80090013
		NTE_BAD_PROV_TYPE = 2148073492, //&H80090014
		NTE_BAD_PUBLIC_KEY = 2148073493, //&H80090015
		NTE_BAD_KEYSET = 2148073494, //&H80090016
		NTE_PROV_TYPE_NOT_DEF = 2148073495, //&H80090017
		NTE_PROV_TYPE_ENTRY_BAD = 2148073496, //&H80090018
		NTE_KEYSET_NOT_DEF = 2148073497, //&H80090019
		NTE_KEYSET_ENTRY_BAD = 2148073498, //&H8009001A
		NTE_PROV_TYPE_NO_MATCH = 2148073499, //&H8009001B
		NTE_SIGNATURE_FILE_BAD = 2148073500, //&H8009001C
		NTE_PROVIDER_DLL_FAIL = 2148073501, //&H8009001D
		NTE_PROV_DLL_NOT_FOUND = 2148073502, //&H8009001E
		NTE_BAD_KEYSET_PARAM = 2148073503, //&H8009001F
		NTE_FAIL = 2148073504, //&H80090020
		NTE_SYS_ERR = 2148073505, //&H80090021
		NTE_SILENT_CONTEXT = 2148073506, //&H80090022

		TRUST_E_BAD_DIGEST = 0x80096010,
		TRUST_E_BASIC_CONSTRAINTS = 0x80096019,
		TRUST_E_CERT_SIGNATURE = 0x80096004,
		TRUST_E_COUNTER_SIGNER = 0x80096003,
		TRUST_E_EXPLICIT_DISTRUST = 0x800B0111,
		TRUST_E_FINANCIAL_CRITERIA = 0x8009601E,
		TRUST_E_NO_SIGNER_CERT = 0x80096002,
		TRUST_E_SYSTEM_ERROR = 0x80096001,
		TRUST_E_TIME_STAMP = 0x80096005,
	}
}
