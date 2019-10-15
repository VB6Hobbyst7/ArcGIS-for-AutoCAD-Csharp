using AFA.Resources;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using System.Reflection;
using System.Runtime.InteropServices;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;

namespace AFA
{
    public static class CmdLine
	{
		[DllImport("acad.exe", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto, EntryPoint = "?acedPostCommand@@YAHPB_W@Z")]
		private static extern int acedPostCommand(string strExpr);

		public static void CancelActiveCommand()
		{
			if (!Application.IsQuiescent)
			{
				try
				{
					CmdLine.acedPostCommand("CANCELCMD");
				}
				catch
				{
				}
			}
		}

		public static void ExecuteQuietCommand(string cmdString)
		{
			try
			{
				if (!(Application.DocumentManager.MdiActiveDocument == null))
				{
					if (!cmdString.EndsWith(" "))
					{
						cmdString += " ";
					}
					Application.DocumentManager.MdiActiveDocument.SendStringToExecute(cmdString, true, false, false);
				}
			}
			catch
			{
			}
		}

		public static void InvokeCommand(Document doc, string cmdString)
		{
			object acadDocument = DocumentExtension.GetAcadDocument(doc);
			object[] args = new object[]
			{
				cmdString
			};
			acadDocument.GetType().InvokeMember("SendCommand", BindingFlags.InvokeMethod, null, acadDocument, args);
		}

		public static void DisplayCountMessage(Editor ed, int count)
		{
			ed.WriteMessage("\n" + AfaStrings.MSG_SELECT_FEATURESFOUND + string.Format(":  {0}", count));
		}
	}
}
