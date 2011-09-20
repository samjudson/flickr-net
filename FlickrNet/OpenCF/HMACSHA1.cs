//==========================================================================================
//
//		OpenNETCF.Windows.Forms.HMACSHA1
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
using FlickrNet.Security.Cryptography.NativeMethods;

namespace FlickrNet.Security.Cryptography
{
    internal class HMACSHA1 : KeyedHashAlgorithm
	{
		public HMACSHA1()
		{
			key = FlickrNet.Security.Cryptography.NativeMethods.Rand.GetRandomBytes(64);
			/*
			//1st cut was wrong
			IntPtr prov = Flickr.Security.Cryptography.Context.AcquireContext();
			//3DES is WSE default?
			IntPtr ipKey = Flickr.Security.Cryptography.Key.GenKey(prov, Calg.TRIP_DES, GenKeyParam.EXPORTABLE);
			key = Flickr.Security.Cryptography.Key.ExportSessionKey(prov, ipKey, 24, true);
			//reversed above
			Flickr.Security.Cryptography.Key.DestroyKey(ipKey);
			Flickr.Security.Cryptography.Context.ReleaseContext(prov);
			*/
		}

		private byte [] rgbInner;
		private byte [] rgbOuter;
		//MONO
		private byte[] KeySetup (byte[] key, byte padding) 
		{
			byte[] buf = new byte [64];
			for (int i = 0; i < key.Length; ++i)
				buf [i] = (byte) ((byte) key [i] ^ padding);
			for (int i = key.Length; i < 64; ++i)
				buf [i] = padding;
			return buf;
		}

		public HMACSHA1(byte [] sessKey)
		{
			//if(sessKey.Length != 16)
			//	throw new Exception("only supports 16 byte RC2 key lengths");
			key = (byte []) sessKey.Clone();
		}

		private byte [] key = null;
		public override byte[] Key 
		{ 
			get{return key;}
			set{key = value;}
		}

		private byte [] hash = null;
		public override byte[] Hash 
		{ 
			get{return hash;}
		}

		public override int HashSize 
		{ 
			get{return 160;}
		}

		//http://groups.google.com/groups?q=calg_hmac&hl=en&lr=&ie=UTF-8&oe=UTF-8&selm=8bWw7.2452%241q2.225894%40news2-win.server.ntlworld.com&rnum=1
		public override byte [] ComputeHash(byte [] buffer)
		{
			byte [] tempBa = (byte []) buffer.Clone();
			rgbInner = KeySetup(key, 0x36);
			rgbOuter = KeySetup(key, 0x5C);

			IntPtr prov = FlickrNet.Security.Cryptography.NativeMethods.Context.AcquireContext();
			
			IntPtr hash1 = FlickrNet.Security.Cryptography.NativeMethods.Hash.CreateHash(prov, CalgHash.SHA1);
			FlickrNet.Security.Cryptography.NativeMethods.Hash.HashData(hash1, rgbInner);
			FlickrNet.Security.Cryptography.NativeMethods.Hash.HashData(hash1, tempBa);
			hash = FlickrNet.Security.Cryptography.NativeMethods.Hash.GetHashParam(hash1);
			FlickrNet.Security.Cryptography.NativeMethods.Hash.DestroyHash(hash1);

			IntPtr hash2 = FlickrNet.Security.Cryptography.NativeMethods.Hash.CreateHash(prov, CalgHash.SHA1);
			FlickrNet.Security.Cryptography.NativeMethods.Hash.HashData(hash2, rgbOuter);
			FlickrNet.Security.Cryptography.NativeMethods.Hash.HashData(hash2, hash);
			hash = FlickrNet.Security.Cryptography.NativeMethods.Hash.GetHashParam(hash2);
			FlickrNet.Security.Cryptography.NativeMethods.Hash.DestroyHash(hash2);
			
			FlickrNet.Security.Cryptography.NativeMethods.Context.ReleaseContext(prov);
			return hash;

			/*
			//1st cut was wrong
			IntPtr prov = Flickr.Security.Cryptography.Context.AcquireContext();
			byte [] baKey = (byte []) key.Clone();
			//reversed below
			IntPtr ipKey = IntPtr.Zero;
			if(baKey.Length == 8)
				ipKey = Flickr.Security.Cryptography.Key.ImportSessionKey(prov, Calg.DES, baKey, true);
			if(baKey.Length == 16)
				ipKey = Flickr.Security.Cryptography.Key.ImportSessionKey(prov, Calg.RC2, baKey, true);
			if(baKey.Length == 24)
				ipKey = Flickr.Security.Cryptography.Key.ImportSessionKey(prov, Calg.TRIP_DES, baKey, true);
			
			IntPtr hmacHash = Flickr.Security.Cryptography.Hash.CreateHash(prov, CalgHash.HMAC, ipKey);
			byte [] baHmacInfo = new byte[20]; //create new HMAC_Info byte[]
			byte [] algId = BitConverter.GetBytes((uint)CalgHash.SHA1); 
			Buffer.BlockCopy(algId, 0, baHmacInfo, 0, 4); //set HashAlgid
			Flickr.Security.Cryptography.Hash.SetHashParam(hmacHash, HashParam.HMAC_INFO, baHmacInfo);
			Flickr.Security.Cryptography.Hash.HashData(hmacHash, buffer);
			hash = Flickr.Security.Cryptography.Hash.GetHashParam(hmacHash);
			Flickr.Security.Cryptography.Hash.DestroyHash(hmacHash);
			
			Flickr.Security.Cryptography.Key.DestroyKey(ipKey);
			Flickr.Security.Cryptography.Context.ReleaseContext(prov);
			return hash;
			*/
		}
	}
}
