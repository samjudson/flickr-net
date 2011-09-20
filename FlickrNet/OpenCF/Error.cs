//==========================================================================================
//
//		OpenNETCF.Windows.Forms.Error
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
using System.Diagnostics;

namespace FlickrNet.Security.Cryptography.NativeMethods
{
    internal class Error
	{
		public Error() {}

		[DllImport("coredll.dll", EntryPoint="GetLastError", SetLastError=true)]
		private static extern uint GetLastErrorCe();
		[DllImport("kernel32.dll", EntryPoint="GetLastError", SetLastError=true)]
		private static extern uint GetLastErrorXp();
		public static uint GetLastError()
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return GetLastErrorCe();
			else
				return GetLastErrorXp();
		}

		public const uint FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000;

		[DllImport("coredll.dll", EntryPoint="FormatMessage", SetLastError=true)]
		private static extern uint FormatMessageCe(uint dwFlags, string lpSource, uint dwMessageId, 
			uint dwLanguageId, StringBuilder lpBuffer, uint nSize, string [] Arguments);
		[DllImport("kernel32.dll", EntryPoint="FormatMessage", SetLastError=true)]
		private static extern uint FormatMessageXp(uint dwFlags, string lpSource, uint dwMessageId, 
			uint dwLanguageId, StringBuilder lpBuffer, uint nSize, string [] Arguments);
		public static uint FormatMessage(uint dwFlags, string lpSource, uint dwMessageId, 
			uint dwLanguageId, StringBuilder lpBuffer, uint nSize, string [] Arguments)
		{
			if(System.Environment.OSVersion.Platform == PlatformID.WinCE)
				return FormatMessageCe(dwFlags, lpSource, dwMessageId, dwLanguageId, 
					lpBuffer, nSize, Arguments);
			else
				return FormatMessageXp(dwFlags, lpSource, dwMessageId, dwLanguageId, 
					lpBuffer, nSize, Arguments);
		}

		public static ErrCode HandleRetVal(bool retVal)
		{
			ErrCode [] eca = new ErrCode[0];
			return HandleRetVal(retVal, eca);
		}

		public static ErrCode HandleRetVal(bool retVal, ErrCode expected)
		{
			ErrCode [] eca = new ErrCode[1];
			eca[0] = expected;
			return HandleRetVal(retVal, eca);
		}

		public static ErrCode HandleRetVal(bool retVal, ErrCode [] expected)
		{
			ErrCode ec = ErrCode.SUCCESS;
			if(retVal == false)
			{
				uint lastErr = (uint) Marshal.GetLastWin32Error();
				ec = (ErrCode) lastErr;
				bool isExpected = false;
				foreach(ErrCode expect in expected)
				{
					if(ec == expect)
						isExpected = true;
				}
				if(isExpected == false)
					throw new Exception("bNb.Sec: " + ec.ToString());						
			}
			return ec;
		}
	}
}
