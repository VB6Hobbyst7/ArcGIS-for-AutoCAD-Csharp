using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AFA
{
	public class EntityAPI
	{
		[LispFunction("esri_attributes_get")]
		public object ESRI_Attributes_Get(ResultBuffer rb)
		{
			object result;
			try
			{
				ObjectId argument = LspUtil.GetArgument(rb, 0, ObjectId.Null);
				if (argument == ObjectId.Null)
				{
					result = null;
				}
				else
				{
					string assocParam = LspUtil.GetAssocParam(rb, "FCNAME", null);
					string assocParam2 = LspUtil.GetAssocParam(rb, "STNAME", null);
					string fieldName = LspUtil.GetAssocParam(rb, "FIELDNAME", null);
					MSCFeatureClass featureClass = this.GetFeatureClass(assocParam, assocParam2);
					List<CadField> list;
					if (featureClass == null)
					{
						list = CadField.EntityCadFields(argument);
					}
					else
					{
						list = featureClass.GetEntityFields(argument);
					}
					if (!string.IsNullOrEmpty(fieldName))
					{
						list = (from x in list
						where x.Name == fieldName
						select x).ToList<CadField>();
					}
					if (list.Count == 0)
					{
						result = null;
					}
					else
					{
						List<TypedValue> list2 = new List<TypedValue>();
						list2.Add(new TypedValue(5016, null));
						foreach (CadField current in list)
						{
							LspUtil.AppendDottedPair(ref list2, current.Name, current.Value.Value);
						}
						list2.Add(new TypedValue(5017, null));
						ResultBuffer resultBuffer = new ResultBuffer(list2.ToArray());
						result = resultBuffer;
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_attributes_delete")]
		public object ESRI_DeleteFeatureClassAttributes(ResultBuffer rb)
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
					if (typedValue.TypeCode != 5006)
					{
						result = null;
					}
					else
					{
						ObjectId id = (ObjectId)typedValue.Value;
						List<string> list = new List<string>();
						if (array.Count<TypedValue>() > 1)
						{
							TypedValue[] array2 = array;
							for (int i = 0; i < array2.Length; i++)
							{
								TypedValue typedValue2 = array2[i];
								if (typedValue2.TypeCode == 5005)
								{
									list.Add(typedValue2.Value.ToString());
								}
							}
						}
						List<CadField> list2 = CadField.EntityCadFields(id);
						if (list2.Count == 0)
						{
							result = LspUtil.LispTrue;
						}
						else
						{
							List<CadField> list3 = null;
							if (list.Count == 0)
							{
								list3 = list2;
							}
							else
							{
								list3 = new List<CadField>();
								foreach (CadField current in list2)
								{
									foreach (string current2 in list)
									{
										if (string.Equals(current2, current.Name, StringComparison.CurrentCultureIgnoreCase) && !list3.Contains(current))
										{
											list3.Add(current);
										}
									}
								}
							}
							if (list3.Count == 0)
							{
								result = LspUtil.LispTrue;
							}
							else
							{
								Document document = AfaDocData.ActiveDocData.Document;
								Database database = document.Database;
								using (document.LockDocument((DocumentLockMode)4, null, null, false))
								{
									using (Transaction transaction = database.TransactionManager.StartTransaction())
									{
										foreach (CadField current3 in list3)
										{
											if (!current3.ReadOnly && current3.Visible)
											{
												CadField.RemoveCadAttribute(database, transaction, id, current3);
											}
										}
										transaction.Commit();
									}
								}
								result = LspUtil.LispTrue;
							}
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

		[LispFunction("esri_attributes_set")]
		public object ESRI_SetFeatureClassAttributes(ResultBuffer rb)
		{
			object result;
			try
			{
				TypedValue[] array = rb.AsArray();
				if (array.Count<TypedValue>() < 3)
				{
					result = null;
				}
				else
				{
					TypedValue typedValue = array[0];
					if (typedValue.TypeCode != 5006)
					{
						result = null;
					}
					else
					{
						ObjectId id = (ObjectId)typedValue.Value;
						TypedValue typedValue2 = array[1];
						if (typedValue2.TypeCode != 5005)
						{
							result = null;
						}
						else
						{
							string name = typedValue2.Value.ToString();
							MSCFeatureClass featureClass = this.GetFeatureClass(name, null);
							if (featureClass == null)
							{
								result = null;
							}
							else
							{
								Dictionary<string, object> dictionary = LspUtil.BuildAssocDictionary(rb);
								if (dictionary.Count == 0)
								{
									result = null;
								}
								else
								{
									List<CadField> list = new List<CadField>();
									foreach (KeyValuePair<string, object> current in dictionary)
									{
										CadField cadField = this.CreateAttributeValue(featureClass, current.Key, current.Value);
										if (cadField != null)
										{
											list.Add(cadField);
										}
									}
									if (list.Count == 0)
									{
										result = null;
									}
									else
									{
										result = CadField.AddCadAttributesToEntity(id, list);
									}
								}
							}
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

		private MSCFeatureClass GetFeatureClass(string name, string subtypeName)
		{
			foreach (MSCFeatureClass current in AfaDocData.ActiveDocData.DocDataset.FeatureClasses.Values)
			{
				if (current.Name == name)
				{
					MSCFeatureClass result;
					if (string.IsNullOrEmpty(subtypeName))
					{
						result = current;
						return result;
					}
					result = this.GetSubtype(current, subtypeName);
					return result;
				}
			}
			foreach (MSCFeatureService current2 in AfaDocData.ActiveDocData.DocDataset.FeatureServices.Values)
			{
				if (current2.Name == name)
				{
					MSCFeatureClass result;
					if (string.IsNullOrEmpty(subtypeName))
					{
						result = current2;
						return result;
					}
					result = this.GetSubtype(current2, subtypeName);
					return result;
				}
			}
			return null;
		}

		private MSCFeatureClass GetSubtype(MSCFeatureClass fc, string subtypeName)
		{
			foreach (MSCFeatureClass current in fc.SubTypes)
			{
				if (current.Name == subtypeName)
				{
					return current;
				}
			}
			return null;
		}

		private CadField CreateAttributeValue(MSCFeatureClass fc, string fieldName, object value)
		{
			CadField result;
			try
			{
				CadField cadField = FeatureClassAPI.FindField(fc.Fields, fieldName);
				if (cadField == null)
				{
					result = null;
				}
				else if (cadField.ReadOnly || !cadField.Visible)
				{
					result = null;
				}
				else
				{
					TypedValue value2 = CadField.CreateTypedValue(cadField.FieldType, value.ToString());
					result = new CadField(cadField)
					{
						Value = value2
					};
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private CadField CreateAttributeValue(MSCFeatureClass fc, ref object firstValue, ref object secondValue)
		{
			TypedValue typedValue = (TypedValue)firstValue;
			TypedValue typedValue2 = (TypedValue)secondValue;
			string text = typedValue.Value.ToString();
			object value = typedValue2.Value;
			string fieldName = text.ToString();
			CadField cadField = FeatureClassAPI.FindField(fc.Fields, fieldName);
			if (cadField == null)
			{
				firstValue = null;
				secondValue = null;
				return null;
			}
			if (cadField.ReadOnly || !cadField.Visible)
			{
				return null;
			}
			TypedValue value2 = CadField.CreateTypedValue(cadField.FieldType, typedValue2.Value.ToString());
			if (value2.TypeCode != cadField.Value.TypeCode)
			{
				firstValue = null;
				secondValue = null;
				return null;
			}
			CadField cadField2 = new CadField(cadField);
			cadField2.Value = value2;
			if (cadField2.Value.TypeCode == 1)
			{
				string text2 = cadField2.Value.Value.ToString();
				if (text2.Length > (int)cadField.Length)
				{
					firstValue = null;
					secondValue = null;
					return null;
				}
			}
			firstValue = null;
			secondValue = null;
			return cadField2;
		}
	}
}
