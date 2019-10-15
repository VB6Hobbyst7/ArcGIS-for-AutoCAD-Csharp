using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace AFA
{
	public class RasterServiceAPI
	{
		[LispFunction("esri_map_addWorldStreetMap")]
		public object ESRI_Map_AddStreetMap(ResultBuffer rb)
		{
			object result;
			try
			{
				string url = "http://services.arcgisonline.com/arcgis/services/World_Street_Map/MapServer";
				if (this.AddServiceFromURL(url))
				{
					result = AfaDocData.ActiveDocData.CurrentMapService.Name;
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_map_names")]
		public object ESRI_Map_Names(ResultBuffer rb)
		{
			object result;
			try
			{
				MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
				if (docDataset.MapServices.Count > 0)
				{
					ResultBuffer resultBuffer = new ResultBuffer();
					foreach (string current in docDataset.MapServices.Keys)
					{
						resultBuffer.Add(new TypedValue(5005, current));
					}
					result = resultBuffer;
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_map_setcurrent")]
		public object ESRI_Map_SetCurrent(ResultBuffer rb)
		{
			object result;
			try
			{
				string argument = LspUtil.GetArgument(rb, 0, null);
				MSCMapService mapService = this.GetMapService(argument);
				if (mapService == null)
				{
					result = null;
				}
				else
				{
					AfaDocData.ActiveDocData.CurrentMapService = mapService;
					ArcGISRibbon.SetActiveRasterService(mapService);
					result = LspUtil.LispTrue;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_map_getcurrent")]
		public object ESRI_Map_GetCurrent(ResultBuffer rb)
		{
			object result;
			try
			{
				MSCMapService currentMapService = AfaDocData.ActiveDocData.CurrentMapService;
				if (currentMapService == null)
				{
					result = null;
				}
				else
				{
					result = currentMapService.Name;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_map_add")]
		public object ESRI_Map_Add(ResultBuffer rb)
		{
			object result;
			try
			{
				if (null == Application.DocumentManager.MdiActiveDocument)
				{
					result = null;
				}
				else
				{
					string assocParam = LspUtil.GetAssocParam(rb, "URL", null);
					bool assocParam2 = LspUtil.GetAssocParam(rb, "DYNAMIC", true);
					bool assocParam3 = LspUtil.GetAssocParam(rb, "VISISBLE", true);
					short num = LspUtil.GetAssocParam(rb, "TRANSPARENCY", 0);
					Point2d assocParam4 = LspUtil.GetAssocParam(rb, "EXTMIN", Point2d.Origin);
					Point2d assocParam5 = LspUtil.GetAssocParam(rb, "EXTMAX", Point2d.Origin);
					string assocParam6 = LspUtil.GetAssocParam(rb, "FORMAT", "PNG24");
					if (string.IsNullOrEmpty(assocParam))
					{
						result = null;
					}
					else
					{
						Mouse.OverrideCursor = Cursors.Wait;
						AGSMapService aGSMapService = AGSMapService.BuildMapServiceFromURL(null, assocParam);
						if (aGSMapService != null)
						{
							string text = MSCPrj.ReadWKT(Application.DocumentManager.MdiActiveDocument);
							if (string.IsNullOrEmpty(text))
							{
								text = MSCPrj.AssignWKT(AfaDocData.ActiveDocData.Document, aGSMapService.GetWKT());
							}
							aGSMapService.IsVisible = assocParam3;
							aGSMapService.ExportOptions.Dynamic = assocParam2;
							aGSMapService.ExportOptions.Format = assocParam6;
							if (num == 100)
							{
								num = 99;
							}
							aGSMapService.ExportOptions.Transparency = byte.Parse(num.ToString());
							aGSMapService.ExportOptions.OutputWKT = text;
							Extent extent = new Extent(assocParam4, assocParam5);
							if (extent.IsValid())
							{
								extent.SetWKTFrom(text);
								aGSMapService.ExportOptions.BoundingBox = extent;
							}
							if (!aGSMapService.AddService())
							{
								Mouse.OverrideCursor = null;
								result = null;
							}
							else
							{
								Mouse.OverrideCursor = null;
								MSCMapService currentMapService = AfaDocData.ActiveDocData.CurrentMapService;
								if (!assocParam3)
								{
									currentMapService.Visible = false;
								}
								result = currentMapService.Name;
							}
						}
						else
						{
							result = null;
						}
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_map_get")]
		public object ESRI_Map_Get(ResultBuffer rb)
		{
			object result;
			try
			{
				string argument = LspUtil.GetArgument(rb, 0, null);
				MSCMapService mSCMapService = null;
				if (!string.IsNullOrEmpty(argument))
				{
					mSCMapService = this.GetMapService(argument);
				}
				else
				{
					mSCMapService = AfaDocData.ActiveDocData.CurrentMapService;
				}
				if (mSCMapService == null)
				{
					result = null;
				}
				else
				{
					List<TypedValue> list = new List<TypedValue>();
					list.Add(new TypedValue(5016, null));
					if (mSCMapService.ExportOptions != null)
					{
						LspUtil.AppendDottedPair(ref list, 5005, "DYNAMIC", 5003, mSCMapService.ExportOptions.Dynamic);
						LspUtil.AppendDottedPair(ref list, 5005, "TRANSPARENCY", 5003, mSCMapService.ExportOptions.Transparency);
						try
						{
							if (mSCMapService.BoundaryExtent.IsValid())
							{
								LspUtil.AppendDottedPair(ref list, "EXT", mSCMapService.BoundaryExtent);
							}
						}
						catch
						{
						}
						if (!string.IsNullOrEmpty(mSCMapService.ExportOptions.Format))
						{
							LspUtil.AppendDottedPair(ref list, 5005, "FORMAT", 5005, mSCMapService.ExportOptions.Format);
						}
					}
					LspUtil.AppendDottedPair(ref list, 5005, "VISIBILE", 5003, mSCMapService.GetVisibility());
					LspUtil.AppendDottedPair(ref list, 5005, "NAME", 5005, mSCMapService.Name);
					LspUtil.AppendDottedPair(ref list, 5005, "ENAME", 5006, mSCMapService.RasterObjectId);
					if (!string.IsNullOrEmpty(mSCMapService.ConnectionURL))
					{
						LspUtil.AppendDottedPair(ref list, 5005, "URL", 5005, mSCMapService.ConnectionURL);
					}
					if (!string.IsNullOrEmpty(mSCMapService.RestEndpoint))
					{
						LspUtil.AppendDottedPair(ref list, 5005, "RESTURL", 5005, mSCMapService.RestEndpoint);
					}
					try
					{
						AGSConnection parent = mSCMapService.ParentService.Parent;
						if (!string.IsNullOrEmpty(parent.Name))
						{
							LspUtil.AppendDottedPair(ref list, 5005, "CONNECTION_NAME", 5005, parent.Name);
						}
					}
					catch
					{
					}
					try
					{
						if (!string.IsNullOrEmpty(mSCMapService.CurrentFileName))
						{
							LspUtil.AppendDottedPair(ref list, "CURRENTFILE", 5005, mSCMapService.CurrentFileName);
						}
						mSCMapService.ParentService.GetWKT();
						if (mSCMapService.ParentService != null)
						{
							foreach (KeyValuePair<string, object> current in mSCMapService.ParentService.Properties)
							{
								try
								{
									LspUtil.AppendDottedPair(ref list, current.Key, current.Value);
								}
								catch
								{
								}
							}
						}
					}
					catch
					{
					}
					list.Add(new TypedValue(5017, null));
					ResultBuffer resultBuffer = new ResultBuffer(list.ToArray());
					result = resultBuffer;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_map_set")]
		public object ESRI_Map_Set(ResultBuffer rb)
		{
			object result;
			try
			{
				if (null == Application.DocumentManager.MdiActiveDocument)
				{
					result = null;
				}
				else
				{
					string argument = LspUtil.GetArgument(rb, 0, null);
					MSCMapService mSCMapService;
					if (!string.IsNullOrEmpty(argument))
					{
						mSCMapService = this.GetMapService(argument);
					}
					else
					{
						mSCMapService = AfaDocData.ActiveDocData.CurrentMapService;
					}
					if (mSCMapService == null)
					{
						result = null;
					}
					else
					{
						short num = short.Parse(mSCMapService.GetTransparency().ToString());
						Point2d minPoint = mSCMapService.ExportOptions.BoundingBox.MinPoint;
						Point2d maxPoint = mSCMapService.ExportOptions.BoundingBox.MaxPoint;
						bool assocParam = LspUtil.GetAssocParam(rb, "DYNAMIC", mSCMapService.Dynamic);
						bool assocParam2 = LspUtil.GetAssocParam(rb, "VISIBLE", mSCMapService.Visible);
						short assocParam3 = LspUtil.GetAssocParam(rb, "TRANSPARENCY", num);
						Point2d assocParam4 = LspUtil.GetAssocParam(rb, "EXTMIN", minPoint);
						Point2d assocParam5 = LspUtil.GetAssocParam(rb, "EXTMAX", maxPoint);
						string assocParam6 = LspUtil.GetAssocParam(rb, "FORMAT", mSCMapService.ExportOptions.Format);
						string text = MSCPrj.ReadWKT(Application.DocumentManager.MdiActiveDocument);
						if (mSCMapService != null)
						{
							if (assocParam2 != mSCMapService.Visible)
							{
								mSCMapService.SetVisbility(assocParam2);
							}
							if (assocParam3 != num)
							{
								mSCMapService.SetTransparency((int)assocParam3);
							}
							if (assocParam != mSCMapService.Dynamic)
							{
								mSCMapService.Dynamic = assocParam;
							}
							mSCMapService.ExportOptions.Format = assocParam6;
							Extent extent = new Extent(assocParam4, assocParam5);
							if (extent.IsValid())
							{
								extent.SetWKTFrom(text);
								mSCMapService.UpdateExtentLimit(extent);
							}
							mSCMapService.ExportOptions.OutputWKT = text;
							mSCMapService.Write();
							mSCMapService.RefreshService();
							Mouse.OverrideCursor = null;
							result = true;
						}
						else
						{
							result = null;
						}
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_map_remove")]
		public object ESRI_Map_Remove(ResultBuffer rb)
		{
			object result;
			try
			{
				string argument = LspUtil.GetArgument(rb, 0, null);
				if (string.IsNullOrEmpty(argument))
				{
					result = null;
				}
				else
				{
					MSCMapService mapService = this.GetMapService(argument);
					if (mapService == null)
					{
						result = null;
					}
					else if (mapService.DeleteService())
					{
						result = LspUtil.LispTrue;
					}
					else
					{
						result = null;
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_map_sendbehind")]
		public object ESRI_Map_SendBehind(ResultBuffer rb)
		{
			object result;
			try
			{
				string argument = LspUtil.GetArgument(rb, 0, null);
				MSCRasterService mSCRasterService;
				if (!string.IsNullOrEmpty(argument))
				{
					mSCRasterService = this.GetMapService(argument);
				}
				else
				{
					mSCRasterService = AfaDocData.ActiveDocData.CurrentMapService;
				}
				if (mSCRasterService == null)
				{
					result = null;
				}
				else
				{
					mSCRasterService.SendBehind();
					result = LspUtil.LispTrue;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_image_names")]
		public object ESRI_Image_Names(ResultBuffer rb)
		{
			object result;
			try
			{
				MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
				if (docDataset.ImageServices.Count > 0)
				{
					ResultBuffer resultBuffer = new ResultBuffer();
					foreach (string current in docDataset.ImageServices.Keys)
					{
						resultBuffer.Add(new TypedValue(5005, current));
					}
					result = resultBuffer;
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_image_getcurrent")]
		public object ESRI_Image_GetCurrent(ResultBuffer rb)
		{
			object result;
			try
			{
				MSCImageService currentImageService = AfaDocData.ActiveDocData.CurrentImageService;
				if (currentImageService == null)
				{
					result = null;
				}
				else
				{
					result = currentImageService.Name;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_image_setcurrent")]
		public object ESRI_Image_SetCurrent(ResultBuffer rb)
		{
			object result;
			try
			{
				string argument = LspUtil.GetArgument(rb, 0, null);
				MSCImageService imageService = this.GetImageService(argument);
				if (imageService == null)
				{
					result = null;
				}
				else
				{
					AfaDocData.ActiveDocData.CurrentImageService = imageService;
					ArcGISRibbon.SetActiveRasterService(imageService);
					result = LspUtil.LispTrue;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_image_add")]
		public object ESRI_Image_Add(ResultBuffer rb)
		{
			object result;
			try
			{
				if (null == Application.DocumentManager.MdiActiveDocument)
				{
					result = null;
				}
				else
				{
					string assocParam = LspUtil.GetAssocParam(rb, "URL", null);
					bool assocParam2 = LspUtil.GetAssocParam(rb, "DYNAMIC", true);
					bool assocParam3 = LspUtil.GetAssocParam(rb, "VISIBLE", true);
					short num = LspUtil.GetAssocParam(rb, "TRANSPARENCY", 0);
					Point2d assocParam4 = LspUtil.GetAssocParam(rb, "EXTMIN", Point2d.Origin);
					Point2d assocParam5 = LspUtil.GetAssocParam(rb, "EXTMAX", Point2d.Origin);
					string assocParam6 = LspUtil.GetAssocParam(rb, "FORMAT", "PNG24");
					if (string.IsNullOrEmpty(assocParam))
					{
						result = null;
					}
					else
					{
						Mouse.OverrideCursor = Cursors.Wait;
						AGSImageService aGSImageService = AGSImageService.BuildImageServiceFromURL(null, assocParam);
						if (aGSImageService != null)
						{
							string text = MSCPrj.ReadWKT(Application.DocumentManager.MdiActiveDocument);
							if (string.IsNullOrEmpty(text))
							{
								text = MSCPrj.AssignWKT(AfaDocData.ActiveDocData.Document, aGSImageService.GetWKT());
							}
							aGSImageService.ExportOptions.Dynamic = assocParam2;
							aGSImageService.ExportOptions.Format = assocParam6;
							if (num == 100)
							{
								num = 99;
							}
							aGSImageService.ExportOptions.Transparency = byte.Parse(num.ToString());
							Extent extent = new Extent(assocParam4, assocParam5);
							if (extent.IsValid())
							{
								extent.SetWKTFrom(text);
								aGSImageService.ExportOptions.BoundingBox = extent;
							}
							aGSImageService.ExportOptions.OutputWKT = text;
							if (!aGSImageService.AddService(aGSImageService.ExportOptions))
							{
								Mouse.OverrideCursor = null;
								result = null;
							}
							else
							{
								Mouse.OverrideCursor = null;
								MSCImageService currentImageService = AfaDocData.ActiveDocData.CurrentImageService;
								if (!assocParam3)
								{
									currentImageService.Visible = false;
								}
								result = currentImageService.Name;
							}
						}
						else
						{
							result = null;
						}
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_image_get")]
		public object ESRI_Image_Get(ResultBuffer rb)
		{
			object result;
			try
			{
				string argument = LspUtil.GetArgument(rb, 0, null);
				MSCImageService mSCImageService = null;
				if (!string.IsNullOrEmpty(argument))
				{
					mSCImageService = this.GetImageService(argument);
				}
				else
				{
					mSCImageService = AfaDocData.ActiveDocData.CurrentImageService;
				}
				if (mSCImageService == null)
				{
					result = null;
				}
				else
				{
					List<TypedValue> list = new List<TypedValue>();
					list.Add(new TypedValue(5016, null));
					if (mSCImageService.ExportOptions != null)
					{
						LspUtil.AppendDottedPair(ref list, 5005, "DYNAMIC", 5003, mSCImageService.ExportOptions.Dynamic);
						LspUtil.AppendDottedPair(ref list, 5005, "TRANSPARENCY", 5003, mSCImageService.ExportOptions.Transparency);
						try
						{
							if (mSCImageService.BoundaryExtent.IsValid())
							{
								LspUtil.AppendDottedPair(ref list, "EXT", mSCImageService.BoundaryExtent);
							}
						}
						catch
						{
						}
						LspUtil.AppendDottedPair(ref list, 5005, "VISIBILE", 5003, mSCImageService.GetVisibility());
						if (!string.IsNullOrEmpty(mSCImageService.ExportOptions.Format))
						{
							LspUtil.AppendDottedPair(ref list, 5005, "FORMAT", 5005, mSCImageService.ExportOptions.Format);
						}
						if (!string.IsNullOrEmpty(mSCImageService.ConnectionURL))
						{
							LspUtil.AppendDottedPair(ref list, 5005, "URL", 5005, mSCImageService.RestEndpoint);
						}
						if (!string.IsNullOrEmpty(mSCImageService.RestEndpoint))
						{
							LspUtil.AppendDottedPair(ref list, 5005, "RESTURL", 5005, mSCImageService.RestEndpoint);
						}
						LspUtil.AppendDottedPair(ref list, "COMPRESSION", mSCImageService.ExportOptions.TransCompression);
						LspUtil.AppendDottedPair(ref list, "MOSAICMETHOD", mSCImageService.ExportOptions.MosaicMethod);
						LspUtil.AppendDottedPair(ref list, "MOSAICOPERATOR", mSCImageService.ExportOptions.MosaicOperator);
						LspUtil.AppendDottedPair(ref list, "MOSAICRULE", mSCImageService.ExportOptions.MosaicRule);
						LspUtil.AppendDottedPair(ref list, "ASCENDING", mSCImageService.ExportOptions.Ascending);
						LspUtil.AppendDottedPair(ref list, "BANDIDS", mSCImageService.ExportOptions.BandIds);
						LspUtil.AppendDottedPair(ref list, "INTERPOLATION", mSCImageService.ExportOptions.Interpolation);
						LspUtil.AppendDottedPair(ref list, "LOCKRASTERID", mSCImageService.ExportOptions.LockRasterID);
						LspUtil.AppendDottedPair(ref list, "NODATAVALUE", mSCImageService.ExportOptions.NoDataValue);
						LspUtil.AppendDottedPair(ref list, "ORDERBASEVALUE", mSCImageService.ExportOptions.OrderBaseValue);
						LspUtil.AppendDottedPair(ref list, "ORDERFIELD", mSCImageService.ExportOptions.OrderField);
						LspUtil.AppendDottedPair(ref list, "RENDERINGRULE", mSCImageService.ExportOptions.RenderingRule);
						LspUtil.AppendDottedPair(ref list, "PIXELTYPE", mSCImageService.ExportOptions.PixelType);
						LspUtil.AppendDottedPair(ref list, "QUALITY", mSCImageService.ExportOptions.Quality);
					}
					LspUtil.AppendDottedPair(ref list, 5005, "NAME", 5005, mSCImageService.Name);
					LspUtil.AppendDottedPair(ref list, 5005, "ENAME", 5006, mSCImageService.RasterObjectId);
					if (!string.IsNullOrEmpty(mSCImageService.ConnectionURL))
					{
						LspUtil.AppendDottedPair(ref list, 5005, "URL", 5005, mSCImageService.ConnectionURL);
					}
					try
					{
						AGSConnection parent = mSCImageService.ParentService.Parent;
						if (!string.IsNullOrEmpty(parent.Name))
						{
							LspUtil.AppendDottedPair(ref list, 5005, "CONNECTION_NAME", 5005, parent.Name);
						}
					}
					catch
					{
					}
					try
					{
						if (!string.IsNullOrEmpty(mSCImageService.CurrentFileName))
						{
							LspUtil.AppendDottedPair(ref list, "CURRENTFILE", 5005, mSCImageService.CurrentFileName);
						}
						mSCImageService.ParentService.GetWKT();
						if (mSCImageService.ParentService != null)
						{
							foreach (KeyValuePair<string, object> current in mSCImageService.ParentService.Properties)
							{
								try
								{
									LspUtil.AppendDottedPair(ref list, current.Key, current.Value);
								}
								catch
								{
								}
							}
						}
					}
					catch
					{
					}
					list.Add(new TypedValue(5017, null));
					ResultBuffer resultBuffer = new ResultBuffer(list.ToArray());
					result = resultBuffer;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_image_set")]
		public object ESRI_Image_Update(ResultBuffer rb)
		{
			object result;
			try
			{
				if (null == Application.DocumentManager.MdiActiveDocument)
				{
					result = null;
				}
				else
				{
					string argument = LspUtil.GetArgument(rb, 0, null);
					MSCImageService mSCImageService;
					if (!string.IsNullOrEmpty(argument))
					{
						mSCImageService = this.GetImageService(argument);
					}
					else
					{
						mSCImageService = AfaDocData.ActiveDocData.CurrentImageService;
					}
					if (mSCImageService == null)
					{
						result = null;
					}
					else
					{
						short num = short.Parse(mSCImageService.GetTransparency().ToString());
						Point2d minPoint = mSCImageService.ExportOptions.BoundingBox.MinPoint;
						Point2d maxPoint = mSCImageService.ExportOptions.BoundingBox.MaxPoint;
						bool assocParam = LspUtil.GetAssocParam(rb, "DYNAMIC", mSCImageService.Dynamic);
						bool assocParam2 = LspUtil.GetAssocParam(rb, "VISIBLE", mSCImageService.GetVisibility());
						short assocParam3 = LspUtil.GetAssocParam(rb, "TRANSPARENCY", num);
						Point2d assocParam4 = LspUtil.GetAssocParam(rb, "EXTMIN", minPoint);
						Point2d assocParam5 = LspUtil.GetAssocParam(rb, "EXTMAX", maxPoint);
						string assocParam6 = LspUtil.GetAssocParam(rb, "FORMAT", mSCImageService.ExportOptions.Format);
						string text = MSCPrj.ReadWKT(Application.DocumentManager.MdiActiveDocument);
						if (mSCImageService != null)
						{
							mSCImageService.SetVisbility(assocParam2);
							if (assocParam3 != num)
							{
								mSCImageService.SetTransparency((int)assocParam3);
							}
							if (assocParam != mSCImageService.Dynamic)
							{
								mSCImageService.Dynamic = assocParam;
							}
							mSCImageService.ExportOptions.Format = assocParam6;
							mSCImageService.ExportOptions.OutputWKT = text;
							Extent extent = new Extent(assocParam4, assocParam5);
							if (extent.IsValid())
							{
								extent.SetWKTFrom(text);
								mSCImageService.UpdateExtentLimit(extent);
							}
							mSCImageService.Write();
							mSCImageService.RefreshService();
							Mouse.OverrideCursor = null;
							result = true;
						}
						else
						{
							result = null;
						}
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_image_remove")]
		public object ESRI_Image_Remove(ResultBuffer rb)
		{
			object result;
			try
			{
				string argument = LspUtil.GetArgument(rb, 0, null);
				if (string.IsNullOrEmpty(argument))
				{
					result = null;
				}
				else
				{
					MSCImageService imageService = this.GetImageService(argument);
					if (imageService == null)
					{
						result = null;
					}
					else if (imageService.DeleteService())
					{
						result = LspUtil.LispTrue;
					}
					else
					{
						result = null;
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_image_sendbehind")]
		public object ESRI_Image_SendBehind(ResultBuffer rb)
		{
			object result;
			try
			{
				string argument = LspUtil.GetArgument(rb, 0, null);
				MSCRasterService mSCRasterService;
				if (!string.IsNullOrEmpty(argument))
				{
					mSCRasterService = this.GetImageService(argument);
				}
				else
				{
					mSCRasterService = AfaDocData.ActiveDocData.CurrentImageService;
				}
				if (mSCRasterService == null)
				{
					result = null;
				}
				else
				{
					mSCRasterService.SendBehind();
					result = LspUtil.LispTrue;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private bool AddServiceFromURL(string url)
		{
			if (null == Application.DocumentManager.MdiActiveDocument)
			{
				return false;
			}
			Mouse.OverrideCursor = Cursors.Wait;
			AGSMapService aGSMapService = AGSMapService.BuildMapServiceFromURL(null, url);
			if (aGSMapService == null)
			{
				return false;
			}
			string value = MSCPrj.ReadWKT(Application.DocumentManager.MdiActiveDocument);
			if (string.IsNullOrEmpty(value))
			{
				value = MSCPrj.AssignWKT(AfaDocData.ActiveDocData.Document, aGSMapService.GetWKT());
			}
			if (!aGSMapService.AddService())
			{
				Mouse.OverrideCursor = null;
				return false;
			}
			Mouse.OverrideCursor = null;
			return true;
		}

		private MSCImageService GetImageService(string name)
		{
			MSCImageService result;
			try
			{
				MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
				if (docDataset.ImageServices.Count == 0)
				{
					result = null;
				}
				else
				{
					foreach (string current in docDataset.ImageServices.Keys)
					{
						if (string.Equals(current, name, StringComparison.CurrentCultureIgnoreCase))
						{
							result = docDataset.ImageServices[current];
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

		private MSCMapService GetMapService(string name)
		{
			MSCMapService result;
			try
			{
				MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
				if (docDataset.MapServices.Count == 0)
				{
					result = null;
				}
				else
				{
					foreach (string current in docDataset.MapServices.Keys)
					{
						if (string.Equals(current, name, StringComparison.CurrentCultureIgnoreCase))
						{
							result = docDataset.MapServices[current];
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
	}
}
