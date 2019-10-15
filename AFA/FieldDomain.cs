using ArcGIS10Types;
using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AFA
{
    public class FieldDomain
	{
		private MSCDataset _parentDataset;

		public static string DictionaryName = "ESRI_DOMAINS";

		public string Name
		{
			get;
			set;
		}

		public string DisplayName
		{
			get;
			set;
		}

		public CadField.CadFieldType FieldType
		{
			get;
			set;
		}

		public string DomainType
		{
			get;
			set;
		}

		public ObjectId Id
		{
			get;
			set;
		}

		public MSCDataset ParentDataset
		{
			get
			{
				if (this._parentDataset == null)
				{
					try
					{
						this._parentDataset = AfaDocData.ActiveDocData.DocDataset;
					}
					catch
					{
						return null;
					}
				}
				return this._parentDataset;
			}
			set
			{
				this._parentDataset = value;
			}
		}

		public ObservableDictionary<string, MSCCodedValue> CodedValues
		{
			get;
			set;
		}

		public MSCCodedValue FauxNull
		{
			get;
			set;
		}

		public object MinValue
		{
			get;
			set;
		}

		public object MaxValue
		{
			get;
			set;
		}

		public ObservableCollection<MSCCodedValue> CodedValuesDisplayCollection
		{
			get
			{
				if (this.CodedValues == null)
				{
					return null;
				}
				if (this.CodedValues.Count == 0)
				{
					return null;
				}
				ObservableCollection<MSCCodedValue> observableCollection = new ObservableCollection<MSCCodedValue>();
				if (this.FauxNull != null)
				{
					observableCollection.Add(this.FauxNull);
				}
				foreach (MSCCodedValue current in this.CodedValues.Values)
				{
					observableCollection.Add(current);
				}
				return observableCollection;
			}
		}

		public List<MSCCodedValue> CodedValuesDisplayList
		{
			get
			{
				if (this.CodedValues == null)
				{
					return null;
				}
				if (this.CodedValues.Count == 0)
				{
					return null;
				}
				List<MSCCodedValue> list = new List<MSCCodedValue>();
				if (this.FauxNull != null)
				{
					list.Add(this.FauxNull);
				}
				foreach (MSCCodedValue current in this.CodedValues.Values)
				{
					list.Add(current);
				}
				return list;
			}
		}

		public FieldDomain(string name, CadField.CadFieldType fieldType, string domainType)
		{
			this.DisplayName = name;
			this.Name = DocUtil.FixSymbolName(name);
			this.FieldType = fieldType;
			this.DomainType = domainType;
			this.CodedValues = new ObservableDictionary<string, MSCCodedValue>();
			this.FauxNull = null;
			this.MinValue = 0;
			this.MaxValue = 0;
		}

		public FieldDomain(Transaction t, ObjectId id)
		{
			this.DomainType = "RangeDomain";
			this.CodedValues = new ObservableDictionary<string, MSCCodedValue>();
			this.MinValue = 0;
			this.MaxValue = 0;
			this.Id = id;
			this.Read(this.Id, t);
		}

		public Type GetFieldType()
		{
			if (this.FieldType == CadField.CadFieldType.Double)
			{
				return typeof(double);
			}
			if (this.FieldType == CadField.CadFieldType.Integer)
			{
				return typeof(int);
			}
			if (this.FieldType == CadField.CadFieldType.Short)
			{
				return typeof(short);
			}
			if (this.FieldType == CadField.CadFieldType.String)
			{
				return typeof(string);
			}
			return typeof(int);
		}

		public bool HasCodedValues()
		{
			return this.CodedValues != null && this.CodedValues.Count != 0;
		}

		public void Write(Database db, Transaction t)
		{
			try
			{
				DBDictionary dBDictionary = this.ParentDataset.Open(db, t, FieldDomain.DictionaryName, (OpenMode)1);
				DBDictionary dBDictionary2 = new DBDictionary();
				this.Id = dBDictionary.SetAt(this.Name, dBDictionary2);
				dBDictionary2.DisableUndoRecording(true);
				t.AddNewlyCreatedDBObject(dBDictionary2, true);
				this.WriteTypedValue(db, t, dBDictionary2, "KeyName", new TypedValue(1, this.Name));
				this.WriteTypedValue(db, t, dBDictionary2, "DisplayName", new TypedValue(1, this.DisplayName));
				this.WriteTypedValue(db, t, dBDictionary2, "DomainType", new TypedValue(1, this.DomainType));
				this.WriteTypedValue(db, t, dBDictionary2, "DomainFieldType", new TypedValue(90, this.FieldType));
				if (this.DomainType == "RangeDomain")
				{
					this.WriteTypedValue(db, t, dBDictionary2, "DomainMinimum", CadField.CreateTypedValue(this.MinValue));
					this.WriteTypedValue(db, t, dBDictionary2, "DomainMaximum", CadField.CreateTypedValue(this.MaxValue));
				}
				if (this.DomainType == "CodedValueDomain")
				{
					List<TypedValue> list = new List<TypedValue>();
					foreach (KeyValuePair<string, MSCCodedValue> keyValuePair in this.CodedValues)
					{
						MSCCodedValue value = keyValuePair.Value;
						list.Add(CadField.CreateTypedValue(value.DisplayName));
						list.Add(CadField.CreateTypedValue(value.Value));
					}
					try
					{
						ResultBuffer rb = new ResultBuffer(list.ToArray());
						this.WriteResultBuffer(db, t, dBDictionary2, "CodedValues", rb);
					}
					catch (System.Exception)
					{
					}
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
				if (dBDictionary.Contains("KeyName"))
				{
					Xrecord xrecord = (Xrecord)t.GetObject(dBDictionary.GetAt("KeyName"), 0);
					TypedValue[] array = xrecord.Data.AsArray();
					for (int i = 0; i < array.Length; i++)
					{
						TypedValue typedValue = array[i];
						string name = typedValue.Value.ToString();
						this.Name = name;
					}
				}
				else
				{
					this.Name = "DomainName";
				}
				if (dBDictionary.Contains("DisplayName"))
				{
					Xrecord xrecord2 = (Xrecord)t.GetObject(dBDictionary.GetAt("DisplayName"), 0);
					TypedValue[] array2 = xrecord2.Data.AsArray();
					for (int j = 0; j < array2.Length; j++)
					{
						TypedValue typedValue2 = array2[j];
						string displayName = typedValue2.Value.ToString();
						this.DisplayName = displayName;
					}
				}
				else
				{
					this.DisplayName = this.Name;
				}
				if (dBDictionary.Contains("DomainType"))
				{
					Xrecord xrecord3 = (Xrecord)t.GetObject(dBDictionary.GetAt("DomainType"), 0);
					TypedValue[] array3 = xrecord3.Data.AsArray();
					for (int k = 0; k < array3.Length; k++)
					{
						TypedValue typedValue3 = array3[k];
						string domainType = typedValue3.Value.ToString();
						this.DomainType = domainType;
					}
				}
				if (dBDictionary.Contains("DomainFieldType"))
				{
					Xrecord xrecord4 = (Xrecord)t.GetObject(dBDictionary.GetAt("DomainFieldType"), 0);
					TypedValue[] array4 = xrecord4.Data.AsArray();
					int fieldType = int.Parse(array4[0].Value.ToString());
					this.FieldType = (CadField.CadFieldType)fieldType;
				}
				if (this.DomainType == "RangeDomain")
				{
					Xrecord xrecord5 = (Xrecord)t.GetObject(dBDictionary.GetAt("DomainMinimum"), 0);
					TypedValue[] array5 = xrecord5.Data.AsArray();
					this.MinValue = array5[0].Value;
					Xrecord xrecord6 = (Xrecord)t.GetObject(dBDictionary.GetAt("DomainMaximum"), 0);
					TypedValue[] array6 = xrecord6.Data.AsArray();
					this.MaxValue = array6[0].Value;
				}
				else if (this.DomainType == "CodedValueDomain")
				{
					this.CodedValues = new ObservableDictionary<string, MSCCodedValue>();
					Xrecord xrecord7 = (Xrecord)t.GetObject(dBDictionary.GetAt("CodedValues"), 0);
					TypedValue[] array7 = xrecord7.Data.AsArray();
					int num = array7.Length;
					for (int l = 0; l < num; l += 2)
					{
						try
						{
							TypedValue typedValue4 = array7[l];
							TypedValue typedValue5 = array7[l + 1];
							MSCCodedValue mSCCodedValue = new MSCCodedValue(typedValue4.Value.ToString(), typedValue5.Value);
							this.CodedValues.Add(mSCCodedValue.DisplayName, mSCCodedValue);
						}
						catch
						{
						}
					}
				}
			}
			catch (SystemException)
			{
			}
		}

		protected void WriteResultBuffer(Database db, Transaction t, DBDictionary dict, string key, ResultBuffer rb)
		{
			try
			{
				Xrecord xrecord = new Xrecord();
				xrecord.Data=(rb);
				dict.SetAt(key, xrecord);
				xrecord.DisableUndoRecording(true);
				t.AddNewlyCreatedDBObject(xrecord, true);
			}
			catch (System.Exception ex)
			{
				throw ex;
			}
		}

		protected void WriteTypedValue(Database db, Transaction t, DBDictionary dict, string key, TypedValue tv)
		{
			Xrecord xrecord = new Xrecord();
			xrecord.Data=(new ResultBuffer(new TypedValue[]
			{
				tv
			}));
			dict.SetAt(key, xrecord);
			xrecord.DisableUndoRecording(true);
			t.AddNewlyCreatedDBObject(xrecord, true);
		}

		public bool IsWithinRangeValue(object newValue)
		{
			bool result;
			try
			{
				TypedValue typedValue = CadField.CreateTypedValue(this.FieldType, newValue.ToString());
				TypedValue typedValue2 = CadField.CreateTypedValue(this.FieldType, this.MinValue.ToString());
				TypedValue typedValue3 = CadField.CreateTypedValue(this.FieldType, this.MaxValue.ToString());
				IComparable comparable = (IComparable)typedValue.Value;
				if (comparable.CompareTo(typedValue2.Value) < 0)
				{
					result = false;
				}
				else if (comparable.CompareTo(typedValue3.Value) > 0)
				{
					result = false;
				}
				else
				{
					result = true;
				}
			}
			catch
			{
				result = true;
			}
			return result;
		}

		public string FixName(Database db, Transaction t)
		{
			if (!this.ParentDataset.Contains(db, t, FieldDomain.DictionaryName))
			{
				return this.Name;
			}
			if (this.IsUniqueName(db, t))
			{
				return this.Name;
			}
			string name = this.Name;
			for (int i = 0; i < 999; i++)
			{
				this.Name = name + i.ToString();
				if (this.IsUniqueName(db, t))
				{
					return this.Name;
				}
			}
			return null;
		}

		private bool IsUniqueName(Database db, Transaction t)
		{
			DBDictionary dBDictionary = this.ParentDataset.Open(db, t, FieldDomain.DictionaryName, 0);
			if (!dBDictionary.Contains(this.Name))
			{
				return true;
			}
			FieldDomain fieldDomain = new FieldDomain(t, dBDictionary.GetAt(this.Name));
			fieldDomain.ParentDataset = this.ParentDataset;
			if (FieldDomain.AreEqual(this, fieldDomain))
			{
				this.Id = fieldDomain.Id;
				return true;
			}
			return false;
		}

		public MSCCodedValue GetCodedValue(object testValue)
		{
			foreach (MSCCodedValue current in this.CodedValues.Values)
			{
				if (current.Value.Equals(testValue))
				{
					MSCCodedValue result = current;
					return result;
				}
			}
			if (this.FauxNull != null)
			{
				return this.FauxNull;
			}
			return null;
		}

		public bool IsValidCodedValue(object testValue)
		{
			return this.CodedValues == null || this.GetCodedValue(testValue) != null;
		}

		public TypedValue CheckValue(object testValue)
		{
			MSCCodedValue mSCCodedValue = this.GetCodedValue(testValue);
			if (mSCCodedValue == null)
			{
				if (this.FauxNull == null)
				{
					this.SetFauxNull(testValue);
				}
				mSCCodedValue = this.FauxNull;
			}
			return CadField.CreateTypedValue(this.FieldType, mSCCodedValue.Value);
		}

		public MSCCodedValue SetFauxNull(object nullValue)
		{
			this.FauxNull = new MSCCodedValue(" ", nullValue);
			return this.FauxNull;
		}

		public MSCCodedValue GenerateFauxNull()
		{
			this.FauxNull = null;
			if (this.FieldType == CadField.CadFieldType.Integer)
			{
				int num = 0;
				while (true)
				{
					MSCCodedValue codedValue = this.GetCodedValue(num);
					if (codedValue == null)
					{
						break;
					}
					num--;
					if (codedValue == null)
					{
						goto Block_3;
					}
				}
				this.FauxNull = new MSCCodedValue(" ", num);
				return this.FauxNull;
				Block_3:;
			}
			else if (this.FieldType == CadField.CadFieldType.Double)
			{
				double num2 = 0.0;
				while (true)
				{
					MSCCodedValue codedValue2 = this.GetCodedValue(num2);
					if (codedValue2 == null)
					{
						break;
					}
					num2 -= 1.0;
					if (codedValue2 == null)
					{
						goto Block_6;
					}
				}
				this.FauxNull = new MSCCodedValue(" ", num2);
				return this.FauxNull;
				Block_6:;
			}
			else if (this.FieldType == CadField.CadFieldType.Short)
			{
				short num3 = 0;
				while (true)
				{
					MSCCodedValue codedValue3 = this.GetCodedValue(num3);
					if (codedValue3 == null)
					{
						break;
					}
					num3 -= 1;
					if (codedValue3 == null)
					{
						goto Block_9;
					}
				}
				this.FauxNull = new MSCCodedValue(" ", num3);
				return this.FauxNull;
				Block_9:;
			}
			else if (this.FieldType == CadField.CadFieldType.String)
			{
				string text = "null";
				if (this.GetCodedValue(text) == null)
				{
					this.FauxNull = new MSCCodedValue(" ", text);
					return this.FauxNull;
				}
				text = "";
				if (this.GetCodedValue(text) == null)
				{
					this.FauxNull = new MSCCodedValue(" ", text);
					return this.FauxNull;
				}
				int num4 = 1;
				while (true)
				{
					text = Convert.ToString(num4);
					MSCCodedValue codedValue4 = this.GetCodedValue(text);
					if (codedValue4 == null)
					{
						break;
					}
					num4++;
					if (num4 > 255)
					{
						goto Block_14;
					}
					if (codedValue4 == null)
					{
						goto IL_1A9;
					}
				}
				this.FauxNull = new MSCCodedValue(" ", text);
				return this.FauxNull;
				Block_14:
				return null;
			}
			IL_1A9:
			return null;
		}

		public bool IsFauxNull(MSCCodedValue testValue)
		{
			return testValue == null || (this.FauxNull != null && this.FauxNull.Value.Equals(testValue.Value));
		}

		public MSCCodedValue GetFirstCodedValue()
		{
			Array array = this.CodedValues.ToArray<KeyValuePair<string, MSCCodedValue>>();
			return (MSCCodedValue)array.GetValue(0);
		}

		public static FieldDomain ToDomain(Transaction t, MSCDataset parent, ObjectId id)
		{
			FieldDomain result;
			try
			{
				FieldDomain fieldDomain = new FieldDomain(t, id);
				if (parent != null)
				{
					fieldDomain.ParentDataset = parent;
				}
				fieldDomain.Read(id, t);
				result = fieldDomain;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static FieldDomain ToDomain(Transaction t, ObjectId id)
		{
			FieldDomain result;
			try
			{
				FieldDomain fieldDomain = new FieldDomain(t, id);
				fieldDomain.Read(id, t);
				result = fieldDomain;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static FieldDomain GetDomain(Domain fs_domain)
		{
			FieldDomain result;
			try
			{
				ObservableDictionary<string, FieldDomain> domains = AfaDocData.ActiveDocData.DocDataset.Domains;
				string key = DocUtil.FixSymbolName(fs_domain.DomainName);
				FieldDomain fieldDomain = null;
				if (domains.TryGetValue(key, out fieldDomain) && FieldDomain.AreEquivalent(fieldDomain, fs_domain))
				{
					result = fieldDomain;
				}
				else
				{
					string domainType = "";
					if (fs_domain is CodedValueDomain)
					{
						domainType = "CodedValueDomain";
					}
					else if (fs_domain is RangeDomain)
					{
						domainType = "RangeDomain";
					}
					CadField.CadFieldType cadFieldType = FieldDomain.GetCadFieldType(fs_domain.FieldType);
					fieldDomain = new FieldDomain(fs_domain.DomainName, cadFieldType, domainType);
					if (fs_domain is CodedValueDomain)
					{
						CodedValueDomain codedValueDomain = (CodedValueDomain)fs_domain;
						if (codedValueDomain.CodedValues.Length <= 0)
						{
							goto IL_129;
						}
						CodedValue[] array = codedValueDomain.CodedValues.ToArray<CodedValue>();
						try
						{
							CodedValue[] array2 = array;
							for (int i = 0; i < array2.Length; i++)
							{
								CodedValue codedValue = array2[i];
								fieldDomain.CodedValues.Add(DocUtil.FixSymbolName(codedValue.Name), new MSCCodedValue(codedValue.Name, cadFieldType, codedValue.Code));
							}
							goto IL_129;
						}
						catch (SystemException ex)
						{
							string arg_FC_0 = ex.Message;
							goto IL_129;
						}
					}
					if (fs_domain is RangeDomain)
					{
						RangeDomain rangeDomain = (RangeDomain)fs_domain;
						fieldDomain.MinValue = rangeDomain.MinValue;
						fieldDomain.MaxValue = rangeDomain.MaxValue;
					}
					IL_129:
					AfaDocData.ActiveDocData.DocDataset.Domains.Add(fieldDomain.Name, fieldDomain);
					result = fieldDomain;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static CadField.CadFieldType GetCadFieldType(esriFieldType t)
		{
			CadField.CadFieldType result = CadField.CadFieldType.String;
			if (t == esriFieldType.esriFieldTypeDouble)
			{
				result = CadField.CadFieldType.Double;
			}
			else if (t == esriFieldType.esriFieldTypeSingle)
			{
				result = CadField.CadFieldType.Double;
			}
			else if (t == esriFieldType.esriFieldTypeInteger)
			{
				result = CadField.CadFieldType.Integer;
			}
			else if (t == esriFieldType.esriFieldTypeOID)
			{
				result = CadField.CadFieldType.Integer;
			}
			else if (t == esriFieldType.esriFieldTypeSmallInteger)
			{
				result = CadField.CadFieldType.Short;
			}
			else if (t == esriFieldType.esriFieldTypeString)
			{
				result = CadField.CadFieldType.String;
			}
			return result;
		}

		private static bool AreEquivalent(FieldDomain a, Domain b)
		{
			if (string.Compare(a.DisplayName, b.DomainName, true) != 0)
			{
				return false;
			}
			CadField.CadFieldType cadFieldType = FieldDomain.GetCadFieldType(b.FieldType);
			if (cadFieldType != a.FieldType)
			{
				return false;
			}
			if (b is CodedValueDomain)
			{
				if (a.DomainType != "CodedValueDomain")
				{
					return false;
				}
			}
			else if (b is RangeDomain && a.DomainType != "RangeDomain")
			{
				return false;
			}
			return true;
		}

		private static bool AreEqual(FieldDomain a, FieldDomain b)
		{
			return string.Compare(a.Name, b.Name, true) == 0 && string.Compare(a.DisplayName, b.DisplayName, true) == 0 && string.Compare(a.DomainType, b.DomainType, true) == 0 && a.FieldType == b.FieldType && a.MinValue.Equals(b.MinValue) && a.MaxValue.Equals(b.MaxValue) && a.CodedValues.Count == b.CodedValues.Count && Utility.DictionaryEqual<string, MSCCodedValue>(a.CodedValues, b.CodedValues);
		}
	}
}
