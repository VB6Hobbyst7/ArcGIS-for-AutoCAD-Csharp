using AFA.Resources;
using ArcGIS10Types;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using DataColumn = System.Data.DataColumn;
using DataTable = System.Data.DataTable;
using Exception = Autodesk.AutoCAD.Runtime.Exception;

namespace AFA
{
    public class MSCFeatureClass
	{
		public enum fcTypeCode
		{
			fcTypePoint = 1,
			fcTypePolyline,
			fcTypePolygon,
			fcTypeAnnotation,
			fcTypeMultiPatch
		}

		public static string s_fcQuery = "FeatureQuery";

		public static string s_fcType = "FeatureType";

		private static string[] m_typeStrings = new string[]
		{
			"",
			"Point",
			"Polyline",
			"Polygon",
			"Annotation",
			"MultiPatch"
		};

		private static string[] m_ESRItypeStrings = new string[]
		{
			"",
			"esriGeometryPoint",
			"esriGeometryPolyline",
			"esriGeometryPolygon",
			"esriGeometryAnnotation",
			"esriGeometryMultiPatch"
		};

		public static TypedValue[] tvPoint = new TypedValue[]
		{
			new TypedValue(-4, "<or"),
			new TypedValue(0, "POINT"),
			new TypedValue(0, "INSERT"),
			new TypedValue(0, "SHAPE"),
			new TypedValue(0, "HATCH"),
			new TypedValue(0, "PROXY"),
			new TypedValue(-4, "or>")
		};

		public static TypedValue[] tvPolyline = new TypedValue[]
		{
			new TypedValue(-4, "<or"),
			new TypedValue(0, "ARC"),
			new TypedValue(0, "CIRCLE"),
			new TypedValue(0, "ELLIPSE"),
			new TypedValue(0, "LINE"),
			new TypedValue(0, "MLINE"),
			new TypedValue(0, "*POLYLINE"),
			new TypedValue(0, "SPLINE"),
			new TypedValue(0, "SOLID"),
			new TypedValue(0, "3DFACE"),
			new TypedValue(-4, "or>")
		};

		public static TypedValue[] tvPolygon = new TypedValue[]
		{
			new TypedValue(-4, "<or"),
			new TypedValue(-4, "<AND"),
			new TypedValue(0, "*POLYLINE"),
			new TypedValue(-4, "&"),
			new TypedValue(70, 1),
			new TypedValue(-4, "AND>"),
			new TypedValue(-4, "<AND"),
			new TypedValue(0, "MLINE"),
			new TypedValue(-4, "&"),
			new TypedValue(71, 2),
			new TypedValue(-4, "AND>"),
			new TypedValue(-4, "<AND"),
			new TypedValue(0, "SPLINE"),
			new TypedValue(-4, "&"),
			new TypedValue(70, 1),
			new TypedValue(-4, "AND>"),
			new TypedValue(0, "CIRCLE"),
			new TypedValue(0, "SOLID"),
			new TypedValue(0, "ELLIPSE"),
			new TypedValue(0, "3DFACE"),
			new TypedValue(-4, "or>")
		};

		public static TypedValue[] tvAnnotation = new TypedValue[]
		{
			new TypedValue(-4, "<or"),
			new TypedValue(0, "TEXT"),
			new TypedValue(0, "MTEXT"),
			new TypedValue(0, "ATTRIBUTE"),
			new TypedValue(0, "ATTDEF"),
			new TypedValue(-4, "or>")
		};

		public static TypedValue[] tvMultiPatch = MSCFeatureClass.tvPolygon;

		private static DataTable _dataTable = null;

		public static string DictionaryName = "ESRI_FEATURES";

		public event PropertyChangedEventHandler PropertyChanged;

		public ObjectId Id
		{
			get;
			set;
		}

		public MSCDataset ParentDataset
		{
			get;
			set;
		}

		public MSCFeatureClass ParentFC
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public MSCFeatureClass.fcTypeCode GeometryType
		{
			get;
			set;
		}

		public ResultBuffer Query
		{
			get;
			set;
		}

		public List<CadField> Fields
		{
			get;
			set;
		}

		public ObservableCollection<MSCFeatureClassSubType> SubTypes
		{
			get;
			set;
		}

		public bool IsSubType
		{
			get
			{
				return this.ParentFC != null;
			}
		}

		public bool ReadOnly
		{
			get;
			set;
		}

		public string TypeField
		{
			get;
			set;
		}

		public ResultBuffer FCQuery
		{
			get
			{
				return MSCFeatureClass.BuildTypeQuery(this.GeometryType, this.Query.AsArray());
			}
		}

		protected TypedValue[] TypeQuery
		{
			get
			{
				return MSCFeatureClass.GetTypeQuery(this.GeometryType);
			}
		}

		public static string FixType(string srcString)
		{
			if (srcString.ToLower().Contains("polygon"))
			{
				return MSCFeatureClass.m_typeStrings[3];
			}
			if (srcString.ToLower().Contains("polyline"))
			{
				return MSCFeatureClass.m_typeStrings[2];
			}
			if (srcString.ToLower().Contains("point"))
			{
				return MSCFeatureClass.m_typeStrings[1];
			}
			if (srcString.ToLower().Contains("multipatch"))
			{
				return MSCFeatureClass.m_typeStrings[5];
			}
			if (srcString.ToLower().Contains("annotation"))
			{
				return MSCFeatureClass.m_typeStrings[4];
			}
			if (srcString.ToLower().Contains("text"))
			{
				return MSCFeatureClass.m_typeStrings[4];
			}
			return MSCFeatureClass.m_typeStrings[1];
		}

		public override string ToString()
		{
			return this.Name;
		}

		public static ResultBuffer BuildTypeQuery(MSCFeatureClass.fcTypeCode typeCode, TypedValue[] tv)
		{
			ResultBuffer resultBuffer = new ResultBuffer(new TypedValue[]
			{
				new TypedValue(-4, "<and")
			});
			TypedValue[] typeQuery = MSCFeatureClass.GetTypeQuery(typeCode);
			for (int i = 0; i < typeQuery.Length; i++)
			{
				resultBuffer.Add(typeQuery[i]);
			}
			for (int j = 0; j < tv.Length; j++)
			{
				resultBuffer.Add(tv[j]);
			}
			resultBuffer.Add(new TypedValue(-4, "and>"));
			return resultBuffer;
		}

		public void SetGeometryType(string s)
		{
			this.GeometryType = MSCFeatureClass.GetGeomType(s);
		}

		public SelectionFilter GetSelectionFilter()
		{
			SelectionFilter result;
			try
			{
				result = new SelectionFilter(this.FCQuery.AsArray());
			}
			catch
			{
				ResultBuffer resultBuffer = new ResultBuffer();
				SelectionFilter selectionFilter = new SelectionFilter(resultBuffer.AsArray());
				result = selectionFilter;
			}
			return result;
		}

