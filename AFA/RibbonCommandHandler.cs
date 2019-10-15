using AFA.Resources;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.Windows;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Input;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace AFA
{
    internal class RibbonCommandHandler : ICommand
	{
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object param)
		{
			return true;
		}

		private void CommandSelectCrossingWindow()
		{
			if (Application.DocumentManager.MdiActiveDocument == null)
			{
				return;
			}
			Document mdiActiveDocument = Application.DocumentManager.MdiActiveDocument;
			Editor editor = mdiActiveDocument.Editor;
			PromptPointOptions promptPointOptions = new PromptPointOptions(AfaStrings.MSG_SELECT_FIRSTCORNER);
			PromptPointResult point = editor.GetPoint(promptPointOptions);
			if (point.Status != (PromptStatus)5100)
			{
				editor.WriteMessage(AfaStrings.MSG_SELECTCORNER_ERROR);
				return;
			}
			PromptCornerOptions promptCornerOptions = new PromptCornerOptions(AfaStrings.MSG_SELECT_SECONDCORNER, point.Value);
			PromptPointResult corner = editor.GetCorner(promptCornerOptions);
			if (corner.Status != (PromptStatus)5100)
			{
				editor.WriteMessage(AfaStrings.MSG_SELECTCORNER_ERROR);
				return;
			}
			PromptSelectionResult promptSelectionResult = editor.SelectCrossingWindow(point.Value, corner.Value);
			if (promptSelectionResult.Status == (PromptStatus)( -5002))
			{
				editor.WriteMessage(AfaStrings.CommandCancelled);
				return;
			}
			PromptStatus arg_C3_0 = promptSelectionResult.Status;
		}

		public void Execute(object param)
		{
			RibbonButton ribbonButton = param as RibbonButton;
			if (ribbonButton != null)
			{
				if (ribbonButton.Id == AfaStrings.RBN_BTN_ESRIMAPS)
				{
					try
					{
						if (Application.IsQuiescent)
						{
							CmdLine.CancelActiveCommand();
							string cmdString = "_ESRI_AddESRIMap ";
							CmdLine.ExecuteQuietCommand(cmdString);
						}
						return;
					}
					catch (SystemException ex)
					{
						MessageBox.Show(ex.Message);
						return;
					}
				}
				if (ribbonButton.Id == AfaStrings.RBN_BTN_ADDMAPSERVICE)
				{
					CmdLine.CancelActiveCommand();
					string cmdString2 = "_ESRI_AddService ";
					CmdLine.ExecuteQuietCommand(cmdString2);
				}
				else
				{
					if (ribbonButton.Id == AfaStrings.RBN_BTN_TOC)
					{
						CmdLine.CancelActiveCommand();
						string cmdString3 = "ESRI_SHOWTOC ";
						CmdLine.ExecuteQuietCommand(cmdString3);
						return;
					}
					if (ribbonButton.Id == AfaStrings.RBN_BTN_LAYER_MANAGER)
					{
						CmdLine.CancelActiveCommand();
						string cmdString4 = ".LAYER ";
						CmdLine.ExecuteQuietCommand(cmdString4);
						return;
					}
					if (ribbonButton.Id == AfaStrings.RBN_BTN_ASSIGN_COORDSYS)
					{
						CmdLine.CancelActiveCommand();
						string cmdString5 = ".ESRI_ASSIGNCS ";
						CmdLine.ExecuteQuietCommand(cmdString5);
						return;
					}
					if (ribbonButton.Id == AfaStrings.RBN_BTN_LIST_COORDSYS)
					{
						CmdLine.CancelActiveCommand();
						string cmdString6 = ".ESRI_LISTCS ";
						CmdLine.ExecuteQuietCommand(cmdString6);
						return;
					}
					if (ribbonButton.Id == AfaStrings.RBN_BTN_REMOVE_COORDSYS)
					{
						CmdLine.CancelActiveCommand();
						string cmdString7 = ".esri_RemoveCS ";
						CmdLine.ExecuteQuietCommand(cmdString7);
						return;
					}
					if (ribbonButton.Id == AfaStrings.RBN_BTN_ZOOMEXTENTS)
					{
						CmdLine.CancelActiveCommand();
						string cmdString8 = ".ESRI_ZOOMEXTENTS ";
						CmdLine.ExecuteQuietCommand(cmdString8);
						return;
					}
					if (ribbonButton.Id == AfaStrings.RBN_BTN_ZOOMPREVIOUS)
					{
						CmdLine.CancelActiveCommand();
						string cmdString9 = "'_ZOOM _PREVIOUS ";
						CmdLine.ExecuteQuietCommand(cmdString9);
						return;
					}
					if (ribbonButton.Id == AfaStrings.RBN_BTN_ZOOMWINDOW)
					{
						CmdLine.CancelActiveCommand();
						string cmdString10 = "'_ZOOM _WINDOW ";
						CmdLine.ExecuteQuietCommand(cmdString10);
						return;
					}
					if (ribbonButton.Id == AfaStrings.RBN_BTN_RESOURCECENTER)
					{
						Process.Start(AfaStrings.URI_RESOURCECENTER);
						return;
					}
					if (ribbonButton.Id == AfaStrings.RBN_BTN_HELP)
					{
						CmdLine.CancelActiveCommand();
						string cmdString11 = "ESRI_ArcGISHELP ";
						CmdLine.ExecuteQuietCommand(cmdString11);
						return;
					}
					if (ribbonButton.Id == AfaStrings.RBN_BTN_ABOUT)
					{
						CmdLine.CancelActiveCommand();
						string cmdString12 = "ESRI_AboutArcGIS ";
						CmdLine.ExecuteQuietCommand(cmdString12);
						return;
					}
					if (ribbonButton.Id == "Command")
					{
						CmdLine.CancelActiveCommand();
						string cmdString13 = (string)ribbonButton.CommandParameter;
						CmdLine.ExecuteQuietCommand(cmdString13);
						return;
					}
					string message = "Placeholder for " + ribbonButton.Id;
					ErrorReport.ShowErrorMessage(message);
					return;
				}
				return;
			}
		}
	}
}
