//==========================================================================================
//
//		OpenNETCF.Windows.Forms.Dsa
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
	//ms-help://MS.VSCC.2003/MS.MSDNQTR.2003FEB.1033/security/security/dss_provider_key_blobs.htm
	//http://groups.google.com/groups?hl=en&lr=&ie=UTF-8&oe=UTF-8&threadm=eI4EjIeWCHA.1748%40tkmsftngp09&rnum=6&prev=/groups%3Fq%3Ddsa%2Bgroup:microsoft.public.platformsdk.security%26hl%3Den%26lr%3D%26ie%3DUTF-8%26oe%3DUTF-8%26group%3Dmicrosoft.public.platformsdk.security%26selm%3DeI4EjIeWCHA.1748%2540tkmsftngp09%26rnum%3D6
    internal class Dsa
	{
		public Dsa(){}

		/// <summary>
		/// rips apart rawKey into public byte [] for DsaParameters class
		/// </summary>
		public Dsa(byte [] rawKey)
		{
			this.rawKey = rawKey; //336 //444

			pks = new PUBLICKEYSTRUC();
			pks.bType = rawKey[0]; //7 //6
			pks.bVersion = rawKey[1]; //2
			pks.reserved = BitConverter.ToUInt16(rawKey, 2); //0
			pks.aiKeyAlg = BitConverter.ToUInt32(rawKey, 4); //8704

			kb = (KeyBlob) pks.bType; //private //public
			c = (Calg) pks.aiKeyAlg; //DSS_SIGN
			
			if(kb != KeyBlob.PUBLICKEYBLOB && kb != KeyBlob.PRIVATEKEYBLOB)
				throw new Exception("unsupported blob type");

			dpk = new DSSPUBKEY();
			//PRIV This must always be set to 0x32535344. the ASCII encoding of DSS2.
			//PUB This must always be set to 0x31535344, the ASCII encoding of DSS1.  
			dpk.magic = BitConverter.ToUInt32(rawKey, 8); //844321604 //827544388
			//Number of bits in the DSS key BLOB's prime, P. 
			dpk.bitlen = BitConverter.ToUInt32(rawKey, 12); //1024
			uint byteLen = dpk.bitlen / 8; //128

			this.SetSizeAndPosition(dpk.bitlen);

			bool revBytes = true;
			P = Format.GetBytes(this.rawKey, pPos, pLen, revBytes); 
			Q = Format.GetBytes(this.rawKey, qPos, qLen, revBytes); 
			G = Format.GetBytes(this.rawKey, gPos, gLen, revBytes); 
			if(kb == KeyBlob.PRIVATEKEYBLOB)
			{
				X = Format.GetBytes(this.rawKey, xyPos, xLen, revBytes);
				PgenCounter = Format.GetBytes(this.rawKey, xPgenCounterPos, pgenCounterLen, revBytes);
				Seed = Format.GetBytes(this.rawKey, xSeedPos, seedLen, revBytes); 
			}
			if(kb == KeyBlob.PUBLICKEYBLOB)
			{
				Y = Format.GetBytes(this.rawKey, xyPos, yLen, revBytes);
				PgenCounter = Format.GetBytes(this.rawKey, yPgenCounterPos, pgenCounterLen, revBytes);
				Seed = Format.GetBytes(this.rawKey, ySeedPos, seedLen, revBytes);
			}

			ds = new DSSSEED();
			byte [] baPcTemp = new byte[4];
			Buffer.BlockCopy(PgenCounter, 0, baPcTemp, 0, PgenCounter.Length);
			ds.counter = (uint) BitConverter.ToInt32(baPcTemp, 0);
			ds.seed = (byte[]) Seed.Clone();
			
			Int64 bij = 0;
			Int64 bip = BitConverter.ToInt64(P, 0);
			Int64 biq = BitConverter.ToInt64(Q, 0);

			bij = (bip - 1) / biq;
			byte [] baJ = BitConverter.GetBytes(bij);
			int len = baJ.Length;
			int rem = (len % 4); //seems to be 4 byte blocks for J?
			int pad = 0;
			if(rem != 0)
			{
				pad = 4 - rem;
				len = len + pad;
			}
			this.J = new byte[len];
			Array.Copy(baJ, 0, this.J, pad, baJ.Length);
		}

		/// <summary>
		/// returns public byte arrays in xml format
		/// </summary>
		public string ToXmlString(bool privateKey)
		{
			MemoryStream ms = new MemoryStream();
			XmlTextWriter xtw = new XmlTextWriter(ms, null);
			xtw.WriteStartElement("DSAKeyValue");
			xtw.WriteElementString("P", Format.GetB64(this.P));
			xtw.WriteElementString("Q", Format.GetB64(this.Q));
			xtw.WriteElementString("G", Format.GetB64(this.G));
			xtw.WriteElementString("Y", Format.GetB64(this.Y));
			if(this.J != null && this.J.Length > 0)
				xtw.WriteElementString("J", Format.GetB64(this.J));
			xtw.WriteElementString("Seed", Format.GetB64(this.Seed));
			xtw.WriteElementString("PgenCounter", Format.GetB64(this.PgenCounter));
			if(privateKey == true)
				xtw.WriteElementString("X", Format.GetB64(this.X));
			xtw.WriteEndElement();
			xtw.Flush();
			return Encoding.UTF8.GetString(ms.GetBuffer(), 0, (int)ms.Length);
		}

		/// <summary>
		/// builds up public byte arrays, and rawKey from xml
		/// </summary>
		public void FromXmlString(string dsaKeyValue)
		{
			bool privateKey = false;
			StringReader sr = new StringReader(dsaKeyValue);
			XmlTextReader xtr = new XmlTextReader(sr);
			xtr.WhitespaceHandling = WhitespaceHandling.None;
			while(xtr.Read())
			{
				if(xtr.NodeType == XmlNodeType.Element)
				{
					switch(xtr.LocalName)
					{
						case "P":
							this.P = Format.GetB64(xtr.ReadString());
							break;
						case "Q":
							this.Q = Format.GetB64(xtr.ReadString());
							break;
						case "G":
							this.G = Format.GetB64(xtr.ReadString());
							break;
						case "Y":
							this.Y = Format.GetB64(xtr.ReadString());
							break;
						case "J":
							this.J = Format.GetB64(xtr.ReadString());
							break;
						case "Seed":
							this.Seed = Format.GetB64(xtr.ReadString());
							break;
						case "PgenCounter":
							this.PgenCounter = Format.GetB64(xtr.ReadString());
							break;
						case "X":
							privateKey = true;
							this.X = Format.GetB64(xtr.ReadString());
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
			uint dsaMagic = 0;
			uint caSize = 0;
			uint bitLen = (uint) this.P.Length * 8;
			this.SetSizeAndPosition(bitLen);

			if(privateKey == true)
			{
				kb = KeyBlob.PRIVATEKEYBLOB;
				caSize = privByteLen; //336 
				dsaMagic = 0x32535344; //ASCII encoding of "DSS2"
			}
			else //public
			{
				kb = KeyBlob.PUBLICKEYBLOB;
				caSize = pubByteLen; //444
				dsaMagic = 0x31535344; //ASCII encoding of "DSS1"
			}

			rawKey = new byte[caSize];

			//PUBLICKEYSTRUC
			rawKey[0] = (byte) kb; //bType
			rawKey[1] = (byte) 2; //bVersion
			//reserved 2,3
			c = Calg.DSS_SIGN;
			byte [] baKeyAlg = BitConverter.GetBytes((uint)c); //aiKeyAlg
			Buffer.BlockCopy(baKeyAlg, 0, rawKey, 4, 4);

			pks = new PUBLICKEYSTRUC();
			pks.bType = rawKey[0];
			pks.bVersion = rawKey[1];
			pks.reserved = BitConverter.ToUInt16(rawKey, 2);
			pks.aiKeyAlg = BitConverter.ToUInt32(rawKey, 4);

			//DSSPUBKEY
			byte [] baMagic = BitConverter.GetBytes(dsaMagic);//magic
			Buffer.BlockCopy(baMagic, 0, rawKey, 8, 4);
			byte [] baBitlen = BitConverter.GetBytes(bitLen);//bitlen
			Buffer.BlockCopy(baBitlen, 0, rawKey, 12, 4);

			dpk = new DSSPUBKEY();
			dpk.magic = BitConverter.ToUInt32(rawKey, 8);
			dpk.bitlen = BitConverter.ToUInt32(rawKey, 12);
			uint byteLen = dpk.bitlen / 8;

			bool revBytes = true;
			Format.SetBytes(this.rawKey, pPos, pLen, this.P, revBytes);
			Format.SetBytes(this.rawKey, qPos, qLen, this.Q, revBytes);
			Format.SetBytes(this.rawKey, gPos, gLen, this.G, revBytes);
			if(privateKey == true)
			{
				Format.SetBytes(this.rawKey, xyPos, xLen, this.X, revBytes);
				Format.SetBytes(this.rawKey, xPgenCounterPos, pgenCounterLen, this.PgenCounter, revBytes);
				Format.SetBytes(this.rawKey, xSeedPos, seedLen, this.Seed, revBytes);
				//this.Y = null;
			}
			else //public
			{
				Format.SetBytes(this.rawKey, xyPos, yLen, this.Y, revBytes);
				Format.SetBytes(this.rawKey, yPgenCounterPos, pgenCounterLen, this.PgenCounter, revBytes);
				Format.SetBytes(this.rawKey, ySeedPos, seedLen, this.Seed, revBytes);
				this.X = null;
			}

			ds = new DSSSEED();
			byte [] baPcTemp = new byte[4];
			Buffer.BlockCopy(PgenCounter, 0, baPcTemp, 0, PgenCounter.Length);
			//Array.Reverse(baPcTemp, 0, baPcTemp.Length);
			ds.counter = (uint) BitConverter.ToInt32(baPcTemp, 0);
			ds.seed = (byte[]) Seed.Clone();

			//nowhere to put J
			//this.J = null;
		}

		public byte [] rawKey;
		public PUBLICKEYSTRUC pks;
		public DSSPUBKEY dpk;
		public KeyBlob kb;
		public Calg c;
		public DSSSEED ds;

		public byte [] P; //prime modulus
		public byte [] Q; //prime
		public byte [] G; //generator
		public byte [] Y; //public 
		public byte [] J; 
		public byte [] Seed; 
		public byte [] PgenCounter;
		public byte [] X; //secret exponent - private

		private uint pksLen;
		private uint dpkLen;
		private uint pLen;
		private uint qLen;
		private uint gLen;
		private uint xLen; //private
		private uint yLen; //public
		private uint pgenCounterLen;
		private uint seedLen;

		//private uint pksPos;
		private uint dpkPos;
		private uint pPos;
		private uint qPos;
		private uint gPos;
		private uint xyPos; //private & public
		private uint xPgenCounterPos;
		private uint yPgenCounterPos;
		private uint xSeedPos;
		private uint ySeedPos;

		private uint privByteLen;
		private uint pubByteLen;

		private void SetSizeAndPosition(uint bitLen)
		{
			//size = 8 8 128 20 128 (20 128) 4 20
			pksLen = 8;
			dpkLen = 8;
			pLen = bitLen / 8; //128
			qLen = 20;
			gLen = bitLen / 8; //128
			xLen = 20;
			yLen = bitLen / 8; //128
			pgenCounterLen = 4;
			seedLen = 20;

			privByteLen = pksLen + dpkLen + pLen + qLen + gLen + xLen + pgenCounterLen + seedLen;
			pubByteLen = pksLen + dpkLen + pLen + qLen + gLen + yLen + pgenCounterLen + seedLen;

			//1024 =  8 16 144 164 292 292 (312 420) (316 424)
			//pksPos = 0;
			dpkPos = pksLen; //8
			pPos = dpkPos + dpkLen; //16
			qPos = pPos + pLen; //144
			gPos = qPos + qLen; //164
			xyPos = gPos + gLen; //292
			//depends
			xPgenCounterPos = xyPos + xLen; //312
			yPgenCounterPos = xyPos + yLen; //420
			xSeedPos = xPgenCounterPos + pgenCounterLen; //316
			ySeedPos = yPgenCounterPos + pgenCounterLen; //424
		}
	}
}
