using AFA.Resources;
using Autodesk.AutoCAD.ApplicationServices;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace AFA
{
	public class ServiceProperties : UserControl, IComponentConnector, IStyleConnector
	{
		private List<string> SpRefOptions = new List<string>
		{
			AfaStrings.ServiceSpatialReference,
			AfaStrings.DrawingSpatialReference
		};

		private List<string> SpRefPickerOptions = new List<string>
		{
			AfaStrings.ServiceSpatialReference,
			AfaStrings.SelectASpatialReference
		};

		private List<string> OrderFieldChoices = new List<string>
		{
			"<None>"
		};

		private bool LimitedImageSupport;

		private object ThisService;

		private string[] SupportedMosaicMethods = new string[]
		{
			"None",
			"Closest to Center",
			"Closest to Nadir",
			"By Attribute",
			"Lock Raster",
			"North-West",
			"Seamline"
		};

		private string[] SupportedMosaicOperators = new string[]
		{
			"<None>",
			"First",
			"Last",
			"Min",
			"Max",
			"Mean",
			"Blend"
		};

		internal Expander exServiceInfo;

		internal ListBox lbServiceInfo;

		internal Expander exExportProps;

		internal ComboBox cbBoundingBoxTypes;

		internal ContentControl ctrlBoundingBoxExtent;

		internal ContentControl ctrlExportProps;

		private bool _contentLoaded;

		public Extent DefaultExtent
		{
			get;
			set;
		}

		private Extent CustomExtent
		{
			get;
			set;
		}

		public bool IgnoreTextBoxChange
		{
			get;
			set;
		}

		public Dictionary<string, Extent> DocExtentOptions
		{
			get;
			set;
		}

		public Dictionary<string, Extent> ExtentOptions
		{
			get;
			set;
		}

		public string WhereClause
		{
			get;
			set;
		}

		public AGSExportOptions ExportOptions
		{
			get;
			set;
		}

		public ServiceProperties()
		{
			this.InitializeComponent();
			this.InitExtentOptions();
		}

		private void BuildDocExtentOptions()
		{
			Document document = AfaDocData.ActiveDocData.Document;
			this.DocExtentOptions = new Dictionary<string, Extent>();
			this.DefaultExtent = new Extent(0.0, 0.0, 100.0, 100.0);
			this.DefaultExtent.SetWKTFrom(AfaDocData.ActiveDocData.DocPRJ.WKT);
			if (DocUtil.HasLimits(AfaDocData.ActiveDocData.Document))
			{
				this.DocExtentOptions.Add(AfaStrings.DrawingLimits, DocUtil.GetLimitExtent(document));
			}
			Extent dwgExtent = DocUtil.GetDwgExtent(document);
			if (dwgExtent != null)
			{
				this.DocExtentOptions.Add(AfaStrings.DrawingExtents, DocUtil.GetDwgExtent(document));
			}
			Extent viewExtent = DocUtil.GetViewExtent(document);
			this.DocExtentOptions.Add(AfaStrings.CurrentView, viewExtent);
			this.DocExtentOptions.Add(AfaStrings.SelectCorners, this.DefaultExtent);
		}

		private void InitExtentOptions()
		{
		}

		public void SetService(object o, AGSExportOptions localExportOptions)
		{
			try
			{
				if (localExportOptions != null)
				{
					this.ExportOptions = localExportOptions;
				}
				this.SetService(o);
			}
			catch
			{
			}
		}

		public void SetService(object o)
		{
			try
			{
				if (!object.Equals(o, this.ThisService))
				{
					this.ThisService = o;
					this.exServiceInfo.Visibility = Visibility.Collapsed;
					this.exExportProps.Visibility = Visibility.Collapsed;
					bool dynamic = true;
					if (this.ExportOptions != null)
					{
						dynamic = this.ExportOptions.Dynamic;
					}
					if (o != this.lbServiceInfo.ItemsSource)
					{
						AGSObject aGSObject = o as AGSObject;
						if (aGSObject != null)
						{
							this.IgnoreTextBoxChange = true;
							this.lbServiceInfo.ItemsSource = aGSObject.Properties;
							string text = "";
							if (this.cbBoundingBoxTypes.SelectedItem != null)
							{
								text = this.cbBoundingBoxTypes.SelectedItem.ToString();
							}
							this.ExtentOptions = this.BuildExtentOptions(aGSObject);
							this.cbBoundingBoxTypes.ItemsSource = this.ExtentOptions.Keys;
							Extent obj;
							if (this.ExtentOptions.TryGetValue(text, out obj))
							{
								this.cbBoundingBoxTypes.SelectedValue = text;
								this.ctrlBoundingBoxExtent.Content = Utility.CloneObject(obj);
							}
							else
							{
								this.cbBoundingBoxTypes.SelectedIndex = 0;
								this.ctrlBoundingBoxExtent.Content = Utility.CloneObject(this.ExtentOptions.Values.ElementAt(0));
							}
							this.IgnoreTextBoxChange = false;
						}
						AGSLayer aGSLayer = o as AGSLayer;
						if (aGSLayer != null)
						{
							this.IgnoreTextBoxChange = true;
							this.lbServiceInfo.ItemsSource = aGSLayer.Properties;
							this.exServiceInfo.Visibility = Visibility.Visible;
							string text2 = "";
							if (this.cbBoundingBoxTypes.SelectedItem != null)
							{
								text2 = this.cbBoundingBoxTypes.SelectedItem.ToString();
							}
							this.ExtentOptions = this.BuildExtentOptions(aGSLayer.Service);
							this.cbBoundingBoxTypes.ItemsSource = this.ExtentOptions.Keys;
							Extent obj2;
							if (this.ExtentOptions.TryGetValue(text2, out obj2))
							{
								this.cbBoundingBoxTypes.SelectedValue = text2;
								this.ctrlBoundingBoxExtent.Content = Utility.CloneObject(obj2);
							}
							else
							{
								this.cbBoundingBoxTypes.SelectedIndex = 0;
								this.ctrlBoundingBoxExtent.Content = Utility.CloneObject(this.ExtentOptions.Values.ElementAt(0));
							}
							this.IgnoreTextBoxChange = false;
							this.exExportProps.Visibility = Visibility.Visible;
							this.ctrlExportProps.Content = aGSLayer;
							this.ExportOptions = aGSLayer.Service.ExportOptions;
							this.ExportOptions.Dynamic = dynamic;
						}
						else
						{
							AGSFeatureServiceLayer aGSFeatureServiceLayer = o as AGSFeatureServiceLayer;
							if (aGSFeatureServiceLayer == null)
							{
								AGSFeatureService aGSFeatureService = o as AGSFeatureService;
								if (aGSFeatureService != null)
								{
									this.lbServiceInfo.ItemsSource = aGSFeatureService.Properties;
									this.exServiceInfo.Visibility = Visibility.Visible;
									this.exExportProps.Visibility = Visibility.Visible;
									this.ctrlExportProps.Content = aGSFeatureService;
									this.ExportOptions = aGSFeatureService.ExportOptions;
									this.ExportOptions.Dynamic = dynamic;
								}
								AGSService aGSService = o as AGSService;
								if (aGSService != null)
								{
									this.lbServiceInfo.ItemsSource = aGSService.Properties;
									this.exServiceInfo.Visibility = Visibility.Visible;
									this.exExportProps.Visibility = Visibility.Visible;
									this.ctrlExportProps.Content = aGSService;
									this.ExportOptions = aGSService.ExportOptions;
									this.ExportOptions.Dynamic = dynamic;
								}
								AGSImageService aGSImageService = o as AGSImageService;
								if (aGSImageService != null)
								{
									foreach (AGSField current in aGSImageService.Fields.Values)
									{
										if (!(current.Type == "esriFieldTypeOID") && !(current.Type == "esriFieldTypeGeometry"))
										{
											this.OrderFieldChoices.Add(current.Name);
										}
									}
									string version = aGSImageService.Version;
									if (version.StartsWith("9"))
									{
										this.LimitedImageSupport = true;
									}
								}
							}
						}
					}
				}
			}
			catch
			{
				MessageBox.Show("In Catch of SetService");
			}
		}

		private Dictionary<string, Extent> BuildExtentOptions(IAGSObject svc)
		{
			Dictionary<string, Extent> result;
			try
			{
				this.BuildDocExtentOptions();
				Dictionary<string, Extent> dictionary = new Dictionary<string, Extent>();
				if (svc.Properties.ContainsKey("Full Extent") && this.ValidateExtent(svc.Properties["Full Extent"] as Extent))
				{
					dictionary.Add(AfaStrings.ServiceFullExtent, svc.Properties["Full Extent"] as Extent);
				}
				if (svc.Properties.ContainsKey("Initial Extent") && this.ValidateExtent(svc.Properties["Initial Extent"] as Extent))
				{
					dictionary.Add(AfaStrings.ServiceInitialExtent, svc.Properties["Initial Extent"] as Extent);
				}
				if (svc.Properties.ContainsKey("Extent") && this.ValidateExtent(svc.Properties["Extent"] as Extent))
				{
					dictionary.Add(AfaStrings.ServiceExtent, svc.Properties["Extent"] as Extent);
				}
				foreach (KeyValuePair<string, Extent> current in this.DocExtentOptions)
				{
					dictionary.Add(current.Key, current.Value);
				}
				if (this.CustomExtent == null)
				{
					dictionary.Add(AfaStrings.Custom, this.DefaultExtent);
				}
				else
				{
					dictionary.Add(AfaStrings.Custom, this.CustomExtent);
				}
				result = dictionary;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private bool ValidateExtent(Extent ext)
		{
			bool result;
			try
			{
				result = ext.IsValid();
			}
			catch
			{
				result = false;
			}
			return result;
		}

		private void cbBoundingBoxTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				this.IgnoreTextBoxChange = true;
				if (e.AddedItems.Count > 0)
				{
					string arg_23_0 = this.cbBoundingBoxTypes.Text;
					string text = e.AddedItems[0].ToString();
					if (text == AfaStrings.DrawingExtents)
					{
						Extent dwgExtent = DocUtil.GetDwgExtent(AfaDocData.ActiveDocData.Document);
						this.ExtentOptions[text] = dwgExtent;
					}
					if (text == AfaStrings.CurrentView)
					{
						Extent viewExtent = DocUtil.GetViewExtent(AfaDocData.ActiveDocData.Document);
						this.ExtentOptions[text] = viewExtent;
					}
					if (text == AfaStrings.SelectCorners)
					{
						this.cbBoundingBoxTypes.Refresh();
						Window window = Window.GetWindow(this);
						window.Visibility = Visibility.Collapsed;
						Extent extentsFromCorners = DocUtil.GetExtentsFromCorners(AfaDocData.ActiveDocData.Document);
						window.Visibility = Visibility.Visible;
						if (!DocUtil.IsValidExtent(extentsFromCorners))
						{
							ComboBox comboBox = (ComboBox)sender;
							comboBox.SelectedItem = e.RemovedItems[0];
							this.IgnoreTextBoxChange = false;
							return;
						}
						this.ctrlBoundingBoxExtent.Content = extentsFromCorners;
						this.ExtentOptions[AfaStrings.Custom] = (this.CustomExtent = extentsFromCorners);
						text = (this.cbBoundingBoxTypes.Text = AfaStrings.Custom);
					}
					if (text != AfaStrings.Custom)
					{
						this.ctrlBoundingBoxExtent.Content = Utility.CloneObject(this.ExtentOptions[text]);
					}
				}
				this.IgnoreTextBoxChange = false;
			}
			catch
			{
			}
		}

		private void ext_TextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				if (!this.IgnoreTextBoxChange)
				{
					this.cbBoundingBoxTypes.Text = AfaStrings.Custom;
					this.cbBoundingBoxTypes.Refresh();
				}
			}
			catch
			{
			}
		}

		private void cbImageFormat_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.ExportOptions.SupportedFormats == null)
				{
					MSCRasterService mSCRasterService = this.ThisService as MSCRasterService;
					if (mSCRasterService != null)
					{
						try
						{
							this.ExportOptions.SupportedFormats = mSCRasterService.ParentService.SupportedImageTypes.ToArray();
						}
						catch
						{
						}
					}
					if (this.ExportOptions.SupportedFormats == null)
					{
						this.ExportOptions.SupportedFormats = DocUtil.CADSupportedImageFormats;
					}
				}
				if (string.IsNullOrEmpty(this.ExportOptions.Format))
				{
					this.ExportOptions.Format = "PNG24";
				}
				ComboBox comboBox = (ComboBox)sender;
				comboBox.ItemsSource = this.ExportOptions.SupportedFormats;
				comboBox.SelectedValue = this.ExportOptions.Format;
			}
			catch
			{
			}
		}

		private void cbRefresh_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				ComboBox comboBox = (ComboBox)sender;
				if (this.ExportOptions.Dynamic)
				{
					comboBox.SelectedIndex = 0;
				}
				else
				{
					comboBox.SelectedIndex = 1;
				}
			}
			catch
			{
			}
		}

		private void cbRefresh_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.AddedItems.Count > 0)
			{
				ComboBoxItem comboBoxItem = e.AddedItems[0] as ComboBoxItem;
				if (comboBoxItem.Content.ToString() == AfaStrings.Dynamically)
				{
					this.ExportOptions.Dynamic = true;
					return;
				}
				if (comboBoxItem.Content.ToString() == AfaStrings.Editing)
				{
					this.ExportOptions.Dynamic = true;
					return;
				}
				this.ExportOptions.Dynamic = false;
			}
		}

		private void cbCompression_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				ComboBox comboBox = (ComboBox)sender;
				if (string.IsNullOrEmpty(this.ExportOptions.TransCompression))
				{
					this.ExportOptions.TransCompression = "None";
				}
				comboBox.SelectedItem = this.ExportOptions.TransCompression;
			}
			catch
			{
			}
		}

		private void cbInterpolation_Loaded(object sender, RoutedEventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			if (string.IsNullOrEmpty(this.ExportOptions.Interpolation))
			{
				this.ExportOptions.Interpolation = "Default";
			}
			comboBox.SelectedItem = this.ExportOptions.Interpolation;
		}

		private void cbMosaicMethod_Loaded(object sender, RoutedEventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			comboBox.ItemsSource = this.SupportedMosaicMethods;
			if (string.IsNullOrEmpty(this.ExportOptions.MosaicMethod))
			{
				this.ExportOptions.MosaicMethod = "esriMosaicNone";
			}
			if (this.LimitedImageSupport)
			{
				this.ExportOptions.MosaicMethod = "esriMosaicNone";
				comboBox.IsEnabled = false;
			}
		}

		private void cbMosaicMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ComboBox arg_06_0 = (ComboBox)sender;
		}

		private void cbMosaicOperator_Loaded(object sender, RoutedEventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			comboBox.ItemsSource = this.SupportedMosaicOperators;
			if (string.IsNullOrEmpty(this.ExportOptions.MosaicOperator))
			{
				this.ExportOptions.MosaicOperator = "<None>";
			}
			if (this.LimitedImageSupport)
			{
				this.ExportOptions.MosaicOperator = "<None>";
				comboBox.IsEnabled = false;
			}
		}

		private void cbOrderField_Loaded(object sender, RoutedEventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			comboBox.ItemsSource = this.OrderFieldChoices;
			if (string.IsNullOrEmpty(this.ExportOptions.OrderField))
			{
				this.ExportOptions.OrderField = "<None>";
			}
			comboBox.SelectedItem = this.ExportOptions.OrderField;
			if (this.LimitedImageSupport)
			{
				comboBox.IsEnabled = false;
			}
		}

		private void tbQuality_TextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				if (e.Changes.Count > 0)
				{
					TextBox textBox = e.Source as TextBox;
					if (!string.IsNullOrEmpty(textBox.Text))
					{
						this.ExportOptions.Quality = int.Parse(textBox.Text);
					}
				}
			}
			catch
			{
			}
		}

		private void tbWhere_TextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				if (e.Changes.Count > 0)
				{
					TextBox textBox = e.Source as TextBox;
					this.WhereClause = textBox.Text;
				}
			}
			catch
			{
			}
		}

		private void tbTransparency_TextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				if (e.Changes.Count > 0)
				{
					TextBox textBox = e.Source as TextBox;
					this.ExportOptions.Transparency = byte.Parse(textBox.Text);
				}
			}
			catch
			{
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
			Uri resourceLocator = new Uri("/ArcGISForAutoCAD;component/ui/serviceproperties.xaml", UriKind.Relative);
            System.Windows.Application.LoadComponent(this, resourceLocator);
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.exServiceInfo = (Expander)target;
				return;
			case 2:
				this.lbServiceInfo = (ListBox)target;
				return;
			case 3:
				this.exExportProps = (Expander)target;
				return;
			case 8:
				this.cbBoundingBoxTypes = (ComboBox)target;
				this.cbBoundingBoxTypes.SelectionChanged += new SelectionChangedEventHandler(this.cbBoundingBoxTypes_SelectionChanged);
				return;
			case 9:
				this.ctrlBoundingBoxExtent = (ContentControl)target;
				return;
			case 10:
				this.ctrlExportProps = (ContentControl)target;
				return;
			}
			this._contentLoaded = true;
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 4:
				((TextBox)target).TextChanged += new TextChangedEventHandler(this.ext_TextChanged);
				return;
			case 5:
				((TextBox)target).TextChanged += new TextChangedEventHandler(this.ext_TextChanged);
				return;
			case 6:
				((TextBox)target).TextChanged += new TextChangedEventHandler(this.ext_TextChanged);
				return;
			case 7:
				((TextBox)target).TextChanged += new TextChangedEventHandler(this.ext_TextChanged);
				return;
			case 8:
			case 9:
			case 10:
				break;
			case 11:
				((ComboBox)target).Loaded += new RoutedEventHandler(this.cbRefresh_Loaded);
				((ComboBox)target).SelectionChanged += new SelectionChangedEventHandler(this.cbRefresh_SelectionChanged);
				return;
			case 12:
				((ComboBox)target).Loaded += new RoutedEventHandler(this.cbImageFormat_Loaded);
				return;
			case 13:
				((TextBox)target).TextChanged += new TextChangedEventHandler(this.tbTransparency_TextChanged);
				return;
			case 14:
				((ComboBox)target).Loaded += new RoutedEventHandler(this.cbRefresh_Loaded);
				((ComboBox)target).SelectionChanged += new SelectionChangedEventHandler(this.cbRefresh_SelectionChanged);
				return;
			case 15:
				((ComboBox)target).Loaded += new RoutedEventHandler(this.cbInterpolation_Loaded);
				return;
			case 16:
				((ComboBox)target).Loaded += new RoutedEventHandler(this.cbCompression_Loaded);
				return;
			case 17:
				((TextBox)target).TextChanged += new TextChangedEventHandler(this.tbQuality_TextChanged);
				return;
			case 18:
				((ComboBox)target).Loaded += new RoutedEventHandler(this.cbImageFormat_Loaded);
				return;
			case 19:
				((ComboBox)target).Loaded += new RoutedEventHandler(this.cbMosaicMethod_Loaded);
				((ComboBox)target).SelectionChanged += new SelectionChangedEventHandler(this.cbMosaicMethod_SelectionChanged);
				return;
			case 20:
				((ComboBox)target).Loaded += new RoutedEventHandler(this.cbOrderField_Loaded);
				return;
			case 21:
				((ComboBox)target).Loaded += new RoutedEventHandler(this.cbMosaicOperator_Loaded);
				return;
			case 22:
				((TextBox)target).TextChanged += new TextChangedEventHandler(this.tbTransparency_TextChanged);
				return;
			case 23:
				((ComboBox)target).Loaded += new RoutedEventHandler(this.cbRefresh_Loaded);
				((ComboBox)target).SelectionChanged += new SelectionChangedEventHandler(this.cbRefresh_SelectionChanged);
				return;
			case 24:
				((ComboBox)target).Loaded += new RoutedEventHandler(this.cbRefresh_Loaded);
				((ComboBox)target).SelectionChanged += new SelectionChangedEventHandler(this.cbRefresh_SelectionChanged);
				break;
			default:
				return;
			}
		}
	}
}
