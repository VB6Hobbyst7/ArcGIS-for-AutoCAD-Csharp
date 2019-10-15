using AFA.Resources;
using AFA.UI;
using ArcGIS10Types;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Input;

namespace AFA
{
	public class AGSImageService : AGSRasterService, IAGSImageService, IAGSService, IAGSObject, ILongProcessCommand
	{
		public event CommandStartedEventHandler CommandStarted;

		public event CommandEndedEventHandler CommandEnded;

		public event CommandProgressEventHandler CommandProgress;

		public event CommandProgressUpdateValuesEventHandler CommandUpdateProgressValues;

		public IDictionary<string, AGSField> Fields
		{
			get;
			set;
		}

		public string Version
		{
			get;
			set;
		}

		public IList<string> SupportedPixelTypes
		{
			get
			{
				return new List<string>
				{
					"Default",
					"C128",
					"C64",
					"F32",
					"S16",
					"S32",
					"S8",
					"U1",
					"U16",
					"U32",
					"U4",
					"U8",
					"UNKNOWN"
				};
			}
		}

		public IList<string> SupportedInterpolationTypes
		{
			get
			{
				return new List<string>
				{
					"Default",
					"RSP_BilinearInterpolation",
					"RSP_CubicConvolution",
					"RSP_Majority",
					"RSP_NearestNeighbor"
				};
			}
		}

		public AGSImageService(string name, AGSConnection parent) : base(name, parent)
		{
			base.Properties = new Dictionary<string, object>();
			this.Fields = new Dictionary<string, AGSField>();
			this.Version = parent.Version;
			this.RefreshProperties();
			this.InitializeExportProperties();
		}

