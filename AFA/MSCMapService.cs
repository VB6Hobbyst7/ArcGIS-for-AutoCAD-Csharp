using AFA.Resources;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;
using Exception = Autodesk.AutoCAD.Runtime.Exception;

namespace AFA
{
    public class MSCMapService : MSCRasterService
	{
		protected string DictionaryName = "ESRI_MAPSERVICES";

		public override AGSRasterService ParentService
		{
			get;
			set;
		}

		public List<MSCMapLayer> Layers
		{
			get;
			set;
		}

		public string ServiceFullName
		{
			get;
			set;
		}

		public string ErrorMessage
		{
			get;
			set;
		}

		public string CurrentImagePath
		{
			get;
			set;
		}

		private ObjectId AcadDictionaryID
		{
			get;
			set;
		}

		public bool SupportsDisconnect
		{
			get
			{
				return ((AGSMapService)this.ParentService).SupportsDisconnect();
			}
		}

		public string RestEndpoint
		{
			get
			{
				Uri uri = new Uri(Path.Combine(this.ParentService.Parent.URL, this.ParentService.FullName));
				Uri uri2 = new Uri(Path.Combine(uri.ToString(), "MapServer"));
				return uri2.ToString();
			}
		}

		public MSCMapService(MSCDataset parentDataset, AGSMapService parentMap, AGSExportOptions eo)
		{
			this.ParentService = (AGSMapService)Utility.CloneObject(parentMap);
			base.ParentDataset = parentDataset;
			try
			{
				base.ConnectionURL = this.ParentService.Parent.Soap_URL;
				base.UserName = this.ParentService.Parent.UserName;
				this.ServiceFullName = this.ParentService.FullName;
				base.ConnectionURL = this.ParentService.Parent.Soap_URL;
				base.UserName = this.ParentService.Parent.UserName;
				base.ConnectionName = this.ParentService.Parent.Name;
				this.InitializeLayers();
				base.ExportOptions = eo;
				base.CurrentFileName = eo.OutputFile;
				this.UpdateForm = new HiddenUpdateForm(base.ParentDataset.ParentDocument);
				base.StartReactors();
				AfaDocData.ActiveDocData.CurrentMapService = this;
			}
			catch (SystemException ex)
			{
				this.ErrorMessage = ex.Message;
			}
		}

		public MSCMapService(Transaction t, MSCDataset parent, string name, ObjectId id)
		{
			base.ParentDataset = parent;
			this.AcadDictionaryID = id;
			base.Name = name;
			this.Layers = new List<MSCMapLayer>();
			this.Read(id, t);
			this.UpdateForm = new HiddenUpdateForm(base.ParentDataset.ParentDocument);
			base.CheckForUpdates();
			base.StartReactors();
		}

		private void InitializeChildLayers(MSCMapLayer parentLyr)
		{
			AGSMapService aGSMapService = (AGSMapService)this.ParentService;
			if (parentLyr.ChildLayerIDs.Count == 0)
			{
				return;
			}
			using (IEnumerator<object> enumerator = aGSMapService.MapLayers.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					AGSMapLayer aGSMapLayer = (AGSMapLayer)enumerator.Current;
					if (aGSMapLayer.parentLayerId == parentLyr.ID)
					{
						MSCMapLayer mSCMapLayer = new MSCMapLayer();
						mSCMapLayer.Name = aGSMapLayer.Name;
						mSCMapLayer.ID = aGSMapLayer.Id;
						mSCMapLayer.Visible = aGSMapLayer.IsVisible;
						mSCMapLayer.Layers = new List<MSCMapLayer>();
						mSCMapLayer.ParentMap = this;
						mSCMapLayer.ChildLayerIDs = new List<int>(aGSMapLayer.ChildLayerIds);
						if (mSCMapLayer.ChildLayerIDs.Count != 0)
						{
							this.InitializeChildLayers(mSCMapLayer);
						}
						parentLyr.Layers.Add(mSCMapLayer);
					}
				}
			}
		}

