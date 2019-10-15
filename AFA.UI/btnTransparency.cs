using AFA.Resources;
using Autodesk.Private.Windows;
using Autodesk.Windows;
using System;
using System.Windows.Controls;

namespace AFA.UI
{
    public class btnTransparency : RibbonSplitButton
	{
		private string name = AfaStrings.RBN_BTN_TRANSPARENCY;

		private string tooltipTitle = AfaStrings.RBN_BTN_TRANSPARENCY_TOOLTIP_TITLE;

		private string tooltipDescr = AfaStrings.RBN_BTN_TRANSPARENCY_TOOLTIP;

		private string smallImageName = "img_transparency_small";

		private string largeImageName = "img_transparency_large";

		private RibbonItemSize btnsize;

		private Orientation orientation = Orientation.Vertical;

		private RibbonButton btn0;

		private RibbonButton btn10;

		private RibbonButton btn20;

		private RibbonButton btn30;

		private RibbonButton btn40;

		private RibbonButton btn50;

		private RibbonButton btn60;

		private RibbonButton btn70;

		private RibbonButton btn80;

		private RibbonButton btn90;

		private RibbonButton btn100;

		public btnTransparency()
		{
            //base.Id=(this.name);
            //base.Name=(this.name);
            //base.Text=(this.name);
            //base.IsEnabled=(true);
            //base.add_DropDownOpened(new EventHandler<EventArgs>(this.btnTransparency_DropDownOpened));
            //base.Image=(ArcGISRibbon.loadImage(this.smallImageName));
            //base.LargeImage=(ArcGISRibbon.loadImage(this.largeImageName));
            //base.Size=(this.btnsize);
            //base.ShowImage=(true);
            //base.ShowText=(false);
            //base.Orientation=(this.orientation);
            //base.KeyTip=("This is a keytip");
            //base.ToolTip=(ArcGISRibbon.createToolTip(this.tooltipTitle, this.tooltipDescr));
            //base.CommandHandler=(new btn_TransparencyCommandHandler());
            //base.HelpSource=(ArcGISRibbon.HelpPath);
            //base.set_ListButtonStyle(0);
            //RibbonRowPanel ribbonRowPanel = new RibbonRowPanel();
            //ribbonRowPanel.set_GroupName(AfaStrings.RBN_BTN_TRANSPARENCY);
            //this.btn100 = new btnTransparencyPercent("100%", 100, (RibbonToolTip)base.get_ToolTip());
            //ribbonRowPanel.Items.Add(this.btn100);
            //base.Items.Add(this.btn100);
            //this.btn90 = new btnTransparencyPercent("90%", 90, (RibbonToolTip)base.get_ToolTip());
            //ribbonRowPanel.Items.Add(this.btn90);
            //base.Items.Add(this.btn90);
            //this.btn80 = new btnTransparencyPercent("80%", 80, (RibbonToolTip)base.get_ToolTip());
            //ribbonRowPanel.Items.Add(this.btn80);
            //base.Items.Add(this.btn80);
            //this.btn70 = new btnTransparencyPercent("70%", 70, (RibbonToolTip)base.get_ToolTip());
            //ribbonRowPanel.Items.Add(this.btn70);
            //base.Items.Add(this.btn70);
            //this.btn60 = new btnTransparencyPercent("60%", 60, (RibbonToolTip)base.get_ToolTip());
            //ribbonRowPanel.Items.Add(this.btn60);
            //base.Items.Add(this.btn60);
            //this.btn50 = new btnTransparencyPercent("50%", 50, (RibbonToolTip)base.get_ToolTip());
            //ribbonRowPanel.Items.Add(this.btn50);
            //base.Items.Add(this.btn50);
            //this.btn40 = new btnTransparencyPercent("40%", 40, (RibbonToolTip)base.get_ToolTip());
            //ribbonRowPanel.Items.Add(this.btn40);
            //base.Items.Add(this.btn40);
            //this.btn30 = new btnTransparencyPercent("30%", 30, (RibbonToolTip)base.get_ToolTip());
            //ribbonRowPanel.Items.Add(this.btn30);
            //base.Items.Add(this.btn30);
            //this.btn20 = new btnTransparencyPercent("20%", 20, (RibbonToolTip)base.get_ToolTip());
            //ribbonRowPanel.Items.Add(this.btn20);
            //base.Items.Add(this.btn20);
            //this.btn10 = new btnTransparencyPercent("10%", 10, (RibbonToolTip)base.get_ToolTip());
            //ribbonRowPanel.Items.Add(this.btn10);
            //base.Items.Add(this.btn10);
            //this.btn0 = new btnTransparencyPercent("0%", 0, (RibbonToolTip)base.get_ToolTip());
            //ribbonRowPanel.Items.Add(this.btn0);
            //base.Items.Add(this.btn0);
            this.name = AfaStrings.RBN_BTN_TRANSPARENCY;
            this.tooltipTitle = AfaStrings.RBN_BTN_TRANSPARENCY_TOOLTIP_TITLE;
            this.tooltipDescr = AfaStrings.RBN_BTN_TRANSPARENCY_TOOLTIP;
            this.smallImageName = "img_transparency_small";
            this.largeImageName = "img_transparency_large";
            this.orientation = Orientation.Vertical;
            base.Id = this.name;
            base.Name = this.name;
            base.Text = this.name;
            base.IsEnabled = true;
            base.DropDownOpened += new EventHandler<EventArgs>(this.btnTransparency_DropDownOpened);
            base.Image = ArcGISRibbon.loadImage(this.smallImageName);
            base.LargeImage = ArcGISRibbon.loadImage(this.largeImageName);
            base.Size = this.btnsize;
            base.ShowImage = true;
            base.ShowText = false;
            base.Orientation = this.orientation;
            base.KeyTip = "This is a keytip";
            base.ToolTip = ArcGISRibbon.createToolTip(this.tooltipTitle, this.tooltipDescr);
            base.CommandHandler=(new btn_TransparencyCommandHandler());
            base.HelpSource = ArcGISRibbon.HelpPath;
            base.ListButtonStyle = RibbonListButtonStyle.SplitButton;
            RibbonRowPanel panel = new RibbonRowPanel
            {
                GroupName = AfaStrings.RBN_BTN_TRANSPARENCY
            };
            this.btn100 = new btnTransparencyPercent("100%", 100, (RibbonToolTip)base.ToolTip);
            panel.Items.Add(this.btn100);
            base.Items.Add(this.btn100);
            this.btn90 = new btnTransparencyPercent("90%", 90, (RibbonToolTip)base.ToolTip);
            panel.Items.Add(this.btn90);
            base.Items.Add(this.btn90);
            this.btn80 = new btnTransparencyPercent("80%", 80, (RibbonToolTip)base.ToolTip);
            panel.Items.Add(this.btn80);
            base.Items.Add(this.btn80);
            this.btn70 = new btnTransparencyPercent("70%", 70, (RibbonToolTip)base.ToolTip);
            panel.Items.Add(this.btn70);
            base.Items.Add(this.btn70);
            this.btn60 = new btnTransparencyPercent("60%", 60, (RibbonToolTip)base.ToolTip);
            panel.Items.Add(this.btn60);
            base.Items.Add(this.btn60);
            this.btn50 = new btnTransparencyPercent("50%", 50, (RibbonToolTip)base.ToolTip);
            panel.Items.Add(this.btn50);
            base.Items.Add(this.btn50);
            this.btn40 = new btnTransparencyPercent("40%", 40, (RibbonToolTip)base.ToolTip);
            panel.Items.Add(this.btn40);
            base.Items.Add(this.btn40);
            this.btn30 = new btnTransparencyPercent("30%", 30, (RibbonToolTip)base.ToolTip);
            panel.Items.Add(this.btn30);
            base.Items.Add(this.btn30);
            this.btn20 = new btnTransparencyPercent("20%", 20, (RibbonToolTip)base.ToolTip);
            panel.Items.Add(this.btn20);
            base.Items.Add(this.btn20);
            this.btn10 = new btnTransparencyPercent("10%", 10, (RibbonToolTip)base.ToolTip);
            panel.Items.Add(this.btn10);
            base.Items.Add(this.btn10);
            this.btn0 = new btnTransparencyPercent("0%", 0, (RibbonToolTip)base.ToolTip);
            panel.Items.Add(this.btn0);
            base.Items.Add(this.btn0);

        }