		public bool AddService(AGSExportOptions eo)
		{
			bool result;
			try
			{
				base.ErrorMessage = null;
				this.InitializeImageService();
				eo.OutputFile = Utility.TemporaryFilePath();
				eo.AcadDocument = AfaDocData.ActiveDocData.Document;
				Size size = Application.ToSystemDrawingSize(eo.AcadDocument.Window.DeviceIndependentSize);
				eo.Width = size.Width;
				eo.Height = size.Height;
				bool flag = this.ExportMapNow(eo, new ExportImageEventHandler(this.AddImageService), null);
				result = flag;
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

		public override bool ExportMapNow(AGSExportOptions eo, ExportImageEventHandler action, AGSExportOptions cancelOptions)
		{
			bool result;
			try
			{
				base.ErrorMessage = "";
				string arg = base.Parent.URL + "/" + base.FullName + "/ImageServer/exportImage";
				if (eo == null)
				{
					eo = this.ExportOptions;
				}
				StringBuilder arg2 = this.BuildURIRequest(eo);
				IDictionary<string, object> dictionary = base.Parent.MakeDictionaryRequest(arg + arg2);
				if (dictionary != null)
				{
					SpatialReference spatialReference = null;
					if (dictionary != null)
					{
						object obj;
						if (!dictionary.TryGetValue("href", out obj))
						{
							string text = AfaStrings.Error;
							if (dictionary.ContainsKey("error"))
							{
								IDictionary<string, object> dictionary2 = dictionary["error"] as IDictionary<string, object>;
								object obj2;
								if (dictionary2.ContainsKey("details") && dictionary2.TryGetValue("details", out obj2))
								{
									object[] array = obj2 as object[];
									if (array != null)
									{
										text = text + " - " + array[0].ToString();
									}
								}
							}
							Mouse.OverrideCursor = null;
							base.ErrorMessage = text;
						}
						if (string.IsNullOrEmpty(base.ErrorMessage))
						{
							string text2 = obj as string;
							if (text2.Length > 0)
							{
								ExportedImage exportedImage = new ExportedImage(this, text2);
								if (dictionary.TryGetValue("width", out obj))
								{
									exportedImage.Properties.Add("width", obj);
								}
								if (dictionary.TryGetValue("height", out obj))
								{
									exportedImage.Properties.Add("height", obj);
								}
								if (dictionary.TryGetValue("scale", out obj))
								{
									exportedImage.Properties.Add("scale", obj);
								}
								if (dictionary.TryGetValue("extent", out obj))
								{
									IDictionary<string, object> dictionary3 = obj as IDictionary<string, object>;
									object obj3;
									if (dictionary3.TryGetValue("xmin", out obj3))
									{
										exportedImage.Properties.Add("xmin", obj3);
									}
									if (dictionary3.TryGetValue("xmax", out obj3))
									{
										exportedImage.Properties.Add("xmax", obj3);
									}
									if (dictionary3.TryGetValue("ymax", out obj3))
									{
										exportedImage.Properties.Add("ymax", obj3);
									}
									if (dictionary3.TryGetValue("ymin", out obj3))
									{
										exportedImage.Properties.Add("ymin", obj3);
									}
									if (dictionary3.TryGetValue("spatialReference", out obj3))
									{
										IDictionary<string, object> dictionary4 = obj3 as IDictionary<string, object>;
										object obj4;
										if (dictionary4.TryGetValue("wkid", out obj4))
										{
											exportedImage.Properties.Add("spatialReference", obj4);
											int num = int.Parse(obj4.ToString());
											spatialReference = AGSSpatialReference.SpRefFromWKID(ref num);
										}
										else if (dictionary4.TryGetValue("wkt", out obj4))
										{
											exportedImage.Properties.Add("spatialReference", obj4);
											string text3 = obj4.ToString();
											spatialReference = AGSSpatialReference.SpRefFromWKT(ref text3);
										}
									}
									PointN pointN = new PointN();
									if (spatialReference != null)
									{
										pointN.SpatialReference = spatialReference;
									}
									double num2 = double.Parse(exportedImage.Properties["xmin"].ToString());
									double num3 = double.Parse(exportedImage.Properties["ymin"].ToString());
									double num4 = double.Parse(exportedImage.Properties["xmax"].ToString());
									double num5 = double.Parse(exportedImage.Properties["ymax"].ToString());
									pointN.X = num2;
									pointN.Y = num3;
									pointN.Z = 0.0;
									PointN[] points = new PointN[]
									{
										pointN,
										new PointN
										{
											SpatialReference = spatialReference,
											X = num4,
											Y = num3,
											Z = 0.0
										},
										new PointN
										{
											SpatialReference = spatialReference,
											X = num2,
											Y = num5,
											Z = 0.0
										}
									};
									try
									{
										if (File.Exists(eo.OutputFile))
										{
											File.Delete(eo.OutputFile);
											if (App.TempFiles.Contains(eo.OutputFile))
											{
												App.TempFiles.Remove(eo.OutputFile);
											}
										}
										if (!base.Parent.DownloadFile(text2, eo.OutputFile))
										{
											eo.OutputFile = text2;
											if (string.IsNullOrEmpty(eo.OutputFile))
											{
												ErrorReport.ShowErrorMessage(AfaStrings.ErrorCreatingOutputFile);
											}
										}
									}
									catch
									{
										eo.OutputFile = text2;
										if (string.IsNullOrEmpty(eo.OutputFile))
										{
											base.ErrorMessage = AfaStrings.ErrorCreatingOutputFile;
											result = false;
											return result;
										}
									}
									finally
									{
										base.IsValid = true;
										if (action != null)
										{
											action(this, new ExportedImageEventArgs(this, eo, points));
										}
									}
								}
							}
						}
					}
				}
				result = string.IsNullOrEmpty(base.ErrorMessage);
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

		private void InitializeImageService()
		{
			try
			{
				bool arg_06_0 = base.IsValid;
			}
			catch (SystemException ex)
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errInitializingImageService;
				}
				base.IsValid = false;
				throw ex;
			}
			catch
			{
				if (string.IsNullOrEmpty(base.ErrorMessage))
				{
					base.ErrorMessage = AfaStrings.ErrorEncounteredIn + AfaStrings.errInitializingImageService;
				}
				base.IsValid = false;
			}
		}

		private void AddImageService(object sender, ExportedImageEventArgs e)
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
					ObjectId objectId = DocUtil.DefineRasterImage(AfaDocData.ActiveDocData.Document, e.ExportOptions.OutputFile, point3d, v, v2, "ESRI_" + e.MapName, e.ExportOptions.Transparency);
					if (!objectId.IsNull)
					{
						DocUtil.SetEntityDisableUndo(AfaDocData.ActiveDocData.Document, objectId, true);
						e.ExportOptions.BoundingBox = new Extent(point3d, p);
						e.ExportOptions.BoundingBox.SpatialReference = e.ExportOptions.OutputWKT;
						MSCImageService mSCImageService = MSCDataset.AddImageService((AGSImageService)e.MapService, objectId, e.ExportOptions);
						if (mSCImageService != null)
						{
							DocUtil.AttachHyperlink(AfaDocData.ActiveDocData.Document, objectId, mSCImageService.Name, mSCImageService.RestEndpoint);
							mSCImageService.UpdateToCurrentView();
							mSCImageService.CheckForUpdates();
						}
					}
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

		public static AGSImageService BuildImageServiceFromURL(string Name, string URL)
		{
			AGSImageService aGSImageService = null;
			AGSImageService result;
			try
			{
				string text = AGSConnection.BuildConnectionURL(URL);
				if (string.IsNullOrEmpty(text))
				{
					result = aGSImageService;
				}
				else
				{
					AGSConnection aGSConnection = AGSConnection.ReestablishConnection(text, null, "");
					if (aGSConnection != null)
					{
						try
						{
							string name = AGSImageService.BuildFullNameFromURL(URL);
							if (string.IsNullOrEmpty(Name))
							{
								Name = AGSImageService.BuildNameFromURL(URL);
							}
							aGSImageService = new AGSImageService(name, aGSConnection);
							aGSImageService.Name = Name;
							if (aGSImageService.IsValid)
							{
								result = aGSImageService;
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
					else if (!(text2.ToLower() == "imageserver/") && !(text2.ToLower() == "imageserver") && flag)
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

		public static string BuildFullNameFromURL(string serviceURL)
		{
			string result;
			try
			{
				Uri uri = new Uri(serviceURL);
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
					else if (!(text2.ToLower() == "imageserver/") && !(text2.ToLower() == "imageserver") && flag)
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

		public override ITask GetExportTask(AGSExportOptions eo, HiddenUpdateForm form)
		{
			string arg = base.Parent.URL + "/" + base.FullName + "/ImageServer/exportImage";
			StringBuilder arg2 = this.BuildURIRequest(eo);
			string requestURL = arg + arg2;
			return new UpdateRasterImageTask(base.Parent)
			{
				RequestURL = requestURL,
				UpdateForm = form,
				OutputFile = eo.OutputFile,
				OutputSpatialReference = AGSSpatialReference.SpRefFromString(eo.OutputWKT)
			};
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

		private string BuildRequestSpatialRefString(string tag, string spRefString)
		{
			string result;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (!string.IsNullOrEmpty(spRefString) && spRefString != "<Unknown>" && spRefString != "<Undefined>")
				{
					if (AGSImageService.IsInteger(spRefString))
					{
						stringBuilder.AppendFormat("&{0}={1}", tag, spRefString);
					}
					else
					{
						stringBuilder.AppendFormat("&{0}={{\"wkt\" : \"{1}\"}}", tag, this.ReplaceQuotes(spRefString));
					}
				}
				string text = "";
				text += stringBuilder;
				result = text;
			}
			catch
			{
				result = "";
			}
			return result;
		}

		public StringBuilder BuildURIRequest(AGSExportOptions eo)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("?f={0}", "json");
			stringBuilder.AppendFormat("&size={0},{1}", eo.Width, eo.Height);
			stringBuilder.AppendFormat("&format={0}", eo.Format.ToLower());
			stringBuilder.AppendFormat("&compressionQuality={0}", eo.Quality);
			stringBuilder.AppendFormat("&compression={0}", eo.TransCompression);
			if (eo.Interpolation != "Default")
			{
				stringBuilder.AppendFormat("&interpoloation={0}", eo.Interpolation);
			}
			if (string.IsNullOrEmpty(eo.OutputWKT))
			{
				eo.OutputWKT = this.GetWKT();
			}
			stringBuilder.Append(this.BuildRequestSpatialRefString("imageSR", eo.OutputWKT));
			if (eo.BoundingBox == null)
			{
				if (this.ExportOptions.BoundingBox == null)
				{
					Extent extent = null;
					if (base.Properties.ContainsKey("Full Extent"))
					{
						extent = (base.Properties["Full Extent"] as Extent);
						if (extent.IsValid())
						{
							eo.BoundingBox = extent;
						}
					}
					else if (extent == null && base.Properties.ContainsKey("Initial Extent"))
					{
						extent = (base.Properties["Initial Extent"] as Extent);
						if (extent.IsValid())
						{
							eo.BoundingBox = extent;
						}
					}
					else if (base.Properties.ContainsKey("Extent"))
					{
						extent = (base.Properties["Extent"] as Extent);
						if (extent.IsValid())
						{
							eo.BoundingBox = extent;
						}
					}
				}
				eo.BoundingBox = this.ExportOptions.BoundingBox;
			}
			if (eo.BoundingBox != null && eo.BoundingBox.IsValid())
			{
				stringBuilder.AppendFormat("&bbox={0},{1},{2},{3}", new object[]
				{
					eo.BoundingBox.XMin,
					eo.BoundingBox.YMin,
					eo.BoundingBox.XMax,
					eo.BoundingBox.YMax
				});
				stringBuilder.Append(this.BuildRequestSpatialRefString("bBoxSR", eo.BoundingBox.SpatialReference));
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			stringBuilder2.Append("&mosaicRule={\"mosaicMethod\" : \"");
			stringBuilder2.Append(eo.MosaicMethod);
			stringBuilder2.Append("\"");
			string arg = "true";
			if (eo.MosaicOperator != "<None>")
			{
				stringBuilder2.AppendFormat(", \"mosaicOperation\" : \"{0}\"", eo.MosaicOperator);
			}
			if (eo.MosaicMethod == "esriMosaicLockRaster" && !string.IsNullOrEmpty(eo.LockRasterID))
			{
				stringBuilder2.AppendFormat(", \"lockRasterIds\" : [{0}]", eo.LockRasterID);
			}
			if (eo.MosaicMethod == "esriMosaicAttribute" && !string.IsNullOrEmpty(eo.OrderField))
			{
				stringBuilder2.AppendFormat(", \"sortField\" : \"{0}\"", eo.OrderField);
				if (!string.IsNullOrEmpty(eo.OrderBaseValue))
				{
					stringBuilder2.AppendFormat(", \"sortValue\" : \"{0}\"", eo.OrderBaseValue);
				}
			}
			if (!eo.Ascending)
			{
				arg = "false";
			}
			stringBuilder2.AppendFormat(", \"ascending\" : {0}", arg);
			stringBuilder2.Append("}");
			stringBuilder.AppendFormat("{0}", stringBuilder2.ToString());
			return stringBuilder;
		}

		public void ExportImage(AGSExportOptions eo, ExportImageEventHandler action)
		{
			try
			{
				string arg = base.Parent.URL + "/" + base.FullName + "/ImageServer/exportImage";
				StringBuilder arg2 = this.BuildURIRequest(eo);
				Mouse.OverrideCursor = Cursors.Wait;
				IDictionary<string, object> dictionary = base.Parent.MakeDictionaryRequest(arg + arg2);
				if (dictionary == null)
				{
					Mouse.OverrideCursor = null;
					if (string.IsNullOrEmpty(base.Parent.ErrorMessage))
					{
						ErrorReport.ShowErrorMessage(AfaStrings.AnUnexpectedErrorOccured);
					}
					else
					{
						ErrorReport.ShowErrorMessage(base.Parent.ErrorMessage);
					}
					return;
				}
				SpatialReference spatialReference = null;
				if (dictionary != null)
				{
					object obj;
					if (!dictionary.TryGetValue("href", out obj))
					{
						string text = AfaStrings.Error;
						if (dictionary.ContainsKey("error"))
						{
							IDictionary<string, object> dictionary2 = dictionary["error"] as IDictionary<string, object>;
							if (dictionary2.ContainsKey("details"))
							{
								object obj2;
								if (dictionary2.TryGetValue("details", out obj2))
								{
									object[] array = obj2 as object[];
									if (array != null)
									{
										text = text + " - " + array[0].ToString();
									}
								}
							}
							else
							{
								text += AfaStrings.UnknownError;
							}
						}
						Mouse.OverrideCursor = null;
						ErrorReport.ShowErrorMessage(text);
						return;
					}
					string text2 = obj as string;
					if (text2.Length > 0)
					{
						ExportedImage exportedImage = new ExportedImage(this, text2);
						if (dictionary.TryGetValue("width", out obj))
						{
							exportedImage.Properties.Add("width", obj);
						}
						if (dictionary.TryGetValue("height", out obj))
						{
							exportedImage.Properties.Add("height", obj);
						}
						if (dictionary.TryGetValue("scale", out obj))
						{
							exportedImage.Properties.Add("scale", obj);
						}
						if (dictionary.TryGetValue("extent", out obj))
						{
							IDictionary<string, object> dictionary3 = obj as IDictionary<string, object>;
							object obj3;
							if (dictionary3.TryGetValue("xmin", out obj3))
							{
								exportedImage.Properties.Add("xmin", obj3);
							}
							if (dictionary3.TryGetValue("xmax", out obj3))
							{
								exportedImage.Properties.Add("xmax", obj3);
							}
							if (dictionary3.TryGetValue("ymax", out obj3))
							{
								exportedImage.Properties.Add("ymax", obj3);
							}
							if (dictionary3.TryGetValue("ymin", out obj3))
							{
								exportedImage.Properties.Add("ymin", obj3);
							}
							if (dictionary3.TryGetValue("spatialReference", out obj3))
							{
								IDictionary<string, object> dictionary4 = obj3 as IDictionary<string, object>;
								object obj4;
								if (dictionary4.TryGetValue("wkid", out obj4))
								{
									exportedImage.Properties.Add("spatialReference", obj4);
									int num = int.Parse(obj4.ToString());
									spatialReference = AGSSpatialReference.SpRefFromWKID(ref num);
								}
								else if (dictionary4.TryGetValue("wkt", out obj4))
								{
									exportedImage.Properties.Add("spatialReference", obj4);
									string text3 = obj4.ToString();
									spatialReference = AGSSpatialReference.SpRefFromWKT(ref text3);
								}
							}
							PointN pointN = new PointN();
							if (spatialReference != null)
							{
								pointN.SpatialReference = spatialReference;
							}
							double num2 = double.Parse(exportedImage.Properties["xmin"].ToString());
							double num3 = double.Parse(exportedImage.Properties["ymin"].ToString());
							double num4 = double.Parse(exportedImage.Properties["xmax"].ToString());
							double num5 = double.Parse(exportedImage.Properties["ymax"].ToString());
							pointN.X = num2;
							pointN.Y = num3;
							pointN.Z = 0.0;
							PointN[] points = new PointN[]
							{
								pointN,
								new PointN
								{
									SpatialReference = spatialReference,
									X = num4,
									Y = num3,
									Z = 0.0
								},
								new PointN
								{
									SpatialReference = spatialReference,
									X = num2,
									Y = num5,
									Z = 0.0
								}
							};
							try
							{
								if (File.Exists(eo.OutputFile))
								{
									File.Delete(eo.OutputFile);
									if (App.TempFiles.Contains(eo.OutputFile))
									{
										App.TempFiles.Remove(eo.OutputFile);
									}
								}
								new WebClient();
								if (!base.Parent.DownloadFile(text2, eo.OutputFile))
								{
									eo.OutputFile = text2;
									if (string.IsNullOrEmpty(eo.OutputFile))
									{
										ErrorReport.ShowErrorMessage(AfaStrings.ErrorCreatingOutputFile);
									}
								}
							}
							catch
							{
								eo.OutputFile = text2;
								if (string.IsNullOrEmpty(eo.OutputFile))
								{
									ErrorReport.ShowErrorMessage(AfaStrings.ErrorCreatingOutputFile);
								}
							}
							finally
							{
								if (action != null)
								{
									action(this, new ExportedImageEventArgs(this, eo, points));
								}
							}
							Mouse.OverrideCursor = null;
							return;
						}
					}
				}
			}
			catch
			{
				Mouse.OverrideCursor = null;
			}
			Mouse.OverrideCursor = null;
		}

		private void RefreshProperties()
		{
			string arg = base.Parent.URL + "/" + base.FullName + "/ImageServer";
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("?f={0}", "json");
			IDictionary<string, object> dictionary = base.Parent.MakeDictionaryRequest(arg + stringBuilder);
			if (dictionary != null)
			{
				this.PopulateProperties(dictionary);
				base.IsValid = true;
				return;
			}
			base.IsValid = false;
			ErrorReport.ShowErrorMessage(base.Parent.ErrorMessage);
		}

		private void PopulateProperties(IDictionary<string, object> results)
		{
			string text = "";
			base.Properties.Add("Type", "Image Server");
			object obj;
			if (results.TryGetValue("serviceDescription", out obj))
			{
				string value = obj as string;
				if (!string.IsNullOrEmpty(value))
				{
					base.Properties.Add("Service Description", value);
				}
			}
			if (results.TryGetValue("name", out obj))
			{
				string value2 = obj as string;
				if (!string.IsNullOrEmpty(value2))
				{
					base.Properties.Add("Image Name", value2);
				}
			}
			if (results.TryGetValue("description", out obj))
			{
				string value3 = obj as string;
				if (!string.IsNullOrEmpty(value3))
				{
					base.Properties.Add("Description", value3);
				}
			}
			if (results.TryGetValue("copyrightText", out obj))
			{
				string value4 = obj as string;
				if (!string.IsNullOrEmpty(value4))
				{
					base.Properties.Add("Copyright", value4);
				}
			}
			if (results.TryGetValue("objectIdField", out obj))
			{
				string value5 = (string)obj;
				if (!string.IsNullOrEmpty(value5))
				{
					base.Properties.Add("Object ID Field", value5);
				}
				this.Version = "10.0";
			}
			if (results.TryGetValue("currentVersion", out obj))
			{
				string text2 = obj.ToString();
				if (!string.IsNullOrEmpty(text2))
				{
					this.Version = text2;
				}
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
			if (results.TryGetValue("extent", out obj) && obj != null)
			{
				IDictionary<string, object> dict3 = obj as IDictionary<string, object>;
				Extent extent3 = new Extent(dict3);
				text = extent3.SpatialReference;
				if (extent3.IsValid())
				{
					base.Properties.Add("Extent", extent3);
				}
			}
			if (results.TryGetValue("pixelSizeX", out obj))
			{
				try
				{
					base.Properties.Add("Pixel Size X", obj.ToString());
				}
				catch
				{
				}
			}
			if (results.TryGetValue("pixelSizeY", out obj))
			{
				try
				{
					base.Properties.Add("Pixel Size Y", obj.ToString());
				}
				catch
				{
				}
			}
			if (results.TryGetValue("bandCount", out obj))
			{
				try
				{
					int num = (int)obj;
					base.Properties.Add("Band Count", num.ToString());
				}
				catch
				{
				}
			}
			if (results.TryGetValue("pixelType", out obj))
			{
				try
				{
					string value6 = obj as string;
					base.Properties.Add("Pixel Type", value6);
				}
				catch
				{
				}
			}
			if (results.TryGetValue("minPixelSize", out obj))
			{
				try
				{
					base.Properties.Add("Min Pixel Size", obj.ToString());
				}
				catch
				{
				}
			}
			if (results.TryGetValue("maxPixelSize", out obj))
			{
				try
				{
					base.Properties.Add("Max Pixel Size", obj.ToString());
				}
				catch
				{
				}
			}
			if (results.TryGetValue("serviceDataType", out obj))
			{
				try
				{
					string value7 = (string)obj;
					if (!string.IsNullOrEmpty(value7))
					{
						base.Properties.Add("Service Data Type", value7);
					}
				}
				catch
				{
				}
			}
			if (results.TryGetValue("supportedImageFormatTypes", out obj))
			{
				StringList stringList = new StringList();
				string text3 = obj as string;
				string[] array = text3.Split(new char[]
				{
					','
				});
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text4 = array2[i];
					if (DocUtil.CADSupportedImageFormats.Contains(text4.ToUpper()))
					{
						stringList.Add(text4);
					}
				}
				base.SupportedImageTypes = stringList;
			}
			else
			{
				base.SupportedImageTypes = new StringList();
				string[] cADSupportedImageFormats = DocUtil.CADSupportedImageFormats;
				for (int j = 0; j < cADSupportedImageFormats.Length; j++)
				{
					string item = cADSupportedImageFormats[j];
					base.SupportedImageTypes.Add(item);
				}
			}
			if (results.TryGetValue("capabilities", out obj))
			{
				string value8 = obj as string;
				base.Properties.Add("Capabilities", value8);
			}
			if (results.TryGetValue("fields", out obj) && obj != null)
			{
				this.BuildFields(obj as IList<object>);
				int num2 = 1;
				foreach (KeyValuePair<string, AGSField> current in this.Fields)
				{
					if (current.Value != null)
					{
						base.Properties.Add("Field " + num2.ToString(), current.Value.ToString());
						num2++;
					}
				}
			}
			if (results.TryGetValue("spatialReference", out obj))
			{
				IDictionary<string, object> dictionary = obj as IDictionary<string, object>;
				if (dictionary.ContainsKey("wkid"))
				{
					object value9;
					dictionary.TryGetValue("wkid", out value9);
					base.Properties.Add("WKID", value9);
				}
				if (dictionary.ContainsKey("wkt"))
				{
					object value10;
					dictionary.TryGetValue("wkt", out value10);
					base.Properties.Add("WKT", value10);
					return;
				}
			}
			else if (!string.IsNullOrEmpty(text))
			{
				int num3;
				if (int.TryParse(text, out num3))
				{
					base.Properties.Add("WKID", num3);
					return;
				}
				base.Properties.Add("WKT", text);
			}
		}

		protected void BuildFields(IList<object> list)
		{
			this.Fields.Clear();
			try
			{
				foreach (object current in list)
				{
					Dictionary<string, object> dict = current as Dictionary<string, object>;
					AGSField aGSField = new AGSField();
					aGSField.Initialize(dict);
					if (!this.Fields.ContainsKey(aGSField.Name))
					{
						this.Fields.Add(aGSField.Name, aGSField);
					}
				}
			}
			catch (SystemException)
			{
			}
		}

		private void InitializeExportProperties()
		{
			this.ExportOptions = new AGSExportOptions();
			this.ExportOptions.Quality = 75;
			this.ExportOptions.TransCompression = "None";
			this.ExportOptions.Interpolation = "Default";
			this.ExportOptions.MosaicMethod = "esriMosaicNone";
			this.ExportOptions.OrderField = "<None>";
			this.ExportOptions.OrderBaseValue = "";
			this.ExportOptions.LockRasterID = "";
			this.ExportOptions.Ascending = true;
			this.ExportOptions.MosaicOperator = "<None>";
			this.ExportOptions.Format = "PNG24";
			this.ExportOptions.Dynamic = true;
			if (base.SupportedImageTypes == null)
			{
				base.SupportedImageTypes = new StringList();
				base.SupportedImageTypes.Add("PNG");
			}
			this.ExportOptions.SupportedFormats = base.SupportedImageTypes.ToArray();
			this.ExportOptions.Transparency = 30;
		}

		private string ReplaceQuotes(string strToEsc)
		{
			if (strToEsc.IndexOf("\"") > -1)
			{
				strToEsc = strToEsc.Replace("\"", "\\\"");
			}
			return strToEsc;
		}

		private string HTMLEncodeSpecialChars(string text)
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

		private static bool IsInteger(string theValue)
		{
			bool result;
			try
			{
				Convert.ToInt32(theValue);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}
	}
}
