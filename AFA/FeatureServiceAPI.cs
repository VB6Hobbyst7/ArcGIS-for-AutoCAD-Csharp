using AFA.UI;
using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace AFA
{
	public class FeatureServiceAPI
	{
		[LispFunction("esri_featureservice_get")]
		public object ESRI_FeatureService_Get(ResultBuffer rb)
		{
			object result;
			try
			{
				string argument = LspUtil.GetArgument(rb, 0, null);
				MSCFeatureService mSCFeatureService = null;
				if (!string.IsNullOrEmpty(argument))
				{
					mSCFeatureService = this.GetFeatureService(argument);
				}
				else
				{
					mSCFeatureService = this.GetCurrentFeatureService();
				}
				if (mSCFeatureService == null)
				{
					result = null;
				}
				else
				{
					List<TypedValue> list = new List<TypedValue>();
					list.Add(new TypedValue(5016, null));
					LspUtil.AppendDottedPair(ref list, "SERVICENAME", mSCFeatureService.Name);
					LspUtil.AppendDottedPair(ref list, "GEOMTYPE", MSCFeatureClass.GetGeomString(mSCFeatureService.GeometryType));
					if (!string.IsNullOrEmpty(mSCFeatureService.TypeField))
					{
						LspUtil.AppendDottedPair(ref list, "TYPEFIELD", mSCFeatureService.TypeField);
					}
					LspUtil.AppendDottedPair(ref list, "CADLAYER", mSCFeatureService.LayerName);
					LspUtil.AppendDottedPair(ref list, "LAYERID", mSCFeatureService.ServiceLayerID);
					if (mSCFeatureService.ExportOptions != null)
					{
						LspUtil.AppendDottedPair(ref list, 5005, "EDITMODE", 5003, mSCFeatureService.ExportOptions.Dynamic);
						try
						{
							if (mSCFeatureService.ExportOptions.BoundingBox.IsValid())
							{
								LspUtil.AppendDottedPair(ref list, "EXT", mSCFeatureService.ExportOptions.BoundingBox);
							}
						}
						catch
						{
						}
					}
					LspUtil.AppendDottedPair(ref list, 5005, "NAME", 5005, mSCFeatureService.Name);
					if (!string.IsNullOrEmpty(mSCFeatureService.ConnectionURL))
					{
						LspUtil.AppendDottedPair(ref list, 5005, "URL", 5005, mSCFeatureService.ConnectionURL);
					}
					try
					{
						AGSConnection parent = mSCFeatureService.ParentService.Parent;
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
						mSCFeatureService.ParentService.GetWKT();
						if (mSCFeatureService.ParentService != null)
						{
							foreach (KeyValuePair<string, object> current in mSCFeatureService.ParentService.Properties)
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

		[LispFunction("esri_featureservice_set")]
		public object ESRI_FeatureService_set(ResultBuffer rb)
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
					MSCFeatureService mSCFeatureService;
					if (!string.IsNullOrEmpty(argument))
					{
						mSCFeatureService = this.GetFeatureService(argument);
					}
					else
					{
						mSCFeatureService = this.GetCurrentFeatureService();
					}
					if (mSCFeatureService == null)
					{
						result = null;
					}
					else
					{
						AGSExportOptions exportOptions = mSCFeatureService.ExportOptions;
						bool assocParam = LspUtil.GetAssocParam(rb, "EDITMODE", exportOptions.Dynamic);
						Point2d assocParam2 = LspUtil.GetAssocParam(rb, "EXTMIN", exportOptions.BoundingBox.MinPoint);
						Point2d assocParam3 = LspUtil.GetAssocParam(rb, "EXTMAX", exportOptions.BoundingBox.MaxPoint);
						string text = MSCPrj.ReadWKT(Application.DocumentManager.MdiActiveDocument);
						if (mSCFeatureService != null)
						{
							if (assocParam)
							{
								mSCFeatureService.QueryOnly = false;
								mSCFeatureService.SetLayerLock(false);
							}
							else
							{
								mSCFeatureService.QueryOnly = true;
								mSCFeatureService.SetLayerLock(true);
							}
							mSCFeatureService.ExportOptions.Dynamic = assocParam;
							mSCFeatureService.ExportOptions.OutputWKT = text;
							Extent extent = new Extent(assocParam2, assocParam3);
							if (extent.IsValid())
							{
								extent.SetWKTFrom(text);
								mSCFeatureService.UpdateExtentLimit(extent);
							}
							mSCFeatureService.Write();
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

		[LispFunction("esri_featureservice_names")]
		public object ESRI_featureservice_names(ResultBuffer rb)
		{
			object result;
			try
			{
				MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
				if (docDataset.FeatureServices.Count > 0)
				{
					ResultBuffer resultBuffer = new ResultBuffer();
					foreach (string current in docDataset.FeatureServices.Keys)
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

		[LispFunction("esri_featureservice_add")]
		public object ESRI_AddFeatureService(ResultBuffer rb)
		{
			if (null == Application.DocumentManager.MdiActiveDocument)
			{
				return null;
			}
			string assocParam = LspUtil.GetAssocParam(rb, "URL", null);
			string assocParam2 = LspUtil.GetAssocParam(rb, "EDITMODE", "EDIT");
			Point2d assocParam3 = LspUtil.GetAssocParam(rb, "EXTMIN", Point2d.Origin);
			Point2d assocParam4 = LspUtil.GetAssocParam(rb, "EXTMAX", Point2d.Origin);
			string assocParam5 = LspUtil.GetAssocParam(rb, "LAYERIDS", "");
			bool assocParam6 = LspUtil.GetAssocParam(rb, "EXTRACT", false);
			if (string.IsNullOrEmpty(assocParam))
			{
				return null;
			}
			object result;
			try
			{
				Mouse.OverrideCursor = Cursors.Wait;
				string text = MSCPrj.ReadWKT(Application.DocumentManager.MdiActiveDocument);
				AGSFeatureService aGSFeatureService = AGSFeatureService.BuildFeatureServiceFromURL(null, assocParam);
				if (aGSFeatureService != null)
				{
					if (string.IsNullOrEmpty(text))
					{
						text = MSCPrj.AssignWKT(AfaDocData.ActiveDocData.Document, aGSFeatureService.GetWKT());
					}
					if (assocParam2 != "EDIT")
					{
						aGSFeatureService.ExportOptions.Dynamic = false;
					}
					Extent extent = new Extent(assocParam3, assocParam4);
					aGSFeatureService.ExportOptions.OutputWKT = text;
					if (extent.IsValid())
					{
						extent.SetWKTFrom(text);
						aGSFeatureService.ExportOptions.BoundingBox = extent;
					}
					List<int> layerList = null;
					bool flag;
					if (!string.IsNullOrEmpty(assocParam5))
					{
						string[] array = assocParam5.Split(new char[]
						{
							','
						});
						int[] collection = Array.ConvertAll<string, int>(array, new Converter<string, int>(int.Parse));
						layerList = new List<int>(collection);
						if (assocParam6)
						{
							flag = aGSFeatureService.ExtractService(layerList);
						}
						else
						{
							flag = aGSFeatureService.AddService(layerList);
						}
					}
					else if (assocParam6)
					{
						flag = aGSFeatureService.ExtractService(layerList);
					}
					else
					{
						flag = aGSFeatureService.AddService(layerList);
					}
					Mouse.OverrideCursor = null;
					if (flag)
					{
						result = true;
					}
					else
					{
						result = false;
					}
				}
				else
				{
					Mouse.OverrideCursor = null;
					result = null;
				}
			}
			catch
			{
				Mouse.OverrideCursor = null;
				result = null;
			}
			return result;
		}

		[LispFunction("esri_featureservice_remove")]
		public object ESRI_FeatureService_remove(ResultBuffer rb)
		{
			object result;
			try
			{
				string argument = LspUtil.GetArgument(rb, 0, null);
				MSCFeatureService mSCFeatureService;
				if (!string.IsNullOrEmpty(argument))
				{
					mSCFeatureService = this.GetFeatureService(argument);
				}
				else
				{
					mSCFeatureService = this.GetCurrentFeatureService();
				}
				if (mSCFeatureService == null)
				{
					result = null;
				}
				else
				{
					bool queryOnly = mSCFeatureService.QueryOnly;
					if (mSCFeatureService.DeleteService())
					{
						AfaDocData.ActiveDocData.DocDataset.FeatureServices.Remove(argument);
						MSCDataset.SetDefaultActiveFeatureClass();
						if (AfaDocData.ActiveDocData.DocDataset.FeatureServices.Count == 0)
						{
							ArcGISRibbon.EnableFeatureServiceButtons(false);
						}
						if (!queryOnly)
						{
							ToolPalette.UpdatePalette(AfaDocData.ActiveDocData.DocDataset.ParentDocument, AfaDocData.ActiveDocData.DocDataset, false);
						}
					}
					result = true;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_featureservice_getcurrent")]
		public object ESRI_GetCurrentFeatureService(ResultBuffer rb)
		{
			object result;
			try
			{
				MSCFeatureClass mSCFeatureClass = AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype();
				if (mSCFeatureClass.IsSubType)
				{
					mSCFeatureClass = mSCFeatureClass.ParentFC;
				}
				if (mSCFeatureClass.GetType() != typeof(MSCFeatureService))
				{
					result = null;
				}
				else
				{
					result = mSCFeatureClass.Name;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_featureservice_setcurrent")]
		public object ESRI_SetCurrentFeatureService(ResultBuffer rb)
		{
			object result;
			try
			{
				TypedValue[] array = rb.AsArray();
				if (array.Count<TypedValue>() < 1)
				{
					result = null;
				}
				else
				{
					TypedValue typedValue = array[0];
					if (typedValue.TypeCode != 5005)
					{
						result = null;
					}
					else
					{
						string name = typedValue.Value.ToString();
						MSCFeatureClass mSCFeatureClass = this.GetFeatureClassOrService(name);
						if (mSCFeatureClass == null)
						{
							result = null;
						}
						else if (!(mSCFeatureClass is MSCFeatureService))
						{
							result = null;
						}
						else
						{
							if (array.Count<TypedValue>() > 1)
							{
								TypedValue typedValue2 = array[1];
								if (typedValue2.TypeCode != 5005)
								{
									result = null;
									return result;
								}
								string stName = typedValue2.Value.ToString();
								MSCFeatureClassSubType mSCFeatureClassSubType = mSCFeatureClass.FindSubtypeName(stName);
								if (mSCFeatureClassSubType != null)
								{
									mSCFeatureClass = mSCFeatureClassSubType;
								}
							}
							ArcGISRibbon.SetActiveFeatureClass(mSCFeatureClass);
							result = mSCFeatureClass.Name;
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

		public static CadField FindField(List<CadField> fieldList, string fieldName)
		{
			CadField result;
			try
			{
				foreach (CadField current in fieldList)
				{
					if (string.Equals(current.Name, fieldName, StringComparison.CurrentCultureIgnoreCase))
					{
						result = current;
						return result;
					}
				}
				result = null;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private MSCFeatureClass GetCurrentFeatureClassOrService()
		{
			return AfaDocData.ActiveDocData.GetTopActiveFeatureClass();
		}

		private MSCFeatureService GetCurrentFeatureService()
		{
			MSCFeatureClass topActiveFeatureClass = AfaDocData.ActiveDocData.GetTopActiveFeatureClass();
			if (topActiveFeatureClass is MSCFeatureService)
			{
				return (MSCFeatureService)topActiveFeatureClass;
			}
			return null;
		}

		private MSCFeatureClass GetFeatureClassOrService(string name)
		{
			MSCFeatureClass result;
			try
			{
				MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
				if (docDataset.FeatureClasses.Count != 0)
				{
					foreach (string current in docDataset.FeatureClasses.Keys)
					{
						if (string.Equals(current, name, StringComparison.CurrentCultureIgnoreCase))
						{
							result = docDataset.FeatureClasses[current];
							return result;
						}
					}
				}
				if (docDataset.FeatureServices.Count != 0)
				{
					foreach (string current2 in docDataset.FeatureServices.Keys)
					{
						if (string.Equals(current2, name, StringComparison.CurrentCultureIgnoreCase))
						{
							result = docDataset.FeatureServices[current2];
							return result;
						}
					}
				}
				result = null;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private MSCFeatureService GetFeatureService(string name)
		{
			MSCFeatureService result;
			try
			{
				MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
				if (docDataset.FeatureServices.Count == 0)
				{
					result = null;
				}
				else
				{
					foreach (string current in docDataset.FeatureServices.Keys)
					{
						if (string.Equals(current, name, StringComparison.CurrentCultureIgnoreCase))
						{
							result = docDataset.FeatureServices[current];
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
