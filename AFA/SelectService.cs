using AFA.Resources;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace AFA
{
    public class SelectService : Window, IComponentConnector
	{
		private bool CurrentlyAdding;

		internal System.Windows.Controls.TreeView tvServices;

		internal ServiceProperties spProps;

		internal System.Windows.Controls.Primitives.StatusBar sbar;

		internal System.Windows.Controls.Button btnAdd;

		internal System.Windows.Controls.Button btnExport;

		internal System.Windows.Controls.Button btnExtract;

		internal System.Windows.Controls.Button btnCancel;

		private bool _contentLoaded;

		public AGSConnection Connection
		{
			get;
			set;
		}

		public bool IsValid
		{
			get;
			set;
		}

		public SelectService(AGSConnection connection)
		{
			this.Connection = connection;
			this.InitializeComponent();
			AfaDocData.ActiveDocData.Document.BeginDocumentClose+=(new DocumentBeginCloseEventHandler(this.Document_BeginDocumentClose));
			this.IsValid = true;
			this.spProps.IgnoreTextBoxChange = true;
			this.btnAdd.Visibility = Visibility.Collapsed;
			this.btnExport.Visibility = Visibility.Collapsed;
			this.btnExtract.Visibility = Visibility.Collapsed;
			if (!this.Connection.LoadConnectionProperties())
			{
				ErrorReport.ShowErrorMessage(this.Connection.ErrorMessage);
				this.IsValid = false;
				return;
			}
			try
			{
				if (!connection.FoldersLoaded)
				{
					Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
					if (!connection.LoadChildren())
					{
						Mouse.OverrideCursor = null;
						ErrorReport.ShowErrorMessage(this.Connection.ErrorMessage);
						this.IsValid = false;
					}
				}
				if (connection.folder.Items.Count == 0)
				{
					ErrorReport.ShowErrorMessage(AfaStrings.NoSupportedServicesFoundOn + connection.Name);
					this.IsValid = false;
				}
				else
				{
					this.tvServices.ItemsSource = connection.folder.Items;
					this.IsValid = true;
					this.spProps.IgnoreTextBoxChange = false;
				}
			}
			catch
			{
				Mouse.OverrideCursor = null;
				this.IsValid = false;
				ErrorReport.ShowErrorMessage(AfaStrings.UnknownError);
			}
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			try
			{
				base.OnSourceInitialized(e);
				SelectConnection.WindowOpen = true;
				this.HideMinimizeAndMaximizeButtons();
			}
			catch
			{
			}
		}

		private void Document_BeginDocumentClose(object sender, DocumentBeginCloseEventArgs e)
		{
			try
			{
				base.Close();
			}
			catch
			{
			}
		}

		private void OnWindowClosing(object sender, CancelEventArgs e)
		{
			try
			{
				AfaDocData.ActiveDocData.Document.BeginDocumentClose-=(new DocumentBeginCloseEventHandler(this.Document_BeginDocumentClose));
				SelectConnection.WindowOpen = false;
			}
			catch
			{
			}
		}

		private AGSExportOptions InitExportOptions(object srcObject)
		{
			AGSExportOptions result;
			try
			{
				AGSExportOptions aGSExportOptions = new AGSExportOptions();
				aGSExportOptions.AcadDocument = AfaDocData.ActiveDocData.Document;
				Extent extent = this.spProps.ctrlBoundingBoxExtent.Content as Extent;
				aGSExportOptions.BoundingBox = extent;
				aGSExportOptions.OutputWKT = AfaDocData.ActiveDocData.DocPRJ.WKT;
				aGSExportOptions.WhereClause = "";
				System.Drawing.Size size = Autodesk.AutoCAD.ApplicationServices.Application.ToSystemDrawingSize(AfaDocData.ActiveDocData.Document.Window.DeviceIndependentSize);
				aGSExportOptions.Width = size.Width;
				aGSExportOptions.Height = size.Height;
				aGSExportOptions.DPI = 96;
				aGSExportOptions.Transparency = this.spProps.ExportOptions.Transparency;
				aGSExportOptions.Format = this.spProps.ExportOptions.Format;
				aGSExportOptions.Dynamic = this.spProps.ExportOptions.Dynamic;
				if (aGSExportOptions.Format == null)
				{
					aGSExportOptions.Format = "PNG24";
				}
				AGSService aGSService = srcObject as AGSService;
				AGSLayer aGSLayer = srcObject as AGSLayer;
				string text = "";
				try
				{
					if (aGSService != null)
					{
						text = aGSService.GetWKT();
					}
					else if (aGSLayer != null)
					{
						text = aGSLayer.GetWKT();
					}
					if (string.IsNullOrEmpty(text))
					{
						text = "PROJCS[\"WGS_1984_Web_Mercator_Auxiliary_Sphere\",GEOGCS[\"GCS_WGS_1984\",DATUM[\"D_WGS_1984\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Mercator_Auxiliary_Sphere\"],PARAMETER[\"False_Easting\",0.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",0.0],PARAMETER[\"Standard_Parallel_1\",0.0],PARAMETER[\"Auxiliary_Sphere_Type\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3857]]";
					}
				}
				catch
				{
					text = "PROJCS[\"WGS_1984_Web_Mercator_Auxiliary_Sphere\",GEOGCS[\"GCS_WGS_1984\",DATUM[\"D_WGS_1984\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Mercator_Auxiliary_Sphere\"],PARAMETER[\"False_Easting\",0.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",0.0],PARAMETER[\"Standard_Parallel_1\",0.0],PARAMETER[\"Auxiliary_Sphere_Type\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3857]]";
				}
				if (string.IsNullOrEmpty(extent.SpatialReference))
				{
					extent.SpatialReference = text;
				}
				aGSExportOptions.OutputWKT = MSCPrj.CurrentWKT(AfaDocData.ActiveDocData.Document, text);
				try
				{
					aGSExportOptions.Interpolation = this.spProps.ExportOptions.Interpolation;
					aGSExportOptions.MosaicMethod = this.spProps.ExportOptions.MosaicMethod;
					aGSExportOptions.Quality = this.spProps.ExportOptions.Quality;
					aGSExportOptions.TransCompression = this.spProps.ExportOptions.TransCompression;
					aGSExportOptions.OrderField = this.spProps.ExportOptions.OrderField;
					aGSExportOptions.OrderBaseValue = this.spProps.ExportOptions.OrderBaseValue;
					aGSExportOptions.MosaicOperator = this.spProps.ExportOptions.MosaicOperator;
					aGSExportOptions.LockRasterID = this.spProps.ExportOptions.LockRasterID;
					aGSExportOptions.Ascending = this.spProps.ExportOptions.Ascending;
				}
				catch
				{
					result = aGSExportOptions;
					return result;
				}
				result = aGSExportOptions;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (!this.CurrentlyAdding)
				{
					this.CurrentlyAdding = true;
					this.ClearStatusMessage();
					this.btnAdd.IsEnabled = false;
					this.btnCancel.IsEnabled = false;
					this.btnExtract.IsEnabled = false;
					this.btnExport.IsEnabled = false;
					Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
					try
					{
						if (this.tvServices.SelectedItem is AGSFeatureServiceLayer)
						{
							AGSFeatureServiceLayer aGSFeatureServiceLayer = this.tvServices.SelectedItem as AGSFeatureServiceLayer;
							if (aGSFeatureServiceLayer == null)
							{
								goto IL_69B;
							}
							if (MSCDataset.ContainsFeatureService(aGSFeatureServiceLayer.FeatureService.Parent.Soap_URL, aGSFeatureServiceLayer.FeatureService.FullName, aGSFeatureServiceLayer.Id))
							{
								this.ShowStatusMessage(aGSFeatureServiceLayer.Name + " " + AfaStrings.AlreadyExistsInDrawing);
								this.btnAdd.IsEnabled = true;
								this.btnCancel.IsEnabled = true;
								this.btnExtract.IsEnabled = true;
								this.btnExport.IsEnabled = false;
								Mouse.OverrideCursor = null;
								this.CurrentlyAdding = false;
								return;
							}
							AGSExportOptions aGSExportOptions = this.InitExportOptions(aGSFeatureServiceLayer);
							aGSExportOptions.WhereClause = this.spProps.WhereClause;
							aGSExportOptions.Dynamic = aGSFeatureServiceLayer.Service.ExportOptions.Dynamic;
							aGSExportOptions.Dynamic = this.spProps.ExportOptions.Dynamic;
							aGSExportOptions.OutputWKT = MSCPrj.CurrentWKT(aGSExportOptions.AcadDocument, aGSFeatureServiceLayer.FeatureService.GetWKT());
							Extent boundingBox = this.spProps.ctrlBoundingBoxExtent.Content as Extent;
							List<int> list = new List<int>();
							list.Add(aGSFeatureServiceLayer.Id);
							aGSExportOptions.BoundingBox = boundingBox;
							try
							{
								try
								{
									this.ShowStatusMessage(AfaStrings.RequestingFeatures);
									base.Visibility = Visibility.Collapsed;
									string text = "";
									if (aGSFeatureServiceLayer.FeatureService.AddService(aGSExportOptions, list, ref text))
									{
										this.ShowStatusMessage(AfaStrings.AddingServiceComplete);
										if (!string.IsNullOrEmpty(text))
										{
											ErrorReport.ShowErrorMessage(text);
										}
									}
									else
									{
										this.ShowStatusMessage(AfaStrings.ErrorAddingService + aGSFeatureServiceLayer.FeatureService.ErrorMessage);
										if (!string.IsNullOrEmpty(text))
										{
											ErrorReport.ShowErrorMessage(text);
										}
									}
									base.Visibility = Visibility.Visible;
								}
								catch
								{
									this.btnAdd.IsEnabled = true;
									this.btnCancel.IsEnabled = true;
									this.btnExtract.IsEnabled = true;
									this.btnExport.IsEnabled = false;
									Mouse.OverrideCursor = null;
									this.ShowStatusMessage(AfaStrings.ErrorRetrievingFeatures);
								}
								goto IL_69B;
							}
							finally
							{
								this.btnAdd.IsEnabled = true;
								this.btnCancel.IsEnabled = true;
								this.btnExtract.IsEnabled = true;
								this.btnExport.IsEnabled = false;
								Mouse.OverrideCursor = null;
							}
						}
						if (this.tvServices.SelectedItem is AGSFeatureService)
						{
							AGSFeatureService aGSFeatureService = this.tvServices.SelectedItem as AGSFeatureService;
							if (aGSFeatureService != null)
							{
								this.spProps.IsEnabled = false;
								base.IsEnabled = false;
								List<int> list2 = new List<int>();
								foreach (object current in aGSFeatureService.Items)
								{
									try
									{
										AGSFeatureServiceLayer aGSFeatureServiceLayer2 = current as AGSFeatureServiceLayer;
										if (aGSFeatureServiceLayer2.IsSelected)
										{
											if (MSCDataset.ContainsFeatureService(aGSFeatureService.Parent.Soap_URL, aGSFeatureService.FullName, aGSFeatureServiceLayer2.Id))
											{
												this.ShowStatusMessage(aGSFeatureServiceLayer2.Name + " " + AfaStrings.AlreadyExistsInDrawing);
											}
											else
											{
												list2.Add(aGSFeatureServiceLayer2.Id);
											}
										}
									}
									catch
									{
									}
								}
								if (list2.Count == 0)
								{
									MessageBoxEx.Show(new WindowWrapper(Autodesk.AutoCAD.ApplicationServices.Core.Application.MainWindow.Handle), AfaStrings.NoValidFeatureServiceLayersFound, 20000u);
								}
								else
								{
									AGSExportOptions eo = this.InitExportOptions(aGSFeatureService);
									object arg_3B3_0 = this.spProps.ctrlBoundingBoxExtent.Content;
									try
									{
										this.ShowStatusMessage(AfaStrings.RequestingFeatures);
										string text2 = "";
										if (aGSFeatureService.AddService(eo, list2, ref text2))
										{
											this.ShowStatusMessage(AfaStrings.ExtractingFeaturesComplete);
										}
										else
										{
											this.ShowStatusMessage(AfaStrings.ErrorExtractingFeatures);
										}
										if (!string.IsNullOrEmpty(text2))
										{
											ErrorReport.ShowErrorMessage(text2);
										}
									}
									catch
									{
										this.btnAdd.IsEnabled = true;
										this.btnCancel.IsEnabled = true;
										this.btnExtract.IsEnabled = true;
										this.btnExport.IsEnabled = false;
										Mouse.OverrideCursor = null;
										this.ShowStatusMessage(AfaStrings.ErrorRetrievingFeatures);
										this.spProps.IsEnabled = true;
										base.IsEnabled = true;
									}
								}
							}
							try
							{
								this.btnAdd.IsEnabled = true;
								this.btnCancel.IsEnabled = true;
								this.btnExtract.IsEnabled = true;
								this.btnExport.IsEnabled = false;
								Mouse.OverrideCursor = null;
								this.spProps.IsEnabled = true;
								base.IsEnabled = true;
								goto IL_69B;
							}
							catch
							{
								goto IL_69B;
							}
						}
						if (this.tvServices.SelectedItem is AGSMapService)
						{
							AGSMapService aGSMapService = this.tvServices.SelectedItem as AGSMapService;
							if (aGSMapService != null)
							{
								List<AGSMapLayer> list3 = new List<AGSMapLayer>();
								foreach (object current2 in aGSMapService.Items)
								{
									try
									{
										AGSMapLayer item = current2 as AGSMapLayer;
										list3.Add(item);
									}
									catch
									{
									}
								}
								AGSExportOptions eo2 = this.InitExportOptions(aGSMapService);
								try
								{
									this.ShowStatusMessage(AfaStrings.MapRequested);
									if (aGSMapService.AddService(eo2, list3))
									{
										this.ShowStatusMessage(AfaStrings.AddingServiceComplete);
									}
									else
									{
										this.ShowStatusMessage(AfaStrings.ErrorAddingService + aGSMapService.ErrorMessage);
									}
								}
								catch
								{
								}
								this.btnAdd.IsEnabled = true;
								this.btnCancel.IsEnabled = true;
								this.btnExtract.IsEnabled = true;
								this.btnExport.IsEnabled = true;
								Mouse.OverrideCursor = null;
							}
						}
						else if (this.tvServices.SelectedItem is AGSImageService)
						{
							AGSImageService aGSImageService = this.tvServices.SelectedItem as AGSImageService;
							if (aGSImageService != null)
							{
								AGSExportOptions eo3 = this.InitExportOptions(aGSImageService);
								try
								{
									this.ShowStatusMessage(AfaStrings.MapRequested);
									if (aGSImageService.AddService(eo3))
									{
										this.ShowStatusMessage(AfaStrings.AddingServiceComplete);
									}
									else
									{
										this.ShowStatusMessage(AfaStrings.ErrorAddingService + aGSImageService.ErrorMessage);
									}
								}
								catch
								{
								}
								this.btnAdd.IsEnabled = true;
								this.btnCancel.IsEnabled = true;
								this.btnExtract.IsEnabled = true;
								this.btnExport.IsEnabled = true;
								Mouse.OverrideCursor = null;
							}
						}
						else
						{
							ErrorReport.ShowErrorMessage("DEBUG: Add isn't implemented in this release");
							this.btnAdd.IsEnabled = true;
							this.btnCancel.IsEnabled = true;
							this.btnExtract.IsEnabled = true;
							this.btnExport.IsEnabled = true;
							Mouse.OverrideCursor = null;
						}
						IL_69B:
						this.CurrentlyAdding = false;
					}
					catch (SystemException)
					{
						this.CurrentlyAdding = false;
						this.btnAdd.IsEnabled = true;
						this.btnCancel.IsEnabled = true;
						this.btnExtract.IsEnabled = true;
						this.btnExport.IsEnabled = true;
						Mouse.OverrideCursor = null;
					}
				}
			}
			catch
			{
			}
		}

		private void btnExport_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				AGSObject aGSObject = this.tvServices.SelectedItem as AGSObject;
				Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
				saveFileDialog.Title = AfaStrings.SaveLocalImageFile;
				string formatFileExtension = MSCRasterService.GetFormatFileExtension(aGSObject.ExportOptions.Format);
				if (formatFileExtension == null)
				{
					formatFileExtension = MSCRasterService.GetFormatFileExtension("PNG24");
				}
				saveFileDialog.DefaultExt = formatFileExtension;
				string fileName = aGSObject.Name + "." + formatFileExtension;
				saveFileDialog.FileName = fileName;
				saveFileDialog.Filter = string.Format("{0} {1}|*.{0}", formatFileExtension, AfaStrings.Files);
				saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
				saveFileDialog.CheckFileExists = false;
				saveFileDialog.OverwritePrompt = true;
				saveFileDialog.CheckPathExists = true;
				bool? flag = saveFileDialog.ShowDialog();
				while (flag == true && File.Exists(saveFileDialog.FileName))
				{
					try
					{
						File.Delete(saveFileDialog.FileName);
					}
					catch (Exception ex)
					{
						ErrorReport.ShowErrorMessage(ex.Message);
						flag = saveFileDialog.ShowDialog();
					}
				}
				if (flag == true)
				{
					string fileName2 = saveFileDialog.FileName;
					AGSMapService aGSMapService = aGSObject as AGSMapService;
					if (aGSMapService != null)
					{
						List<string> list = new List<string>();
						foreach (object current in aGSMapService.Items)
						{
							try
							{
								AGSMapLayer aGSMapLayer = current as AGSMapLayer;
								if (aGSMapLayer.IsVisible)
								{
									list.Add(aGSMapLayer.Name);
								}
							}
							catch
							{
							}
						}
						AGSExportOptions aGSExportOptions = this.InitExportOptions(aGSMapService);
						aGSExportOptions.OutputFile = fileName2;
						aGSExportOptions.AcadDocument = AfaDocData.ActiveDocData.Document;
						if (!aGSMapService.ExportMapNow(aGSExportOptions, new ExportImageEventHandler(this.AddRasterImage), null))
						{
							ErrorReport.ShowErrorMessage(aGSMapService.ErrorMessage);
						}
						else
						{
							App.TempFiles.Remove(aGSExportOptions.OutputFile);
							this.ShowStatusMessage(AfaStrings.MapExportRequested);
						}
					}
					AGSImageService aGSImageService = aGSObject as AGSImageService;
					if (aGSImageService != null)
					{
						AGSExportOptions aGSExportOptions2 = this.InitExportOptions(aGSImageService);
						aGSExportOptions2.AcadDocument = AfaDocData.ActiveDocData.Document;
						aGSExportOptions2.OutputFile = fileName2;
						this.ShowStatusMessage(AfaStrings.ExportingImageService);
						aGSImageService.ExportImage(aGSExportOptions2, new ExportImageEventHandler(this.AddRasterImage));
						App.TempFiles.Remove(aGSExportOptions2.OutputFile);
						this.ClearProgressBar();
					}
				}
			}
			catch
			{
				ErrorReport.ShowErrorMessage(AfaStrings.ErrorGeneratingExport);
			}
		}

		private void btnExtract_Click(object sender, RoutedEventArgs e)
		{
			this.btnAdd.IsEnabled = false;
			this.btnCancel.IsEnabled = false;
			this.btnExtract.IsEnabled = false;
			this.btnExport.IsEnabled = false;
			try
			{
				AGSObject aGSObject = this.tvServices.SelectedItem as AGSObject;
				AGSMapService aGSMapService = aGSObject as AGSMapService;
				if (aGSMapService != null)
				{
					List<int> list = new List<int>();
					foreach (object current in aGSMapService.Items)
					{
						try
						{
							AGSMapLayer aGSMapLayer = current as AGSMapLayer;
							if (aGSMapLayer.IsVisible && aGSMapLayer.IsFeatureLayer)
							{
								list.Add(aGSMapLayer.Id);
							}
							if (aGSMapLayer.IsGroupLayer)
							{
								foreach (object current2 in aGSMapLayer.Items)
								{
									AGSMapLayer aGSMapLayer2 = current2 as AGSMapLayer;
									if (aGSMapLayer2.IsParentVisible && aGSMapLayer2.IsVisible && aGSMapLayer2.IsFeatureLayer)
									{
										list.Add(aGSMapLayer2.Id);
									}
								}
							}
						}
						catch
						{
						}
					}
					try
					{
						Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
						AGSExportOptions aGSExportOptions = this.InitExportOptions(aGSMapService);
						aGSExportOptions.AcadDocument = AfaDocData.ActiveDocData.Document;
						base.Visibility = Visibility.Collapsed;
						string text = "";
						if (aGSMapService.ExportFeatures(aGSExportOptions, list, ref text))
						{
							this.ShowStatusMessage(AfaStrings.ExtractingFeaturesComplete);
						}
						else
						{
							this.ShowStatusMessage(AfaStrings.ErrorExtractingFeatures);
						}
						if (!string.IsNullOrEmpty(text))
						{
							ErrorReport.ShowErrorMessage(text);
						}
						base.Visibility = Visibility.Visible;
						Mouse.OverrideCursor = null;
						this.btnAdd.IsEnabled = true;
						this.btnCancel.IsEnabled = true;
						this.btnExtract.IsEnabled = true;
						this.btnExport.IsEnabled = true;
						return;
					}
					catch
					{
						this.btnAdd.IsEnabled = true;
						this.btnCancel.IsEnabled = true;
						this.btnExtract.IsEnabled = true;
						this.btnExport.IsEnabled = true;
						Mouse.OverrideCursor = null;
						return;
					}
				}
				AGSMapLayer aGSMapLayer3 = this.tvServices.SelectedItem as AGSMapLayer;
				if (aGSMapLayer3 != null)
				{
					AGSExportOptions aGSExportOptions2 = this.InitExportOptions(aGSMapLayer3);
					List<int> list2 = new List<int>();
					if (aGSMapLayer3.IsGroupLayer)
					{
						using (IEnumerator<int> enumerator3 = aGSMapLayer3.ChildLayerIds.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								int current3 = enumerator3.Current;
								list2.Add(current3);
							}
							goto IL_27D;
						}
					}
					list2.Add(aGSMapLayer3.Id);
					IL_27D:
					try
					{
						this.ShowStatusMessage(AfaStrings.RequestingFeatures);
						Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
						MSCPrj.ReadWKT(AfaDocData.ActiveDocData.Document);
						base.Visibility = Visibility.Collapsed;
						aGSExportOptions2.OutputWKT = MSCPrj.CurrentWKT(AfaDocData.ActiveDocData.Document, aGSMapLayer3.Map.GetWKT());
						aGSExportOptions2.AcadDocument = AfaDocData.ActiveDocData.Document;
						aGSExportOptions2.WhereClause = this.spProps.WhereClause;
						string text2 = "";
						if (aGSMapLayer3.Map.ExportFeatures(aGSExportOptions2, list2, ref text2))
						{
							this.ShowStatusMessage(AfaStrings.ExtractingFeaturesComplete);
						}
						else
						{
							this.ShowStatusMessage(AfaStrings.ErrorExtractingFeatures);
						}
						if (!string.IsNullOrEmpty(text2))
						{
							ErrorReport.ShowErrorMessage(text2);
						}
						base.Visibility = Visibility.Visible;
						Mouse.OverrideCursor = null;
					}
					catch
					{
						this.btnAdd.IsEnabled = true;
						this.btnCancel.IsEnabled = true;
						this.btnExtract.IsEnabled = true;
						this.btnExport.IsEnabled = true;
						this.ShowStatusMessage(AfaStrings.ErrorRetrievingFeatures);
						Mouse.OverrideCursor = null;
					}
				}
				AGSFeatureService aGSFeatureService = aGSObject as AGSFeatureService;
				if (aGSFeatureService != null)
				{
					List<int> list3 = new List<int>();
					foreach (object current4 in aGSFeatureService.Items)
					{
						try
						{
							AGSFeatureServiceLayer aGSFeatureServiceLayer = current4 as AGSFeatureServiceLayer;
							if (aGSFeatureServiceLayer.IsSelected)
							{
								list3.Add(aGSFeatureServiceLayer.Id);
							}
						}
						catch
						{
						}
					}
					try
					{
						Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
						AGSExportOptions eo = this.InitExportOptions(aGSFeatureService);
						this.spProps.IsEnabled = false;
						base.IsEnabled = false;
						string text3 = "";
						if (aGSFeatureService.ExportFeatures(eo, list3, ref text3))
						{
							this.ShowStatusMessage(AfaStrings.ExtractingFeaturesComplete);
						}
						else
						{
							this.ShowStatusMessage(AfaStrings.ErrorExtractingFeatures);
						}
						if (!string.IsNullOrEmpty(text3))
						{
							ErrorReport.ShowErrorMessage(text3);
						}
						Mouse.OverrideCursor = null;
						this.btnAdd.IsEnabled = true;
						this.btnCancel.IsEnabled = true;
						this.btnExtract.IsEnabled = true;
						this.spProps.IsEnabled = true;
						base.IsEnabled = true;
						return;
					}
					catch
					{
						this.btnAdd.IsEnabled = true;
						this.btnCancel.IsEnabled = true;
						this.btnExtract.IsEnabled = true;
						this.btnExport.IsEnabled = true;
						Mouse.OverrideCursor = null;
						this.spProps.IsEnabled = true;
						base.IsEnabled = true;
						return;
					}
				}
				AGSFeatureServiceLayer aGSFeatureServiceLayer2 = this.tvServices.SelectedItem as AGSFeatureServiceLayer;
				if (aGSFeatureServiceLayer2 != null)
				{
					AGSExportOptions aGSExportOptions3 = this.InitExportOptions(aGSFeatureServiceLayer2);
					List<int> list4 = new List<int>();
					list4.Add(aGSFeatureServiceLayer2.Id);
					try
					{
						this.ShowStatusMessage(AfaStrings.RequestingFeatures);
						Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
						aGSExportOptions3.OutputWKT = MSCPrj.CurrentWKT(aGSExportOptions3.AcadDocument, aGSFeatureServiceLayer2.FeatureService.GetWKT());
						base.Visibility = Visibility.Collapsed;
						string text4 = "";
						if (aGSFeatureServiceLayer2.FeatureService.ExportFeatures(aGSExportOptions3, list4, ref text4))
						{
							this.ShowStatusMessage(AfaStrings.ExtractingFeaturesComplete);
						}
						else
						{
							this.ShowStatusMessage(AfaStrings.ErrorExtractingFeatures);
						}
						if (!string.IsNullOrEmpty(text4))
						{
							ErrorReport.ShowErrorMessage(text4);
						}
						base.Visibility = Visibility.Visible;
						Mouse.OverrideCursor = null;
					}
					catch
					{
						this.btnAdd.IsEnabled = true;
						this.btnCancel.IsEnabled = true;
						this.btnExtract.IsEnabled = true;
						base.Visibility = Visibility.Visible;
						this.ShowStatusMessage(AfaStrings.ErrorRetrievingFeatures);
						Mouse.OverrideCursor = null;
					}
				}
				this.btnAdd.IsEnabled = true;
				this.btnCancel.IsEnabled = true;
				this.btnExtract.IsEnabled = true;
				this.btnExport.IsEnabled = true;
			}
			catch
			{
				try
				{
					base.Visibility = Visibility.Visible;
					Mouse.OverrideCursor = null;
					this.btnAdd.IsEnabled = true;
					this.btnCancel.IsEnabled = true;
					this.btnExtract.IsEnabled = true;
					this.btnExport.IsEnabled = true;
				}
				catch
				{
				}
			}
		}

		private void btnBack_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
			Autodesk.AutoCAD.ApplicationServices.Core.Application.ShowModalWindow(new SelectConnection
			{
				lbConnections = 
				{
					ItemsSource = App.Connections
				}
			});
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				base.Close();
			}
			catch
			{
			}
		}

		private void tvServices_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			try
			{
				object selectedItem = this.tvServices.SelectedItem;
				this.ClearStatusMessage();
				AGSLayer aGSLayer = selectedItem as AGSLayer;
				if (aGSLayer != null)
				{
					this.btnExport.Visibility = Visibility.Collapsed;
					this.btnExtract.Visibility = Visibility.Collapsed;
					if (aGSLayer.IsFeatureLayer || aGSLayer.IsGroupLayer)
					{
						IAGSMapService iAGSMapService = aGSLayer.Service as IAGSMapService;
						if (iAGSMapService != null && iAGSMapService.SupportsData)
						{
							this.btnExtract.Visibility = Visibility.Visible;
							this.btnExtract.IsEnabled = true;
						}
						else
						{
							this.btnExtract.Visibility = Visibility.Collapsed;
						}
					}
					this.btnAdd.Visibility = Visibility.Collapsed;
					IAGSFeatureServiceLayer iAGSFeatureServiceLayer = aGSLayer as IAGSFeatureServiceLayer;
					if (iAGSFeatureServiceLayer != null)
					{
						this.btnAdd.Visibility = Visibility.Visible;
						this.btnAdd.IsEnabled = true;
						this.btnExtract.Visibility = Visibility.Visible;
						this.btnExtract.IsEnabled = true;
					}
				}
				else
				{
					AGSMapService aGSMapService = selectedItem as AGSMapService;
					if (aGSMapService != null)
					{
						this.btnAdd.Visibility = Visibility.Visible;
						this.btnAdd.IsEnabled = true;
						if (aGSMapService.SupportsData && aGSMapService.ContainsFeatureLayers)
						{
							this.btnExtract.Visibility = Visibility.Visible;
							this.btnExtract.IsEnabled = true;
						}
						else
						{
							this.btnExtract.Visibility = Visibility.Collapsed;
						}
						if (aGSMapService.SupportsDisconnect())
						{
							this.btnExport.Visibility = Visibility.Visible;
							this.btnExport.IsEnabled = true;
						}
						else
						{
							this.btnExport.Visibility = Visibility.Collapsed;
						}
					}
					else
					{
						AGSImageService aGSImageService = selectedItem as AGSImageService;
						if (aGSImageService != null)
						{
							this.btnAdd.Visibility = Visibility.Visible;
							this.btnAdd.IsEnabled = true;
							this.btnExtract.Visibility = Visibility.Collapsed;
							this.btnExtract.IsEnabled = true;
							this.btnExport.Visibility = Visibility.Visible;
							this.btnExport.IsEnabled = true;
						}
						else
						{
							AGSFeatureService aGSFeatureService = selectedItem as AGSFeatureService;
							if (aGSFeatureService != null)
							{
								this.btnAdd.Visibility = Visibility.Visible;
								this.btnExtract.Visibility = Visibility.Visible;
								this.btnExport.Visibility = Visibility.Collapsed;
								this.btnAdd.IsEnabled = true;
								this.btnExtract.IsEnabled = true;
							}
							else
							{
								this.btnAdd.Visibility = Visibility.Collapsed;
								this.btnExtract.Visibility = Visibility.Collapsed;
								this.btnExport.Visibility = Visibility.Collapsed;
							}
						}
					}
				}
				this.spProps.SetService(this.tvServices.SelectedItem);
			}
			catch
			{
			}
		}

		private void AddRasterImage(object sender, ExportedImageEventArgs e)
		{
			try
			{
				this.ClearStatusMessage();
				if (!string.IsNullOrEmpty(e.ErrorMessage))
				{
					ErrorReport.ShowErrorMessage(e.ErrorMessage);
				}
				else if (e.Points != null && !string.IsNullOrEmpty(e.ExportOptions.OutputFile))
				{
					Point3d point3d = new Point3d(e.Points[0].X, e.Points[0].Y, 0.0);
					Point3d point3d2 = new Point3d(e.Points[1].X, e.Points[1].Y, 0.0);
					Point3d point3d3 = new Point3d(e.Points[2].X, e.Points[2].Y, 0.0);
					Vector3d v = point3d2 - point3d;
					Vector3d v2 = point3d3 - point3d;
					this.ShowStatusMessage(AfaStrings.AddingExportToDrawing);
					DocUtil.DefineRasterImage(e.ExportOptions.AcadDocument, e.ExportOptions.OutputFile, point3d, v, v2, "", e.ExportOptions.Transparency);
					this.ClearStatusMessage();
				}
				else
				{
					ErrorReport.ShowErrorMessage(AfaStrings.ErrorGeneratingImage);
				}
			}
			catch
			{
				this.ShowStatusMessage(AfaStrings.ErrorInDefiningExportedImage);
			}
		}

		private void tvServices_OnMouseDoubleClick(object sender, System.Windows.Input.MouseEventArgs e)
		{
			try
			{
				if (this.tvServices.SelectedItem is AGSMapService || this.tvServices.SelectedItem is AGSFeatureService || this.tvServices.SelectedItem is AGSFeatureServiceLayer || this.tvServices.SelectedItem is AGSImageService)
				{
					IEnumerable itemsSource = this.spProps.lbServiceInfo.ItemsSource;
					this.spProps.lbServiceInfo.ItemsSource = null;
					this.btnAdd_Click(sender, e);
					this.spProps.lbServiceInfo.ItemsSource = itemsSource;
				}
			}
			catch
			{
			}
		}

		private void ShowStatusMessage(string msg)
		{
			try
			{
				this.ClearStatusMessage();
				System.Windows.Controls.Label label = new System.Windows.Controls.Label();
				label.Content = msg;
				label.FontSize *= 0.75;
				this.sbar.Items.Add(label);
				this.sbar.Refresh();
			}
			catch
			{
			}
		}

		private void ClearStatusMessage()
		{
			try
			{
				if (this.sbar != null && this.sbar.Items != null)
				{
					this.sbar.Items.Clear();
				}
			}
			catch
			{
			}
		}

		private void StartProgressBar(string msg)
		{
			this.sbar.Items.Clear();
			System.Windows.Controls.Label label = new System.Windows.Controls.Label();
			label.Background = new LinearGradientBrush(Colors.LightBlue, Colors.SlateBlue, 90.0);
			label.Content = msg;
			this.sbar.Items.Add(label);
			System.Windows.Controls.ProgressBar progressBar = new System.Windows.Controls.ProgressBar();
			progressBar.Width = 150.0;
			progressBar.Height = 15.0;
			Duration duration = new Duration(TimeSpan.FromSeconds(1.0));
			DoubleAnimation doubleAnimation = new DoubleAnimation(100.0, duration);
			doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
			progressBar.BeginAnimation(RangeBase.ValueProperty, doubleAnimation);
			this.sbar.Items.Add(progressBar);
		}

		private void ClearProgressBar()
		{
			try
			{
				this.sbar.Items.Clear();
			}
			catch
			{
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			SelectConnection.WindowOpen = true;
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/ArcGISForAutoCAD;component/ui/selectservice.xaml", UriKind.Relative);
			System.Windows.Application.LoadComponent(this, resourceLocator);
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				((SelectService)target).Closing += new CancelEventHandler(this.OnWindowClosing);
				((SelectService)target).Loaded += new RoutedEventHandler(this.Window_Loaded);
				return;
			case 2:
				this.tvServices = (System.Windows.Controls.TreeView)target;
				this.tvServices.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(this.tvServices_SelectedItemChanged);
				this.tvServices.MouseDoubleClick += new MouseButtonEventHandler(this.tvServices_OnMouseDoubleClick);
				return;
			case 3:
				this.spProps = (ServiceProperties)target;
				return;
			case 4:
				this.sbar = (System.Windows.Controls.Primitives.StatusBar)target;
				return;
			case 5:
				this.btnAdd = (System.Windows.Controls.Button)target;
				this.btnAdd.Click += new RoutedEventHandler(this.btnAdd_Click);
				return;
			case 6:
				this.btnExport = (System.Windows.Controls.Button)target;
				this.btnExport.Click += new RoutedEventHandler(this.btnExport_Click);
				return;
			case 7:
				this.btnExtract = (System.Windows.Controls.Button)target;
				this.btnExtract.Click += new RoutedEventHandler(this.btnExtract_Click);
				return;
			case 8:
				this.btnCancel = (System.Windows.Controls.Button)target;
				this.btnCancel.Click += new RoutedEventHandler(this.btnCancel_Click);
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}
	}
}
