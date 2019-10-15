using System.Windows;

namespace AFAProto
{
    public static class ErrorReport
	{
		public static void ShowErrorMessage(string message)
		{
			MessageBox.Show(message, "ArcGIS for AutoCAD Error");
		}
	}
}
