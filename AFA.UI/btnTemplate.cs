using AFA.Resources;
using Autodesk.Windows;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AFA.UI
{
	public class btnTemplate : RibbonButton
	{
		private class btn_CommandHandler : ICommand
		{
			public event EventHandler CanExecuteChanged;

			public void Execute(object param)
			{
				RibbonButton ribbonButton = param as RibbonButton;
				if (ribbonButton != null)
				{
					MessageBox.Show("Not implemented");
				}
			}

			public bool CanExecute(object param)
			{
				return true;
			}
		}

		private string name = AfaStrings.RBN_BTN_MAP_EXPORTFEATURES;

		private string tooltipTitle = AfaStrings.RBN_BTN_MAP_EXPORTFEATURES_TOOLTIP_TITLE;

		private string tooltipDescr = AfaStrings.RBN_BTN_MAP_EXPORTFEATURES_TOOLTIP;

		private string smallImageName = "img_extract_small";

		private string largeImageName = "img_extract_large";

		private RibbonItemSize btnsize;

		private Orientation orientation;

		private bool showTextUnderButton = true;

		public btnTemplate()
		{
			base.Id=(this.name);
			base.Name=(this.name);
			base.Text=(this.name);
			base.IsEnabled=(true);
			base.Image=(ArcGISRibbon.loadImage(this.smallImageName));
			base.LargeImage=(ArcGISRibbon.loadImage(this.largeImageName));
			base.Size=(this.btnsize);
			base.ShowImage=(true);
			base.ShowText=(this.showTextUnderButton);
			base.Orientation=(this.orientation);
			base.KeyTip=("This is a keytip");
			base.ToolTip=(ArcGISRibbon.createToolTip(this.tooltipTitle, this.tooltipDescr));
			base.CommandHandler=(new btnTemplate.btn_CommandHandler());
			base.HelpSource=(ArcGISRibbon.HelpPath);
		}
	}
}