        private void btnTransparency_DropDownOpened(object sender, EventArgs e)
		{
            //try
            //{
            //	MSCRasterService activeRasterService = ArcGISRibbon.GetActiveRasterService();
            //	if (activeRasterService != null)
            //	{
            //		byte transparency = activeRasterService.GetTransparency();
            //		int num = Convert.ToInt32(10.0 * Math.Floor((double)transparency / 10.0));
            //		using (IEnumerator<RibbonItem> enumerator = base.Items.GetEnumerator())
            //		{
            //			while (enumerator.MoveNext())
            //			{
            //				btnTransparencyPercent btnTransparencyPercent = (btnTransparencyPercent)enumerator.Current;
            //				if (num == btnTransparencyPercent.PercentValue)
            //				{
            //					btnTransparencyPercent.IsChecked=(true);
            //				}
            //				else
            //				{
            //					btnTransparencyPercent.IsChecked=(false);
            //				}
            //			}
            //		}
            //	}
            //}
            //catch
            //{
            //}
            try
            {
                MSCRasterService activeRasterService = ArcGISRibbon.GetActiveRasterService();
                if (activeRasterService != null)
                {
                    int num2 = Convert.ToInt32((double)(10.0 * Math.Floor((double)(((double)activeRasterService.GetTransparency()) / 10.0))));
                    foreach (btnTransparencyPercent percent in base.Items)
                    {
                        if (num2 == percent.PercentValue)
                        {
                            percent.IsChecked = true;
                            continue;
                        }
                        percent.IsChecked = false;
                    }
                }
            }
            catch
            {
            }

        }
    }
}
