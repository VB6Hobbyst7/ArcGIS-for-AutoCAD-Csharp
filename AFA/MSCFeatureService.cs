using AFA.Resources;
using ArcGIS10Types;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using Exception = Autodesk.AutoCAD.Runtime.Exception;

namespace AFA
{
    public class MSCFeatureService : MSCFeatureClass
	{
		private class TrackedEntity
		{
			private ObjectId ID;

			private Point3dCollection GripPoints = new Point3dCollection();

			private IntegerCollection Snaps = new IntegerCollection();

			private IntegerCollection GeometryIDs = new IntegerCollection();

			private ObjectId layerID;

			public TrackedEntity(Entity ent)
			{
				this.ID = ent.Id;
				ent.GetGripPoints(this.GripPoints, this.Snaps, this.GeometryIDs);
				this.layerID = ent.LayerId;
			}

			private static bool GripsModified(Point3dCollection a, Point3dCollection b)
			{
				bool result;
				try
				{
					if (a.Count != b.Count)
					{
						result = true;
					}
					else if (a.Count == 0)
					{
						result = true;
					}
					else
					{
						for (int i = 0; i < a.Count; i++)
						{
							Point3d point3d = a[(i)];
							Point3d point3d2 = b[(i)];
							if (point3d != point3d2)
							{
								result = true;
								return result;
							}
						}
						result = false;
					}
				}
				catch
				{
					result = true;
				}
				return result;
			}

			public static bool AreEqual(MSCFeatureService.TrackedEntity a, MSCFeatureService.TrackedEntity b, bool checkLayer)
			{
				return (checkLayer && !a.layerID.Equals(b.layerID)) || !MSCFeatureService.TrackedEntity.GripsModified(a.GripPoints, b.GripPoints);
			}
		}

		protected new string DictionaryName = "ESRI_FEATURESERVICES";

		private Dictionary<ObjectId, MSCFeatureService.TrackedEntity> _Tracking = new Dictionary<ObjectId, MSCFeatureService.TrackedEntity>();

		private List<int> DeletedOIDList;

		private List<ObjectId> NewObjectIdList;

		public AGSFeatureService ParentService
		{
			get;
			set;
		}

		public string LayerName
		{
			get;
			set;
		}

		public int ServiceLayerID
		{
			get;
			set;
		}

		public bool QueryOnly
		{
			get;
			set;
		}

		public bool HasZ
		{
			get;
			set;
		}

		public bool HasM
		{
			get;
			set;
		}

		public AGSExportOptions ExportOptions
		{
			get;
			set;
		}

		public string ServiceFullName
		{
			get;
			set;
		}

		public string ConnectionURL
		{
			get;
			set;
		}

		public IDictionary<string, object> Properties
		{
			get
			{
				return this.InitializeProperties();
			}
			set
			{
				this.UpdateProperties(value);
			}
		}

		private string UserName
		{
			get;
			set;
		}

		private string ConnectionName
		{
			get;
			set;
		}

		public string ErrorMessage
		{
			get;
			set;
		}

		public List<ObjectId> ModifiedObjects
		{
			get;
			set;
		}

		public Dictionary<ObjectId, int> OriginalServiceIDs
		{
			get;
			set;
		}

		public DataObject Prototype
		{
			get;
			set;
		}

		public bool HasCommitErrors
		{
			get;
			set;
		}

		public Dictionary<ObjectId, EditResult> CommitErrors
		{
			get;
			set;
		}

		public bool IsDirtyAfterCommit
		{
			get;
			set;
		}

		public MSCFeatureService(MSCDataset parentDataset, AGSFeatureService parentFS, int layerID, AGSExportOptions eo) : base(parentDataset)
		{
			this.ParentService = (AGSFeatureService)Utility.CloneObject(parentFS);
			this.ServiceLayerID = layerID;
			this.OriginalServiceIDs = new Dictionary<ObjectId, int>();
			this.ModifiedObjects = new List<ObjectId>();
			this.HasCommitErrors = false;
			this.IsDirtyAfterCommit = false;
			try
			{
				this.ConnectionURL = this.ParentService.Parent.Soap_URL;
				this.UserName = this.ParentService.Parent.UserName;
				this.QueryOnly = !eo.Dynamic;
				base.ReadOnly = true;
				this.ServiceFullName = this.ParentService.FullName;
				this.ConnectionURL = this.ParentService.Parent.Soap_URL;
				this.UserName = this.ParentService.Parent.UserName;
				this.ConnectionName = this.ParentService.Parent.Name;
				this.ExportOptions = (AGSExportOptions)Utility.CloneObject(eo);
				this.StartReactors();
			}
			catch (SystemException ex)
			{
				this.ErrorMessage = ex.Message;
			}
		}

		public MSCFeatureService(Transaction t, MSCDataset parent, string name, ObjectId id) : base(name, parent, id, t)
		{
			base.ReadOnly = true;
			this.QueryOnly = true;
			this.OriginalServiceIDs = new Dictionary<ObjectId, int>();
			this.ModifiedObjects = new List<ObjectId>();
			this.HasCommitErrors = false;
			this.IsDirtyAfterCommit = false;
			this.HasZ = false;
			this.HasM = false;
			base.Read(id, t);
			this.Read(id, t);
			this.StartReactors();
		}

