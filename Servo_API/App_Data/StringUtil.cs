/*
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.

*/
using System;

namespace Servosms.Sysitem.Classes
{
	/// <summary>
	/// Summary description for StringUtil.
	/// </summary>
	/// 
	public class StringUtil
	{
		/// <summary>
		/// This method return the string with its first character upper and remaining lower e.g. bbNISYS ----> Bbnisys.
		/// </summary>
		public static string FirstCharUpper(string s)
		{ 
			string str = s;
			string fChar = "";
			string rStr  = "";     
			string strResult = "";
			if (str != "")
			{
				fChar = str.Substring(0,1);
				rStr  = str.Substring(1);
				strResult = fChar.ToUpper() + rStr.ToLower();
			}
			return strResult;     
		}

		/// <summary>
		/// This method return the string with its only first character upper and remaining lower e.g. bbNISYS Tech. ----> Bbnisys tech.
		/// </summary>
		public static string OnlyFirstCharUpper(string s)
		{ 
			string str = s;
			string fChar = "";
			string rStr  = "";     
			string strResult = "";

			if (str != "")
			{
				fChar = str.Substring(0,1);
				rStr  = str.Substring(1);
				strResult = fChar.ToUpper() + rStr;
			}
     
			return strResult;     
		}
		
		/// <summary>
		/// This method is used to get string(str) of the specified length(len).
		/// </summary>
		/// <param name="str"></param>
		/// <param name="len"></param>
		/// <returns></returns>
		public static string trimlength(string str,int len)
		{
			if(str.Length > len )
				return str.Substring(0,len);
			else
				return str;
		}
	}
}