		private void InitializeLayers()
		{
			this.Layers = new List<MSCMapLayer>();
			AGSMapService aGSMapService = (AGSMapService)this.ParentService;
			using (IEnumerator<object> enumerator = aGSMapService.MapLayers.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					AGSMapLayer aGSMapLayer = (AGSMapLayer)enumerator.Current;
					MSCMapLayer mSCMapLayer = new MSCMapLayer();
					if (aGSMapLayer.parentLayerId == -1)
					{
						mSCMapLayer.ParentLayerID = aGSMapLayer.parentLayerId;
						mSCMapLayer.Name = aGSMapLayer.Name;
						mSCMapLayer.ID = aGSMapLayer.Id;
						mSCMapLayer.Visible = aGSMapLayer.IsVisible;
						mSCMapLayer.ParentMap = this;
						mSCMapLayer.Layers = new List<MSCMapLayer>();
						mSCMapLayer.ChildLayerIDs = new List<int>(aGSMapLayer.ChildLayerIds);
						this.InitializeChildLayers(mSCMapLayer);
						this.Layers.Add(mSCMapLayer);
					}
				}
			}
		}

		private void InitExportOptions(Document doc)
		{
			base.ExportOptions.AcadDocument = doc;
			if (base.BoundaryExtent != null)
			{
				base.ExportOptions.BoundingBox = new Extent(base.BoundaryExtent);
			}
			else
			{
				base.ExportOptions.BoundingBox = new Extent(doc);
			}
			try
			{
				System.Drawing.Size size = Application.ToSystemDrawingSize(doc.Window.DeviceIndependentSize);
				base.ExportOptions.Width = size.Width;
				base.ExportOptions.Height = size.Height;
			}
			catch
			{
				base.ExportOptions.Width = 1280;
				base.ExportOptions.Height = 1280;
			}
			base.ExportOptions.DPI = 96;
			base.ExportOptions.Format = "PNG24";
		}

		private void InitExportOptions()
		{
			this.InitExportOptions(base.ParentDataset.ParentDocument);
		}

		private void SetParentSubLayerVisibility(MSCMapLayer parentLayer)
		{
			if (parentLayer.Layers.Count == 0)
			{
				return;
			}
			AGSMapService aGSMapService = (AGSMapService)this.ParentService;
			foreach (MSCMapLayer current in parentLayer.Layers)
			{
				AGSMapLayer aGSMapLayer = aGSMapService.FindMapLayer(current.ID);
				if (aGSMapLayer != null)
				{
					aGSMapLayer.IsVisible = current.Visible;
				}
				if (current.Layers.Count > 0)
				{
					this.SetParentSubLayerVisibility(current);
				}
			}
		}

		protected override void SetParentLayerVisibility()
		{
			AGSMapService aGSMapService = (AGSMapService)this.ParentService;
			foreach (MSCMapLayer current in this.Layers)
			{
				AGSMapLayer aGSMapLayer = aGSMapService.FindMapLayer(current.ID);
				if (aGSMapLayer != null)
				{
					aGSMapLayer.IsVisible = current.Visible;
				}
				if (current.Layers.Count > 0)
				{
					this.SetParentSubLayerVisibility(current);
				}
			}
		}

		protected override bool ReconnectService()
		{
			bool result;
			try
			{
				if (this.ParentService != null)
				{
					if (this.ParentService.IsValid)
					{
						result = true;
					}
					else
					{
						result = this.ParentService.Challenge();
					}
				}
				else if (string.IsNullOrEmpty(base.ConnectionURL))
				{
					result = false;
				}
				else
				{
					AGSConnection connection = AGSConnection.GetConnection(base.ConnectionURL, base.ConnectionName, base.UserName);
					if (connection != null)
					{
						if (!connection.ConnectionFailed)
						{
							if (connection.LoadConnectionProperties())
							{
								this.ParentService = new AGSMapService(this.ServiceFullName, base.ConnectionURL, connection);
								result = this.ParentService.Challenge();
							}
							else
							{
								this.ErrorMessage = connection.ErrorMessage;
								result = false;
							}
						}
						else
						{
							this.ErrorMessage = connection.ErrorMessage;
							result = false;
						}
					}
					else
					{
						this.ErrorMessage = AfaStrings.ErrorConnectingToServer;
						result = false;
					}
				}
			}
			catch
			{
				this.ErrorMessage = AfaStrings.ErrorConnectingToServer;
				result = false;
			}
			return result;
		}

		public override void Write()
		{
			Database database = base.ParentDataset.ParentDocument.Database;
			try
			{
				using (base.ParentDataset.ParentDocument.LockDocument((DocumentLockMode)20, null, null, false))
				{
					var transactionManager = database.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						this.Write(database, transaction);
						transaction.Commit();
					}
				}
			}
			catch
			{
				this.ErrorMessage = "Error writing image service to file.";
			}
		}

		public void Write(Database db, Transaction t)
		{
			try
			{
				DBDictionary parentDict = base.ParentDataset.Open(db, t, this.DictionaryName, (OpenMode)1);
				DBDictionary dBDictionary = this.WriteMapDictionary(parentDict, t);
				if (dBDictionary != null && this.Layers.Count > 0)
				{
					DBDictionary dBDictionary2 = new DBDictionary();
					dBDictionary.SetAt("ESRI_Layers", dBDictionary2);
					dBDictionary2.DisableUndoRecording(true);
					t.AddNewlyCreatedDBObject(dBDictionary2, true);
					foreach (MSCMapLayer current in this.Layers)
					{
						current.WriteMapLayer(dBDictionary2, t);
					}
				}
			}
			catch
			{
			}
		}

		protected DBDictionary WriteMapDictionary(DBDictionary parentDict, Transaction t)
		{
			DBDictionary result;
			try
			{
				parentDict.UpgradeOpen();
				DBDictionary dBDictionary = new DBDictionary();
				this.AcadDictionaryID = parentDict.SetAt(base.Name, dBDictionary);
				dBDictionary.DisableUndoRecording(true);
				t.AddNewlyCreatedDBObject(dBDictionary, true);
				DocUtil.WriteXRecord(t, dBDictionary, "AutoCADID", (DxfCode)330, base.RasterObjectId);
				DocUtil.WriteXRecord(t, dBDictionary, "ServiceFullName", (DxfCode)1, this.ServiceFullName);
				DocUtil.WriteXRecord(t, dBDictionary, "ConnectionName", (DxfCode)1, base.ConnectionName);
				DocUtil.WriteXRecord(t, dBDictionary, "ConnectionURL", (DxfCode)1, base.ConnectionURL);
				DocUtil.WriteXRecord(t, dBDictionary, "UserName", (DxfCode)1, base.UserName);
				if (base.Dynamic)
				{
					Xrecord xrecord = new Xrecord();
					xrecord.Data=(new ResultBuffer(new TypedValue[]
					{
						new TypedValue(290, true)
					}));
					dBDictionary.SetAt("Dynamic", xrecord);
					xrecord.DisableUndoRecording(true);
					t.AddNewlyCreatedDBObject(xrecord, true);
				}
				DocUtil.WriteBoundingBox(t, dBDictionary, base.BoundaryExtent);
				DocUtil.WriteXRecord(t, dBDictionary, "ImageFormat", (DxfCode)1, base.ExportOptions.Format);
				result = dBDictionary;
			}
			catch (Exception ex)
			{
				string arg_122_0 = ex.Message;
				result = null;
			}
			return result;
		}

		protected void Read(ObjectId id, Transaction t)
		{
			try
			{
				DBDictionary dBDictionary = (DBDictionary)t.GetObject(this.AcadDictionaryID, 0);
				base.RasterObjectId = (ObjectId)DocUtil.ReadXRecord(t, dBDictionary, (DxfCode)330, "AutoCADID");
				this.ServiceFullName = (string)DocUtil.ReadXRecord(t, dBDictionary, (DxfCode)1, "ServiceFullName");
				base.ConnectionName = (string)DocUtil.ReadXRecord(t, dBDictionary, (DxfCode)1, "ConnectionName");
				base.ConnectionURL = (string)DocUtil.ReadXRecord(t, dBDictionary, (DxfCode)1, "ConnectionURL");
				base.UserName = (string)DocUtil.ReadXRecord(t, dBDictionary, (DxfCode)1, "UserName");
				base.BoundaryExtent = DocUtil.ReadBoundingBox(t, dBDictionary);
				base.ExportOptions = new AGSExportOptions();
				this.InitExportOptions();
				base.ExportOptions.Format = (string)DocUtil.ReadXRecord(t, dBDictionary, (DxfCode)1, "ImageFormat");
				base.ExportOptions.BoundingBox = new Extent(base.BoundaryExtent);
				base.ExportOptions.OutputWKT = MSCPrj.ReadWKT(base.ParentDataset.ParentDocument);
				try
				{
					base.Dynamic = false;
					if (dBDictionary.Contains("Dynamic"))
					{
						Xrecord xrecord = (Xrecord)t.GetObject(dBDictionary.GetAt("Dynamic"), 0);
						TypedValue[] array = xrecord.Data.AsArray();
						for (int i = 0; i < array.Length; i++)
						{
							TypedValue typedValue = array[i];
							if (typedValue.TypeCode == 290)
							{
								base.Dynamic = (0 != Convert.ToInt16(typedValue.Value));
							}
						}
					}
					base.ExportOptions.Dynamic = base.Dynamic;
				}
				catch
				{
					base.ExportOptions.Dynamic = false;
				}
				if (dBDictionary.Contains("ESRI_Layers"))
				{
					DBDictionary dBDictionary2 = (DBDictionary)t.GetObject(dBDictionary.GetAt("ESRI_Layers"), 0);
					using (DbDictionaryEnumerator enumerator = dBDictionary2.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							DBDictionaryEntry current = enumerator.Current;
							MSCMapLayer mSCMapLayer = new MSCMapLayer(t, current.Key, current.Value);
							mSCMapLayer.ParentMap = this;
							if (mSCMapLayer.Layers != null)
							{
								foreach (MSCMapLayer current2 in mSCMapLayer.Layers)
								{
									current2.ParentMap = this;
								}
							}
							this.Layers.Add(mSCMapLayer);
						}
					}
				}
				if (this.ReconnectService())
				{
					base.RefreshConnectedService();
				}
			}
			catch
			{
			}
		}

		public void RemoveDictionary()
		{
			Database database = AfaDocData.ActiveDocData.Document.Database;
			try
			{
				using (Transaction transaction = database.TransactionManager.StartTransaction())
				{
					this.RemoveDictionary(database, transaction);
					transaction.Commit();
				}
			}
			catch
			{
			}
		}

		private void RemoveDictionary(Database db, Transaction t)
		{
			try
			{
				DBDictionary dBDictionary = base.ParentDataset.Open(db, t, this.DictionaryName, (OpenMode)1);
				if (dBDictionary.Contains(this.AcadDictionaryID))
				{
					dBDictionary.Remove(this.AcadDictionaryID);
					DBObject @object = t.GetObject(this.AcadDictionaryID, (OpenMode)1);
					@object.Erase();
				}
			}
			catch
			{
			}
		}

		public bool DeleteService()
		{
			base.StopReactors();
			base.StopReactors();
			Database database = base.ParentDataset.ParentDocument.Database;
			Document parentDocument = base.ParentDataset.ParentDocument;
			bool result;
			try
			{
				using (base.ParentDataset.ParentDocument.LockDocument((DocumentLockMode)20, null, null, false))
				{
					var  transactionManager = base.ParentDataset.ParentDocument.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						RasterImage rasterImage = (RasterImage)transaction.GetObject(base.RasterObjectId, (OpenMode)1);
						rasterImage.DisableUndoRecording(true);
						ObjectId imageDefId = rasterImage.ImageDefId;
						try
						{
							RasterImageDef rasterImageDef = (RasterImageDef)transaction.GetObject(imageDefId, (OpenMode)1);
							rasterImageDef.DisableUndoRecording(true);
							rasterImageDef.Unload(true);
							try
							{
								File.Delete(rasterImageDef.SourceFileName);
								if (App.TempFiles.Contains(rasterImageDef.SourceFileName))
								{
									App.TempFiles.Remove(rasterImageDef.SourceFileName);
								}
							}
							catch
							{
							}
							if (!rasterImageDef.IsErased)
							{
								rasterImageDef.Erase();
							}
							ObjectId imageDictionary = RasterImageDef.GetImageDictionary(database);
							DBDictionary dBDictionary = (DBDictionary)transaction.GetObject(imageDictionary, (OpenMode)1);
							dBDictionary.Remove(imageDefId);
							if (!rasterImage.IsErased)
							{
								rasterImage.Erase(false);
							}
							this.RemoveDictionary(database, transaction);
						}
						catch
						{
						}
						parentDocument.TransactionManager.QueueForGraphicsFlush();
						parentDocument.TransactionManager.FlushGraphics();
						parentDocument.Editor.UpdateScreen();
						transaction.Commit();
						base.ParentDataset.MapServices.Remove(base.Name);
						MSCDataset.SetDefaultActiveRasterServices();
						result = true;
					}
				}
			}
			catch
			{
				MessageBox.Show("Error in MSCMapService.DeleteService");
				result = false;
			}
			return result;
		}

		private void AppendVisibleChildLayer(MSCMapLayer parentLayer, ref List<int> layerIdList)
		{
			foreach (MSCMapLayer current in parentLayer.Layers)
			{
				if (current.Visible)
				{
					if (current.ChildLayerIDs.Count > 0)
					{
						this.AppendVisibleChildLayer(current, ref layerIdList);
					}
					else
					{
						layerIdList.Add(current.ID);
					}
				}
			}
		}

		public List<int> BuildVisibleFeatureLayerList()
		{
			List<int> list = new List<int>();
			foreach (MSCMapLayer current in this.Layers)
			{
				if (current.Visible)
				{
					if (current.ChildLayerIDs.Count > 0)
					{
						this.AppendVisibleChildLayer(current, ref list);
					}
					else
					{
						list.Add(current.ID);
					}
				}
			}
			return list;
		}

		public bool InitializeIdentify(Extent ext)
		{
			List<int> mapLayers = this.BuildVisibleFeatureLayerList();
			AGSMapService aGSMapService = (AGSMapService)this.ParentService;
			if (!aGSMapService.Identify(base.ExportOptions, ext, mapLayers))
			{
				return false;
			}
			foreach (MSCMapLayer current in this.Layers)
			{
				if (current.Visible)
				{
					AGSMapLayer aGSMapLayer = aGSMapService.FindMapLayer(current.ID);
					if (aGSMapLayer != null)
					{
						current.IdentifyResults = aGSMapLayer.IdentifyResults;
						this.UpdateChildIdentifyResults(current);
					}
				}
			}
			return true;
		}

		private void UpdateChildIdentifyResults(MSCMapLayer parentLayer)
		{
			AGSMapService aGSMapService = (AGSMapService)this.ParentService;
			foreach (MSCMapLayer current in parentLayer.Layers)
			{
				if (current.Visible)
				{
					AGSMapLayer aGSMapLayer = aGSMapService.FindMapLayer(current.ID);
					if (aGSMapLayer != null)
					{
						current.IdentifyResults = aGSMapLayer.IdentifyResults;
						this.UpdateChildIdentifyResults(current);
					}
				}
			}
		}
	}
}
