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
	public class MapTableViewer : Window, IComponentConnector
	{
		internal DataGrid dg;

		internal ComboBox cbMapLayers;

		internal Button btnClose;

		private bool _contentLoaded;

		public MapTableViewer(MSCMapService ms, Extent ext)
		{
			this.InitializeComponent();
			Mouse.OverrideCursor = Cursors.Wait;
			if (ms.InitializeIdentify(ext))
			{
				this.PopulateMapLayerList(ms);
				this.cbMapLayers.SelectedIndex = 0;
			}
			Mouse.OverrideCursor = null;
		}

		public MapTableViewer(MSCMapService ms, Extent ext, MSCMapLayer startingLayer)
		{
			this.InitializeComponent();
			Mouse.OverrideCursor = Cursors.Wait;
			if (ms.InitializeIdentify(ext))
			{
				this.PopulateMapLayerList(ms);
				foreach (KeyValuePair<string, MSCMapLayer> keyValuePair in ((IEnumerable)this.cbMapLayers.Items))
				{
					if (startingLayer == keyValuePair.Value)
					{
						this.cbMapLayers.SelectedItem = keyValuePair;
						break;
					}
				}
			}
			Mouse.OverrideCursor = null;
		}

		public int LayerCount()
		{
			int result;
			try
			{
				result = this.cbMapLayers.Items.Count;
			}
			catch
			{
				result = 0;
			}
			return result;
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			this.HideMinimizeAndMaximizeButtons();
		}

		private void PopulateMapLayerChildList(MSCMapLayer parentLyr, string displayName, ref Dictionary<string, MSCMapLayer> items)
		{
			foreach (MSCMapLayer current in parentLyr.Layers)
			{
				if (current.Visible && current.IdentifyResults != null)
				{
					if (current.Layers.Count == 0)
					{
						if (current.IdentifyResults.Rows.Count != 0)
						{
							items.Add(displayName + "-" + current.Name, current);
						}
					}
					else
					{
						this.PopulateMapLayerChildList(current, displayName + "-" + current.Name, ref items);
					}
				}
			}
		}

		private void PopulateMapLayerList(MSCMapService ms)
		{
			Dictionary<string, MSCMapLayer> dictionary = new Dictionary<string, MSCMapLayer>();
			foreach (MSCMapLayer current in ms.Layers)
			{
				if (current.Visible && current.IdentifyResults != null)
				{
					if (current.Layers.Count == 0)
					{
						if (current.IdentifyResults.Rows.Count != 0)
						{
							dictionary.Add(current.Name, current);
						}
					}
					else
					{
						this.PopulateMapLayerChildList(current, current.Name, ref dictionary);
					}
				}
			}
			this.cbMapLayers.ItemsSource = dictionary;
		}

		private void cbMapLayers_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			MSCMapLayer value = ((KeyValuePair<string, MSCMapLayer>)this.cbMapLayers.SelectedItem).Value;
			this.dg.ItemsSource = value.IdentifyResults.DefaultView;
		}

		private void btnClose_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/ArcGISForAutoCAD;component/ui/maptableviewer.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.dg = (DataGrid)target;
				return;
			case 2:
				this.cbMapLayers = (ComboBox)target;
				this.cbMapLayers.SelectionChanged += new SelectionChangedEventHandler(this.cbMapLayers_SelectionChanged);
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
	}
}
