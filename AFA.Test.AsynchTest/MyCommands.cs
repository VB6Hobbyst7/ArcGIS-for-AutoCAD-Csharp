using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using System;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;
using Exception = Autodesk.AutoCAD.Runtime.Exception;

namespace AFA.Test.AsynchTest
{
    public class MyCommands
	{
		private static frmGoogleData _frm;

        //[CommandMethod("")]
        [CommandMethod("DoTasks", CommandFlags.Session)]

        public static void RunAsyncTasks()
		{
			Document mdiActiveDocument = Application.DocumentManager.MdiActiveDocument;
			if (mdiActiveDocument != null)
			{
				mdiActiveDocument.Editor.WriteMessage("\nStart tasks execution...\n");
			}
			try
			{
				AsyncTaskManager asyncTaskManager = new AsyncTaskManager();
				asyncTaskManager.TasksExecutionCompleted += new EventHandler(MyCommands.taskManager_TasksExecutionCompleted);
				asyncTaskManager.RunTasks();
			}
			catch (Exception ex)
			{
				if (mdiActiveDocument != null)
				{
					mdiActiveDocument.Editor.WriteMessage("\nInitializing process error:\n" + ex.Message + "\n");
				}
			}
		}

        //[CommandMethod]
        [CommandMethod("TaskData", CommandFlags.Session)]
        public static void ShowTaskData()
		{
			if (MyCommands._frm != null)
			{
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(Application.MainWindow.Handle, MyCommands._frm);
			}
		}

		private static void taskManager_TasksExecutionCompleted(object sender, EventArgs e)
		{
			AsyncTaskManager asyncTaskManager = sender as AsyncTaskManager;
			if (asyncTaskManager != null && asyncTaskManager.ResultData.Count > 0 && asyncTaskManager.ResultData.ContainsKey("Retrieve Google Map Data"))
			{
				MyCommands.ShowTaskDataInForm(asyncTaskManager.ResultData["Retrieve Google Map Data"]);
			}
		}

		private static void ShowTaskDataInForm(object taskResultData)
		{
			if (MyCommands._frm != null)
			{
				MyCommands._frm.Dispose();
			}
			MyCommands._frm = new frmGoogleData(taskResultData);
		}
	}
}
