using AFA.Resources;
using Autodesk.AutoCAD.ApplicationServices;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace AFA.UI
{
    public class TOCControl : UserControl, IComponentConnector, IStyleConnector
	{
		internal Expander exFeatureClasses;

		internal TreeView tvFeatureClasses;

		internal Expander exFeatureServices;

		internal TreeView tvFeatureServices;

		internal Expander exMapServices;

		internal TreeView tvMapServices;

		internal Expander exImageServices;

		internal TreeView tvImageServices;

		private bool _contentLoaded;

		public TOCControl()
		{
			try
			{
				this.InitializeComponent();
				this.tvFeatureClasses.ItemsSource = AfaDocData.ActiveDocData.DocDataset.FeatureClassViewList;
				this.tvFeatureServices.ItemsSource = AfaDocData.ActiveDocData.DocDataset.FeatureServiceViewList;
				this.tvMapServices.ItemsSource = AfaDocData.ActiveDocData.DocDataset.MapServices;
				this.tvImageServices.ItemsSource = AfaDocData.ActiveDocData.DocDataset.ImageServices;
				Application.DocumentManager.DocumentBecameCurrent+=(new DocumentCollectionEventHandler(this.DocumentManager_DocumentBecameCurrent));
				Application.DocumentManager.DocumentToBeDestroyed+=(new DocumentCollectionEventHandler(this.DocumentManager_DocumentToBeDestroyed));
			}
			catch
			{
			}
		}

		public void OnClosing(CancelEventArgs e)
		{
			try
			{
				this.tvFeatureClasses.ItemsSource = null;
				this.tvFeatureServices.ItemsSource = null;
				this.tvMapServices.ItemsSource = null;
				this.tvImageServices.ItemsSource = null;
				Application.DocumentManager.DocumentBecameCurrent-=(new DocumentCollectionEventHandler(this.DocumentManager_DocumentBecameCurrent));
				Application.DocumentManager.DocumentToBeDestroyed-=(new DocumentCollectionEventHandler(this.DocumentManager_DocumentToBeDestroyed));
				GC.Collect();
			}
			catch
			{
			}
		}

		private void DocumentManager_DocumentBecameCurrent(object sender, DocumentCollectionEventArgs e)
		{
			try
			{
				if (AfaDocData.ActiveDocData.DocDataset != null)
				{
					this.tvFeatureClasses.ItemsSource = AfaDocData.ActiveDocData.DocDataset.FeatureClassViewList;
					this.tvFeatureServices.ItemsSource = AfaDocData.ActiveDocData.DocDataset.FeatureServiceViewList;
					this.tvMapServices.ItemsSource = AfaDocData.ActiveDocData.DocDataset.MapServices;
					this.tvImageServices.ItemsSource = AfaDocData.ActiveDocData.DocDataset.ImageServices;
				}
			}
			catch
			{
				this.tvFeatureClasses.ItemsSource = null;
				this.tvFeatureServices.ItemsSource = null;
				this.tvMapServices.ItemsSource = null;
				this.tvImageServices.ItemsSource = null;
			}
		}

		private void DocumentManager_DocumentToBeDestroyed(object sender, DocumentCollectionEventArgs e)
		{
			try
			{
				this.tvFeatureClasses.ItemsSource = null;
				this.tvFeatureServices.ItemsSource = null;
				this.tvMapServices.ItemsSource = null;
				this.tvImageServices.ItemsSource = null;
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}
			catch
			{
			}
		}

		private void OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			try
			{
				if (e.NewValue != null)
				{
					if (e.NewValue is FCView)
					{
						this.SetActiveFeatureClass(e.NewValue);
						e.Handled = true;
					}
					if (e.NewValue is FCTag)
					{
						this.SetActiveFeatureClass(e.NewValue);
						e.Handled = true;
					}
					if (e.NewValue is MSCFeatureClass || e.NewValue is MSCFeatureService || e.NewValue is MSCFeatureClassSubType)
					{
						this.SetActiveFeatureClass(e.NewValue);
						e.Handled = true;
					}
					else if (e.NewValue.GetType() == typeof(KeyValuePair<string, MSCFeatureClass>))
					{
						this.SetActiveFeatureClass(e.NewValue);
						e.Handled = true;
					}
					else if (e.NewValue.GetType() == typeof(KeyValuePair<string, MSCFeatureService>))
					{
						this.SetActiveFeatureClass(e.NewValue);
						e.Handled = true;
					}
					else if (e.NewValue.GetType() == typeof(KeyValuePair<string, MSCMapService>))
					{
						this.SetActiveRaster(e.NewValue);
						e.Handled = true;
					}
					else if (e.NewValue.GetType() == typeof(MSCMapLayer))
					{
						this.SetActiveRaster(e.NewValue);
						e.Handled = true;
					}
					else if (e.NewValue.GetType() == typeof(KeyValuePair<string, MSCImageService>))
					{
						this.SetActiveRaster(e.NewValue);
						e.Handled = true;
					}
				}
			}
			catch
			{
			}
		}

		private void tvImageServices_MouseUp(object sender, MouseButtonEventArgs e)
		{
			try
			{
				object selectedItem = this.tvImageServices.SelectedItem;
				this.SetActiveRaster(selectedItem);
			}
			catch
			{
			}
		}

		private void tvMapServices_MouseUp(object sender, MouseButtonEventArgs e)
		{
			try
			{
				object selectedItem = this.tvMapServices.SelectedItem;
				this.SetActiveRaster(selectedItem);
			}
			catch
			{
			}
		}

		private void OnSelectedMapChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			try
			{
				if (e.NewValue != null)
				{
					if (e.NewValue.GetType() == typeof(KeyValuePair<string, MSCMapService>))
					{
						KeyValuePair<string, MSCMapService> keyValuePair = (KeyValuePair<string, MSCMapService>)e.NewValue;
						AfaDocData.ActiveDocData.CurrentMapService = keyValuePair.Value;
						ArcGISRibbon.SetActiveRasterService(keyValuePair.Value);
						keyValuePair.Value.CheckForUpdates();
					}
					else if (e.NewValue.GetType() == typeof(KeyValuePair<string, MSCImageService>))
					{
						KeyValuePair<string, MSCImageService> keyValuePair2 = (KeyValuePair<string, MSCImageService>)e.NewValue;
						AfaDocData.ActiveDocData.CurrentImageService = keyValuePair2.Value;
						ArcGISRibbon.SetActiveRasterService(keyValuePair2.Value);
						keyValuePair2.Value.CheckForUpdates();
					}
					else if (e.NewValue.GetType() == typeof(MSCMapLayer))
					{
						MSCMapLayer mSCMapLayer = (MSCMapLayer)e.NewValue;
						ArcGISRibbon.SetActiveRasterService(mSCMapLayer.ParentMap);
						AfaDocData.ActiveDocData.CurrentMapService = mSCMapLayer.ParentMap;
					}
				}
			}
			catch
			{
			}
		}

		private void OnRemoveAllFeatureClasses_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
				if (docDataset.FeatureClasses.Count == 0)
				{
					AfaDocData.ActiveDocData.Document.Editor.WriteMessage(AfaStrings.NoFeatureClassesFound);
				}
				else
				{
					MSCFeatureClass[] array = docDataset.FeatureClasses.Values.ToArray<MSCFeatureClass>();
					MSCFeatureClass[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						MSCFeatureClass fc = array2[i];
						docDataset.RemoveFeatureClass(fc);
					}
					MSCDataset.SetDefaultActiveFeatureClass();
				}
			}
			catch
			{
				MSCDataset.SetDefaultActiveFeatureClass();
			}
		}

		private void OnSelectAllClicked(object sender, RoutedEventArgs e)
		{
			CmdLine.CancelActiveCommand();
			string cmdString = ".ESRI_SelectFeatures";
			CmdLine.ExecuteQuietCommand(cmdString);
		}

		private void OnOpenAttributeTable(object sender, RoutedEventArgs e)
		{
			CmdLine.CancelActiveCommand();
			string cmdString = ".ESRI_Table";
			CmdLine.ExecuteQuietCommand(cmdString);
		}

		private void OnZoomToExtents(object sender, RoutedEventArgs e)
		{
			CmdLine.CancelActiveCommand();
			string cmdString = ".ESRI_ZoomFeatures";
			CmdLine.ExecuteQuietCommand(cmdString);
		}

		private void OnSelectByAttributes(object sender, RoutedEventArgs e)
		{
			CmdLine.CancelActiveCommand();
			string cmdString = ".ESRI_SelectByAttribute";
			CmdLine.ExecuteQuietCommand(cmdString);
		}

		private void OnZoomToSelected(object sender, RoutedEventArgs e)
		{
			CmdLine.CancelActiveCommand();
			string cmdString = ".ESRI_ZoomSelected";
			CmdLine.ExecuteQuietCommand(cmdString);
		}

		private void OnClearSelection(object sender, RoutedEventArgs e)
		{
			CmdLine.CancelActiveCommand();
			string cmdString = ".ESRI_ClearSelected";
			CmdLine.ExecuteQuietCommand(cmdString);
		}

		private void OnOpenTableSelected(object sender, RoutedEventArgs e)
		{
			CmdLine.CancelActiveCommand();
			string cmdString = ".esri_TableSelected";
			CmdLine.ExecuteQuietCommand(cmdString);
		}

		private void OnDeleteFeatureClass(object sender, RoutedEventArgs e)
		{
			CmdLine.CancelActiveCommand();
			string cmdString = ".ESRI_DeleteFeatureClass";
			CmdLine.ExecuteQuietCommand(cmdString);
		}

		private void OnOpenFeatureClassProperties(object sender, RoutedEventArgs e)
		{
			CmdLine.CancelActiveCommand();
			string cmdString = ".esri_FeatureClassProperties ";
			CmdLine.ExecuteQuietCommand(cmdString);
		}

		private void OnOpenFeatureServiceProperties(object sender, RoutedEventArgs e)
		{
			CmdLine.CancelActiveCommand();
			string cmdString = ".esri_FeatureClassProperties";
			CmdLine.ExecuteQuietCommand(cmdString);
		}

		private void OnExtractFeatureService(object sender, RoutedEventArgs e)
		{
			CmdLine.CancelActiveCommand();
			string cmdString = ".ESRI_DisconnectFeatureService";
			CmdLine.ExecuteQuietCommand(cmdString);
		}

		private void OnRefreshFeatureService(object sender, RoutedEventArgs e)
		{
			CmdLine.CancelActiveCommand();
			string cmdString = ".esri_Synchronize";
			CmdLine.ExecuteQuietCommand(cmdString);
		}

		private void OnDeleteFeatureService(object sender, RoutedEventArgs e)
		{
			string cmdString = ".ESRI_DeleteFeatureService";
			CmdLine.ExecuteQuietCommand(cmdString);
		}

		private void OnDiscardEdits(object sender, RoutedEventArgs e)
		{
			string text = "esri_DiscardEdits ";
			AfaDocData.ActiveDocData.Document.SendStringToExecute(text, true, false, false);
		}

		private void OnSetCurrentDrawingLayer(object sender, RoutedEventArgs e)
		{
			string text = "ESRI_SetCurrentDrawingLayer ";
			AfaDocData.ActiveDocData.Document.SendStringToExecute(text, true, false, false);
		}

		private void OnDiscardAllEdits(object sender, RoutedEventArgs e)
		{
			string text = "esri_DiscardAllEdits ";
			AfaDocData.ActiveDocData.Document.SendStringToExecute(text, true, false, false);
		}

		private void OnAddNewService(object sender, RoutedEventArgs e)
		{
			string text = "_ESRI_AddService ";
			AfaDocData.ActiveDocData.Document.SendStringToExecute(text, true, false, false);
		}

		private void OnAddNewFeatureClass(object sender, RoutedEventArgs e)
		{
			string text = ".ESRI_NewFeatureClass ";
			AfaDocData.ActiveDocData.Document.SendStringToExecute(text, true, false, false);
		}

		private void OnUpdateAll(object sender, RoutedEventArgs e)
		{
			string text = "esri_SynchronizeAll ";
			AfaDocData.ActiveDocData.Document.SendStringToExecute(text, true, false, false);
		}

		private void OnExtractAllFeatureServices_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
				if (docDataset.FeatureServices.Count == 0)
				{
					AfaDocData.ActiveDocData.Document.Editor.WriteMessage(AfaStrings.NoFeatureServicesFound);
				}
				else
				{
					List<string> list = new List<string>();
					foreach (MSCFeatureService current in docDataset.FeatureServices.Values)
					{
						string name = current.Name;
						MSCFeatureClass mSCFeatureClass = current.Disconnect();
						if (mSCFeatureClass != null)
						{
							list.Add(name);
						}
					}
					foreach (string current2 in list)
					{
						docDataset.FeatureServices.Remove(current2);
					}
					MSCDataset.SetDefaultActiveFeatureClass();
					ToolPalette.UpdatePalette(docDataset.ParentDocument, docDataset, false);
				}
			}
			catch
			{
			}
		}

		private void OnRemoveAllFeatureServices_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
				if (docDataset.FeatureServices.Count == 0)
				{
					AfaDocData.ActiveDocData.Document.Editor.WriteMessage(AfaStrings.NoFeatureServicesFound);
				}
				else
				{
					List<string> list = new List<string>();
					foreach (MSCFeatureService current in docDataset.FeatureServices.Values)
					{
						string name = current.Name;
						if (current.DeleteService())
						{
							list.Add(name);
						}
					}
					foreach (string current2 in list)
					{
						docDataset.FeatureServices.Remove(current2);
					}
					MSCDataset.SetDefaultActiveFeatureClass();
					ArcGISRibbon.EnableFeatureServiceButtons(false);
				}
			}
			catch
			{
			}
		}

		private void OnRefreshMapService(object sender, RoutedEventArgs e)
		{
			MSCRasterService activeRasterService = ArcGISRibbon.GetActiveRasterService();
			if (activeRasterService == null)
			{
				return;
			}
			activeRasterService.RefreshService();
			activeRasterService.CheckForUpdates();
		}

		private void OnZoomMapService(object sender, RoutedEventArgs e)
		{
			MSCRasterService activeRasterService = ArcGISRibbon.GetActiveRasterService();
			if (activeRasterService == null)
			{
				return;
			}
			activeRasterService.ZoomExtents();
		}

		private void OnDeleteMapService(object sender, RoutedEventArgs e)
		{
			try
			{
				MSCRasterService activeRasterService = ArcGISRibbon.GetActiveRasterService();
				if (activeRasterService != null)
				{
					if (activeRasterService.GetType() == typeof(MSCMapService))
					{
						MSCMapService mSCMapService = (MSCMapService)activeRasterService;
						mSCMapService.DeleteService();
					}
					else if (activeRasterService.GetType() == typeof(MSCImageService))
					{
						MSCImageService mSCImageService = (MSCImageService)activeRasterService;
						mSCImageService.DeleteService();
					}
					MSCDataset.SetDefaultActiveRasterServices();
				}
			}
			catch
			{
			}
		}

		private void OnToggleDynamicMap(object sender, RoutedEventArgs e)
		{
			MSCRasterService activeRasterService = ArcGISRibbon.GetActiveRasterService();
			if (activeRasterService == null)
			{
				return;
			}
			if (activeRasterService.ParentService == null)
			{
				return;
			}
			if (activeRasterService != null)
			{
				activeRasterService.Dynamic = !activeRasterService.Dynamic;
				activeRasterService.ExportOptions.Dynamic = activeRasterService.Dynamic;
				activeRasterService.Write();
				if (activeRasterService.Dynamic)
				{
					activeRasterService.RefreshConnectedService();
				}
			}
		}

		private void OnSendBehind(object sender, RoutedEventArgs e)
		{
			MSCRasterService activeRasterService = ArcGISRibbon.GetActiveRasterService();
			if (activeRasterService == null)
			{
				return;
			}
			if (activeRasterService != null)
			{
				activeRasterService.SendBehind();
			}
		}

		private void OnOpenMapServiceProperties(object sender, RoutedEventArgs e)
		{
			CmdLine.CancelActiveCommand();
			string cmdString = ".esri_MapServiceProperties ";
			CmdLine.ExecuteQuietCommand(cmdString);
		}

		private void OnClickMapExport(object sender, RoutedEventArgs e)
		{
			CmdLine.CancelActiveCommand();
			string cmdString = ".ESRI_ExportMap ";
			CmdLine.ExecuteQuietCommand(cmdString);
		}

		private void OnToggleVisibility(object sender, RoutedEventArgs e)
		{
			MSCRasterService activeRasterService = ArcGISRibbon.GetActiveRasterService();
			if (activeRasterService == null)
			{
				return;
			}
			activeRasterService.ToggleVisiblity();
		}

		private void OnUpdateAllMapServices(object sender, RoutedEventArgs e)
		{
			try
			{
				MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
				foreach (MSCMapService current in docDataset.MapServices.Values)
				{
					current.RefreshService();
				}
			}
			catch
			{
			}
		}

		private void OnRemoveAllMaps_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
				foreach (MSCMapService current in docDataset.MapServices.Values)
				{
					current.DeleteService();
				}
			}
			catch
			{
			}
		}

		private void OnUpdateAllImageServices(object sender, RoutedEventArgs e)
		{
			try
			{
				MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
				foreach (MSCImageService current in docDataset.ImageServices.Values)
				{
					current.RefreshService();
				}
			}
			catch
			{
			}
		}

		private void OnRemoveAllImages_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
				foreach (MSCImageService current in docDataset.ImageServices.Values)
				{
					current.DeleteService();
				}
			}
			catch
			{
			}
		}

		private void OnCheckMapServiceLayer(object sender, RoutedEventArgs e)
		{
			try
			{
				CheckBox checkBox = sender as CheckBox;
				MSCMapLayer mSCMapLayer = (MSCMapLayer)checkBox.Tag;
				MSCMapService parentMap = mSCMapLayer.ParentMap;
				if (parentMap != null)
				{
					this.SetActiveRaster(parentMap);
					parentMap.Write();
					parentMap.RefreshConnectedService();
					parentMap.CheckForUpdates();
				}
			}
			catch
			{
			}
		}

		private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			try
			{
				CheckBox checkBox = sender as CheckBox;
				MSCMapLayer mSCMapLayer = (MSCMapLayer)checkBox.Tag;
				MSCMapService parentMap = mSCMapLayer.ParentMap;
				if (parentMap != null)
				{
					this.SetActiveRaster(parentMap);
					parentMap.Write();
					parentMap.RefreshConnectedService();
					parentMap.CheckForUpdates();
				}
			}
			catch
			{
			}
		}

		private void OnOpenMapServiceLayerProperties(object sender, RoutedEventArgs e)
		{
			CmdLine.CancelActiveCommand();
			string cmdString = ".esri_MapServiceProperties ";
			CmdLine.ExecuteQuietCommand(cmdString);
		}

		private void SetActiveFeatureClass(object obj)
		{
			if (obj.GetType() == typeof(FCTag))
			{
				FCTag fCTag = (FCTag)obj;
				MSCFeatureClass featureClass = fCTag.GetFeatureClass(AfaDocData.ActiveDocData.DocDataset);
				AfaDocData.ActiveDocData.SetActiveFeatureClass(featureClass);
				ArcGISRibbon.SetActiveFeatureClass(AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype());
			}
			if (obj.GetType() == typeof(FCView))
			{
				FCView fCView = (FCView)obj;
				MSCFeatureClass featureClass2 = fCView.FC.GetFeatureClass(AfaDocData.ActiveDocData.DocDataset);
				AfaDocData.ActiveDocData.SetActiveFeatureClass(featureClass2);
				ArcGISRibbon.SetActiveFeatureClass(AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype());
			}
			if (obj.GetType() == typeof(KeyValuePair<string, MSCFeatureClass>))
			{
				KeyValuePair<string, MSCFeatureClass> keyValuePair = (KeyValuePair<string, MSCFeatureClass>)obj;
				AfaDocData.ActiveDocData.SetActiveFeatureClass(keyValuePair.Value);
				ArcGISRibbon.SetActiveFeatureClass(AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype());
				return;
			}
			if (obj.GetType() == typeof(KeyValuePair<string, MSCFeatureService>))
			{
				KeyValuePair<string, MSCFeatureService> keyValuePair2 = (KeyValuePair<string, MSCFeatureService>)obj;
				AfaDocData.ActiveDocData.SetActiveFeatureClass(keyValuePair2.Value);
				ArcGISRibbon.SetActiveFeatureClass(AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype());
				return;
			}
			if (obj is MSCFeatureClass || obj is MSCFeatureService || obj is MSCFeatureClassSubType)
			{
				AfaDocData.ActiveDocData.SetActiveFeatureClass((MSCFeatureClass)obj);
				ArcGISRibbon.SetActiveFeatureClass(AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype());
			}
		}

		private void SetActiveRaster(object obj)
		{
			if (obj == null)
			{
				return;
			}
			if (obj.GetType() == typeof(KeyValuePair<string, MSCMapService>))
			{
				KeyValuePair<string, MSCMapService> keyValuePair = (KeyValuePair<string, MSCMapService>)obj;
				AfaDocData.ActiveDocData.CurrentMapService = keyValuePair.Value;
				AfaDocData.ActiveDocData.CurrentMapService.IsVisible();
				ArcGISRibbon.SetActiveRasterService(keyValuePair.Value);
				keyValuePair.Value.CheckForUpdates();
				return;
			}
			if (obj.GetType() == typeof(MSCMapLayer))
			{
				MSCMapLayer mSCMapLayer = (MSCMapLayer)obj;
				AfaDocData.ActiveDocData.CurrentMapService = mSCMapLayer.ParentMap;
				AfaDocData.ActiveDocData.CurrentMapService.IsVisible();
				ArcGISRibbon.SetActiveRasterService(mSCMapLayer.ParentMap);
				mSCMapLayer.ParentMap.CheckForUpdates();
				return;
			}
			if (obj.GetType() == typeof(KeyValuePair<string, MSCImageService>))
			{
				KeyValuePair<string, MSCImageService> keyValuePair2 = (KeyValuePair<string, MSCImageService>)obj;
				AfaDocData.ActiveDocData.CurrentImageService = keyValuePair2.Value;
				AfaDocData.ActiveDocData.CurrentImageService.IsVisible();
				ArcGISRibbon.SetActiveRasterService(keyValuePair2.Value);
				keyValuePair2.Value.CheckForUpdates();
				return;
			}
			if (obj.GetType() == typeof(MSCMapService))
			{
				MSCMapService mSCMapService = (MSCMapService)obj;
				AfaDocData.ActiveDocData.CurrentMapService = mSCMapService;
				AfaDocData.ActiveDocData.CurrentMapService.IsVisible();
				ArcGISRibbon.SetActiveRasterService(mSCMapService);
				mSCMapService.CheckForUpdates();
				return;
			}
			if (obj.GetType() == typeof(MSCImageService))
			{
				MSCImageService mSCImageService = (MSCImageService)obj;
				AfaDocData.ActiveDocData.CurrentImageService = mSCImageService;
				AfaDocData.ActiveDocData.CurrentImageService.IsVisible();
				ArcGISRibbon.SetActiveRasterService(mSCImageService);
				mSCImageService.CheckForUpdates();
			}
		}

		private void EnableSetCurrentDrawingLayer(bool isEnabled, ContextMenu cm)
		{
			try
			{
				foreach (object current in ((IEnumerable)cm.Items))
				{
					if (current is MenuItem)
					{
						MenuItem menuItem = (MenuItem)current;
						if (menuItem.HasHeader && (string)menuItem.Header == AfaStrings.lblSetCurrentDwgLayer)
						{
							menuItem.IsEnabled = isEnabled;
							break;
						}
					}
				}
			}
			catch
			{
			}
		}

		private void OnContextMenuOpened_Img(object sender, ContextMenuEventArgs e)
		{
			try
			{
				TextBlock textBlock = (TextBlock)sender;
				this.SetActiveRaster(textBlock.DataContext);
			}
			catch
			{
			}
		}

		private void OnContextMenuOpened_Map(object sender, RoutedEventArgs e)
		{
			try
			{
				TextBlock textBlock = (TextBlock)sender;
				this.SetActiveRaster(textBlock.DataContext);
			}
			catch
			{
			}
		}

		private void OnContextMenuOpened_MapLayer(object sender, RoutedEventArgs e)
		{
			try
			{
				TextBlock textBlock = (TextBlock)sender;
				this.SetActiveRaster(textBlock.DataContext);
			}
			catch
			{
			}
		}

		private void OnContextMenuOpened_FC(object sender, RoutedEventArgs e)
		{
			try
			{
				TextBlock textBlock = (TextBlock)sender;
				this.SetActiveFeatureClass(textBlock.DataContext);
				MSCFeatureClass activeFeatureClassOrSubtype = AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype();
				if (activeFeatureClassOrSubtype != null)
				{
					if (!activeFeatureClassOrSubtype.IsSingleLayerQuery())
					{
						this.EnableSetCurrentDrawingLayer(false, textBlock.ContextMenu);
					}
					else
					{
						this.EnableSetCurrentDrawingLayer(true, textBlock.ContextMenu);
					}
				}
			}
			catch
			{
			}
		}

		private void DynamicMenuItem_Loaded(object sender, RoutedEventArgs e)
		{
			MenuItem menuItem = (MenuItem)sender;
			try
			{
				MSCRasterService mSCRasterService = null;
				try
				{
					mSCRasterService = ((KeyValuePair<string, MSCMapService>)menuItem.DataContext).Value;
				}
				catch
				{
					mSCRasterService = ((KeyValuePair<string, MSCImageService>)menuItem.DataContext).Value;
				}
				if (mSCRasterService != null)
				{
					menuItem.IsChecked = mSCRasterService.Dynamic;
				}
			}
			catch
			{
				menuItem.IsEnabled = false;
			}
		}

		private void VisibleMenuItem_Loaded(object sender, RoutedEventArgs e)
		{
			MenuItem menuItem = (MenuItem)sender;
			try
			{
				MSCRasterService mSCRasterService = null;
				try
				{
					mSCRasterService = ((KeyValuePair<string, MSCMapService>)menuItem.DataContext).Value;
				}
				catch
				{
					mSCRasterService = ((KeyValuePair<string, MSCImageService>)menuItem.DataContext).Value;
				}
				if (mSCRasterService != null)
				{
					menuItem.IsChecked = mSCRasterService.Visible;
				}
			}
			catch
			{
				menuItem.IsEnabled = false;
			}
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/ArcGISForAutoCAD;component/ui/toccontrol.xaml", UriKind.Relative);
            System.Windows.Application.LoadComponent(this, resourceLocator);
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnAddNewFeatureClass);
				return;
			case 2:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnRemoveAllFeatureClasses_Click);
				return;
			case 3:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnAddNewService);
				return;
			case 4:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnUpdateAll);
				return;
			case 5:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnDiscardAllEdits);
				return;
			case 6:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnExtractAllFeatureServices_Click);
				return;
			case 7:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnRemoveAllFeatureServices_Click);
				return;
			case 8:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnOpenAttributeTable);
				return;
			case 9:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnZoomToExtents);
				return;
			case 10:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnSelectByAttributes);
				return;
			case 11:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnClearSelection);
				return;
			case 12:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnSelectAllClicked);
				return;
			case 13:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnOpenTableSelected);
				return;
			case 14:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnDeleteFeatureClass);
				return;
			case 15:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnOpenFeatureClassProperties);
				return;
			case 16:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnOpenAttributeTable);
				return;
			case 17:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnZoomToExtents);
				return;
			case 18:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnSelectByAttributes);
				return;
			case 19:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnClearSelection);
				return;
			case 20:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnSelectAllClicked);
				return;
			case 21:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnOpenTableSelected);
				return;
			case 22:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnSetCurrentDrawingLayer);
				return;
			case 23:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnRefreshFeatureService);
				return;
			case 24:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnDiscardEdits);
				return;
			case 25:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnExtractFeatureService);
				return;
			case 26:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnDeleteFeatureService);
				return;
			case 27:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnOpenFeatureServiceProperties);
				return;
			case 28:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnOpenAttributeTable);
				return;
			case 29:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnZoomToExtents);
				return;
			case 30:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnSelectByAttributes);
				return;
			case 31:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnClearSelection);
				return;
			case 32:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnSelectAllClicked);
				return;
			case 33:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnOpenTableSelected);
				return;
			case 34:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnSetCurrentDrawingLayer);
				return;
			case 35:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnOpenFeatureClassProperties);
				return;
			case 36:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnAddNewService);
				return;
			case 37:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnUpdateAllMapServices);
				return;
			case 38:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnRemoveAllMaps_Click);
				return;
			case 39:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnZoomMapService);
				return;
			case 40:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnSendBehind);
				return;
			case 41:
				((MenuItem)target).Loaded += new RoutedEventHandler(this.VisibleMenuItem_Loaded);
				((MenuItem)target).Click += new RoutedEventHandler(this.OnToggleVisibility);
				return;
			case 42:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnRefreshMapService);
				return;
			case 43:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnToggleDynamicMap);
				((MenuItem)target).Loaded += new RoutedEventHandler(this.DynamicMenuItem_Loaded);
				return;
			case 44:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnDeleteMapService);
				return;
			case 45:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnClickMapExport);
				return;
			case 46:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnOpenMapServiceProperties);
				return;
			case 47:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnOpenMapServiceLayerProperties);
				return;
			case 48:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnAddNewService);
				return;
			case 49:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnUpdateAllImageServices);
				return;
			case 50:
				((MenuItem)target).Click += new RoutedEventHandler(this.OnRemoveAllImages_Click);
				return;
			case 54:
				this.exFeatureClasses = (Expander)target;
				return;
			case 55:
				this.tvFeatureClasses = (TreeView)target;
				this.tvFeatureClasses.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(this.OnSelectedItemChanged);
				return;
			case 58:
				this.exFeatureServices = (Expander)target;
				return;
			case 59:
				this.tvFeatureServices = (TreeView)target;
				this.tvFeatureServices.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(this.OnSelectedItemChanged);
				return;
			case 62:
				this.exMapServices = (Expander)target;
				return;
			case 63:
				this.tvMapServices = (TreeView)target;
				this.tvMapServices.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(this.OnSelectedMapChanged);
				this.tvMapServices.MouseUp += new MouseButtonEventHandler(this.tvMapServices_MouseUp);
				return;
			case 64:
				this.exImageServices = (Expander)target;
				return;
			case 65:
				this.tvImageServices = (TreeView)target;
				this.tvImageServices.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(this.OnSelectedMapChanged);
				this.tvImageServices.MouseUp += new MouseButtonEventHandler(this.tvImageServices_MouseUp);
				return;
			}
			this._contentLoaded = true;
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 51:
				((CheckBox)target).Checked += new RoutedEventHandler(this.OnCheckMapServiceLayer);
				((CheckBox)target).Unchecked += new RoutedEventHandler(this.CheckBox_Unchecked);
				((CheckBox)target).ContextMenuOpening += new ContextMenuEventHandler(this.OnContextMenuOpened_Map);
				return;
			case 52:
				((TextBlock)target).ContextMenuOpening += new ContextMenuEventHandler(this.OnContextMenuOpened_MapLayer);
				return;
			case 53:
				((TextBlock)target).ContextMenuOpening += new ContextMenuEventHandler(this.OnContextMenuOpened_Img);
				return;
			case 54:
			case 55:
			case 58:
			case 59:
				break;
			case 56:
				((TextBlock)target).ContextMenuOpening += new ContextMenuEventHandler(this.OnContextMenuOpened_FC);
				return;
			case 57:
				((TextBlock)target).ContextMenuOpening += new ContextMenuEventHandler(this.OnContextMenuOpened_FC);
				return;
			case 60:
				((TextBlock)target).ContextMenuOpening += new ContextMenuEventHandler(this.OnContextMenuOpened_FC);
				return;
			case 61:
				((TextBlock)target).ContextMenuOpening += new ContextMenuEventHandler(this.OnContextMenuOpened_FC);
				break;
			default:
				return;
			}
		}
	}
}
