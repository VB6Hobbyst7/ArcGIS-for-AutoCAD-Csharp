using AFA.Resources;
using Autodesk.Windows;
using System;

namespace AFA.UI
{
	public class comboSubtypes : RibbonCombo
	{
		private static string name = AfaStrings.RBN_COMBO_SUBTYPE;

		private static string tooltipTitle = AfaStrings.RBN_COMBO_SUBTYPE_TOOLTIP_TITLE;

		private static string tooltipDescr = AfaStrings.RBN_COMBO_SUBTYPE_TOOLTIP;

		private static bool showTextUnderButton = false;

		public comboSubtypes()
		{
			base.Id=(comboSubtypes.name);
			base.Name=(comboSubtypes.name);
			base.Tag=(comboSubtypes.name);
			base.Text=(comboSubtypes.name);
			base.IsEnabled=(true);
			base.ShowText=(comboSubtypes.showTextUnderButton);
			base.KeyTip=("This is a keytip");
			base.ToolTip=(ArcGISRibbon.createToolTip(comboSubtypes.tooltipTitle, comboSubtypes.tooltipDescr));
			base.HelpSource=(ArcGISRibbon.HelpPath);
		}

		~comboSubtypes()
		{
			try
			{
				this.Uninitialize();
			}
			catch
			{
			}
		}

		public void Initialize()
		{
			base.DropDownOpened+=(new EventHandler<EventArgs>(this.subclassCombo_DropDownOpened));
			base.CurrentChanged+=(new EventHandler<RibbonPropertyChangedEventArgs>(this.subclassCombo_CurrentChanged));
		}

		public void Uninitialize()
		{
			try
			{
				base.DropDownOpened-=(new EventHandler<EventArgs>(this.subclassCombo_DropDownOpened));
				base.CurrentChanged-=(new EventHandler<RibbonPropertyChangedEventArgs>(this.subclassCombo_CurrentChanged));
				this.ClearItems();
			}
			catch
			{
			}
		}

		public void ClearItems()
		{
			try
			{
				if (base.Items.Count > 0)
				{
					base.Items.Clear();
				}
			}
			catch
			{
			}
		}

		private void subclassCombo_DropDownOpened(object sender, EventArgs e)
		{
			RibbonCombo ribbonCombo = sender as RibbonCombo;
			ribbonCombo.IsEditable=(false);
			ribbonCombo.Items.Clear();
			MSCFeatureClass topActiveFeatureClass = AfaDocData.ActiveDocData.GetTopActiveFeatureClass();
			MSCFeatureClass activeSubtype = AfaDocData.ActiveDocData.GetActiveSubtype();
			if (topActiveFeatureClass != null)
			{
				if (topActiveFeatureClass.SubTypes != null && topActiveFeatureClass.SubTypes.Count > 0)
				{
					RibbonLabel ribbonLabel = new RibbonLabel();
					ribbonLabel.Tag=(null);
					ribbonLabel.Text=("All Types");
					ribbonCombo.Items.Add(ribbonLabel);
					ribbonCombo.Current=(ribbonLabel);
				}
				foreach (MSCFeatureClass current in topActiveFeatureClass.SubTypes)
				{
					RibbonLabel ribbonLabel2 = new RibbonLabel();
					ribbonLabel2.Tag=(new FCTag(current));
					ribbonLabel2.Text=(current.Name);
					ribbonCombo.Items.Add(ribbonLabel2);
					if (activeSubtype == current)
					{
						ribbonCombo.Current=(ribbonLabel2);
					}
				}
			}
		}

		private void subclassCombo_CurrentChanged(object sender, RibbonPropertyChangedEventArgs e)
		{
			try
			{
				RibbonCombo ribbonCombo = sender as RibbonCombo;
				RibbonLabel ribbonLabel = ribbonCombo.Current as RibbonLabel;
				if (ribbonLabel != null)
				{
					FCTag fCTag = (FCTag)ribbonLabel.Tag;
					if (fCTag != null)
					{
						AfaDocData.ActiveDocData.SetActiveFeatureClass(fCTag.GetFeatureClass(AfaDocData.ActiveDocData.DocDataset));
					}
					else
					{
						AfaDocData.ActiveDocData.ClearActiveSubtype();
					}
				}
			}
			catch
			{
				AfaDocData.ActiveDocData.ClearActiveSubtype();
				MSCFeatureClass topActiveFeatureClass = AfaDocData.ActiveDocData.GetTopActiveFeatureClass();
				ArcGISRibbon.SetFeatureClassButtonState(topActiveFeatureClass);
			}
		}
	}
}
