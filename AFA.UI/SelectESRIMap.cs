using AFA.Resources;
using AGOBasemap;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace AFA.UI
{
	public class SelectESRIMap : Window, IComponentConnector, IStyleConnector
	{
		internal ListBox lbMaps;

		internal Button btnClose;

		private bool _contentLoaded;

		public SelectESRIMap(List<OnlineSearchItem> maps)
		{
			try
			{
				this.InitializeComponent();
				if (maps != null && maps.Count > 0)
				{
					this.lbMaps.ItemsSource = maps;
				}
			}
			catch (SystemException ex)
			{
				ErrorReport.ShowErrorMessage("Error initializing ESRI Map window.  " + ex.Message);
			}
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			this.HideMinimizeAndMaximizeButtons();
		}

		private void btnClose_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		private void MapButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				base.IsEnabled = false;
				base.Hide();
				Mouse.OverrideCursor = Cursors.Wait;
				OnlineSearchItem onlineSearchItem = (OnlineSearchItem)this.lbMaps.SelectedItem;
				string item = onlineSearchItem.Item;
				string uRL = item;
				if (item.ToLower().Contains("/rest/"))
				{
					int startIndex = item.ToLower().IndexOf("/rest/");
					uRL = item.Remove(startIndex, 5);
				}
				AGSMapService aGSMapService = AGSMapService.BuildMapServiceFromURL(onlineSearchItem.Name, uRL);
				string wKT = AfaDocData.ActiveDocData.DocPRJ.WKT;
				if (aGSMapService != null)
				{
					if (string.IsNullOrEmpty(wKT))
					{
						try
						{
							wKT = aGSMapService.GetWKT();
							MSCPrj.AssignWKT(AfaDocData.ActiveDocData.Document, wKT);
						}
						catch
						{
							AfaDocData.ActiveDocData.Document.Editor.WriteMessage(AfaStrings.UnableToAssignCoordinateSystem);
						}
					}
					aGSMapService.ExportOptions.OutputWKT = wKT;
					if (!aGSMapService.AddService())
					{
						string text = AfaStrings.ErrorAddingService;
						if (!string.IsNullOrEmpty(aGSMapService.ErrorMessage))
						{
							text = text + "  " + aGSMapService.ErrorMessage;
						}
						ErrorReport.ShowErrorMessage(text);
					}
				}
				else
				{
					ErrorReport.ShowErrorMessage(AfaStrings.ErrorAddingService);
				}
				Mouse.OverrideCursor = null;
				base.IsEnabled = true;
				base.Close();
			}
			catch
			{
				Mouse.OverrideCursor = null;
				base.IsEnabled = true;
				base.Close();
			}
		}

		private void Button_GotFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				Button button = sender as Button;
				string b = button.Tag as string;
				foreach (OnlineSearchItem onlineSearchItem in ((IEnumerable)this.lbMaps.Items))
				{
					if (onlineSearchItem.Name == b)
					{
						this.lbMaps.SelectedItem = onlineSearchItem;
						break;
					}
				}
			}
			catch (SystemException ex)
			{
				ErrorReport.ShowErrorMessage(ex.Message);
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
			Uri resourceLocator = new Uri("/ArcGISForAutoCAD;component/ui/selectesrimap.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 2:
				this.lbMaps = (ListBox)target;
				return;
			case 3:
				this.btnClose = (Button)target;
				this.btnClose.Click += new RoutedEventHandler(this.btnClose_Click);
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId != 1)
			{
				return;
			}
			((Button)target).Click += new RoutedEventHandler(this.MapButton_Click);
			((Button)target).GotFocus += new RoutedEventHandler(this.Button_GotFocus);
		}
	}
}
