using AFA.Resources;
using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace AFA.UI
{
	public class btnRemoveMap : RibbonButton
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
					MSCRasterService activeRasterService = ArcGISRibbon.GetActiveRasterService();
					if (activeRasterService == null)
					{
						Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage(AfaStrings.NoCurrentMapServiceFound);
						return;
					}
					if (activeRasterService.GetType() == typeof(MSCMapService))
					{
						MSCMapService mSCMapService = (MSCMapService)activeRasterService;
						mSCMapService.DeleteService();
						return;
					}
					if (activeRasterService.GetType() == typeof(MSCImageService))
					{
						MSCImageService mSCImageService = (MSCImageService)activeRasterService;
						mSCImageService.DeleteService();
						return;
					}
					Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage(AfaStrings.NoCurrentMapServiceFound);
				}
			}

			public bool CanExecute(object param)
			{
				return true;
			}
		}

		private string name = AfaStrings.RBN_BTN_REMOVEMAP;

		private string tooltipTitle = AfaStrings.RBN_BTN_REMOVEMAP_TOOLTIP_TITLE;

		private string tooltipDescr = AfaStrings.RBN_BTN_REMOVEMAP_TOOLTIP;

		private string smallImageName = "img_removemap_small";

		private string largeImageName = "img_removemap_large";

		private RibbonItemSize btnsize;

		public btnRemoveMap()
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
			base.CommandHandler=(new btnRemoveMap.btn_CommandHandler());
			base.HelpSource=(ArcGISRibbon.HelpPath);
		}
	}
}
