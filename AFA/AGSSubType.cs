using System.Collections.Generic;
using System.Text;

namespace AFA
{
    public class AGSSubType
	{
		public Dictionary<string, AGSDomain> Domains;

		private IList<AGSTemplate> Templates;

		public object ID
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public AGSColor SuggestedColor
		{
			get;
			set;
		}

		public void Initialize(Dictionary<string, object> dict)
		{
			object obj;
			if (dict.TryGetValue("id", out obj))
			{
				this.ID = obj;
			}
			if (dict.TryGetValue("name", out obj))
			{
				this.Name = (obj as string);
			}
			if (dict.TryGetValue("domains", out obj) && obj != null)
			{
				IDictionary<string, object> dictionary = obj as IDictionary<string, object>;
				if (dictionary.Count > 0)
				{
					this.Domains = new Dictionary<string, AGSDomain>();
					foreach (KeyValuePair<string, object> current in dictionary)
					{
						AGSDomain aGSDomain = new AGSDomain();
						aGSDomain.Name = current.Key;
						aGSDomain.Initialize(current.Value as Dictionary<string, object>);
						this.Domains.Add(current.Key, aGSDomain);
					}
				}
			}
			if (dict.TryGetValue("templates", out obj) && obj != null)
			{
				IList<object> list = obj as IList<object>;
				if (list.Count > 0)
				{
					this.Templates = new List<AGSTemplate>();
					foreach (object current2 in list)
					{
						AGSTemplate aGSTemplate = new AGSTemplate();
						aGSTemplate.Initialize(current2 as Dictionary<string, object>);
						this.Templates.Add(aGSTemplate);
					}
				}
			}
		}

		public override string ToString()
		{
			string result;
			try
			{
				string arg = "";
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("{0}, {1}", this.ID, this.Name);
				if (this.Domains != null)
				{
					stringBuilder.Append(" (Domains: ");
					foreach (KeyValuePair<string, AGSDomain> current in this.Domains)
					{
						stringBuilder.AppendFormat("<{0}>", current.Key);
					}
					stringBuilder.Append(")");
				}
				result = arg + stringBuilder;
			}
			catch
			{
				result = "Invalid Field";
			}
			return result;
		}
	}
}
