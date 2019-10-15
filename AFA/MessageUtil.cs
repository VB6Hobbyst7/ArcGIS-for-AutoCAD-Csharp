using System.Windows;

namespace AFA
{
    public static class MessageUtil
	{
		public static MessageBoxResult ShowYesNoCancel(string message)
		{
			string caption = "ArcGIS for AutoCAD";
			return MessageBox.Show(message, caption, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
		}

		public static bool ShowYesNo(string message)
		{
			string caption = "ArcGIS for AutoCAD";
			MessageBoxResult messageBoxResult = MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
			return messageBoxResult == MessageBoxResult.Yes;
		}
	}
}
