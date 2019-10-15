using Autodesk.AutoCAD.ApplicationServices.Core;
using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace AFA.UI
{
	internal class PromptForCredentials
	{
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct CREDUI_INFO
		{
			public int cbSize;

			public IntPtr hwndParent;

			public string pszMessageText;

			public string pszCaptionText;

			public IntPtr hbmBanner;
		}

		private enum PromptForWindowsCredentialsFlags
		{
			CREDUIWIN_GENERIC = 1,
			CREDUIWIN_CHECKBOX,
			CREDUIWIN_AUTHPACKAGE_ONLY = 16,
			CREDUIWIN_IN_CRED_ONLY = 32,
			CREDUIWIN_ENUMERATE_ADMINS = 256,
			CREDUIWIN_ENUMERATE_CURRENT_USER = 512,
			CREDUIWIN_SECURE_PROMPT = 4096,
			CREDUIWIN_PACK_32_WOW = 268435456
		}

		[Flags]
		private enum CREDUI_FLAGS
		{
			INCORRECT_PASSWORD = 1,
			DO_NOT_PERSIST = 2,
			REQUEST_ADMINISTRATOR = 4,
			EXCLUDE_CERTIFICATES = 8,
			REQUIRE_CERTIFICATE = 16,
			SHOW_SAVE_CHECK_BOX = 64,
			ALWAYS_SHOW_UI = 128,
			REQUIRE_SMARTCARD = 256,
			PASSWORD_ONLY_OK = 512,
			VALIDATE_USERNAME = 1024,
			COMPLETE_USERNAME = 2048,
			PERSIST = 4096,
			SERVER_CREDENTIAL = 16384,
			EXPECT_CONFIRMATION = 131072,
			GENERIC_CREDENTIALS = 262144,
			USERNAME_TARGET_CREDENTIALS = 524288,
			KEEP_USERNAME = 1048576
		}

		public enum CredUIReturnCodes
		{
			NO_ERROR,
			ERROR_CANCELLED = 1223,
			ERROR_NO_SUCH_LOGON_SESSION = 1312,
			ERROR_NOT_FOUND = 1168,
			ERROR_INVALID_ACCOUNT_NAME = 1315,
			ERROR_INSUFFICIENT_BUFFER = 122,
			ERROR_INVALID_PARAMETER = 87,
			ERROR_INVALID_FLAGS = 1004
		}

		[DllImport("ole32.dll")]
		public static extern void CoTaskMemFree(IntPtr ptr);

		[DllImport("credui.dll", CharSet = CharSet.Unicode)]
		private static extern uint CredUIPromptForWindowsCredentials(ref PromptForCredentials.CREDUI_INFO notUsedHere, int authError, ref uint authPackage, IntPtr InAuthBuffer, uint InAuthBufferSize, out IntPtr refOutAuthBuffer, out uint refOutAuthBufferSize, ref bool fSave, PromptForCredentials.PromptForWindowsCredentialsFlags flags);

		[DllImport("credui.dll", CharSet = CharSet.Auto)]
		private static extern bool CredUnPackAuthenticationBuffer(int dwFlags, IntPtr pAuthBuffer, uint cbAuthBuffer, StringBuilder pszUserName, ref int pcchMaxUserName, StringBuilder pszDomainName, ref int pcchMaxDomainame, StringBuilder pszPassword, ref int pcchMaxPassword);

		[DllImport("credui")]
		private static extern PromptForCredentials.CredUIReturnCodes CredUIPromptForCredentials(ref PromptForCredentials.CREDUI_INFO creditUR, string targetName, IntPtr reserved1, int iError, StringBuilder userName, int maxUserName, StringBuilder password, int maxPassword, [MarshalAs(UnmanagedType.Bool)] ref bool pfSave, PromptForCredentials.CREDUI_FLAGS flags);

		[DllImport("credui.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool CredPackAuthenticationBuffer(int dwFlags, string pszUserName, string pszPassword, IntPtr pPackedCredentials, ref int pcbPackedCredentials);

		private static bool IsWinVistaOrHigher()
		{
			OperatingSystem oSVersion = Environment.OSVersion;
			return oSVersion.Version.Major >= 6;
		}

		public static bool GetCredentials(string captionText, string messageText, string userName, out NetworkCredential networkCredential)
		{
			if (PromptForCredentials.IsWinVistaOrHigher())
			{
				return PromptForCredentials.GetCredentialsNewStyle(captionText, messageText, userName, out networkCredential);
			}
			return PromptForCredentials.GetCredentialsOldSchool(captionText, messageText, userName, out networkCredential);
		}

		private static bool GetCredentialsOldSchool(string captionText, string messageText, string userName, out NetworkCredential networkCredential)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder(userName);
			PromptForCredentials.CREDUI_INFO cREDUI_INFO = default(PromptForCredentials.CREDUI_INFO);
			cREDUI_INFO.cbSize = Marshal.SizeOf(cREDUI_INFO);
			bool flag = false;
			cREDUI_INFO.pszCaptionText = captionText;
			PromptForCredentials.CREDUI_FLAGS flags = PromptForCredentials.CREDUI_FLAGS.ALWAYS_SHOW_UI | PromptForCredentials.CREDUI_FLAGS.GENERIC_CREDENTIALS;
			if (PromptForCredentials.CredUIPromptForCredentials(ref cREDUI_INFO, messageText, IntPtr.Zero, 0, stringBuilder2, 100, stringBuilder, 100, ref flag, flags) == PromptForCredentials.CredUIReturnCodes.NO_ERROR)
			{
				string userName2 = stringBuilder2.ToString();
				string password = stringBuilder.ToString();
				networkCredential = new NetworkCredential(userName2, password);
				return true;
			}
			networkCredential = null;
			return false;
		}

		private static bool GetCredentialsNewStyle(string captionText, string messageText, string userName, out NetworkCredential networkCredential)
		{
			PromptForCredentials.CREDUI_INFO cREDUI_INFO = default(PromptForCredentials.CREDUI_INFO);
			cREDUI_INFO.pszCaptionText = captionText;
			cREDUI_INFO.pszMessageText = messageText;
			cREDUI_INFO.hwndParent = Application.MainWindow.Handle;
			string empty = string.Empty;
			int num = 0;
			IntPtr intPtr = IntPtr.Zero;
			PromptForCredentials.CredPackAuthenticationBuffer(0, userName, empty, IntPtr.Zero, ref num);
			if (num > 0)
			{
				intPtr = Marshal.AllocCoTaskMem(num);
				PromptForCredentials.CredPackAuthenticationBuffer(0, userName, empty, intPtr, ref num);
			}
			cREDUI_INFO.cbSize = Marshal.SizeOf(cREDUI_INFO);
			uint num2 = 0u;
			IntPtr intPtr2 = (IntPtr)0;
			bool flag = false;
			uint cbAuthBuffer;
			uint num3 = PromptForCredentials.CredUIPromptForWindowsCredentials(ref cREDUI_INFO, 0, ref num2, intPtr, 1024u, out intPtr2, out cbAuthBuffer, ref flag, PromptForCredentials.PromptForWindowsCredentialsFlags.CREDUIWIN_GENERIC);
			if (num3 != 0u)
			{
				networkCredential = null;
				return false;
			}
			StringBuilder stringBuilder = new StringBuilder(100);
			StringBuilder stringBuilder2 = new StringBuilder(100);
			StringBuilder stringBuilder3 = new StringBuilder(100);
			int num4 = 100;
			int num5 = 100;
			int num6 = 100;
			if (num3 == 0u && PromptForCredentials.CredUnPackAuthenticationBuffer(0, intPtr2, cbAuthBuffer, stringBuilder, ref num4, stringBuilder3, ref num5, stringBuilder2, ref num6))
			{
				PromptForCredentials.CoTaskMemFree(intPtr2);
				networkCredential = new NetworkCredential
				{
					UserName = stringBuilder.ToString(),
					Password = stringBuilder2.ToString(),
					Domain = stringBuilder3.ToString()
				};
				return true;
			}
			networkCredential = null;
			return false;
		}

		private static bool ShowPrompt()
		{
			bool flag = false;
			int authError = 0;
			uint num = 0u;
			PromptForCredentials.CREDUI_INFO cREDUI_INFO = default(PromptForCredentials.CREDUI_INFO);
			cREDUI_INFO.cbSize = Marshal.SizeOf(cREDUI_INFO);
			cREDUI_INFO.pszCaptionText = "Connect to your application";
			cREDUI_INFO.pszMessageText = "Enter your credentials!";
			cREDUI_INFO.hwndParent = Application.MainWindow.Handle;
			while (true)
			{
				IntPtr intPtr;
				uint num3;
				uint num2 = PromptForCredentials.CredUIPromptForWindowsCredentials(ref cREDUI_INFO, authError, ref num, (IntPtr)0, 0u, out intPtr, out num3, ref flag, (PromptForCredentials.PromptForWindowsCredentialsFlags)0);
				if (num2 != 0u)
				{
					break;
				}
				bool flag2 = false;
				if (flag2)
				{
					return true;
				}
				authError = 1326;
			}
			return false;
		}
	}
}
