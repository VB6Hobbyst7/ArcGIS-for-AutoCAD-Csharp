using AFA.Resources;
using Autodesk.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace AFA.UI
{
	public class btnGeolocate : RibbonButton
	{
		private class btn_CommandHandler : ICommand
		{
			public event EventHandler CanExecuteChanged;

			public void Execute(object param)
			{
				RibbonButton ribbonButton = param as RibbonButton;
				if (ribbonButton != null)
				{
					string cmdString = ".ESRI_LOCATE ";
					CmdLine.ExecuteQuietCommand(cmdString);
				}
			}

			public bool CanExecute(object param)
			{
				return true;
			}
		}

		private string name = AfaStrings.RBN_BTN_LOCATE;

		private string tooltipTitle = AfaStrings.RBN_BTN_LOCATE_TOOLTIP_TITLE;

		private string tooltipDescr = AfaStrings.RBN_BTN_LOCATE_TOOLTIP;

		private string smallImageName = "img_geolocate_small";

		private string largeImageName = "img_geolocate_large";

		private bool showTextUnderButton = true;

		private RibbonItemSize btnsize = (RibbonItemSize)1;

		private Orientation orientation = Orientation.Vertical;

		public btnGeolocate()
		{
			base.Id=(this.name);
			base.Name=(this.name);
			base.Tag=(this.name);
			base.Text=(this.name);
			base.IsEnabled=(true);
			base.ShowText=(this.showTextUnderButton);
			base.Image=(ArcGISRibbon.loadImage(this.smallImageName));
			base.LargeImage=(ArcGISRibbon.loadImage(this.largeImageName));
			base.Size=(this.btnsize);
			base.ShowImage=(true);
			base.ShowText=(true);
			base.Orientation=(this.orientation);
			base.KeyTip=("This is a keytip");
			base.ToolTip=(ArcGISRibbon.createToolTip(this.tooltipTitle, this.tooltipDescr));
			base.CommandHandler=(new btnGeolocate.btn_CommandHandler());
			base.HelpSource=(ArcGISRibbon.HelpPath);
		}
	}
}
