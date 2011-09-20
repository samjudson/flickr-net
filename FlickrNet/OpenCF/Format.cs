//==========================================================================================
//
//		OpenNETCF.Windows.Forms.Format
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
    internal class Format
	{
		//used by Rsa and Dsa
		public static byte [] GetBytes (byte [] target, uint location, uint length, bool reverse)
		{
			byte[] baRev = new byte[length];
			Array.Copy(target, (int)location, baRev, 0, (int)length);
			if(reverse == true)
				Array.Reverse(baRev, 0, baRev.Length);
			if(baRev[0] == 0) //trim zeros
			{
				int zeroIter = 0;
				while(baRev[zeroIter] == 0)
					++zeroIter;
				byte[] baRevClone = (byte[])baRev.Clone();
				baRev = new byte[baRevClone.Length - zeroIter];
				Array.Copy(baRevClone, zeroIter, baRev, 0, baRev.Length);
			}
			return baRev;
		}

		//used by Rsa and Dsa
		public static void SetBytes (byte [] target, uint location, uint length, byte [] data, bool reverse)
		{
			byte [] tempData = (byte[]) data.Clone();
			if(reverse == true)
				Array.Reverse(tempData, 0, tempData.Length);
			Buffer.BlockCopy(tempData, 0, target, (int) location, tempData.Length);
			int padding = (int) length - (int) tempData.Length;
			for(uint i=0; i<padding; i++)
			{
				uint offset = location + (uint) tempData.Length + i;
				target[offset] = 0;
			}
		}

		public static byte [] GetBytes(string value)
		{
			return System.Text.Encoding.UTF8.GetBytes(value);
		}

		public static string GetString(byte [] value)
		{
			return System.Text.Encoding.UTF8.GetString(value, 0, value.Length);
		}
		
		public static string RevB64(byte [] value)
		{
			Array.Reverse(value, 0, value.Length);
			return Convert.ToBase64String(value, 0, value.Length);
		}

		public static byte [] RevB64(string value)
		{
			byte [] ba = Convert.FromBase64String(value);
			Array.Reverse(ba, 0, ba.Length);
			return ba;
		}

		public static string GetB64(byte [] value)
		{
			return Convert.ToBase64String(value, 0, value.Length);
		}
		
		public static byte [] GetB64(string value)
		{
			return Convert.FromBase64String(value);
		}

		public static string GetHexBin(byte [] inBuffer)
		{
			StringBuilder sb = new StringBuilder();
			for(int i=0; i<inBuffer.Length; i++)
			{
				sb = sb.Append(inBuffer[i].ToString("X2"));
			}
			return sb.ToString();
		}

		public static bool ZeroBytes(byte [] value)
		{
			for(int i=0; i<value.Length; i++)
			{
				if(value[i] != 0)
					return false;
			}
			return true; //all zero's
		}

		public static bool CompareBytes(byte [] one, byte [] two)
		{
			if(one.Length != two.Length)
				return false;
			for(int i=0; i<one.Length; i++)
			{
				if(one[i] != two[i])
					return false;
			}
			return true; //same
		}

		public static void HiddenBytes(byte [] plain, byte [] cipher)
		{
			//find all 1st matches
			byte startByte = plain[0];
			for(int i=0; i<cipher.Length; i++)
			{
				if(cipher[i] == startByte)
				{
					//forward
					bool match = true;
					int forIter = i;
					for(int j=0; j<plain.Length; j++)
					{
						if(forIter >= cipher.Length)
						{
							match = false;
							break;
						}
						if(cipher[forIter] != plain[j])
						{
							match = false;
							break;
						}
						forIter = forIter + 1;
					}
					if(match == true)
						throw new Exception("forward pass matched");
					//reverse
					match = true;
					int revIter = i;
					for(int j=0; j<plain.Length; j++)
					{
						if(revIter < 0)
						{
							match = false;
							break;
						}
						if(cipher[revIter] != plain[j])
						{
							match = false;
							break;
						}
						revIter = revIter - 1;
					}
					if(match == true)
						throw new Exception("reverse pass matched");
				}
			}
		}

		public static void SameBytes(byte [] one, byte [] two)
		{
			bool salted = false;
			if(one.Length != two.Length)
				throw new Exception("byte lengths are different");
			for(int i=0; i<one.Length; i++)
			{
				if(one[i] != two[i])
				{
					if(one[i]-1 == two[i] || one[i]+1 == two[i])
						salted = true;
					else
						throw new Exception("bytes do not match");
				}
			}
			if(salted == true)
				throw new Exception("bytes are salted");
		}
	}
}
