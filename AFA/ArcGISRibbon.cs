using AFA.Resources;
using AFA.UI;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.Windows;
using System;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace AFA
{
    public static class ArcGISRibbon
	{
		private static string _ribbonUID = "{CF41554D-EE79-4d11-8180-B027080F9C5E}";

		private static comboFeatureClass _fcCombo;

		private static comboSubtypes _stCombo;

		private static RibbonCombo _rsCombo = null;

		public static Uri HelpPath;

		private static RibbonPanel _panelMaps;

		private static btnIdentifyMap _btnIdentifyMap;

		private static btnZoomMapExtents _btnZoomMapExtents;

		private static btnMapProperties _btnMapProperties;

		private static btnRemoveMap _btnMapRemove;

		private static btnToggleVisibility _btnToggleVisibility;

		private static btnSetMapLimit _btnSetMapLimit;

		private static btnExportMap _btnExportMap;

		private static btnTransparency _btnTransparency;

		private static btnRefreshMap _btnRefreshMap;

		private static btnSendBehind _btnSendBehind = null;

		private static btnExtractFeatures _btnExtractFeatures = null;

		private static btnGeolocate _btnGeolocate;

		private static btnSynchronizeAll _btnSynchronizeAll;

		private static btnSynchronize _btnSynchronize;

		private static btnDiscardAllEdits _btnDiscardAllEdits;

		private static btnDiscardEdits _btnDiscardEdits;

		private static btnRemoveFeatureService _btnRemoveFS;

		private static btnSetFeatureServiceLimit _btnSetFeatureServiceLimit;

		private static btnSetCurrentLayer _btnSetCurrentLayer;

		private static RibbonButton _btnAttributeIdentify;

		private static RibbonButton _btnFCProps;

		private static RibbonButton _btnOpenTable;

		private static RibbonButton _btnZoomFeatures;

		private static RibbonButton _btnSelectObjects;

		private static RibbonButton _btnSelectAll;

		private static RibbonButton _btnSelectNone;

		private static RibbonButton _btnSelectByAttributes;

		private static RibbonButton _btnNewFeatureClass;

		public static void SetTopFeatureClass(MSCFeatureClass fc)
		{
			if (fc != null)
			{
				if (fc.IsSubType)
				{
					return;
				}
				if (_fcCombo == null)
				{
					return;
				}
				_fcCombo.ClearItems();
				if (fc != null)
				{
					RibbonLabel ribbonLabel = new RibbonLabel();
					ribbonLabel.Text=fc.Name;
					ribbonLabel.Tag=new FCTag(fc);
					_fcCombo.Items.Add(ribbonLabel);
					_fcCombo.Current=(ribbonLabel);
					return;
				}
			}
			else
			{
				ClearSubtypeCombo();
                _stCombo.IsEnabled = false;
				ClearFeatureClassCombo();
				SetFeatureClassButtonState(null);
			}
		}

		public static void SetSubTypeComboToDefault(MSCFeatureClass fc)
		{
			ArcGISRibbon._stCombo.ClearItems();
			ArcGISRibbon._stCombo.IsEnabled=false;
			if (fc.SubTypes.Count > 0)
			{
				ArcGISRibbon._stCombo.IsEnabled=(true);
				RibbonLabel ribbonLabel = new RibbonLabel();
				ribbonLabel.Text="All Types";
				ArcGISRibbon._stCombo.Items.Add(ribbonLabel);
				ArcGISRibbon._stCombo.Current=(ribbonLabel);
			}
		}

		public static void SetActiveFeatureClass(MSCFeatureClass fc)
		{
			if (fc == null)
			{
				ArcGISRibbon.ClearSubtypeCombo();
				ArcGISRibbon._stCombo.IsEnabled=false;
				ArcGISRibbon.ClearFeatureClassCombo();
				ArcGISRibbon.SetFeatureClassButtonState(null);
				return;
			}
			MSCFeatureClass topFeatureClass = fc;
			MSCFeatureClass fc2 = null;
			if (fc.IsSubType)
			{
				fc2 = fc;
				topFeatureClass = fc.ParentFC;
			}
			ArcGISRibbon.SetTopFeatureClass(topFeatureClass);
			if (fc.IsSubType)
			{
				ArcGISRibbon._stCombo.ClearItems();
				ArcGISRibbon._stCombo.IsEnabled=(true);
				RibbonLabel ribbonLabel = new RibbonLabel();
				ribbonLabel.Text=(fc.Name);
				ribbonLabel.Tag=new FCTag(fc2);
				ArcGISRibbon._stCombo.Items.Add(ribbonLabel);
				ArcGISRibbon._stCombo.Current=(ribbonLabel);
			}
			else
			{
				ArcGISRibbon.SetSubTypeComboToDefault(fc);
			}
			ArcGISRibbon.SetFeatureClassButtonState(fc);
		}

		public static void SetActiveRasterService(MSCRasterService rs)
		{
			if (ArcGISRibbon._rsCombo == null)
			{
				return;
			}
			if (rs == null)
			{
				ArcGISRibbon.ClearRasterCombo();
				return;
			}
			RibbonLabel ribbonLabel = new RibbonLabel();
			ribbonLabel.Text=(rs.Name);
			ribbonLabel.Tag=(rs);
			ArcGISRibbon._rsCombo.Items.Add(ribbonLabel);
			ArcGISRibbon._rsCombo.Current=(ribbonLabel);
			ArcGISRibbon.SetMapButtonState(rs);
		}

		public static string GetActiveRasterServiceName()
		{
			string result;
			try
			{
				RibbonLabel ribbonLabel = (RibbonLabel)ArcGISRibbon._rsCombo.Current;
				result = ribbonLabel.Text;
			}
			catch
			{
				result = "";
			}
			return result;
		}

		public static MSCRasterService GetActiveRasterService()
		{
			MSCRasterService result;
			try
			{
				RibbonLabel ribbonLabel = (RibbonLabel)ArcGISRibbon._rsCombo.Current;
				result = (MSCRasterService)ribbonLabel.Tag;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static void ClearRasterCombo()
		{
			try
			{
				ArcGISRibbon._rsCombo.Items.Clear();
				ArcGISRibbon.SetMapButtonState((MSCImageService)null);
			}
			catch
			{
			}
		}

		public static void ClearSubtypeCombo()
		{
			try
			{
				ArcGISRibbon._stCombo.Items.Clear();
			}
			catch
			{
			}
		}

		public static void ClearFeatureClassCombo()
		{
			try
			{
				ArcGISRibbon._fcCombo.Items.Clear();
			}
			catch
			{
			}
		}

		public static Uri BuildHelpPath()
		{
			Uri result;
			try
			{
				CultureInfo cultureInfo = new CultureInfo(SystemObjects.DynamicLinker.ProductLcid);
				string text = Assembly.GetExecutingAssembly().Location;
				text = Path.GetDirectoryName(text);
				string text2 = Path.Combine(text, cultureInfo.IetfLanguageTag);
				text2 = Path.Combine(text2, "arcgis_for_autocad.chm");
				if (File.Exists(text2))
				{
					result = new Uri(text2);
				}
				else
				{
					text = Path.Combine(text, "arcgis_for_autocad.chm");
					result = new Uri(text);
				}
			}
			catch
			{
				result = new Uri("");
			}
			return result;
		}

		public static void Initialize()
		{
            try
            {
                HelpPath = BuildHelpPath();
                createRibbon();
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.DocumentBecameCurrent += new DocumentCollectionEventHandler(ArcGISRibbon.DocumentManager_DocumentBecameCurrent);
               
                Autodesk.AutoCAD.ApplicationServices.Application.SystemVariableChanged += new Autodesk.AutoCAD.ApplicationServices.SystemVariableChangedEventHandler(ArcGISRibbon.Application_SystemVariableChanged);
                Autodesk.AutoCAD.ApplicationServices.Application.EndCustomizationMode += new EventHandler(ArcGISRibbon.Application_EndCustomizationMode);
                try
                {
                    MSCDataset.SetDefaultActiveFeatureClass();
                    MSCDataset.SetDefaultActiveRasterServices();
                }
                catch
                {
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

		private static void Application_EndCustomizationMode(object sender, EventArgs e)
		{
            Autodesk.AutoCAD.ApplicationServices.Application.Idle+=new EventHandler(ArcGISRibbon.Application_Idle);
		}

		private static void Application_Idle(object sender, EventArgs e)
		{
            Autodesk.AutoCAD.ApplicationServices.Application.Idle-=new EventHandler(ArcGISRibbon.Application_Idle);
			ArcGISRibbon.createRibbon();
			MSCFeatureClass activeFeatureClassOrSubtype = AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype();
			if (activeFeatureClassOrSubtype != null)
			{
				ArcGISRibbon.SetActiveFeatureClass(activeFeatureClassOrSubtype);
			}
			else
			{
				MSCDataset.SetDefaultActiveFeatureClass();
			}
			MSCRasterService mSCRasterService = AfaDocData.ActiveDocData.CurrentMapService;
			if (mSCRasterService == null)
			{
				mSCRasterService = AfaDocData.ActiveDocData.CurrentImageService;
			}
			if (mSCRasterService != null)
			{
				ArcGISRibbon.SetActiveRasterService(mSCRasterService);
				return;
			}
			MSCDataset.SetDefaultActiveRasterServices();
		}

		private static void Application_SystemVariableChanged(object sender, Autodesk.AutoCAD.ApplicationServices.SystemVariableChangedEventArgs e)
		{
			if (e.Name.ToLower() == "wscurrent")
			{
				ArcGISRibbon.createRibbon();
				MSCFeatureClass activeFeatureClassOrSubtype = AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype();
				if (activeFeatureClassOrSubtype != null)
				{
					ArcGISRibbon.SetActiveFeatureClass(activeFeatureClassOrSubtype);
				}
				else
				{
					MSCDataset.SetDefaultActiveFeatureClass();
				}
				MSCRasterService mSCRasterService = AfaDocData.ActiveDocData.CurrentMapService;
				if (mSCRasterService == null)
				{
					mSCRasterService = AfaDocData.ActiveDocData.CurrentImageService;
				}
				if (mSCRasterService != null)
				{
					ArcGISRibbon.SetActiveRasterService(mSCRasterService);
					return;
				}
				MSCDataset.SetDefaultActiveRasterServices();
			}
		}

		private static void DocumentManager_DocumentBecameCurrent(object sender, DocumentCollectionEventArgs e)
		{
			try
			{
				if (ArcGISRibbon._fcCombo != null)
				{
					ArcGISRibbon._fcCombo.Items.Clear();
				}
				if (ArcGISRibbon._stCombo != null)
				{
					ArcGISRibbon._stCombo.Items.Clear();
				}
				if (ArcGISRibbon._rsCombo != null)
				{
					ArcGISRibbon._rsCombo.Items.Clear();
				}
				ArcGISRibbon.SetMapButtonState((MSCImageService)null);
				ArcGISRibbon.SetFeatureClassButtonState(null);
				MSCDataset.SetDefaultActiveFeatureClass();
				MSCDataset.SetDefaultActiveRasterServices();
			}
			catch
			{
			}
		}

		public static void reInitialize()
		{
			ArcGISRibbon.createRibbon();
		}

		public static void Terminate()
		{
			try
			{
				ArcGISRibbon.clearRibbon();
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.DocumentBecameCurrent-=new DocumentCollectionEventHandler(ArcGISRibbon.DocumentManager_DocumentBecameCurrent);

                Autodesk.AutoCAD.ApplicationServices.Application.SystemVariableChanged-=new Autodesk.AutoCAD.ApplicationServices.SystemVariableChangedEventHandler(ArcGISRibbon.Application_SystemVariableChanged);
                Autodesk.AutoCAD.ApplicationServices.Application.EndCustomizationMode-=new EventHandler(ArcGISRibbon.Application_EndCustomizationMode);
			}
			catch
			{
			}
		}

		private static void clearRibbon()
		{
			try
			{
				RibbonControl ribbon = ComponentManager.Ribbon;
				if (ribbon != null)
				{
					RibbonTab ribbonTab = ribbon.FindTab("ArcGIS");
					if (ribbonTab != null)
					{
						ribbon.Tabs.Remove(ribbonTab);
					}
				}
				ArcGISRibbon._fcCombo = null;
				ArcGISRibbon._stCombo = null;
			}
			catch
			{
			}
		}

		private static RibbonTabCollection InitializeTabCollection()
		{
			RibbonTabCollection ribbonTabCollection = new RibbonTabCollection();
			RibbonTab ribbonTab = new RibbonTab();
			ribbonTab.Title=(AfaStrings.GISServices);
			ribbonTab.UID=("GIS Services");
			ribbonTab.Id=("GIS Services");
			ribbonTabCollection.Add(ribbonTab);
			RibbonTab ribbonTab2 = new RibbonTab();
			ribbonTab2.Title=(AfaStrings.DrawingCoordinates);
			ribbonTab2.UID=("Drawing Coordinates");
			ribbonTab2.Id=("Drawing Coordinates");
			ribbonTabCollection.Add(ribbonTab2);
			RibbonTab ribbonTab3 = new RibbonTab();
			ribbonTab3.Title=(AfaStrings.DrawingOrganization);
			ribbonTab3.UID=("Drawing Organization");
			ribbonTab3.Id=("Drawing Organization");
			ribbonTabCollection.Add(ribbonTab3);
			RibbonTab ribbonTab4 = new RibbonTab();
			ribbonTab4.Title=(AfaStrings.GISEditing);
			ribbonTab4.UID=("GIS Editing");
			ribbonTab4.Id=("GIS Editing");
			ribbonTabCollection.Add(ribbonTab4);
			RibbonTab ribbonTab5 = new RibbonTab();
			ribbonTab5.Title=(AfaStrings.Resources);
			ribbonTab5.UID=("Resources");
			ribbonTab5.Id=("Resources");
			ribbonTabCollection.Add(ribbonTab5);
			return ribbonTabCollection;
		}

		private static void removeRibbon()
		{
			try
			{
				RibbonControl ribbon = ComponentManager.Ribbon;
				RibbonTab ribbonTab = null;
				foreach (RibbonTab current in ribbon.Tabs)
				{
					if (current.UID == ArcGISRibbon._ribbonUID)
					{
						ribbonTab = current;
						break;
					}
				}
				if (ribbonTab != null)
				{
					ribbon.Tabs.Remove(ribbonTab);
				}
			}
			catch
			{
			}
		}

		private static void createRibbon()
		{
			try
			{
				RibbonControl ribbon = ComponentManager.Ribbon;
				foreach (RibbonTab current in ribbon.Tabs)
				{
					if (current.UID == ArcGISRibbon._ribbonUID)
					{
						current.IsVisible=true;
						return;
					}
				}
				RibbonTab ribbonTab = new RibbonTab();
				ribbonTab.Title=("ArcGIS");
				ribbonTab.UID=(ArcGISRibbon._ribbonUID);
				ribbonTab.Id=("ArcGIS");
				ribbonTab.Name=("ArcGIS");
				ribbon.Tabs.Add(ribbonTab);
				ArcGISRibbon.addContent(ribbonTab);
				ribbonTab.IsActive=true;
			}
			catch (System.Exception)
			{
				throw;
			}
		}

		private static void addContent(RibbonTab ribbonTab)
		{
			try
			{
				RibbonPanelCollection ribbonPanelCollection = ArcGISRibbon.InitializeRibbonPanelSources();
				foreach (RibbonPanel current in ribbonPanelCollection)
				{
					ribbonTab.Panels.Add(current);
				}
			}
			catch (System.Exception ex)
			{
				ErrorReport.ShowErrorMessage(ex.Message);
			}
		}

		private static RibbonPanelCollection InitializeRibbonPanelSources()
		{
			RibbonPanelCollection ribbonPanelCollection = new RibbonPanelCollection();
			RibbonPanelSource ribbonPanelSource = new RibbonPanelSource();
			ribbonPanelSource.Title=(AfaStrings.RBN_PANEL_ADD);
			RibbonPanel ribbonPanel = new RibbonPanel();
			ribbonPanel.Source=(ribbonPanelSource);
			ribbonPanelCollection.Add(ribbonPanel);
			ArcGISRibbon.addButtonsPanelAdd(ribbonPanelSource);
			RibbonPanelSource ribbonPanelSource2 = new RibbonPanelSource();
			ribbonPanelSource2.Title=(AfaStrings.RBN_PANEL_MANAGE);
			RibbonPanel ribbonPanel2 = new RibbonPanel();
			ribbonPanel2.Source=(ribbonPanelSource2);
			ribbonPanelCollection.Add(ribbonPanel2);
			ArcGISRibbon.addButtonsPanelManage(ribbonPanelSource2);
			RibbonPanelSource ribbonPanelSource3 = new RibbonPanelSource();
			ribbonPanelSource3.Title=(AfaStrings.RBN_PANEL_NAVIGATE);
			RibbonPanel ribbonPanel3 = new RibbonPanel();
			ribbonPanel3.Source=(ribbonPanelSource3);
			ribbonPanelCollection.Add(ribbonPanel3);
			ArcGISRibbon.addButtonsPanelNavigate(ribbonPanelSource3);
			RibbonPanelSource ribbonPanelSource4 = new RibbonPanelSource();
			ribbonPanelSource4.Title=(AfaStrings.RBN_PANEL_MAPS);
			ArcGISRibbon._panelMaps = new RibbonPanel();
			ArcGISRibbon._panelMaps.Source=(ribbonPanelSource4);
			ribbonPanelCollection.Add(ArcGISRibbon._panelMaps);
			ArcGISRibbon.addButtonsPanelMaps(ribbonPanelSource4);
			RibbonPanelSource ribbonPanelSource5 = new RibbonPanelSource();
			ribbonPanelSource5.Title=(AfaStrings.RBN_PANEL_FEATURES);
			RibbonPanel ribbonPanel4 = new RibbonPanel();
			ribbonPanel4.Source=(ribbonPanelSource5);
			ribbonPanelCollection.Add(ribbonPanel4);
			ArcGISRibbon.addButtonsPanelFeatures(ribbonPanelSource5);
			RibbonPanelSource ribbonPanelSource6 = new RibbonPanelSource();
			ribbonPanelSource6.Title=(AfaStrings.RBN_PANEL_RESOURCES);
			RibbonPanel ribbonPanel5 = new RibbonPanel();
			ribbonPanel5.Source=(ribbonPanelSource6);
			ribbonPanelCollection.Add(ribbonPanel5);
			ArcGISRibbon.addButtonsPanelResources(ribbonPanelSource6);
			return ribbonPanelCollection;
		}

		private static void addCSMenu(RibbonRowPanel ribbonPanelSource)
		{
			RibbonSplitButton ribbonSplitButton = new RibbonSplitButton();
			ribbonSplitButton.Text=(AfaStrings.RBN_BTN_COORDSYS);
			ribbonSplitButton.Name=(AfaStrings.RBN_BTN_COORDSYS);
			ribbonSplitButton.Id=(AfaStrings.RBN_BTN_COORDSYS);
			ribbonSplitButton.Orientation=(Orientation.Vertical);
			ribbonSplitButton.Size=0;
			ribbonSplitButton.Image=(ArcGISRibbon.loadImage(ImageResources.img_coordsys_small));
			ribbonSplitButton.ShowText=(false);
			ribbonSplitButton.CommandHandler=(new RibbonCommandHandler());
			ribbonSplitButton.HelpSource=(ArcGISRibbon.HelpPath);
			ribbonSplitButton.ListButtonStyle=(0);
			RibbonRowPanel ribbonRowPanel = new RibbonRowPanel();
			ribbonRowPanel.GroupName=(AfaStrings.RBN_BTN_COORDSYS);
			RibbonButton ribbonButton = new RibbonButton();
			ribbonButton.Name=(AfaStrings.RBN_BTN_ASSIGN_COORDSYS);
			ribbonButton.Id=(AfaStrings.RBN_BTN_ASSIGN_COORDSYS);
			ribbonButton.Text=(AfaStrings.RBN_BTN_ASSIGN_COORDSYS);
			ribbonButton.Size=(0);
			ribbonButton.ToolTip=(ArcGISRibbon.createToolTip(AfaStrings.RBN_BTN_ASSIGN_COORDSYS_TOOLTIP_TITLE, AfaStrings.RBN_BTN_ASSIGN_COORDSYS_TOOLTIP));
			ribbonButton.CommandHandler=(new RibbonCommandHandler());
			ribbonButton.ResizeStyle=(RibbonItemResizeStyles)1;
			ribbonButton.ShowText=(true);
			ribbonButton.Image=(ArcGISRibbon.loadImage(ImageResources.img_coordsys_small));
			ribbonButton.CommandHandler=(new RibbonCommandHandler());
			ribbonButton.HelpSource=(ArcGISRibbon.HelpPath);
			ribbonRowPanel.Items.Add(ribbonButton);
			ribbonSplitButton.Items.Add(ribbonButton);
			RibbonButton ribbonButton2 = new RibbonButton();
			ribbonButton2.Name=(AfaStrings.RBN_BTN_LIST_COORDSYS);
			ribbonButton2.Id=(AfaStrings.RBN_BTN_LIST_COORDSYS);
			ribbonButton2.Text=(AfaStrings.RBN_BTN_LIST_COORDSYS);
			ribbonButton2.Size=(0);
			ribbonButton2.ToolTip=(ArcGISRibbon.createToolTip(AfaStrings.RBN_BTN_LIST_COORDSYS_TOOLTIP_TITLE, AfaStrings.RBN_BTN_LIST_COORDSYS_TOOLTIP));
			ribbonButton2.CommandHandler=(new RibbonCommandHandler());
			ribbonButton2.HelpSource=(ArcGISRibbon.HelpPath);
			ribbonButton2.ResizeStyle=(RibbonItemResizeStyles)1;
			ribbonButton2.ShowText=true;
			ribbonButton2.Image=(ArcGISRibbon.loadImage(ImageResources.img_coordsys_small));
			ribbonRowPanel.Items.Add(ribbonButton2);
			ribbonSplitButton.Items.Add(ribbonButton2);
			RibbonButton ribbonButton3 = new RibbonButton();
			ribbonButton3.Name=(AfaStrings.RBN_BTN_REMOVE_COORDSYS);
			ribbonButton3.Id=(AfaStrings.RBN_BTN_REMOVE_COORDSYS);
			ribbonButton3.Text=(AfaStrings.RBN_BTN_REMOVE_COORDSYS);
			ribbonButton3.Size=(0);
			ribbonButton3.ToolTip=(ArcGISRibbon.createToolTip(AfaStrings.RBN_BTN_REMOVE_COORDSYS_TOOLTIP_TITLE, AfaStrings.RBN_BTN_REMOVE_COORDSYS_TOOLTIP));
			ribbonButton3.CommandHandler=(new RibbonCommandHandler());
			ribbonButton3.HelpSource=(ArcGISRibbon.HelpPath);
			ribbonButton3.ResizeStyle=(RibbonItemResizeStyles)(1);
			ribbonButton3.ShowText=(true);
			ribbonButton3.Image=(ArcGISRibbon.loadImage(ImageResources.img_coordsys_small));
			ribbonRowPanel.Items.Add(ribbonButton3);
			ribbonSplitButton.Items.Add(ribbonButton3);
			ribbonPanelSource.Items.Add(ribbonSplitButton);
		}

		private static void addSeparator(RibbonPanelSource ribbonPanelSource)
		{
			RibbonSeparator ribbonSeparator = new RibbonSeparator();
			ribbonSeparator.SeparatorStyle=(0);
			ribbonPanelSource.Items.Add(ribbonSeparator);
		}

		private static void addRowBreak(RibbonRowPanel panel)
		{
			RibbonRowBreak item = new RibbonRowBreak();
			panel.Items.Add(item);
		}

		private static void addLargeButton(RibbonPanelSource ribbonPanelSource, string Id, string ImageName, string buttonName, string textDescription, object toolTip, string command)
		{
			RibbonButton ribbonButton = new RibbonButton();
			if (buttonName == "")
			{
				buttonName = Id;
			}
			ribbonButton.Id=(Id);
			ribbonButton.Name=(buttonName);
			ribbonButton.Tag=(buttonName);
			ribbonButton.Text=(textDescription);
			ribbonButton.KeyTip=(textDescription);
			if (toolTip.GetType() == typeof(RibbonToolTip))
			{
				ribbonButton.ToolTip=(toolTip);
			}
			else
			{
				RibbonToolTip ribbonToolTip = new RibbonToolTip();
				ribbonToolTip.Content=(toolTip);
				ribbonToolTip.Title=(Id);
			}
			ribbonButton.LargeImage=(ArcGISRibbon.loadImage(ImageName));
			ribbonButton.Image=(ArcGISRibbon.loadImage(ImageName));
			ribbonButton.Size=(RibbonItemSize)1;
			ribbonButton.ShowImage=(true);
			ribbonButton.ShowText=(true);
			ribbonButton.Orientation=(Orientation.Vertical);
			ribbonButton.CommandParameter=(command);
			ribbonButton.CommandHandler=(new RibbonCommandHandler());
			ribbonButton.HelpSource=(ArcGISRibbon.HelpPath);
			ribbonPanelSource.Items.Add(ribbonButton);
		}

		public static RibbonToolTip createToolTip(string title, string textDescription)
		{
			RibbonToolTip ribbonToolTip = new RibbonToolTip();
			ribbonToolTip.Title=(title);
			ribbonToolTip.Content=(textDescription);
			ribbonToolTip.HelpSource=(ArcGISRibbon.HelpPath);
			return ribbonToolTip;
		}

		private static RibbonButton createSmallButton(string Id, string ImageName, string textDescription, object toolTip, string command)
		{
			return ArcGISRibbon.createSmallButton(Id, ImageName, ImageName, textDescription, toolTip, command, Orientation.Vertical);
		}

		private static RibbonButton createSmallButton(string Id, string imageName, string textDescription, object toolTip, string command, Orientation orientation)
		{
			return ArcGISRibbon.createSmallButton(Id, imageName, imageName, textDescription, toolTip, command, orientation);
		}

		private static RibbonButton createSmallButton(string Id, string smallImageName, string largeImageName, string textDescription, object toolTip, string command, Orientation orientation)
		{
			RibbonButton ribbonButton = new RibbonButton();
			ribbonButton.Id=(Id);
			ribbonButton.Name=(Id);
			ribbonButton.Text=(textDescription);
			ribbonButton.Image=(ArcGISRibbon.loadImage(smallImageName));
			ribbonButton.LargeImage=(ArcGISRibbon.loadImage(largeImageName));
			ribbonButton.Size=(RibbonItemSize)(0);
			ribbonButton.ShowImage=(true);
			ribbonButton.ShowText=(false);
			ribbonButton.Orientation=(orientation);
			ribbonButton.KeyTip=("This is a keytip");
			if (toolTip.GetType() == typeof(RibbonToolTip))
			{
				ribbonButton.ToolTip=(toolTip);
			}
			else
			{
				RibbonToolTip ribbonToolTip = new RibbonToolTip();
				ribbonToolTip.Content=(toolTip);
				ribbonToolTip.Title=(Id);
			}
			ribbonButton.CommandParameter=(command);
			ribbonButton.CommandHandler=(new RibbonCommandHandler());
			ribbonButton.HelpSource=(ArcGISRibbon.HelpPath);
			return ribbonButton;
		}

		private static void InitializeRasterServiceCombo(RibbonCombo combo)
		{
			try
			{
				if (combo.Items.Count > 0)
				{
					combo.Items.Clear();
				}
				foreach (MSCRasterService current in AfaDocData.ActiveDocData.DocDataset.MapServices.Values)
				{
					RibbonLabel ribbonLabel = new RibbonLabel();
					ribbonLabel.Tag=(current);
					ribbonLabel.Text=(current.Name);
					combo.Items.Add(ribbonLabel);
				}
				foreach (MSCRasterService current2 in AfaDocData.ActiveDocData.DocDataset.ImageServices.Values)
				{
					RibbonLabel ribbonLabel2 = new RibbonLabel();
					ribbonLabel2.Tag=(current2);
					ribbonLabel2.Text=(current2.Name);
					combo.Items.Add(ribbonLabel2);
				}
			}
			catch
			{
			}
		}

		private static void addRSCombo(RibbonRowPanel ribbonPanelSource, string Id, string textDescription, object toolTip)
		{
			ArcGISRibbon._rsCombo = new RibbonCombo();
			ArcGISRibbon._rsCombo.ToolTip=(toolTip);
			ArcGISRibbon._rsCombo.DropDownOpened+=(new EventHandler<EventArgs>(ArcGISRibbon.RasterServiceCombo_DropDownOpened));
			ArcGISRibbon._rsCombo.CurrentChanged+=(new EventHandler<RibbonPropertyChangedEventArgs>(ArcGISRibbon.rsCombo_CurrentChanged));
			ArcGISRibbon.InitializeRasterServiceCombo(ArcGISRibbon._rsCombo);
			ribbonPanelSource.Items.Add(ArcGISRibbon._rsCombo);
			try
			{
				if (AfaDocData.ActiveDocData.CurrentMapService != null)
				{
					ArcGISRibbon.SetActiveRasterService(AfaDocData.ActiveDocData.CurrentMapService);
				}
				else
				{
					ArcGISRibbon.SetActiveRasterService(AfaDocData.ActiveDocData.CurrentImageService);
				}
			}
			catch
			{
			}
		}

		private static bool IsAFeatureService(MSCFeatureClass fc)
		{
			MSCFeatureService mSCFeatureService = fc as MSCFeatureService;
			if (mSCFeatureService != null)
			{
				return true;
			}
			MSCFeatureClassSubType mSCFeatureClassSubType = fc as MSCFeatureClassSubType;
			return mSCFeatureClassSubType != null && ArcGISRibbon.IsAFeatureService(mSCFeatureClassSubType.ParentFC);
		}

		private static MSCFeatureService GetMSCFeatureService(MSCFeatureClass fc)
		{
			MSCFeatureService mSCFeatureService = fc as MSCFeatureService;
			if (mSCFeatureService != null)
			{
				return mSCFeatureService;
			}
			MSCFeatureClassSubType mSCFeatureClassSubType = fc as MSCFeatureClassSubType;
			if (mSCFeatureClassSubType != null)
			{
				return ArcGISRibbon.GetMSCFeatureService(mSCFeatureClassSubType.ParentFC);
			}
			return null;
		}

		public static void EnableFeatureServiceButtons(bool enabled)
		{
			if (ArcGISRibbon._btnSynchronizeAll != null)
			{
				ArcGISRibbon._btnSynchronizeAll.IsEnabled=(enabled);
			}
			if (ArcGISRibbon._btnDiscardAllEdits != null)
			{
				ArcGISRibbon._btnDiscardAllEdits.IsEnabled=(enabled);
			}
			if (ArcGISRibbon._btnSetFeatureServiceLimit != null)
			{
				ArcGISRibbon._btnSetFeatureServiceLimit.IsEnabled=(enabled);
			}
		}

		public static void SetFeatureClassButtonState(MSCFeatureClass fc)
		{
			if (fc != null)
			{
				if (ArcGISRibbon._btnAttributeIdentify != null)
				{
					ArcGISRibbon._btnAttributeIdentify.IsEnabled=(true);
				}
				if (ArcGISRibbon._btnFCProps != null)
				{
					ArcGISRibbon._btnFCProps.IsEnabled=(true);
				}
				if (ArcGISRibbon._btnOpenTable != null)
				{
					ArcGISRibbon._btnOpenTable.IsEnabled=(true);
				}
				if (ArcGISRibbon._btnZoomFeatures != null)
				{
					ArcGISRibbon._btnZoomFeatures.IsEnabled=(true);
				}
				if (ArcGISRibbon._btnSelectObjects != null)
				{
					ArcGISRibbon._btnSelectObjects.IsEnabled=(true);
				}
				if (ArcGISRibbon._btnSelectAll != null)
				{
					ArcGISRibbon._btnSelectAll.IsEnabled=(true);
				}
				if (ArcGISRibbon._btnSelectByAttributes != null)
				{
					ArcGISRibbon._btnSelectByAttributes.IsEnabled=(true);
				}
				if (ArcGISRibbon._btnSetCurrentLayer != null)
				{
					if (fc.IsSingleLayerQuery())
					{
						ArcGISRibbon._btnSetCurrentLayer.IsEnabled=(true);
					}
					else
					{
						ArcGISRibbon._btnSetCurrentLayer.IsEnabled=(false);
					}
				}
				MSCFeatureService mSCFeatureService = ArcGISRibbon.GetMSCFeatureService(fc);
				if (mSCFeatureService != null)
				{
					if (ArcGISRibbon._btnSynchronize != null)
					{
						ArcGISRibbon._btnSynchronize.IsEnabled=(true);
					}
					if (ArcGISRibbon._btnRemoveFS != null)
					{
						ArcGISRibbon._btnRemoveFS.IsEnabled=(true);
					}
					if (ArcGISRibbon._btnDiscardEdits != null)
					{
						ArcGISRibbon._btnDiscardEdits.IsEnabled=(!mSCFeatureService.QueryOnly);
						return;
					}
				}
				else
				{
					if (ArcGISRibbon._btnSynchronize != null)
					{
						ArcGISRibbon._btnSynchronize.IsEnabled=(false);
					}
					if (ArcGISRibbon._btnRemoveFS != null)
					{
						ArcGISRibbon._btnRemoveFS.IsEnabled=(false);
					}
					if (ArcGISRibbon._btnDiscardEdits != null)
					{
						ArcGISRibbon._btnDiscardEdits.IsEnabled=(false);
						return;
					}
				}
			}
			else
			{
				if (ArcGISRibbon._btnAttributeIdentify != null)
				{
					ArcGISRibbon._btnAttributeIdentify.IsEnabled=(false);
				}
				if (ArcGISRibbon._btnFCProps != null)
				{
					ArcGISRibbon._btnFCProps.IsEnabled=(false);
				}
				if (ArcGISRibbon._btnOpenTable != null)
				{
					ArcGISRibbon._btnOpenTable.IsEnabled=(false);
				}
				if (ArcGISRibbon._btnZoomFeatures != null)
				{
					ArcGISRibbon._btnZoomFeatures.IsEnabled=(false);
				}
				if (ArcGISRibbon._btnSelectObjects != null)
				{
					ArcGISRibbon._btnSelectObjects.IsEnabled=(false);
				}
				if (ArcGISRibbon._btnSelectAll != null)
				{
					ArcGISRibbon._btnSelectAll.IsEnabled=(false);
				}
				if (ArcGISRibbon._btnSelectByAttributes != null)
				{
					ArcGISRibbon._btnSelectByAttributes.IsEnabled=(false);
				}
				if (ArcGISRibbon._btnSynchronize != null)
				{
					ArcGISRibbon._btnSynchronize.IsEnabled=(false);
				}
				if (ArcGISRibbon._btnRemoveFS != null)
				{
					ArcGISRibbon._btnRemoveFS.IsEnabled=(false);
				}
				if (ArcGISRibbon._btnDiscardEdits != null)
				{
					ArcGISRibbon._btnDiscardEdits.IsEnabled=(false);
				}
			}
		}

		private static void SetMapButtonState(MSCRasterService rs)
		{
			if (rs.GetType() == typeof(MSCMapService))
			{
				MSCMapService mapButtonState = (MSCMapService)rs;
				ArcGISRibbon.SetMapButtonState(mapButtonState);
				return;
			}
			if (rs.GetType() == typeof(MSCImageService))
			{
				MSCImageService mapButtonState2 = (MSCImageService)rs;
				ArcGISRibbon.SetMapButtonState(mapButtonState2);
			}
		}

		private static void SetButtonState(RibbonButton btn, bool enable)
		{
			if (btn == null)
			{
				return;
			}
			btn.IsEnabled=(enable);
		}

		private static void SetMapButtonState(MSCMapService ms)
		{
			if (ms != null)
			{
				if (ArcGISRibbon._btnMapRemove != null)
				{
					ArcGISRibbon._btnMapRemove.IsEnabled=(true);
				}
				if (ArcGISRibbon._btnMapProperties != null)
				{
					ArcGISRibbon._btnMapProperties.IsEnabled=(true);
				}
				if (ArcGISRibbon._btnToggleVisibility != null)
				{
					ArcGISRibbon._btnToggleVisibility.IsEnabled=(true);
				}
				if (ArcGISRibbon._btnRefreshMap != null)
				{
					ArcGISRibbon._btnRefreshMap.IsEnabled=(true);
				}
				if (ArcGISRibbon._btnSendBehind != null)
				{
					ArcGISRibbon._btnSendBehind.IsEnabled=(true);
				}
				if (ArcGISRibbon._btnSetMapLimit != null)
				{
					ArcGISRibbon._btnSetMapLimit.IsEnabled=(true);
				}
				if (ArcGISRibbon._btnTransparency != null)
				{
					ArcGISRibbon._btnTransparency.IsEnabled=(true);
				}
				if (ArcGISRibbon._btnZoomMapExtents != null)
				{
					ArcGISRibbon._btnZoomMapExtents.IsEnabled=(true);
				}
				try
				{
					if (ArcGISRibbon._btnExportMap != null)
					{
						if (((AGSMapService)ms.ParentService).SupportsDisconnect())
						{
							ArcGISRibbon._btnExportMap.IsEnabled=(true);
						}
						else
						{
							ArcGISRibbon._btnExportMap.IsEnabled=(false);
						}
					}
					if (((AGSMapService)ms.ParentService).SupportsData)
					{
						if (ArcGISRibbon._btnExtractFeatures != null)
						{
							ArcGISRibbon._btnExtractFeatures.IsEnabled=(true);
						}
					}
					else if (ArcGISRibbon._btnExtractFeatures != null)
					{
						ArcGISRibbon._btnExtractFeatures.IsEnabled=(false);
					}
				}
				catch
				{
					if (ArcGISRibbon._btnExtractFeatures != null)
					{
						ArcGISRibbon._btnExtractFeatures.IsEnabled=(false);
					}
				}
				try
				{
					if (((AGSMapService)ms.ParentService).SupportsQuery)
					{
						if (ArcGISRibbon._btnIdentifyMap != null)
						{
							ArcGISRibbon._btnIdentifyMap.IsEnabled=(true);
						}
					}
					else if (ArcGISRibbon._btnIdentifyMap != null)
					{
						ArcGISRibbon._btnIdentifyMap.IsEnabled=(false);
					}
					goto IL_231;
				}
				catch
				{
					if (ArcGISRibbon._btnIdentifyMap != null)
					{
						ArcGISRibbon._btnIdentifyMap.IsEnabled=(false);
					}
					goto IL_231;
				}
			}
			if (ArcGISRibbon._btnExportMap != null)
			{
				ArcGISRibbon._btnExportMap.IsEnabled=(false);
			}
			if (ArcGISRibbon._btnMapRemove != null)
			{
				ArcGISRibbon._btnMapRemove.IsEnabled=(false);
			}
			if (ArcGISRibbon._btnMapProperties != null)
			{
				ArcGISRibbon._btnMapProperties.IsEnabled=(false);
			}
			if (ArcGISRibbon._btnRefreshMap != null)
			{
				ArcGISRibbon._btnRefreshMap.IsEnabled=(false);
			}
			if (ArcGISRibbon._btnTransparency != null)
			{
				ArcGISRibbon._btnTransparency.IsEnabled=(false);
			}
			if (ArcGISRibbon._btnSendBehind != null)
			{
				ArcGISRibbon._btnSendBehind.IsEnabled=(false);
			}
			if (ArcGISRibbon._btnSetMapLimit != null)
			{
				ArcGISRibbon._btnSetMapLimit.IsEnabled=(false);
			}
			if (ArcGISRibbon._btnZoomMapExtents != null)
			{
				ArcGISRibbon._btnZoomMapExtents.IsEnabled=(false);
			}
			if (ArcGISRibbon._btnExtractFeatures != null)
			{
				ArcGISRibbon._btnExtractFeatures.IsEnabled=(false);
			}
			if (ArcGISRibbon._btnIdentifyMap != null)
			{
				ArcGISRibbon._btnIdentifyMap.IsEnabled=(false);
			}
			if (ArcGISRibbon._btnToggleVisibility != null)
			{
				ArcGISRibbon._btnToggleVisibility.IsEnabled=(false);
			}
			IL_231:
			if (ArcGISRibbon._panelMaps.RibbonControl!= null)
			{
				ArcGISRibbon._panelMaps.RibbonControl.UpdateLayout();
			}
		}

		private static void SetMapButtonState(MSCImageService rs)
		{
			if (rs != null)
			{
				if (ArcGISRibbon._btnExportMap != null)
				{
					ArcGISRibbon._btnExportMap.IsEnabled=(true);
				}
				if (ArcGISRibbon._btnMapRemove != null)
				{
					ArcGISRibbon._btnMapRemove.IsEnabled=(true);
				}
				if (ArcGISRibbon._btnMapProperties != null)
				{
					ArcGISRibbon._btnMapProperties.IsEnabled=(true);
				}
				if (ArcGISRibbon._btnTransparency != null)
				{
					ArcGISRibbon._btnTransparency.IsEnabled=(true);
				}
				if (ArcGISRibbon._btnRefreshMap != null)
				{
					ArcGISRibbon._btnRefreshMap.IsEnabled=(true);
				}
				if (ArcGISRibbon._btnSendBehind != null)
				{
					ArcGISRibbon._btnSendBehind.IsEnabled=(true);
				}
				if (ArcGISRibbon._btnSetMapLimit != null)
				{
					ArcGISRibbon._btnSetMapLimit.IsEnabled=(true);
				}
				if (ArcGISRibbon._btnZoomMapExtents != null)
				{
					ArcGISRibbon._btnZoomMapExtents.IsEnabled=(true);
				}
				if (ArcGISRibbon._btnExtractFeatures != null)
				{
					ArcGISRibbon._btnExtractFeatures.IsEnabled=(false);
				}
				if (ArcGISRibbon._btnIdentifyMap != null)
				{
					ArcGISRibbon._btnIdentifyMap.IsEnabled=(false);
				}
				if (ArcGISRibbon._btnToggleVisibility != null)
				{
					ArcGISRibbon._btnToggleVisibility.IsEnabled=(true);
				}
			}
			else
			{
				if (ArcGISRibbon._btnExportMap != null)
				{
					ArcGISRibbon._btnExportMap.IsEnabled=(false);
				}
				if (ArcGISRibbon._btnMapRemove != null)
				{
					ArcGISRibbon._btnMapRemove.IsEnabled=(false);
				}
				if (ArcGISRibbon._btnMapProperties != null)
				{
					ArcGISRibbon._btnMapProperties.IsEnabled=(false);
				}
				if (ArcGISRibbon._btnRefreshMap != null)
				{
					ArcGISRibbon._btnRefreshMap.IsEnabled=(false);
				}
				if (ArcGISRibbon._btnSendBehind != null)
				{
					ArcGISRibbon._btnSendBehind.IsEnabled=(false);
				}
				if (ArcGISRibbon._btnTransparency != null)
				{
					ArcGISRibbon._btnTransparency.IsEnabled=(false);
				}
				if (ArcGISRibbon._btnSetMapLimit != null)
				{
					ArcGISRibbon._btnSetMapLimit.IsEnabled=(false);
				}
				if (ArcGISRibbon._btnZoomMapExtents != null)
				{
					ArcGISRibbon._btnZoomMapExtents.IsEnabled=(false);
				}
				if (ArcGISRibbon._btnExtractFeatures != null)
				{
					ArcGISRibbon._btnExtractFeatures.IsEnabled=(false);
				}
				if (ArcGISRibbon._btnIdentifyMap != null)
				{
					ArcGISRibbon._btnIdentifyMap.IsEnabled=(false);
				}
				if (ArcGISRibbon._btnToggleVisibility != null)
				{
					ArcGISRibbon._btnToggleVisibility.IsEnabled=(false);
				}
			}
			if (ArcGISRibbon._panelMaps.RibbonControl != null)
			{
				ArcGISRibbon._panelMaps.RibbonControl.UpdateLayout();
			}
		}

		private static void rsCombo_CurrentChanged(object sender, RibbonPropertyChangedEventArgs e)
		{
			RibbonCombo ribbonCombo = sender as RibbonCombo;
			RibbonLabel ribbonLabel = ribbonCombo.Current as RibbonLabel;
			if (ribbonLabel == null)
			{
                ArcGISRibbon.SetMapButtonState((MSCRasterService)null);
				return;
			}
			MSCRasterService mSCRasterService = (MSCRasterService)ribbonLabel.Tag;
			if (mSCRasterService == null)
			{
				AfaDocData.ActiveDocData.CurrentMapService = null;
				AfaDocData.ActiveDocData.CurrentImageService = null;
				ArcGISRibbon.SetMapButtonState((MSCRasterService)null);
				return;
			}
			if (typeof(MSCMapService) == mSCRasterService.GetType())
			{
				MSCMapService mSCMapService = (MSCMapService)mSCRasterService;
				AfaDocData.ActiveDocData.CurrentMapService = mSCMapService;
				ArcGISRibbon.SetMapButtonState(mSCMapService);
				return;
			}
			if (typeof(MSCImageService) == mSCRasterService.GetType())
			{
				AfaDocData.ActiveDocData.CurrentImageService = (MSCImageService)mSCRasterService;
				ArcGISRibbon.SetMapButtonState((MSCImageService)mSCRasterService);
			}
		}

		private static void RasterServiceCombo_DropDownOpened(object sender, EventArgs e)
		{
			RibbonCombo ribbonCombo = sender as RibbonCombo;
			ribbonCombo.Items.Clear();
			MSCRasterService mSCRasterService = AfaDocData.ActiveDocData.CurrentMapService;
			if (mSCRasterService == null)
			{
				mSCRasterService = AfaDocData.ActiveDocData.CurrentImageService;
			}
			foreach (MSCRasterService current in AfaDocData.ActiveDocData.DocDataset.MapServices.Values)
			{
				RibbonLabel ribbonLabel = new RibbonLabel();
				ribbonLabel.Tag=(current);
				ribbonLabel.Text=(current.Name);
				ribbonCombo.Items.Add(ribbonLabel);
				if (current == mSCRasterService)
				{
					ribbonCombo.Current=(ribbonLabel);
				}
			}
			foreach (MSCRasterService current2 in AfaDocData.ActiveDocData.DocDataset.ImageServices.Values)
			{
				RibbonLabel ribbonLabel2 = new RibbonLabel();
				ribbonLabel2.Tag=(current2);
				ribbonLabel2.Text=(current2.Name);
				ribbonCombo.Items.Add(ribbonLabel2);
				if (current2 == mSCRasterService)
				{
					ribbonCombo.Current=(ribbonLabel2);
				}
			}
		}

		private static void addButtonsPanelAdd(RibbonPanelSource ribbonPanelSource)
		{
			ArcGISRibbon.addLargeButton(ribbonPanelSource, AfaStrings.RBN_BTN_ESRIMAPS, "img_ESRIMaps_large", "", AfaStrings.RBN_BTN_ESRIMAPS, ArcGISRibbon.createToolTip(AfaStrings.RBN_BTN_ESRIMAPS_TOOLTIP_TITLE, AfaStrings.RBN_BTN_ESRIMAPS_TOOLTIP), "");
			ArcGISRibbon.addLargeButton(ribbonPanelSource, AfaStrings.RBN_BTN_ADDMAPSERVICE, "img_AddMapService_large", "", AfaStrings.RBN_BTN_ADDMAPSERVICE, ArcGISRibbon.createToolTip(AfaStrings.RBN_BTN_ADDMAPSERVICE_TOOLTIP_TITLE, AfaStrings.RBN_BTN_ADDMAPSERVICE_TOOLTIP), "");
			RibbonRowPanel ribbonRowPanel = new RibbonRowPanel();
			ribbonRowPanel.Name=(AfaStrings.RBN_PANEL_ADD);
			ribbonPanelSource.Items.Add(ribbonRowPanel);
		}

		private static void addButtonsPanelManage(RibbonPanelSource ribbonPanelSource)
		{
			ArcGISRibbon.addLargeButton(ribbonPanelSource, AfaStrings.RBN_BTN_TOC, "img_toc_large", "", AfaStrings.RBN_BTN_TOC, ArcGISRibbon.createToolTip(AfaStrings.RBN_BTN_TOC_TOOLTIP_TITLE, AfaStrings.RBN_BTN_TOC_TOOLTIP), "");
			RibbonRowPanel ribbonRowPanel = new RibbonRowPanel();
			ribbonRowPanel.Name=(AfaStrings.RBN_PANEL_MANAGE);
			RibbonButton item = ArcGISRibbon.createSmallButton(AfaStrings.RBN_BTN_LAYER_MANAGER, "img_layermanager_small", AfaStrings.RBN_BTN_LAYER_MANAGER, ArcGISRibbon.createToolTip(AfaStrings.RBN_BTN_LAYER_MANAGER_TOOLTIP_TITLE, AfaStrings.RBN_BTN_LAYER_MANAGER_TOOLTIP), "");
			ribbonRowPanel.Items.Add(item);
			ArcGISRibbon.addRowBreak(ribbonRowPanel);
			btnImportSchema item2 = new btnImportSchema();
			ribbonRowPanel.Items.Add(item2);
			ArcGISRibbon.addRowBreak(ribbonRowPanel);
			ArcGISRibbon.addCSMenu(ribbonRowPanel);
			ribbonPanelSource.Items.Add(ribbonRowPanel);
		}

		private static void addButtonsPanelNavigate(RibbonPanelSource ribbonPanelSource)
		{
			ArcGISRibbon._btnGeolocate = new btnGeolocate();
			ribbonPanelSource.Items.Add(ArcGISRibbon._btnGeolocate);
			ArcGISRibbon.addLargeButton(ribbonPanelSource, AfaStrings.RBN_BTN_ZOOMEXTENTS, "Zoom_Map_Extent32", "", AfaStrings.RBN_BTN_ZOOMEXTENTS, ArcGISRibbon.createToolTip(AfaStrings.RBN_BTN_ZOOMEXTENTS_TOOLTIP_TITLE, AfaStrings.RBN_BTN_ZOOMEXTENTS_TOOLTIP), "");
			RibbonRowPanel ribbonRowPanel = new RibbonRowPanel();
			ribbonRowPanel.Name=(AfaStrings.RBN_PANEL_NAVIGATE);
			RibbonButton item = ArcGISRibbon.createSmallButton(AfaStrings.RBN_BTN_ZOOMPREVIOUS, "img_zoomprevious_small", AfaStrings.RBN_BTN_ZOOMPREVIOUS, ArcGISRibbon.createToolTip(AfaStrings.RBN_BTN_ZOOMPREVIOUS_TOOLTIP_TITLE, AfaStrings.RBN_BTN_ZOOMPREVIOUS_TOOLTIP), "");
			ribbonRowPanel.Items.Add(item);
			ArcGISRibbon.addRowBreak(ribbonRowPanel);
			btnPan item2 = new btnPan();
			ribbonRowPanel.Items.Add(item2);
			ArcGISRibbon.addRowBreak(ribbonRowPanel);
			RibbonButton item3 = ArcGISRibbon.createSmallButton(AfaStrings.RBN_BTN_ZOOMWINDOW, "img_zoomwindow_small", AfaStrings.RBN_BTN_ZOOMWINDOW, ArcGISRibbon.createToolTip(AfaStrings.RBN_BTN_ZOOMWINDOW_TOOLTIP_TITLE, AfaStrings.RBN_BTN_ZOOMWINDOW_TOOLTIP), "");
			ribbonRowPanel.Items.Add(item3);
			ribbonPanelSource.Items.Add(ribbonRowPanel);
		}

		private static void addButtonsPanelFeatures(RibbonPanelSource ribbonPanelSource)
		{
			ArcGISRibbon._btnAttributeIdentify = new btnAttributeIdentify();
			ribbonPanelSource.Items.Add(ArcGISRibbon._btnAttributeIdentify);
			RibbonRowPanel ribbonRowPanel = new RibbonRowPanel();
			ribbonRowPanel.Name=(AfaStrings.RBN_PANEL_FEATURES);
			ArcGISRibbon._fcCombo = new comboFeatureClass();
			ribbonRowPanel.Items.Add(ArcGISRibbon._fcCombo);
			ArcGISRibbon._fcCombo.Initialize();
			RibbonSeparator ribbonSeparator = new RibbonSeparator();
			ribbonSeparator.SeparatorStyle=(RibbonSeparatorStyle)(1);
			ribbonRowPanel.Items.Add(ribbonSeparator);
			RibbonSeparator ribbonSeparator2 = new RibbonSeparator();
			ribbonSeparator2.SeparatorStyle=(RibbonSeparatorStyle)(1);
			ribbonRowPanel.Items.Add(ribbonSeparator2);
			ArcGISRibbon._btnOpenTable = new btnOpenTable();
			ribbonRowPanel.Items.Add(ArcGISRibbon._btnOpenTable);
			ArcGISRibbon._btnSelectByAttributes = new btnSelectByAttributes();
			ribbonRowPanel.Items.Add(ArcGISRibbon._btnSelectByAttributes);
			ArcGISRibbon._btnSelectNone = new btnSelectNone();
			ribbonRowPanel.Items.Add(ArcGISRibbon._btnSelectNone);
			RibbonSeparator ribbonSeparator3 = new RibbonSeparator();
			ribbonSeparator3.SeparatorStyle=(RibbonSeparatorStyle)(0);
			ribbonRowPanel.Items.Add(ribbonSeparator3);
			ArcGISRibbon._btnFCProps = new btnFCProps();
			ribbonRowPanel.Items.Add(ArcGISRibbon._btnFCProps);
			RibbonSeparator ribbonSeparator4 = new RibbonSeparator();
			ribbonSeparator4.SeparatorStyle=(RibbonSeparatorStyle)(1);
			ribbonRowPanel.Items.Add(ribbonSeparator4);
			RibbonSeparator ribbonSeparator5 = new RibbonSeparator();
			ribbonSeparator5.SeparatorStyle=(RibbonSeparatorStyle)(0);
			ribbonRowPanel.Items.Add(ribbonSeparator5);
			RibbonSeparator ribbonSeparator6 = new RibbonSeparator();
			ribbonSeparator6.SeparatorStyle=(RibbonSeparatorStyle)(1);
			ribbonRowPanel.Items.Add(ribbonSeparator6);
			ArcGISRibbon._btnRemoveFS = new btnRemoveFeatureService();
			ribbonRowPanel.Items.Add(ArcGISRibbon._btnRemoveFS);
			ArcGISRibbon._btnDiscardEdits = new btnDiscardEdits();
			ribbonRowPanel.Items.Add(ArcGISRibbon._btnDiscardEdits);
			ArcGISRibbon.addRowBreak(ribbonRowPanel);
			ArcGISRibbon._stCombo = new comboSubtypes();
			ribbonRowPanel.Items.Add(ArcGISRibbon._stCombo);
			ArcGISRibbon._stCombo.Initialize();
			RibbonSeparator ribbonSeparator7 = new RibbonSeparator();
			ribbonSeparator7.SeparatorStyle=(RibbonSeparatorStyle)(1);
			ribbonRowPanel.Items.Add(ribbonSeparator7);
			RibbonSeparator ribbonSeparator8 = new RibbonSeparator();
			ribbonSeparator8.SeparatorStyle=(RibbonSeparatorStyle)(1);
			ribbonRowPanel.Items.Add(ribbonSeparator8);
			ArcGISRibbon._btnZoomFeatures = new btnZoomFeatures();
			ribbonRowPanel.Items.Add(ArcGISRibbon._btnZoomFeatures);
			ArcGISRibbon._btnSelectObjects = new btnSelectObjects();
			ribbonRowPanel.Items.Add(ArcGISRibbon._btnSelectObjects);
			ArcGISRibbon._btnSelectAll = new btnSelectAll();
			ribbonRowPanel.Items.Add(ArcGISRibbon._btnSelectAll);
			RibbonSeparator ribbonSeparator9 = new RibbonSeparator();
			ribbonSeparator9.SeparatorStyle=(RibbonSeparatorStyle)(0);
			ribbonRowPanel.Items.Add(ribbonSeparator9);
			ArcGISRibbon._btnNewFeatureClass = new btnNewFeatureClass();
			ribbonRowPanel.Items.Add(ArcGISRibbon._btnNewFeatureClass);
			RibbonSeparator ribbonSeparator10 = new RibbonSeparator();
			ribbonSeparator10.SeparatorStyle=(RibbonSeparatorStyle)(1);
			ribbonRowPanel.Items.Add(ribbonSeparator10);
			RibbonSeparator ribbonSeparator11 = new RibbonSeparator();
			ribbonSeparator11.SeparatorStyle= (RibbonSeparatorStyle)(0);
			ribbonRowPanel.Items.Add(ribbonSeparator11);
			RibbonSeparator ribbonSeparator12 = new RibbonSeparator();
			ribbonSeparator12.SeparatorStyle=(RibbonSeparatorStyle)(1);
			ribbonRowPanel.Items.Add(ribbonSeparator12);
			ArcGISRibbon._btnSetCurrentLayer = new btnSetCurrentLayer();
			ribbonRowPanel.Items.Add(ArcGISRibbon._btnSetCurrentLayer);
			ArcGISRibbon._btnSynchronize = new btnSynchronize();
			ribbonRowPanel.Items.Add(ArcGISRibbon._btnSynchronize);
			ArcGISRibbon.addRowBreak(ribbonRowPanel);
			ribbonPanelSource.Items.Add(ribbonRowPanel);
			ribbonPanelSource.Items.Add(new RibbonSeparator());
			ArcGISRibbon._btnSynchronizeAll = new btnSynchronizeAll();
			ribbonPanelSource.Items.Add(ArcGISRibbon._btnSynchronizeAll);
			RibbonRowPanel ribbonRowPanel2 = new RibbonRowPanel();
			ribbonRowPanel2.Name=(AfaStrings.RBN_PANEL_FEATURES);
			ArcGISRibbon._btnDiscardAllEdits = new btnDiscardAllEdits();
			ribbonRowPanel2.Items.Add(ArcGISRibbon._btnDiscardAllEdits);
			ArcGISRibbon.addRowBreak(ribbonRowPanel2);
			ArcGISRibbon._btnSetFeatureServiceLimit = new btnSetFeatureServiceLimit();
			ribbonRowPanel2.Items.Add(ArcGISRibbon._btnSetFeatureServiceLimit);
			ribbonPanelSource.Items.Add(ribbonRowPanel2);
		}

		private static void addButtonsPanelMaps(RibbonPanelSource ribbonPanelSource)
		{
			ArcGISRibbon._btnIdentifyMap = new btnIdentifyMap();
			ribbonPanelSource.Items.Add(ArcGISRibbon._btnIdentifyMap);
			RibbonRowPanel ribbonRowPanel = new RibbonRowPanel();
			ribbonRowPanel.Name=(AfaStrings.RBN_PANEL_MAPS);
			ArcGISRibbon.addRSCombo(ribbonRowPanel, AfaStrings.RBN_COMBO_MAPS, AfaStrings.RBN_COMBO_MAPS, ArcGISRibbon.createToolTip(AfaStrings.RBN_COMBO_MAPS_TOOLTIP_TITLE, AfaStrings.RBN_COMBO_MAPS_TOOLTIP));
			ArcGISRibbon.addRowBreak(ribbonRowPanel);
			ArcGISRibbon._btnSetMapLimit = new btnSetMapLimit();
			ribbonRowPanel.Items.Add(ArcGISRibbon._btnSetMapLimit);
			ArcGISRibbon._btnExportMap = new btnExportMap();
			ribbonRowPanel.Items.Add(ArcGISRibbon._btnExportMap);
			ArcGISRibbon._btnMapRemove = new btnRemoveMap();
			ribbonRowPanel.Items.Add(ArcGISRibbon._btnMapRemove);
			ArcGISRibbon._btnMapProperties = new btnMapProperties();
			ribbonRowPanel.Items.Add(ArcGISRibbon._btnMapProperties);
			ArcGISRibbon.addRowBreak(ribbonRowPanel);
			ArcGISRibbon._btnZoomMapExtents = new btnZoomMapExtents();
			ribbonRowPanel.Items.Add(ArcGISRibbon._btnZoomMapExtents);
			ArcGISRibbon._btnToggleVisibility = new btnToggleVisibility();
			ribbonRowPanel.Items.Add(ArcGISRibbon._btnToggleVisibility);
			ArcGISRibbon._btnRefreshMap = new btnRefreshMap();
			ribbonRowPanel.Items.Add(ArcGISRibbon._btnRefreshMap);
			ArcGISRibbon._btnTransparency = new btnTransparency();
			ribbonRowPanel.Items.Add(ArcGISRibbon._btnTransparency);
			ribbonPanelSource.Items.Add(ribbonRowPanel);
		}

		private static void addButtonsPanelResources(RibbonPanelSource ribbonPanelSource)
		{
			ArcGISRibbon.addLargeButton(ribbonPanelSource, AfaStrings.RBN_BTN_HELP, "img_help_large", "", AfaStrings.RBN_BTN_HELP, ArcGISRibbon.createToolTip(AfaStrings.RBN_BTN_HELP_TOOLTIP_TITLE, AfaStrings.RBN_BTN_HELP_TOOLTIP), "");
			RibbonRowPanel ribbonRowPanel = new RibbonRowPanel();
			ribbonRowPanel.Name=(AfaStrings.RBN_PANEL_RESOURCES);
			RibbonButton item = ArcGISRibbon.createSmallButton(AfaStrings.RBN_BTN_RESOURCECENTER, "img_resourcecenter_small", AfaStrings.RBN_BTN_RESOURCECENTER, ArcGISRibbon.createToolTip(AfaStrings.RBN_BTN_RESOURCECENTER_TOOLTIP_TITLE, AfaStrings.RBN_BTN_RESOURCECENTER_TOOLTIP), "");
			ribbonRowPanel.Items.Add(item);
			ArcGISRibbon.addRowBreak(ribbonRowPanel);
			RibbonButton item2 = ArcGISRibbon.createSmallButton(AfaStrings.RBN_BTN_ABOUT, "img_about_small", AfaStrings.RBN_BTN_ABOUT, ArcGISRibbon.createToolTip(AfaStrings.RBN_BTN_ABOUT_TOOLTIP_TITLE, AfaStrings.RBN_BTN_ABOUT_TOOLTIP), "");
			ribbonRowPanel.Items.Add(item2);
			ribbonPanelSource.Items.Add(ribbonRowPanel);
		}

		public static BitmapImage loadImage(System.Drawing.Image myImage)
		{
			if (myImage == null)
			{
				return null;
			}
			BitmapImage bitmapImage = new BitmapImage();
			bitmapImage.BeginInit();
			MemoryStream memoryStream = new MemoryStream();
			myImage.Save(memoryStream, ImageFormat.Png);
			memoryStream.Seek(0L, SeekOrigin.Begin);
			bitmapImage.StreamSource = memoryStream;
			bitmapImage.EndInit();
			return bitmapImage;
		}

		public static BitmapImage loadImage(string imageName)
		{
			BitmapImage result;
			try
			{
				ResourceManager resourceManager = ImageResources.ResourceManager;
				object @object = resourceManager.GetObject(imageName);
				System.Drawing.Image myImage = (System.Drawing.Image)@object;
				result = ArcGISRibbon.loadImage(myImage);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static void RefreshUI()
		{
			ArcGISRibbon.reInitialize();
		}

		public static ResultBuffer ChangeCulture(ResultBuffer buf)
		{
			ResultBuffer result;
			try
			{
				Array array = buf.AsArray();
				int length = array.Length;
				if (length < 1)
				{
					result = new ResultBuffer(new TypedValue[]
					{
						new TypedValue(5019, false)
					});
				}
				else
				{
					string name = Convert.ToString(((TypedValue)array.GetValue(0)).Value);
					Thread.CurrentThread.CurrentCulture = new CultureInfo(name);
					ArcGISRibbon.reInitialize();
					result = new ResultBuffer(new TypedValue[]
					{
						new TypedValue(5019, true)
					});
				}
			}
			catch (SystemException ex)
			{
				string arg_AC_0 = ex.Message;
				result = new ResultBuffer(new TypedValue[]
				{
					new TypedValue(5019, false)
				});
			}
			return result;
		}
	}
}
