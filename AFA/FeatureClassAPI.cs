using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AFA
{
	public class FeatureClassAPI
	{
		[LispFunction("esri_featureclass_get")]
		public object ESRI_FeatureClass_Get(ResultBuffer rb)
		{
			object result;
			try
			{
				string argument = LspUtil.GetArgument(rb, 0, null);
				MSCFeatureClass mSCFeatureClass;
				if (!string.IsNullOrEmpty(argument))
				{
					mSCFeatureClass = this.GetFeatureClassOrService(argument);
				}
				else
				{
					mSCFeatureClass = this.GetCurrentFeatureClassOrService();
				}
				if (mSCFeatureClass == null)
				{
					result = null;
				}
				else
				{
					string argument2 = LspUtil.GetArgument(rb, 1, null);
					if (argument2 != null)
					{
						mSCFeatureClass = mSCFeatureClass.FindSubtypeName(argument2);
					}
					if (mSCFeatureClass == null)
					{
						result = null;
					}
					else
					{
						List<TypedValue> list = new List<TypedValue>();
						list.Add(new TypedValue(5016, null));
						LspUtil.AppendDottedPair(ref list, "NAME", mSCFeatureClass.Name);
						LspUtil.AppendDottedPair(ref list, "GEOMTYPE", MSCFeatureClass.GetGeomString(mSCFeatureClass.GeometryType));
						if (!string.IsNullOrEmpty(mSCFeatureClass.TypeField))
						{
							LspUtil.AppendDottedPair(ref list, "TYPEFIELD", mSCFeatureClass.TypeField);
						}
						if (mSCFeatureClass.IsSingleLayerQuery())
						{
							LspUtil.AppendDottedPair(ref list, "CADLAYER", mSCFeatureClass.GetFirstLayerFromQuery());
						}
						list.Add(new TypedValue(5017, null));
						ResultBuffer resultBuffer = new ResultBuffer(list.ToArray());
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

		[LispFunction("esri_subtype_names")]
		public object ESRI_Subytpe_Names(ResultBuffer rb)
		{
			object result;
			try
			{
				string argument = LspUtil.GetArgument(rb, 0, null);
				MSCFeatureClass mSCFeatureClass;
				if (!string.IsNullOrEmpty(argument))
				{
					mSCFeatureClass = this.GetFeatureClassOrService(argument);
				}
				else
				{
					mSCFeatureClass = this.GetCurrentFeatureClassOrService();
				}
				if (mSCFeatureClass == null)
				{
					result = null;
				}
				else if (mSCFeatureClass.SubTypes.Count == 0)
				{
					result = null;
				}
				else
				{
					ResultBuffer resultBuffer = new ResultBuffer();
					foreach (MSCFeatureClassSubType current in mSCFeatureClass.SubTypes)
					{
						resultBuffer.Add(new TypedValue(5005, current.Name));
					}
					result = resultBuffer;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_featureclass_names")]
		public object ESRI_GetFeatureClassList(ResultBuffer rb)
		{
			object result;
			try
			{
				MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
				if (docDataset.FeatureClasses.Count > 0)
				{
					ResultBuffer resultBuffer = new ResultBuffer();
					foreach (string current in docDataset.FeatureClasses.Keys)
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

		[LispFunction("esri_featureclass_add")]
		public object ESRI_AddFeatureClass(ResultBuffer rb)
		{
			object result;
			try
			{
				TypedValue[] array = rb.AsArray();
				if (array.Count<TypedValue>() == 0)
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
						string text = typedValue.Value.ToString();
						MSCFeatureClass localFeatureClass = this.GetLocalFeatureClass(text);
						if (localFeatureClass != null)
						{
							result = null;
						}
						else
						{
							string assocParam = LspUtil.GetAssocParam(rb, "GeomType", "Point");
							string assocParam2 = LspUtil.GetAssocParam(rb, "LayerFilter", "*");
							if (string.IsNullOrEmpty(text))
							{
								result = null;
							}
							else if (string.IsNullOrEmpty(assocParam))
							{
								result = null;
							}
							else if (string.IsNullOrEmpty(assocParam2))
							{
								result = null;
							}
							else
							{
								MSCFeatureClass mSCFeatureClass = AfaDocData.ActiveDocData.DocDataset.CreateFeatureClass(text);
								mSCFeatureClass.GeometryType = MSCFeatureClass.GetGeomType(assocParam);
								TypedValue typedValue2 = new TypedValue(8, assocParam2);
								mSCFeatureClass.Query = new ResultBuffer(new TypedValue[]
								{
									typedValue2
								});
								mSCFeatureClass.Write(AfaDocData.ActiveDocData.Document);
								AfaDocData.ActiveDocData.DocDataset.FeatureClasses.Add(text, mSCFeatureClass);
								AfaDocData.ActiveDocData.DocDataset.FeatureClassViewList.Add(new FCView(mSCFeatureClass));
								AfaDocData.ActiveDocData.SetActiveFeatureClass(mSCFeatureClass);
								ArcGISRibbon.SetActiveFeatureClass(mSCFeatureClass);
								result = mSCFeatureClass.Name;
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

		[LispFunction("esri_featureclass_remove")]
		public object ESRI_RemoveFeatureClass(ResultBuffer rb)
		{
			object result;
			try
			{
				string argument = LspUtil.GetArgument(rb, 0, null);
				MSCFeatureClass mSCFeatureClass;
				if (!string.IsNullOrEmpty(argument))
				{
					mSCFeatureClass = this.GetFeatureClassOrService(argument);
				}
				else
				{
					mSCFeatureClass = this.GetCurrentFeatureClassOrService();
				}
				if (mSCFeatureClass == null)
				{
					result = null;
				}
				else
				{
					MSCFeatureClass activeFeatureClassOrSubtype = AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype();
					bool flag = mSCFeatureClass == activeFeatureClassOrSubtype;
					AfaDocData.ActiveDocData.DocDataset.RemoveFeatureClass(mSCFeatureClass);
					if (flag)
					{
						MSCDataset.SetDefaultActiveFeatureClass();
					}
					result = LspUtil.LispTrue;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_featureclass_getquery")]
		public object ESRI_GetFeatureClassQuery(ResultBuffer rb)
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
						MSCFeatureClass localFeatureClass = this.GetLocalFeatureClass(name);
						if (localFeatureClass == null)
						{
							result = null;
						}
						else
						{
							TypedValue[] array2 = localFeatureClass.Query.AsArray();
							if (array2.Length == 0)
							{
								localFeatureClass.SetDefaultQuery();
							}
							ResultBuffer resultBuffer = new ResultBuffer(localFeatureClass.Query.AsArray());
							result = resultBuffer;
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

		[LispFunction("esri_featureclass_setquery")]
		public object ESRI_SetFeatureClassQuery(ResultBuffer rb)
		{
			object result;
			try
			{
				TypedValue[] array = rb.AsArray();
				if (array.Count<TypedValue>() < 4)
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
						MSCFeatureClass localFeatureClass = this.GetLocalFeatureClass(name);
						if (localFeatureClass == null)
						{
							result = null;
						}
						else if (array[1].TypeCode != 5016)
						{
							result = null;
						}
						else
						{
							int num = array.Length;
							ResultBuffer resultBuffer = new ResultBuffer();
							for (int i = 2; i < num - 1; i++)
							{
								resultBuffer.Add(array[i]);
							}
							localFeatureClass.Query = resultBuffer;
							localFeatureClass.Write(localFeatureClass.ParentDataset.ParentDocument);
							result = true;
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

		[LispFunction("esri_featureclass_getcurrent")]
		public object ESRI_GetCurrentFeatureClass(ResultBuffer rb)
		{
			object result;
			try
			{
				MSCFeatureClass mSCFeatureClass = AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype();
				if (mSCFeatureClass.IsSubType)
				{
					mSCFeatureClass = mSCFeatureClass.ParentFC;
				}
				if (mSCFeatureClass.GetType() == typeof(MSCFeatureService))
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

		[LispFunction("esri_subtype_getcurrent")]
		public object ESRI_GetCurrentSubtypeName(ResultBuffer rb)
		{
			object result;
			try
			{
				MSCFeatureClass activeFeatureClassOrSubtype = AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype();
				if (!activeFeatureClassOrSubtype.IsSubType)
				{
					result = null;
				}
				else if (activeFeatureClassOrSubtype.GetType() == typeof(MSCFeatureService))
				{
					result = null;
				}
				else
				{
					result = activeFeatureClassOrSubtype.Name;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_subtype_getcadlayer")]
		public object ESRI_GetSubtypeLayer(ResultBuffer rb)
		{
			object result;
			try
			{
				string argument = LspUtil.GetArgument(rb, 0, null);
				string argument2 = LspUtil.GetArgument(rb, 1, null);
				if (string.IsNullOrEmpty(argument) || string.IsNullOrEmpty(argument2))
				{
					result = null;
				}
				else
				{
					MSCFeatureClass featureClassOrService = this.GetFeatureClassOrService(argument);
					MSCFeatureClassSubType mSCFeatureClassSubType = featureClassOrService.FindSubtypeName(argument2);
					result = mSCFeatureClassSubType.CadLayerName;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_featureclass_setcurrent")]
		public object ESRI_SetCurrentFeatureClass(ResultBuffer rb)
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

		[LispFunction("esri_fielddef_names")]
		public object ESRI_GetFeatureClassFieldNames(ResultBuffer rb)
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
						MSCFeatureClass featureClassOrService = this.GetFeatureClassOrService(name);
						if (featureClassOrService == null)
						{
							result = null;
						}
						else if (featureClassOrService.Fields.Count == 0)
						{
							result = null;
						}
						else
						{
							ResultBuffer resultBuffer = new ResultBuffer();
							foreach (CadField current in featureClassOrService.Fields)
							{
								if (current.Visible)
								{
									resultBuffer.Add(new TypedValue(5005, current.Name));
								}
							}
							result = resultBuffer;
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

		[LispFunction("esri_fielddef_get")]
		public object ESRI_GetFeatureClassFieldDefinition(ResultBuffer rb)
		{
			object result;
			try
			{
				TypedValue[] array = rb.AsArray();
				if (array.Count<TypedValue>() < 2)
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
						string subtypeName = null;
						if (array.Count<TypedValue>() == 3)
						{
							TypedValue typedValue2 = array[2];
							if (typedValue2.TypeCode != 5005)
							{
								result = null;
								return result;
							}
							subtypeName = typedValue2.Value.ToString();
						}
						MSCFeatureClass featureClassOrService = this.GetFeatureClassOrService(name, subtypeName);
						if (featureClassOrService == null)
						{
							result = null;
						}
						else
						{
							TypedValue typedValue3 = array[1];
							if (typedValue3.TypeCode != 5005)
							{
								result = null;
							}
							else
							{
								string fieldName = typedValue3.Value.ToString();
								if (featureClassOrService.Fields.Count == 0)
								{
									result = null;
								}
								else
								{
									CadField cadField = FeatureClassAPI.FindField(featureClassOrService.Fields, fieldName);
									if (cadField == null)
									{
										result = null;
									}
									else
									{
										List<TypedValue> list = new List<TypedValue>();
										list.Add(new TypedValue(5016, null));
										LspUtil.AppendDottedPair(ref list, 5005, "NAME", 5005, cadField.Name);
										LspUtil.AppendDottedPair(ref list, 5005, "TYPE", 5005, CadField.CodeString((int)cadField.FieldType));
										LspUtil.AppendDottedPair(ref list, 5005, "RO", 5003, cadField.ReadOnly);
										if (cadField.FieldType == CadField.CadFieldType.String)
										{
											LspUtil.AppendDottedPair(ref list, 5005, "LENGTH", 5003, cadField.Length);
										}
										LspUtil.AppendDottedPair(ref list, "VALUE", cadField.Value);
										list.Add(new TypedValue(5017, null));
										ResultBuffer resultBuffer = new ResultBuffer(list.ToArray());
										result = resultBuffer;
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

		[LispFunction("esri_fielddef_remove")]
		public object ESRI_RemoveFeatureClassFieldDefinition(ResultBuffer rb)
		{
			object result;
			try
			{
				TypedValue[] array = rb.AsArray();
				if (array.Count<TypedValue>() < 2)
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
						MSCFeatureClass localFeatureClass = this.GetLocalFeatureClass(name);
						if (localFeatureClass == null)
						{
							result = null;
						}
						else
						{
							TypedValue typedValue2 = array[1];
							if (typedValue2.TypeCode != 5005)
							{
								result = null;
							}
							else
							{
								string fieldName = typedValue2.Value.ToString();
								if (localFeatureClass.Fields.Count == 0)
								{
									result = null;
								}
								else
								{
									CadField cadField = FeatureClassAPI.FindField(localFeatureClass.Fields, fieldName);
									if (cadField == null)
									{
										result = null;
									}
									else if (cadField.ReadOnly)
									{
										result = null;
									}
									else if (localFeatureClass.Fields.Remove(cadField))
									{
										localFeatureClass.Write(AfaDocData.ActiveDocData.Document);
										result = LspUtil.LispTrue;
									}
									else
									{
										result = null;
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

		[LispFunction("esri_fielddef_add")]
		public object ESRI_AddFeatureClassFieldDefinition(ResultBuffer rb)
		{
			object result;
			try
			{
				TypedValue[] array = rb.AsArray();
				if (array.Count<TypedValue>() < 2)
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
						MSCFeatureClass localFeatureClass = this.GetLocalFeatureClass(name);
						if (localFeatureClass == null)
						{
							result = null;
						}
						else
						{
							string text = null;
							string strVal = null;
							object obj = null;
							object obj2 = null;
							object obj3 = null;
							object obj4 = null;
							TypedValue[] array2 = array;
							for (int i = 0; i < array2.Length; i++)
							{
								TypedValue typedValue2 = array2[i];
								if (typedValue2.TypeCode == 5016)
								{
									obj3 = null;
									obj4 = null;
								}
								else if (typedValue2.TypeCode == 5017)
								{
									obj3 = null;
									obj4 = null;
								}
								else if (typedValue2.TypeCode == 5018)
								{
									if (obj3 != null && obj4 != null)
									{
										TypedValue typedValue3 = (TypedValue)obj3;
										TypedValue typedValue4 = (TypedValue)obj4;
										string a = typedValue3.Value.ToString();
										object value = typedValue4.Value;
										if (string.Equals(a, "NAME", StringComparison.CurrentCultureIgnoreCase))
										{
											text = value.ToString();
										}
										else if (string.Equals(a, "VALUE", StringComparison.CurrentCultureIgnoreCase))
										{
											obj = value.ToString();
										}
										else if (string.Equals(a, "TYPE", StringComparison.CurrentCultureIgnoreCase))
										{
											strVal = value.ToString();
										}
										else if (string.Equals(a, "LENGTH", StringComparison.CurrentCultureIgnoreCase))
										{
											string s = value.ToString();
											obj2 = short.Parse(s);
										}
									}
								}
								else if (obj3 == null)
								{
									obj3 = typedValue2;
								}
								else
								{
									obj4 = typedValue2;
								}
							}
							if (string.IsNullOrEmpty(text))
							{
								result = null;
							}
							else
							{
								CadField cadField = FeatureClassAPI.FindField(localFeatureClass.Fields, text);
								if (cadField != null)
								{
									result = null;
								}
								else
								{
									CadField cadField2 = new CadField();
									cadField2.Name = text;
									CadField.CadFieldType cadFieldType = CadField.FieldTypeCode(strVal);
									if (obj == null)
									{
										if (cadFieldType == CadField.CadFieldType.Double)
										{
											obj = 0.0;
										}
										if (cadFieldType == CadField.CadFieldType.Integer)
										{
											obj = 0;
										}
										if (cadFieldType == CadField.CadFieldType.Short)
										{
											obj = 0;
										}
										if (cadFieldType == CadField.CadFieldType.String)
										{
											obj = "";
										}
									}
									if (obj != null)
									{
										cadField2.Value = new TypedValue((int)cadFieldType, obj);
									}
									if (obj2 == null && cadFieldType == CadField.CadFieldType.String)
									{
										obj2 = 255;
									}
									if (obj2 != null)
									{
										short num = short.Parse(obj2.ToString());
										if (num > 0 && num < 256)
										{
											obj2 = num;
										}
										cadField2.Length = (short)obj2;
									}
									cadField2.ReadOnly = false;
									cadField2.TypeField = false;
									cadField2.Visible = true;
									localFeatureClass.Fields.Add(cadField2);
									localFeatureClass.Write(AfaDocData.ActiveDocData.Document);
									result = cadField2.Name;
								}
							}
						}
					}
				}
			}
			catch (SystemException)
			{
				result = null;
			}
			return result;
		}

		private MSCFeatureClass GetCurrentFeatureClassOrService()
		{
			return AfaDocData.ActiveDocData.GetTopActiveFeatureClass();
		}

		private MSCFeatureClass GetCurrentLocalFeatureClass()
		{
			MSCFeatureClass topActiveFeatureClass = AfaDocData.ActiveDocData.GetTopActiveFeatureClass();
			if (topActiveFeatureClass is MSCFeatureService)
			{
				return null;
			}
			return topActiveFeatureClass;
		}

		private MSCFeatureClass GetFeatureClassOrService(string name, string subtypeName)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			MSCFeatureClass featureClassOrService = this.GetFeatureClassOrService(name);
			if (featureClassOrService == null)
			{
				return featureClassOrService;
			}
			if (string.IsNullOrEmpty(subtypeName))
			{
				return featureClassOrService;
			}
			foreach (MSCFeatureClass current in featureClassOrService.SubTypes)
			{
				if (string.Equals(current.Name, subtypeName, StringComparison.CurrentCultureIgnoreCase))
				{
					return current;
				}
			}
			return null;
		}

		private MSCFeatureClass GetFeatureClassOrService(string name)
		{
			MSCFeatureClass result;
			try
			{
				MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
				if (docDataset.FeatureClasses.Count + docDataset.FeatureServices.Count == 0)
				{
					result = null;
				}
				else
				{
					foreach (string current in docDataset.FeatureClasses.Keys)
					{
						if (string.Equals(current, name, StringComparison.CurrentCultureIgnoreCase))
						{
							result = docDataset.FeatureClasses[current];
							return result;
						}
					}
					foreach (string current2 in docDataset.FeatureServices.Keys)
					{
						if (string.Equals(current2, name, StringComparison.CurrentCultureIgnoreCase))
						{
							result = docDataset.FeatureServices[current2];
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

		private MSCFeatureClass GetLocalFeatureClass(string name)
		{
			MSCFeatureClass result;
			try
			{
				MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
				if (docDataset.FeatureClasses.Count == 0)
				{
					result = null;
				}
				else
				{
					foreach (string current in docDataset.FeatureClasses.Keys)
					{
						if (string.Equals(current, name, StringComparison.CurrentCultureIgnoreCase))
						{
							result = docDataset.FeatureClasses[current];
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
