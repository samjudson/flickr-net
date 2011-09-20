//==========================================================================================
//
//		OpenNETCF.Windows.Forms.Rsa
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
using System.Xml;
using System.IO;
using System.Text;

namespace FlickrNet.Security.Cryptography.NativeMethods
{
    internal class Rsa
	{
		public Rsa(){}

		/// <summary>
		/// rips apart rawKey into public byte [] for RsaParameters class
		/// </summary>
		public Rsa(byte [] rawKey)
		{
			this.rawKey = rawKey; //596

			pks = new PUBLICKEYSTRUC();
			pks.bType = rawKey[0]; //7
			pks.bVersion = rawKey[1]; //2
			pks.reserved = BitConverter.ToUInt16(rawKey, 2); //0
			pks.aiKeyAlg = BitConverter.ToUInt32(rawKey, 4); //41984

			kb = (KeyBlob) pks.bType; //PRIVATE
			c = (Calg) pks.aiKeyAlg; //RSA_KEYX
			
			if(kb != KeyBlob.PUBLICKEYBLOB && kb != KeyBlob.PRIVATEKEYBLOB)
				throw new Exception("unsupported blob type");

			rpk = new RSAPUBKEY();
			rpk.magic = BitConverter.ToUInt32(rawKey, 8); //843141970
			rpk.bitlen = BitConverter.ToUInt32(rawKey, 12); //1024
			rpk.pubexp = BitConverter.ToUInt32(rawKey, 16); //65537
			uint byteLen = rpk.bitlen / 8; //128

			this.SetSizeAndPosition(rpk.bitlen);

			//public
			Modulus = Format.GetBytes(this.rawKey, modulusPos, modulusLen, true);
			Exponent = Format.GetBytes(this.rawKey, exponentPos, exponentLen, true);
			//private
			if(kb == KeyBlob.PRIVATEKEYBLOB)
			{
				this.P = Format.GetBytes(this.rawKey, prime1Pos, prime1Len, true);
				this.Q = Format.GetBytes(this.rawKey, prime2Pos, prime2Len, true);
				this.DP = Format.GetBytes(this.rawKey, exponent1Pos, exponent1Len, true);
				this.DQ = Format.GetBytes(this.rawKey, exponent2Pos, exponent2Len, true);
				this.InverseQ = Format.GetBytes(this.rawKey, coefficientPos, coefficientLen, true);
				this.D = Format.GetBytes(this.rawKey, privateExponentPos, privateExponentLen, true);
			}
			else
			{
				this.P = null;
				this.Q = null;
				this.DP = null;
				this.DQ = null;
				this.InverseQ = null;
				this.D = null;
			}
		}

		/// <summary>
		/// returns public byte arrays in xml format
		/// </summary>
		public string ToXmlString(bool privateKey)
		{
			MemoryStream ms = new MemoryStream();
			XmlTextWriter xtw = new XmlTextWriter(ms, null);
			xtw.WriteStartElement("RSAKeyValue");
			xtw.WriteElementString("Modulus", Format.GetB64(this.Modulus));
			xtw.WriteElementString("Exponent", Format.GetB64(this.Exponent));
			if(privateKey == true)
			{
				xtw.WriteElementString("P", Format.GetB64(this.P));
				xtw.WriteElementString("Q", Format.GetB64(this.Q));
				xtw.WriteElementString("DP", Format.GetB64(this.DP));
				xtw.WriteElementString("DQ", Format.GetB64(this.DQ));
				xtw.WriteElementString("InverseQ", Format.GetB64(this.InverseQ));
				xtw.WriteElementString("D", Format.GetB64(this.D));
			}
			xtw.WriteEndElement();
			xtw.Flush();
			return Encoding.UTF8.GetString(ms.GetBuffer(), 0, (int)ms.Length);
		}

