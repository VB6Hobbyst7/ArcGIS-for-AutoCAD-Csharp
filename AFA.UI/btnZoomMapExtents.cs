using AFA.Resources;
using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace AFA.UI
{
	public class btnZoomMapExtents : RibbonButton
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
					Editor arg_31_0 = AfaDocData.ActiveDocData.DocDataset.ParentDocument.Editor;
					MSCRasterService activeRasterService = ArcGISRibbon.GetActiveRasterService();
					if (activeRasterService == null)
					{
						Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage(AfaStrings.NoCurrentMapServiceFound);
						return;
					}
					activeRasterService.ZoomExtents();
				}
			}

			public bool CanExecute(object param)
			{
				return Application.DocumentManager.MdiActiveDocument != null;
			}
		}

		private string name = AfaStrings.RBN_BTN_ZOOMMAP;

		private string tooltipTitle = AfaStrings.RBN_BTN_ZOOMMAP_TOOLTIP_TITLE;

		private string tooltipDescr = AfaStrings.RBN_BTN_ZOOMMAP_TOOLTIP;

		private string smallImageName = "img_zoomextents_small";

		private string largeImageName = "img_zoomextents_large";

		private RibbonItemSize btnsize;

		private Orientation orientation;

		public btnZoomMapExtents()
		{
			base.Id=(this.name);
			base.Name=(this.name);
			base.Text=(this.name);
			base.IsEnabled=(false);
			base.Image=(ArcGISRibbon.loadImage(this.smallImageName));
			base.LargeImage=(ArcGISRibbon.loadImage(this.largeImageName));
			base.Size=(this.btnsize);
			base.ShowImage=(true);
			base.ShowText=(base.Size == (RibbonItemSize)1);
			base.Orientation=(Orientation.Vertical);
			base.KeyTip=("This is a keytip");
			base.ToolTip=(ArcGISRibbon.createToolTip(this.tooltipTitle, this.tooltipDescr));
			base.CommandHandler=(new btnZoomMapExtents.btn_CommandHandler());
			base.HelpSource=(ArcGISRibbon.HelpPath);
		}
	}
}
