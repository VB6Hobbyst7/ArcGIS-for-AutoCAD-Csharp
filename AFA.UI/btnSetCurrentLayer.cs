using AFA.Resources;
using Autodesk.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace AFA.UI
{
	public class btnSetCurrentLayer : RibbonButton
	{
		private class btn_CommandHandler : ICommand
		{
			public event EventHandler CanExecuteChanged;

			public void Execute(object param)
			{
				RibbonButton ribbonButton = param as RibbonButton;
				if (ribbonButton != null)
				{
					CmdLine.CancelActiveCommand();
					string cmdString = ".ESRI_SetCurrentDrawingLayer ";
					CmdLine.ExecuteQuietCommand(cmdString);
				}
			}

			public bool CanExecute(object param)
			{
				return true;
			}
		}

		private string name = AfaStrings.RBN_BTN_SETCURRENTDRAWINGLAYER;

		private string tooltipTitle = AfaStrings.RBN_BTN_SETCURRENTDRAWINGLAYER_TOOLTIP_TITLE;

		private string tooltipDescr = AfaStrings.RBN_BTN_SETCURRENTDRAWINGLAYER_TOOLTIP;

		private string smallImageName = "img_setcurrentdrawinglayer_small";

		private string largeImageName = "img_setcurrentdrawinglayer_large";

		private RibbonItemSize btnsize;

		public btnSetCurrentLayer()
		{
			base.Id=(this.name);
			base.Name=(this.name);
			base.Text=(this.name);
			base.IsEnabled=(false);
			base.Image=(ArcGISRibbon.loadImage(this.smallImageName));
			base.LargeImage=(ArcGISRibbon.loadImage(this.largeImageName));
			base.Size=(this.btnsize);
			base.ShowImage=(true);
			base.ShowText=(this.btnsize == (RibbonItemSize)1);
			base.Orientation=(Orientation.Vertical);
			base.KeyTip=("This is a keytip");
			base.ToolTip=(ArcGISRibbon.createToolTip(this.tooltipTitle, this.tooltipDescr));
			base.CommandHandler=(new btnSetCurrentLayer.btn_CommandHandler());
			base.HelpSource=(ArcGISRibbon.HelpPath);
		}
	}
}
