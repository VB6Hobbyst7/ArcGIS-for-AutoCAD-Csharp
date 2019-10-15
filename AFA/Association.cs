using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AFA
{
	public class Association
	{
		[Flags]
		private enum AssocF
		{
			Init_NoRemapCLSID = 1,
			Init_ByExeName = 2,
			Open_ByExeName = 2,
			Init_DefaultToStar = 4,
			Init_DefaultToFolder = 8,
			NoUserSettings = 16,
			NoTruncate = 32,
			Verify = 64,
			RemapRunDll = 128,
			NoFixUps = 256,
			IgnoreBaseClass = 512
		}

		private enum AssocStr
		{
			Command = 1,
			Executable,
			FriendlyDocName,
			FriendlyAppName,
			NoOpen,
			ShellNewValue,
			DDECommand,
			DDEIfExec,
			DDEApplication,
			DDETopic
		}

		[DllImport("Shlwapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern uint AssocQueryString(Association.AssocF flags, Association.AssocStr str, string pszAssoc, string pszExtra, [Out] StringBuilder pszOut, [In] [Out] ref uint pcchOut);

		public static string FindAssoc(string ext)
		{
			uint capacity = 0u;
			Association.AssocQueryString(Association.AssocF.Verify, Association.AssocStr.Executable, ext, null, null, ref capacity);
			StringBuilder stringBuilder = new StringBuilder((int)capacity);
			Association.AssocQueryString(Association.AssocF.Verify, Association.AssocStr.Executable, ext, null, stringBuilder, ref capacity);
			return stringBuilder.ToString();
		}
	}
}
