using AFA.Resources;
using System.Collections.Generic;
using System.Text;

namespace AFA
{
    public class AGSDomain
	{
		public Dictionary<string, object> CodedValues;

		public Dictionary<string, object> Range;

		public string Name
		{
			get;
			set;
		}

		public string Type
		{
			get;
			set;
		}

		public void Initialize(Dictionary<string, object> dict)
		{
			this.CodedValues = null;
			this.Range = null;
			foreach (KeyValuePair<string, object> current in dict)
			{
				if (current.Key == "name")
				{
					this.Name = (current.Value as string);
				}
				else if (current.Key == "type")
				{
					this.Type = (current.Value as string);
				}
				else
				{
					if (current.Key == "codedValues")
					{
						IList<object> list = current.Value as IList<object>;
						this.CodedValues = new Dictionary<string, object>();
						using (IEnumerator<object> enumerator2 = list.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								object current2 = enumerator2.Current;
								IDictionary<string, object> dictionary = current2 as IDictionary<string, object>;
								object obj;
								if (dictionary.TryGetValue("name", out obj))
								{
									string key = obj as string;
									if (dictionary.TryGetValue("code", out obj) && !this.CodedValues.ContainsKey(key))
									{
										this.CodedValues.Add(key, obj);
									}
								}
							}
							continue;
						}
					}
					if (current.Key == "range")
					{
						IList<object> list2 = current.Value as IList<object>;
						this.Range = new Dictionary<string, object>();
						this.Range.Add("Min", list2[0]);
						this.Range.Add("Max", list2[1]);
					}
				}
			}
		}

		public override string ToString()
		{
			string result;
			try
			{
				if (this.Name == null || this.Type == null)
				{
					result = AfaStrings.InvalidDomain;
				}
				else
				{
					string arg = "";
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendFormat("{0}, {1}", this.Name, this.Type);
					if (this.CodedValues != null)
					{
						stringBuilder.Append(" Coded Values=");
						foreach (KeyValuePair<string, object> current in this.CodedValues)
						{
							stringBuilder.AppendFormat("{0}:{1} ", current.Key, current.Value);
						}
					}
					if (this.Range != null)
					{
						stringBuilder.Append(" Range=");
						foreach (KeyValuePair<string, object> current2 in this.Range)
						{
							stringBuilder.AppendFormat("{0}:{1} ", current2.Key, current2.Value);
						}
					}
					result = arg + stringBuilder;
				}
			}
			catch
			{
				result = AfaStrings.InvalidDomain;
			}
			return result;
		}
	}
}
