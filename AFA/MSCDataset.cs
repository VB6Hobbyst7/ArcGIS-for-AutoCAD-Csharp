using AFA.Resources;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace AFA
{
    public class MSCDataset
	{
		public Dictionary<string, MSCFeatureClass> FeatureClasses;

		public Dictionary<string, MSCFeatureService> FeatureServices;

		public ObservableDictionary<string, MSCMapService> MapServices;

		public ObservableDictionary<string, MSCImageService> ImageServices;

		public ObservableDictionary<string, FieldDomain> Domains;

		public ObservableCollection<FCView> FeatureClassViewList;

		public ObservableCollection<FCView> FeatureServiceViewList;

		private bool reactorsStarted;

		private bool ignoreSaveWarnings;

		public ObjectId FeatureClassAcadID
		{
			get;
			set;
		}

		public ObjectId FeatureServiceAcadID
		{
			get;
			set;
		}

		public ObjectId MapServiceAcadId
		{
			get;
			set;
		}

		public ObjectId ImageServiceAcadId
		{
			get;
			set;
		}

		public ObjectId DomainsAcadId
		{
			get;
			set;
		}

		public Document ParentDocument
		{
			get;
			set;
		}

		public int NumberOfSaves
		{
			get;
			private set;
		}

		public MSCDataset()
		{
			this.FeatureClassAcadID = ObjectId.Null;
			this.FeatureServiceAcadID = ObjectId.Null;
			this.FeatureClasses = new Dictionary<string, MSCFeatureClass>(StringComparer.InvariantCultureIgnoreCase);
			this.FeatureServices = new Dictionary<string, MSCFeatureService>(StringComparer.InvariantCultureIgnoreCase);
			this.FeatureClassViewList = new ObservableCollection<FCView>();
			this.FeatureServiceViewList = new ObservableCollection<FCView>();
			this.MapServices = new ObservableDictionary<string, MSCMapService>(StringComparer.InvariantCultureIgnoreCase);
			this.ImageServices = new ObservableDictionary<string, MSCImageService>(StringComparer.InvariantCultureIgnoreCase);
			this.Domains = new ObservableDictionary<string, FieldDomain>(StringComparer.InvariantCultureIgnoreCase);
			this.NumberOfSaves = 0;
		}

		public void Initialize(Document doc, Transaction t)
		{
			this.ParentDocument = doc;
			DBDictionary dBDictionary = null;
			try
			{
				dBDictionary = DocUtil.OpenNOD(doc.Database, t, 0);
				this.Domains.Clear();
				if (dBDictionary.Contains("ESRI_DOMAINS"))
				{
					this.DomainsAcadId = dBDictionary.GetAt("ESRI_DOMAINS");
					DBDictionary dBDictionary2 = this.Open(doc.Database, t, "ESRI_DOMAINS", 0);
					int arg_5D_0 = dBDictionary2.Count;
					using (DbDictionaryEnumerator enumerator = dBDictionary2.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							DBDictionaryEntry current = enumerator.Current;
							FieldDomain fieldDomain = new FieldDomain(t, current.Value);
							fieldDomain.ParentDataset = this;
							if (fieldDomain.Name != current.Key)
							{
								fieldDomain.Name = current.Key;
							}
							this.Domains.Add(current.Key, fieldDomain);
						}
					}
				}
				this.FeatureClasses.Clear();
				this.FeatureClassViewList.Clear();
                System.Windows.Forms.Application.UseWaitCursor = true;
				if (dBDictionary.Contains("ESRI_FEATURES"))
				{
					this.FeatureClassAcadID = dBDictionary.GetAt("ESRI_FEATURES");
					DBDictionary dBDictionary3 = this.Open(doc.Database, t, "ESRI_FEATURES", 0);
					using (DbDictionaryEnumerator enumerator2 = dBDictionary3.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							DBDictionaryEntry current2 = enumerator2.Current;
							MSCFeatureClass mSCFeatureClass = new MSCFeatureClass(current2.Key, this, current2.Value, t);
							this.FeatureClasses.Add(current2.Key, mSCFeatureClass);
							this.FeatureClassViewList.Add(new FCView(mSCFeatureClass));
						}
					}
				}
				this.FeatureServices.Clear();
				this.FeatureServiceViewList.Clear();
				if (dBDictionary.Contains("ESRI_FEATURESERVICES"))
				{
					ProgressMeter progressMeter = new ProgressMeter();
					progressMeter.Start(AfaStrings.LoadingFeatureServices);
					this.FeatureServiceAcadID = dBDictionary.GetAt("ESRI_FEATURESERVICES");
					DBDictionary dBDictionary4 = this.Open(doc.Database, t, "ESRI_FEATURESERVICES", 0);
					int count = dBDictionary4.Count;
					progressMeter.SetLimit(count);
					using (DbDictionaryEnumerator enumerator3 = dBDictionary4.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							DBDictionaryEntry current3 = enumerator3.Current;
							MSCFeatureService mSCFeatureService = new MSCFeatureService(t, this, current3.Key, current3.Value);
							this.FeatureServices.Add(current3.Key, mSCFeatureService);
							this.FeatureServiceViewList.Add(new FCView(mSCFeatureService));
							Thread.Sleep(2);
                            Autodesk.AutoCAD.ApplicationServices.Application.StatusBar.Update();
                            Autodesk.AutoCAD.ApplicationServices.Core.Application.UpdateScreen();
							progressMeter.MeterProgress();
                            System.Windows.Forms.Application.DoEvents();
						}
					}
					progressMeter.Stop();
				}
				this.MapServices.Clear();
				if (dBDictionary.Contains("ESRI_MAPSERVICES"))
				{
					ProgressMeter progressMeter2 = new ProgressMeter();
					progressMeter2.Start(AfaStrings.LoadingMapServices);
					this.MapServiceAcadId = dBDictionary.GetAt("ESRI_MAPSERVICES");
					DBDictionary dBDictionary5 = this.Open(doc.Database, t, "ESRI_MAPSERVICES", 0);
					int arg_2E6_0 = dBDictionary5.Count;
					using (DbDictionaryEnumerator enumerator4 = dBDictionary5.GetEnumerator())
					{
						while (enumerator4.MoveNext())
						{
							DBDictionaryEntry current4 = enumerator4.Current;
							MSCMapService value = new MSCMapService(t, this, current4.Key, current4.Value);
							this.MapServices.Add(current4.Key, value);
							Thread.Sleep(2);
                            Autodesk.AutoCAD.ApplicationServices.Application.StatusBar.Update();
                            Autodesk.AutoCAD.ApplicationServices.Core.Application.UpdateScreen();
                            progressMeter2.MeterProgress();
                            System.Windows.Forms.Application.DoEvents();
                        }
					}
					progressMeter2.Stop();
				}
				this.ImageServices.Clear();
				if (dBDictionary.Contains("ESRI_IMAGESERVICES"))
				{
					ProgressMeter progressMeter3 = new ProgressMeter();
					progressMeter3.Start(AfaStrings.LoadingImageServices);
					this.ImageServiceAcadId = dBDictionary.GetAt("ESRI_IMAGESERVICES");
					DBDictionary dBDictionary6 = this.Open(doc.Database, t, "ESRI_IMAGESERVICES", 0);
					int count2 = dBDictionary6.Count;
					progressMeter3.SetLimit(count2 * 2);
					using (DbDictionaryEnumerator enumerator5 = dBDictionary6.GetEnumerator())
					{
						while (enumerator5.MoveNext())
						{
							DBDictionaryEntry current5 = enumerator5.Current;
							progressMeter3.MeterProgress();
							MSCImageService value2 = new MSCImageService(t, this, current5.Key, current5.Value);
							this.ImageServices.Add(current5.Key, value2);
							Thread.Sleep(2);
                            Autodesk.AutoCAD.ApplicationServices.Application.StatusBar.Update();
                            Autodesk.AutoCAD.ApplicationServices.Core.Application.UpdateScreen();
                            progressMeter3.MeterProgress();
                            System.Windows.Forms.Application.DoEvents();
                        }
					}
					progressMeter3.Stop();
				}
				this.StopReactors();
				this.StartReactors();
                System.Windows.Forms.Application.UseWaitCursor = false;
			}
			catch
			{
                System.Windows.Forms.Application.UseWaitCursor = false;
			}
		}

		public void Uninitialize()
		{
			try
			{
				this.StopReactors();
				this.FeatureClassAcadID = ObjectId.Null;
				this.FeatureServiceAcadID = ObjectId.Null;
				this.FeatureClasses.Clear();
				this.FeatureServices.Clear();
				this.FeatureServiceViewList.Clear();
				this.FeatureClassViewList.Clear();
				this.MapServices.Clear();
				this.ImageServices.Clear();
				this.NumberOfSaves = 0;
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}
			catch
			{
			}
		}

		public bool Contains(Database db, Transaction t, string dictionaryName)
		{
			bool result;
			try
			{
				DBDictionary dBDictionary = DocUtil.OpenNOD(db, t, 0);
				if (dBDictionary.Contains(dictionaryName))
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public DBDictionary Open(Database db, Transaction t, string dictionaryName, OpenMode mode)
		{
			DBDictionary dBDictionary = DocUtil.OpenNOD(db, t, mode);
			if (dBDictionary.Contains(dictionaryName))
			{
				return (DBDictionary)t.GetObject(dBDictionary.GetAt(dictionaryName), mode);
			}
			DBDictionary dBDictionary2 = new DBDictionary();
			ObjectId objectId = dBDictionary.SetAt(dictionaryName, dBDictionary2);
			t.AddNewlyCreatedDBObject(dBDictionary2, true);
			return (DBDictionary)t.GetObject(objectId, mode);
		}

		public void RemoveFeatureClass(MSCFeatureClass fc)
		{
			if (null == Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument)
			{
				return;
			}
			Document mdiActiveDocument = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
			Database arg_24_0 = mdiActiveDocument.Database;
			string name = fc.Name;
			try
			{
				using (Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument.LockDocument((DocumentLockMode)20, null, null, false))
				{
					using (Transaction transaction = mdiActiveDocument.TransactionManager.StartTransaction())
					{
						DBDictionary dBDictionary = (DBDictionary)transaction.GetObject(this.FeatureClassAcadID, (OpenMode)1);
						dBDictionary.DisableUndoRecording(true);
						dBDictionary.Remove(fc.Id);
						DBObject @object = transaction.GetObject(fc.Id, (OpenMode)1);
						@object.Erase();
						mdiActiveDocument.TransactionManager.QueueForGraphicsFlush();
						mdiActiveDocument.TransactionManager.FlushGraphics();
						mdiActiveDocument.Editor.UpdateScreen();
						transaction.Commit();
					}
				}
				foreach (FCView current in this.FeatureClassViewList)
				{
					if (current.FC.fcName == name)
					{
						this.FeatureClassViewList.Remove(current);
						break;
					}
				}
				this.FeatureClasses.Remove(name);
			}
			catch
			{
			}
		}

		public MSCFeatureClassSubType CreateSubtype(string name)
		{
			return new MSCFeatureClassSubType(this)
			{
				Name = DocUtil.FixSymbolName(name)
			};
		}

		public MSCFeatureClass CreateFeatureClass(string name)
		{
			return new MSCFeatureClass(this)
			{
				Name = DocUtil.FixSymbolName(name)
			};
		}

		public MSCFeatureService CreateFeatureService(string name, AGSFeatureService parentService, int layerID, AGSExportOptions eo)
		{
			return new MSCFeatureService(this, parentService, layerID, eo)
			{
				Name = DocUtil.FixSymbolName(name)
			};
		}

		public static MSCFeatureClass AddSimpleSubtype(MSCFeatureClass parent, string suggestedSubTypeName, string type, string suggestedLayerName, List<CadField> cf, string typeFieldName, object typeValue, AGSColor suggestedColor)
		{
			if (null == Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument)
			{
				return null;
			}
			Document mdiActiveDocument = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
			Database database = mdiActiveDocument.Database;
			MSCDataset parentDataset = parent.ParentDataset;
			MSCFeatureClass result;
			try
			{
				using (Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument.LockDocument((DocumentLockMode)20, null, null, false))
				{
					var  transactionManager = database.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						DBDictionary dBDictionary = (DBDictionary)transaction.GetObject(parent.Id, (OpenMode)1);
						dBDictionary.DisableUndoRecording(true);
						string name = DocUtil.FixSymbolName(suggestedSubTypeName);
						string text = suggestedLayerName;
						DocUtil.GetLayer(database, transaction, ref text, suggestedColor);
						MSCFeatureClassSubType mSCFeatureClassSubType = AfaDocData.ActiveDocData.DocDataset.CreateSubtype(name);
						mSCFeatureClassSubType.GeometryType = MSCFeatureClass.GetTypeCodeFromESRIType(type);
						mSCFeatureClassSubType.SetQueryFromLayers(Utility.InitializeStringList(text));
						mSCFeatureClassSubType.Fields = cf;
						mSCFeatureClassSubType.TypeField = typeFieldName;
						mSCFeatureClassSubType.TypeValue = typeValue;
						mSCFeatureClassSubType.ParentFC = parent;
						CadField.SetTypeFieldValue(mSCFeatureClassSubType.Fields, mSCFeatureClassSubType.TypeField, mSCFeatureClassSubType.TypeValue);
						mSCFeatureClassSubType.CadLayerName = text;
						parent.SubTypes.Add(mSCFeatureClassSubType);
						parent.TypeField = typeFieldName;
						parent.Write(database, transaction);
						transaction.Commit();
						if (parent.GetType() == typeof(MSCFeatureService))
						{
							parentDataset.RemoveFromFeatureServiceViewList(parent.Name);
							parentDataset.FeatureServiceViewList.Add(new FCView(parent));
						}
						else
						{
							parentDataset.RemoveFromFeatureClassViewList(parent.Name);
							parentDataset.FeatureClassViewList.Add(new FCView(parent));
						}
						result = mSCFeatureClassSubType;
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static bool ContainsFeatureService(string connectionURL, string ServiceFullName, int LayerID)
		{
			foreach (MSCFeatureService current in AfaDocData.ActiveDocData.DocDataset.FeatureServices.Values)
			{
				if (current.ConnectionURL == connectionURL && current.ServiceFullName == ServiceFullName && current.ServiceLayerID == LayerID)
				{
					return true;
				}
			}
			return false;
		}

		public static MSCFeatureService AddFeatureService(ref string srcName, AGSFeatureService parent, int layerID, string type, ref List<string> layerNames, List<CadField> cf, AGSExportOptions eo)
		{
			if (null == Autodesk.AutoCAD.ApplicationServices.Core. Application.DocumentManager.MdiActiveDocument)
			{
				return null;
			}
			Database database = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument.Database;
			database.DisableUndoRecording(true);
			MSCFeatureService result;
			try
			{
				using (Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument.LockDocument((DocumentLockMode)20, null, null, false))
				{
					var  transactionManager = database.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						string text = layerNames[0];
						layerNames[0] = text;
						DocUtil.GetLayer(database, transaction, ref text);
						string text2 = MSCDataset.GenerateUniqueName(srcName);
						MSCFeatureService mSCFeatureService = AfaDocData.ActiveDocData.DocDataset.CreateFeatureService(text2, parent, layerID, eo);
						mSCFeatureService.GeometryType = MSCFeatureClass.GetTypeCodeFromESRIType(type);
						mSCFeatureService.SetQueryFromLayers(layerNames);
						mSCFeatureService.Fields = cf;
						mSCFeatureService.LayerName = text;
						mSCFeatureService.Write(database, transaction);
						transaction.Commit();
						database.DisableUndoRecording(false);
						AfaDocData.ActiveDocData.DocDataset.FeatureServices.Add(text2, mSCFeatureService);
						AfaDocData.ActiveDocData.DocDataset.FeatureServiceViewList.Add(new FCView(mSCFeatureService));
						ArcGISRibbon.EnableFeatureServiceButtons(true);
						if (AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype() == null)
						{
							AfaDocData.ActiveDocData.SetActiveFeatureClass(mSCFeatureService);
							ArcGISRibbon.SetActiveFeatureClass(mSCFeatureService);
						}
						result = mSCFeatureService;
					}
				}
			}
			catch
			{
				database.DisableUndoRecording(false);
				result = null;
			}
			return result;
		}

		public static MSCFeatureClass AddSimpleFeatureClass(string srcName, string type, List<string> layerNames, List<CadField> cf, bool useUniqueName, AGSColor suggestedColor)
		{
			if (null == Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument)
			{
				return null;
			}
			Database database = Application.DocumentManager.MdiActiveDocument.Database;
			MSCFeatureClass result;
			try
			{
				database.DisableUndoRecording(true);
				using (Application.DocumentManager.MdiActiveDocument.LockDocument((DocumentLockMode)20, null, null, false))
				{
					var  transactionManager = database.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						string text = DocUtil.FixSymbolName(srcName);
						DocUtil.GetLayer(database, transaction, ref text, suggestedColor);
						if (useUniqueName)
						{
							text = MSCDataset.GenerateUniqueName(text);
						}
						MSCFeatureClass mSCFeatureClass = AfaDocData.ActiveDocData.DocDataset.CreateFeatureClass(text);
						mSCFeatureClass.GeometryType = MSCFeatureClass.GetTypeCodeFromESRIType(type);
						mSCFeatureClass.SetQueryFromLayers(layerNames);
						mSCFeatureClass.Fields = cf;
						mSCFeatureClass.Write(database, transaction);
						transaction.Commit();
						AfaDocData.ActiveDocData.DocDataset.FeatureClasses.Add(text, mSCFeatureClass);
						AfaDocData.ActiveDocData.DocDataset.FeatureClassViewList.Add(new FCView(mSCFeatureClass));
						if (AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype() == null)
						{
							AfaDocData.ActiveDocData.SetActiveFeatureClass(mSCFeatureClass);
							ArcGISRibbon.SetActiveFeatureClass(mSCFeatureClass);
						}
						database.DisableUndoRecording(false);
						result = mSCFeatureClass;
					}
				}
			}
			catch
			{
				database.DisableUndoRecording(false);
				result = null;
			}
			return result;
		}

		private MSCImageService CreateImageService(string name, AGSImageService parentService, AGSExportOptions eo)
		{
			return new MSCImageService(this, parentService, eo)
			{
				Name = DocUtil.FixSymbolName(name)
			};
		}

		public static MSCImageService AddImageService(AGSImageService parent, ObjectId rasterImageID, AGSExportOptions eo)
		{
			MSCImageService result;
			try
			{
				MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
				string text = docDataset.GenerateUniqueName(parent);
				MSCImageService mSCImageService = docDataset.CreateImageService(text, parent, eo);
				mSCImageService.RasterObjectId = rasterImageID;
				mSCImageService.BoundaryExtent = DocUtil.GetExtentFromObject(AfaDocData.ActiveDocData.Document, rasterImageID);
				mSCImageService.BoundaryExtent.SpatialReference = AfaDocData.ActiveDocData.DocPRJ.WKT;
				mSCImageService.Write();
				docDataset.ImageServices.Add(text, mSCImageService);
				AfaDocData.ActiveDocData.CurrentImageService = mSCImageService;
				ArcGISRibbon.SetActiveRasterService(mSCImageService);
				result = mSCImageService;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private MSCMapService CreateMapService(string name, AGSMapService parentService, AGSExportOptions eo)
		{
			return new MSCMapService(this, parentService, eo)
			{
				Name = DocUtil.FixSymbolName(name)
			};
		}

		private string GenerateUniqueName(AGSRasterService map)
		{
			string name = map.Name;
			string text = name;
			int num = 0;
			while (this.MapServices.ContainsKey(text))
			{
				num++;
				text = name + num.ToString();
			}
			while (this.ImageServices.ContainsKey(text))
			{
				num++;
				text = name + num.ToString();
			}
			return text;
		}

		public List<ObjectId> GetMapServiceIds()
		{
			List<ObjectId> list = new List<ObjectId>();
			foreach (MSCMapService mSCMapService in this.MapServices)
			{
				list.Add(mSCMapService.RasterObjectId);
			}
			return list;
		}

		public MSCMapService GetMapService(ObjectId id)
		{
			foreach (MSCMapService current in this.MapServices.Values)
			{
				if (current.RasterObjectId == id)
				{
					return current;
				}
			}
			return null;
		}

		public string GetMapServiceKey(ObjectId id)
		{
			foreach (KeyValuePair<string, MSCMapService> keyValuePair in this.MapServices)
			{
				if (id == keyValuePair.Value.RasterObjectId)
				{
					return keyValuePair.Key;
				}
			}
			return null;
		}

		public static MSCMapService AddMapService(AGSMapService parent, ObjectId rasterImageID, List<AGSMapLayer> layers, AGSExportOptions eo)
		{
			MSCMapService result;
			try
			{
				MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
				string text = docDataset.GenerateUniqueName(parent);
				MSCMapService mSCMapService = docDataset.CreateMapService(text, parent, eo);
				mSCMapService.RasterObjectId = rasterImageID;
				mSCMapService.BoundaryExtent = DocUtil.GetExtentFromObject(AfaDocData.ActiveDocData.Document, rasterImageID);
				mSCMapService.BoundaryExtent.SpatialReference = AfaDocData.ActiveDocData.DocPRJ.WKT;
				mSCMapService.Write();
				docDataset.MapServices.Add(text, mSCMapService);
				AfaDocData.ActiveDocData.CurrentMapService = mSCMapService;
				ArcGISRibbon.SetActiveRasterService(mSCMapService);
				result = mSCMapService;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private string GetAssociatedMapServerURL(string featureServiceURL)
		{
			string text = string.Copy(featureServiceURL);
			int num = text.LastIndexOf("/FeatureServer");
			if (num > 1)
			{
				text = text.Substring(0, num);
			}
			num = text.LastIndexOf("\\FeatureServer");
			if (num > 1)
			{
				text = text.Substring(0, num);
			}
			return text;
		}

		public void RefreshAssociatedMaps(string featureServiceURL)
		{
			try
			{
				string b = this.GetAssociatedMapServerURL(featureServiceURL).ToUpper();
				foreach (MSCMapService current in this.MapServices.Values)
				{
					AGSMapService aGSMapService = (AGSMapService)current.ParentService;
					if (string.IsNullOrEmpty(aGSMapService.ServiceURL))
					{
						string serviceURL = aGSMapService.Parent.Soap_URL + "/" + aGSMapService.FullName + "/MapServer";
						aGSMapService.ServiceURL = serviceURL;
					}
					if (!string.IsNullOrEmpty(aGSMapService.ServiceURL) && aGSMapService.ServiceURL.ToUpper() == b)
					{
						current.RefreshService();
					}
				}
			}
			catch
			{
			}
		}

		public void RefreshAllMaps()
		{
			foreach (MSCMapService current in this.MapServices.Values)
			{
				current.RefreshService();
			}
			foreach (MSCImageService current2 in this.ImageServices.Values)
			{
				current2.RefreshService();
			}
		}

		public void UpdateMaps()
		{
			foreach (MSCMapService current in this.MapServices.Values)
			{
				current.CheckForUpdates();
			}
			foreach (MSCImageService current2 in this.ImageServices.Values)
			{
				current2.CheckForUpdates();
			}
			foreach (MSCMapService current3 in this.MapServices.Values)
			{
				current3.CheckForUpdates();
			}
			foreach (MSCImageService current4 in this.ImageServices.Values)
			{
				current4.CheckForUpdates();
			}
			PaletteUtils.ActivateEditor();
			this.ParentDocument.Editor.UpdateScreen();
		}

		public static string GenerateUniqueName(string baseName)
		{
			if (!string.IsNullOrEmpty(baseName))
			{
				baseName = DocUtil.FixSymbolName(baseName);
				string text = baseName;
				int num = 0;
				while (AfaDocData.ActiveDocData.DocDataset.FeatureServices.ContainsKey(text))
				{
					text = baseName + ++num;
				}
				while (AfaDocData.ActiveDocData.DocDataset.FeatureClasses.ContainsKey(text))
				{
					text = baseName + ++num;
				}
				return text;
			}
			return baseName;
		}

		public static void SetDefaultActiveFeatureClass()
		{
			try
			{
				MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
				if (AfaDocData.ActiveDocData.DocDataset.FeatureClasses.Count > 0)
				{
					AfaDocData.ActiveDocData.SetActiveFeatureClass(docDataset.FeatureClasses.ElementAt(0).Value);
				}
				else if (AfaDocData.ActiveDocData.DocDataset.FeatureServices.Count > 0)
				{
					AfaDocData.ActiveDocData.SetActiveFeatureClass(docDataset.FeatureServices.ElementAt(0).Value);
				}
				else
				{
					AfaDocData.ActiveDocData.ClearActiveFeatureClass();
				}
				ArcGISRibbon.SetActiveFeatureClass(AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype());
				if (AfaDocData.ActiveDocData.DocDataset.FeatureServices.Count == 0)
				{
					ArcGISRibbon.EnableFeatureServiceButtons(false);
				}
				else
				{
					ArcGISRibbon.EnableFeatureServiceButtons(true);
				}
			}
			catch
			{
			}
		}

		public static void SetDefaultActiveRasterServices()
		{
			try
			{
				if (AfaDocData.ActiveDocData.DocDataset.MapServices.Count > 0)
				{
					AfaDocData.ActiveDocData.CurrentMapService = AfaDocData.ActiveDocData.DocDataset.MapServices.ElementAt(0).Value;
				}
				else
				{
					AfaDocData.ActiveDocData.CurrentMapService = null;
				}
				if (AfaDocData.ActiveDocData.DocDataset.ImageServices.Count > 0)
				{
					AfaDocData.ActiveDocData.CurrentImageService = AfaDocData.ActiveDocData.DocDataset.ImageServices.ElementAt(0).Value;
				}
				else
				{
					AfaDocData.ActiveDocData.CurrentImageService = null;
				}
				if (AfaDocData.ActiveDocData.CurrentMapService != null)
				{
					ArcGISRibbon.SetActiveRasterService(AfaDocData.ActiveDocData.CurrentMapService);
				}
				else if (AfaDocData.ActiveDocData.CurrentImageService != null)
				{
					ArcGISRibbon.SetActiveRasterService(AfaDocData.ActiveDocData.CurrentImageService);
				}
				else
				{
					ArcGISRibbon.ClearRasterCombo();
				}
			}
			catch
			{
			}
		}

		private void SetCurrentLayerToDefault(Transaction t, ObjectId defaultLayerID)
		{
			try
			{
				LayerTableRecord layerTableRecord = (LayerTableRecord)t.TransactionManager.GetObject(defaultLayerID, (OpenMode)1, false, true);
				layerTableRecord.IsFrozen=(false);
				layerTableRecord.IsHidden=(false);
				layerTableRecord.IsLocked=(false);
			}
			catch (System.Exception ex)
			{
				string arg_33_0 = ex.Message;
			}
		}

		public void ShowFeatureServiceLayers(bool show)
		{
			if (AfaDocData.ActiveDocData.FeatureServiceLayersVisible && show)
			{
				return;
			}
			if (this.FeatureServices == null)
			{
				return;
			}
			if (this.FeatureServices.Count == 0)
			{
				return;
			}
			Database database;
			ObjectId clayer;
			try
			{
				database = this.ParentDocument.Database;
				clayer = database.Clayer;
			}
			catch
			{
				return;
			}
			try
			{
				using (this.ParentDocument.LockDocument())
				{
					database.DisableUndoRecording(true);
					using (Transaction transaction = this.ParentDocument.TransactionManager.StartTransaction())
					{
						ObjectId layerZero = database.LayerZero;
						LayerTable layerTable = (LayerTable)transaction.TransactionManager.GetObject(database.LayerTableId, (OpenMode)1, false);
						foreach (MSCFeatureService current in this.FeatureServices.Values)
						{
							string text = current.LayerName;
							ObjectId objectId = ObjectId.Null;
							if (layerTable.IncludingHidden.Has(text))
							{
								objectId = layerTable.IncludingHidden[(text)];
							}
							else
							{
								text = "*" + text;
								if (layerTable.IncludingHidden.Has(text))
								{
									objectId = layerTable.IncludingHidden[(text)];
								}
							}
							if (objectId == clayer)
							{
								this.SetCurrentLayerToDefault(transaction, layerZero);
							}
							if (!objectId.IsNull)
							{
								try
								{
									LayerTableRecord layerTableRecord = (LayerTableRecord)transaction.TransactionManager.GetObject(objectId, (OpenMode)1, false, true);
									layerTableRecord.IsHidden=(!show);
									layerTableRecord.IsFrozen=(!show);
									if (show)
									{
										layerTableRecord.ViewportVisibilityDefault=(true);
									}
								}
								catch (System.Exception ex)
								{
									string arg_167_0 = ex.Message;
								}
							}
							foreach (MSCFeatureClassSubType current2 in current.SubTypes)
							{
								objectId = ObjectId.Null;
								text = current2.CadLayerName;
								if (layerTable.IncludingHidden.Has(text))
								{
									objectId = layerTable.IncludingHidden[(text)];
								}
								else
								{
									text = "*" + text;
									if (layerTable.IncludingHidden.Has(text))
									{
										objectId = layerTable.IncludingHidden[(text)];
									}
								}
								if (objectId == clayer)
								{
									this.SetCurrentLayerToDefault(transaction, layerZero);
								}
								if (!objectId.IsNull)
								{
									try
									{
										LayerTableRecord layerTableRecord2 = (LayerTableRecord)transaction.TransactionManager.GetObject(objectId, (OpenMode)1, false, true);
										layerTableRecord2.IsHidden=(!show);
										layerTableRecord2.IsHidden=(!show);
										if (show)
										{
											layerTableRecord2.ViewportVisibilityDefault=(true);
										}
									}
									catch (System.Exception ex2)
									{
										string arg_245_0 = ex2.Message;
									}
								}
							}
						}
						this.ParentDocument.TransactionManager.QueueForGraphicsFlush();
						this.ParentDocument.TransactionManager.FlushGraphics();
						this.ParentDocument.Editor.UpdateScreen();
						transaction.Commit();
						database.DisableUndoRecording(false);
						if (show)
						{
							DocUtil.ShowBlocks(this.ParentDocument);
							this.ParentDocument.TransactionManager.FlushGraphics();
							this.ParentDocument.Editor.UpdateScreen();
							this.ParentDocument.Editor.Regen();
							PaletteUtils.ActivateEditor();
						}
					}
				}
				AfaDocData.ActiveDocData.FeatureServiceLayersVisible = show;
			}
			catch
			{
				database.DisableUndoRecording(false);
			}
		}

		public void RemoveFromFeatureClassViewList(string Name)
		{
			foreach (FCView current in this.FeatureClassViewList)
			{
				if (current.FC.fcName == Name)
				{
					this.FeatureClassViewList.Remove(current);
					break;
				}
			}
		}

		public void RemoveFromFeatureServiceViewList(string Name)
		{
			foreach (FCView current in this.FeatureServiceViewList)
			{
				if (current.FC.fcName == Name)
				{
					this.FeatureServiceViewList.Remove(current);
					break;
				}
			}
		}

		private void TestLayers()
		{
			Database database = this.ParentDocument.Database;
			ObjectId clayer = database.Clayer;
			try
			{
				string text = "";
				using (this.ParentDocument.LockDocument((DocumentLockMode)32, null, null, false))
				{
					var transactionManager = database.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						LayerTable layerTable = (LayerTable)transaction.TransactionManager.GetObject(database.LayerTableId, 0, false);
						using (SymbolTableEnumerator enumerator = layerTable.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								ObjectId current = enumerator.Current;
								LayerTableRecord layerTableRecord = (LayerTableRecord)transaction.TransactionManager.GetObject(current, 0, false, true);
								text += layerTableRecord.Name;
								if (layerTableRecord.IsHidden)
								{
									text += "  HIDDEN";
								}
								if (current == clayer)
								{
									text += "  CURRENT";
								}
								text += "\n\r";
							}
						}
						transaction.Commit();
						ErrorReport.ShowErrorMessage(text);
					}
				}
			}
			catch
			{
			}
		}

		public void RefreshAllFeatureServices()
		{
			ArcGISRibbon.EnableFeatureServiceButtons(false);
			if (this.FeatureServices == null)
			{
				return;
			}
			if (this.FeatureServices.Count == 0)
			{
				return;
			}
			ArcGISRibbon.EnableFeatureServiceButtons(true);
			string text = "";
			try
			{
				foreach (MSCFeatureService current in this.FeatureServices.Values)
				{
					string text2 = "";
					current.Refresh(ref text2);
					if (!string.IsNullOrEmpty(text2))
					{
						text = text + text2 + "\r\n";
					}
					if (!current.QueryOnly && !current.HasCommitErrors && current.ParentService.IsValid)
					{
						current.ParentDataset.RefreshAssociatedMaps(current.ParentService.ServiceURL);
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					MessageBoxEx.Show(new WindowWrapper(Application.MainWindow.Handle), text, AfaStrings.RefreshResults, 10000u);
				}
			}
			catch
			{
				MessageBoxEx.Show(new WindowWrapper(Application.MainWindow.Handle), "Error in RefreshAllFeatureServices", 30000u);
			}
		}

		public bool PrepareToCommitAllFeatureServices(ref string resultsString)
		{
			if (this.FeatureServices == null)
			{
				return false;
			}
			if (this.FeatureServices.Count == 0)
			{
				return false;
			}
			resultsString = "";
			bool result;
			try
			{
				foreach (MSCFeatureService current in this.FeatureServices.Values)
				{
					string text = "";
					current.PrepareToCommit(ref text);
					if (!string.IsNullOrEmpty(text))
					{
						resultsString = resultsString + text + "\r\n";
					}
				}
				result = true;
			}
			catch
			{
				resultsString += AfaStrings.ErrorInPreparingToCommit;
				result = false;
			}
			return result;
		}

		public bool CommitAllFeatureServices()
		{
			if (this.FeatureServices == null)
			{
				return false;
			}
			if (this.FeatureServices.Count == 0)
			{
				return false;
			}
			string text = "";
			bool result;
			try
			{
				Document parentDocument = this.ParentDocument;
				Editor editor = parentDocument.Editor;
				string arg = "";
				if (AfaDocData.ActiveDocData.DocDataset.PrepareToCommitAllFeatureServices(ref arg))
				{
					string message = string.Format("{0}\n{1}", arg, AfaStrings.ProceedQuestion);
					if (!MessageUtil.ShowYesNo(message))
					{
						editor.WriteMessage("\n" + AfaStrings.CommandCancelled);
						result = false;
						return result;
					}
					arg = "";
				}
				foreach (MSCFeatureService current in this.FeatureServices.Values)
				{
					string text2 = "";
					current.CommitEdits(ref text2);
					if (!string.IsNullOrEmpty(text2))
					{
						text = text + text2 + "\r\n";
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					MessageBoxEx.Show(new WindowWrapper(Application.MainWindow.Handle), text, AfaStrings.SynchronizeResults, 30000u);
				}
				result = true;
			}
			catch
			{
				MessageBoxEx.Show(new WindowWrapper(Application.MainWindow.Handle), "Error in CommitAllFeatureServices", 10000u);
				result = false;
			}
			return result;
		}

		public void StartReactors()
		{
			try
			{
				if (!this.reactorsStarted)
				{
					Document parentDocument = this.ParentDocument;
					using (parentDocument.LockDocument((DocumentLockMode)20, null, null, false))
					{
						this.reactorsStarted = true;
						parentDocument.Database.BeginSave+=(new DatabaseIOEventHandler(this.Database_BeginSave));
						parentDocument.Database.SaveComplete+=new DatabaseIOEventHandler(this.Database_SaveComplete);
						parentDocument.BeginDocumentClose+=(new DocumentBeginCloseEventHandler(this.doc_BeginDocumentClose));
					}
				}
			}
			catch
			{
			}
		}

		public void StopReactors()
		{
			try
			{
				if (this.reactorsStarted)
				{
					this.reactorsStarted = false;
					Document parentDocument = this.ParentDocument;
					using (parentDocument.LockDocument((DocumentLockMode)20, null, null, false))
					{
						parentDocument.Database.BeginSave-=(new DatabaseIOEventHandler(this.Database_BeginSave));
						parentDocument.Database.SaveComplete-=(new DatabaseIOEventHandler(this.Database_SaveComplete));
						parentDocument.BeginDocumentClose-=(new DocumentBeginCloseEventHandler(this.doc_BeginDocumentClose));
					}
				}
			}
			catch
			{
			}
		}

		private void doc_BeginDocumentClose(object sender, DocumentBeginCloseEventArgs e)
		{
			try
			{
				if (this.FeatureServices.Count == 0 || this.NumberOfSaves == 0)
				{
					this.ClearCachedImages();
				}
				else
				{
					bool flag = false;
					foreach (MSCFeatureService current in this.FeatureServices.Values)
					{
						if (current.IsDirtyAfterCommit)
						{
							flag = true;
							break;
						}
					}
					if (flag && !MessageUtil.ShowYesNo(AfaStrings.DataIntegrityWarning))
					{
						e.Veto();
					}
					else
					{
						this.ShowFeatureServiceLayers(false);
						this.ClearCachedImages();
					}
				}
			}
			catch
			{
			}
		}

		private void Database_SaveComplete(object sender, DatabaseIOEventArgs e)
		{
			try
			{
				this.ShowFeatureServiceLayers(true);
				this.NumberOfSaves++;
			}
			catch
			{
			}
		}

		public bool HasFeatureServicesOpenedForEditing()
		{
			if (this.FeatureServices == null)
			{
				return false;
			}
			if (this.FeatureServices.Count == 0)
			{
				return false;
			}
			bool result = false;
			foreach (MSCFeatureService current in this.FeatureServices.Values)
			{
				if (!current.QueryOnly)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		private void Database_BeginSave(object sender, DatabaseIOEventArgs e)
		{
			try
			{
				if (!this.ignoreSaveWarnings)
				{
					string fileName = e.FileName;
					string a = Path.GetExtension(fileName).ToLower();
					if (a != ".sv$")
					{
						if (this.HasFeatureServicesOpenedForEditing() && MessageUtil.ShowYesNo(AfaStrings.CommitNowQuestion))
						{
							this.CommitAllFeatureServices();
						}
						this.ShowFeatureServiceLayers(false);
					}
				}
			}
			catch
			{
			}
		}

		public void SaveParent()
		{
			try
			{
				this.StopReactors();
				this.ignoreSaveWarnings = true;
				this.ParentDocument.Database.SaveAs(this.ParentDocument.Name, true, (DwgVersion)31, this.ParentDocument.Database.SecurityParameters);
				this.ignoreSaveWarnings = false;
			}
			catch
			{
			}
			finally
			{
				this.StartReactors();
			}
		}

		public void ClearCachedImages()
		{
			try
			{
				foreach (MSCMapService current in this.MapServices.Values)
				{
					try
					{
						if (File.Exists(current.CurrentFileName))
						{
							File.Delete(current.CurrentFileName);
							if (App.TempFiles.Contains(current.CurrentFileName))
							{
								App.TempFiles.Remove(current.CurrentFileName);
							}
						}
					}
					catch
					{
					}
				}
				foreach (MSCImageService current2 in this.ImageServices.Values)
				{
					try
					{
						if (File.Exists(current2.CurrentFileName))
						{
							File.Delete(current2.CurrentFileName);
							if (App.TempFiles.Contains(current2.CurrentFileName))
							{
								App.TempFiles.Remove(current2.CurrentFileName);
							}
						}
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
		}

		public int Import(Database sourceDB, Database destDB)
		{
			int num = 0;
			string text = null;
			int result;
			try
			{
				MSCDataset parent = new MSCDataset();
				MSCDataset arg_14_0 = AfaDocData.ActiveDocData.DocDataset;
				List<MSCFeatureClass> list = new List<MSCFeatureClass>();
				using (Transaction transaction = sourceDB.TransactionManager.StartTransaction())
				{
					DocUtil.OpenNOD(sourceDB, transaction, 0);
					if (this.Contains(sourceDB, transaction, "ESRI_PRJ"))
					{
						text = MSCPrj.ReadWKT(sourceDB);
					}
					if (this.Contains(sourceDB, transaction, "ESRI_FEATURES"))
					{
						DBDictionary dBDictionary = this.Open(sourceDB, transaction, "ESRI_FEATURES", 0);
						using (DbDictionaryEnumerator enumerator = dBDictionary.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								DBDictionaryEntry current = enumerator.Current;
								MSCFeatureClass item = new MSCFeatureClass(current.Key, parent, current.Value, transaction);
								list.Add(item);
							}
						}
					}
					transaction.Commit();
				}
				string value = MSCPrj.ReadWKT(destDB);
				if (string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(text))
				{
					MSCPrj.AssignWKT(AfaDocData.ActiveDocData.Document, text);
				}
				if (list.Count > 0)
				{
					using (AfaDocData.ActiveDocData.Document.LockDocument())
					{
						using (Transaction transaction2 = destDB.TransactionManager.StartTransaction())
						{
							foreach (MSCFeatureClass current2 in list)
							{
								foreach (CadField current3 in current2.Fields)
								{
									if (current3.Domain != null)
									{
										current3.Domain.Id = ObjectId.Null;
									}
								}
								current2.ParentDataset = AfaDocData.ActiveDocData.DocDataset;
								string name = MSCDataset.GenerateUniqueName(current2.Name);
								current2.Name = name;
								foreach (MSCFeatureClassSubType current4 in current2.SubTypes)
								{
									string name2 = MSCDataset.GenerateUniqueName(current4.Name);
									current4.Name = name2;
								}
								current2.Write(destDB, transaction2);
								this.FeatureClasses.Add(current2.Name, current2);
								num++;
							}
							transaction2.Commit();
						}
					}
				}
				if (num > 0)
				{
					AfaDocData.ActiveDocData.Refresh();
				}
				result = num;
			}
			catch
			{
				result = num;
			}
			return result;
		}
	}
}
