using AFA.Resources;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using System.Diagnostics;
using System.IO;

namespace AFA
{
    public class DatasetLog
	{
		private static string logFilePath;

		public static void WriteString(Document doc, string s)
		{
			try
			{
				DatasetLog.logFilePath = Path.ChangeExtension(doc.Database.Filename, "agslog");
				using (StreamWriter streamWriter = File.AppendText(DatasetLog.logFilePath))
				{
					DatasetLog.Log(s, streamWriter);
					streamWriter.Close();
				}
			}
			catch
			{
			}
		}

		private static void Log(string logMessage, TextWriter w)
		{
			w.WriteLine(logMessage);
			w.Flush();
		}

		public static string ReadAll(Document doc)
		{
			string text = "";
			string result;
			try
			{
				DatasetLog.logFilePath = Path.ChangeExtension(doc.Database.Filename, "agslog");
				using (StreamReader streamReader = File.OpenText(DatasetLog.logFilePath))
				{
					text = streamReader.ReadToEnd();
					streamReader.Close();
				}
				result = text;
			}
			catch
			{
				result = text;
			}
			return result;
		}

		public static void ShowLog(Document doc)
		{
			if (doc == null)
			{
				return;
			}
			DatasetLog.logFilePath = Path.ChangeExtension(doc.Database.Filename, "agslog");
			Editor editor = doc.Editor;
			string text = Association.FindAssoc(".txt");
			if (text == null)
			{
				editor.WriteMessage(AfaStrings.Error);
				return;
			}
			if (!File.Exists(DatasetLog.logFilePath))
			{
				editor.WriteMessage(AfaStrings.NoLogFileFound);
				return;
			}
			Process.Start(text, DatasetLog.logFilePath);
		}
	}
}