		/// <summary>
		/// builds up public byte arrays, and rawKey from xml
		/// </summary>
		public void FromXmlString(string rsaKeyValue)
		{
			bool privateKey = false;
			StringReader sr = new StringReader(rsaKeyValue);
			XmlTextReader xtr = new XmlTextReader(sr);
			xtr.WhitespaceHandling = WhitespaceHandling.None;
			while(xtr.Read())
			{
				if(xtr.NodeType == XmlNodeType.Element)
				{
					switch(xtr.LocalName)
					{
						case "Modulus":
							this.Modulus = Format.GetB64(xtr.ReadString());
							break;
						case "Exponent":
							this.Exponent = Format.GetB64(xtr.ReadString());
							break;
						case "P":
							this.P = Format.GetB64(xtr.ReadString());
							break;
						case "Q":
							this.Q = Format.GetB64(xtr.ReadString());
							break;
						case "DP":
							this.DP = Format.GetB64(xtr.ReadString());
							break;
						case "DQ":
							this.DQ = Format.GetB64(xtr.ReadString());
							break;
						case "InverseQ":
							this.InverseQ = Format.GetB64(xtr.ReadString());
							break;
						case "D":
							privateKey = true;
							this.D = Format.GetB64(xtr.ReadString());
							break;
						default:
							break;
					}
				} 
			} 
			BuildRawKey(privateKey);
		}

		public void BuildRawKey(bool privateKey)
		{
			//build up rawKey byte[]
			uint rsaMagic = 0;
			int caSize = 0;
			uint bitLen = (uint) this.Modulus.Length * 8;

			if(privateKey == true)
			{
				kb = KeyBlob.PRIVATEKEYBLOB;
				caSize = 20 + (9 * ((int)bitLen / 16));
				rsaMagic = 0x32415352; //ASCII encoding of "RSA2"
			}
			else //public
			{
				kb = KeyBlob.PUBLICKEYBLOB;
				caSize = 20 + ((int)bitLen / 8);
				rsaMagic = 0x31415352; //ASCII encoding of "RSA1"
			}

			rawKey = new byte[caSize];

			//PUBLICKEYSTRUC
			rawKey[0] = (byte) kb; //bType
			rawKey[1] = (byte) 2; //bVersion
			//reserved 2,3
			c = Calg.RSA_KEYX;
			byte [] baKeyAlg = BitConverter.GetBytes((uint)c);//aiKeyAlg
			Buffer.BlockCopy(baKeyAlg, 0, rawKey, 4, 4);

			pks = new PUBLICKEYSTRUC();
			pks.bType = rawKey[0];
			pks.bVersion = rawKey[1];
			pks.reserved = BitConverter.ToUInt16(rawKey, 2);
			pks.aiKeyAlg = BitConverter.ToUInt32(rawKey, 4);

			//RSAPUBKEY
			byte [] baMagic = BitConverter.GetBytes(rsaMagic);//magic
			Buffer.BlockCopy(baMagic, 0, rawKey, 8, 4);
			byte [] baBitlen = BitConverter.GetBytes(bitLen);//bitlen
			Buffer.BlockCopy(baBitlen, 0, rawKey, 12, 4);

			this.SetSizeAndPosition(bitLen);
			Format.SetBytes(this.rawKey, exponentPos, exponentLen, this.Exponent, true); //pubexp

			rpk = new RSAPUBKEY();
			rpk.magic = BitConverter.ToUInt32(rawKey, 8);
			rpk.bitlen = BitConverter.ToUInt32(rawKey, 12);
			rpk.pubexp = BitConverter.ToUInt32(rawKey, 16);
			uint byteLen = rpk.bitlen / 8;

			//public
			Format.SetBytes(this.rawKey, modulusPos, modulusLen, this.Modulus, true);
			Format.SetBytes(this.rawKey, exponentPos, exponentLen, this.Exponent, true);
			//private
			if(privateKey == true)
			{
				Format.SetBytes(this.rawKey, prime1Pos, prime1Len, this.P, true);
				Format.SetBytes(this.rawKey, prime2Pos, prime2Len, this.Q, true);
				Format.SetBytes(this.rawKey, exponent1Pos, exponent1Len, this.DP, true);
				Format.SetBytes(this.rawKey, exponent2Pos, exponent2Len, this.DQ, true);
				Format.SetBytes(this.rawKey, coefficientPos, coefficientLen, this.InverseQ, true);
				Format.SetBytes(this.rawKey, privateExponentPos, privateExponentLen, this.D, true);
			}
			else
			{
				this.P = null;
				this.Q = null;
				this.DP = null;
				this.DQ = null;
				this.InverseQ = null;
				this.D = null;
			}
		}

