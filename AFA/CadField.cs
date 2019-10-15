using ArcGIS10Types;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AFA
{
	public class CadField
	{
		public enum CadFieldType
		{
			Double = 40,
			String = 1,
			Integer = 90,
			Short = 70
		}

		public static Dictionary<string, int> CadFieldStrings = new Dictionary<string, int>
		{
			{
				"Double",
				40
			},
			{
				"String",
				1
			},
			{
				"Integer",
				90
			},
			{
				"Short",
				70
			},
			{
				"Date",
				1
			},
			{
				"Float",
				40
			}
		};

		public string Name
		{
			get;
			set;
		}

		public TypedValue Value
		{
			get;
			set;
		}

		public bool ReadOnly
		{
			get;
			set;
		}

		public bool Visible
		{
			get;
			set;
		}

		public ObjectId Id
		{
			get;
			set;
		}

		public short Length
		{
			get;
			set;
		}

		public bool TypeField
		{
			get;
			set;
		}

		public FieldDomain Domain
		{
			get;
			set;
		}

		public short ExtendedType
		{
			get;
			set;
		}

		public CadField.CadFieldType FieldType
		{
			get
			{
				if (this.Value.TypeCode== 40)
				{
					return CadField.CadFieldType.Double;
				}
				if (this.Value.TypeCode== 70)
				{
					return CadField.CadFieldType.Short;
				}
				if (this.Value.TypeCode == 90)
				{
					return CadField.CadFieldType.Integer;
				}
				return CadField.CadFieldType.String;
			}
		}

		public bool SupportsLength
		{
			get
			{
				return this.FieldType == CadField.CadFieldType.String;
			}
		}

		public CadField()
		{
			this.Visible = true;
			this.ReadOnly = false;
			this.Id = ObjectId.Null;
			this.Domain = null;
			this.ExtendedType = -1;
		}

		public CadField(CadField srcCF)
		{
			this.Name = srcCF.Name;
			this.Visible = srcCF.Visible;
			this.Length = srcCF.Length;
			this.Id = srcCF.Id;
			this.ReadOnly = srcCF.ReadOnly;
			this.TypeField = srcCF.TypeField;
			this.Domain = srcCF.Domain;
			this.ExtendedType = srcCF.ExtendedType;
			TypedValue arg_6C_0 = srcCF.Value;
			srcCF.Value = new TypedValue((int)srcCF.Value.TypeCode, srcCF.Value.Value);
		}

		public object GetExtendedValue()
		{
			if (this.ExtendedType == -1)
			{
				return this.Value.Value;
			}
			object result = this.Value.Value;
			switch (this.ExtendedType)
			{
			case 2:
			{
				double num = 0.0;
				if (!double.TryParse(this.Value.Value.ToString(), out num))
				{
					return null;
				}
				result = num;
				break;
			}
			case 3:
			{
				float num2 = 0f;
				if (!float.TryParse(this.Value.Value.ToString(), out num2))
				{
					return null;
				}
				result = num2;
				break;
			}
			case 5:
			{
				DateTime today = DateTime.Today;
				if (DateTime.TryParse(this.Value.Value.ToString(), out today))
				{
					return today;
				}
				return null;
			}
			}
			return result;
		}

		public TypedValue ToTV()
		{
			return this.Value;
		}

		public string GetTypeString()
		{
			if (this.ExtendedType == 5)
			{
				return "Date";
			}
			if (this.ExtendedType == 3)
			{
				return "Float";
			}
			if (this.Value.TypeCode== 40)
			{
				return "Double";
			}
			if (this.Value.TypeCode == 70)
			{
				return "Short";
			}
			if (this.Value.TypeCode == 90)
			{
				return "Integer";
			}
			return "Text";
		}

		public static int GetTypeCode(string str)
		{
			int result;
			try
			{
				result = CadField.CadFieldStrings[str];
			}
			catch
			{
				result = 1;
			}
			return result;
		}

		public static string CodeString(int value)
		{
			foreach (KeyValuePair<string, int> current in CadField.CadFieldStrings)
			{
				if (current.Value == value)
				{
					return current.Key;
				}
			}
			return "";
		}

		public static CadField.CadFieldType FieldTypeCode(string strVal)
		{
			if (CadField.CadFieldStrings.ContainsKey(strVal))
			{
				int result;
				CadField.CadFieldStrings.TryGetValue(strVal, out result);
				return (CadField.CadFieldType)result;
			}
			return CadField.CadFieldType.String;
		}

		public static bool IsValidTypedValue(CadField.CadFieldType code, string value)
		{
			if (value == null)
			{
				return false;
			}
			if (value == "")
			{
				return true;
			}
			if (CadField.CadFieldType.Double == code)
			{
				double num;
				return double.TryParse(value, out num);
			}
			if (CadField.CadFieldType.String == code)
			{
				return true;
			}
			if (CadField.CadFieldType.Integer == code)
			{
				int num2;
				return int.TryParse(value, out num2);
			}
			short num3;
			return CadField.CadFieldType.Short == code && short.TryParse(value, out num3);
		}

		public static TypedValue CreateTypedValue(string typeString, object value)
		{
			CadField.CadFieldType code = CadField.TypeCodeFromString(typeString);
			return CadField.CreateTypedValue(code, value.ToString());
		}

		public static CadField.CadFieldType TypeCodeFromString(string typeString)
		{
			CadField.CadFieldType result = CadField.CadFieldType.String;
			if (string.Equals("Date", typeString, StringComparison.CurrentCultureIgnoreCase))
			{
				result = CadField.CadFieldType.String;
			}
			else if (string.Equals("Float", typeString, StringComparison.CurrentCultureIgnoreCase))
			{
				result = CadField.CadFieldType.Double;
			}
			else if (string.Equals("Double", typeString, StringComparison.CurrentCultureIgnoreCase))
			{
				result = CadField.CadFieldType.Double;
			}
			else if (string.Equals("Short", typeString, StringComparison.CurrentCultureIgnoreCase))
			{
				result = CadField.CadFieldType.Short;
			}
			else if (string.Equals("Integer", typeString, StringComparison.CurrentCultureIgnoreCase))
			{
				result = CadField.CadFieldType.Integer;
			}
			else if (string.Equals("Text", typeString, StringComparison.CurrentCultureIgnoreCase))
			{
				result = CadField.CadFieldType.String;
			}
			return result;
		}

		public static short ExtendedTypeFromString(string typeString)
		{
			if (string.Equals("Date", typeString, StringComparison.CurrentCultureIgnoreCase))
			{
				return 5;
			}
			if (string.Equals("Float", typeString, StringComparison.CurrentCultureIgnoreCase))
			{
				return 3;
			}
			return -1;
		}

		public static TypedValue CreateTypedValue(CadField.CadFieldType code, object objValue)
		{
			string text = objValue.ToString();
			if (!CadField.IsValidTypedValue(code, text))
			{
				return new TypedValue(1, text);
			}
			if (CadField.CadFieldType.Double == code)
			{
				return new TypedValue((int)((short)code), double.Parse(text));
			}
			if (CadField.CadFieldType.String == code)
			{
				return new TypedValue(1, text);
			}
			if (CadField.CadFieldType.Integer == code)
			{
				return new TypedValue((int)((short)code), int.Parse(text));
			}
			if (CadField.CadFieldType.Short == code)
			{
				return new TypedValue((int)((short)code), short.Parse(text));
			}
			return new TypedValue(1, text);
		}

		public static TypedValue CreateTypedValue(object value)
		{
			if (value is double)
			{
				return new TypedValue(40, double.Parse(value.ToString()));
			}
			if (value is int)
			{
				return new TypedValue(90, int.Parse(value.ToString()));
			}
			if (value is short)
			{
				return new TypedValue(70, short.Parse(value.ToString()));
			}
			return new TypedValue(1, value.ToString());
		}

		public static object CreateDefaultValue(CadField.CadFieldType fieldType)
		{
			if (fieldType == CadField.CadFieldType.String)
			{
				return "";
			}
			if (fieldType == CadField.CadFieldType.Double)
			{
				return 0.0;
			}
			if (fieldType == CadField.CadFieldType.Integer)
			{
				return 0;
			}
			if (fieldType == CadField.CadFieldType.Short)
			{
				return 0;
			}
			return null;
		}

		public static bool IsValidFieldName(string name)
		{
			string text = " <>/\\“:;?*|=‘";
			return name.Length != 0 && name.Length <= 30 && -1 == name.IndexOfAny(text.ToCharArray()) && char.IsLetter(name[0]);
		}

		public static string FixFieldName(string name)
		{
			name = name.Replace("<", "_lt_");
			name = name.Replace(">", "_gt_");
			name = name.Replace("/", ".");
			name = name.Replace("\\", ".");
			name = name.Replace(":", ".");
			name = name.Replace("?", ".");
			name = name.Replace("*", ".");
			name = name.Replace("|", "-");
			name = name.Replace("=", "-");
			name = name.Replace(" ", "-");
			if (!char.IsLetter(name[0]))
			{
				name = "a" + name;
			}
			if (name.Length > 30)
			{
				name.Remove(30);
			}
			return name;
		}

		public void Read(ObjectId id, string name, Transaction t)
		{
			Xrecord xrecord = (Xrecord)t.GetObject(id, 0);
			if (xrecord != null)
			{
				CadField cadField = CadField.ToCadField(name, xrecord, t);
				cadField.Id = id;
			}
		}

		public void WriteValue(DBDictionary dict, Transaction t)
		{
			try
			{
				Xrecord xrecord = new Xrecord();
				ResultBuffer data = new ResultBuffer(new TypedValue[]
				{
					this.Value
				});
				xrecord.Data=(data);
				this.Id = dict.SetAt(this.Name, xrecord);
				xrecord.DisableUndoRecording(true);
				t.AddNewlyCreatedDBObject(xrecord, true);
			}
			catch
			{
			}
		}

		public void Write(DBDictionary dict, Transaction t)
		{
			try
			{
				Xrecord xrecord = new Xrecord();
				ResultBuffer resultBuffer = new ResultBuffer(new TypedValue[]
				{
					this.Value
				});
				if (this.ReadOnly)
				{
					resultBuffer.Add(new TypedValue(1, "ReadOnly"));
				}
				if (!this.Visible)
				{
					resultBuffer.Add(new TypedValue(1, "NotVisible"));
				}
				if (this.TypeField)
				{
					resultBuffer.Add(new TypedValue(1, "TypeField"));
				}
				if (this.Length > 0)
				{
					resultBuffer.Add(new TypedValue(90, this.Length));
				}
				if (this.Value.TypeCode == 1 && this.Length <= 0)
				{
					resultBuffer.Add(new TypedValue(90, 225));
				}
				if (this.ExtendedType > -1)
				{
					resultBuffer.Add(new TypedValue(170, this.ExtendedType));
				}
				if (this.Domain != null)
				{
					if (this.Domain.Id.IsNull)
					{
						this.Domain.ParentDataset = AfaDocData.ActiveDocData.DocDataset;
						this.Domain.FixName(dict.Database, t);
						if (string.IsNullOrEmpty(this.Domain.Name))
						{
							this.Domain = null;
						}
						if (this.Domain != null)
						{
							this.Domain.Write(dict.Database, t);
						}
					}
					if (!this.Domain.Id.IsNull)
					{
						resultBuffer.Add(new TypedValue(340, this.Domain.Id));
					}
					else
					{
						this.Domain = null;
					}
				}
				xrecord.Data=(resultBuffer);
				this.Id = dict.SetAt(this.Name, xrecord);
				xrecord.DisableUndoRecording(true);
				t.AddNewlyCreatedDBObject(xrecord, true);
			}
			catch
			{
			}
		}

		private static DBDictionary OpenAttributeDictionary(ObjectId id, Transaction t, OpenMode mode)
		{
            //DBObject @object = t.GetObject(id, (OpenMode)1);
            //DBDictionary parentDict;
            //try
            //{
            //	parentDict = (DBDictionary)t.GetObject(@object.ExtensionDictionary(), 1, false);
            //}
            //catch
            //{
            //	@object.CreateExtensionDictionary();
            //	parentDict = (DBDictionary)t.GetObject(@object.ExtensionDictionary(), 1, false);
            //}
            //return CadField.OpenAttributeDictionary(parentDict, t, mode);

            DBDictionary dictionary;
            DBObject obj2 = t.GetObject(id, OpenMode.ForWrite);
            if (!obj2.ExtensionDictionary.IsValid)
            {
                obj2.CreateExtensionDictionary();
                dictionary = (DBDictionary)t.GetObject(obj2.ExtensionDictionary, OpenMode.ForWrite, false);
            }
            else
            {
                try
                {
                    dictionary = (DBDictionary)t.GetObject(obj2.ExtensionDictionary, OpenMode.ForWrite, false);
                }
                catch
                {
                    obj2.CreateExtensionDictionary();
                    dictionary = (DBDictionary)t.GetObject(obj2.ExtensionDictionary, OpenMode.ForWrite, false);
                }
            }
            return OpenAttributeDictionary(dictionary, t, mode);
        }

		private static DBDictionary OpenAttributeDictionary(DBDictionary parentDict, Transaction t, OpenMode mode)
		{
			if (parentDict.Contains("ESRI_ATTRIBUTES"))
			{
				return (DBDictionary)t.GetObject(parentDict.GetAt("ESRI_ATTRIBUTES"), mode);
			}
			DBDictionary dBDictionary = new DBDictionary();
			ObjectId objectId = parentDict.SetAt("ESRI_ATTRIBUTES", dBDictionary);
			dBDictionary.DisableUndoRecording(true);
			t.AddNewlyCreatedDBObject(dBDictionary, true);
			return (DBDictionary)t.GetObject(objectId, mode);
		}

		public static void WriteNewAttributeDictionary(List<CadField> Fields, DBDictionary parentDict, Transaction t)
		{
			DBDictionary dBDictionary = new DBDictionary();
			parentDict.SetAt("ESRI_ATTRIBUTES", dBDictionary);
			dBDictionary.DisableUndoRecording(true);
			t.AddNewlyCreatedDBObject(dBDictionary, true);
			if (Fields.Count > 0)
			{
				CadField.WriteFields(Fields, dBDictionary, t);
			}
		}

		public static void WriteFields(List<CadField> Fields, DBDictionary attrDict, Transaction t)
		{
			try
			{
				foreach (CadField current in Fields)
				{
					current.Write(attrDict, t);
				}
			}
			catch
			{
			}
		}

		public static CadField ToCadField(string name, Xrecord xrec, Transaction t)
		{
			CadField cadField = new CadField();
			cadField.Name = name;
			TypedValue[] array = xrec.Data.AsArray();
			cadField.Value = array[0];
			for (int i = 1; i < array.Length; i++)
			{
				if (array[i].TypeCode== 1)
				{
					if ((string)array[i].Value == "ReadOnly")
					{
						cadField.ReadOnly = true;
					}
					else if ((string)array[i].Value == "NotVisible")
					{
						cadField.Visible = false;
					}
					else if ((string)array[i].Value== "TypeField")
					{
						cadField.TypeField = true;
					}
				}
				if (array[i].TypeCode== 90)
				{
					try
					{
						cadField.Length = short.Parse(((int)array[i].Value).ToString());
					}
					catch
					{
						cadField.Length = 255;
					}
				}
				if (array[i].TypeCode == 170)
				{
					try
					{
						short extendedType = (short)array[i].Value;
						cadField.ExtendedType = extendedType;
						if (cadField.ExtendedType == 3)
						{
							double value = (double)cadField.Value.Value;
							Convert.ToSingle(value);
						}
					}
					catch
					{
						cadField.ExtendedType = -1;
					}
				}
				if (array[i].TypeCode == 340)
				{
					try
					{
						cadField.Domain = FieldDomain.ToDomain(t, (ObjectId)array[i].Value);
						if (cadField.Domain != null)
						{
							if (cadField.Domain.CodedValues != null && cadField.Domain.GetCodedValue(cadField.Value.Value) == null)
							{
								cadField.Domain.SetFauxNull(cadField.Value.Value);
							}
							if (cadField.TypeField && cadField.Domain.FauxNull == null)
							{
								cadField.Domain.GenerateFauxNull();
							}
						}
					}
					catch
					{
						cadField.Domain = null;
					}
				}
			}
			return cadField;
		}

		public static CadField ToCadField(ArcGIS10Types.Field f, object defaultValue)
		{
			TypedValue value = new TypedValue(0, null);
			object obj = defaultValue;
			bool readOnly = false;
			if (f.Type == esriFieldType.esriFieldTypeGeometry)
			{
				return null;
			}
			if (f.Type == esriFieldType.esriFieldTypeBlob)
			{
				return null;
			}
			if (f.Type == esriFieldType.esriFieldTypeDate)
			{
				if (obj == null)
				{
					obj = 0;
				}
				value = new TypedValue(1, obj.ToString());
			}
			else if (f.Type == esriFieldType.esriFieldTypeDouble)
			{
				if (obj == null)
				{
					obj = 0.0;
				}
				value = new TypedValue(40, (double)obj);
			}
			else if (f.Type == esriFieldType.esriFieldTypeGlobalID)
			{
				if (obj == null)
				{
					obj = "";
				}
				value = new TypedValue(1, obj.ToString());
			}
			else if (f.Type == esriFieldType.esriFieldTypeGUID)
			{
				if (obj == null)
				{
					obj = "";
				}
				value = new TypedValue(1, obj.ToString());
				readOnly = true;
			}
			else if (f.Type == esriFieldType.esriFieldTypeInteger)
			{
				if (obj == null)
				{
					obj = 0;
				}
				value = new TypedValue(90, int.Parse(obj.ToString()));
			}
			else if (f.Type == esriFieldType.esriFieldTypeOID)
			{
				if (obj == null)
				{
					obj = -1;
				}
				value = new TypedValue(90, int.Parse(obj.ToString()));
				readOnly = true;
			}
			else
			{
				if (f.Type == esriFieldType.esriFieldTypeRaster)
				{
					return null;
				}
				if (f.Type == esriFieldType.esriFieldTypeSingle)
				{
					if (obj == null)
					{
						obj = 0f;
					}
					value = new TypedValue(40, double.Parse(obj.ToString()));
				}
				else if (f.Type == esriFieldType.esriFieldTypeSmallInteger)
				{
					if (obj == null)
					{
						obj = 0;
					}
					value = new TypedValue(70, obj);
				}
				else if (f.Type == esriFieldType.esriFieldTypeString)
				{
					if (obj == null)
					{
						obj = "";
					}
					value = new TypedValue(1, obj.ToString());
				}
				else if (f.Type == esriFieldType.esriFieldTypeXML)
				{
					if (obj == null)
					{
						obj = "";
					}
					value = new TypedValue(1, obj.ToString());
				}
			}
			if (value.Value != null)
			{
				CadField cadField = new CadField();
				cadField.Name = f.Name;
				cadField.Value = value;
				cadField.ReadOnly = readOnly;
				cadField.ExtendedType = (short)f.Type;
				if (value.TypeCode== 1)
				{
					short num = (short)f.Length;
					cadField.Length = 255;
					if (num > 0)
					{
						cadField.Length = num;
					}
					else
					{
						num = (short)value.Value.ToString().Length;
						if (num > 0)
						{
							cadField.Length = num;
						}
					}
				}
				return cadField;
			}
			return null;
		}

		public static CadField ToCadField(PropertyInfo f, object defaultValue)
		{
			TypedValue value = new TypedValue(0, null);
			object obj = defaultValue;
			bool readOnly = false;
			if (f.Type == esriFieldType.esriFieldTypeGeometry)
			{
				return null;
			}
			if (f.Type == esriFieldType.esriFieldTypeBlob)
			{
				return null;
			}
			if (f.Type == esriFieldType.esriFieldTypeDate)
			{
				if (obj == null)
				{
					obj = 0;
				}
				value = new TypedValue(1, obj.ToString());
			}
			else if (f.Type == esriFieldType.esriFieldTypeDouble)
			{
				if (obj == null)
				{
					obj = 0.0;
				}
				value = new TypedValue(40, obj);
			}
			else if (f.Type == esriFieldType.esriFieldTypeGlobalID)
			{
				if (obj == null)
				{
					obj = "";
				}
				value = new TypedValue(1, obj.ToString());
				readOnly = true;
			}
			else if (f.Type == esriFieldType.esriFieldTypeGUID)
			{
				if (obj == null)
				{
					obj = "";
				}
				value = new TypedValue(1, obj.ToString());
				readOnly = true;
			}
			else if (f.Type == esriFieldType.esriFieldTypeInteger)
			{
				if (obj == null)
				{
					obj = 0;
				}
				value = new TypedValue(90, obj);
			}
			else if (f.Type == esriFieldType.esriFieldTypeOID)
			{
				if (obj == null)
				{
					obj = -1;
				}
				value = new TypedValue(90, obj);
				readOnly = true;
			}
			else
			{
				if (f.Type == esriFieldType.esriFieldTypeRaster)
				{
					return null;
				}
				if (f.Type == esriFieldType.esriFieldTypeSingle)
				{
					if (obj == null)
					{
						obj = 0f;
					}
					value = new TypedValue(40, obj);
				}
				else if (f.Type == esriFieldType.esriFieldTypeSmallInteger)
				{
					if (obj == null)
					{
						obj = 0;
					}
					value = new TypedValue(70, obj);
				}
				else if (f.Type == esriFieldType.esriFieldTypeString)
				{
					if (obj == null)
					{
						obj = "";
					}
					value = new TypedValue(1, obj.ToString());
				}
				else if (f.Type == esriFieldType.esriFieldTypeXML)
				{
					if (obj == null)
					{
						obj = "";
					}
					value = new TypedValue(1, obj.ToString());
				}
			}
			if (value.Value != null)
			{
				CadField cadField = new CadField();
				cadField.Name = f.Name;
				cadField.Value = value;
				cadField.ReadOnly = readOnly;
				cadField.ExtendedType = (short)f.Type;
				if (value.TypeCode == 1)
				{
					short num = (short)f.Length;
					cadField.Length = 255;
					if (num > 0)
					{
						cadField.Length = num;
					}
					else
					{
						num = (short)value.Value.ToString().Length;
						if (num > 0)
						{
							cadField.Length = num;
						}
					}
				}
				if (f.Domain != null)
				{
					CadField.SetFieldDomain(cadField, f.Domain);
				}
				return cadField;
			}
			return null;
		}

		public static void SetFieldDomain(CadField f, Domain fs_domain)
		{
			FieldDomain domain = FieldDomain.GetDomain(fs_domain);
			f.Domain = domain;
			if (fs_domain is CodedValueDomain && domain.CodedValues != null && domain.GetCodedValue(f.Value.Value) == null)
			{
				domain.SetFauxNull(f.Value.Value);
			}
		}

		public static CadField ToCadField(PropertySetProperty p)
		{
			TypedValue value = new TypedValue(0, null);
			if (p.Value is int)
			{
				value = new TypedValue(90, p.Value);
			}
			else if (p.Value is short || p.Value is short)
			{
				value = new TypedValue(70, (short)p.Value);
			}
			else if (p.Value is bool)
			{
				value = new TypedValue(70, p.Value);
			}
			else if (p.Value is double)
			{
				value = new TypedValue(40, p.Value);
			}
			else if (p.Value is float)
			{
				value = new TypedValue(40, p.Value);
			}
			else if (p.Value is string)
			{
				object value2 = p.Value;
				value = new TypedValue(1, value2);
			}
			else if (p.Value is DateTime)
			{
				value = new TypedValue(1, ((DateTime)p.Value).ToString());
			}
			if (value.Value != null)
			{
				return new CadField
				{
					Name = p.Key,
					Value = value
				};
			}
			return null;
		}

		public static CadField ToCadField(CadField f, object newValue)
		{
			CadField result;
			try
			{
				TypedValue value = new TypedValue((int)f.Value.TypeCode, newValue);
				if (value.Value != null)
				{
					result = new CadField
					{
						Name = f.Name,
						Value = value,
						ReadOnly = f.ReadOnly,
						TypeField = f.TypeField,
						Length = f.Length
					};
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

		public static CadField ToCadField(AGSField f, object value)
		{
			TypedValue value2 = new TypedValue(0, null);
			bool readOnly = false;
			short extendedType = -1;
			if (f.Type == "esriFieldTypeGeometry")
			{
				return null;
			}
			if (f.Type == "esriFieldTypeBlob")
			{
				return null;
			}
			if (f.Type == "esriFieldTypeDate")
			{
				if (value == null)
				{
					value = "";
				}
				value2 = new TypedValue(1, value.ToString());
				extendedType = 5;
			}
			else if (f.Type == "esriFieldTypeDouble")
			{
				extendedType = 2;
				if (value == null)
				{
					value = 0.0;
				}
				value2 = new TypedValue(40, double.Parse(value.ToString()));
			}
			else if (f.Type == "esriFieldTypeGlobalID")
			{
				extendedType = 9;
				if (value == null)
				{
					value = "";
				}
				value2 = new TypedValue(1, value.ToString());
			}
			else if (f.Type == "esriFieldTypeGUID")
			{
				if (value == null)
				{
					value = "";
				}
				value2 = new TypedValue(1, value.ToString());
				extendedType = 11;
				readOnly = true;
			}
			else if (f.Type == "esriFieldTypeInteger")
			{
				if (value == null)
				{
					value = 0;
				}
				value2 = new TypedValue(90, value);
				extendedType = 0;
			}
			else if (f.Type == "esriFieldTypeOID")
			{
				if (value == null)
				{
					value = 0;
				}
				value2 = new TypedValue(90, value);
				readOnly = true;
				extendedType = 7;
			}
			else
			{
				if (f.Type == "esriFieldTypeRaster")
				{
					return null;
				}
				if (f.Type == "esriFieldTypeSingle")
				{
					if (value == null)
					{
						value = 0f;
					}
					value2 = new TypedValue(40, double.Parse(value.ToString()));
					extendedType = 3;
				}
				else if (f.Type == "esriFieldTypeSmallInteger")
				{
					if (value == null)
					{
						value = 0;
					}
					value2 = new TypedValue(70, (short)value);
					extendedType = 1;
				}
				else if (f.Type == "esriFieldTypeString")
				{
					if (value == null)
					{
						value = "";
					}
					value2 = new TypedValue(1, value.ToString());
					extendedType = 4;
				}
				else if (f.Type == "esriFieldTypeXML")
				{
					if (value == null)
					{
						value = "";
					}
					value2 = new TypedValue(1, value.ToString());
					extendedType = 12;
				}
			}
			if (value2.Value != null)
			{
				CadField cadField = new CadField();
				cadField.Name = f.Name;
				cadField.Value = value2;
				cadField.ReadOnly = readOnly;
				cadField.TypeField = f.IsTypeField;
				cadField.ExtendedType = extendedType;
				if (value2.TypeCode == 1)
				{
					cadField.Length = 255;
					short num = (short)f.Length;
					if (num > 0)
					{
						cadField.Length = num;
					}
					else
					{
						num = (short)value2.Value.ToString().Length;
						if (num > 0)
						{
							cadField.Length = num;
						}
					}
				}
				return cadField;
			}
			return null;
		}

		public static List<CadField> ToCadFields(DBDictionary attrDict, Transaction t)
		{
			List<CadField> list = new List<CadField>();
			List<CadField> result;
			try
			{
				using (DbDictionaryEnumerator enumerator = attrDict.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DBDictionaryEntry current = enumerator.Current;
						string key = current.Key;
						Xrecord xrecord = (Xrecord)t.GetObject(current.Value, 0);
						if (xrecord != null)
						{
							CadField cadField = CadField.ToCadField(key, xrecord, t);
							cadField.Id = current.Value;
							if (cadField != null)
							{
								list.Add(cadField);
							}
						}
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

		public static List<CadField> ToCadFields(Fields fields)
		{
			List<CadField> list = new List<CadField>();
			List<CadField> result;
			try
			{
                ArcGIS10Types.Field[] fieldArray = fields.FieldArray;
				for (int i = 0; i < fieldArray.Length; i++)
				{
                    ArcGIS10Types.Field field = fieldArray[i];
					CadField cadField = CadField.ToCadField(field, field.DefaultValue);
					if (cadField != null)
					{
						list.Add(cadField);
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

		public static List<CadField> ToCadFields(PropertyInfo[] fields, TemplateInfo template, PropertySet domains)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Dictionary<string, Domain> dictionary2 = new Dictionary<string, Domain>();
			try
			{
				if (domains != null)
				{
					PropertySetProperty[] propertyArray = domains.PropertyArray;
					for (int i = 0; i < propertyArray.Length; i++)
					{
						PropertySetProperty propertySetProperty = propertyArray[i];
						try
						{
							DomainInfo domainInfo = (DomainInfo)propertySetProperty.Value;
							if (domainInfo.Inherited)
							{
								for (int j = 0; j < fields.Length; j++)
								{
									PropertyInfo propertyInfo = fields[j];
									if (string.Equals(propertyInfo.Name, propertySetProperty.Key))
									{
										domainInfo.Domain = propertyInfo.Domain;
									}
								}
							}
							dictionary2.Add(propertySetProperty.Key, domainInfo.Domain);
						}
						catch
						{
						}
					}
				}
				if (template != null)
				{
					PropertySetProperty[] propertyArray2 = template.Prototype.Properties.PropertyArray;
					for (int k = 0; k < propertyArray2.Length; k++)
					{
						PropertySetProperty propertySetProperty2 = propertyArray2[k];
						dictionary.Add(propertySetProperty2.Key, propertySetProperty2.Value);
					}
				}
			}
			catch
			{
			}
			List<CadField> list = new List<CadField>();
			List<CadField> result;
			try
			{
				for (int l = 0; l < fields.Length; l++)
				{
					PropertyInfo propertyInfo2 = fields[l];
					object defaultValue = null;
					if (dictionary.ContainsKey(propertyInfo2.Name))
					{
						defaultValue = dictionary[propertyInfo2.Name];
					}
					if (dictionary2.ContainsKey(propertyInfo2.Name))
					{
						propertyInfo2.Domain = dictionary2[propertyInfo2.Name];
					}
					CadField cadField = CadField.ToCadField(propertyInfo2, defaultValue);
					if (cadField != null)
					{
						if (propertyInfo2.IsNullable && cadField.Domain != null && cadField.Domain.HasCodedValues())
						{
							cadField.Domain.GenerateFauxNull();
						}
						list.Add(cadField);
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

		public static List<CadField> ToCadFields(Fields fields, string typeIDField, int typeIdValue)
		{
			List<CadField> list = new List<CadField>();
			List<CadField> result;
			try
			{
                ArcGIS10Types.Field[] fieldArray = fields.FieldArray;
				for (int i = 0; i < fieldArray.Length; i++)
				{
                    ArcGIS10Types.Field field = fieldArray[i];
					CadField cadField = CadField.ToCadField(field, field.DefaultValue);
					if (cadField != null)
					{
						if (typeIDField != null && cadField.Name.ToLower() == typeIDField.ToLower())
						{
							cadField.ReadOnly = true;
							cadField.Value = new TypedValue((int)cadField.Value.TypeCode, typeIdValue);
						}
						list.Add(cadField);
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

		public static List<CadField> ToCadFields(PropertySetProperty[] properties, List<CadField> fcFields)
		{
			List<CadField> list = new List<CadField>();
			List<CadField> result;
			try
			{
				for (int i = 0; i < properties.Length; i++)
				{
					PropertySetProperty p = properties[i];
					CadField f = CadField.ToCadField(p);
					if (f != null)
					{
						CadField cadField = fcFields.Find((CadField cf) => f.Name == cf.Name);
						if (cadField != null)
						{
							f.TypeField = cadField.TypeField;
							f.ReadOnly = cadField.ReadOnly;
							f.Visible = cadField.Visible;
							f.Domain = cadField.Domain;
							f.ExtendedType = cadField.ExtendedType;
						}
						if (cadField.Value.Value != f.Value.Value)
						{
							list.Add(f);
						}
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

		public static List<CadField> ToCadFields(List<CadField> defaultFields, Record r, string typeFieldName, bool skipDefaults)
		{
			List<CadField> list = new List<CadField>();
			List<CadField> result;
			try
			{
				int num = -1;
				foreach (CadField current in defaultFields)
				{
					num++;
					if (r.Values[num] != null && current.Name != typeFieldName)
					{
						CadField cadField = CadField.ToCadField(current, r.Values[num]);
						if (cadField != null)
						{
							if (!skipDefaults)
							{
								list.Add(cadField);
							}
							else if (cadField.Value.Value != current.Value.Value)
							{
								list.Add(cadField);
							}
						}
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

		public static List<CadField> ToCadFields(System.Data.DataTable table, MSCFeatureClass fc)
		{
			string columnName = "Key";
			string columnName2 = "Value";
			List<CadField> list = new List<CadField>();
			foreach (CadField current in fc.Fields)
			{
				int num = -1;
				for (int i = 0; i < table.Rows.Count; i++)
				{
					if (current.Name == table.Rows[i][columnName].ToString())
					{
						num = i;
						break;
					}
				}
				if (num != -1)
				{
					try
					{
						CadField cadField = new CadField(current);
						cadField.Value = CadField.CreateTypedValue(current.FieldType, table.Rows[num][columnName2].ToString());
						if (cadField.Value != current.Value)
						{
							list.Add(cadField);
						}
					}
					catch
					{
					}
				}
			}
			return list;
		}

		public static List<CadField> ToCadFields(DataRow row, MSCFeatureClass fc)
		{
			string columnName = "Key";
			string columnName2 = "Value";
			List<CadField> list = new List<CadField>();
			CadField cadField = null;
			foreach (CadField current in fc.Fields)
			{
				if (current.Name == row[columnName].ToString())
				{
					cadField = current;
					break;
				}
			}
			if (cadField == null)
			{
				return list;
			}
			try
			{
				list.Add(new CadField(cadField)
				{
					Value = CadField.CreateTypedValue(cadField.FieldType, row[columnName2].ToString())
				});
			}
			catch
			{
			}
			return list;
		}

		public static List<CadField> ToCadFields(IDictionary<string, AGSField> fields, Record r, string typeFieldName)
		{
			List<CadField> list = new List<CadField>();
			List<CadField> result;
			try
			{
				int num = -1;
				foreach (AGSField current in fields.Values)
				{
					num++;
					if (r.Values[num] != null && current.Name != typeFieldName)
					{
						CadField cadField = CadField.ToCadField(current, r.Values[num]);
						if (cadField != null)
						{
							list.Add(cadField);
						}
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

		public static List<CadField> StripDefaults(List<CadField> defaults, List<CadField> fields)
		{
			List<CadField> list = new List<CadField>();
			foreach (CadField f in fields)
			{
				if (defaults.Find((CadField cf) => f.Name == cf.Name && f.Value == cf.Value) == null)
				{
					list.Add(f);
				}
			}
			return list;
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

		public static List<CadField> EntityCadFields(ObjectId id)
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
						list = CadField.EntityCadFields(id, transaction);
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

		public static List<CadField> EntityCadFields(ObjectId id, Transaction t)
		{
			List<CadField> result;
			try
			{
				DBObject @object = t.GetObject(id, 0);
				ObjectId extensionDictionary = @object.ExtensionDictionary;
				DBDictionary dBDictionary = (DBDictionary)t.GetObject(extensionDictionary, 0);
				if (dBDictionary.Contains("ESRI_ATTRIBUTES"))
				{
					DBDictionary attrDict = (DBDictionary)t.GetObject(dBDictionary.GetAt("ESRI_ATTRIBUTES"), 0);
					List<CadField> list = CadField.ToCadFields(attrDict, t);
					result = list;
				}
				else
				{
					result = new List<CadField>();
				}
			}
			catch
			{
				result = new List<CadField>();
			}
			return result;
		}

		public static void RemoveCadAttribute(Database db, Transaction t, ObjectId id, CadField cf)
		{
			try
			{
				DBDictionary dBDictionary = CadField.OpenAttributeDictionary(id, t, (OpenMode)1);
				if (dBDictionary.Contains(cf.Name))
				{
					dBDictionary.Remove(cf.Name);
				}
			}
			catch
			{
			}
		}

		public static void AddCadAttributeToEntity(Database db, Transaction t, ObjectId id, CadField cf)
		{
			try
			{
				DBDictionary dict = CadField.OpenAttributeDictionary(id, t, (OpenMode)1);
				if (cf.FieldType == CadField.CadFieldType.String && cf.Length == 0)
				{
					cf.Length = 254;
				}
				cf.WriteValue(dict, t);
			}
			catch
			{
			}
		}

		public static bool AddCadAttributesToEntity(ObjectId id, List<CadField> fields)
		{
			bool result;
			try
			{
				bool flag = false;
				Document document = AfaDocData.ActiveDocData.Document;
				Database database = document.Database;
				using (document.LockDocument((DocumentLockMode)4, null, null, false))
				{
					using (Transaction transaction = database.TransactionManager.StartTransaction())
					{
						flag = CadField.AddCadAttributesToEntity(database, transaction, id, fields);
						transaction.Commit();
					}
				}
				result = flag;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool AddCadAttributesToEntityDictionary(Transaction t, DBDictionary attrDictionary, List<CadField> fields)
		{
			bool result;
			try
			{
				foreach (CadField current in fields)
				{
					current.WriteValue(attrDictionary, t);
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool AddCadAttributesToEntity(Database db, Transaction t, ObjectId id, List<CadField> fields)
		{
			bool result;
			try
			{
				DBDictionary attrDictionary = CadField.OpenAttributeDictionary(id, t, (OpenMode)1);
				result = CadField.AddCadAttributesToEntityDictionary(t, attrDictionary, fields);
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static void RemoveCadAttribute(ObjectId id, CadField cf)
		{
			Document document = AfaDocData.ActiveDocData.Document;
			Database database = document.Database;
			try
			{
				using (document.LockDocument())
				{
					var transactionManager = database.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						CadField.RemoveCadAttribute(database, transaction, id, cf);
						transaction.Commit();
					}
				}
			}
			catch
			{
				ErrorReport.ShowErrorMessage("DEBUG:  Error removing CAD Attribute");
			}
		}

		public static void AddCadAttributeToEntity(ObjectId id, CadField cf)
		{
			Document document = AfaDocData.ActiveDocData.Document;
			Database database = document.Database;
			try
			{
				using (document.LockDocument())
				{
					var transactionManager = database.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						CadField.AddCadAttributeToEntity(database, transaction, id, cf);
						transaction.Commit();
					}
				}
			}
			catch
			{
				ErrorReport.ShowErrorMessage("DEBUG:  Error attaching CAD Attribute");
			}
		}

		public static List<CadField> SetTypeField(List<CadField> srcList, string typeFieldName, object typeValue, FieldDomain fieldDomain)
		{
			List<CadField> result;
			try
			{
				foreach (CadField current in srcList)
				{
					if (typeValue == null && fieldDomain != null)
					{
						typeValue = fieldDomain.FauxNull.Value;
					}
					if (current.Name == typeFieldName)
					{
						current.TypeField = true;
						if (fieldDomain.FauxNull == null)
						{
							fieldDomain.GenerateFauxNull();
						}
						current.Domain = fieldDomain;
						if (typeValue == null)
						{
							typeValue = CadField.CreateDefaultValue(current.FieldType);
						}
						current.Value = CadField.CreateTypedValue(current.FieldType, typeValue);
						result = srcList;
						return result;
					}
				}
				result = srcList;
			}
			catch
			{
				result = srcList;
			}
			return result;
		}

		public static void SetTypeFieldValue(List<CadField> srcList, string fieldName, object typeValue)
		{
			try
			{
				foreach (CadField current in srcList)
				{
					if (current.Name == fieldName)
					{
						current.Value = CadField.CreateTypedValue(typeValue);
						current.TypeField = true;
						break;
					}
				}
			}
			catch
			{
			}
		}

		public static List<CadField> SetTypeFieldValue(List<CadField> srcList, object typeValue)
		{
			List<CadField> result;
			try
			{
				foreach (CadField current in srcList)
				{
					if (current.TypeField)
					{
						current.Value = CadField.CreateTypedValue(typeValue);
						result = srcList;
						return result;
					}
				}
				result = srcList;
			}
			catch
			{
				result = srcList;
			}
			return result;
		}

		public void FixDomainValue()
		{
			if (this.Domain == null)
			{
				return;
			}
			object value = this.Value.Value;
			if (this.Domain.DomainType == "CodedValueDomain")
			{
				foreach (MSCCodedValue current in this.Domain.CodedValues.Values)
				{
					if (value.Equals(current.Value))
					{
						return;
					}
				}
				this.Value = CadField.CreateTypedValue(this.Domain.CodedValues.First<KeyValuePair<string, MSCCodedValue>>().Value);
			}
			this.Domain.DomainType = "RangeDomain";
		}
	}
}