		public void SetQueryFromLayers(List<string> layerNames)
		{
			if (layerNames.Count > 0)
			{
				string text = DocUtil.FixLayerName(layerNames[0]);
				for (int i = 1; i < layerNames.Count; i++)
				{
					string str = DocUtil.FixLayerName(layerNames[i]);
					text = text + "," + str;
				}
				this.Query = new ResultBuffer(new TypedValue[]
				{
					new TypedValue(8, text)
				});
			}
		}

		public void SetDefaultQuery()
		{
			this.Query = new ResultBuffer(new TypedValue[]
			{
				new TypedValue(8, "*")
			});
		}

		public MSCFeatureClass(MSCDataset parent)
		{
			this.Id = ObjectId.Null;
			this.Name = "";
			this.GeometryType = MSCFeatureClass.fcTypeCode.fcTypePoint;
			this.ParentDataset = parent;
			this.Query = new ResultBuffer(new TypedValue[]
			{
				new TypedValue(8, "*")
			});
			this.Fields = new List<CadField>();
			this.SubTypes = new ObservableCollection<MSCFeatureClassSubType>();
			this.ParentFC = null;
		}

		public MSCFeatureClass(string name, MSCDataset parent, ObjectId id, Transaction t)
		{
			this.Name = name;
			this.Id = id;
			this.GeometryType = MSCFeatureClass.fcTypeCode.fcTypePoint;
			this.Query = new ResultBuffer(new TypedValue[]
			{
				new TypedValue(8, "*")
			});
			this.ParentDataset = parent;
			this.ParentFC = null;
			this.SubTypes = new ObservableCollection<MSCFeatureClassSubType>();
			this.Read(id, t);
		}

		~MSCFeatureClass()
		{
			try
			{
				this.Query.Dispose();
				if (this.SubTypes != null)
				{
					this.SubTypes.Clear();
				}
			}
			catch
			{
			}
		}

		protected virtual void Read(ObjectId id, Transaction t)
		{
			try
			{
				DBDictionary dBDictionary = (DBDictionary)t.GetObject(this.Id, 0);
				if (dBDictionary.Contains("FeatureType"))
				{
					Xrecord xrecord = (Xrecord)t.GetObject(dBDictionary.GetAt("FeatureType"), 0);
					TypedValue[] array = xrecord.Data.AsArray();
					for (int i = 0; i < array.Length; i++)
					{
						TypedValue typedValue = array[i];
						string type = typedValue.Value.ToString();
						this.GeometryType = MSCFeatureClass.GetGeomType(type);
					}
				}
				if (dBDictionary.Contains("FeatureQuery"))
				{
					Xrecord xrecord2 = (Xrecord)t.GetObject(dBDictionary.GetAt("FeatureQuery"), 0);
					this.Query = xrecord2.Data;
				}
				if (dBDictionary.Contains("ESRI_ATTRIBUTES"))
				{
					DBDictionary attrDict = (DBDictionary)t.GetObject(dBDictionary.GetAt("ESRI_ATTRIBUTES"), 0);
					this.Fields = CadField.ToCadFields(attrDict, t);
				}
				if (dBDictionary.Contains("ESRI_Subtypes"))
				{
					DBDictionary dBDictionary2 = (DBDictionary)t.GetObject(dBDictionary.GetAt("ESRI_Subtypes"), 0);
					using (DbDictionaryEnumerator enumerator = dBDictionary2.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							DBDictionaryEntry current = enumerator.Current;
							MSCFeatureClassSubType mSCFeatureClassSubType = new MSCFeatureClassSubType(current.Key, this.ParentDataset, current.Value, t);
							mSCFeatureClassSubType.ParentFC = this;
							this.SubTypes.Add(mSCFeatureClassSubType);
						}
					}
				}
				if (dBDictionary.Contains("ESRI_ReadOnly"))
				{
					Xrecord xrecord3 = (Xrecord)t.GetObject(dBDictionary.GetAt("ESRI_ReadOnly"), 0);
					TypedValue[] array2 = xrecord3.Data.AsArray();
					for (int j = 0; j < array2.Length; j++)
					{
						TypedValue typedValue2 = array2[j];
						if (typedValue2.TypeCode == 290)
						{
							this.ReadOnly = (0 != Convert.ToInt16(typedValue2.Value));
						}
					}
				}
				if (this is MSCFeatureClassSubType)
				{
					MSCFeatureClassSubType mSCFeatureClassSubType2 = (MSCFeatureClassSubType)this;
					if (dBDictionary.Contains("SubtypeLayer"))
					{
						Xrecord xrecord4 = (Xrecord)t.GetObject(dBDictionary.GetAt("SubtypeLayer"), 0);
						TypedValue[] array3 = xrecord4.Data.AsArray();
						for (int k = 0; k < array3.Length; k++)
						{
							TypedValue typedValue3 = array3[k];
							if (typedValue3.TypeCode == 1)
							{
								mSCFeatureClassSubType2.CadLayerName = (string)typedValue3.Value;
							}
						}
					}
					if (dBDictionary.Contains("SubtypeField"))
					{
						Xrecord xrecord5 = (Xrecord)t.GetObject(dBDictionary.GetAt("SubtypeField"), 0);
						TypedValue[] array4 = xrecord5.Data.AsArray();
						for (int l = 0; l < array4.Length; l++)
						{
							TypedValue typedValue4 = array4[l];
							if (typedValue4.TypeCode == 1)
							{
								mSCFeatureClassSubType2.TypeField = (string)typedValue4.Value;
							}
						}
					}
					if (dBDictionary.Contains("SubtypeValue"))
					{
						Xrecord xrecord6 = (Xrecord)t.GetObject(dBDictionary.GetAt("SubtypeValue"), 0);
						TypedValue typedValue5 = xrecord6.Data.AsArray()[0];
						mSCFeatureClassSubType2.TypeValue = typedValue5.Value;
					}
				}
				else if (dBDictionary.Contains("SubtypeField"))
				{
					Xrecord xrecord7 = (Xrecord)t.GetObject(dBDictionary.GetAt("SubtypeField"), 0);
					TypedValue[] array5 = xrecord7.Data.AsArray();
					for (int m = 0; m < array5.Length; m++)
					{
						TypedValue typedValue6 = array5[m];
						if (typedValue6.TypeCode == 1)
						{
							this.TypeField = (string)typedValue6.Value;
						}
					}
				}
			}
			catch
			{
			}
		}

