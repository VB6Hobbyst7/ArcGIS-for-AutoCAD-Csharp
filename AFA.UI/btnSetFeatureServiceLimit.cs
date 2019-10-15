using AFA.Resources;
using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace AFA.UI
{
	public class btnSetFeatureServiceLimit : RibbonButton
	{
		private class btn_CommandHandler : ICommand
		{
			public event EventHandler CanExecuteChanged;

			public void Execute(object param)
			{
				if (Application.DocumentManager.MdiActiveDocument == null)
				{
					return;
				}
				RibbonButton ribbonButton = param as RibbonButton;
				if (ribbonButton != null)
				{
					CmdLine.CancelActiveCommand();
					string text = ".ESRI_SetFeatureServiceLimit ";
					Application.DocumentManager.MdiActiveDocument.SendStringToExecute(text, true, false, false);
				}
			}

			public bool CanExecute(object param)
			{
				return true;
			}
		}

		private string name = AfaStrings.RBN_BTN_SETSERVICELIMITCURRENTVIEW;

		private string tooltipTitle = AfaStrings.RBN_BTN_SETSERVICELIMITCURRENTVIEW_TOOLTIP_TITLE;

		private string tooltipDescr = AfaStrings.RBN_BTN_SETSERVICELIMITCURRENTVIEW_TOOLTIP;

		private string smallImageName = "img_setservicelimitcurrentview_small";

		private string largeImageName = "img_setservicelimitcurrentview_large";

		private RibbonItemSize btnsize;

		public btnSetFeatureServiceLimit()
		{
			base.Id=(this.name);
			base.Name=(this.name);
			base.Text=(this.name);
			base.IsEnabled=(false);
			base.Image=(ArcGISRibbon.loadImage(this.smallImageName));
			base.LargeImage=(ArcGISRibbon.loadImage(this.largeImageName));
			base.Size=(this.btnsize);
			base.ShowImage=(true);
			base.ShowText=(false);
			base.Orientation=(Orientation.Vertical);
			base.KeyTip=("This is a keytip");
			base.ToolTip=(ArcGISRibbon.createToolTip(this.tooltipTitle, this.tooltipDescr));
			base.CommandHandler=(new btnSetFeatureServiceLimit.btn_CommandHandler());
			base.HelpSource=(ArcGISRibbon.HelpPath);
		}
	}
}
