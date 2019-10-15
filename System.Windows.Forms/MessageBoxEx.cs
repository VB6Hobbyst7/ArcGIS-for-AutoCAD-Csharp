using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms
{
    public class MessageBoxEx
	{
		public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

		public delegate void TimerProc(IntPtr hWnd, uint uMsg, UIntPtr nIDEvent, uint dwTime);

		public struct CWPRETSTRUCT
		{
			public IntPtr lResult;

			public IntPtr lParam;

			public IntPtr wParam;

			public uint message;

			public IntPtr hwnd;
		}

		public const int WH_CALLWNDPROCRET = 12;

		public const int WM_DESTROY = 2;

		public const int WM_INITDIALOG = 272;

		public const int WM_TIMER = 275;

		public const int WM_USER = 1024;

		public const int DM_GETDEFID = 1024;

		private const int TimerID = 42;

		private static MessageBoxEx.HookProc hookProc;

		private static MessageBoxEx.TimerProc hookTimer;

		private static uint hookTimeout;

		private static string hookCaption;

		private static IntPtr hHook;

		public static DialogResult Show(string text, uint uTimeout)
		{
			MessageBoxEx.Setup("", uTimeout);
			return MessageBox.Show(text);
		}

		public static DialogResult Show(string text, string caption, uint uTimeout)
		{
			MessageBoxEx.Setup(caption, uTimeout);
			return MessageBox.Show(text, caption);
		}

		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, uint uTimeout)
		{
			MessageBoxEx.Setup(caption, uTimeout);
			return MessageBox.Show(text, caption, buttons);
		}

		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, uint uTimeout)
		{
			MessageBoxEx.Setup(caption, uTimeout);
			return MessageBox.Show(text, caption, buttons, icon);
		}

		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defButton, uint uTimeout)
		{
			MessageBoxEx.Setup(caption, uTimeout);
			return MessageBox.Show(text, caption, buttons, icon, defButton);
		}

		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defButton, MessageBoxOptions options, uint uTimeout)
		{
			MessageBoxEx.Setup(caption, uTimeout);
			return MessageBox.Show(text, caption, buttons, icon, defButton, options);
		}

		public static DialogResult Show(IWin32Window owner, string text, uint uTimeout)
		{
			MessageBoxEx.Setup("", uTimeout);
			return MessageBox.Show(owner, text);
		}

		public static DialogResult Show(IWin32Window owner, string text, string caption, uint uTimeout)
		{
			MessageBoxEx.Setup(caption, uTimeout);
			return MessageBox.Show(owner, text, caption);
		}

		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, uint uTimeout)
		{
			MessageBoxEx.Setup(caption, uTimeout);
			return MessageBox.Show(owner, text, caption, buttons);
		}

		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, uint uTimeout)
		{
			MessageBoxEx.Setup(caption, uTimeout);
			return MessageBox.Show(owner, text, caption, buttons, icon);
		}

		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defButton, uint uTimeout)
		{
			MessageBoxEx.Setup(caption, uTimeout);
			return MessageBox.Show(owner, text, caption, buttons, icon, defButton);
		}

		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defButton, MessageBoxOptions options, uint uTimeout)
		{
			MessageBoxEx.Setup(caption, uTimeout);
			return MessageBox.Show(owner, text, caption, buttons, icon, defButton, options);
		}

		[DllImport("User32.dll")]
		public static extern UIntPtr SetTimer(IntPtr hWnd, UIntPtr nIDEvent, uint uElapse, MessageBoxEx.TimerProc lpTimerFunc);

		[DllImport("User32.dll")]
		public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll")]
		public static extern IntPtr SetWindowsHookEx(int idHook, MessageBoxEx.HookProc lpfn, IntPtr hInstance, int threadId);

		[DllImport("user32.dll")]
		public static extern int UnhookWindowsHookEx(IntPtr idHook);

		[DllImport("user32.dll")]
		public static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll")]
		public static extern int GetWindowTextLength(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int maxLength);

		[DllImport("user32.dll")]
		public static extern int EndDialog(IntPtr hDlg, IntPtr nResult);

		static MessageBoxEx()
		{
			MessageBoxEx.hookProc = new MessageBoxEx.HookProc(MessageBoxEx.MessageBoxHookProc);
			MessageBoxEx.hookTimer = new MessageBoxEx.TimerProc(MessageBoxEx.MessageBoxTimerProc);
			MessageBoxEx.hookTimeout = 0u;
			MessageBoxEx.hookCaption = null;
			MessageBoxEx.hHook = IntPtr.Zero;
		}

		private static void Setup(string caption, uint uTimeout)
		{
			if (MessageBoxEx.hHook != IntPtr.Zero)
			{
				throw new NotSupportedException("multiple calls are not supported");
			}
			MessageBoxEx.hookTimeout = uTimeout;
			MessageBoxEx.hookCaption = ((caption != null) ? caption : "");
			MessageBoxEx.hHook = MessageBoxEx.SetWindowsHookEx(12, MessageBoxEx.hookProc, IntPtr.Zero, AppDomain.GetCurrentThreadId());
		}

		private static IntPtr MessageBoxHookProc(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode < 0)
			{
				return MessageBoxEx.CallNextHookEx(MessageBoxEx.hHook, nCode, wParam, lParam);
			}
			MessageBoxEx.CWPRETSTRUCT cWPRETSTRUCT = (MessageBoxEx.CWPRETSTRUCT)Marshal.PtrToStructure(lParam, typeof(MessageBoxEx.CWPRETSTRUCT));
			IntPtr idHook = MessageBoxEx.hHook;
			if (MessageBoxEx.hookCaption != null && cWPRETSTRUCT.message == 272u)
			{
				int windowTextLength = MessageBoxEx.GetWindowTextLength(cWPRETSTRUCT.hwnd);
				StringBuilder stringBuilder = new StringBuilder(windowTextLength + 1);
				MessageBoxEx.GetWindowText(cWPRETSTRUCT.hwnd, stringBuilder, stringBuilder.Capacity);
				if (MessageBoxEx.hookCaption == stringBuilder.ToString())
				{
					MessageBoxEx.hookCaption = null;
					MessageBoxEx.SetTimer(cWPRETSTRUCT.hwnd, (UIntPtr)42u, MessageBoxEx.hookTimeout, MessageBoxEx.hookTimer);
					MessageBoxEx.UnhookWindowsHookEx(MessageBoxEx.hHook);
					MessageBoxEx.hHook = IntPtr.Zero;
				}
			}
			return MessageBoxEx.CallNextHookEx(idHook, nCode, wParam, lParam);
		}

		private static void MessageBoxTimerProc(IntPtr hWnd, uint uMsg, UIntPtr nIDEvent, uint dwTime)
		{
			if (nIDEvent == (UIntPtr)42u)
			{
				short value = (short)((int)MessageBoxEx.SendMessage(hWnd, 1024, IntPtr.Zero, IntPtr.Zero));
				MessageBoxEx.EndDialog(hWnd, (IntPtr)((int)value));
			}
		}
	}
}