		public virtual void Write(Document doc)
		{
			Database database = doc.Database;
			try
			{
				using (doc.LockDocument((DocumentLockMode)20, null, null, false))
				{
					var  transactionManager = database.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						this.Write(database, transaction);
						transaction.Commit();
					}
				}
			}
			catch
			{
			}
		}

		public static void UpdateDefaultsFromSubtype(MSCFeatureClass fc)
		{
			try
			{
				if (!string.IsNullOrEmpty(fc.TypeField))
				{
					CadField cadField = CadField.FindField(fc.Fields, fc.TypeField);
					if (cadField != null)
					{
						object value = cadField.Value.Value;
						MSCFeatureClass subType = fc.GetSubType(value);
						if (subType != null)
						{
							fc.Fields = new List<CadField>(subType.Fields);
						}
					}
				}
			}
			catch
			{
			}
		}

		public virtual void Write(Database db, Transaction t)
		{
			try
			{
				DBDictionary dBDictionary = this.ParentDataset.Open(db, t, MSCFeatureClass.DictionaryName, (OpenMode)1);
				if (this.ParentFC != null)
				{
					dBDictionary = this.ParentFC.Open(t, (OpenMode)1);
				}
				if (MSCFeatureClass.DictionaryName == "ESRI_FEATURES" && this.ParentDataset.FeatureClassAcadID == ObjectId.Null)
				{
					this.ParentDataset.FeatureClassAcadID = dBDictionary.ObjectId;
				}
				if (MSCFeatureClass.DictionaryName == "ESRI_FEATURESERVICES" && this.ParentDataset.FeatureServiceAcadID == ObjectId.Null)
				{
					this.ParentDataset.FeatureServiceAcadID = dBDictionary.ObjectId;
				}
				MSCFeatureClass.UpdateDefaultsFromSubtype(this);
				DBDictionary dBDictionary2 = this.WriteFCDictionary(dBDictionary, t);
				if (dBDictionary2 != null && this.SubTypes.Count > 0)
				{
					DBDictionary dBDictionary3 = new DBDictionary();
					dBDictionary2.SetAt("ESRI_Subtypes", dBDictionary3);
					t.AddNewlyCreatedDBObject(dBDictionary3, true);
					foreach (MSCFeatureClass current in this.SubTypes)
					{
						current.WriteFCDictionary(dBDictionary3, t);
					}
				}
			}
			catch
			{
			}
		}

		public virtual DBDictionary Open(Transaction t, OpenMode mode)
		{
			return (DBDictionary)t.GetObject(this.Id, mode);
		}

		public virtual DBDictionary WriteFCDictionary(DBDictionary parentDict, Transaction t)
		{
			DBDictionary result;
			try
			{
				parentDict.UpgradeOpen();
				DBDictionary dBDictionary = new DBDictionary();
				this.Id = parentDict.SetAt(this.Name, dBDictionary);
				dBDictionary.DisableUndoRecording(true);
				t.AddNewlyCreatedDBObject(dBDictionary, true);
				Xrecord xrecord = new Xrecord();
				xrecord.Data=(new ResultBuffer(new TypedValue[]
				{
					new TypedValue(1, MSCFeatureClass.GetGeomString(this.GeometryType))
				}));
				dBDictionary.SetAt("FeatureType", xrecord);
				xrecord.DisableUndoRecording(true);
				t.AddNewlyCreatedDBObject(xrecord, true);
				Xrecord xrecord2 = new Xrecord();
				xrecord2.Data=(this.Query);
				dBDictionary.SetAt("FeatureQuery", xrecord2);
				xrecord2.DisableUndoRecording(true);
				t.AddNewlyCreatedDBObject(xrecord2, true);
				CadField.WriteNewAttributeDictionary(this.Fields, dBDictionary, t);
				if (this.ReadOnly)
				{
					Xrecord xrecord3 = new Xrecord();
					xrecord3.Data=(new ResultBuffer(new TypedValue[]
					{
						new TypedValue(290, true)
					}));
					dBDictionary.SetAt("ESRI_ReadOnly", xrecord3);
					xrecord3.DisableUndoRecording(true);
					t.AddNewlyCreatedDBObject(xrecord3, true);
				}
				if (!string.IsNullOrEmpty(this.TypeField))
				{
					Xrecord xrecord4 = new Xrecord();
					xrecord4.Data=(new ResultBuffer(new TypedValue[]
					{
						new TypedValue(1, this.TypeField)
					}));
					dBDictionary.SetAt("SubtypeField", xrecord4);
					xrecord4.DisableUndoRecording(true);
					t.AddNewlyCreatedDBObject(xrecord4, true);
				}
				if (this is MSCFeatureClassSubType)
				{
					MSCFeatureClassSubType mSCFeatureClassSubType = this as MSCFeatureClassSubType;
					Xrecord xrecord5 = new Xrecord();
					xrecord5.Data=(new ResultBuffer(new TypedValue[]
					{
						new TypedValue(1, mSCFeatureClassSubType.CadLayerName)
					}));
					dBDictionary.SetAt("SubtypeLayer", xrecord5);
					xrecord5.DisableUndoRecording(true);
					t.AddNewlyCreatedDBObject(xrecord5, true);
					Xrecord xrecord6 = new Xrecord();
					xrecord6.Data=(new ResultBuffer(new TypedValue[]
					{
						CadField.CreateTypedValue(mSCFeatureClassSubType.TypeValue)
					}));
					dBDictionary.SetAt("SubtypeValue", xrecord6);
					xrecord6.DisableUndoRecording(true);
					t.AddNewlyCreatedDBObject(xrecord6, true);
				}
				result = dBDictionary;
			}
			catch (System.Exception ex)
			{
				string arg_248_0 = ex.Message;
				result = null;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public DataTable GetEmptyDataTable()
		{
			DataTable result;
			try
			{
				MSCFeatureClass._dataTable = new DataTable();
				MSCFeatureClass._dataTable.Rows.Clear();
				MSCFeatureClass._dataTable.Columns.Clear();
				MSCFeatureClass._dataTable.Columns.Add(AfaStrings.EntityID, typeof(IntPtr));
				MSCFeatureClass._dataTable.Columns[AfaStrings.EntityID].ReadOnly = true;
				MSCFeatureClass._dataTable.Columns.Add(AfaStrings.EntityType, typeof(string));
				MSCFeatureClass._dataTable.Columns[AfaStrings.EntityType].ReadOnly = true;
				if (this is MSCFeatureService)
				{
					MSCFeatureClass._dataTable.Columns.Add(AfaStrings.Status, typeof(string));
					MSCFeatureClass._dataTable.Columns[AfaStrings.Status].ReadOnly = true;
				}
				if (this is MSCFeatureClassSubType && this.ParentFC is MSCFeatureService)
				{
					MSCFeatureClass._dataTable.Columns.Add(AfaStrings.Status, typeof(string));
					MSCFeatureClass._dataTable.Columns[AfaStrings.Status].ReadOnly = true;
				}
				foreach (CadField current in this.Fields)
				{
					TypedValue arg_149_0 = current.Value;
					CadField.CadFieldType typeCode = (CadField.CadFieldType)current.Value.TypeCode;
					CadField.CadFieldType cadFieldType = typeCode;
					DataColumn dataColumn;
					if (cadFieldType <= CadField.CadFieldType.Double)
					{
						if (cadFieldType == CadField.CadFieldType.String)
						{
							goto IL_1E6;
						}
						if (cadFieldType != CadField.CadFieldType.Double)
						{
							goto IL_1E6;
						}
						dataColumn = MSCFeatureClass._dataTable.Columns.Add(current.Name, typeof(double));
					}
					else if (cadFieldType != CadField.CadFieldType.Short)
					{
						if (cadFieldType != CadField.CadFieldType.Integer)
						{
							goto IL_1E6;
						}
						dataColumn = MSCFeatureClass._dataTable.Columns.Add(current.Name, typeof(int));
					}
					else
					{
						dataColumn = MSCFeatureClass._dataTable.Columns.Add(current.Name, typeof(short));
					}
					IL_206:
					if (dataColumn == null)
					{
						continue;
					}
					dataColumn.ReadOnly = current.ReadOnly;
					if (current.Domain != null)
					{
						dataColumn.ExtendedProperties.Add("Domain", current.Domain);
					}
					if (current.ExtendedType == 5)
					{
						dataColumn.ExtendedProperties.Add("DateField", esriFieldType.esriFieldTypeDate);
						continue;
					}
					continue;
					IL_1E6:
					dataColumn = MSCFeatureClass._dataTable.Columns.Add(current.Name, typeof(string));
					goto IL_206;
				}
				result = MSCFeatureClass._dataTable;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public DataTable GetDataTable(ObjectIdCollection selectedIDS)
		{
			DataTable result;
			try
			{
				MSCFeatureClass._dataTable = this.GetEmptyDataTable();
				if (MSCFeatureClass._dataTable == null)
				{
					result = null;
				}
				else
				{
					Document parentDocument = this.ParentDataset.ParentDocument;
					if (selectedIDS != null)
					{
						ObjectId[] featureIds = this.GetFeatureIds(selectedIDS);
						if (featureIds.Length != 0)
						{
							Database database = parentDocument.Database;
							List<ObjectId> list = new List<ObjectId>();
							using (parentDocument.LockDocument((DocumentLockMode)32, null, null, false))
							{
								using (Transaction transaction = database.TransactionManager.StartTransaction())
								{
									ObjectId[] array = featureIds;
									for (int i = 0; i < array.Length; i++)
									{
										ObjectId objectId = array[i];
										try
										{
											if (!list.Contains(objectId))
											{
												DataRow dataRow = MSCFeatureClass._dataTable.NewRow();
												dataRow[AfaStrings.EntityID] = objectId.OldIdPtr;
												dataRow[AfaStrings.EntityType] = objectId.ObjectClass.DxfName;
												DBObject groupEntity = this.GetGroupEntity(objectId, transaction);
												List<CadField> entityFields;
												if (groupEntity == null)
												{
													entityFields = this.GetEntityFields(objectId, transaction);
													if (this is MSCFeatureService)
													{
														this.AppendFeatureServiceStateField(entityFields, objectId);
													}
													if (this is MSCFeatureClassSubType && this.ParentFC is MSCFeatureService)
													{
														this.AppendFeatureServiceStateField(entityFields, objectId);
													}
												}
												else
												{
													Group group = groupEntity as Group;
													dataRow[AfaStrings.EntityType] = group.Id.ObjectClass.DxfName;
													dataRow[AfaStrings.EntityID] = groupEntity.Id.OldIdPtr;
													ObjectId[] allEntityIds = group.GetAllEntityIds();
													ObjectId[] array2 = allEntityIds;
													for (int j = 0; j < array2.Length; j++)
													{
														ObjectId item = array2[j];
														list.Add(item);
													}
													entityFields = this.GetEntityFields(group.Id, transaction);
													if (this is MSCFeatureService)
													{
														this.AppendFeatureServiceStateField(entityFields, group.Id);
													}
													if (this is MSCFeatureClassSubType && this.ParentFC is MSCFeatureService)
													{
														this.AppendFeatureServiceStateField(entityFields, objectId);
													}
												}
												foreach (CadField current in entityFields)
												{
													try
													{
														dataRow[current.Name] = current.Value.Value;
													}
													catch (SystemException ex)
													{
														string arg_237_0 = ex.Message;
													}
												}
												MSCFeatureClass._dataTable.Rows.Add(dataRow);
											}
										}
										catch (SystemException ex2)
										{
											string arg_26F_0 = ex2.Message;
										}
									}
									transaction.Commit();
								}
							}
						}
					}
					result = MSCFeatureClass._dataTable;
				}
			}
			catch (Exception ex3)
			{
				string arg_2B8_0 = ex3.Message;
				result = null;
			}
			catch (SystemException ex4)
			{
				string arg_2C7_0 = ex4.Message;
				result = null;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public DataTable GetDataTable()
		{
			DataTable result;
			try
			{
				ObjectId[] featureIds = this.GetFeatureIds();
				if (featureIds.Length > 0)
				{
					result = this.GetDataTable(new ObjectIdCollection(featureIds));
				}
				else
				{
					result = this.GetEmptyDataTable();
				}
			}
			catch
			{
				result = this.GetEmptyDataTable();
			}
			return result;
		}

		public DataTable GetDataTableFromSelection()
		{
			ObjectIdCollection objectIdCollection = new ObjectIdCollection(this.GetFeatureIdsFromSelection());
			if (objectIdCollection != null)
			{
				return this.GetDataTable(objectIdCollection);
			}
			return this.GetEmptyDataTable();
		}

		public DataTable Identify(ObjectIdCollection initialIds)
		{
			string columnName = "Key";
			string columnName2 = "Value";
			string columnName3 = "FieldType";
			string columnName4 = "ReadOnly";
			string columnName5 = "FieldLength";
			string entityID = AfaStrings.EntityID;
			string value = "Type";
			string text = "BaseField";
			string columnName6 = "CodedValues";
			string columnName7 = "CodedValue";
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add(columnName);
			dataTable.Columns.Add(columnName2, typeof(object));
			dataTable.Columns.Add(columnName3, typeof(CadField.CadFieldType));
			dataTable.Columns.Add(columnName4, typeof(bool));
			dataTable.Columns.Add(columnName5, typeof(int));
			dataTable.Columns.Add(text, typeof(CadField));
			dataTable.Columns[text].ReadOnly = true;
			dataTable.Columns.Add(columnName6, typeof(ObservableCollection<MSCCodedValue>));
			dataTable.Columns.Add(columnName7, typeof(MSCCodedValue));
			Document parentDocument = this.ParentDataset.ParentDocument;
			Database database = parentDocument.Database;
			using (parentDocument.LockDocument((DocumentLockMode)32, null, null, false))
			{
				using (Transaction transaction = database.TransactionManager.StartTransaction())
				{
					ObjectIdCollection objectIdCollection = null;
					try
					{
						objectIdCollection = new ObjectIdCollection(this.GetFeatureIds(initialIds));
						if (objectIdCollection.Count == 0)
						{
							DataTable result = dataTable;
							return result;
						}
					}
					catch
					{
						DataTable result = dataTable;
						return result;
					}
					ObjectId objectId = objectIdCollection[0];
					List<CadField> entityFields = this.GetEntityFields(objectIdCollection[0], transaction);
					DataRow dataRow = dataTable.NewRow();
					dataRow[columnName] = entityID;
					dataRow[columnName2] = objectIdCollection[0];
					dataRow[columnName4] = true;
					dataRow[columnName5] = 0;
					dataTable.Rows.Add(dataRow);
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2[columnName] = value;
					dataRow2[columnName2] = objectId.ObjectClass.DxfName;
					dataRow2[columnName4] = true;
					dataRow2[columnName5] = 0;
					dataTable.Rows.Add(dataRow2);
					foreach (CadField current in entityFields)
					{
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3[columnName] = current.Name;
						object extendedValue = current.GetExtendedValue();
						dataRow3[columnName2] = extendedValue;
						dataRow3[columnName3] = current.FieldType;
						dataRow3[columnName4] = current.ReadOnly;
						dataRow3[columnName5] = (int)current.Length;
						dataRow3[text] = current;
						if (current.Domain != null)
						{
							current.Value = current.Domain.CheckValue(extendedValue);
							extendedValue = current.GetExtendedValue();
							if (current.Domain.CodedValues != null)
							{
								dataRow3[columnName6] = current.Domain.CodedValuesDisplayCollection;
								dataRow3[columnName7] = current.Domain.GetCodedValue(extendedValue);
							}
						}
						dataTable.Rows.Add(dataRow3);
					}
					MSCCodedValue mSCCodedValue = new MSCCodedValue(AFA.UI.Identify.VariesValue, null);
					for (int i = 1; i < objectIdCollection.Count; i++)
					{
						ObjectId id = objectIdCollection[(i)];
						DataRow dataRow4 = dataTable.Rows[0];
						DataRow arg_38A_0 = dataTable.Rows[1];
						if (dataRow4[columnName2].ToString() != id.ToString())
						{
							dataTable.Rows[0][columnName2] = string.Format("{0} - ({1} {2})", AFA.UI.Identify.VariesValue, objectIdCollection.Count, AfaStrings.FeaturesSelected);
						}
						if (dataTable.Rows[1][columnName2].ToString() != objectId.ObjectClass.DxfName)
						{
							dataTable.Rows[1][columnName2] = AFA.UI.Identify.VariesValue;
						}
						List<CadField> entityFields2 = this.GetEntityFields(id, transaction);
						foreach (CadField current2 in entityFields2)
						{
							int num = -1;
							for (int j = 0; j < dataTable.Rows.Count; j++)
							{
								if (current2.Name == dataTable.Rows[j][columnName].ToString())
								{
									num = j;
									break;
								}
							}
							if (num != -1 && dataTable.Rows[num][columnName2].ToString() != current2.Value.Value.ToString())
							{
								dataTable.Rows[num][columnName2] = AFA.UI.Identify.VariesValue;
								try
								{
									if (current2.Domain.CodedValues != null)
									{
										ObservableCollection<MSCCodedValue> observableCollection = (ObservableCollection<MSCCodedValue>)dataTable.Rows[num]["CodedValues"];
										if (!observableCollection.Contains(mSCCodedValue))
										{
											observableCollection.Add(mSCCodedValue);
										}
										dataTable.Rows[num]["CodedValue"] = mSCCodedValue;
									}
								}
								catch
								{
								}
							}
						}
					}
					transaction.Commit();
				}
			}
			return dataTable;
		}

		public List<CadField> GetEntityFields(ObjectId id)
		{
			List<CadField> list = null;
			List<CadField> result;
			try
			{
				Document document = AfaDocData.ActiveDocData.Document;
				Database database = document.Database;
				using (document.LockDocument((DocumentLockMode)32, null, null, false))
				{
					using (Transaction transaction = database.TransactionManager.StartTransaction())
					{
						list = this.GetEntityFields(id, transaction);
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

		private void ReplaceField(List<CadField> fieldList, string fieldName, object newValue)
		{
			try
			{
				CadField cadField = CadField.FindField(fieldList, fieldName);
				if (cadField != null)
				{
					CadField item = CadField.ToCadField(cadField, newValue);
					fieldList.Remove(cadField);
					fieldList.Add(item);
				}
			}
			catch
			{
			}
		}

		private MSCFeatureClassSubType GetEntitySubytpe(ObjectId id, Transaction t)
		{
			if (this.SubTypes.Count == 0)
			{
				return null;
			}
			Entity entity = (Entity)t.GetObject(id, 0);
			string layer = entity.Layer;
			foreach (MSCFeatureClassSubType current in this.SubTypes)
			{
				if (object.Equals(current.CadLayerName, layer))
				{
					return current;
				}
			}
			return null;
		}

		private void UpdateSubtypeField(List<CadField> fieldList, ObjectId id, Transaction t)
		{
			try
			{
				if (this.SubTypes.Count != 0)
				{
					MSCFeatureClassSubType entitySubytpe = this.GetEntitySubytpe(id, t);
					if (entitySubytpe != null)
					{
						this.ReplaceField(fieldList, entitySubytpe.TypeField, entitySubytpe.TypeValue);
					}
				}
			}
			catch
			{
			}
		}

		public List<CadField> GetEntityFields(ObjectId id, Transaction t)
		{
			List<CadField> list = new List<CadField>();
			List<CadField> result;
			try
			{
				List<CadField> list2 = CadField.EntityCadFields(id, t);
				List<CadField> fields = this.Fields;
				MSCFeatureClassSubType entitySubytpe = this.GetEntitySubytpe(id, t);
				if (entitySubytpe != null)
				{
					fields = entitySubytpe.Fields;
				}
				foreach (CadField current in fields)
				{
					CadField cadField = null;
					foreach (CadField current2 in list2)
					{
						if (current2.Name == current.Name && current2.Value.TypeCode == current.Value.TypeCode)
						{
							cadField = new CadField(current);
							cadField.Value = current2.Value;
							if (current2.Domain != null)
							{
								cadField.Domain = current2.Domain;
								break;
							}
							break;
						}
					}
					if (cadField != null)
					{
						if (entitySubytpe == null)
						{
							list.Add(cadField);
						}
						else if (cadField.Name == entitySubytpe.TypeField)
						{
							list.Add(current);
						}
						else
						{
							list.Add(cadField);
						}
					}
					else
					{
						list.Add(current);
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

		public void SetEntityFields(ObjectIdCollection ids, DataTable table, Transaction t)
		{
			List<CadField> fields = CadField.ToCadFields(table, this);
			foreach (ObjectId id in ids)
			{
				try
				{
					CadField.AddCadAttributesToEntity(this.ParentDataset.ParentDocument.Database, t, id, fields);
				}
				catch
				{
					DocUtil.ShowDebugMessage(AfaStrings.ErrorInAttachingFields);
				}
			}
		}

		public void SetEntityFields(ObjectIdCollection ids, DataRow row, Transaction t)
		{
			List<CadField> list = CadField.ToCadFields(row, this);
			if (list.Count == 0)
			{
				return;
			}
			foreach (ObjectId id in ids)
			{
				try
				{
					CadField.AddCadAttributeToEntity(this.ParentDataset.ParentDocument.Database, t, id, list[0]);
				}
				catch
				{
					DocUtil.ShowDebugMessage(AfaStrings.ErrorInAttachingFields);
				}
			}
		}

		private void AppendFeatureServiceStateField(List<CadField> fields, ObjectId id)
		{
			MSCFeatureService mSCFeatureService = null;
			if (this is MSCFeatureService)
			{
				mSCFeatureService = (MSCFeatureService)this;
			}
			else if (this is MSCFeatureClassSubType)
			{
				mSCFeatureService = (MSCFeatureService)this.ParentFC;
			}
			if (mSCFeatureService == null)
			{
				return;
			}
			bool flag;
			bool flag2;
			mSCFeatureService.GetEntityState(id, out flag, out flag2);
			CadField cadField = new CadField();
			cadField.Name = AfaStrings.Status;
			cadField.Length = 6;
			if (flag)
			{
				cadField.Value = new TypedValue(1, "Mod");
			}
			else if (flag2)
			{
				cadField.Value = new TypedValue(1, "New");
			}
			else
			{
				cadField.Value = new TypedValue(1, " ");
			}
			cadField.ReadOnly = true;
			fields.Add(cadField);
		}

		private DBObject GetGroupEntity(ObjectId id, Transaction t)
		{
			DBObject @object = t.GetObject(id, 0);
			ObjectIdCollection persistentReactorIds = @object.GetPersistentReactorIds();
			foreach (ObjectId objectId in persistentReactorIds)
			{
				if (objectId.ObjectClass.DxfName == "GROUP")
				{
					DBObject object2 = objectId.GetObject(0);
					if (object2.GetType() == typeof(Group))
					{
						return object2 as Group;
					}
				}
			}
			return null;
		}

		private CadField GetTypeIDFieldValue(ObjectId id, Transaction t, CadField fcField)
		{
			CadField result;
			try
			{
				DBObject @object = t.GetObject(id, 0);
				Entity entity = null;
				if (@object is Entity)
				{
					entity = (Entity)@object;
				}
				else if (@object is Group)
				{
					Group group = (Group)@object;
					ObjectId[] allEntityIds = group.GetAllEntityIds();
					if (allEntityIds.Count<ObjectId>() == 0)
					{
						result = fcField;
						return result;
					}
					entity = (Entity)t.GetObject(allEntityIds[0], 0);
				}
				foreach (MSCFeatureClassSubType current in this.SubTypes)
				{
					if (entity.Layer == current.CadLayerName)
					{
						foreach (CadField current2 in current.Fields)
						{
							if (current2.Name == fcField.Name)
							{
								result = current2;
								return result;
							}
						}
					}
				}
				result = fcField;
			}
			catch
			{
				result = fcField;
			}
			return result;
		}

		public MSCFeatureClassSubType FindSubtypeName(string stName)
		{
			foreach (MSCFeatureClassSubType current in this.SubTypes)
			{
				if (string.Equals(current.Name, stName, StringComparison.CurrentCultureIgnoreCase))
				{
					return current;
				}
			}
			return null;
		}

		public MSCFeatureClassSubType GetSubType(object typeValue)
		{
			foreach (MSCFeatureClassSubType current in this.SubTypes)
			{
				if (current.TypeValue.ToString() == typeValue.ToString())
				{
					return current;
				}
			}
			return null;
		}

		public MSCFeatureClassSubType GetSubTypeFromLayer(string LayerName)
		{
			foreach (MSCFeatureClassSubType current in this.SubTypes)
			{
				if (current.CadLayerName == LayerName)
				{
					return current;
				}
			}
			return null;
		}

		public void UpdateSubtypeLayers(ObjectIdCollection ids, object newValue)
		{
			Document document = AfaDocData.ActiveDocData.Document;
			Database database = document.Database;
			try
			{
				using (document.LockDocument())
				{
					var  transactionManager = document.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						this.UpdateSubtypeLayers(database, transaction, ids, newValue);
						transactionManager.QueueForGraphicsFlush();
						transactionManager.FlushGraphics();
						document.Editor.UpdateScreen();
						transaction.Commit();
					}
				}
			}
			catch
			{
				ErrorReport.ShowErrorMessage("Error updating Subtype Layers");
			}
		}

		public void UpdateSubtypeLayers(Database db, Transaction t, ObjectIdCollection ids, object newValue)
		{
			try
			{
				foreach (ObjectId id in ids)
				{
					this.UpdateSubtypeLayer(db, t, id, newValue);
				}
			}
			catch
			{
				ErrorReport.ShowErrorMessage("Error updating Subtype Layers");
			}
		}

		public void UpdateSubtypeLayer(ObjectId id, object newValue)
		{
			Document document = AfaDocData.ActiveDocData.Document;
			Database database = document.Database;
			try
			{
				using (document.LockDocument())
				{
					var  transactionManager = document.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						this.UpdateSubtypeLayer(database, transaction, id, newValue);
						transaction.Commit();
					}
				}
			}
			catch (SystemException)
			{
				ErrorReport.ShowErrorMessage("DEBUG:  Error attaching CAD Attribute");
			}
		}

		public void UpdateSubtypeLayer(Database db, Transaction t, ObjectId id, object newValue)
		{
			Document document = AfaDocData.ActiveDocData.Document;
			MSCFeatureClass mSCFeatureClass = this;
			if (this.ParentFC != null)
			{
				mSCFeatureClass = this.ParentFC;
			}
			string layerName;
			foreach (MSCFeatureClassSubType current in mSCFeatureClass.SubTypes)
			{
				if (current.TypeValue.ToString().Equals(newValue.ToString()))
				{
					try
					{
						layerName = current.CadLayerName;
						ObjectId existingLayer = DocUtil.GetExistingLayer(db, t, layerName);
						this.SetEntityLayer(id, existingLayer, document, t);
						return;
					}
					catch (Exception ex)
					{
						throw ex;
					}
					catch (SystemException ex2)
					{
						string arg_7A_0 = ex2.Message;
						throw ex2;
					}
				}
			}
			layerName = this.GetFirstLayerFromQuery();
			ObjectId existingLayer2 = DocUtil.GetExistingLayer(db, t, layerName);
			this.SetEntityLayer(id, existingLayer2, document, t);
		}

		protected void SetEntityLayer(ObjectId id, ObjectId layerId, Document doc, Transaction t)
		{
			if (id.ObjectClass.Name == "AcDbGroup")
			{
				Group group = (Group)t.TransactionManager.GetObject(id, (OpenMode)1);
				if (group != null)
				{
					ObjectId[] allEntityIds = group.GetAllEntityIds();
					ObjectId[] array = allEntityIds;
					for (int i = 0; i < array.Length; i++)
					{
						ObjectId id2 = array[i];
						this.SetEntityLayer(id2, layerId, doc, t);
					}
					return;
				}
			}
			else
			{
				try
				{
					Entity entity = (Entity)t.TransactionManager.GetObject(id, (OpenMode)1);
					entity.SetLayerId(layerId, true);
					doc.TransactionManager.QueueForGraphicsFlush();
					doc.TransactionManager.FlushGraphics();
					doc.Editor.UpdateScreen();
				}
				catch
				{
				}
			}
		}

		public static string GetTypeCodeString(MSCFeatureClass.fcTypeCode code)
		{
			return MSCFeatureClass.m_typeStrings[(int)code];
		}

		public static string GetESRITypeCodeString(MSCFeatureClass.fcTypeCode code)
		{
			return MSCFeatureClass.m_ESRItypeStrings[(int)code];
		}

		public static MSCFeatureClass.fcTypeCode GetTypeCodeFromESRIType(string esriTypeString)
		{
			if (esriTypeString == "esriGeometryPoint")
			{
				return MSCFeatureClass.fcTypeCode.fcTypePoint;
			}
			if (esriTypeString == "esriGeometryPolyline")
			{
				return MSCFeatureClass.fcTypeCode.fcTypePolyline;
			}
			if (esriTypeString == "esriGeometryPolygon")
			{
				return MSCFeatureClass.fcTypeCode.fcTypePolygon;
			}
			if (esriTypeString == "esriGeometryMultiPatch")
			{
				return MSCFeatureClass.fcTypeCode.fcTypeMultiPatch;
			}
			if (esriTypeString == "esriGeometryAnnotation")
			{
				return MSCFeatureClass.fcTypeCode.fcTypeAnnotation;
			}
			return MSCFeatureClass.fcTypeCode.fcTypePoint;
		}

		public static MSCFeatureClass.fcTypeCode GetGeomType(string type)
		{
			type = type.ToLower();
			int num = 0;
			string[] typeStrings = MSCFeatureClass.m_typeStrings;
			for (int i = 0; i < typeStrings.Length; i++)
			{
				string text = typeStrings[i];
				if (type == text.ToLower())
				{
					return (MSCFeatureClass.fcTypeCode)num;
				}
				num++;
			}
			return MSCFeatureClass.fcTypeCode.fcTypePoint;
		}

		public static string GetGeomString(MSCFeatureClass.fcTypeCode typeCode)
		{
			string result;
			try
			{
				result = MSCFeatureClass.m_typeStrings[(int)typeCode];
			}
			catch
			{
				result = MSCFeatureClass.m_typeStrings[1];
			}
			return result;
		}

		public static TypedValue[] GetTypeQuery(MSCFeatureClass.fcTypeCode geometryType)
		{
			switch (geometryType)
			{
			case MSCFeatureClass.fcTypeCode.fcTypePolyline:
				return MSCFeatureClass.tvPolyline;
			case MSCFeatureClass.fcTypeCode.fcTypePolygon:
				return MSCFeatureClass.tvPolygon;
			case MSCFeatureClass.fcTypeCode.fcTypeAnnotation:
				return MSCFeatureClass.tvAnnotation;
			case MSCFeatureClass.fcTypeCode.fcTypeMultiPatch:
				return MSCFeatureClass.tvMultiPatch;
			}
			return MSCFeatureClass.tvPoint;
		}

		private PromptSelectionResult GetEntities()
		{
			Document parentDocument = this.ParentDataset.ParentDocument;
			Editor editor = parentDocument.Editor;
			ResultBuffer fCQuery = this.FCQuery;
			SelectionFilter selectionFilter = new SelectionFilter(fCQuery.AsArray());
			return editor.SelectAll(selectionFilter);
		}

		public void DeleteEntities()
		{
			List<ObjectId> list = new List<ObjectId>(this.GetFeatureIds());
			List<ObjectId> list2 = new List<ObjectId>();
			try
			{
				Document parentDocument = this.ParentDataset.ParentDocument;
				Database database = parentDocument.Database;
				new ObjectIdCollection(this.GetFeatureIds());
				using (parentDocument.LockDocument((DocumentLockMode)4, null, null, false))
				{
					var  transactionManager = database.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						database.DisableUndoRecording(true);
						foreach (ObjectId current in list)
						{
							DBObject groupEntity = this.GetGroupEntity(current, transaction);
							if (groupEntity != null)
							{
								if (!list2.Contains(current))
								{
									list2.Add(current);
								}
							}
							else if (!current.IsErased)
							{
								DBObject @object = transaction.GetObject(current, (OpenMode)1);
								@object.Erase();
							}
						}
						foreach (ObjectId current2 in list2)
						{
							if (!current2.IsErased)
							{
								DBObject groupEntity2 = this.GetGroupEntity(current2, transaction);
								groupEntity2.Erase();
							}
						}
						transaction.Commit();
						database.DisableUndoRecording(false);
					}
				}
			}
			catch
			{
				ErrorReport.ShowErrorMessage("Error deleting entities");
			}
		}

		public ObjectId[] GetEntityIds()
		{
			ObjectId[] result;
			try
			{
				PromptSelectionResult entities = this.GetEntities();
				if (entities.Status == (PromptStatus)5100)
				{
					result = entities.Value.GetObjectIds();
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

		public ObjectId[] GetFeatureIds()
		{
			Document parentDocument = this.ParentDataset.ParentDocument;
			Database database = parentDocument.Database;
			ObjectId[] result;
			using (parentDocument.LockDocument((DocumentLockMode)32, null, null, false))
			{
				var  transactionManager = database.TransactionManager;
				using (Transaction transaction = transactionManager.StartTransaction())
				{
					List<ObjectId> list = new List<ObjectId>();
					try
					{
						ObjectIdCollection objectIdCollection = new ObjectIdCollection(this.GetEntityIds());
						foreach (ObjectId objectId in objectIdCollection)
						{
							DBObject groupEntity = this.GetGroupEntity(objectId, transaction);
							if (groupEntity != null)
							{
								if (!list.Contains(groupEntity.Id))
								{
									list.Add(groupEntity.Id);
								}
							}
							else
							{
								list.Add(objectId);
							}
						}
						transaction.Commit();
						result = list.ToArray();
					}
					catch
					{
						transaction.Commit();
						result = list.ToArray();
					}
				}
			}
			return result;
		}

		public ObjectId[] GetFeatureIdsFromSelection()
		{
			Document parentDocument = this.ParentDataset.ParentDocument;
			Editor editor = parentDocument.Editor;
			PromptSelectionResult promptSelectionResult = editor.SelectImplied();
			ObjectId[] result;
			try
			{
				if (promptSelectionResult.Status == (PromptStatus)5100)
				{
					SelectionSet value = promptSelectionResult.Value;
					ObjectId[] objectIds = value.GetObjectIds();
					if (objectIds != null && objectIds.Length > 0)
					{
						ObjectId[] featureIds = this.GetFeatureIds(new ObjectIdCollection(objectIds));
						result = featureIds;
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

		public ObjectId[] GetFeatureIds(ObjectIdCollection Source)
		{
			List<ObjectId> list = new List<ObjectId>();
			ObjectId[] result;
			try
			{
				Document parentDocument = this.ParentDataset.ParentDocument;
				Database database = parentDocument.Database;
				ObjectIdCollection objectIdCollection = new ObjectIdCollection(this.GetFeatureIds());
				using (parentDocument.LockDocument((DocumentLockMode)32, null, null, false))
				{
					var  transactionManager = database.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						foreach (ObjectId objectId in Source)
						{
							ObjectId objectId2 = objectId;
							DBObject groupEntity = this.GetGroupEntity(objectId, transaction);
							if (groupEntity != null)
							{
								objectId2 = groupEntity.Id;
							}
							if (objectIdCollection.Contains(objectId2) && !list.Contains(objectId2))
							{
								list.Add(objectId2);
							}
						}
						transaction.Commit();
					}
				}
				result = list.ToArray();
			}
			catch
			{
				result = list.ToArray();
			}
			return result;
		}

		public void Select()
		{
			Document parentDocument = this.ParentDataset.ParentDocument;
			DocumentLock documentLock = parentDocument.LockDocument((DocumentLockMode)32, null, null, false);
			using (documentLock)
			{
                System.Windows.Forms.Application.UseWaitCursor = true;
				try
				{
					ObjectId[] entityIds = this.GetEntityIds();
					if (entityIds == null)
					{
                        System.Windows.Forms.Application.UseWaitCursor = false;
						CmdLine.DisplayCountMessage(parentDocument.Editor, 0);
					}
					else if (entityIds.Length == 0)
					{
                        System.Windows.Forms.Application.UseWaitCursor = false;
						CmdLine.DisplayCountMessage(parentDocument.Editor, 0);
					}
					else
					{
						parentDocument.Editor.SetImpliedSelection(DocUtil.ExpandGroupObjectIds(parentDocument, entityIds));
                        System.Windows.Forms.Application.UseWaitCursor = false;
						CmdLine.DisplayCountMessage(parentDocument.Editor, entityIds.Length);
					}
				}
				catch
				{
                    System.Windows.Forms.Application.UseWaitCursor = false;
				}
			}
		}

		public bool IsSingleLayerQuery()
		{
			if (base.GetType() == typeof(MSCFeatureClassSubType))
			{
				return true;
			}
			bool result;
			try
			{
				TypedValue[] array = this.Query.AsArray();
				if (array.Length > 1)
				{
					result = false;
				}
				else if (array[0].TypeCode != 8)
				{
					result = false;
				}
				else
				{
					string source = (string)array[0].Value;
					if (source.Contains(','))
					{
						result = false;
					}
					else if (source.Contains('*'))
					{
						result = false;
					}
					else if (source.Contains('?'))
					{
						result = false;
					}
					else
					{
						result = true;
					}
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public string GetSingleLayerQueryLayerName()
		{
			if (!this.IsSingleLayerQuery())
			{
				return null;
			}
			string result;
			try
			{
				TypedValue[] array = this.Query.AsArray();
				string text = (string)array[0].Value;
				result = text;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public string GetFirstLayerFromQuery()
		{
			string text = "0";
			string result;
			try
			{
				TypedValue[] array = this.Query.AsArray();
				if (array.Length > 1)
				{
					result = text;
				}
				else
				{
					TypedValue typedValue = array[0];
					bool flag = false;
					TypedValue[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						TypedValue typedValue2 = array2[i];
						if (typedValue2.TypeCode == 8)
						{
							typedValue = typedValue2;
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						result = text;
					}
					else
					{
						string text2 = (string)typedValue.Value;
						string[] array3 = text2.Split(new char[]
						{
							','
						});
						string text3 = array3[0];
						if (!text3.Contains('*') && !text3.Contains('?'))
						{
							text = text3.Trim();
						}
						result = text;
					}
				}
			}
			catch
			{
				result = text;
			}
			return result;
		}

		public int ZoomFeatures()
		{
			int result;
			try
			{
				Document document = AfaDocData.ActiveDocData.Document;
				Editor editor = document.Editor;
				PromptSelectionResult promptSelectionResult = editor.SelectImplied();
				SelectionSet impliedSelection = null;
				if (promptSelectionResult.Status == (PromptStatus)5100)
				{
					impliedSelection = promptSelectionResult.Value;
				}
				ObjectId[] entityIds = this.GetEntityIds();
				if (entityIds != null)
				{
					if (entityIds.Length > 0)
					{
						document.Editor.SetImpliedSelection(entityIds);
						Utils.ZoomObjects(false);
					}
					editor.SetImpliedSelection(impliedSelection);
					result = entityIds.Length;
				}
				else
				{
					result = 0;
				}
			}
			catch
			{
				result = 0;
			}
			return result;
		}

		private void OnPropertyChanged(string propName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
			}
		}
	}
}
