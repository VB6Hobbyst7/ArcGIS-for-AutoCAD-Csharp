using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace AFA
{
	public class Utility
	{
		public static bool DictionaryEqual<TKey, TValue>(IDictionary<TKey, TValue> first, IDictionary<TKey, TValue> second)
		{
			if (first == second)
			{
				return true;
			}
			if (first == null || second == null)
			{
				return false;
			}
			if (first.Count != second.Count)
			{
				return false;
			}
			EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			foreach (KeyValuePair<TKey, TValue> current in first)
			{
				TValue y;
				if (!second.TryGetValue(current.Key, out y))
				{
					bool result = false;
					return result;
				}
				if (!@default.Equals(current.Value, y))
				{
					bool result = false;
					return result;
				}
			}
			return true;
		}

		public static List<string> InitializeStringList(string initialValue)
		{
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(initialValue))
			{
				list.Add(initialValue);
			}
			return list;
		}

		public static T DeepClone<T>(T obj)
		{
			T result;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					binaryFormatter.Serialize(memoryStream, obj);
					memoryStream.Position = 0L;
					result = (T)((object)binaryFormatter.Deserialize(memoryStream));
				}
			}
			catch
			{
				result = (T)((object)Utility.CloneObject(obj));
			}
			return result;
		}

		public static object CloneObject(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			Type type = obj.GetType();
			MethodInfo method = type.GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic);
			if (method != null)
			{
				return method.Invoke(obj, null);
			}
			return null;
		}

		public static string TemporaryFilePath()
		{
			return Path.GetTempFileName();
		}
	}
}
