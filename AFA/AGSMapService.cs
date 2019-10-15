using AFA.Resources;
using AFA.UI;
using ArcGIS10Types;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace AFA
{
    public class AGSMapService : AGSRasterService, IAGSMapService, IAGSService, IAGSObject, ILongProcessCommand
	{
		public bool IsWaiting;

		private SpatialReference outputSR;

		private MapServerInfo _mapinfo;

		private MapServerProxy _mapservice;

		private ExportedImage _currentImage;

		private ProgressIndicator _progress;

		private static int MAX_RECOMMENDED = 15000;

		public event ExportImageEventHandler OnExportImage;

		public event CommandStartedEventHandler CommandStarted;

		public event CommandEndedEventHandler CommandEnded;

		public event CommandProgressEventHandler CommandProgress;

		public event CommandProgressUpdateValuesEventHandler CommandUpdateProgressValues;

		public IDictionary<int, object> MapLayers
		{
			get;
			set;
		}

		public bool IsVisible
		{
			get;
			set;
		}

		public bool HasTiles
		{
			get;
			set;
		}

		public bool SupportsMap
		{
			get;
			set;
		}

		public bool SupportsQuery
		{
			get;
			set;
		}

		public bool SupportsData
		{
			get;
			set;
		}

		public ExportedImage OutputImage
		{
			get;
			set;
		}

		public bool IsFixedScale
		{
			get;
			set;
		}

		public string ServiceURL
		{
			get;
			set;
		}

		public string ThumbnailURL
		{
			get;
			set;
		}

		public bool ContainsFeatureLayers
		{
			get
			{
				try
				{
					foreach (object current in this.MapLayers.Values)
					{
						AGSMapLayer aGSMapLayer = current as AGSMapLayer;
						if (aGSMapLayer.IsFeatureLayer)
						{
							bool result = true;
							return result;
						}
						if (aGSMapLayer.IsGroupLayer)
						{
							foreach (object current2 in aGSMapLayer.Items)
							{
								AGSMapLayer aGSMapLayer2 = current2 as AGSMapLayer;
								if (aGSMapLayer2.IsFeatureLayer)
								{
									bool result = true;
									return result;
								}
							}
						}
					}
				}
				catch
				{
					bool result = false;
					return result;
				}
				return false;
			}
		}

		public IList<object> Items
		{
			get
			{
				IList<object> list = new List<object>();
				foreach (KeyValuePair<int, object> current in this.MapLayers)
				{
					AGSMapLayer aGSMapLayer = current.Value as AGSMapLayer;
					if (aGSMapLayer.parentLayerId == -1)
					{
						list.Add(current.Value);
					}
				}
				return list;
			}
		}

		public AGSMapService(string name, string serviceURL, AGSConnection parent) : base(name, parent)
		{
			try
			{
				this.MapLayers = new Dictionary<int, object>();
				base.ErrorMessage = "";
				this.RefreshProperties();
				if (parent.ConnectionFailed)
				{
					throw new Exception(AfaStrings.ErrorEncounteredIn + AfaStrings.errMapConstructor);
				}
				this.InitializeExportProperties();
				this.IsVisible = true;
			}
			catch
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errMapConstructor;
				}
				throw;
			}
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
					else if (!(text2.ToLower() == "mapserver/") && !(text2.ToLower() == "mapserver") && flag)
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
					else if (!(text2.ToLower() == "mapserver/") && !(text2.ToLower() == "mapserver") && flag)
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

		public static AGSMapService BuildMapServiceFromURL(string Name, string URL)
		{
			AGSMapService aGSMapService = null;
			AGSMapService result;
			try
			{
				string text = AGSConnection.BuildConnectionURL(URL);
				if (string.IsNullOrEmpty(text))
				{
					result = aGSMapService;
				}
				else
				{
					AGSConnection aGSConnection = AGSConnection.ReestablishConnection(text, null, "");
					if (aGSConnection != null)
					{
						try
						{
							string name = AGSMapService.BuildFullNameFromURL(URL);
							if (string.IsNullOrEmpty(Name))
							{
								Name = AGSMapService.BuildNameFromURL(URL);
							}
							aGSMapService = new AGSMapService(name, URL, aGSConnection);
							aGSMapService.Name = Name;
							if (aGSMapService.IsValid)
							{
								result = aGSMapService;
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

		public List<AGSMapLayer> GetDefaultMapLayers()
		{
			List<AGSMapLayer> list = new List<AGSMapLayer>();
			if (this.Items == null)
			{
				return list;
			}
			foreach (object current in this.Items)
			{
				try
				{
					AGSMapLayer item = current as AGSMapLayer;
					list.Add(item);
				}
				catch
				{
				}
			}
			return list;
		}

		public override string GetWKT()
		{
			string result;
			try
			{
				string text = null;
				if (base.Properties.ContainsKey("WKT"))
				{
					result = base.Properties["WKT"].ToString();
				}
				else
				{
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
					if (string.IsNullOrEmpty(text) && this.InitializeMapService())
					{
						MapDescription defaultMapDescription = this._mapinfo.DefaultMapDescription;
						text = defaultMapDescription.SpatialReference.WKT;
						if (string.IsNullOrEmpty(text) && defaultMapDescription.SpatialReference.WKIDSpecified)
						{
							text = defaultMapDescription.SpatialReference.WKID.ToString();
						}
					}
					result = text;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

     
        private ImageDescription InitializeImageDescription(AGSExportOptions eo)
		{
            ImageDescription description = new ImageDescription()
            {
                ImageType = new ImageType()
            };
            description.ImageType.ImageFormat = ConvertImageFormat(eo.Format);
					 description.ImageType.ImageReturnType = esriImageReturnType.esriImageReturnURL;
            description.ImageDisplay = new ImageDisplay()
            {
                ImageHeight = eo.Height,
                ImageWidth = eo.Width,
                ImageDPI = (double)eo.DPI
            };
            return description;

            }

    
    

		public override ITask GetExportTask(AGSExportOptions eo, HiddenUpdateForm form)
		{
			ImageDescription imagedescription = this.InitializeImageDescription(eo);
			if (!this.InitializeMapService())
			{
				return null;
			}
			MapDescription mapDescription = (MapDescription)Utility.CloneObject(this._mapinfo.DefaultMapDescription);
			new List<SpatialReference>();
			EnvelopeN envelopeN = this._mapinfo.FullExtent as EnvelopeN;
			envelopeN.XMin = eo.BoundingBox.XMin.Value;
			envelopeN.XMax = eo.BoundingBox.XMax.Value;
			envelopeN.YMin = eo.BoundingBox.YMin.Value;
			envelopeN.YMax = eo.BoundingBox.YMax.Value;
			envelopeN.SpatialReference = AGSSpatialReference.SpRefFromString(eo.BoundingBox.SpatialReference);
			mapDescription.MapArea.Extent = envelopeN;
			if (envelopeN.SpatialReference == null)
			{
				EnvelopeN envelopeN2 = (EnvelopeN)this._mapinfo.FullExtent;
				envelopeN.SpatialReference = envelopeN2.SpatialReference;
			}
			this.outputSR = AGSSpatialReference.SpRefFromString(eo.OutputWKT);
			mapDescription.SpatialReference = this.outputSR;
			LayerDescription[] layerDescriptions = mapDescription.LayerDescriptions;
			for (int i = 0; i < layerDescriptions.Length; i++)
			{
				LayerDescription layerDescription = layerDescriptions[i];
				if (this.MapLayers.ContainsKey(layerDescription.LayerID))
				{
					AGSMapLayer aGSMapLayer = this.MapLayers[layerDescription.LayerID] as AGSMapLayer;
					layerDescription.Visible = aGSMapLayer.IsVisible;
				}
			}
			mapDescription.TransparentColor = this._mapinfo.DefaultMapDescription.BackgroundSymbol.Color;
			return new UpdateMapImageTask
			{
				Imagedescription = imagedescription,
				Mapservice = this._mapservice,
				Mapdesc = mapDescription,
				UpdateForm = form,
				OutputFile = eo.OutputFile,
				OutputSpatialReference = this.outputSR,
				GeometryService = base.Parent.GeometryService
			};
		}

		private void InitializeExportProperties()
		{
			this.ExportOptions = new AGSExportOptions();
			this.ExportOptions.Dynamic = true;
			this.ExportOptions.Format = "PNG24";
			if (base.SupportedImageTypes == null)
			{
				base.SupportedImageTypes = new StringList();
				base.SupportedImageTypes.Add("PNG");
			}
			this.ExportOptions.SupportedFormats = base.SupportedImageTypes.ToArray();
			this.ExportOptions.Transparency = 30;
		}

		public override bool ExportMapNow(AGSExportOptions eo, ExportImageEventHandler action, AGSExportOptions cancelOptions)
		{
			bool result;
			try
			{
				this.OnExportImage = action;
				base.ErrorMessage = "";
				ImageDescription imageDescription = this.InitializeImageDescription(eo);
				if (!this.InitializeMapService())
				{
					result = false;
				}
				else
				{
					MapDescription mapDescription = (MapDescription)Utility.CloneObject(this._mapinfo.DefaultMapDescription);
					new List<SpatialReference>();
					EnvelopeN envelopeN = this._mapinfo.FullExtent as EnvelopeN;
					envelopeN.XMin = eo.BoundingBox.XMin.Value;
					envelopeN.XMax = eo.BoundingBox.XMax.Value;
					envelopeN.YMin = eo.BoundingBox.YMin.Value;
					envelopeN.YMax = eo.BoundingBox.YMax.Value;
					envelopeN.SpatialReference = AGSSpatialReference.SpRefFromString(eo.BoundingBox.SpatialReference);
					mapDescription.MapArea.Extent = envelopeN;
					if (envelopeN.SpatialReference == null)
					{
						EnvelopeN envelopeN2 = (EnvelopeN)this._mapinfo.FullExtent;
						envelopeN.SpatialReference = envelopeN2.SpatialReference;
					}
					this.outputSR = AGSSpatialReference.SpRefFromString(eo.OutputWKT);
					mapDescription.SpatialReference = this.outputSR;
					mapDescription.TransparentColor = this._mapinfo.DefaultMapDescription.BackgroundSymbol.Color;
					LayerDescription[] layerDescriptions = mapDescription.LayerDescriptions;
					for (int i = 0; i < layerDescriptions.Length; i++)
					{
						LayerDescription layerDescription = layerDescriptions[i];
						if (this.MapLayers.ContainsKey(layerDescription.LayerID))
						{
							AGSMapLayer aGSMapLayer = this.MapLayers[layerDescription.LayerID] as AGSMapLayer;
							layerDescription.Visible = aGSMapLayer.IsVisible;
						}
					}
					MapImage mapImage = this._mapservice.ExportMapImage(mapDescription, imageDescription);
					string text = mapImage.ImageURL;
					if (string.IsNullOrEmpty(text))
					{
						base.ErrorMessage = AfaStrings.ErrorGeneratingImage;
						this.IsWaiting = false;
						result = false;
					}
					else
					{
						if (!string.IsNullOrEmpty(text))
						{
							EnvelopeN envelopeN3 = mapImage.Extent as EnvelopeN;
							ExportedImage exportedImage = new ExportedImage(this, text);
							exportedImage.Properties.Add("width", mapImage.ImageWidth);
							exportedImage.Properties.Add("height", mapImage.ImageHeight);
							exportedImage.Properties.Add("scale", mapImage.MapScale);
							PointN[] array = new PointN[]
							{
								new PointN
								{
									SpatialReference = envelopeN3.SpatialReference,
									X = envelopeN3.XMin,
									Y = envelopeN3.YMin,
									Z = 0.0
								},
								new PointN
								{
									SpatialReference = envelopeN3.SpatialReference,
									X = envelopeN3.XMax,
									Y = envelopeN3.YMin,
									Z = 0.0
								},
								new PointN
								{
									SpatialReference = envelopeN3.SpatialReference,
									X = envelopeN3.XMin,
									Y = envelopeN3.YMax,
									Z = 0.0
								}
							};
							exportedImage.Properties.Add("xmin", envelopeN3.XMin);
							exportedImage.Properties.Add("xmax", envelopeN3.XMax);
							exportedImage.Properties.Add("ymin", envelopeN3.YMin);
							exportedImage.Properties.Add("ymax", envelopeN3.YMax);
							exportedImage.Properties.Add("axis1X", array[1].X);
							exportedImage.Properties.Add("axis1Y", array[1].Y);
							exportedImage.Properties.Add("axis2X", array[2].X);
							exportedImage.Properties.Add("axis2Y", array[2].Y);
							if (envelopeN3.SpatialReference != null)
							{
								exportedImage.Properties.Add("wkt", envelopeN3.SpatialReference.WKT);
							}
							this._currentImage = exportedImage;
							try
							{
								if (!string.IsNullOrEmpty(eo.OutputFile))
								{
									if (File.Exists(eo.OutputFile))
									{
										File.Delete(eo.OutputFile);
										if (App.TempFiles.Contains(eo.OutputFile))
										{
											App.TempFiles.Remove(eo.OutputFile);
										}
									}
									if (base.Parent.DownloadFile(this._currentImage.Path, eo.OutputFile))
									{
										text = eo.OutputFile;
									}
								}
								else
								{
									eo.OutputFile = text;
								}
							}
							catch
							{
								eo.OutputFile = text;
								base.ErrorMessage = AfaStrings.ErrorUnableToAccessOutputImage + " (" + text + ")";
								result = false;
								return result;
							}
							finally
							{
								this.IsWaiting = false;
								base.IsValid = true;
								if (this.OnExportImage != null)
								{
									this.OnExportImage(this, new ExportedImageEventArgs(this, eo, array));
								}
							}
						}
						result = string.IsNullOrEmpty(base.ErrorMessage);
					}
				}
			}
			catch (SystemException)
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorInExportMap2;
				}
				result = false;
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

		public bool ExportFeatures(AGSExportOptions eo, List<int> layerIDs, ref string errList)
		{
			base.ErrorMessage = null;
			errList = "";
			this._progress = null;
			bool result;
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
				try
				{
					if (!this.InitializeMapService())
					{
						if (this.CommandEnded != null)
						{
							this.CommandEnded(this, EventArgs.Empty);
						}
						result = false;
					}
					else
					{
						foreach (int current in layerIDs)
						{
							if (this.ReportCheckCancel())
							{
								break;
							}
							base.ErrorMessage = "";
							LayerDescription activeLayerDescription = this.GetActiveLayerDescription(this._mapinfo.DefaultMapDescription, current);
							if (activeLayerDescription != null)
							{
								string text = "";
								AGSMapLayer aGSMapLayer = this.ValidateMapLayerForExport(activeLayerDescription.LayerID, ref text);
								if (aGSMapLayer == null)
								{
									if (!string.IsNullOrEmpty(errList))
									{
										errList += "\n";
									}
									string text2 = errList;
									errList = string.Concat(new string[]
									{
										text2,
										text,
										" (",
										aGSMapLayer.Name,
										")"
									});
								}
								else
								{
									try
									{
										this.ExportFeatures(activeLayerDescription, eo);
									}
									catch
									{
										if (!string.IsNullOrEmpty(errList))
										{
											errList += "\n";
										}
										string text3 = errList;
										errList = string.Concat(new string[]
										{
											text3,
											base.ErrorMessage,
											" (",
											aGSMapLayer.Name,
											")"
										});
									}
								}
							}
						}
						if (this.CommandEnded != null)
						{
							this.CommandEnded(this, EventArgs.Empty);
						}
						result = string.IsNullOrEmpty(errList);
					}
				}
				catch
				{
					if (this.CommandEnded != null)
					{
						this.CommandEnded(this, EventArgs.Empty);
					}
					if (string.IsNullOrEmpty(base.ErrorMessage))
					{
						base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errExportingFeatures;
					}
					result = false;
				}
			}
			return result;
		}

		private void GetLayerDrawingDescription(int layerID)
		{
			List<int> list = new List<int>();
			try
			{
				MapServerInfo serverInfo = this._mapservice.GetServerInfo(this._mapservice.GetDefaultMapName());
				MapLayerInfo[] mapLayerInfos = serverInfo.MapLayerInfos;
				MapLayerInfo[] array = mapLayerInfos;
				for (int i = 0; i < array.Length; i++)
				{
					MapLayerInfo mapLayerInfo = array[i];
					if (mapLayerInfo.HasLayerDrawingDescription)
					{
						list.Add(mapLayerInfo.LayerID);
					}
				}
			}
			catch
			{
			}
			try
			{
				int arg_61_0 = list.Count;
			}
			catch (SystemException)
			{
			}
		}

		private LayerDescription GetActiveLayerDescription(MapDescription mapdesc, int layerId)
		{
			LayerDescription[] layerDescriptions = mapdesc.LayerDescriptions;
			for (int i = 0; i < layerDescriptions.Length; i++)
			{
				LayerDescription layerDescription = layerDescriptions[i];
				if (layerDescription.LayerID == layerId)
				{
					return layerDescription;
				}
			}
			return null;
		}

		private AGSMapLayer ValidateMapLayerForExport(int layerId, ref string errMsg)
		{
			AGSMapLayer result;
			try
			{
				AGSMapLayer aGSMapLayer = this.MapLayers[layerId] as AGSMapLayer;
				if (aGSMapLayer == null)
				{
					errMsg = AfaStrings.UndefinedLayer + aGSMapLayer.Name;
					result = null;
				}
				else if (!aGSMapLayer.IsFeatureLayer)
				{
					errMsg = AfaStrings.NoFeatureLayerFound + aGSMapLayer.Name;
					result = null;
				}
				else if (string.IsNullOrEmpty(aGSMapLayer.GeomField))
				{
					errMsg = AfaStrings.NoGeometryFieldPublishedOn + aGSMapLayer.Name;
					result = null;
				}
				else
				{
					result = aGSMapLayer;
				}
			}
			catch (Exception ex)
			{
				errMsg = ex.Message;
				result = null;
			}
			return result;
		}

		private SubTypeDictionary BuildSubTypes(AGSLayer lyr, ref string typeIDField, ref int subtypeCount)
		{
			object obj = null;
			SubTypeDictionary subTypeDictionary = null;
			SubTypeDictionary result;
			try
			{
				if (lyr.Properties.TryGetValue("Type Id Field", out obj))
				{
					typeIDField = (obj as string);
					if (!string.IsNullOrEmpty(typeIDField) && lyr.Properties.TryGetValue("Subtypes", out obj))
					{
						subTypeDictionary = (obj as SubTypeDictionary);
						if (subTypeDictionary != null)
						{
							subtypeCount = subTypeDictionary.Count;
						}
					}
				}
				result = subTypeDictionary;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private void ExportFeatures(LayerDescription activelayerdesc, AGSExportOptions eo)
		{
			try
			{
				string msg = "";
				AGSMapLayer aGSMapLayer = this.ValidateMapLayerForExport(activelayerdesc.LayerID, ref msg);
				if (aGSMapLayer == null)
				{
					this.ReportExportError(msg, false);
				}
				else
				{
					this.ReportExportStatus(AfaStrings.Processing + aGSMapLayer.Name, "");
					MapDescription defaultMapDescription = this._mapinfo.DefaultMapDescription;
					SpatialReference spatialReference = defaultMapDescription.SpatialReference;
					SpatialReference spatialReference2 = AGSSpatialReference.SpRefFromString(eo.OutputWKT);
					List<CadField> cf = CadField.ToCadFields(this._mapinfo.MapLayerInfos[aGSMapLayer.Id].Fields);
					if (string.IsNullOrEmpty(eo.BoundingBox.SpatialReference))
					{
						eo.BoundingBox.SpatialReference = spatialReference.WKT;
					}
					EnvelopeN env = AGSService.InitializeEnvelope(defaultMapDescription.MapArea.Extent as EnvelopeN, eo.BoundingBox);
					SpatialFilter spatialFilter = AGSService.BuildSpatialFilter(env, aGSMapLayer.GeomField);
					string defaultActiveLayerDesc = string.Copy(activelayerdesc.DefinitionExpression);
					spatialFilter.WhereClause = this.CombineWhereClauses(activelayerdesc.DefinitionExpression, eo.WhereClause);
					string text = base.Name + "_" + aGSMapLayer.Name;
					MSCFeatureClass fc = MSCDataset.AddSimpleFeatureClass(text, aGSMapLayer.GeometryType, Utility.InitializeStringList(text), cf, true, null);
					this.ExtractAndDraw(aGSMapLayer, null, fc, defaultMapDescription, ref spatialReference, spatialReference2, env, activelayerdesc, spatialFilter, defaultActiveLayerDesc);
				}
			}
			catch (SystemException)
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errExportingFeatures;
				}
				throw;
			}
		}

		private string CombineWhereClauses(string baseClause, string clause)
		{
			string text = baseClause;
			if (!string.IsNullOrEmpty(baseClause))
			{
				if (!string.IsNullOrEmpty(clause))
				{
					text = text + " AND (" + clause + ")";
				}
			}
			else
			{
				text = clause;
			}
			if (string.IsNullOrEmpty(text))
			{
				text = "";
			}
			return text;
		}

		private FIDSet BuildFIDSet(string name, LayerDescription activelayerdesc, SpatialFilter spatialfilter)
		{
			FIDSet result;
			try
			{
				if (!this.ReportCheckCancel())
				{
					if (this.InitializeMapService())
					{
						if (!this.ReportCheckCancel())
						{
							result = this._mapservice.QueryFeatureIDs2(name, activelayerdesc, spatialfilter);
							return result;
						}
					}
					else
					{
						base.ErrorMessage = base.Parent.ErrorMessage;
					}
				}
				result = null;
			}
			catch (Exception ex)
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errQueryFeatureIDs;
				}
				throw ex;
			}
			return result;
		}

		private int FindGeometryFieldIndex(RecordSet recordset)
		{
			int result;
			try
			{
				int num = -1;
                ArcGIS10Types.Field[] fieldArray = recordset.Fields.FieldArray;
				for (int i = 0; i < fieldArray.Length; i++)
				{
                    ArcGIS10Types.Field field = fieldArray[i];
					num++;
					if (field.Type == esriFieldType.esriFieldTypeGeometry)
					{
						result = num;
						return result;
					}
				}
				result = -1;
			}
			catch
			{
				result = -1;
			}
			return result;
		}

		private RecordSet ProjectRecordset(RecordSet rs, SpatialReference outputSR, EnvelopeN env, ref string errMsg)
		{
			if (this.ReportCheckCancel())
			{
				return null;
			}
			if (base.Parent.GeometryService == null)
			{
				errMsg = AfaStrings.NoGeometryServerDefinedUnableToProjectFeatures;
				return rs;
			}
			RecordSet result;
			try
			{
				int num = this.FindGeometryFieldIndex(rs);
				if (num == -1)
				{
					result = rs;
				}
				else
				{
					SpatialReference spatialReference = rs.Fields.FieldArray[num].GeometryDef.SpatialReference;
					if (spatialReference == null)
					{
						errMsg = AfaStrings.ErrorProjectingFeatures;
						result = rs;
					}
					else
					{
						rs = base.Parent.GeometryService.ProjectRecordSet(env, rs, num, spatialReference, outputSR);
						if (rs == null)
						{
							errMsg = AfaStrings.ErrorProjectingFeatures;
						}
						result = rs;
					}
				}
			}
			catch
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorProjectingFeatures;
				}
				throw;
			}
			return result;
		}

		private RecordSet QueryFeatures(string name, ref FIDSet fidset, LayerDescription activelayerdesc, bool useTrueCurves, ref string errMsg)
		{
			return null;
		}

		private bool ExtractAndDraw(AGSMapLayer thisLayer, AGSSubType thisSubtype, MSCFeatureClass fc, MapDescription mapdesc, ref SpatialReference inputSR, SpatialReference outputSR, EnvelopeN env, LayerDescription activelayerdesc, SpatialFilter spatialfilter, string defaultActiveLayerDesc)
		{
			if (this.ReportCheckCancel())
			{
				return false;
			}
			bool result;
			try
			{
				FIDSet fIDSet = this.BuildFIDSet(mapdesc.Name, activelayerdesc, spatialfilter);
				if (fIDSet == null)
				{
					this.ReportExportError(base.ErrorMessage, false);
					result = false;
				}
				else
				{
					int num = fIDSet.FIDArray.Length;
					if (num == 0)
					{
						this.ReportExportError(AfaStrings.NoFeaturesFound, false);
						result = true;
					}
					else
					{
						this.ReportExportStatus(num + AfaStrings.FeaturesFound);
						if (num > AGSMapService.MAX_RECOMMENDED)
						{
							string text = num + AfaStrings.FeaturesFound;
							text = text + "\r\n" + AfaStrings.CountExceedsRecommended + string.Format(" ({0})", AGSMapService.MAX_RECOMMENDED);
							if (!MessageUtil.ShowYesNo(text))
							{
								this.ReportForceCancel();
								result = false;
								return result;
							}
						}
						string text2 = "";
						RecordSet recordSet = this.QueryFeatures(mapdesc.Name, ref fIDSet, activelayerdesc, true, ref text2);
						if (string.IsNullOrEmpty(text2))
						{
							if (recordSet.Records.Length == 0)
							{
								this.ReportExportError(AfaStrings.NoFeaturesFound, false);
								result = true;
							}
							else
							{
								RecordSet drs = this.QueryFeatures(mapdesc.Name, ref fIDSet, activelayerdesc, false, ref text2);
								if (!string.IsNullOrEmpty(text2))
								{
									this.ReportExportError(text2, false);
									result = false;
								}
								else
								{
									ExportedFeaturesEventArgs exportedFeaturesEventArgs = new ExportedFeaturesEventArgs(base.Name, thisLayer, thisSubtype, fc, recordSet, drs, this.FindGeometryFieldIndex(recordSet));
									exportedFeaturesEventArgs.TypeFieldName = thisLayer.TypeIDField;
									this.ReportExportStatus(string.Format(AfaStrings.DrawingFeatureCount, recordSet.Records.Length, num));
									this.AddExtractedFeatures(this, exportedFeaturesEventArgs);
									int arg_1A3_0 = recordSet.Records.Length;
									result = true;
								}
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

		private void AddExtractedFeatures(object sender, ExportedFeaturesEventArgs e)
		{
			if (!string.IsNullOrEmpty(e.ErrorMessage))
			{
				return;
			}
			try
			{
				RecordSet records = e.records;
				RecordSet densifiedRecords = e.densifiedRecords;
				int geometryIndex = e.GeometryIndex;
				int num = -1;
				AGSMapLayer arg_2B_0 = e.Layer;
				if (records.Records.Length == 0)
				{
					this.ReportExportStatus(AfaStrings.NoFeaturesFound);
				}
				else
				{
					for (int i = 0; i < e.records.Fields.FieldArray.Length; i++)
					{
                        ArcGIS10Types.Field field = e.records.Fields.FieldArray[i];
						if (field.Name == "Elevation" && (field.Type == esriFieldType.esriFieldTypeDouble || field.Type == esriFieldType.esriFieldTypeInteger))
						{
							num = i;
							break;
						}
					}
					List<CadFeature> list = new List<CadFeature>();
					int num2 = 0;
					double defaultElevation = 0.0;
					Record[] records2 = records.Records;
					for (int j = 0; j < records2.Length; j++)
					{
						Record record = records2[j];
						Record record2 = densifiedRecords.Records[num2];
						Geometry geometry = record.Values[geometryIndex] as Geometry;
						if (geometry != null)
						{
							try
							{
								double num3;
								if (num != -1 && double.TryParse(record.Values[num].ToString(), out num3))
								{
									defaultElevation = num3;
								}
							}
							catch
							{
								defaultElevation = 0.0;
							}
							List<Entity> list2 = GIS2CAD.ToEntity((Geometry)record.Values[geometryIndex], (Geometry)record2.Values[geometryIndex], defaultElevation, ObjectId.Null);
							if (list2 != null && list2.Count > 0)
							{
								CadFeature cadFeature = new CadFeature();
								cadFeature.EntList = list2;
								cadFeature.Fields = CadField.ToCadFields(e.Layer.Fields, record, e.TypeFieldName);
								cadFeature.Fields = CadField.StripDefaults(e.CadFC.Fields, cadFeature.Fields);
								list.Add(cadFeature);
							}
						}
						num2++;
					}
					try
					{
						string text = e.Layer.Name + "_" + e.MapName;
						if (e.Subtype != null)
						{
							string str = text;
							text = e.Subtype.Name + "_" + str;
						}
						DocUtil.AddCadFeaturesToModelSpace(AfaDocData.ActiveDocData.Document, list, text);
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

		public bool AddService()
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
					aGSExportOptions.BoundingBox = extent;
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
					result = this.AddService(aGSExportOptions, this.GetDefaultMapLayers());
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

		public bool AddService(AGSExportOptions eo, List<AGSMapLayer> layers)
		{
			bool result;
			try
			{
				base.ErrorMessage = null;
				if (!this.InitializeMapService())
				{
					if (this.CommandEnded != null)
					{
						this.CommandEnded(this, EventArgs.Empty);
					}
					result = false;
				}
				else
				{
					eo.OutputFile = Utility.TemporaryFilePath();
					eo.AcadDocument = AfaDocData.ActiveDocData.Document;
					bool flag = this.ExportMapNow(eo, new ExportImageEventHandler(this.AddMapService), null);
					result = flag;
				}
			}
			catch (SystemException ex)
			{
				if (this.CommandEnded != null)
				{
					this.CommandEnded(this, EventArgs.Empty);
				}
				ErrorReport.ShowErrorMessage(ex.Message);
				result = false;
			}
			return result;
		}

		private void AddMapService(object sender, ExportedImageEventArgs e)
		{
			try
			{
				if (!string.IsNullOrEmpty(e.ErrorMessage))
				{
					ErrorReport.ShowErrorMessage(e.ErrorMessage);
				}
				else if (e.Points != null && !string.IsNullOrEmpty(e.ExportOptions.OutputFile))
				{
					Point3d point3d = new Point3d(e.Points[0].X, e.Points[0].Y, 0.0);
					Point3d point3d2 = new Point3d(e.Points[1].X, e.Points[1].Y, 0.0);
					Point3d point3d3 = new Point3d(e.Points[2].X, e.Points[2].Y, 0.0);
					Point3d p = new Point3d(e.Points[1].X, e.Points[2].Y, 0.0);
					Vector3d v = point3d2 - point3d;
					Vector3d v2 = point3d3 - point3d;
					MSCMapService mSCMapService = null;
					try
					{
						Document document = AfaDocData.ActiveDocData.Document;
						string outputFile = e.ExportOptions.OutputFile;
						string suggestedName = "ESRI_" + e.MapName;
						byte transparency = e.ExportOptions.Transparency;
						ObjectId objectId = DocUtil.DefineRasterImage(document, outputFile, point3d, v, v2, suggestedName, transparency);
						if (!objectId.IsNull)
						{
							DocUtil.SetEntityDisableUndo(AfaDocData.ActiveDocData.Document, objectId, true);
							e.ExportOptions.BoundingBox = new Extent(point3d, p);
							e.ExportOptions.BoundingBox.SpatialReference = e.ExportOptions.OutputWKT;
							mSCMapService = MSCDataset.AddMapService((AGSMapService)e.MapService, objectId, null, e.ExportOptions);
							if (mSCMapService != null)
							{
								DocUtil.AttachHyperlink(document, objectId, mSCMapService.Name, mSCMapService.RestEndpoint);
							}
						}
					}
					catch (SystemException)
					{
						base.ErrorMessage = AfaStrings.ErrorDefiningRasterInDrawing;
					}
					if (mSCMapService != null)
					{
						mSCMapService.UpdateToCurrentView();
						mSCMapService.CheckForUpdates();
					}
				}
				else
				{
					base.ErrorMessage = AfaStrings.ErrorGeneratingImage;
				}
			}
			catch
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorGeneratingImage;
				}
				throw;
			}
		}

		private bool InitializeMapService()
		{
			if (!base.IsValid || this._mapservice == null)
			{
				base.IsValid = false;
				this._mapservice = new MapServerProxy();
				this.ServiceURL = base.Parent.Soap_URL + "/" + base.FullName + "/MapServer";
				this._mapservice.Url = base.Parent.AppendToken(this.ServiceURL);
				this._mapservice.Proxy = WebRequest.DefaultWebProxy;
				this._mapservice.Credentials = base.Parent.Credentials;
				if (this._mapservice.Credentials == null)
				{
					this._mapservice.Credentials = CredentialCache.DefaultNetworkCredentials;
					this._mapservice.UseDefaultCredentials = true;
				}
				base.ErrorMessage = "";
				string text = "";
				try
				{
					text = this._mapservice.GetDefaultMapName();
					if (string.IsNullOrEmpty(text))
					{
						bool result = false;
						return result;
					}
				}
				catch (WebException ex)
				{
					bool result;
					if (base.Challenge())
					{
						this._mapservice.Credentials = base.Parent.Credentials;
						if (this._mapservice.Credentials == null)
						{
							this._mapservice.Credentials = CredentialCache.DefaultNetworkCredentials;
							this._mapservice.UseDefaultCredentials = true;
						}
						this._mapservice.Proxy = WebRequest.DefaultWebProxy;
						try
						{
							text = this._mapservice.GetDefaultMapName();
							if (string.IsNullOrEmpty(text))
							{
								result = false;
								return result;
							}
							goto IL_18F;
						}
						catch
						{
							base.ErrorMessage = ex.Message;
							result = false;
							return result;
						}
						goto IL_176;
						IL_18F:
						goto IL_191;
					}
					IL_176:
					base.ErrorMessage = base.Parent.ErrorMessage;
					result = false;
					return result;
				}
	    		IL_191:
				try
				{
					this._mapinfo = this._mapservice.GetServerInfo(text);
					this.IsFixedScale = this._mapservice.IsFixedScaleMap(text);
					MapDescription defaultMapDescription = this._mapinfo.DefaultMapDescription;
					try
					{
						EnvelopeN envelopeN = (EnvelopeN)defaultMapDescription.MapArea.Extent;
						base.Properties["WKT"] = envelopeN.SpatialReference.WKT;
					}
					catch
					{
						base.ErrorMessage = AfaStrings.UndefinedExtent;
						bool result = false;
						return result;
					}
				}
				catch
				{
					base.ErrorMessage = AfaStrings.UnableToRetrieveServiceProperties;
					bool result = false;
					return result;
				}
			}
			base.IsValid = true;
			return true;
		}

		private void RefreshProperties()
		{
			try
			{
				if (base.Parent == null)
				{
					base.Properties.Add(AfaStrings.Error, AfaStrings.UnableToRetrieveServiceProperties);
					base.IsValid = false;
				}
				else if (base.Parent.ConnectionFailed)
				{
					base.Properties.Add(AfaStrings.Error, AfaStrings.UnableToRetrieveServiceProperties);
					base.IsValid = false;
				}
				else
				{
					string arg = base.Parent.URL + "/" + base.FullName + "/MapServer";
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendFormat("?f={0}", "json");
					IDictionary<string, object> dictionary = base.Parent.MakeDictionaryRequest(arg + stringBuilder);
					if (dictionary != null)
					{
						string errorMessage;
						if (!base.Parent.CheckResultsForError(dictionary, out errorMessage))
						{
							this.PopulateLayers(dictionary);
							this.PopulateProperties(dictionary);
							base.IsValid = true;
						}
						else
						{
							base.ErrorMessage = errorMessage;
							base.IsValid = false;
						}
					}
					else
					{
						base.Properties.Add(AfaStrings.Error, AfaStrings.UnableToRetrieveServiceProperties);
						base.IsValid = false;
					}
				}
			}
			catch
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errRefreshingProperties;
				}
				throw;
			}
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
						IDictionary<string, object> dictionary = current as IDictionary<string, object>;
						object obj2;
						if (dictionary.TryGetValue("id", out obj2))
						{
							AGSMapLayer aGSMapLayer = new AGSMapLayer((int)obj2, this);
							if (dictionary.TryGetValue("name", out obj2))
							{
								aGSMapLayer.Name = obj2.ToString();
							}
							if (dictionary.TryGetValue("parentLayerID", out obj2))
							{
								aGSMapLayer.parentLayerId = (int)obj2;
							}
							if (dictionary.TryGetValue("defaultVisibility", out obj2))
							{
								aGSMapLayer.DefaultVisibility = (bool)obj2;
								aGSMapLayer.IsVisible = aGSMapLayer.DefaultVisibility;
							}
							if (dictionary.TryGetValue("subLayerIds", out obj2) && obj2 != null)
							{
								IList<object> list2 = obj2 as IList<object>;
								foreach (object current2 in list2)
								{
									aGSMapLayer.ChildLayerIds.Add((int)current2);
								}
							}
							if (aGSMapLayer.GeometryType != "esriGeometryMultipoint")
							{
								this.MapLayers.Add(aGSMapLayer.Id, aGSMapLayer);
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

		public new bool SupportsDisconnect()
		{
			return !base.Parent.URL.ToLower().Contains("services.arcgisonline.com");
		}

		private void PopulateProperties(IDictionary<string, object> results)
		{
			base.Properties.Add("Type", "Map Server");
			this.SupportsMap = false;
			this.SupportsQuery = false;
			this.SupportsData = false;
			object obj;
			if (results.TryGetValue("thumbnail", out obj))
			{
				obj.ToString();
			}
			if (results.TryGetValue("singleFusedMapCache", out obj))
			{
				base.Properties.Add("Single Fused Map Cache", obj);
			}
			if (results.TryGetValue("capabilities", out obj))
			{
				string text = obj as string;
				base.Properties.Add("Capabilities", text);
				string[] array = text.Split(new char[]
				{
					','
				});
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string a = array2[i];
					if (a == "Map")
					{
						this.SupportsMap = true;
					}
					if (a == "Query")
					{
						this.SupportsQuery = true;
					}
					if (a == "Data")
					{
						this.SupportsData = false;
					}
				}
			}
			if (results.TryGetValue("serviceDescription", out obj))
			{
				string value = obj as string;
				if (!string.IsNullOrEmpty(value))
				{
					base.Properties.Add("Service Description", obj);
				}
			}
			if (results.TryGetValue("mapName", out obj))
			{
				string value2 = obj as string;
				if (!string.IsNullOrEmpty(value2))
				{
					base.Properties.Add("Map Name", obj);
				}
			}
			if (results.TryGetValue("description", out obj))
			{
				string value3 = obj as string;
				if (!string.IsNullOrEmpty(value3))
				{
					base.Properties.Add("Description", obj);
				}
			}
			if (results.TryGetValue("copyrightText", out obj))
			{
				string value4 = obj as string;
				if (!string.IsNullOrEmpty(value4))
				{
					base.Properties.Add("Copyright", obj);
				}
			}
			if (results.TryGetValue("documentInfo", out obj))
			{
				IDictionary<string, object> dictionary = obj as IDictionary<string, object>;
				foreach (KeyValuePair<string, object> current in dictionary)
				{
					if (current.Value != null)
					{
						string value5 = current.Value as string;
						if (!string.IsNullOrEmpty(value5))
						{
							base.Properties.Add(current.Key, current.Value);
						}
					}
				}
			}
			if (results.TryGetValue("spatialReference", out obj))
			{
				IDictionary<string, object> dictionary2 = obj as IDictionary<string, object>;
				if (dictionary2.ContainsKey("wkid"))
				{
					object value6;
					dictionary2.TryGetValue("wkid", out value6);
					base.Properties.Add("WKID", value6);
				}
				if (dictionary2.ContainsKey("wkt"))
				{
					object value7;
					dictionary2.TryGetValue("wkt", out value7);
					base.Properties.Add("WKT", value7);
				}
			}
			if (results.TryGetValue("units", out obj))
			{
				base.Properties.Add("Units", obj);
			}
			if (results.TryGetValue("initialExtent", out obj))
			{
				IDictionary<string, object> dict = obj as IDictionary<string, object>;
				Extent extent = new Extent(dict);
				if (extent.IsValid())
				{
					base.Properties.Add("Initial Extent", extent);
				}
			}
			if (results.TryGetValue("fullExtent", out obj))
			{
				IDictionary<string, object> dict2 = obj as IDictionary<string, object>;
				Extent extent2 = new Extent(dict2);
				if (extent2.IsValid())
				{
					base.Properties.Add("Full Extent", extent2);
				}
			}
			if (results.TryGetValue("tileInfo", out obj))
			{
				this.HasTiles = true;
			}
			if (results.TryGetValue("supportedImageFormatTypes", out obj))
			{
				StringList stringList = new StringList();
				string text2 = obj as string;
				string[] array3 = text2.Split(new char[]
				{
					','
				});
				string[] array4 = array3;
				for (int j = 0; j < array4.Length; j++)
				{
					string text3 = array4[j];
					if (DocUtil.CADSupportedImageFormats.Contains(text3.ToUpper()))
					{
						stringList.Add(text3);
					}
				}
				base.SupportedImageTypes = stringList;
				return;
			}
			base.SupportedImageTypes = new StringList();
			base.SupportedImageTypes.Add("PNG");
		}

		public static esriImageFormat ConvertImageFormat(string ImageFormat)
		{
			if (ImageFormat == "PNG")
			{
				return esriImageFormat.esriImagePNG;
			}
			if (ImageFormat == "PNG24")
			{
				return esriImageFormat.esriImagePNG24;
			}
			if (ImageFormat == "AI")
			{
				return esriImageFormat.esriImageAI;
			}
			if (ImageFormat == "BMP")
			{
				return esriImageFormat.esriImageBMP;
			}
			if (ImageFormat == "DIB")
			{
				return esriImageFormat.esriImageDIB;
			}
			if (ImageFormat == "EMF")
			{
				return esriImageFormat.esriImageEMF;
			}
			if (ImageFormat == "GIF")
			{
				return esriImageFormat.esriImageGIF;
			}
			if (ImageFormat == "JPG")
			{
				return esriImageFormat.esriImageJPG;
			}
			if (ImageFormat == "JPGPNG")
			{
				return esriImageFormat.esriImageJPGPNG;
			}
			if (ImageFormat == "PDF")
			{
				return esriImageFormat.esriImagePDF;
			}
			if (ImageFormat == "PNG32")
			{
				return esriImageFormat.esriImagePNG32;
			}
			if (ImageFormat == "PS")
			{
				return esriImageFormat.esriImagePS;
			}
			if (ImageFormat == "SVG")
			{
				return esriImageFormat.esriImageSVG;
			}
			if (ImageFormat == "TIFF")
			{
				return esriImageFormat.esriImageTIFF;
			}
			return esriImageFormat.esriImagePNG;
		}

		public AGSMapLayer FindMapLayer(int layerID)
		{
			using (IEnumerator<object> enumerator = this.MapLayers.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					AGSMapLayer aGSMapLayer = (AGSMapLayer)enumerator.Current;
					if (aGSMapLayer.Id == layerID)
					{
						return aGSMapLayer;
					}
				}
			}
			return null;
		}

		public string ReplaceQuotes(string strToEsc)
		{
			if (strToEsc.IndexOf("\"") > -1)
			{
				strToEsc = strToEsc.Replace("\"", "\\\"");
			}
			return strToEsc;
		}

		public string HTMLEncodeSpecialChars(string text)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];
				if (c > '\u007f')
				{
					stringBuilder.Append(string.Format("&#{0};", (int)c));
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		public string GenerateMapLayerGeometryReport(int layerID)
		{
			if (!this.InitializeMapService())
			{
				return "";
			}
			MapDescription arg_19_0 = this._mapinfo.DefaultMapDescription;
			string defaultMapName = this._mapservice.GetDefaultMapName();
			new QueryFilter();
			AGSMapLayer lyr = this.MapLayers[layerID] as AGSMapLayer;
			FIDSet fIDSet = this._mapservice.QueryFeatureIDs(defaultMapName, layerID, null);
			RecordSet recordSet = null;
			try
			{
				recordSet = this._mapservice.QueryFeatureData(defaultMapName, layerID, null);
			}
			catch
			{
				return "";
			}
			int arg_7B_0 = recordSet.Records.Length;
			int arg_7A_0 = fIDSet.FIDArray.Length;
			return AGSMapService.GenerateGeometryReport(recordSet, lyr);
		}

		public static string GenerateGeometryReport(RecordSet rs, AGSMapLayer lyr)
		{
			string text = "";
			int num = rs.Fields.FieldArray.Length;
			int num2 = -1;
			int num3 = -1;
			int num4 = -1;
			for (int i = 0; i < num; i++)
			{
                ArcGIS10Types.Field field = rs.Fields.FieldArray[i];
				if (field.Type == esriFieldType.esriFieldTypeOID)
				{
					text += field.Name;
					num4 = i;
				}
				else if (field.Name == lyr.DisplayField)
				{
					text += string.Format(", {0}", field.Name);
					num3 = i;
				}
				else if (field.Type == esriFieldType.esriFieldTypeGeometry)
				{
					text += string.Format(", {0}", field.Name);
					num2 = i;
				}
			}
			Record[] records = rs.Records;
			for (int j = 0; j < records.Length; j++)
			{
				Record record = records[j];
				StringBuilder stringBuilder = new StringBuilder();
				if (num4 >= 0)
				{
					stringBuilder.AppendFormat("\n{0}", record.Values[num4]);
				}
				if (num3 >= 0)
				{
					stringBuilder.AppendFormat("; {0}", record.Values[num3]);
				}
				if (num2 >= 0)
				{
					if (lyr.GeometryType == "esriGeometryPoint")
					{
						PointN srcPt = record.Values[num2] as PointN;
						AGSPoint arg = new AGSPoint(srcPt);
						stringBuilder.AppendFormat("; Point: {0}", arg);
					}
					else if (lyr.GeometryType == "esriGeometryMultipoint")
					{
						MultipointN srcMp = record.Values[num2] as MultipointN;
						AGSMultipoint arg2 = new AGSMultipoint(srcMp);
						stringBuilder.AppendFormat("; Multipoint: {0}", arg2);
					}
					else if (lyr.GeometryType == "esriGeometryPolyline")
					{
						PolylineN srcPLine = record.Values[num2] as PolylineN;
						AGSPolyline arg3 = new AGSPolyline(srcPLine);
						stringBuilder.AppendFormat("; Polyline: {0}", arg3);
					}
					else if (lyr.GeometryType == "esriGeometryPolygon")
					{
						PolygonN srcPoly = record.Values[num2] as PolygonN;
						AGSPolygon arg4 = new AGSPolygon(srcPoly);
						stringBuilder.AppendFormat("; Polygon: {0}", arg4);
					}
					else if (lyr.GeometryType == "esriGeometryEnvelope")
					{
						object arg_224_0 = record.Values[num2];
						stringBuilder.AppendFormat("; Envelope: {0}", record.Values[num2].ToString());
					}
					else
					{
						stringBuilder.AppendFormat("; {0}: {1}", lyr.GeometryType, record.Values[num2].ToString());
					}
				}
				text += stringBuilder;
			}
			return text;
		}

		public bool Identify(AGSExportOptions eo, Extent ext, List<int> mapLayers)
		{
			bool result;
			try
			{
				base.ErrorMessage = null;
				if (!this.SupportsQuery)
				{
					base.ErrorMessage = AfaStrings.QueryNotSupported;
					result = false;
				}
				else if (!this.InitializeMapService())
				{
					result = false;
				}
				else
				{
					ImageDescription imageDescription = this.InitializeImageDescription(eo);
					MapDescription mapDescription = (MapDescription)Utility.CloneObject(this._mapinfo.DefaultMapDescription);
					new List<SpatialReference>();
					EnvelopeN envelopeN = this._mapinfo.FullExtent as EnvelopeN;
					envelopeN.SpatialReference = AGSSpatialReference.SpRefFromString(ext.SpatialReference);
					envelopeN.XMin = ext.XMin.Value;
					envelopeN.XMax = ext.XMax.Value;
					envelopeN.YMin = ext.YMin.Value;
					envelopeN.YMax = ext.YMax.Value;
					if (envelopeN.SpatialReference == null)
					{
						EnvelopeN envelopeN2 = (EnvelopeN)this._mapinfo.FullExtent;
						envelopeN.SpatialReference = envelopeN2.SpatialReference;
					}
					mapDescription.MapArea.Extent = envelopeN;
					mapDescription.SpatialReference = envelopeN.SpatialReference;
					this.outputSR = AGSSpatialReference.SpRefFromString(eo.OutputWKT);
					if (!this.IsFixedScale && this.outputSR != null)
					{
						mapDescription.SpatialReference = this.outputSR;
					}
					List<int> list = new List<int>();
					LayerDescription[] layerDescriptions = mapDescription.LayerDescriptions;
					for (int i = 0; i < layerDescriptions.Length; i++)
					{
						LayerDescription layerDescription = layerDescriptions[i];
						if (this.MapLayers.ContainsKey(layerDescription.LayerID))
						{
							AGSMapLayer aGSMapLayer = this.MapLayers[layerDescription.LayerID] as AGSMapLayer;
							if (aGSMapLayer.IsVisible)
							{
								list.Add(layerDescription.LayerID);
							}
						}
					}
					if (list.Count == 0)
					{
						result = false;
					}
					else
					{
						int tolerance = 3;
						esriIdentifyOption identifyOption = esriIdentifyOption.esriIdentifyAllLayers;
						foreach (int current in list)
						{
							int[] layerIDs = new int[]
							{
								current
							};
							MapServerIdentifyResult[] array;
							try
							{
								array = this._mapservice.Identify(mapDescription, imageDescription.ImageDisplay, envelopeN, tolerance, identifyOption, layerIDs);
							}
							catch
							{
                                System.Data.DataTable identifyResults = new System.Data.DataTable();
								AGSMapLayer aGSMapLayer2 = this.FindMapLayer(current);
								aGSMapLayer2.IdentifyResults = identifyResults;
								continue;
							}
                            System.Data.DataTable dataTable = new System.Data.DataTable();
							try
							{
								if (array != null && array.Length != 0)
								{
									MapServerIdentifyResult mapServerIdentifyResult = array[0];
									dataTable.Columns.Add("Feature ID", typeof(int));
									PropertySetProperty[] propertyArray = mapServerIdentifyResult.Properties.PropertyArray;
									for (int j = 0; j < propertyArray.Length; j++)
									{
										PropertySetProperty propertySetProperty = propertyArray[j];
										dataTable.Columns.Add(propertySetProperty.Key);
										dataTable.Columns[propertySetProperty.Key].ReadOnly = true;
									}
									MapServerIdentifyResult[] array2 = array;
									for (int k = 0; k < array2.Length; k++)
									{
										MapServerIdentifyResult mapServerIdentifyResult2 = array2[k];
										DataRow dataRow = dataTable.NewRow();
										if (mapServerIdentifyResult2.FeatureIDSpecified)
										{
											dataRow["Feature ID"] = mapServerIdentifyResult2.FeatureID;
										}
										PropertySetProperty[] propertyArray2 = mapServerIdentifyResult2.Properties.PropertyArray;
										for (int l = 0; l < propertyArray2.Length; l++)
										{
											PropertySetProperty propertySetProperty2 = propertyArray2[l];
											dataRow[propertySetProperty2.Key] = propertySetProperty2.Value;
										}
										dataTable.Rows.Add(dataRow);
									}
								}
								AGSMapLayer aGSMapLayer3 = this.FindMapLayer(current);
								aGSMapLayer3.IdentifyResults = dataTable;
							}
							catch
							{
								AGSMapLayer aGSMapLayer4 = this.FindMapLayer(current);
								aGSMapLayer4.IdentifyResults = dataTable;
							}
						}
						result = true;
					}
				}
			}
			catch
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errIdentifyingMapFeatures;
				}
				throw;
			}
			return result;
		}
	}
}