		public void Write()
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
				this.ErrorMessage = AfaStrings.ErrorUpdatingFeatureService;
			}
		}

		public override void Write(Database db, Transaction t)
		{
			try
			{
				DBDictionary dBDictionary = base.ParentDataset.Open(db, t, this.DictionaryName, (OpenMode)1);
				dBDictionary.DisableUndoRecording(true);
				if (base.ParentFC != null)
				{
					dBDictionary = base.ParentFC.Open(t, (OpenMode)1);
				}
				MSCFeatureClass.UpdateDefaultsFromSubtype(this);
				DBDictionary dBDictionary2 = this.WriteFCDictionary(dBDictionary, t);
				dBDictionary2.DisableUndoRecording(true);
				if (dBDictionary2 != null && base.SubTypes.Count > 0)
				{
					DBDictionary dBDictionary3 = new DBDictionary();
					dBDictionary2.SetAt("ESRI_Subtypes", dBDictionary3);
					dBDictionary3.DisableUndoRecording(true);
					t.AddNewlyCreatedDBObject(dBDictionary3, true);
					foreach (MSCFeatureClass current in base.SubTypes)
					{
						current.WriteFCDictionary(dBDictionary3, t);
					}
				}
			}
			catch
			{
			}
		}

		protected DBDictionary WriteFCDictionary(DBDictionary parentDict, Transaction t)
		{
			DBDictionary result;
			try
			{
				parentDict.UpgradeOpen();
				DBDictionary dBDictionary = new DBDictionary();
				base.Id = parentDict.SetAt(base.Name, dBDictionary);
				dBDictionary.DisableUndoRecording(true);
				t.AddNewlyCreatedDBObject(dBDictionary, true);
				Xrecord xrecord = new Xrecord();
				xrecord.Data=(new ResultBuffer(new TypedValue[]
				{
					new TypedValue(1, MSCFeatureClass.GetGeomString(base.GeometryType))
				}));
				dBDictionary.SetAt("FeatureType", xrecord);
				xrecord.DisableUndoRecording(true);
				t.AddNewlyCreatedDBObject(xrecord, true);
				Xrecord xrecord2 = new Xrecord();
				xrecord2.Data=(base.Query);
				dBDictionary.SetAt("FeatureQuery", xrecord2);
				xrecord2.DisableUndoRecording(true);
				t.AddNewlyCreatedDBObject(xrecord2, true);
				CadField.WriteNewAttributeDictionary(base.Fields, dBDictionary, t);
				if (base.ReadOnly)
				{
					DocUtil.WriteXRecord(t, dBDictionary, "ESRI_ReadOnly", (DxfCode)290, true);
				}
				if (!string.IsNullOrEmpty(base.TypeField))
				{
					Xrecord xrecord3 = new Xrecord();
					xrecord3.Data=(new ResultBuffer(new TypedValue[]
					{
						new TypedValue(1, base.TypeField)
					}));
					dBDictionary.SetAt("SubtypeField", xrecord3);
					xrecord3.DisableUndoRecording(true);
					t.AddNewlyCreatedDBObject(xrecord3, true);
				}
				DocUtil.WriteXRecord(t, dBDictionary, "CadLayerName", (DxfCode)1, this.LayerName);
				DocUtil.WriteXRecord(t, dBDictionary, "ServiceLayerId", (DxfCode)90, this.ServiceLayerID);
				DocUtil.WriteXRecord(t, dBDictionary, "QueryOnly", (DxfCode)290, this.QueryOnly);
				DocUtil.WriteXRecord(t, dBDictionary, "HasZ", (DxfCode)290, this.HasZ);
				DocUtil.WriteXRecord(t, dBDictionary, "HasM", (DxfCode)290, this.HasM);
				DocUtil.WriteXRecord(t, dBDictionary, "ServiceFullName", (DxfCode)1, this.ServiceFullName);
				DocUtil.WriteXRecord(t, dBDictionary, "ConnectionName", (DxfCode)1, this.ConnectionName);
				DocUtil.WriteXRecord(t, dBDictionary, "ConnectionURL", (DxfCode)1, this.ConnectionURL);
				DocUtil.WriteXRecord(t, dBDictionary, "UserName", (DxfCode)1, this.UserName);
				DocUtil.WriteXRecord(t, dBDictionary, "Where", (DxfCode)1, this.ExportOptions.WhereClause);
				DocUtil.WriteBoundingBox(t, dBDictionary, this.ExportOptions.BoundingBox);
				this.WriteOriginalServiceIds(t, dBDictionary, "OriginalIDs");
				this.WriteModifiedIds(t, dBDictionary, "ModifiedIDs");
				result = dBDictionary;
			}
			catch (Exception ex)
			{
				string arg_258_0 = ex.Message;
				result = null;
			}
			return result;
		}

		private void WriteModifiedIds(Transaction t, DBDictionary dict, string key)
		{
			if (this.ModifiedObjects == null)
			{
				return;
			}
			if (this.ModifiedObjects.Count == 0)
			{
				return;
			}
			ResultBuffer resultBuffer = new ResultBuffer();
			foreach (ObjectId current in this.ModifiedObjects)
			{
				TypedValue typedValue = new TypedValue(330, current);
				resultBuffer.Add(typedValue);
			}
			DocUtil.WriteXRecord(t, dict, key, resultBuffer);
		}

		private void WriteOriginalServiceIds(Transaction t, DBDictionary dict, string key)
		{
			if (this.OriginalServiceIDs == null)
			{
				return;
			}
			if (this.OriginalServiceIDs.Count == 0)
			{
				return;
			}
			ResultBuffer resultBuffer = new ResultBuffer();
			foreach (KeyValuePair<ObjectId, int> current in this.OriginalServiceIDs)
			{
				TypedValue typedValue = new TypedValue(330, current.Key);
				TypedValue typedValue2 = new TypedValue(90, current.Value);
				resultBuffer.Add(typedValue);
				resultBuffer.Add(typedValue2);
			}
			DocUtil.WriteXRecord(t, dict, key, resultBuffer);
		}

		private void ReadOriginalServiceIds(Transaction t, DBDictionary dict, string key)
		{
			this.OriginalServiceIDs = new Dictionary<ObjectId, int>();
			if (dict.Contains(key))
			{
				Xrecord xrecord = (Xrecord)t.GetObject(dict.GetAt(key), 0);
				TypedValue[] array = xrecord.Data.AsArray();
				for (int i = 0; i < array.Length; i++)
				{
					ObjectId key2 = ObjectId.Null;
					int num = -1;
					if (array[i].TypeCode == 330)
					{
						key2 = (ObjectId)array[i].Value;
					}
					i++;
					if (array[i].TypeCode == 90)
					{
						num = (int)array[i].Value;
					}
					if (num != -1)
					{
						this.OriginalServiceIDs.Add(key2, num);
					}
				}
			}
		}

		private void ReadModifiedIds(Transaction t, DBDictionary dict, string key)
		{
			this.ModifiedObjects = new List<ObjectId>();
			if (dict.Contains(key))
			{
				Xrecord xrecord = (Xrecord)t.GetObject(dict.GetAt(key), 0);
				TypedValue[] array = xrecord.Data.AsArray();
				for (int i = 0; i < array.Length; i++)
				{
					ObjectId item = ObjectId.Null;
					if (array[i].TypeCode == 330)
					{
						item = (ObjectId)array[i].Value;
						this.ModifiedObjects.Add(item);
					}
				}
			}
		}

		protected override void Read(ObjectId id, Transaction t)
		{
			try
			{
				DBDictionary dict = (DBDictionary)t.GetObject(base.Id, 0);
				this.LayerName = (string)DocUtil.ReadXRecord(t, dict, (DxfCode)1, "CadLayerName");
				this.ServiceLayerID = (int)DocUtil.ReadXRecord(t, dict, (DxfCode)90, "ServiceLayerId");
				this.ServiceFullName = (string)DocUtil.ReadXRecord(t, dict, (DxfCode)1, "ServiceFullName");
				this.ConnectionName = (string)DocUtil.ReadXRecord(t, dict, (DxfCode)1, "ConnectionName");
				this.ConnectionURL = (string)DocUtil.ReadXRecord(t, dict, (DxfCode)1, "ConnectionURL");
				this.UserName = (string)DocUtil.ReadXRecord(t, dict, (DxfCode)1, "UserName");
				this.HasZ = (0 != Convert.ToInt32(DocUtil.ReadXRecord(t, dict, (DxfCode)290, "HasZ")));
				this.HasM = (0 != Convert.ToInt32(DocUtil.ReadXRecord(t, dict, (DxfCode)290, "HasM")));
				this.QueryOnly = (0 != Convert.ToInt32(DocUtil.ReadXRecord(t, dict, (DxfCode)290, "QueryOnly")));
				this.ExportOptions = new AGSExportOptions();
				this.ExportOptions.WhereClause = (string)DocUtil.ReadXRecord(t, dict, (DxfCode)1, "Where");
				this.ExportOptions.BoundingBox = DocUtil.ReadBoundingBox(t, dict);
				this.ExportOptions.Dynamic = !this.QueryOnly;
				this.ReadOriginalServiceIds(t, dict, "OriginalIDs");
				this.ReadModifiedIds(t, dict, "ModifiedIDs");
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
				if (base.ParentFC != null)
				{
					dBDictionary = base.ParentFC.Open(t, (OpenMode)1);
				}
				if (dBDictionary.Contains(base.Id))
				{
					dBDictionary.Remove(base.Id);
					DBObject @object = t.GetObject(base.Id, (OpenMode)1);
					@object.Erase();
				}
			}
			catch
			{
			}
		}

		private IDictionary<string, object> InitializeProperties()
		{
			return new Dictionary<string, object>();
		}

		private void UpdateProperties(IDictionary<string, object> newProps)
		{
		}

		public bool Refresh(ref string resultString)
		{
			Database database = base.ParentDataset.ParentDocument.Database;
			bool result;
			try
			{
				resultString = resultString + base.Name + " : ";
				if (!this.ReconnectFeatureService())
				{
					resultString += AfaStrings.UnableToConnectFeatureService;
					ErrorReport.ShowErrorMessage(AfaStrings.UnableToConnectFeatureService + base.Name);
					result = false;
				}
				else
				{
					AGSFeatureService parentService = this.ParentService;
					if (!parentService.Challenge())
					{
						resultString += AfaStrings.UnableToConnectFeatureService;
						ErrorReport.ShowErrorMessage(AfaStrings.UnableToConnectFeatureService + base.Name);
						result = false;
					}
					else
					{
						database.DisableUndoRecording(true);
						this.SetLayerLock(false);
						this.EraseFeatures();
						this.OriginalServiceIDs.Clear();
						this.ModifiedObjects.Clear();
						MSCFeatureService mSCFeatureService = null;
						base.ParentDataset.FeatureServices.TryGetValue(base.Name, out mSCFeatureService);
						if (!parentService.RefreshService(ref mSCFeatureService))
						{
							if (this.QueryOnly)
							{
								this.SetLayerLock(true);
							}
							mSCFeatureService.Write();
							database.DisableUndoRecording(false);
							if (string.IsNullOrEmpty(parentService.ErrorMessage))
							{
								resultString += AfaStrings.UnableToConnectFeatureService;
							}
							else
							{
								resultString += parentService.ErrorMessage;
							}
							result = false;
						}
						else
						{
							if (this.QueryOnly)
							{
								this.SetLayerLock(true);
							}
							mSCFeatureService.Write();
							resultString += AfaStrings.RefreshSucceeded;
							database.DisableUndoRecording(false);
							result = true;
						}
					}
				}
			}
			catch
			{
				database.DisableUndoRecording(false);
				resultString += AfaStrings.ErrorRefreshingFeatures;
				this.ErrorMessage = AfaStrings.ErrorRefreshingFeatures;
				result = false;
			}
			return result;
		}

		public bool ReconnectFeatureService()
		{
			bool result;
			try
			{
				if (this.ParentService != null)
				{
					result = this.ParentService.Challenge();
				}
				else if (string.IsNullOrEmpty(this.ConnectionURL))
				{
					result = false;
				}
				else
				{
					AGSConnection connection = AGSConnection.GetConnection(this.ConnectionURL, this.ConnectionName, this.UserName);
					if (connection != null)
					{
						if (!connection.ConnectionFailed)
						{
							this.ParentService = new AGSFeatureService(this.ServiceFullName, this.ConnectionURL, connection);
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
						this.ErrorMessage = AfaStrings.ErrorInitializingFeatureService;
						result = false;
					}
				}
			}
			catch
			{
				this.ErrorMessage = AfaStrings.ErrorInitializingFeatureService;
				result = false;
			}
			return result;
		}

		public bool PrepareToCommit(ref string rString)
		{
			bool result;
			try
			{
				if (!this.ReconnectFeatureService())
				{
					ErrorReport.ShowErrorMessage(AfaStrings.UnableToConnectFeatureService + base.Name);
					rString += AfaStrings.ErrorConnectingToServer;
					result = false;
				}
				else
				{
					this.DeletedOIDList = new List<int>();
					this.NewObjectIdList = new List<ObjectId>();
					this.BuildModifiedObjectLists(ref this.DeletedOIDList, ref this.NewObjectIdList);
					rString = rString + base.Name + " : ";
					if (this.DeletedOIDList.Count == 0 && this.NewObjectIdList.Count == 0 && this.ModifiedObjects.Count == 0)
					{
						rString += AfaStrings.NoFeaturesFoundToUpdate;
						result = true;
					}
					else
					{
						string text = rString;
						rString = string.Concat(new string[]
						{
							text,
							"\r\n - ",
							this.ModifiedObjects.Count.ToString(),
							" ",
							AfaStrings.FeaturesToBeUpdated,
							"\r\n - ",
							this.NewObjectIdList.Count.ToString(),
							" ",
							AfaStrings.FeaturesToBeAdded,
							"\r\n - ",
							this.DeletedOIDList.Count.ToString(),
							" ",
							AfaStrings.FeaturesToBeDeleted
						});
						result = true;
					}
				}
			}
			catch
			{
				rString += AfaStrings.ErrorInPreparingToCommit;
				result = false;
			}
			return result;
		}

		public bool CommitEdits(ref string rString)
		{
			bool result;
			try
			{
				int chunkSize = 100;
				string text = "";
				if (this.DeletedOIDList == null || this.NewObjectIdList == null)
				{
					this.PrepareToCommit(ref text);
				}
				this.CommitErrors = new Dictionary<ObjectId, EditResult>();
				this.HasCommitErrors = false;
				this._Tracking.Clear();
				rString = rString + base.Name + " : ";
				if (this.DeletedOIDList.Count == 0 && this.NewObjectIdList.Count == 0 && this.ModifiedObjects.Count == 0)
				{
					rString += AfaStrings.NoFeaturesFoundToUpdate;
					result = true;
				}
				else if (!this.ReconnectFeatureService())
				{
					ErrorReport.ShowErrorMessage(AfaStrings.UnableToConnectFeatureService + base.Name);
					rString += AfaStrings.ErrorConnectingToServer;
					result = false;
				}
				else
				{
					List<DataObject> list = null;
					Dictionary<ObjectId, DataObject> dictionary = null;
					this.ParentService.BeginUpdate();
					if (this.NewObjectIdList.Count > 0)
					{
						string hostTypeField = "";
						this.ParentService.InitializePrototypes(this.ServiceLayerID, this, ref hostTypeField);
						dictionary = this.BuildNewFeatures(this.NewObjectIdList, hostTypeField, this.HasZ, this.HasM);
					}
					if (this.ModifiedObjects.Count > 0)
					{
						int[] fidList = this.BuildModifiedObjectIdList();
						chunkSize = this.ModifiedObjects.Count;
						string hostOIDField = "";
						string hostTypeField2 = "";
						List<DataObject> list2 = this.ParentService.QueryByID(base.ParentDataset.ParentDocument, this.ServiceLayerID, this.ExportOptions, fidList, ref hostOIDField, ref hostTypeField2, ref chunkSize);
						if (list2 != null)
						{
							list = this.BuildModifiedFeatures(list2, hostOIDField, hostTypeField2, this.HasZ, this.HasM);
						}
						else
						{
							rString += this.ParentService.ErrorMessage;
						}
					}
					int num = 0;
					int num2 = 0;
					int num3 = 0;
					int num4 = 0;
					if (this.ModifiedObjects.Count > 0)
					{
						List<EditResult> results = this.ParentService.UpdateFeatures(base.ParentDataset.ParentDocument, this.ServiceLayerID, list, chunkSize);
						num2 = this.UpdateModifiedObjectList(results);
						if (num2 != list.Count)
						{
							this.HasCommitErrors = true;
							num4 += list.Count - num2;
						}
						if (num2 > 0)
						{
							this.IsDirtyAfterCommit = true;
						}
					}
					if (this.NewObjectIdList.Count > 0)
					{
						List<EditResult> results2 = this.ParentService.AddNewFeatures(base.ParentDataset.ParentDocument, this.ServiceLayerID, dictionary.Values.ToArray<DataObject>(), chunkSize);
						num = this.UpdatedAddedFeatures(results2, dictionary);
						if (num != dictionary.Count)
						{
							this.HasCommitErrors = true;
							num4 += dictionary.Count - num;
						}
						if (num > 0)
						{
							this.IsDirtyAfterCommit = true;
						}
					}
					if (this.DeletedOIDList.Count > 0)
					{
						List<EditResult> results3 = this.ParentService.DeleteFeatures(base.ParentDataset.ParentDocument, this.ServiceLayerID, this.DeletedOIDList);
						num3 = this.UpdateDeletedFeatures(results3);
					}
					string text2 = rString;
					rString = string.Concat(new string[]
					{
						text2,
						"\r\n - ",
						num2.ToString(),
						" ",
						AfaStrings.FeaturesUpdated,
						"\r\n - ",
						num.ToString(),
						" ",
						AfaStrings.FeaturesAdded,
						"\r\n - ",
						num3.ToString(),
						" ",
						AfaStrings.FeaturesDeleted,
						"\r\n - ",
						num4.ToString(),
						" ",
						AfaStrings.FeaturesFailed
					});
					this.ParentService.EndUpdate();
					this.Write();
					this.NewObjectIdList = null;
					this.DeletedOIDList = null;
					result = !this.HasCommitErrors;
				}
			}
			catch
			{
				this.ParentService.EndUpdate();
				ErrorReport.ShowErrorMessage("DEBUG: Error in updating");
				result = false;
			}
			return result;
		}

		private DataObject GetPrototype(Transaction t, DBObject dbObj)
		{
			DataObject prototype;
			try
			{
				if (base.SubTypes.Count > 0)
				{
					if (dbObj is Entity)
					{
						Entity entity = (Entity)dbObj;
						MSCFeatureClassSubType subTypeFromLayer = base.GetSubTypeFromLayer(entity.Layer);
						if (subTypeFromLayer != null)
						{
							prototype = subTypeFromLayer.Prototype;
							return prototype;
						}
					}
					else if (dbObj is Group)
					{
						Group group = (Group)dbObj;
						ObjectId[] allEntityIds = group.GetAllEntityIds();
						DBObject @object = t.GetObject(allEntityIds[0], 0);
						prototype = this.GetPrototype(t, @object);
						return prototype;
					}
				}
				prototype = this.Prototype;
			}
			catch
			{
				prototype = this.Prototype;
			}
			return prototype;
		}

		private Dictionary<ObjectId, DataObject> BuildNewFeatures(List<ObjectId> newEntities, string hostTypeField, bool hasZ, bool hasM)
		{
			Database database = base.ParentDataset.ParentDocument.Database;
			Dictionary<ObjectId, DataObject> dictionary = new Dictionary<ObjectId, DataObject>();
			Dictionary<ObjectId, DataObject> result;
			try
			{
				string str = MSCPrj.ReadWKT(base.ParentDataset.ParentDocument);
				SpatialReference spRef = AGSSpatialReference.SpRefFromString(str);
				using (base.ParentDataset.ParentDocument.LockDocument((DocumentLockMode)32, null, null, false))
				{
					var  transactionManager = database.TransactionManager;
					database.DisableUndoRecording(true);
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						foreach (ObjectId current in newEntities)
						{
							if (current != ObjectId.Null)
							{
								DBObject @object = transaction.GetObject(current, 0);
								DataObject prototype = this.GetPrototype(transaction, @object);
								DataObject dataObject = CAD2GIS.UpdateGraphicFeature(transaction, prototype, @object, spRef, this, hasZ, hasM);
								if (base.SubTypes.Count > 0)
								{
									this.SetSubtypeValue(transaction, dataObject, @object, hostTypeField);
								}
								if (dataObject != null)
								{
									dictionary.Add(current, dataObject);
								}
							}
						}
						transaction.Commit();
						database.DisableUndoRecording(false);
					}
				}
				result = dictionary;
			}
			catch
			{
				database.DisableUndoRecording(false);
				result = dictionary;
			}
			return result;
		}

		private List<DataObject> BuildModifiedFeatures(List<DataObject> serverDataObjects, string hostOIDField, string hostTypeField, bool hasZ, bool hasM)
		{
			Database database = base.ParentDataset.ParentDocument.Database;
			List<DataObject> list = new List<DataObject>();
			List<DataObject> result;
			try
			{
				string str = MSCPrj.ReadWKT(base.ParentDataset.ParentDocument);
				SpatialReference spRef = AGSSpatialReference.SpRefFromString(str);
				using (base.ParentDataset.ParentDocument.LockDocument((DocumentLockMode)32, null, null, false))
				{
					var transactionManager = database.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						foreach (DataObject current in serverDataObjects)
						{
							int objectId = AGSFeatureService.GetObjectId(hostOIDField, current.Properties.PropertyArray);
							ObjectId objectId2 = ObjectId.Null;
							foreach (KeyValuePair<ObjectId, int> current2 in this.OriginalServiceIDs)
							{
								if (objectId.Equals(current2.Value))
								{
									objectId2 = current2.Key;
									break;
								}
							}
							if (objectId2 != ObjectId.Null)
							{
								DBObject @object = transaction.GetObject(objectId2, 0);
								DataObject dataObject = CAD2GIS.UpdateGraphicFeature(transaction, current, @object, spRef, this, hasZ, hasM);
								if (base.SubTypes.Count > 0)
								{
									this.SetSubtypeValue(transaction, dataObject, @object, hostTypeField);
								}
								if (dataObject != null)
								{
									list.Add(dataObject);
								}
							}
						}
						transaction.Commit();
					}
				}
				result = list;
			}
			catch
			{
				result = list;
			}
			return result;
		}

		private void SetSubtypeValue(Transaction t, DataObject d, DBObject cadObj, string TypeFieldName)
		{
			object obj = null;
			string text = null;
			if (cadObj is Entity)
			{
				Entity entity = (Entity)cadObj;
				text = entity.Layer;
			}
			else if (cadObj is Group)
			{
				Group group = (Group)cadObj;
				ObjectId[] allEntityIds = group.GetAllEntityIds();
				DBObject @object = t.GetObject(allEntityIds[0], 0);
				if (@object is Entity)
				{
					Entity entity2 = (Entity)@object;
					text = entity2.Layer;
				}
			}
			if (text != null)
			{
				MSCFeatureClassSubType subTypeFromLayer = base.GetSubTypeFromLayer(text);
				if (subTypeFromLayer != null)
				{
					obj = subTypeFromLayer.TypeValue;
				}
			}
			if (obj != null)
			{
				PropertySetProperty[] propertyArray = d.Properties.PropertyArray;
				for (int i = 0; i < propertyArray.Length; i++)
				{
					PropertySetProperty propertySetProperty = propertyArray[i];
					if (propertySetProperty.Key == TypeFieldName)
					{
						propertySetProperty.Value = obj;
						return;
					}
				}
				return;
			}
			PropertySetProperty[] propertyArray2 = d.Properties.PropertyArray;
			for (int j = 0; j < propertyArray2.Length; j++)
			{
				PropertySetProperty propertySetProperty2 = propertyArray2[j];
				if (propertySetProperty2.Key == TypeFieldName)
				{
					propertySetProperty2.Value = 0;
					return;
				}
			}
		}

		private int[] BuildModifiedObjectIdList()
		{
			List<int> list = new List<int>();
			if (this.ModifiedObjects.Count > 0)
			{
				foreach (ObjectId current in this.ModifiedObjects)
				{
					list.Add(this.OriginalServiceIDs[current]);
				}
			}
			return list.ToArray();
		}

		private int UpdateDeletedFeatures(List<EditResult> results)
		{
			int num = 0;
			foreach (EditResult current in results)
			{
				ObjectId objectId = ObjectId.Null;
				foreach (KeyValuePair<ObjectId, int> current2 in this.OriginalServiceIDs)
				{
					if (current2.Value == current.OID)
					{
						objectId = current2.Key;
						break;
					}
				}
				if (!(objectId == ObjectId.Null) && current.Succeeded)
				{
					this.OriginalServiceIDs.Remove(objectId);
					num++;
				}
			}
			return num;
		}

		private int UpdateModifiedObjectList(List<EditResult> results)
		{
			int num = 0;
			foreach (EditResult current in results)
			{
				ObjectId objectId = ObjectId.Null;
				foreach (KeyValuePair<ObjectId, int> current2 in this.OriginalServiceIDs)
				{
					if (current2.Value == current.OID)
					{
						objectId = current2.Key;
					}
				}
				if (!(objectId == ObjectId.Null))
				{
					if (current.Succeeded)
					{
						this.ModifiedObjects.Remove(objectId);
						num++;
					}
					else
					{
						this.CommitErrors.Add(objectId, current);
					}
					try
					{
						string s = string.Format("{0}, {1}, {2}, {3}", new object[]
						{
							objectId.ToString(),
							current.OID.ToString(),
							current.Succeeded.ToString(),
							current.Description
						});
						DatasetLog.WriteString(base.ParentDataset.ParentDocument, s);
					}
					catch
					{
					}
				}
			}
			return num;
		}

		private int UpdatedAddedFeatures(List<EditResult> results, Dictionary<ObjectId, DataObject> addedObjects)
		{
			int num = 0;
			int result;
			try
			{
				ObjectId[] array = addedObjects.Keys.ToArray<ObjectId>();
				for (int i = 0; i < results.Count; i++)
				{
					EditResult editResult = results[i];
					if (editResult.Succeeded)
					{
						this.OriginalServiceIDs.Add(array[i], editResult.OID);
						num++;
					}
					else
					{
						this.CommitErrors.Add(array[i], editResult);
					}
					try
					{
						string s = string.Format("{0}, {1}, {2}, {3}", new object[]
						{
							array[i].ToString(),
							editResult.OID.ToString(),
							editResult.Succeeded.ToString(),
							editResult.Description
						});
						DatasetLog.WriteString(base.ParentDataset.ParentDocument, s);
					}
					catch
					{
					}
				}
				result = num;
			}
			catch
			{
				this.HasCommitErrors = true;
				result = num;
			}
			return result;
		}

		private List<ObjectId> BuildGroupObjectIds(ref List<ObjectId> insideGrp)
		{
			Database database = base.ParentDataset.ParentDocument.Database;
			List<ObjectId> list = new List<ObjectId>();
			List<ObjectId> result;
			try
			{
				using (base.ParentDataset.ParentDocument.LockDocument((DocumentLockMode)32, null, null, false))
				{
					var transactionManager = database.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						DBDictionary dBDictionary = (DBDictionary)transaction.GetObject(database.GroupDictionaryId, 0);
						using (DbDictionaryEnumerator enumerator = dBDictionary.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								DBDictionaryEntry current = enumerator.Current;
								if (this.OriginalServiceIDs.Keys.ToArray<ObjectId>().Contains(current.Value))
								{
									Group group = (Group)transaction.GetObject(current.Value, 0);
									if (group.NumEntities > 0)
									{
										list.Add(current.Value);
										if (insideGrp != null)
										{
											ObjectId[] allEntityIds = group.GetAllEntityIds();
											ObjectId[] array = allEntityIds;
											for (int i = 0; i < array.Length; i++)
											{
												ObjectId item = array[i];
												insideGrp.Add(item);
											}
										}
									}
								}
							}
						}
						transaction.Commit();
					}
				}
				result = list;
			}
			catch
			{
				result = list;
			}
			return result;
		}

		private void BuildModifiedObjectLists(ref List<int> deletedOIDList, ref List<ObjectId> newObjectIdList)
		{
			ObjectId[] array = base.GetFeatureIds();
			List<ObjectId> list = new List<ObjectId>();
			List<ObjectId> list2 = this.BuildGroupObjectIds(ref list);
			if (array == null)
			{
				array = new List<ObjectId>().ToArray();
			}
			foreach (ObjectId current in this.OriginalServiceIDs.Keys)
			{
				if (!array.Contains(current) && !list2.Contains(current))
				{
					int num = -1;
					if (this.OriginalServiceIDs.TryGetValue(current, out num) && num != -1)
					{
						deletedOIDList.Add(num);
						if (this.ModifiedObjects.Contains(current))
						{
							this.ModifiedObjects.Remove(current);
						}
					}
				}
			}
			ObjectId[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				ObjectId objectId = array2[i];
				if (!this.OriginalServiceIDs.ContainsKey(objectId) && !list.Contains(objectId))
				{
					newObjectIdList.Add(objectId);
					if (this.ModifiedObjects.Contains(objectId))
					{
						this.ModifiedObjects.Remove(objectId);
					}
				}
			}
		}

		public void EraseFeatures()
		{
			try
			{
				ObjectId[] entityIds = base.GetEntityIds();
				if (entityIds != null)
				{
					DocUtil.EraseEntities(base.ParentDataset.ParentDocument, entityIds);
				}
				DocUtil.EraseGroups(base.ParentDataset.ParentDocument, this.OriginalServiceIDs.Keys.ToArray<ObjectId>());
			}
			catch
			{
			}
		}

		public bool DeleteService()
		{
			this.StopReactors();
			Database database = base.ParentDataset.ParentDocument.Database;
			bool result;
			try
			{
				using (base.ParentDataset.ParentDocument.LockDocument((DocumentLockMode)20, null, null, false))
				{
					var transactionManager = database.TransactionManager;
					database.DisableUndoRecording(true);
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						this.SetLayerLock(false, database, transaction);
						this.EraseFeatures();
						this.RemoveDictionary(database, transaction);
						transaction.Commit();
						database.DisableUndoRecording(false);
						base.ParentDataset.RemoveFromFeatureServiceViewList(base.Name);
						if (base.ParentDataset.FeatureServiceViewList.Count == 0)
						{
							ArcGISRibbon.EnableFeatureServiceButtons(false);
						}
						result = true;
					}
				}
			}
			catch
			{
				database.DisableUndoRecording(false);
				result = false;
			}
			return result;
		}

		public MSCFeatureClass Disconnect()
		{
			MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
			Document parentDocument = docDataset.ParentDocument;
			Database database = parentDocument.Database;
			MSCFeatureClass result;
			try
			{
				Utility.InitializeStringList(this.LayerName);
				using (parentDocument.LockDocument())
				{
					database.DisableUndoRecording(true);
					using (Transaction transaction = parentDocument.TransactionManager.StartTransaction())
					{
						string baseName = this.LayerName.Substring(5);
						string name = base.Name;
						string text = DocUtil.GenerateUniqueLayerName(database, transaction, baseName);
						DocUtil.RenameCADLayer(transaction, database, this.LayerName, text);
						List<string> list = new List<string>();
						list.Add(text);
						MSCFeatureClass mSCFeatureClass = MSCDataset.AddSimpleFeatureClass(name, MSCFeatureClass.GetESRITypeCodeString(base.GeometryType), list, base.Fields, false, null);
						if (base.SubTypes.Count > 0)
						{
							mSCFeatureClass.TypeField = base.TypeField;
							foreach (MSCFeatureClassSubType current in base.SubTypes)
							{
								string suggestedSubTypeName = MSCDataset.GenerateUniqueName(current.Name);
								string baseName2 = current.CadLayerName.Substring(5);
								string text2 = DocUtil.GenerateUniqueLayerName(database, transaction, DocUtil.GenerateUniqueLayerName(database, transaction, baseName2));
								text2 = DocUtil.RenameCADLayer(transaction, database, current.CadLayerName, text2);
								list.Add(text2);
								AGSColor suggestedColor = null;
								MSCDataset.AddSimpleSubtype(mSCFeatureClass, suggestedSubTypeName, MSCFeatureClass.GetESRITypeCodeString(current.GeometryType), text2, current.Fields, current.TypeField, current.TypeValue, suggestedColor);
							}
						}
						mSCFeatureClass.SetQueryFromLayers(list);
						mSCFeatureClass.Write(database, transaction);
						this.StopReactors();
						this.RemoveDictionary(database, transaction);
						transaction.Commit();
						database.DisableUndoRecording(false);
						base.ParentDataset.RemoveFromFeatureServiceViewList(base.Name);
						if (base.ParentDataset.FeatureServiceViewList.Count == 0)
						{
							ArcGISRibbon.EnableFeatureServiceButtons(false);
						}
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

		public bool UpdateExtentLimit(Extent ext)
		{
			if (ext.IsValid() && ext.SpatialReference != null)
			{
				this.ExportOptions.BoundingBox = ext;
				return true;
			}
			return false;
		}

		public bool UpdateExtentFromCurrentView()
		{
			Editor arg_10_0 = base.ParentDataset.ParentDocument.Editor;
			Extent ext;
			return DocUtil.GetActiveViewportExtents(null, out ext) && this.UpdateExtentLimit(ext);
		}

		public void SetLayerLock(bool setLock, Database db, Transaction t)
		{
			DocUtil.SetLayerLock(db, t, this.LayerName, setLock);
			foreach (MSCFeatureClassSubType current in base.SubTypes)
			{
				DocUtil.SetLayerLock(db, t, current.CadLayerName, setLock);
			}
		}

		public void SetLayerLock(bool setLock)
		{
			Database database = base.ParentDataset.ParentDocument.Database;
			try
			{
				using (base.ParentDataset.ParentDocument.LockDocument((DocumentLockMode)20, null, null, false))
				{
					var transactionManager = database.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						DocUtil.SetLayerLock(database, transaction, this.LayerName, setLock);
						foreach (MSCFeatureClassSubType current in base.SubTypes)
						{
							DocUtil.SetLayerLock(database, transaction, current.CadLayerName, setLock);
						}
						transaction.Commit();
					}
				}
			}
			catch
			{
			}
		}

		public void GetEntityState(ObjectId id, out bool IsModified, out bool IsNew)
		{
			IsModified = false;
			IsNew = false;
			if (this.OriginalServiceIDs.ContainsKey(id))
			{
				if (this.ModifiedObjects.Contains(id))
				{
					IsModified = true;
					return;
				}
			}
			else
			{
				IsNew = true;
			}
		}

		private void StartReactors()
		{
			Document parentDocument = base.ParentDataset.ParentDocument;
			parentDocument.Database.BeginSave+=(new DatabaseIOEventHandler(this.Database_BeginSave));
			parentDocument.Database.SaveComplete+=(new DatabaseIOEventHandler(this.Database_SaveComplete));
			parentDocument.Database.ObjectModified+=(new ObjectEventHandler(this.Database_ObjectModified));
			parentDocument.Database.ObjectOpenedForModify+=(new ObjectEventHandler(this.Database_ObjectOpenedForModify));
		}

		private void StopReactors()
		{
			Document parentDocument = base.ParentDataset.ParentDocument;
			parentDocument.Database.BeginSave-=(new DatabaseIOEventHandler(this.Database_BeginSave));
			parentDocument.Database.SaveComplete-=(new DatabaseIOEventHandler(this.Database_SaveComplete));
			parentDocument.Database.ObjectModified-=(new ObjectEventHandler(this.Database_ObjectModified));
			parentDocument.Database.ObjectOpenedForModify-=(new ObjectEventHandler(this.Database_ObjectOpenedForModify));
		}

		private void Database_SaveComplete(object sender, DatabaseIOEventArgs e)
		{
			Database database = (Database)sender;
			string filename = database.Filename;
			string a = System.IO.Path.GetExtension(filename).ToLower();
			if (a != ".sv$")
			{
				this.IsDirtyAfterCommit = false;
			}
			this._Tracking.Clear();
		}

		private void Database_ObjectOpenedForModify(object sender, ObjectEventArgs e)
		{
			try
			{
				if (this.OriginalServiceIDs.Keys.Contains(e.DBObject.Id) && !this.ModifiedObjects.Contains(e.DBObject.Id))
				{
					if (e.DBObject is Entity)
					{
						Entity entity = (Entity)e.DBObject;
						MSCFeatureService.TrackedEntity value = new MSCFeatureService.TrackedEntity(entity);
						if (this._Tracking.ContainsKey(entity.Id))
						{
							this._Tracking.Remove(entity.Id);
						}
						this._Tracking.Add(entity.Id, value);
					}
				}
				else
				{
					ObjectIdCollection persistentReactorIds = e.DBObject.GetPersistentReactorIds();
					foreach (ObjectId objectId in persistentReactorIds)
					{
						if (this.OriginalServiceIDs.Keys.Contains(objectId) && !this.ModifiedObjects.Contains(objectId))
						{
							this.ModifiedObjects.Add(objectId);
							break;
						}
					}
				}
			}
			catch
			{
			}
		}

		private void Database_ObjectModified(object sender, ObjectEventArgs e)
		{
			try
			{
				if (this.OriginalServiceIDs.Keys.Contains(e.DBObject.Id) && !this.ModifiedObjects.Contains(e.DBObject.Id))
				{
					if (e.DBObject is Entity)
					{
						if (this._Tracking.ContainsKey(e.DBObject.Id))
						{
							MSCFeatureService.TrackedEntity a = this._Tracking[e.DBObject.Id];
							MSCFeatureService.TrackedEntity b = new MSCFeatureService.TrackedEntity((Entity)e.DBObject);
							if (!MSCFeatureService.TrackedEntity.AreEqual(a, b, base.IsSubType))
							{
								this.ModifiedObjects.Add(e.DBObject.Id);
							}
							this._Tracking.Remove(e.DBObject.Id);
						}
						else
						{
							this.ModifiedObjects.Add(e.DBObject.Id);
						}
					}
					else
					{
						this.ModifiedObjects.Add(e.DBObject.Id);
					}
				}
				else
				{
					ObjectIdCollection persistentReactorIds = e.DBObject.GetPersistentReactorIds();
					foreach (ObjectId objectId in persistentReactorIds)
					{
						if (this.OriginalServiceIDs.Keys.Contains(objectId) && !this.ModifiedObjects.Contains(objectId))
						{
							this.ModifiedObjects.Add(objectId);
							break;
						}
					}
				}
			}
			catch
			{
			}
		}

		private void Database_BeginSave(object sender, DatabaseIOEventArgs e)
		{
			Document arg_0B_0 = base.ParentDataset.ParentDocument;
			try
			{
				this.Write();
			}
			catch
			{
			}
		}
	}
}
