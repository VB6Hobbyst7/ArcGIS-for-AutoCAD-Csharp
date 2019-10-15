using AFA.Resources;
using Autodesk.Windows;
using System;

namespace AFA.UI
{
	public class comboFeatureClass : RibbonCombo
	{
		private static string name = AfaStrings.RBN_COMBO_FEATURECLASS;

		private static string tooltipTitle = AfaStrings.RBN_COMBO_FEATURECLASS_TOOLTIP_TITLE;

		private static string tooltipDescr = AfaStrings.RBN_COMBO_FEATURECLASS_TOOLTIP;

		private static bool showTextUnderButton = false;

		public comboFeatureClass()
		{
			base.Id=(comboFeatureClass.name);
			base.Name=(comboFeatureClass.name);
			base.Tag=(comboFeatureClass.name);
			base.Text=(comboFeatureClass.name);
			base.IsEnabled=(true);
			base.ShowText=(comboFeatureClass.showTextUnderButton);
			base.KeyTip=("This is a keytip");
			base.ToolTip=(ArcGISRibbon.createToolTip(comboFeatureClass.tooltipTitle, comboFeatureClass.tooltipDescr));
			base.HelpSource=(ArcGISRibbon.HelpPath);
		}

		~comboFeatureClass()
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
			base.DropDownOpened+=(new EventHandler<EventArgs>(this.FeatureClassCombo_DropDownOpened));
			base.CurrentChanged+=(new EventHandler<RibbonPropertyChangedEventArgs>(this.fcCombo_CurrentChanged));
		}

		public void Uninitialize()
		{
			try
			{
				base.DropDownOpened+=(new EventHandler<EventArgs>(this.FeatureClassCombo_DropDownOpened));
				base.CurrentChanged+=(new EventHandler<RibbonPropertyChangedEventArgs>(this.fcCombo_CurrentChanged));
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

		private void FeatureClassCombo_DropDownOpened(object sender, EventArgs e)
		{
			RibbonCombo ribbonCombo = sender as RibbonCombo;
			ribbonCombo.IsEditable=(false);
			ribbonCombo.Items.Clear();
			MSCFeatureClass topActiveFeatureClass = AfaDocData.ActiveDocData.GetTopActiveFeatureClass();
			foreach (MSCFeatureClass current in AfaDocData.ActiveDocData.DocDataset.FeatureClasses.Values)
			{
				RibbonLabel ribbonLabel = new RibbonLabel();
				ribbonLabel.Tag=(new FCTag(current));
				ribbonLabel.Text=(current.Name);
				ribbonCombo.Items.Add(ribbonLabel);
				if (topActiveFeatureClass == current)
				{
					ribbonCombo.Current=(ribbonLabel);
				}
			}
			foreach (MSCFeatureService current2 in AfaDocData.ActiveDocData.DocDataset.FeatureServices.Values)
			{
				RibbonLabel ribbonLabel2 = new RibbonLabel();
				ribbonLabel2.Tag=(new FCTag(current2));
				ribbonLabel2.Text=(current2.Name);
				ribbonCombo.Items.Add(ribbonLabel2);
				if (topActiveFeatureClass == current2)
				{
					ribbonCombo.Current=(ribbonLabel2);
				}
			}
		}

		private void fcCombo_CurrentChanged(object sender, RibbonPropertyChangedEventArgs e)
		{
			try
			{
				RibbonCombo ribbonCombo = sender as RibbonCombo;
				RibbonLabel ribbonLabel = ribbonCombo.Current as RibbonLabel;
				if (ribbonLabel != null)
				{
					FCTag fCTag = (FCTag)ribbonLabel.Tag;
					MSCFeatureClass featureClass = fCTag.GetFeatureClass(AfaDocData.ActiveDocData.DocDataset);
					MSCFeatureClass topActiveFeatureClass = AfaDocData.ActiveDocData.GetTopActiveFeatureClass();
					if (featureClass != topActiveFeatureClass)
					{
						AfaDocData.ActiveDocData.SetActiveFeatureClass(featureClass);
						ArcGISRibbon.SetSubTypeComboToDefault(featureClass);
					}
					ArcGISRibbon.SetFeatureClassButtonState(AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype());
				}
			}
			catch
			{
				AfaDocData.ActiveDocData.ClearActiveFeatureClass();
				ArcGISRibbon.ClearSubtypeCombo();
				ArcGISRibbon.SetFeatureClassButtonState(AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype());
			}
		}
	}
}
