using AFA.Resources;
using AFA.UI;
using ArcGIS10Types;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;


namespace AFA
{
    public class AGSFeatureService : AGSService, IAGSFeatureService, IAGSService, IAGSObject, ILongProcessCommand
	{
		private long maxCount = 500L;

		private SpatialReference inputSR;

		private FeatureServiceProxy _featureservice;

		private ProgressIndicator _progress;

		private static int MAX_RECOMMENDED = 15000;

		private ServerSymbolOutputOptions _symbolOpts = new ServerSymbolOutputOptions();

		public static bool isUpdatingFeatureService = false;

		public event CommandStartedEventHandler CommandStarted;

		public event CommandEndedEventHandler CommandEnded;

		public event CommandProgressEventHandler CommandProgress;

		public event CommandProgressUpdateValuesEventHandler CommandUpdateProgressValues;

		public IDictionary<int, object> MapLayers
		{
			get;
			set;
		}

		public bool SupportsQuery
		{
			get;
			set;
		}

		public bool SupportsEditing
		{
			get;
			set;
		}

		public Extent FullExtent
		{
			get;
			set;
		}

		public AGSExportOptions ExportOpts
		{
			get;
			set;
		}

		public string ServiceURL
		{
			get;
			set;
		}

		private bool HasZ
		{
			get;
			set;
		}

		private bool HasM
		{
			get;
			set;
		}

		public IList<object> Items
		{
			get
			{
				IList<object> result;
				try
				{
					IList<object> list = new List<object>();
					foreach (KeyValuePair<int, object> current in this.MapLayers)
					{
						AGSFeatureServiceLayer aGSFeatureServiceLayer = current.Value as AGSFeatureServiceLayer;
						if (aGSFeatureServiceLayer.parentLayerId == -1)
						{
							list.Add(current.Value);
						}
					}
					result = list;
				}
				catch
				{
					result = null;
				}
				return result;
			}
		}

		public AGSFeatureService(string serviceFullName, string serviceURL, AGSConnection parent) : base(serviceFullName, parent)
		{
			base.Properties.Add("Type", "Feature Server");
			this.MapLayers = new Dictionary<int, object>();
			if (this.RefreshProperties())
			{
				this.InitializeExportProperties();
				this._symbolOpts.PictureOutputType = esriServerPictureOutputType.esriServerPictureOutputAsPNG;
				this.HasZ = false;
				this.HasM = false;
				base.IsValid = true;
				return;
			}
			base.IsValid = false;
			throw new Exception(base.ErrorMessage);
		}

		private void InitializeExportProperties()
		{
			this.ExportOptions = new AGSExportOptions();
			if (this.SupportsEditing)
			{
				this.ExportOptions.Dynamic = true;
				return;
			}
			this.ExportOptions.Dynamic = false;
		}

