using System.Threading;
using System.Windows.Forms;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;

namespace AFA
{
    public static class SplashScreenForm
	{
		public static void DisplaySplashScreenFor(int elapsedSeconds)
		{
			SplashScreen splashScreen = new SplashScreen();
			splashScreen.StartPosition = FormStartPosition.CenterScreen;
			splashScreen.FormBorderStyle = FormBorderStyle.None;
			splashScreen.TopMost = true;
			splashScreen.ShowInTaskbar = false;
			splashScreen.errMsg.Visible = false;
			splashScreen.UseWaitCursor = false;
			Application.ShowModelessDialog(Application.MainWindow.Handle, splashScreen, false);
			Thread.Sleep(elapsedSeconds * 1000);
			splashScreen.Close();
		}

		public static void DisplaySplashScreen()
		{
			Application.ShowModalDialog(new SplashScreen
			{
				Text = "ArcGIS for AutoCAD 300 SP1",
				StartPosition = FormStartPosition.CenterScreen,
				FormBorderStyle = FormBorderStyle.FixedSingle,
				TopMost = true,
				ShowInTaskbar = false,
				errMsg = 
				{
					Visible = false
				},
				UseWaitCursor = false,
				Cursor = Cursors.Default
			});
		}
	}
}
