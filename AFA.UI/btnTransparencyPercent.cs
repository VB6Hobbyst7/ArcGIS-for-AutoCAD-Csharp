using Autodesk.Windows;
using System.Windows.Controls;

namespace AFA.UI
{
    public class btnTransparencyPercent : RibbonButton
	{
		private RibbonItemSize btnsize;

		private Orientation orientation = Orientation.Vertical;

		private string smallImageName = "img_transparency_small";

		private string largeImageName = "img_transparency_large";

		public int PercentValue
		{
			get;
			set;
		}

		public btnTransparencyPercent(string text, int value, RibbonToolTip toolTip)
		{
			base.Id=(text);
			base.Name=(text);
			base.Text=(text);
			base.IsEnabled=(true);
			this.PercentValue = value;
			base.Size=(this.btnsize);
			base.ShowImage=(false);
			base.ShowText=(true);
			base.IsCheckable=(true);
			base.Image=(ArcGISRibbon.loadImage(this.smallImageName));
			base.ToolTip=(Utility.CloneObject(toolTip));
			base.CommandHandler=(new btn_TransparencyCommandHandler());
			base.HelpSource=(ArcGISRibbon.HelpPath);
			base.ResizeStyle=(RibbonItemResizeStyles)(1);
		}
	}
}