		private bool RefreshProperties()
		{
			bool result;
			try
			{
				string arg = base.Parent.URL + "/" + base.FullName + "/FeatureServer";
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("?f={0}", "json");
				IDictionary<string, object> dictionary = base.Parent.MakeDictionaryRequest(arg + stringBuilder);
				if (dictionary != null)
				{
					this.PopulateLayers(dictionary);
					this.PopulateProperties(dictionary);
					result = true;
				}
				else
				{
					if (string.IsNullOrEmpty(base.Parent.ErrorMessage))
					{
						base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errRefreshingProperties;
					}
					result = false;
				}
			}
			catch
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errRefreshingProperties;
				}
				result = false;
			}
			return result;
		}

		private void PopulateLayers(IDictionary<string, object> results)
		{
			try
			{
				if (results.ContainsKey("layers"))
				{
					object obj;
					results.TryGetValue("layers", out obj);
					IList<object> list = obj as IList<object>;
					foreach (object current in list)
					{
						int num = -1;
						string name = "";
						IDictionary<string, object> dictionary = current as IDictionary<string, object>;
						object obj2;
						if (dictionary.TryGetValue("id", out obj2))
						{
							num = (int)obj2;
						}
						if (dictionary.TryGetValue("name", out obj2))
						{
							name = obj2.ToString();
						}
						if (num >= 0)
						{
							AGSFeatureServiceLayer aGSFeatureServiceLayer = new AGSFeatureServiceLayer(num, this);
							aGSFeatureServiceLayer.Name = name;
							aGSFeatureServiceLayer.IsSelected = true;
							aGSFeatureServiceLayer.parentLayerId = -1;
							if (aGSFeatureServiceLayer.GeometryType != "esriGeometryMultipoint")
							{
								this.MapLayers.Add(aGSFeatureServiceLayer.Id, aGSFeatureServiceLayer);
							}
						}
					}
				}
			}
			catch
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errReadingLayerProperties;
				}
				throw;
			}
		}

		private void PopulateProperties(IDictionary<string, object> results)
		{
			object obj;
			if (results.TryGetValue("serviceDescription", out obj))
			{
				base.Properties.Add("Service Description", obj);
			}
			string text = "";
			if (this.SupportsQuery && !this.SupportsEditing)
			{
				text += "Query";
			}
			if (this.SupportsEditing && !this.SupportsQuery)
			{
				text += "Editing";
			}
			if (this.SupportsEditing && this.SupportsQuery)
			{
				text += "Query,Editing";
			}
			base.Properties.Add("Capabilities", text);
			base.Properties.Add("Extent", this.FullExtent);
			if (results.TryGetValue("spatialReference", out obj))
			{
				IDictionary<string, object> dictionary = obj as IDictionary<string, object>;
				if (dictionary.ContainsKey("wkid"))
				{
					object value;
					dictionary.TryGetValue("wkid", out value);
					base.Properties.Add("WKID", value);
				}
				if (dictionary.ContainsKey("wkt"))
				{
					object value2;
					dictionary.TryGetValue("wkt", out value2);
					base.Properties.Add("WKT", value2);
				}
			}
			else
			{
				string spatialReference = this.FullExtent.SpatialReference;
				int num;
				if (int.TryParse(spatialReference, out num))
				{
					base.Properties.Add("WKID", num);
				}
				else
				{
					base.Properties.Add("WKT", spatialReference);
				}
			}
			object value3;
			if (results.TryGetValue("supportedQueryFormats", out value3))
			{
				base.Properties.Add("Supported Query Formats", value3);
			}
			object value4;
			if (results.TryGetValue("hasZ", out value4))
			{
				base.Properties.Add("Has Z", value4);
			}
			results.TryGetValue("enableZDefaults", out value4);
		}

		public override string GetWKT()
		{
			string text = null;
			if (base.Properties.ContainsKey("WKT"))
			{
				return base.Properties["WKT"].ToString();
			}
			if (base.Properties.ContainsKey("WKID"))
			{
				try
				{
					int wKID = Convert.ToInt32(base.Properties["WKID"].ToString());
					text = base.Parent.GeometryService.GetSpatialReferenceWKT(wKID);
					if (!string.IsNullOrEmpty(text))
					{
						base.Properties["WKT"] = text;
					}
				}
				catch
				{
					text = null;
				}
			}
			return text;
		}

		public bool RefreshService(ref MSCFeatureService msc)
		{
           
            if (Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument == null)
			{
				return false;
			}
			MSCFeatureClass mSCFeatureClass = msc;
			bool result;
			try
			{
				base.ErrorMessage = null;
				using (this._progress = new ProgressIndicator(this))
				{
					if (this.CommandStarted != null)
					{
						CommandStartEventArgs commandStartEventArgs = new CommandStartEventArgs();
						commandStartEventArgs.WindowTitle = AfaStrings.Refreshing + base.Name;
						commandStartEventArgs.ProgressMaxValue = 0;
						commandStartEventArgs.ProgressMinValue = 0;
						commandStartEventArgs.PrograssInitValue = 0;
						this.CommandStarted(this, commandStartEventArgs);
						this.ReportExportStatus(AfaStrings.Initializing + " " + base.Name, "");
					}
					string text = MSCPrj.ReadWKT(Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument);
					AfaDocData.ActiveDocData.DocPRJ.WKT = text;
					this.InitializeFeatureService();
					GraphicFeatureLayer[] layers = this._featureservice.GetLayers(this._symbolOpts);
					GraphicFeatureLayer activeLayer = this.GetActiveLayer(msc.ServiceLayerID, layers);
					this.UpdateMSCFeatureService(activeLayer, ref msc);
					msc.ExportOptions.OutputWKT = text;
					this.ExportFeatures(activeLayer, msc.ExportOptions, ref mSCFeatureClass);
					if (string.IsNullOrEmpty(text) && AfaDocData.ActiveDocData != null)
					{
						text = this.GetWKT();
						if (string.IsNullOrEmpty(text))
						{
							result = false;
							return result;
						}
						AfaDocData.ActiveDocData.DocPRJ.WKT = MSCPrj.AssignWKT(AfaDocData.ActiveDocData.Document, text);
					}
					if (this.CommandEnded != null)
					{
						this.CommandEnded(this, EventArgs.Empty);
					}
					base.ErrorMessage = null;
				}
				this._progress = null;
				result = true;
			}
			catch
			{
				this._progress = null;
				if (this.CommandEnded != null)
				{
					this.CommandEnded(this, EventArgs.Empty);
				}
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = string.Concat(new string[]
					{
						AfaStrings.ErrorEncounteredIn,
						AfaStrings.errRefreshingFeatureService,
						" (",
						base.Name,
						")"
					});
				}
				else
				{
					base.ErrorMessage = base.ErrorMessage + " (" + base.Name + ")";
				}
				result = false;
			}
			return result;
		}

		private GraphicFeatureLayer[] GetServerLayers(ServerSymbolOutputOptions options)
		{
			GraphicFeatureLayer[] result;
			try
			{
				result = this._featureservice.GetLayers(options);
			}
			catch (SystemException)
			{
				if (base.Challenge())
				{
					try
					{
						this._featureservice.Proxy = WebRequest.DefaultWebProxy;
						this._featureservice.Credentials = base.Parent.Credentials;
						if (this._featureservice.Credentials == null)
						{
							this._featureservice.UseDefaultCredentials = true;
						}
						this._featureservice.Url = (this.ServiceURL = base.Parent.Soap_URL + "/" + base.FullName + "/MapServer/FeatureServer");
						base.Parent.AppendToken(this._featureservice.Url);
						result = this._featureservice.GetLayers(this._symbolOpts);
						return result;
					}
					catch
					{
						result = null;
						return result;
					}
				}
				result = null;
			}
			return result;
		}

		public bool AddService()
		{
			return this.AddService(null);
		}

		public bool AddService(List<int> layerList)
		{
			bool result;
			try
			{
                System.Windows.Forms.Application.UseWaitCursor = true;
				AGSExportOptions aGSExportOptions = (AGSExportOptions)Utility.CloneObject(this.ExportOptions);
				aGSExportOptions.AcadDocument = AfaDocData.ActiveDocData.Document;
				Size size = Autodesk.AutoCAD.ApplicationServices.Application.ToSystemDrawingSize(aGSExportOptions.AcadDocument.Window.DeviceIndependentSize);
				aGSExportOptions.Width = size.Width;
				aGSExportOptions.Height = size.Height;
				aGSExportOptions.DPI = 96;
				Extent extent = null;
				if (this.ExportOptions.BoundingBox != null && this.ExportOptions.BoundingBox.IsValid())
				{
					extent = this.ExportOptions.BoundingBox;
				}
				if (extent == null)
				{
					if (base.Properties.ContainsKey("Full Extent"))
					{
						extent = (base.Properties["Full Extent"] as Extent);
						if (extent.IsValid())
						{
							aGSExportOptions.BoundingBox = extent;
						}
						else
						{
							extent = null;
						}
					}
					if (extent == null && base.Properties.ContainsKey("Initial Extent"))
					{
						extent = (base.Properties["Initial Extent"] as Extent);
						if (extent.IsValid())
						{
							aGSExportOptions.BoundingBox = extent;
						}
						else
						{
							extent = null;
						}
					}
					if (base.Properties.ContainsKey("Extent"))
					{
						extent = (base.Properties["Extent"] as Extent);
						if (extent.IsValid())
						{
							aGSExportOptions.BoundingBox = extent;
						}
						else
						{
							extent = null;
						}
					}
				}
				if (extent == null)
				{
					base.ErrorMessage = "Unable to determine default service extent";
                    System.Windows.Forms.Application.UseWaitCursor = false;
					result = false;
				}
				else
				{
					if (string.IsNullOrEmpty(aGSExportOptions.OutputWKT))
					{
						aGSExportOptions.OutputWKT = "PROJCS[\"WGS_1984_Web_Mercator_Auxiliary_Sphere\",GEOGCS[\"GCS_WGS_1984\",DATUM[\"D_WGS_1984\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Mercator_Auxiliary_Sphere\"],PARAMETER[\"False_Easting\",0.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",0.0],PARAMETER[\"Standard_Parallel_1\",0.0],PARAMETER[\"Auxiliary_Sphere_Type\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3857]]";
					}
                    System.Windows.Forms.Application.UseWaitCursor = false;
					string text = "";
					if (layerList == null)
					{
						layerList = new List<int>();
						foreach (int current in this.MapLayers.Keys)
						{
							layerList.Add(current);
						}
					}
					result = this.AddService(aGSExportOptions, layerList, ref text);
				}
			}
			catch
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorAddingService;
				}
                System.Windows.Forms.Application.UseWaitCursor = false;
				result = false;
			}
			return result;
		}

		public bool ExtractService(List<int> layerList)
		{
			bool result;
			try
			{
                System.Windows.Forms.Application.UseWaitCursor = true;
				AGSExportOptions aGSExportOptions = (AGSExportOptions)Utility.CloneObject(this.ExportOptions);
				aGSExportOptions.AcadDocument = AfaDocData.ActiveDocData.Document;
				Size size = Autodesk.AutoCAD.ApplicationServices.Application.ToSystemDrawingSize(aGSExportOptions.AcadDocument.Window.DeviceIndependentSize);
				aGSExportOptions.Width = size.Width;
				aGSExportOptions.Height = size.Height;
				aGSExportOptions.DPI = 96;
				Extent extent = null;
				if (this.ExportOptions.BoundingBox != null && this.ExportOptions.BoundingBox.IsValid())
				{
					extent = this.ExportOptions.BoundingBox;
				}
				if (extent == null)
				{
					if (base.Properties.ContainsKey("Full Extent"))
					{
						extent = (base.Properties["Full Extent"] as Extent);
						if (extent.IsValid())
						{
							aGSExportOptions.BoundingBox = extent;
						}
						else
						{
							extent = null;
						}
					}
					if (extent == null && base.Properties.ContainsKey("Initial Extent"))
					{
						extent = (base.Properties["Initial Extent"] as Extent);
						if (extent.IsValid())
						{
							aGSExportOptions.BoundingBox = extent;
						}
						else
						{
							extent = null;
						}
					}
					if (base.Properties.ContainsKey("Extent"))
					{
						extent = (base.Properties["Extent"] as Extent);
						if (extent.IsValid())
						{
							aGSExportOptions.BoundingBox = extent;
						}
						else
						{
							extent = null;
						}
					}
				}
				if (extent == null)
				{
					base.ErrorMessage = "Unable to determine default service extent";
                    System.Windows.Forms.Application.UseWaitCursor = false;
					result = false;
				}
				else
				{
					if (string.IsNullOrEmpty(aGSExportOptions.OutputWKT))
					{
						aGSExportOptions.OutputWKT = "PROJCS[\"WGS_1984_Web_Mercator_Auxiliary_Sphere\",GEOGCS[\"GCS_WGS_1984\",DATUM[\"D_WGS_1984\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Mercator_Auxiliary_Sphere\"],PARAMETER[\"False_Easting\",0.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",0.0],PARAMETER[\"Standard_Parallel_1\",0.0],PARAMETER[\"Auxiliary_Sphere_Type\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3857]]";
					}
                    System.Windows.Forms.Application.UseWaitCursor = false;
					string text = "";
					if (layerList == null)
					{
						layerList = new List<int>();
						foreach (int current in this.MapLayers.Keys)
						{
							layerList.Add(current);
						}
					}
					result = this.ExportFeatures(aGSExportOptions, layerList, ref text);
				}
			}
			catch
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorAddingService;
				}
                System.Windows.Forms.Application.UseWaitCursor = false;
				result = false;
			}
			return result;
		}

		public bool AddService(AGSExportOptions eo, List<int> layerIDs, ref string errList)
		{
			bool result;
			try
			{
				base.ErrorMessage = null;
				bool flag = false;
				using (this._progress = new ProgressIndicator(this))
				{
					if (this.CommandStarted != null)
					{
						CommandStartEventArgs commandStartEventArgs = new CommandStartEventArgs();
						commandStartEventArgs.WindowTitle = AfaStrings.AddingFSLayers;
						commandStartEventArgs.ProgressMaxValue = 0;
						commandStartEventArgs.ProgressMinValue = 0;
						commandStartEventArgs.PrograssInitValue = 0;
						this.CommandStarted(this, commandStartEventArgs);
						this.ReportExportStatus(AfaStrings.Initializing + " " + base.Name, "");
					}
					this.InitializeFeatureService();
					GraphicFeatureLayer[] layers = this._featureservice.GetLayers(this._symbolOpts);
					GraphicFeatureLayer graphicFeatureLayer = null;
					this.ExportOptions.OutputWKT = this.GetWKTFromLayer(layerIDs[0]);
					int num = DocUtil.EntityCount();
					foreach (int current in layerIDs)
					{
						if (this.ReportCheckCancel())
						{
							break;
						}
						base.ErrorMessage = "";
						graphicFeatureLayer = this.GetActiveLayer(current, layers);
						if (graphicFeatureLayer != null)
						{
							if (graphicFeatureLayer.GeometryType == esriGeometryType.esriGeometryMultipoint)
							{
								this.ReportExportStatus(graphicFeatureLayer.Name + " " + AfaStrings.MultipointNotSupported);
							}
							else if (MSCDataset.ContainsFeatureService(base.Parent.Soap_URL, base.FullName, current))
							{
								this.ReportExportStatus(graphicFeatureLayer.Name + " " + AfaStrings.AlreadyExistsInDrawing);
							}
							else
							{
								try
								{
									MSCFeatureService mSCFeatureService = (MSCFeatureService)this.DefineMSCFeatureClass(graphicFeatureLayer, eo, true);
									MSCFeatureClass mSCFeatureClass = mSCFeatureService;
									if (mSCFeatureClass == null)
									{
										if (!string.IsNullOrEmpty(errList))
										{
											errList += "\n";
										}
										string text = errList;
										errList = string.Concat(new string[]
										{
											text,
											AfaStrings.ErrorEncounteredIn,
											AfaStrings.errDefiningLocalFeatureClass,
											" (",
											graphicFeatureLayer.Name,
											")"
										});
									}
									else
									{
										this.ExportFeatures(graphicFeatureLayer, eo, ref mSCFeatureClass);
										if (!eo.Dynamic)
										{
											mSCFeatureService.QueryOnly = true;
											mSCFeatureService.SetLayerLock(true);
										}
										else
										{
											flag = true;
										}
									}
								}
								catch
								{
									if (!string.IsNullOrEmpty(errList))
									{
										errList += "\n";
									}
									string text2 = errList;
									errList = string.Concat(new string[]
									{
										text2,
										base.ErrorMessage,
										" (",
										graphicFeatureLayer.Name,
										")"
									});
								}
							}
						}
					}
					if (num == 0)
					{
						DocUtil.ZoomExtents();
					}
					if (this.CommandEnded != null)
					{
						this.CommandEnded(this, EventArgs.Empty);
					}
					if (flag)
					{
						CmdLine.ExecuteQuietCommand("ESRI_ShowPalette");
					}
				}
				this._progress = null;
				result = true;
			}
			catch
			{
				this._progress = null;
				if (this.CommandEnded != null)
				{
					this.CommandEnded(this, EventArgs.Empty);
				}
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errAddingFeatureService;
				}
				result = false;
			}
			return result;
		}

		public void BeginUpdate()
		{
			using (this._progress = new ProgressIndicator(this))
			{
				if (this.CommandStarted != null)
				{
					CommandStartEventArgs commandStartEventArgs = new CommandStartEventArgs();
					commandStartEventArgs.WindowTitle = AfaStrings.AddingFSLayers;
					commandStartEventArgs.ProgressMaxValue = 0;
					commandStartEventArgs.ProgressMinValue = 0;
					commandStartEventArgs.PrograssInitValue = 0;
					this.CommandStarted(this, commandStartEventArgs);
					this.ReportExportStatus(AfaStrings.Initializing + " " + base.Name, "");
				}
			}
		}

		public void EndUpdate()
		{
			if (this.CommandEnded != null)
			{
				this.CommandEnded(this, EventArgs.Empty);
			}
		}

		public void InitializePrototypes(int layerId, MSCFeatureService svc, ref string hostTypeField)
		{
			base.ErrorMessage = "";
			this.InitializeFeatureService();
			GraphicFeatureLayer[] layers = this._featureservice.GetLayers(this._symbolOpts);
			GraphicFeatureLayer activeLayer = this.GetActiveLayer(layerId, layers);
			hostTypeField = activeLayer.TypeIDPropName;
			if (activeLayer.Types != null)
			{
				DataObjectType[] types = activeLayer.Types;
				for (int i = 0; i < types.Length; i++)
				{
					DataObjectType dataObjectType = types[i];
					try
					{
						if (dataObjectType.Templates[0].Prototype != null)
						{
							MSCFeatureClassSubType subType = svc.GetSubType(dataObjectType.TypeID);
							if (subType != null)
							{
								subType.Prototype = dataObjectType.Templates[0].Prototype;
								GraphicFeature graphicFeature = (GraphicFeature)subType.Prototype;
								if (graphicFeature != null)
								{
									Geometry geometry = graphicFeature.Geometry;
									if (geometry != null)
									{
										this.HasZ = this.GeometrySupportsZ(geometry);
										svc.HasZ = this.HasZ;
										this.HasM = this.GeometrySupportsM(geometry);
										svc.HasM = this.HasM;
										svc.Write();
									}
								}
							}
						}
					}
					catch
					{
					}
				}
			}
			try
			{
				svc.Prototype = activeLayer.Templates[0].Prototype;
				GraphicFeature graphicFeature2 = (GraphicFeature)svc.Prototype;
				if (graphicFeature2 != null)
				{
					Geometry geometry2 = graphicFeature2.Geometry;
					if (geometry2 != null)
					{
						this.HasZ = this.GeometrySupportsZ(geometry2);
						svc.HasZ = this.HasZ;
						this.HasM = this.GeometrySupportsM(geometry2);
						svc.HasM = this.HasM;
						svc.Write();
					}
				}
			}
			catch
			{
				try
				{
					if (svc.SubTypes.Count > 0)
					{
						svc.Prototype = svc.SubTypes[0].Prototype;
					}
				}
				catch
				{
				}
			}
		}

		public ArcGIS10Types.DataObject GetTemplatePrototype(int layerId, string name, ref string hostTypeField)
		{
			base.ErrorMessage = "";
			this.InitializeFeatureService();
			GraphicFeatureLayer[] layers = this._featureservice.GetLayers(this._symbolOpts);
			GraphicFeatureLayer activeLayer = this.GetActiveLayer(layerId, layers);
			hostTypeField = activeLayer.TypeIDPropName;
			ArcGIS10Types.DataObject result;
			try
			{
				if (string.IsNullOrEmpty(name))
				{
					result = activeLayer.Templates[0].Prototype;
				}
				else
				{
					TemplateInfo[] templates = activeLayer.Templates;
					for (int i = 0; i < templates.Length; i++)
					{
						TemplateInfo templateInfo = templates[i];
						if (templateInfo.Name == name)
						{
							result = templateInfo.Prototype;
							return result;
						}
					}
					result = activeLayer.Templates[0].Prototype;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public List<EditResult> AddNewFeatures(Document doc, int layerId, ArcGIS10Types.DataObject[] objArray, int chunkSize)
		{
			base.ErrorMessage = "";
			List<EditResult> list = new List<EditResult>();
			List<ArcGIS10Types.DataObject> list2 = new List<ArcGIS10Types.DataObject>(objArray);
			List<EditResult> result;
			try
			{
				if (list2 == null)
				{
					string text = AfaStrings.NoFeaturesAdded;
					this.ReportExportStatus(text + ": " + base.Name);
					result = list;
				}
				else if (list2.Count == 0)
				{
					string text = AfaStrings.NoFeaturesAdded;
					this.ReportExportStatus(text + ": " + base.Name);
					result = list;
				}
				else
				{
					this.InitializeFeatureService();
					string str = MSCPrj.ReadWKT(doc);
					SpatialReference spatialReference = AGSSpatialReference.SpRefFromString(str);
					DataObjects dataObjects = new DataObjects();
					dataObjects.SpatialReference = spatialReference;
					int num = 0;
					int num2 = 0;
					if (chunkSize != 0 && chunkSize >= list2.Count)
					{
						dataObjects.DataObjectArray = list2.ToArray();
						this.ReportExportStatus(AfaStrings.AddingNewFeaturesOn + base.Name);
						EditResult[] array = this._featureservice.Add(layerId, dataObjects);
						EditResult[] array2 = array;
						for (int i = 0; i < array2.Length; i++)
						{
							EditResult editResult = array2[i];
							list.Add(editResult);
							if (editResult.Succeeded)
							{
								num++;
							}
							else
							{
								num2++;
							}
						}
					}
					else
					{
						int num3;
						for (int j = 0; j < list2.Count; j += num3)
						{
							num3 = list2.Count - j;
							if (num3 > chunkSize)
							{
								num3 = chunkSize;
							}
							ArcGIS10Types.DataObject[] array3 = new ArcGIS10Types.DataObject[num3];
							list2.CopyTo(j, array3, 0, num3);
							dataObjects.DataObjectArray = array3;
							EditResult[] array4 = this._featureservice.Add(layerId, dataObjects);
							EditResult[] array5 = array4;
							for (int k = 0; k < array5.Length; k++)
							{
								EditResult editResult2 = array5[k];
								list.Add(editResult2);
								if (editResult2.Succeeded)
								{
									num++;
								}
								else
								{
									num2++;
								}
							}
						}
					}
					string text = num + AfaStrings.FeaturesAdded;
					if (num2 > 0)
					{
						object obj = text;
						text = string.Concat(new object[]
						{
							obj,
							", ",
							num2,
							AfaStrings.FeaturesFailed
						});
					}
					this.ReportExportStatus(base.Name + " : " + text);
					result = list;
				}
			}
			catch (SystemException ex)
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errAddingFeatures;
				}
				string text = AfaStrings.FailureAddingFeatures;
				ErrorReport.ShowErrorMessage(string.Concat(new string[]
				{
					base.Name,
					" : ",
					base.ErrorMessage,
					": ",
					ex.Message
				}));
				ErrorReport.ShowErrorMessage(base.ErrorMessage);
				result = list;
			}
			catch
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errAddingFeatures;
				}
				string text = AfaStrings.FailureAddingFeatures;
				this.ReportExportStatus(base.Name + " : " + text);
				result = list;
			}
			return result;
		}

		public List<EditResult> UpdateFeatures(Document doc, int layerId, List<ArcGIS10Types.DataObject> modifiedObjects, int chunkSize)
		{
			base.ErrorMessage = "";
			List<EditResult> list = new List<EditResult>();
			List<EditResult> result;
			try
			{
				if (modifiedObjects == null)
				{
					string text = AfaStrings.NoFeaturesUpdated;
					result = list;
				}
				else if (modifiedObjects.Count == 0)
				{
					string text = AfaStrings.NoFeaturesUpdated;
					result = list;
				}
				else
				{
					this.InitializeFeatureService();
					string str = MSCPrj.ReadWKT(doc);
					SpatialReference spatialReference = AGSSpatialReference.SpRefFromString(str);
					DataObjects dataObjects = new DataObjects();
					dataObjects.SpatialReference = spatialReference;
					int num = 0;
					int num2 = 0;
					if (chunkSize >= modifiedObjects.Count)
					{
						dataObjects.DataObjectArray = modifiedObjects.ToArray();
						this.ReportExportStatus(AfaStrings.UpdatingModifiedFeaturesOn + base.Name);
						EditResult[] array = this._featureservice.Update(layerId, dataObjects);
						EditResult[] array2 = array;
						for (int i = 0; i < array2.Length; i++)
						{
							EditResult editResult = array2[i];
							list.Add(editResult);
							if (editResult.Succeeded)
							{
								num++;
							}
							else
							{
								num2++;
							}
						}
					}
					else
					{
						int num3;
						for (int j = 0; j < modifiedObjects.Count; j += num3)
						{
							num3 = modifiedObjects.Count - j;
							if (num3 > chunkSize)
							{
								num3 = chunkSize;
							}
							ArcGIS10Types.DataObject[] array3 = new ArcGIS10Types.DataObject[num3];
							modifiedObjects.CopyTo(j, array3, 0, num3);
							dataObjects.DataObjectArray = array3;
							EditResult[] array4 = this._featureservice.Update(layerId, dataObjects);
							EditResult[] array5 = array4;
							for (int k = 0; k < array5.Length; k++)
							{
								EditResult editResult2 = array5[k];
								list.Add(editResult2);
								if (editResult2.Succeeded)
								{
									num++;
								}
								else
								{
									num2++;
								}
							}
						}
					}
					string text = num + AfaStrings.FeaturesUpdated;
					if (num2 > 0)
					{
						object obj = text;
						text = string.Concat(new object[]
						{
							obj,
							", ",
							num2,
							AfaStrings.FeaturesFailed
						});
					}
					this.ReportExportStatus(base.Name + " : " + text);
					result = list;
				}
			}
			catch (SystemException ex)
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errUpdatingFeatures;
				}
				string text = AfaStrings.FailureAddingFeatures;
				this.ReportExportStatus(base.Name + " : " + text);
				ErrorReport.ShowErrorMessage(string.Concat(new string[]
				{
					base.Name,
					" : ",
					base.ErrorMessage,
					": ",
					ex.Message
				}));
				result = list;
			}
			catch
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errUpdatingFeatures;
				}
				string text = AfaStrings.FailureUpdatingFeatures;
				this.ReportExportStatus(base.Name + " : " + text);
				result = list;
			}
			return result;
		}

		public List<ArcGIS10Types.DataObject> QueryByID(Document doc, int layerID, AGSExportOptions eo, int[] fidList, ref string OIDField, ref string TypeField, ref int chunkSize)
		{
			base.ErrorMessage = "";
			List<ArcGIS10Types.DataObject> list = new List<ArcGIS10Types.DataObject>();
			List<ArcGIS10Types.DataObject> result;
			try
			{
				string str = MSCPrj.ReadWKT(doc);
				SpatialReference outputSpatialReference = AGSSpatialReference.SpRefFromString(str);
				this.InitializeFeatureService();
				GraphicFeatureLayer[] layers = this._featureservice.GetLayers(this._symbolOpts);
				GraphicFeatureLayer activeLayer = this.GetActiveLayer(layerID, layers);
				string geometryFieldName = activeLayer.GeometryFieldName;
				OIDField = activeLayer.OIDPropName;
				TypeField = activeLayer.TypeIDPropName;
				string text = this.BuildExpressionFromFIDS(OIDField, fidList);
				if (string.IsNullOrEmpty(eo.BoundingBox.SpatialReference))
				{
					eo.BoundingBox.SpatialReference = activeLayer.SpatialReference.WKT;
				}
				EnvelopeN env = AGSService.InitializeEnvelope((EnvelopeN)activeLayer.SpatialExtent, eo.BoundingBox);
				SpatialFilter spatialFilter = AGSService.BuildSpatialFilter(env, geometryFieldName);
				string text2 = eo.WhereClause;
				if (string.IsNullOrEmpty(text2))
				{
					text2 = activeLayer.OIDPropName + " > -1";
				}
				spatialFilter.WhereClause = text2;
				spatialFilter.OutputSpatialReference = outputSpatialReference;
				QueryResultOptions queryResultOptions = new QueryResultOptions();
				queryResultOptions.Format = esriQueryResultFormat.esriQueryResultRecordSetAsObject;
				ServiceDataOptions serviceDataOptions = new ServiceDataOptions();
				serviceDataOptions.Format = "native";
				List<int> list2 = new List<int>();
				try
				{
					int[] array = this.BuildFIDSet(layerID, text, spatialFilter);
					if (this.ReportCheckCancel())
					{
						result = null;
					}
					else
					{
						ServiceData serviceData = this._featureservice.Query(layerID, text, spatialFilter, serviceDataOptions);
						DataObjects dataObjects = serviceData.Object as DataObjects;
						ArcGIS10Types.DataObject[] dataObjectArray = dataObjects.DataObjectArray;
						for (int i = 0; i < dataObjectArray.Length; i++)
						{
							ArcGIS10Types.DataObject dataObject = dataObjectArray[i];
							list.Add(dataObject);
							list2.Add(AGSFeatureService.GetObjectId(OIDField, dataObject.Properties.PropertyArray));
						}
						chunkSize = list.Count;
						while (list.Count < array.Length)
						{
							List<int> list3 = new List<int>();
							int[] array2 = array;
							for (int j = 0; j < array2.Length; j++)
							{
								int item = array2[j];
								if (!list2.Contains(item))
								{
									list3.Add(item);
								}
							}
							text = this.BuildExpressionFromFIDS(activeLayer.OIDPropName, list3.ToArray());
							serviceData = this._featureservice.Query(layerID, text, spatialFilter, serviceDataOptions);
							dataObjects = (serviceData.Object as DataObjects);
							ArcGIS10Types.DataObject[] dataObjectArray2 = dataObjects.DataObjectArray;
							for (int k = 0; k < dataObjectArray2.Length; k++)
							{
								ArcGIS10Types.DataObject dataObject2 = dataObjectArray2[k];
								list.Add(dataObject2);
								list2.Add(AGSFeatureService.GetObjectId(activeLayer.OIDPropName, dataObject2.Properties.PropertyArray));
							}
						}
						result = list;
					}
				}
				catch
				{
					if (string.IsNullOrEmpty(base.ErrorMessage))
					{
						base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errQueryingFeatureIDs;
					}
					result = null;
				}
			}
			catch
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errQueryingFeatureIDs;
				}
				result = null;
			}
			return result;
		}

		public List<EditResult> DeleteFeatures(Document doc, int layerId, List<int> objectIds)
		{
			base.ErrorMessage = "";
			List<EditResult> list = new List<EditResult>();
			List<EditResult> result;
			try
			{
				if (objectIds == null)
				{
					string text = AfaStrings.NoFeaturesDeleted;
					result = list;
				}
				else if (objectIds.Count == 0)
				{
					string text = AfaStrings.NoFeaturesDeleted;
					result = list;
				}
				else
				{
					this.InitializeFeatureService();
					if (!string.IsNullOrEmpty(base.ErrorMessage))
					{
						string text = base.ErrorMessage;
						result = list;
					}
					else
					{
						int num = 0;
						int num2 = 0;
						GraphicFeatureLayer[] layers = this._featureservice.GetLayers(this._symbolOpts);
						GraphicFeatureLayer activeLayer = this.GetActiveLayer(layerId, layers);
						if (activeLayer != null)
						{
							this.ReportExportStatus(AfaStrings.DeletingFeaturesFrom + base.Name);
							EditResult[] array = this._featureservice.DeleteByID(layerId, objectIds.ToArray());
							EditResult[] array2 = array;
							for (int i = 0; i < array2.Length; i++)
							{
								EditResult editResult = array2[i];
								list.Add(editResult);
								if (editResult.Succeeded)
								{
									num++;
								}
								else
								{
									num2++;
								}
							}
							string text = num + AfaStrings.FeaturesDeleted;
							if (num2 > 0)
							{
								object obj = text;
								text = string.Concat(new object[]
								{
									obj,
									", ",
									num2,
									AfaStrings.FeaturesFailed
								});
							}
							this.ReportExportStatus(base.Name + " : " + text);
							result = list;
						}
						else
						{
							string text = AfaStrings.FailureUpdatingFeatures;
							this.ReportExportStatus(base.Name + " : " + text);
							result = list;
						}
					}
				}
			}
			catch (SystemException ex)
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.FailureUpdatingFeatures + " " + ex.Message;
				}
				string text = AfaStrings.FailureUpdatingFeatures + " " + ex.Message;
				this.ReportExportStatus(base.Name + " : " + text);
				result = list;
			}
			return result;
		}

		private MSCFeatureService UpdateMSCFeatureService(GraphicFeatureLayer activeLayer, ref MSCFeatureService msc)
		{
			MSCFeatureService result;
			try
			{
				bool arg_05_0 = AGSFeatureService.isUpdatingFeatureService;
				AGSFeatureService.isUpdatingFeatureService = true;
				string msg = "";
				AGSFeatureServiceLayer aGSFeatureServiceLayer = this.ValidateMapLayerForExport(activeLayer.ID, ref msg);
				if (aGSFeatureServiceLayer == null)
				{
					AGSFeatureService.isUpdatingFeatureService = false;
					this.ReportExportError(msg, false);
					base.IsValid = false;
					result = msc;
				}
				else
				{
					base.IsValid = true;
					string arg_4F_0 = activeLayer.GeometryFieldName;
					TemplateInfo template = null;
					if (activeLayer.Templates.Count<TemplateInfo>() > 0)
					{
						template = activeLayer.Templates[0];
					}
					List<CadField> list = CadField.ToCadFields(activeLayer.PropertyInfos, template, null);
					string typeIDPropName = activeLayer.TypeIDPropName;
					PropertyInfo typeIDField = this.GetTypeIDField(activeLayer.PropertyInfos, typeIDPropName);
					FieldDomain fieldDomain = null;
					if (typeIDField != null)
					{
						fieldDomain = this.BuildSubtypeDomain(activeLayer.Types, typeIDField);
						list = CadField.SetTypeField(list, typeIDPropName, null, fieldDomain);
					}
					msc.Fields = list;
					int num = 0;
					SubTypeDictionary subTypeDictionary = this.BuildSubTypes(activeLayer, ref num);
					if (num > 0)
					{
						List<TemplateInfo> list2 = new List<TemplateInfo>();
						List<PropertySet> list3 = new List<PropertySet>();
						if (activeLayer.Types != null)
						{
							DataObjectType[] types = activeLayer.Types;
							for (int i = 0; i < types.Length; i++)
							{
								DataObjectType dataObjectType = types[i];
								if (dataObjectType.PropDomains != null)
								{
									list3.Add(dataObjectType.PropDomains);
								}
								else
								{
									list3.Add(null);
								}
								if (dataObjectType.Templates != null)
								{
									list2.Add(dataObjectType.Templates[0]);
								}
								else
								{
									list2.Add(null);
								}
							}
						}
						List<MSCFeatureClassSubType> list4 = new List<MSCFeatureClassSubType>();
						foreach (MSCFeatureClassSubType current in msc.SubTypes)
						{
							if (this.FindAGSSubType(current.TypeValue, subTypeDictionary) == null)
							{
								list4.Add(current);
								DocUtil.RenameCADLayer(msc.ParentDataset.ParentDocument, current.CadLayerName, "xx_" + current.CadLayerName);
							}
						}
						foreach (MSCFeatureClassSubType current2 in list4)
						{
							msc.SubTypes.Remove(current2);
						}
						List<string> list5 = Utility.InitializeStringList(msc.LayerName);
						int num2 = 0;
						foreach (AGSSubType current3 in subTypeDictionary.Values)
						{
							TemplateInfo template2 = null;
							PropertySet domains = null;
							if (list2 != null)
							{
								template2 = list2[num2];
							}
							if (list3 != null)
							{
								domains = list3[num2];
							}
							num2++;
							List<CadField> list6 = CadField.ToCadFields(activeLayer.PropertyInfos, template2, domains);
							list6 = CadField.SetTypeField(list6, typeIDPropName, current3.ID, fieldDomain);
							MSCFeatureClassSubType mSCFeatureClassSubType = msc.GetSubType(current3.ID);
							if (mSCFeatureClassSubType == null)
							{
								string text = current3.Name + "_" + msc.LayerName;
								mSCFeatureClassSubType = (MSCFeatureClassSubType)MSCDataset.AddSimpleSubtype(msc, text, aGSFeatureServiceLayer.GeometryType, text, list6, typeIDPropName, current3.ID, null);
							}
							else
							{
								mSCFeatureClassSubType.Fields = list6;
								mSCFeatureClassSubType.TypeField = typeIDPropName;
								mSCFeatureClassSubType.TypeValue = current3.ID;
							}
							if (mSCFeatureClassSubType != null)
							{
								list5.Add(mSCFeatureClassSubType.CadLayerName);
							}
						}
						msc.SetQueryFromLayers(list5);
						msc.Write();
					}
					AGSFeatureService.isUpdatingFeatureService = false;
					base.IsValid = true;
					result = msc;
				}
			}
			catch
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errUpdatingMSCFeatureService;
				}
				base.IsValid = false;
				result = msc;
			}
			return result;
		}

		public bool ExportFeatures(AGSExportOptions eo, List<int> layerIDs, ref string errList)
		{
			bool result;
			try
			{
				base.ErrorMessage = null;
				errList = "";
				using (this._progress = new ProgressIndicator(this))
				{
					if (this.CommandStarted != null)
					{
						CommandStartEventArgs commandStartEventArgs = new CommandStartEventArgs();
						commandStartEventArgs.WindowTitle = AfaStrings.ExtractingFeaturesFrom + base.Name;
						commandStartEventArgs.ProgressMaxValue = 0;
						commandStartEventArgs.ProgressMinValue = 0;
						commandStartEventArgs.PrograssInitValue = 0;
						this.CommandStarted(this, commandStartEventArgs);
						this.ReportExportStatus(AfaStrings.Initializing + " " + base.Name, "");
					}
					this.InitializeFeatureService();
					GraphicFeatureLayer[] array = null;
					array = this.GetServerLayers(this._symbolOpts);
					if (array == null)
					{
						this.CommandEnded(this, EventArgs.Empty);
						result = false;
						return result;
					}
					GraphicFeatureLayer graphicFeatureLayer = null;
					int num = DocUtil.EntityCount();
					foreach (int current in layerIDs)
					{
						base.ErrorMessage = "";
						if (!this.ReportCheckCancel() && string.IsNullOrEmpty(base.ErrorMessage))
						{
							graphicFeatureLayer = this.GetActiveLayer(current, array);
							if (graphicFeatureLayer != null)
							{
								try
								{
									MSCFeatureClass mSCFeatureClass = this.DefineMSCFeatureClass(graphicFeatureLayer, eo, false);
									eo.OutputWKT = MSCPrj.ReadWKT(AfaDocData.ActiveDocData.Document);
									if (mSCFeatureClass != null)
									{
										this.ExportFeatures(graphicFeatureLayer, eo, ref mSCFeatureClass);
									}
									else
									{
										if (!string.IsNullOrEmpty(errList))
										{
											errList += "\n";
										}
										string text = errList;
										errList = string.Concat(new string[]
										{
											text,
											AfaStrings.ErrorEncounteredIn,
											AfaStrings.errDefiningLocalFeatureClass,
											" (",
											graphicFeatureLayer.Name,
											")"
										});
									}
								}
								catch
								{
									if (!string.IsNullOrEmpty(errList))
									{
										errList += "\n";
									}
									string text2 = errList;
									errList = string.Concat(new string[]
									{
										text2,
										base.ErrorMessage,
										" (",
										graphicFeatureLayer.Name,
										")"
									});
								}
							}
						}
					}
					if (num == 0)
					{
						DocUtil.ZoomExtents();
					}
					if (this.CommandEnded != null)
					{
						this.CommandEnded(this, EventArgs.Empty);
					}
				}
				this._progress = null;
				result = true;
			}
			catch (SystemException ex)
			{
				this._progress = null;
				if (this.CommandEnded != null)
				{
					this.CommandEnded(this, EventArgs.Empty);
				}
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errExportingFeatures + ": " + ex.Message;
				}
				result = false;
			}
			return result;
		}

		private void ExportFeatures(GraphicFeatureLayer activeLayer, AGSExportOptions eo, ref MSCFeatureClass mscFC)
		{
			try
			{
				string msg = "";
				AGSFeatureServiceLayer aGSFeatureServiceLayer = this.ValidateMapLayerForExport(activeLayer.ID, ref msg);
				if (aGSFeatureServiceLayer == null)
				{
					this.ReportExportError(msg, false);
				}
				else
				{
					MSCFeatureService mSCFeatureService = null;
					if (mscFC is MSCFeatureService)
					{
						mSCFeatureService = (MSCFeatureService)mscFC;
					}
					if (aGSFeatureServiceLayer == null)
					{
						this.ReportExportError(msg, false);
					}
					else
					{
						this.ReportExportStatus(AfaStrings.Processing + aGSFeatureServiceLayer.Name, "");
						if (string.IsNullOrEmpty(eo.OutputWKT))
						{
							eo.OutputWKT = activeLayer.SpatialReference.WKT;
						}
						SpatialReference spatialReference = AGSSpatialReference.SpRefFromString(eo.OutputWKT);
						TemplateInfo template = null;
						if (activeLayer.Templates != null && activeLayer.Templates.Count<TemplateInfo>() > 0)
						{
							template = activeLayer.Templates[0];
						}
						string arg_B6_0 = activeLayer.GeometryFieldName;
						CadField.ToCadFields(activeLayer.PropertyInfos, template, null);
						if (string.IsNullOrEmpty(eo.BoundingBox.SpatialReference))
						{
							eo.BoundingBox.SpatialReference = activeLayer.SpatialReference.WKT;
						}
						EnvelopeN env = AGSService.InitializeEnvelope((EnvelopeN)activeLayer.SpatialExtent, eo.BoundingBox);
						SpatialFilter spatialFilter = AGSService.BuildSpatialFilter(env, aGSFeatureServiceLayer.GeomField);
						string text = eo.WhereClause;
						if (string.IsNullOrEmpty(text))
						{
							text = activeLayer.OIDPropName + " > -1";
						}
						spatialFilter.WhereClause = text;
						spatialFilter.OutputSpatialReference = spatialReference;
						int num = 0;
						SubTypeDictionary subTypeDictionary = this.BuildSubTypes(activeLayer, ref num);
						if (num > 0)
						{
							string typeIDPropName = activeLayer.TypeIDPropName;
							string text2 = "";
							foreach (AGSSubType current in subTypeDictionary.Values)
							{
								if (this.ReportCheckCancel())
								{
									return;
								}
								MSCFeatureClassSubType subType = mscFC.GetSubType(current.ID);
								if (subType != null)
								{
									string cadLayerName = subType.CadLayerName;
									this.ReportExportStatus(AfaStrings.Processing + aGSFeatureServiceLayer.Name + "-" + current.Name, "");
									string defExpression;
									string clause;
									if (current.ID.GetType() == typeof(string))
									{
										defExpression = string.Concat(new object[]
										{
											typeIDPropName,
											"='",
											current.ID,
											"'"
										});
										clause = string.Concat(new object[]
										{
											typeIDPropName,
											"<>'",
											current.ID,
											"'"
										});
									}
									else
									{
										defExpression = typeIDPropName + "=" + current.ID;
										clause = typeIDPropName + "<>" + current.ID;
									}
									text2 = this.CombineWhereClauses(text2, clause);
									if (mSCFeatureService != null && mSCFeatureService.GeometryType == MSCFeatureClass.fcTypeCode.fcTypePoint)
									{
										DocUtil.FixPDMode();
									}
									this.ExtractAndDraw(aGSFeatureServiceLayer, current, subType, activeLayer.ID, spatialReference, env, defExpression, spatialFilter, cadLayerName);
									if (mSCFeatureService != null && this.HasZ)
									{
										mSCFeatureService.HasZ = true;
									}
									if (mSCFeatureService != null && this.HasM)
									{
										mSCFeatureService.HasM = true;
									}
								}
							}
							string text3 = mscFC.GetSingleLayerQueryLayerName();
							if (string.IsNullOrEmpty(text3))
							{
								text3 = aGSFeatureServiceLayer.Name + "_" + base.Name;
							}
							if (mSCFeatureService != null)
							{
								text3 = mSCFeatureService.LayerName;
							}
							if (mSCFeatureService != null && mSCFeatureService.GeometryType == MSCFeatureClass.fcTypeCode.fcTypePoint)
							{
								DocUtil.FixPDMode();
							}
							this.ExtractAndDraw(aGSFeatureServiceLayer, null, mscFC, activeLayer.ID, spatialReference, env, text2, spatialFilter, text3);
							if (mSCFeatureService != null && this.HasZ)
							{
								mSCFeatureService.HasZ = true;
							}
							if (mSCFeatureService != null && this.HasM)
							{
								mSCFeatureService.HasM = true;
							}
						}
						else
						{
							string text4 = mscFC.GetSingleLayerQueryLayerName();
							if (string.IsNullOrEmpty(text4))
							{
								text4 = aGSFeatureServiceLayer.Name + "_" + base.Name;
							}
							if (mscFC is MSCFeatureService)
							{
								text4 = ((MSCFeatureService)mscFC).LayerName;
							}
							this.ExtractAndDraw(aGSFeatureServiceLayer, null, mscFC, activeLayer.ID, spatialReference, env, "", spatialFilter, text4);
							if (mSCFeatureService != null && this.HasZ)
							{
								mSCFeatureService.HasZ = true;
							}
							if (mSCFeatureService != null && this.HasM)
							{
								mSCFeatureService.HasM = true;
							}
						}
					}
				}
			}
			catch
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errExportingFeatures;
				}
				throw;
			}
		}

		private AGSSubType FindAGSSubType(object typeID, SubTypeDictionary types)
		{
			foreach (AGSSubType current in types.Values)
			{
				if (current.ID.ToString() == typeID.ToString())
				{
					return current;
				}
			}
			return null;
		}

		private AGSColor GetAGSColor(ArcGIS10Types.Color agsColor)
		{
			if (agsColor is RgbColor)
			{
				RgbColor rgbColor = (RgbColor)agsColor;
				return new AGSColor(rgbColor.Red, rgbColor.Green, rgbColor.Blue, rgbColor.AlphaValue);
			}
			return null;
		}

		private AGSColor GetSymbolColor(Symbol symbol)
		{
			if (symbol is LineSymbol)
			{
				LineSymbol lineSymbol = (LineSymbol)symbol;
				return this.GetAGSColor(lineSymbol.Color);
			}
			if (symbol is MarkerSymbol)
			{
				MarkerSymbol markerSymbol = (MarkerSymbol)symbol;
				return this.GetAGSColor(markerSymbol.Color);
			}
			if (symbol is FillSymbol)
			{
				FillSymbol fillSymbol = (FillSymbol)symbol;
				return this.GetAGSColor(fillSymbol.Color);
			}
			return null;
		}

		private AGSColor GetColor(SimpleRenderer simpleRenderer)
		{
			if (simpleRenderer != null)
			{
				return this.GetSymbolColor(simpleRenderer.Symbol);
			}
			return null;
		}

		private AGSColor GetDefaultColor(UniqueValueRenderer renderer)
		{
			if (renderer != null)
			{
				return this.GetSymbolColor(renderer.DefaultSymbol);
			}
			return null;
		}

		private AGSColor GetSuggestedLayerColor(GraphicFeatureLayer activeLayer)
		{
			FeatureRenderer featureRenderer = activeLayer.LayerDrawingDescription.FeatureRenderer;
			if (featureRenderer is SimpleRenderer)
			{
				return this.GetColor(featureRenderer as SimpleRenderer);
			}
			if (featureRenderer is UniqueValueRenderer)
			{
				return this.GetDefaultColor(featureRenderer as UniqueValueRenderer);
			}
			return null;
		}

		private MSCFeatureClass DefineMSCFeatureClass(GraphicFeatureLayer activeLayer, AGSExportOptions eo, bool isService)
		{
			MSCFeatureClass result;
			try
			{
				string msg = "";
				AGSFeatureServiceLayer aGSFeatureServiceLayer = this.ValidateMapLayerForExport(activeLayer.ID, ref msg);
				if (aGSFeatureServiceLayer == null)
				{
					this.ReportExportError(msg, false);
					result = null;
				}
				else
				{
					TemplateInfo template = null;
					if (activeLayer.Templates != null && activeLayer.Templates.Count<TemplateInfo>() > 0)
					{
						template = activeLayer.Templates[0];
					}
					string arg_4F_0 = activeLayer.GeometryFieldName;
					List<CadField> list = CadField.ToCadFields(activeLayer.PropertyInfos, template, null);
					int num = 0;
					string text = "";
					SubTypeDictionary subTypeDictionary = this.BuildSubTypes(activeLayer, ref num);
					if (num > 0)
					{
						List<TemplateInfo> list2 = new List<TemplateInfo>();
						List<PropertySet> list3 = new List<PropertySet>();
						if (activeLayer.Types != null)
						{
							DataObjectType[] types = activeLayer.Types;
							for (int i = 0; i < types.Length; i++)
							{
								DataObjectType dataObjectType = types[i];
								if (dataObjectType.PropDomains != null)
								{
									list3.Add(dataObjectType.PropDomains);
								}
								else
								{
									list3.Add(null);
								}
								if (dataObjectType.Templates != null)
								{
									list2.Add(dataObjectType.Templates[0]);
								}
								else
								{
									list2.Add(null);
								}
							}
						}
						string text2 = aGSFeatureServiceLayer.Name + "_" + base.Name;
						List<string> list4 = Utility.InitializeStringList("ESRI_" + text2);
						foreach (AGSSubType current in subTypeDictionary.Values)
						{
							string text3 = current.Name + "_" + text2;
							if (isService)
							{
								text3 = "ESRI_" + text3;
							}
							list4.Add(text3);
						}
						text = activeLayer.TypeIDPropName;
						PropertyInfo typeIDField = this.GetTypeIDField(activeLayer.PropertyInfos, text);
						FieldDomain fieldDomain = null;
						object typeValue = null;
						if (typeIDField != null)
						{
							fieldDomain = this.BuildSubtypeDomain(activeLayer.Types, typeIDField);
							typeValue = fieldDomain.FauxNull.Value;
							list = CadField.SetTypeField(list, text, typeValue, fieldDomain);
						}
						MSCFeatureClass mSCFeatureClass = null;
						if (!isService)
						{
							mSCFeatureClass = MSCDataset.AddSimpleFeatureClass(text2, aGSFeatureServiceLayer.GeometryType, list4, CadField.SetTypeField(list, text, typeValue, fieldDomain), true, this.GetSuggestedLayerColor(activeLayer));
						}
						else
						{
							mSCFeatureClass = MSCDataset.AddFeatureService(ref text2, this, aGSFeatureServiceLayer.Id, aGSFeatureServiceLayer.GeometryType, ref list4, CadField.SetTypeField(list, text, typeValue, fieldDomain), eo);
						}
						int num2 = 1;
						int num3 = 0;
						foreach (AGSSubType current2 in subTypeDictionary.Values)
						{
							TemplateInfo template2 = null;
							PropertySet domains = null;
							if (list2 != null)
							{
								template2 = list2[num3];
							}
							if (list3 != null)
							{
								domains = list3[num3];
							}
							num3++;
							List<CadField> list5 = CadField.ToCadFields(activeLayer.PropertyInfos, template2, domains);
							list5 = CadField.SetTypeField(list5, text, current2.ID, fieldDomain);
							string name = current2.Name;
							string suggestedLayerName = list4[num2++];
							MSCDataset.AddSimpleSubtype(mSCFeatureClass, name, aGSFeatureServiceLayer.GeometryType, suggestedLayerName, list5, text, current2.ID, current2.SuggestedColor);
						}
						ArcGISRibbon.SetTopFeatureClass(mSCFeatureClass);
						ArcGISRibbon.SetSubTypeComboToDefault(mSCFeatureClass);
						result = mSCFeatureClass;
					}
					else
					{
						string text4 = aGSFeatureServiceLayer.Name + "_" + base.Name;
						if (!isService)
						{
							result = MSCDataset.AddSimpleFeatureClass(text4, aGSFeatureServiceLayer.GeometryType, Utility.InitializeStringList(text4), list, true, this.GetSuggestedLayerColor(activeLayer));
						}
						else
						{
							List<string> list6 = Utility.InitializeStringList("ESRI_" + text4);
							result = MSCDataset.AddFeatureService(ref text4, this, aGSFeatureServiceLayer.Id, aGSFeatureServiceLayer.GeometryType, ref list6, CadField.SetTypeField(list, text, null, null), eo);
						}
					}
				}
			}
			catch
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errDefiningLocalFeatureClass;
				}
				result = null;
			}
			return result;
		}

		private GraphicFeatureLayer GetActiveLayer(int id, GraphicFeatureLayer[] layers)
		{
			GraphicFeatureLayer result = null;
			for (int i = 0; i < layers.Length; i++)
			{
				GraphicFeatureLayer graphicFeatureLayer = layers[i];
				if (graphicFeatureLayer.ID == id)
				{
					result = graphicFeatureLayer;
					this.inputSR = graphicFeatureLayer.SpatialReference;
					break;
				}
			}
			return result;
		}

		public string GetWKTFromLayer(int layerID)
		{
			string result = null;
			GraphicFeatureLayer[] layers = this._featureservice.GetLayers(this._symbolOpts);
			GraphicFeatureLayer[] array = layers;
			for (int i = 0; i < array.Length; i++)
			{
				GraphicFeatureLayer graphicFeatureLayer = array[i];
				if (graphicFeatureLayer.ID == layerID && graphicFeatureLayer.SpatialReference != null && !string.IsNullOrEmpty(graphicFeatureLayer.SpatialReference.WKT))
				{
					result = graphicFeatureLayer.SpatialReference.WKT;
				}
			}
			return result;
		}

		private void ReportExportError(string msg, bool fatalError)
		{
			if (this.CommandProgress != null)
			{
				CommandProgressEventArgs commandProgressEventArgs = new CommandProgressEventArgs();
				commandProgressEventArgs.ProgressTitle = msg;
				this.CommandProgress(this, commandProgressEventArgs);
			}
			if (fatalError)
			{
				base.ErrorMessage = msg;
			}
		}

		private void ReportExportStatus(string msg)
		{
			if (this.CommandProgress != null)
			{
				CommandProgressEventArgs commandProgressEventArgs = new CommandProgressEventArgs();
				commandProgressEventArgs.ProgressMessage = msg;
				this.CommandProgress(this, commandProgressEventArgs);
			}
		}

		private void ReportExportStatus(string title, string msg)
		{
			if (this.CommandProgress != null)
			{
				CommandProgressEventArgs commandProgressEventArgs = new CommandProgressEventArgs();
				commandProgressEventArgs.ProgressTitle = title;
				commandProgressEventArgs.ProgressMessage = msg;
				this.CommandProgress(this, commandProgressEventArgs);
			}
		}

		private void ReportIncrementProgress(string msg, int count)
		{
			if (this.CommandProgress != null)
			{
				CommandProgressEventArgs commandProgressEventArgs = new CommandProgressEventArgs();
				commandProgressEventArgs.ProgressMessage = msg;
				commandProgressEventArgs.ProgressValue = count;
				this.CommandProgress(this, commandProgressEventArgs);
			}
		}

		private bool ReportCheckCancel()
		{
			return this._progress != null && this._progress.UserCancelled();
		}

		private void ReportForceCancel()
		{
			if (this._progress != null)
			{
				this._progress.ForceCancel();
			}
		}

		private AGSFeatureServiceLayer ValidateMapLayerForExport(int layerId, ref string errMsg)
		{
			AGSFeatureServiceLayer result;
			try
			{
				AGSFeatureServiceLayer aGSFeatureServiceLayer = this.MapLayers[layerId] as AGSFeatureServiceLayer;
				if (aGSFeatureServiceLayer == null)
				{
					errMsg = AfaStrings.UndefinedLayer + aGSFeatureServiceLayer.Name;
					result = null;
				}
				else
				{
					result = aGSFeatureServiceLayer;
				}
			}
			catch (Exception ex)
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errValidatingLayerForExport;
				}
				throw ex;
			}
			return result;
		}

		private PropertyInfo GetTypeIDField(PropertyInfo[] props, string typeIdFieldName)
		{
			for (int i = 0; i < props.Length; i++)
			{
				PropertyInfo propertyInfo = props[i];
				if (propertyInfo.Name == typeIdFieldName)
				{
					return propertyInfo;
				}
			}
			return null;
		}

		private FieldDomain BuildSubtypeDomain(DataObjectType[] types, PropertyInfo typeIdField)
		{
			string name = base.Name + "_Types";
			CadField cadField = CadField.ToCadField(typeIdField, null);
			FieldDomain fieldDomain = new FieldDomain(name, cadField.FieldType, "CodedValueDomain");
			for (int i = 0; i < types.Length; i++)
			{
				DataObjectType dataObjectType = types[i];
				MSCCodedValue value = new MSCCodedValue(dataObjectType.Name, cadField.FieldType, dataObjectType.TypeID);
				fieldDomain.CodedValues.Add(dataObjectType.Name, value);
			}
			fieldDomain.GenerateFauxNull();
			return fieldDomain;
		}

		private AGSSubType FindSubtypeByValue(SubTypeDictionary dict, string typeValue)
		{
			foreach (AGSSubType current in dict.Values)
			{
				string a = current.ID.ToString();
				if (string.Equals(a, typeValue, StringComparison.CurrentCultureIgnoreCase))
				{
					return current;
				}
			}
			return null;
		}

		private void GenerateDefaultSubtypeColors(SubTypeDictionary dict, GraphicFeatureLayer activeLayer)
		{
			FeatureRenderer featureRenderer = activeLayer.LayerDrawingDescription.FeatureRenderer;
			if (featureRenderer is SimpleRenderer)
			{
				return;
			}
			if (featureRenderer is UniqueValueRenderer)
			{
				UniqueValueRenderer uniqueValueRenderer = featureRenderer as UniqueValueRenderer;
				if (!object.Equals(uniqueValueRenderer.Field1, activeLayer.TypeIDPropName))
				{
					return;
				}
				if (!string.IsNullOrEmpty(uniqueValueRenderer.Field2))
				{
					return;
				}
				if (!string.IsNullOrEmpty(uniqueValueRenderer.Field3))
				{
					return;
				}
				UniqueValueInfo[] uniqueValueInfos = uniqueValueRenderer.UniqueValueInfos;
				for (int i = 0; i < uniqueValueInfos.Length; i++)
				{
					UniqueValueInfo uniqueValueInfo = uniqueValueInfos[i];
					AGSSubType aGSSubType = this.FindSubtypeByValue(dict, uniqueValueInfo.Value);
					if (aGSSubType != null)
					{
						aGSSubType.SuggestedColor = this.GetSymbolColor(uniqueValueInfo.Symbol);
					}
				}
			}
		}

		private SubTypeDictionary BuildSubTypes(GraphicFeatureLayer activeLayer, ref int subtypeCount)
		{
			SubTypeDictionary subTypeDictionary = this.BuildSubTypes(activeLayer.Types, ref subtypeCount);
			if (subtypeCount > 0)
			{
				this.GenerateDefaultSubtypeColors(subTypeDictionary, activeLayer);
			}
			return subTypeDictionary;
		}

		private SubTypeDictionary BuildSubTypes(DataObjectType[] types, ref int subtypeCount)
		{
			SubTypeDictionary subTypeDictionary = new SubTypeDictionary();
			SubTypeDictionary result;
			try
			{
				for (int i = 0; i < types.Length; i++)
				{
					DataObjectType dataObjectType = types[i];
					AGSSubType aGSSubType = new AGSSubType();
					aGSSubType.ID = dataObjectType.TypeID;
					aGSSubType.Name = dataObjectType.Name;
					subTypeDictionary.Add(aGSSubType.Name, aGSSubType);
				}
				subtypeCount = subTypeDictionary.Count;
				result = subTypeDictionary;
			}
			catch
			{
				subtypeCount = 0;
				result = null;
			}
			return result;
		}

		private string CombineWhereClauses(string baseClause, string clause)
		{
			string result;
			if (!string.IsNullOrEmpty(baseClause))
			{
				result = baseClause + " AND (" + clause + ")";
			}
			else
			{
				result = clause;
			}
			return result;
		}

		private int[] BuildFIDSet(int layerID, string defExpression, SpatialFilter spatialfilter)
		{
			int[] result;
			try
			{
				spatialfilter.WhereClause = defExpression;
				result = this._featureservice.QueryIDs(layerID, defExpression, spatialfilter);
			}
			catch (Exception ex)
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errQueryFeatureIDs + "\n" + ex.Message;
				}
				throw;
			}
			return result;
		}

		private GraphicFeature RepairSpatialReference(GraphicFeature f, SpatialReference spRef)
		{
			GraphicFeature result;
			try
			{
				Geometry geometry = f.Geometry;
				if (geometry is PolygonN)
				{
					PolygonN polygonN = (PolygonN)geometry;
					if (polygonN.SpatialReference == null)
					{
						polygonN.SpatialReference = spRef;
						f.Geometry = polygonN;
					}
				}
				else if (geometry is PolylineN)
				{
					PolylineN polylineN = (PolylineN)geometry;
					if (polylineN.SpatialReference == null)
					{
						polylineN.SpatialReference = spRef;
						f.Geometry = polylineN;
					}
				}
				else if (geometry is PointN)
				{
					PointN pointN = (PointN)geometry;
					if (pointN.SpatialReference == null)
					{
						pointN.SpatialReference = spRef;
						f.Geometry = pointN;
					}
				}
				else if (geometry is MultipointN)
				{
					MultipointN multipointN = (MultipointN)geometry;
					if (multipointN.SpatialReference == null)
					{
						multipointN.SpatialReference = spRef;
						f.Geometry = multipointN;
					}
				}
				result = f;
			}
			catch
			{
				result = f;
			}
			return result;
		}

		private void RepairSpatialReference(ref DataObjects dataObjects, SpatialReference spRef)
		{
			try
			{
				int num = dataObjects.DataObjectArray.Length;
				for (int i = 0; i < num; i++)
				{
					GraphicFeature graphicFeature = (GraphicFeature)dataObjects.DataObjectArray[i];
					if (graphicFeature != null)
					{
						dataObjects.DataObjectArray[i] = this.RepairSpatialReference(graphicFeature, spRef);
					}
				}
			}
			catch
			{
			}
		}

		private bool NeedsToBeDensified(Geometry g)
		{
			if (g == null)
			{
				return false;
			}
			if (g is PointN)
			{
				return false;
			}
			if (g is MultipointN)
			{
				return false;
			}
			if (g is PolylineN)
			{
				PolylineN polylineN = g as PolylineN;
				Path[] pathArray = polylineN.PathArray;
				for (int i = 0; i < pathArray.Length; i++)
				{
					Path path = pathArray[i];
					if (path.PointArray == null)
					{
						Segment[] segmentArray = path.SegmentArray;
						for (int j = 0; j < segmentArray.Length; j++)
						{
							Segment segment = segmentArray[j];
							if (segment is EllipticArc)
							{
								bool result = true;
								return result;
							}
							if (segment is BezierCurve)
							{
								bool result = true;
								return result;
							}
						}
					}
				}
				return false;
			}
			if (g is PolygonN)
			{
				PolygonN polygonN = g as PolygonN;
				Ring[] ringArray = polygonN.RingArray;
				for (int k = 0; k < ringArray.Length; k++)
				{
					Ring ring = ringArray[k];
					if (ring.PointArray == null)
					{
						Segment[] segmentArray2 = ring.SegmentArray;
						for (int l = 0; l < segmentArray2.Length; l++)
						{
							Segment segment2 = segmentArray2[l];
							if (segment2 is EllipticArc)
							{
								bool result = true;
								return result;
							}
							if (segment2 is BezierCurve)
							{
								bool result = true;
								return result;
							}
						}
					}
				}
				return false;
			}
			return false;
		}

		private bool NeedsToBeDensified(ref DataObjects dataObjects)
		{
			ArcGIS10Types.DataObject[] dataObjectArray = dataObjects.DataObjectArray;
			for (int i = 0; i < dataObjectArray.Length; i++)
			{
				ArcGIS10Types.DataObject dataObject = dataObjectArray[i];
				GraphicFeature graphicFeature = (GraphicFeature)dataObject;
				if (graphicFeature != null && this.NeedsToBeDensified(graphicFeature.Geometry))
				{
					return true;
				}
			}
			return false;
		}

		private DataObjects DensifyDataObjects(SpatialReference spRef, EnvelopeN env, ref DataObjects dataObjects)
		{
			if (spRef == null)
			{
				base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errDensifyingDataObjects;
				return null;
			}
			DataObjects result;
			try
			{
				if (!this.NeedsToBeDensified(ref dataObjects))
				{
					result = dataObjects;
				}
				else
				{
					List<Geometry> list = new List<Geometry>();
					int num = dataObjects.DataObjectArray.Length;
					double num2 = 0.0;
					for (int i = 0; i < num; i++)
					{
						GraphicFeature graphicFeature = (GraphicFeature)dataObjects.DataObjectArray[i];
						if (graphicFeature != null && graphicFeature.Geometry != null)
						{
							list.Add(graphicFeature.Geometry);
							if (num2 == 0.0)
							{
								num2 = this.SegmentDeviation(graphicFeature.Geometry);
							}
						}
					}
					if (this.ReportCheckCancel())
					{
						result = null;
					}
					else
					{
						Geometry[] array = base.Parent.GeometryService.DensifyGeometry(spRef, num2, list.ToArray());
						for (int j = 0; j < num; j++)
						{
							GraphicFeature graphicFeature2 = (GraphicFeature)dataObjects.DataObjectArray[j];
							if (graphicFeature2 != null && graphicFeature2.Geometry != null)
							{
								graphicFeature2.Geometry = array[j];
								dataObjects.DataObjectArray[j] = graphicFeature2;
							}
						}
						result = dataObjects;
					}
				}
			}
			catch
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errDensifyingDataObjects;
				}
				throw;
			}
			return result;
		}

		private string BuildExpressionFromFIDS(string OIDFieldName, int[] fidList)
		{
			if (fidList.Length == 0)
			{
				return "";
			}
			string text = OIDFieldName + " IN (" + fidList[0];
			for (int i = 1; i < fidList.Length; i++)
			{
				text = text + "," + fidList[i];
			}
			return text + ")";
		}

		private DataObjects QueryFeatures(string name, int layerID, EnvelopeN env, string defExpression, SpatialFilter spatialfilter, bool useTrueCurves, ref string errMsg)
		{
			QueryResultOptions queryResultOptions = new QueryResultOptions();
			queryResultOptions.Format = esriQueryResultFormat.esriQueryResultRecordSetAsObject;
			ServiceDataOptions serviceDataOptions = new ServiceDataOptions();
			serviceDataOptions.Format = "native";
			DataObjects result;
			try
			{
				if (this.ReportCheckCancel())
				{
					result = null;
				}
				else
				{
					spatialfilter.WhereClause = null;
					ServiceData serviceData = this._featureservice.Query(layerID, defExpression, spatialfilter, serviceDataOptions);
					DataObjects dataObjects = serviceData.Object as DataObjects;
					if (!useTrueCurves)
					{
						dataObjects = this.DensifyDataObjects(env.SpatialReference, env, ref dataObjects);
						errMsg = base.ErrorMessage;
					}
					result = dataObjects;
				}
			}
			catch
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorInQueryFeatureData;
					errMsg = base.ErrorMessage;
				}
				throw;
			}
			return result;
		}

		private bool ExtractAndDraw(AGSFeatureServiceLayer thisLayer, AGSSubType thisSubtype, MSCFeatureClass fc, int layerID, SpatialReference outputSR, EnvelopeN env, string defExpression, SpatialFilter spatialfilter, string outputCadLayer)
		{
			if (this.ReportCheckCancel())
			{
				return false;
			}
			bool result;
			try
			{
				List<int> list = new List<int>(this.BuildFIDSet(layerID, defExpression, spatialfilter));
				if (list == null)
				{
					this.ReportExportError(base.ErrorMessage, false);
					result = false;
				}
				else
				{
					int count = list.Count;
					if (count == 0)
					{
						this.ReportExportStatus(AfaStrings.NoFeaturesFoundOn + thisLayer.Name);
						result = true;
					}
					else
					{
						this.ReportExportStatus(count + AfaStrings.FeaturesFound);
						if (count > AGSFeatureService.MAX_RECOMMENDED)
						{
							string text = count + AfaStrings.FeaturesFound;
							text = text + "\r\n" + AfaStrings.CountExceedsRecommended + string.Format(" ({0})", AGSFeatureService.MAX_RECOMMENDED);
							if (!MessageUtil.ShowYesNo(text))
							{
								this.ReportForceCancel();
								result = false;
								return result;
							}
						}
						string text2 = "";
						DataObjects dataObjects = this.QueryFeatures(thisLayer.Name, layerID, env, defExpression, spatialfilter, true, ref text2);
						if (string.IsNullOrEmpty(text2))
						{
							if (dataObjects.DataObjectArray.Length == 0)
							{
								this.ReportExportError(AfaStrings.NoFeaturesFoundOn + thisLayer.Name, false);
								result = true;
							}
							else
							{
								DataObjects densifiedDataObjects = this.QueryFeatures(thisLayer.Name, layerID, env, defExpression, spatialfilter, false, ref text2);
								this.ReportExportStatus(string.Format(AfaStrings.DrawingFeatureCount, dataObjects.DataObjectArray.Length, count));
								this.AddExtractedFeatures(thisLayer, thisSubtype, fc, dataObjects, densifiedDataObjects, outputCadLayer);
								if (dataObjects.DataObjectArray.Length < count)
								{
									SpatialFilter spatialfilter2 = (SpatialFilter)Utility.CloneObject(spatialfilter);
									int num = dataObjects.DataObjectArray.Length;
									int i = num;
									string text3 = "";
									if (!string.IsNullOrEmpty(defExpression))
									{
										text3 = defExpression + " AND ";
									}
									while (i < count)
									{
										if (this.ReportCheckCancel())
										{
											result = true;
											return result;
										}
										if (num > count - i)
										{
											num = count - i;
										}
										string text4 = text3;
										string text5 = text4;
										text4 = string.Concat(new string[]
										{
											text5,
											"(",
											thisLayer.IDField,
											" IN (",
											list[i].ToString()
										});
										int num2 = 1;
										while (num2 < num && num2 + i < count)
										{
											text4 = text4 + ", " + list[i + num2].ToString();
											num2++;
										}
										text4 += "))";
										this.ReportCheckCancel();
										dataObjects = this.QueryFeatures(thisLayer.Name, layerID, env, text4, spatialfilter2, true, ref text2);
										if (!string.IsNullOrEmpty(text2))
										{
											this.ReportExportError(text2, false);
											result = false;
											return result;
										}
										if (dataObjects.DataObjectArray.Length == 0)
										{
											this.ReportExportError(AfaStrings.NoFeaturesFoundOn + thisLayer.Name, false);
											result = true;
											return result;
										}
										this.ReportCheckCancel();
										densifiedDataObjects = this.QueryFeatures(thisLayer.Name, layerID, env, text4, spatialfilter2, false, ref text2);
										this.ReportExportStatus(string.Format(AfaStrings.DrawingFeatureCount, i + dataObjects.DataObjectArray.Length, count));
										this.AddExtractedFeatures(thisLayer, thisSubtype, fc, dataObjects, densifiedDataObjects, outputCadLayer);
										i += dataObjects.DataObjectArray.Length;
									}
								}
								result = true;
							}
						}
						else
						{
							this.ReportExportError(text2, false);
							result = false;
						}
					}
				}
			}
			catch
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errExtractingAndDrawing;
				}
				throw;
			}
			return result;
		}

		private bool GeometrySupportsZ(Geometry g)
		{
			bool result;
			try
			{
				if (g == null)
				{
					result = false;
				}
				else
				{
					PolygonN polygonN = g as PolygonN;
					PolylineN polylineN = g as PolylineN;
					PointN pointN = g as PointN;
					MultipointN multipointN = g as MultipointN;
					if (polygonN != null)
					{
						result = polygonN.HasZ;
					}
					else if (polylineN != null)
					{
						result = polylineN.HasZ;
					}
					else if (pointN != null)
					{
						result = pointN.ZSpecified;
					}
					else if (multipointN != null)
					{
						result = multipointN.HasZ;
					}
					else
					{
						result = false;
					}
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		private bool GeometrySupportsM(Geometry g)
		{
			bool result;
			try
			{
				if (g == null)
				{
					result = false;
				}
				else
				{
					PolygonN polygonN = g as PolygonN;
					PolylineN polylineN = g as PolylineN;
					PointN pointN = g as PointN;
					MultipointN multipointN = g as MultipointN;
					if (polygonN != null)
					{
						result = polygonN.HasM;
					}
					else if (polylineN != null)
					{
						result = polylineN.HasM;
					}
					else if (pointN != null)
					{
						result = pointN.MSpecified;
					}
					else if (multipointN != null)
					{
						result = multipointN.HasM;
					}
					else
					{
						result = false;
					}
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		private double SegmentDeviation(Geometry g)
		{
			double result;
			try
			{
				if (g == null)
				{
					result = 0.0;
				}
				else
				{
					PolygonN polygonN = g as PolygonN;
					PolylineN polylineN = g as PolylineN;
					EnvelopeN envelopeN = null;
					if (polygonN != null)
					{
						envelopeN = (EnvelopeN)polygonN.Extent;
					}
					else if (polylineN != null)
					{
						envelopeN = (EnvelopeN)polylineN.Extent;
					}
					if (envelopeN != null)
					{
						if (envelopeN.YMax != envelopeN.YMin)
						{
							result = (envelopeN.YMax - envelopeN.YMin) / 10000.0;
						}
						else
						{
							result = (envelopeN.XMax - envelopeN.XMin) / 10000.0;
						}
					}
					else
					{
						result = 0.0;
					}
				}
			}
			catch
			{
				result = 0.0;
			}
			return result;
		}

		private void AddExtractedFeatures(AGSLayer thisLayer, AGSSubType thisSubtype, MSCFeatureClass fc, DataObjects dataObjects, DataObjects densifiedDataObjects, string outputCadLayer)
		{
			if (dataObjects.DataObjectArray.Length == 0)
			{
				this.ReportExportError(AfaStrings.NoFeaturesFound, false);
			}
			if (this.ReportCheckCancel())
			{
				return;
			}
			try
			{
				List<CadFeature> list = new List<CadFeature>();
				int num = 0;
				ObjectId blockDefId = ObjectId.Null;
				if (fc.GeometryType == MSCFeatureClass.fcTypeCode.fcTypePoint)
				{
					DocUtil.FixPDMode();
					blockDefId = DocUtil.GetBlockDefinition(fc.ParentDataset.ParentDocument, outputCadLayer);
				}
				if (dataObjects.DataObjectArray.Length > 0)
				{
					ArcGIS10Types.DataObject dataObject = dataObjects.DataObjectArray[0];
					GraphicFeature graphicFeature = (GraphicFeature)dataObject;
					Geometry geometry = graphicFeature.Geometry;
					this.HasZ = this.GeometrySupportsZ(geometry);
					this.HasM = this.GeometrySupportsM(geometry);
				}
				ArcGIS10Types.DataObject[] dataObjectArray = dataObjects.DataObjectArray;
				for (int i = 0; i < dataObjectArray.Length; i++)
				{
					ArcGIS10Types.DataObject dataObject2 = dataObjectArray[i];
					GraphicFeature graphicFeature2 = (GraphicFeature)dataObject2;
					GraphicFeature graphicFeature3 = (GraphicFeature)densifiedDataObjects.DataObjectArray[num];
					if (graphicFeature2 != null)
					{
						Geometry geometry2 = graphicFeature2.Geometry;
						Geometry geometry3 = graphicFeature3.Geometry;
						if (geometry2 != null)
						{
							double defaultElevation = 0.0;
							PropertySetProperty[] propertyArray = dataObject2.Properties.PropertyArray;
							for (int j = 0; j < propertyArray.Length; j++)
							{
								PropertySetProperty propertySetProperty = propertyArray[j];
								double num2;
								if (propertySetProperty.Key == "Elevation" && double.TryParse(propertySetProperty.Value.ToString(), out num2))
								{
									defaultElevation = num2;
									break;
								}
							}
							try
							{
								List<Entity> list2 = GIS2CAD.ToEntity(geometry2, geometry3, defaultElevation, blockDefId);
								if (list2 != null && list2.Count > 0)
								{
									CadFeature cadFeature = new CadFeature();
									if (!string.IsNullOrEmpty(thisLayer.IDField))
									{
										cadFeature.EntList = list2;
									}
									cadFeature.SvcOID = AGSFeatureService.GetObjectId(thisLayer.IDField, dataObject2.Properties.PropertyArray);
									cadFeature.Fields = CadField.ToCadFields(dataObject2.Properties.PropertyArray, fc.Fields);
									list.Add(cadFeature);
								}
							}
							catch
							{
							}
						}
					}
					num++;
				}
				try
				{
					if (!this.ReportCheckCancel())
					{
						Dictionary<ObjectId, int> second = DocUtil.AddCadFeaturesToModelSpace(fc.ParentDataset.ParentDocument, list, outputCadLayer);
						MSCFeatureService mSCFeatureService = null;
						if (fc.IsSubType)
						{
							if (fc.ParentFC is MSCFeatureService)
							{
								mSCFeatureService = (MSCFeatureService)fc.ParentFC;
							}
						}
						else if (fc is MSCFeatureService)
						{
							mSCFeatureService = (MSCFeatureService)fc;
						}
						if (mSCFeatureService != null)
						{
							mSCFeatureService.HasZ = this.HasZ;
							mSCFeatureService.HasM = this.HasM;
							mSCFeatureService.OriginalServiceIDs = mSCFeatureService.OriginalServiceIDs.Concat(second).ToDictionary((KeyValuePair<ObjectId, int> x) => x.Key, (KeyValuePair<ObjectId, int> x) => x.Value);
							mSCFeatureService.Write();
						}
					}
				}
				catch
				{
					if (string.IsNullOrEmpty(base.ErrorMessage))
					{
						base.ErrorMessage = AfaStrings.ErrorAddingEntitiesToDrawing;
					}
					throw;
				}
			}
			catch
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errAddingExtractedFeatures;
				}
				throw;
			}
		}

		public static int GetObjectId(string fieldName, PropertySetProperty[] props)
		{
			for (int i = 0; i < props.Length; i++)
			{
				PropertySetProperty propertySetProperty = props[i];
				if (propertySetProperty.Key == fieldName)
				{
					return (int)propertySetProperty.Value;
				}
			}
			return -1;
		}

		public static string BuildNameFromURL(string mapServiceURL)
		{
			string result;
			try
			{
				Uri uri = new Uri(mapServiceURL);
				string arg_0D_0 = uri.Host;
				string[] segments = uri.Segments;
				string text = "";
				bool flag = false;
				string[] array = segments;
				for (int i = 0; i < array.Length; i++)
				{
					string text2 = array[i];
					if (text2.ToLower() == "services/")
					{
						flag = true;
					}
					else if (!(text2.ToLower() == "featureserver/") && !(text2.ToLower() == "featureserver") && flag)
					{
						text += text2;
					}
				}
				char[] array2 = new char[]
				{
					'/',
					'\\'
				};
				text = text.TrimStart(array2);
				text = text.TrimEnd(array2);
				string[] array3 = text.Split(array2);
				if (array3.Length > 1)
				{
					text = array3[array3.Length - 1];
				}
				result = text;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static string BuildFullNameFromURL(string mapServiceURL)
		{
			string result;
			try
			{
				Uri uri = new Uri(mapServiceURL);
				string arg_0D_0 = uri.Host;
				string[] segments = uri.Segments;
				string text = "";
				bool flag = false;
				string[] array = segments;
				for (int i = 0; i < array.Length; i++)
				{
					string text2 = array[i];
					if (text2.ToLower() == "services/")
					{
						flag = true;
					}
					else if (!(text2.ToLower() == "featureserver/") && !(text2.ToLower() == "featureserver") && flag)
					{
						text += text2;
					}
				}
				char[] trimChars = new char[]
				{
					'/',
					'\\'
				};
				text = text.TrimStart(trimChars);
				text = text.TrimEnd(trimChars);
				result = text;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static AGSFeatureService BuildFeatureServiceFromURL(string Name, string URL)
		{
			AGSFeatureService aGSFeatureService = null;
			AGSFeatureService result;
			try
			{
				string text = AGSConnection.BuildConnectionURL(URL);
				if (string.IsNullOrEmpty(text))
				{
					result = aGSFeatureService;
				}
				else
				{
					AGSConnection aGSConnection = AGSConnection.ReestablishConnection(text, null, "");
					if (aGSConnection != null)
					{
						try
						{
							string serviceFullName = AGSFeatureService.BuildFullNameFromURL(URL);
							if (string.IsNullOrEmpty(Name))
							{
								Name = AGSFeatureService.BuildNameFromURL(URL);
							}
							aGSFeatureService = new AGSFeatureService(serviceFullName, URL, aGSConnection);
							aGSFeatureService.Name = Name;
							if (aGSFeatureService.IsValid)
							{
								result = aGSFeatureService;
								return result;
							}
							result = null;
							return result;
						}
						catch
						{
							result = null;
							return result;
						}
					}
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static AGSFeatureService BuildFeatureServiceFromURL(string Name, string URL, int layerID)
		{
			AGSFeatureService aGSFeatureService = null;
			AGSFeatureService result;
			try
			{
				string text = AGSConnection.BuildConnectionURL(URL);
				if (string.IsNullOrEmpty(text))
				{
					result = aGSFeatureService;
				}
				else
				{
					AGSConnection aGSConnection = AGSConnection.ReestablishConnection(text, null, "");
					if (aGSConnection != null)
					{
						try
						{
							string serviceFullName = AGSFeatureService.BuildFullNameFromURL(URL);
							if (string.IsNullOrEmpty(Name))
							{
								Name = AGSFeatureService.BuildNameFromURL(URL);
							}
							aGSFeatureService = new AGSFeatureService(serviceFullName, URL, aGSConnection);
							aGSFeatureService.Name = Name;
							if (aGSFeatureService.IsValid)
							{
								result = aGSFeatureService;
								return result;
							}
							result = null;
							return result;
						}
						catch
						{
							result = null;
							return result;
						}
					}
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private void InitializeFeatureService()
		{
			try
			{
				if (this._featureservice == null)
				{
					this._featureservice = new FeatureServiceProxy();
					if (string.IsNullOrEmpty(this.ServiceURL))
					{
						this.ServiceURL = base.Parent.Soap_URL + "/" + base.FullName + "/MapServer/FeatureServer";
					}
					this._featureservice.Url = base.Parent.AppendToken(this.ServiceURL);
					this._featureservice.Credentials = base.Parent.Credentials;
					if (this._featureservice.Credentials == null)
					{
						this._featureservice.UseDefaultCredentials = true;
					}
					this._featureservice.Proxy = WebRequest.DefaultWebProxy;
					base.IsValid = true;
				}
				else
				{
					this._featureservice.Credentials = base.Parent.Credentials;
					if (this._featureservice.Credentials == null)
					{
						this._featureservice.UseDefaultCredentials = true;
					}
					this._featureservice.Proxy = WebRequest.DefaultWebProxy;
				}
			}
			catch (SystemException ex)
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errInitializingFeatureService;
				}
				base.IsValid = false;
				throw ex;
			}
		}

		public string GenerateGeometryReport(int layerID)
		{
			string text = "";
			string arg = string.Concat(new string[]
			{
				base.Parent.URL,
				"/",
				base.FullName,
				"/FeatureServer/",
				layerID.ToString(),
				"/query"
			});
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("?f={0}", "json");
			stringBuilder.Append("&returnIdsOnly=true");
			stringBuilder.Append("&where=fid > 0");
			IDictionary<string, object> dictionary = base.Parent.MakeDictionaryRequest(arg + stringBuilder);
			if (dictionary == null)
			{
				return text;
			}
			long num = 0L;
			object obj;
			if (dictionary.TryGetValue("objectIds", out obj))
			{
				if (obj == null)
				{
					return "";
				}
				IList<object> list = obj as IList<object>;
				num = (long)list.Count;
			}
			if (num < this.maxCount)
			{
				stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("?f={0}", "json");
				stringBuilder.Append("&returnIdsOnly=false");
				stringBuilder.Append("&fields=*");
				stringBuilder.Append("&where=fid > 0");
				dictionary = base.Parent.MakeDictionaryRequest(arg + stringBuilder);
				if (dictionary == null)
				{
					return text;
				}
				string type = "";
				text = text + "Feature Service Query for " + base.Name;
				if (dictionary.TryGetValue("objectIdFieldName", out obj))
				{
					text = text + "\nObjectID Field Name = " + obj;
				}
				if (dictionary.TryGetValue("globalIdFieldName", out obj))
				{
					text = text + "\nGlobalID Field Name = " + obj;
				}
				if (dictionary.TryGetValue("geometryType", out obj))
				{
					text = text + "\nGeometry Type = " + obj;
					type = (obj as string);
				}
				if (dictionary.TryGetValue("spatialReference", out obj))
				{
					text = text + "\nSpatial Reference = " + this.SpatialReferenceToString(obj);
				}
				if (dictionary.TryGetValue("features", out obj))
				{
					IList<object> list2 = obj as IList<object>;
					text += "\nFeatures:";
					foreach (object current in list2)
					{
						IDictionary<string, object> dictionary2 = current as IDictionary<string, object>;
						if (dictionary2.TryGetValue("geometry", out obj))
						{
							text = text + "\n\t" + this.GeometryToString(type, obj);
						}
						if (dictionary2.TryGetValue("attributes", out obj))
						{
							text = text + "\n\t" + this.AttributesToString(obj);
						}
					}
				}
			}
			return text;
		}

		private string SpatialReferenceToString(object value)
		{
			IDictionary<string, object> dictionary = value as IDictionary<string, object>;
			object obj;
			if (dictionary.TryGetValue("wkid", out obj))
			{
				return obj as string;
			}
			if (dictionary.TryGetValue("wkt", out obj))
			{
				return obj as string;
			}
			return "";
		}

		private string GeometryToString(string type, object value)
		{
			IDictionary<string, object> dictionary = value as IDictionary<string, object>;
			if (type == "esriGeometryPoint")
			{
				return this.PointToString(dictionary);
			}
			if (type == "esriGeometryMultipoint")
			{
				return this.MultiPointToString(dictionary);
			}
			if (type == "esriPolyline")
			{
				return this.PolylineToString(dictionary);
			}
			if (type == "esriPolygon")
			{
				return this.PolygonToString(dictionary);
			}
			if (type == "esriEnvelope")
			{
				return this.EnvelopeToString(dictionary);
			}
			return "";
		}

		private string PointToString(IDictionary<string, object> pt)
		{
			string text = "";
			object arg;
			if (pt.TryGetValue("x", out arg))
			{
				text = text + "x=" + arg;
			}
			if (pt.TryGetValue("y", out arg))
			{
				text = text + ", y=" + arg;
			}
			return text;
		}

		private string MultiPointToString(IDictionary<string, object> pts)
		{
			string text = "";
			object obj;
			if (pts.TryGetValue("points", out obj))
			{
				IList<object> list = obj as IList<object>;
				using (IEnumerator<object> enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IList<object> list2 = (IList<object>)enumerator.Current;
						string text2 = text;
						text = string.Concat(new string[]
						{
							text2,
							"[",
							list2[0].ToString(),
							", ",
							list2[1].ToString(),
							"] "
						});
					}
				}
			}
			return text;
		}

		private string PolylineToString(IDictionary<string, object> pline)
		{
			string text = "";
			object obj;
			if (pline.TryGetValue("paths", out obj))
			{
				StringBuilder stringBuilder = new StringBuilder();
				IList<object> list = obj as IList<object>;
				using (IEnumerator<object> enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IList<object> list2 = (IList<object>)enumerator.Current;
						IList<object> list3 = list2[0] as IList<object>;
						IList<object> list4 = list2[1] as IList<object>;
						stringBuilder.AppendFormat("[[{0},{1}], [{2},{3}]] ", new object[]
						{
							list3[0].ToString(),
							list3[1].ToString(),
							list4[0].ToString(),
							list4[1].ToString()
						});
					}
				}
				text += stringBuilder;
			}
			return text;
		}

		private string PolygonToString(IDictionary<string, object> pt)
		{
			return "";
		}

		private string EnvelopeToString(IDictionary<string, object> pt)
		{
			return "";
		}

		private string AttributesToString(object value)
		{
			IDictionary<string, object> dictionary = value as IDictionary<string, object>;
			object obj;
			if (dictionary.TryGetValue("wkid", out obj))
			{
				return obj as string;
			}
			if (dictionary.TryGetValue("wkt", out obj))
			{
				return obj as string;
			}
			return "";
		}
	}
}
