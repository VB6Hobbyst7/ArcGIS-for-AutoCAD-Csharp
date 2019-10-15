using AFA.Resources;
using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace AFA.UI
{
	public class btnIdentifyMap : RibbonButton
	{
		private class btn_CommandHandler : ICommand
		{
			public event EventHandler CanExecuteChanged;

			public void Execute(object param)
			{
				RibbonButton ribbonButton = param as RibbonButton;
				if (ribbonButton != null)
				{
					if (Application.DocumentManager.MdiActiveDocument == null)
					{
						return;
					}
					CmdLine.CancelActiveCommand();
					string text = "ESRI_IDENTIFYMAP ";
					Application.DocumentManager.MdiActiveDocument.SendStringToExecute(text, true, false, false);
				}
			}

			public bool CanExecute(object param)
			{
				return true;
			}
		}

		private string name = AfaStrings.RBN_BTN_IDENTIFYMAP;

		private string tooltipTitle = AfaStrings.RBN_BTN_IDENTIFYMAP_TOOLTIP_TITLE;

		private string tooltipDescr = AfaStrings.RBN_BTN_IDENTIFYMAP_TOOLTIP;

		private string smallImageName = "img_identifymap_small";

		private string largeImageName = "img_identifymap_large";

		private RibbonItemSize size =(RibbonItemSize)1;

		public btnIdentifyMap()
		{
			base.Id=(this.name);
			base.Name=(this.name);
			base.Text=(this.name);
			base.IsEnabled=(false);
			base.Image=(ArcGISRibbon.loadImage(this.smallImageName));
			base.LargeImage=(ArcGISRibbon.loadImage(this.largeImageName));
			base.Size=(RibbonItemSize)(1);
			base.ShowImage=(true);
			base.ShowText=(true);
			base.Orientation=(Orientation.Vertical);
			base.KeyTip=("This is a keytip");
			base.ToolTip=(ArcGISRibbon.createToolTip(this.tooltipTitle, this.tooltipDescr));
			base.CommandHandler=(new btnIdentifyMap.btn_CommandHandler());
			base.HelpSource=(ArcGISRibbon.HelpPath);
		}
	}
}
