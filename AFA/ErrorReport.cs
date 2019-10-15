using System.Windows;

namespace AFA
{
    public static class ErrorReport
	{
		public static void ShowErrorMessage(string message)
		{
			MessageBox.Show(message, "ArcGIS for AutoCAD");
		}
	}
}
