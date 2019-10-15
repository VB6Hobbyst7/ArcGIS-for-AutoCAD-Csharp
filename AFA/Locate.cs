using AFA.Resources;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using ESRI_TableViewer;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;


namespace AFA
{
    public class Locate : Window, IComponentConnector
	{
		internal TextBlock tbPlaceWatermarked;

		internal TextBox tbPlace;

		internal TextBlock tbDescription;

		internal ComboBox cbLocator;

		internal Button btnFind;

		internal Button btnClose;

		internal TextBlock textBlock1;

		private bool _contentLoaded;

		public Locate()
		{
			if (App.Locators.Count == 0)
			{
				AGSLocator.LoadLocators();
			}
			this.InitializeComponent();
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			this.HideMinimizeAndMaximizeButtons();
		}

		private void btnFind_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
			this.PerformLocate(this.tbPlace.Text);
		}

		private bool PerformLocate(string locateText)
		{
			Document document = AfaDocData.ActiveDocData.Document;
			Editor editor = document.Editor;
			if (string.IsNullOrEmpty(locateText))
			{
				editor.WriteMessage(AfaStrings.InvalidPlaceText);
				return false;
			}
			AGSGeometryServer sampleServer = AGSGeometryServer.GetSampleServer();
			if (sampleServer == null)
			{
				ErrorReport.ShowErrorMessage(AfaStrings.UnableToConnectGeometryService);
				return false;
			}
			string text = "PROJCS[\"WGS_1984_Web_Mercator_Auxiliary_Sphere\",GEOGCS[\"GCS_WGS_1984\",DATUM[\"D_WGS_1984\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Mercator_Auxiliary_Sphere\"],PARAMETER[\"False_Easting\",0.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",0.0],PARAMETER[\"Standard_Parallel_1\",0.0],PARAMETER[\"Auxiliary_Sphere_Type\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3857]]".ToString();
			bool result;
			try
			{
				text = MSCPrj.ReadWKT(document);
				if (string.IsNullOrEmpty(text))
				{
					text = "PROJCS[\"WGS_1984_Web_Mercator_Auxiliary_Sphere\",GEOGCS[\"GCS_WGS_1984\",DATUM[\"D_WGS_1984\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Mercator_Auxiliary_Sphere\"],PARAMETER[\"False_Easting\",0.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",0.0],PARAMETER[\"Standard_Parallel_1\",0.0],PARAMETER[\"Auxiliary_Sphere_Type\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3857]]".ToString();
					MSCPrj.AssignWKT(document, "PROJCS[\"WGS_1984_Web_Mercator_Auxiliary_Sphere\",GEOGCS[\"GCS_WGS_1984\",DATUM[\"D_WGS_1984\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Mercator_Auxiliary_Sphere\"],PARAMETER[\"False_Easting\",0.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",0.0],PARAMETER[\"Standard_Parallel_1\",0.0],PARAMETER[\"Auxiliary_Sphere_Type\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3857]]");
				}
			}
			catch
			{
				ErrorReport.ShowErrorMessage(AfaStrings.ErrorCreatingWKT);
				result = false;
				return result;
			}
			if (!MSCPrj.IsWKID(text))
			{
				try
				{
					text = sampleServer.GetSpatialReferenceWKID(text);
					if (string.IsNullOrEmpty(text))
					{
						string text2 = sampleServer.ErrorMessage;
						if (string.IsNullOrEmpty(text2))
						{
							text2 = AfaStrings.UnsupportedCoordinateSystemForLocate;
						}
						ErrorReport.ShowErrorMessage(text2);
						result = false;
						return result;
					}
				}
				catch
				{
					text = "PROJCS[\"WGS_1984_Web_Mercator_Auxiliary_Sphere\",GEOGCS[\"GCS_WGS_1984\",DATUM[\"D_WGS_1984\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Mercator_Auxiliary_Sphere\"],PARAMETER[\"False_Easting\",0.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",0.0],PARAMETER[\"Standard_Parallel_1\",0.0],PARAMETER[\"Auxiliary_Sphere_Type\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3857]]";
				}
			}
			try
			{
				Convert.ToInt32(text);
			}
			catch
			{
				editor.WriteMessage(AfaStrings.UnsupportedCoordinateSystemForLocate);
				result = false;
				return result;
			}
			Mouse.OverrideCursor = Cursors.Wait;
			AGSLocator aGSLocator = this.cbLocator.SelectedItem as AGSLocator;
			string uRL = aGSLocator.URL;
			string arg = uRL.TrimEnd(new char[]
			{
				'/',
				'\\'
			});
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("/findAddressCandidates?SingleLine=");
			stringBuilder.Append(this.tbPlace.Text);
			stringBuilder.AppendFormat("&outSR={0}", text);
			stringBuilder.AppendFormat("&f={0}", "json");
			try
			{
				IDictionary<string, object> dictionary = aGSLocator.ParentConnection.MakeDictionaryRequest(arg + stringBuilder);
				if (dictionary.ContainsKey("error"))
				{
					editor.WriteMessage(AfaStrings.ErrorConnectingToServer);
					Mouse.OverrideCursor = null;
					result = false;
				}
				else
				{
					CadField cadField = new CadField();
					cadField.Name = AfaStrings.SearchString;
					cadField.Value = new TypedValue(1, this.tbPlace.Text);
					cadField.ReadOnly = true;
					if (dictionary.ContainsKey("candidates"))
					{
						DocUtil.FixPDMode();
						using (document.LockDocument((DocumentLockMode)20, null, null, false))
						{
							List<ObjectId> list = new List<ObjectId>();
							Database database = document.Database;
							try
							{
								using (Transaction transaction = document.TransactionManager.StartTransaction())
								{
									BlockTable blockTable = (BlockTable)transaction.GetObject(database.BlockTableId, (OpenMode)1, false);
									BlockTableRecord blockTableRecord = (BlockTableRecord)transaction.GetObject(blockTable[(BlockTableRecord.ModelSpace)], (OpenMode)1, false);
									this.CreateLocationsFeatureClass();
									DocUtil.FixPDMode();
									string text3 = "ESRI_Locations";
									ObjectId layer = DocUtil.GetLayer(database, transaction, ref text3, null);
									ObjectId blockDefinition = DocUtil.GetBlockDefinition(document, transaction, "ESRI_Locations");
									IEnumerable<object> enumerable = dictionary["candidates"] as IEnumerable<object>;
									using (IEnumerator<object> enumerator = enumerable.GetEnumerator())
									{
										while (enumerator.MoveNext())
										{
											IDictionary<string, object> dictionary2 = (IDictionary<string, object>)enumerator.Current;
											CadField cadField2 = null;
											CadField cadField3 = null;
											if (dictionary2.ContainsKey("address"))
											{
												string text4 = dictionary2["address"].ToString();
												cadField3 = new CadField();
												cadField3.Name = "Address";
												cadField3.Value = new TypedValue(1, text4);
												cadField3.ReadOnly = true;
											}
											if (dictionary2.ContainsKey("score"))
											{
												try
												{
													int num = Convert.ToInt32(dictionary2["score"]);
													cadField2 = new CadField();
													cadField2.Name = "Score";
													cadField2.Value = new TypedValue(90, num);
													cadField2.ReadOnly = true;
												}
												catch
												{
												}
											}
											if (dictionary2.ContainsKey("location"))
											{
												try
												{
													IDictionary<string, object> dictionary3 = dictionary2["location"] as IDictionary<string, object>;
													double num2 = Convert.ToDouble(dictionary3["x"]);
													double num3 = Convert.ToDouble(dictionary3["y"]);
													Entity entity;
													if (blockDefinition != ObjectId.Null)
													{
														entity = new BlockReference(new Point3d(num2, num3, 0.0), blockDefinition);
													}
													else
													{
														entity = new DBPoint(new Point3d(num2, num3, 0.0));
													}
													entity.LayerId=(layer);
													entity.ColorIndex=(256);
													ObjectId item = blockTableRecord.AppendEntity(entity);
													transaction.AddNewlyCreatedDBObject(entity, true);
													list.Add(item);
													if (cadField3 != null)
													{
														CadField.AddCadAttributeToEntity(database, transaction, entity.ObjectId, cadField3);
													}
													if (cadField2 != null)
													{
														CadField.AddCadAttributeToEntity(database, transaction, entity.ObjectId, cadField2);
													}
													CadField.AddCadAttributeToEntity(database, transaction, entity.ObjectId, cadField);
													document.TransactionManager.QueueForGraphicsFlush();
													document.TransactionManager.FlushGraphics();
													document.Editor.UpdateScreen();
												}
												catch
												{
												}
											}
										}
									}
									transaction.Commit();
									if (list.Count > 0)
									{
										ObjectId[] array = list.ToArray();
										DocUtil.ZoomToEntity(array);
										AfaDocData.ActiveDocData.DocDataset.UpdateMaps();
										AfaDocData.ActiveDocData.Document.Editor.UpdateScreen();
										AfaDocData.ActiveDocData.Document.Editor.Regen();
										Mouse.OverrideCursor = null;
										CmdLine.CancelActiveCommand();
										TableView tableView = new TableView(AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype(), array);
                                        Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Autodesk.AutoCAD.ApplicationServices.Application.MainWindow.Handle, tableView, false);
										tableView.Uninitialize();
										tableView.Dispose();
										this.SelectAndDelete();
										AfaDocData.ActiveDocData.DocDataset.UpdateMaps();
										AfaDocData.ActiveDocData.Document.Editor.UpdateScreen();
										AfaDocData.ActiveDocData.Document.Editor.Regen();
										result = true;
										return result;
									}
									Mouse.OverrideCursor = null;
									editor.WriteMessage(AfaStrings.NoFeaturesFound + "  (" + this.tbPlace.Text + ")");
									result = false;
									return result;
								}
							}
							catch
							{
								Mouse.OverrideCursor = null;
							}
						}
					}
					Mouse.OverrideCursor = null;
					result = false;
				}
			}
			catch
			{
				Mouse.OverrideCursor = null;
				editor.WriteMessage(AfaStrings.ErrorConnectingToServer);
				result = false;
			}
			return result;
		}

		private void SelectAndDelete()
		{
			MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
			if (AfaDocData.ActiveDocData.DocDataset.FeatureClasses.ContainsKey("ESRI_Locations"))
			{
				MSCFeatureClass mSCFeatureClass = AfaDocData.ActiveDocData.DocDataset.FeatureClasses["ESRI_Locations"];
				mSCFeatureClass.DeleteEntities();
				docDataset.RemoveFeatureClass(mSCFeatureClass);
				MSCDataset.SetDefaultActiveFeatureClass();
			}
		}

		private void btnClose_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		private void CreateLocationsFeatureClass()
		{
			if (AfaDocData.ActiveDocData.DocDataset.FeatureClasses.ContainsKey("ESRI_Locations"))
			{
				ArcGISRibbon.SetActiveFeatureClass(AfaDocData.ActiveDocData.DocDataset.FeatureClasses["ESRI_Locations"]);
				return;
			}
			try
			{
				MSCDataset.AddSimpleFeatureClass("ESRI_Locations", "esriGeometryPoint", new List<string>
				{
					"ESRI_Locations"
				}, new List<CadField>
				{
					new CadField
					{
						Name = "Score",
						Value = new TypedValue(90, 100),
						ReadOnly = true
					},
					new CadField
					{
						Name = AfaStrings.SearchString,
						Value = new TypedValue(1, ""),
						Length = 254,
						ReadOnly = true
					},
					new CadField
					{
						Name = "Address",
						Value = new TypedValue(1, "Address"),
						Length = 254,
						ReadOnly = true
					}
				}, true, null);
				ArcGISRibbon.SetActiveFeatureClass(AfaDocData.ActiveDocData.DocDataset.FeatureClasses["ESRI_Locations"]);
			}
			catch
			{
			}
		}

		private void cbLocator_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				if (this.cbLocator.SelectedItem != null)
				{
					AGSLocator aGSLocator = (AGSLocator)this.cbLocator.SelectedValue;
					if (aGSLocator != null)
					{
						string description = aGSLocator.Description;
						if (string.IsNullOrEmpty(description))
						{
							this.tbDescription.Text = aGSLocator.Name;
						}
						else
						{
							this.tbDescription.Text = description;
						}
					}
				}
			}
			catch
			{
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/ArcGISForAutoCAD;component/ui/locate.xaml", UriKind.Relative);
            System.Windows.Application.LoadComponent(this, resourceLocator);
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				((Locate)target).Loaded += new RoutedEventHandler(this.Window_Loaded);
				return;
			case 2:
				this.tbPlaceWatermarked = (TextBlock)target;
				return;
			case 3:
				this.tbPlace = (TextBox)target;
				return;
			case 4:
				this.tbDescription = (TextBlock)target;
				return;
			case 5:
				this.cbLocator = (ComboBox)target;
				this.cbLocator.SelectionChanged += new SelectionChangedEventHandler(this.cbLocator_SelectionChanged);
				return;
			case 6:
				this.btnFind = (Button)target;
				this.btnFind.Click += new RoutedEventHandler(this.btnFind_Click);
				return;
			case 7:
				this.btnClose = (Button)target;
				this.btnClose.Click += new RoutedEventHandler(this.btnClose_Click);
				return;
			case 8:
				this.textBlock1 = (TextBlock)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}
	}
}
