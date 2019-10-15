using AFA;
using AFA.Resources;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;

namespace ESRI_TableViewer
{
    public class Class1
	{
		[CommandMethod("ESRI_Table")]
		public void TabView()
		{
			Editor editor = AfaDocData.ActiveDocData.DocDataset.ParentDocument.Editor;
			try
			{
				try
				{
					int arg_25_0 = editor.SelectAll().Value.Count;
				}
				catch
				{
					editor.WriteMessage("\n" + AfaStrings.NoFeaturesFound);
					return;
				}
				MSCFeatureClass activeFeatureClassOrSubtype = AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype();
				if (activeFeatureClassOrSubtype == null)
				{
					editor.WriteMessage("\n" + AfaStrings.NoCurrentFeatureClassFound);
				}
				else
				{
					TableView tableView = new TableView(activeFeatureClassOrSubtype);
					Application.ShowModalDialog(Application.MainWindow.Handle, tableView, true);
					tableView.Uninitialize();
					tableView.Dispose();
				}
			}
			catch
			{
				editor.WriteMessage("\nError");
			}
		}
	}
}
