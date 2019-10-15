using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace AFA
{
	internal static class WindowExtensions
	{
		[DllImport("user32.dll")]
		internal static extern int SetWindowLong(IntPtr hwnd, int index, int value);

		[DllImport("user32.dll")]
		internal static extern int GetWindowLong(IntPtr hwnd, int index);

		internal static void HideMinimizeAndMaximizeButtons(this Window window)
		{
			IntPtr handle = new WindowInteropHelper(window).Handle;
			long num = (long)WindowExtensions.GetWindowLong(handle, -16);
			WindowExtensions.SetWindowLong(handle, -16, (int)(num & -131073L & -65537L));
		}
	}
}
