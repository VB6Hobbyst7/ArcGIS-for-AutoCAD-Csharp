using AFA.Resources;
using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace AFA.UI
{
	public class btnToggleVisibility : RibbonButton
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
					MSCRasterService activeRasterService = ArcGISRibbon.GetActiveRasterService();
					if (activeRasterService == null)
					{
						Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage(AfaStrings.NoCurrentMapServiceFound);
						return;
					}
					activeRasterService.ToggleVisiblity();
				}
			}

			public bool CanExecute(object param)
			{
				return true;
			}
		}

		private string name = AfaStrings.RBN_BTN_TOGGLEVISIBILITY;

		private string tooltipTitle = AfaStrings.RBN_BTN_TOGGLEVISIBILITY_TOOLTIP_TITLE;

		private string tooltipDescr = AfaStrings.RBN_BTN_TOGGLEVISIBILITY_TOOLTIP;

		private string smallImageName = "img_togglevisibility_small";

		private string largeImageName = "img_togglevisibility_large";

		private RibbonItemSize btnsize;

		private Orientation orientation;

		public btnToggleVisibility()
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
			base.Orientation=(Orientation.Horizontal);
			base.KeyTip=("This is a keytip");
			base.ToolTip=(ArcGISRibbon.createToolTip(this.tooltipTitle, this.tooltipDescr));
			base.CommandHandler=(new btnToggleVisibility.btn_CommandHandler());
			base.HelpSource=(ArcGISRibbon.HelpPath);
		}
	}
}
