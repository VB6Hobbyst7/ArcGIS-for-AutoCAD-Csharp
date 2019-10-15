using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AFA
{
	public static class LspUtil
	{
		public static class BoolParser
		{
			public static bool GetValue(string value)
			{
				return LspUtil.BoolParser.IsTrue(value);
			}

			public static bool IsFalse(string value)
			{
				return !LspUtil.BoolParser.IsTrue(value);
			}

			public static bool CanParse(string value)
			{
				bool result;
				try
				{
					if (value == null)
					{
						result = false;
					}
					else
					{
						value = value.Trim();
						value = value.ToLower();
						if (value == "true")
						{
							result = true;
						}
						else if (value == "t")
						{
							result = true;
						}
						else if (value == "1")
						{
							result = true;
						}
						else if (value == "yes")
						{
							result = true;
						}
						else if (value == "y")
						{
							result = true;
						}
						else if (value == "false")
						{
							result = true;
						}
						else if (value == "f")
						{
							result = true;
						}
						else if (value == "0")
						{
							result = true;
						}
						else if (value == "no")
						{
							result = true;
						}
						else if (value == "n")
						{
							result = true;
						}
						else
						{
							result = false;
						}
					}
				}
				catch
				{
					result = false;
				}
				return result;
			}

			public static bool IsTrue(string value)
			{
				bool result;
				try
				{
					if (value == null)
					{
						result = false;
					}
					else
					{
						value = value.Trim();
						value = value.ToLower();
						if (value == "true")
						{
							result = true;
						}
						else if (value == "t")
						{
							result = true;
						}
						else if (value == "1")
						{
							result = true;
						}
						else if (value == "yes")
						{
							result = true;
						}
						else if (value == "y")
						{
							result = true;
						}
						else
						{
							result = false;
						}
					}
				}
				catch
				{
					result = false;
				}
				return result;
			}
		}

		public static TypedValue LispTrue = new TypedValue(5021);

		public static TypedValue LispNil = new TypedValue(5019);

		public static void AppendDottedPair(ref List<TypedValue> list, string key, int type2, object value2)
		{
			LspUtil.AppendDottedPair(ref list, 5005, key, type2, value2);
		}

		public static void AppendDottedPair(ref List<TypedValue> list, int type1, object value1, int type2, object value2)
		{
			list.Add(new TypedValue(5016));
			list.Add(new TypedValue(type1, value1));
			list.Add(new TypedValue(5018));
			list.Add(new TypedValue(type2, value2));
			list.Add(new TypedValue(5017));
		}

		public static void AppendDottedPair(ref List<TypedValue> list, string key, object value)
		{
			if (value == null)
			{
				return;
			}
			if (value is TypedValue)
			{
				TypedValue typedValue = (TypedValue)value;
				LspUtil.AppendDottedPair(ref list, key, (int)typedValue.TypeCode, typedValue.Value);
				return;
			}
			if (value is string)
			{
				string value2 = value.ToString();
				if (!string.IsNullOrEmpty(value2))
				{
					LspUtil.AppendDottedPair(ref list, key, 5005, value);
					return;
				}
			}
			else
			{
				if (value is short)
				{
					LspUtil.AppendDottedPair(ref list, key, 5003, value);
					return;
				}
				if (value is int)
				{
					LspUtil.AppendDottedPair(ref list, key, 5010, value);
					return;
				}
				if (value is double)
				{
					LspUtil.AppendDottedPair(ref list, key, 5001, value);
					return;
				}
				if (value is bool)
				{
					try
					{
						LspUtil.AppendDottedPair(ref list, key, 5003, Convert.ToInt16((bool)value));
						return;
					}
					catch
					{
						return;
					}
				}
				if (value is bool)
				{
					try
					{
						LspUtil.AppendDottedPair(ref list, key, 5003, Convert.ToInt16((bool)value));
						return;
					}
					catch
					{
						return;
					}
				}
				if (value is Extent)
				{
					Extent extent = (Extent)value;
					Point2d minPoint = extent.MinPoint;
					Point2d maxPoint = extent.MaxPoint;
					LspUtil.AppendDottedPair(ref list, key + "MIN", 5002, minPoint);
					LspUtil.AppendDottedPair(ref list, key + "MAX", 5002, maxPoint);
					return;
				}
				try
				{
					string text = value.ToString();
					if (!string.IsNullOrEmpty(text))
					{
						LspUtil.AppendDottedPair(ref list, key, 5005, text);
					}
				}
				catch
				{
				}
			}
		}

		public static ResultBuffer CreateDottedPair(int type1, object value1, int type2, object value2)
		{
			ResultBuffer resultBuffer = new ResultBuffer();
			resultBuffer.Add(new TypedValue(5016));
			resultBuffer.Add(new TypedValue(type1, value1));
			resultBuffer.Add(new TypedValue(5018));
			resultBuffer.Add(new TypedValue(type2, value2));
			resultBuffer.Add(new TypedValue(5017));
			return resultBuffer;
		}

		public static bool CheckTypedValueCount(ResultBuffer rb, int minimumCount)
		{
			if (minimumCount == 0)
			{
				return true;
			}
			if (rb == null)
			{
				return false;
			}
			TypedValue[] source = rb.AsArray();
			return source.Count<TypedValue>() >= minimumCount;
		}

		public static int GetTypedValueCount(ResultBuffer rb)
		{
			int result;
			try
			{
				TypedValue[] source = rb.AsArray();
				result = source.Count<TypedValue>();
			}
			catch
			{
				result = 0;
			}
			return result;
		}

		public static string GetAssocParam(ResultBuffer rb, string tag, string defaultValue)
		{
			object obj = LspUtil.FetchAssocParam(rb, tag);
			if (obj == null)
			{
				return defaultValue;
			}
			if (obj is string)
			{
				return obj as string;
			}
			return defaultValue;
		}

		public static short GetAssocParam(ResultBuffer rb, string tag, short defaultValue)
		{
			object obj = LspUtil.FetchAssocParam(rb, tag);
			if (obj == null)
			{
				return defaultValue;
			}
			string s = obj.ToString();
			short result;
			if (short.TryParse(s, out result))
			{
				return result;
			}
			return defaultValue;
		}

		public static double GetAssocParam(ResultBuffer rb, string tag, double defaultValue)
		{
			object obj = LspUtil.FetchAssocParam(rb, tag);
			if (obj == null)
			{
				return defaultValue;
			}
			string s = obj.ToString();
			double result;
			if (double.TryParse(s, out result))
			{
				return result;
			}
			return defaultValue;
		}

		public static bool GetAssocParam(ResultBuffer rb, string tag, bool defaultValue)
		{
			object obj = LspUtil.FetchAssocParam(rb, tag);
			if (obj == null)
			{
				return defaultValue;
			}
			string value = obj.ToString();
			if (!LspUtil.BoolParser.CanParse(value))
			{
				return defaultValue;
			}
			return LspUtil.BoolParser.GetValue(value);
		}

		public static Point2d GetAssocParam(ResultBuffer rb, string tag, Point2d defaultValue)
		{
			object obj = LspUtil.FetchAssocParam(rb, tag);
			if (obj == null)
			{
				return defaultValue;
			}
			if (obj is Point2d)
			{
				return (Point2d)obj;
			}
			if (obj is Point3d)
			{
				Point3d point3d = (Point3d)obj;
				Point2d result = new Point2d(point3d.X, point3d.Y);
				return result;
			}
			return defaultValue;
		}

		public static Point3d GetAssocParam(ResultBuffer rb, string tag, Point3d defaultValue)
		{
			object obj = LspUtil.FetchAssocParam(rb, tag);
			if (obj == null)
			{
				return defaultValue;
			}
			if (obj is Point3d)
			{
				return (Point3d)obj;
			}
			if (obj is Point2d)
			{
				Point2d point2d = (Point2d)obj;
				Point3d result = new Point3d(point2d.X, point2d.Y, 0.0);
				return result;
			}
			return defaultValue;
		}

		public static string GetArgument(ResultBuffer rb, int index, string defaultValue)
		{
			string result;
			try
			{
				if (rb == null)
				{
					result = defaultValue;
				}
				else
				{
					TypedValue[] array = rb.AsArray();
					if (array.Count<TypedValue>() < index + 1)
					{
						result = defaultValue;
					}
					else
					{
						object typedValue = LspUtil.GetTypedValue(array[index]);
						if (typedValue is string)
						{
							result = (string)typedValue;
						}
						else
						{
							result = defaultValue;
						}
					}
				}
			}
			catch
			{
				result = defaultValue;
			}
			return result;
		}

		public static ObjectId GetArgument(ResultBuffer rb, int index, ObjectId defaultValue)
		{
			ObjectId result;
			try
			{
				if (rb == null)
				{
					result = defaultValue;
				}
				else
				{
					TypedValue[] array = rb.AsArray();
					if (array.Count<TypedValue>() < index + 1)
					{
						result = defaultValue;
					}
					else
					{
						object typedValue = LspUtil.GetTypedValue(array[index]);
						if (typedValue is ObjectId)
						{
							result = (ObjectId)typedValue;
						}
						else
						{
							result = defaultValue;
						}
					}
				}
			}
			catch
			{
				result = defaultValue;
			}
			return result;
		}

		public static object GetTypedValue(TypedValue tv)
		{
			object result;
			if (tv.TypeCode == 5021)
			{
				result = true;
			}
			else if (tv.TypeCode == 5003)
			{
				result = (short)tv.Value;
			}
			else if (tv.TypeCode == 5010)
			{
				result = (int)tv.Value;
			}
			else if (tv.TypeCode == 5001 || tv.TypeCode == 5004)
			{
				result = (double)tv.Value;
			}
			else if (tv.TypeCode == 5000 || tv.TypeCode == 5019 || tv.TypeCode == 5014)
			{
				result = null;
			}
			else if (tv.TypeCode == 5005)
			{
				result = (tv.Value as string);
			}
			else if (tv.TypeCode == 5002)
			{
				Point2d point2d = (Point2d)tv.Value;
				result = point2d;
			}
			else if (tv.TypeCode == 5009)
			{
				Point3d point3d = (Point3d)tv.Value;
				result = point3d;
			}
			else if (tv.TypeCode == 5006)
			{
				result = tv.Value;
			}
			else if (tv.TypeCode == 5007)
			{
				result = tv.Value;
			}
			else if (tv.TypeCode == 5016)
			{
				result = tv.Value;
			}
			else if (tv.TypeCode == 5017)
			{
				result = tv.Value;
			}
			else if (tv.TypeCode == 5008)
			{
				result = tv.Value;
			}
			else if (tv.TypeCode == 5018)
			{
				result = tv.Value;
			}
			else
			{
				result = tv;
			}
			return result;
		}

		private static object FetchAssocParam(ResultBuffer rb, string tag)
		{
			if (rb == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(tag))
			{
				return null;
			}
			Dictionary<string, object> dictionary = LspUtil.BuildAssocDictionary(rb);
			dictionary.Keys.Any((string key) => key.Equals("key", StringComparison.InvariantCultureIgnoreCase));
			if (!dictionary.ContainsKey(tag))
			{
				return null;
			}
			object obj = dictionary[tag];
			if (obj == null)
			{
				return null;
			}
			return obj;
		}

		private static Point2d BuildPoint2d(List<object> values)
		{
			Point2d result;
			try
			{
				string s = values[0].ToString();
				string s2 = values[1].ToString();
				Point2d point2d = new Point2d(double.Parse(s), double.Parse(s2));
				result = point2d;
			}
			catch
			{
				throw new Exception("Invalid Point Values");
			}
			return result;
		}

		private static Point3d BuildPoint3d(List<object> values)
		{
			Point3d result;
			try
			{
				string s = values[0].ToString();
				string s2 = values[1].ToString();
				string s3 = values[2].ToString();
				Point3d point3d = new Point3d(double.Parse(s), double.Parse(s2), double.Parse(s3));
				result = point3d;
			}
			catch
			{
				throw new Exception("Invalid Point Values");
			}
			return result;
		}

		private static object BuildValueFromGroup(List<object> groupedObjects)
		{
			if (groupedObjects.Count == 1)
			{
				return groupedObjects[0];
			}
			object result;
			try
			{
				if (groupedObjects.Count == 2)
				{
					Point2d point2d = LspUtil.BuildPoint2d(groupedObjects);
					result = point2d;
				}
				else if (groupedObjects.Count == 3)
				{
					Point3d point3d = LspUtil.BuildPoint3d(groupedObjects);
					result = point3d;
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

		public static Dictionary<string, object> BuildAssocDictionary(ResultBuffer rb)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			TypedValue[] array = rb.AsArray();
			object obj = null;
			List<object> list = new List<object>();
			TypedValue[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				TypedValue typedValue = array2[i];
				if (typedValue.TypeCode == 5016)
				{
					list.Clear();
					obj = null;
				}
				else if (typedValue.TypeCode == 5017)
				{
					if (obj != null && list.Count > 0)
					{
						string key = obj.ToString();
						object obj2 = LspUtil.BuildValueFromGroup(list);
						if (obj2 != null)
						{
							dictionary.Add(key, obj2);
						}
						list.Clear();
						obj = null;
					}
				}
				else if (typedValue.TypeCode == 5018)
				{
					if (obj != null && list.Count > 0)
					{
						string key2 = obj.ToString();
						object obj3 = LspUtil.BuildValueFromGroup(list);
						if (obj3 != null)
						{
							dictionary.Add(key2, obj3);
						}
						list.Clear();
						obj = null;
					}
				}
				else if (obj == null)
				{
					obj = typedValue.Value;
				}
				else
				{
					list.Add(typedValue.Value);
				}
			}
			if (obj != null && list.Count > 0)
			{
				string key3 = obj.ToString();
				object obj4 = LspUtil.BuildValueFromGroup(list);
				if (obj4 != null)
				{
					dictionary.Add(key3, obj4);
				}
				list.Clear();
			}
			return dictionary;
		}
	}
}