		/// <summary>
		/// used to extract session keys in the clear
		/// </summary>
		//http://support.microsoft.com/default.aspx?scid=http://support.microsoft.com:80/support/kb/articles/Q228/7/86.ASP&NoWebContent=1
		public void ExponentOfOne()
		{
			this.Exponent = new byte[exponentLen]; 
			this.Exponent[0] = 1;
			Format.SetBytes(this.rawKey, exponentPos, exponentLen, this.Exponent, false);
			this.DP = new byte[exponent1Len]; 
			this.DP[0] = 1;
			Format.SetBytes(this.rawKey, exponent1Pos, exponent1Len, this.DP, false);
			this.DQ = new byte[exponent2Len]; 
			this.DQ[0] = 1;
			Format.SetBytes(this.rawKey, exponent2Pos, exponent2Len, this.DQ, false);
			this.D = new byte[privateExponentLen]; 
			this.D[0] = 1;
			Format.SetBytes(this.rawKey, privateExponentPos, privateExponentLen, this.D, false);
		}

		public byte [] rawKey;
		public PUBLICKEYSTRUC pks;
		public RSAPUBKEY rpk;
		public KeyBlob kb;
		public Calg c;

		//public
		public byte [] Modulus; //n
		public byte [] Exponent; //e
		//private
		public byte [] P;
		public byte [] Q;
		public byte [] DP;
		public byte [] DQ;
		public byte [] InverseQ;
		public byte [] D;

		private uint pksLen;
		private uint rpkLen;
		private uint exponentLen;
		private uint modulusLen;
		private uint prime1Len;
		private uint prime2Len;
		private uint exponent1Len;
		private uint exponent2Len;
		private uint coefficientLen;
		private uint privateExponentLen;

		private uint rpkPos;
		private uint exponentPos;
		private uint modulusPos;
		private uint prime1Pos;
		private uint prime2Pos;
		private uint exponent1Pos;
		private uint exponent2Pos;
		private uint coefficientPos;
		private uint privateExponentPos;

		private uint privByteLen;
		private uint pubByteLen;

		private void SetSizeAndPosition(uint bitLen)
		{
			//size =  8 12 128  64  64  64  64  64 128 = 596!
			pksLen = 8;
			rpkLen = 12;
			exponentLen = 4;
			modulusLen = bitLen / 8; //128
			prime1Len = bitLen / 16; //64
			prime2Len = bitLen / 16; //64
			exponent1Len = bitLen / 16; //64
			exponent2Len = bitLen / 16; //64
			coefficientLen = bitLen / 16; //64
			privateExponentLen = bitLen / 8; //128

			privByteLen = pksLen + rpkLen + modulusLen + prime1Len + prime2Len + exponent1Len + exponent2Len + coefficientLen + privateExponentLen; //596
			pubByteLen = pksLen + rpkLen + modulusLen; //148

			//1024 =  16  20 148 212 276 340 404 468
			//uint pksPos = 0;
			rpkPos = pksLen; //8
			exponentPos = rpkPos + 8; //16
			modulusPos = rpkPos + rpkLen; //20
			prime1Pos = modulusPos + modulusLen; //148
			prime2Pos = prime1Pos + prime1Len; //212
			exponent1Pos = prime2Pos + prime2Len; //276
			exponent2Pos = exponent1Pos + exponent1Len; //340 
			coefficientPos = exponent2Pos + exponent2Len; //404
			privateExponentPos = coefficientPos + coefficientLen; //468
		}
	}
}
