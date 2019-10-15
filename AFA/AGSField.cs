using AFA.Resources;
using ArcGIS10Types;
using System.Collections.Generic;
using System.Text;

namespace AFA
{
    public class AGSField
	{
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

		public string Alias
		{
			get;
			set;
		}

		public AGSDomain Domain
		{
			get;
			set;
		}

		public int Length
		{
			get;
			set;
		}

		public bool Editable
		{
			get;
			set;
		}

		public bool IsOID
		{
			get;
			set;
		}

		public bool IsGeom
		{
			get;
			set;
		}

		public bool IsTypeField
		{
			get;
			set;
		}

		public void Initialize(Dictionary<string, object> dict)
		{
			if (dict == null)
			{
				return;
			}
			foreach (KeyValuePair<string, object> current in dict)
			{
				if (current.Key == "name")
				{
					this.Name = (current.Value as string);
				}
				else if (current.Key == "type")
				{
					this.Type = (current.Value as string);
					this.IsOID = (this.Type == "esriFieldTypeOID");
					this.IsGeom = (this.Type == "esriFieldTypeGeometry");
				}
				else if (current.Key == "alias")
				{
					this.Alias = (current.Value as string);
				}
				else if (current.Key == "editable")
				{
					this.Editable = (bool)current.Value;
				}
				else if (current.Key == "length")
				{
					this.Length = (int)current.Value;
				}
				else if (current.Key == "domain" && current.Value != null)
				{
					this.Domain = new AGSDomain();
					this.Domain.Initialize(current.Value as Dictionary<string, object>);
				}
			}
		}

		public void Initialize(Field f)
		{
			if (f == null)
			{
				return;
			}
			this.Name = f.Name;
			this.Type = f.Type.ToString();
			this.IsOID = (this.Type == "esriFieldTypeOID");
			this.IsGeom = (this.Type == "esriFieldTypeGeometry");
			this.Alias = f.AliasName;
			this.Editable = f.Editable;
			this.Length = f.Length;
			this.IsTypeField = false;
		}

		public override string ToString()
		{
			string result;
			try
			{
				string arg = "";
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("{0}, {1}", this.Name, this.Type);
				if (this.Alias != null)
				{
					stringBuilder.AppendFormat(" Alias={0}", this.Alias);
				}
				if (this.Length != 0)
				{
					stringBuilder.AppendFormat(" Length={0}", this.Length);
				}
				stringBuilder.AppendFormat(" Editable={0}", this.Editable);
				if (this.Domain != null)
				{
					stringBuilder.AppendFormat(" Domain={0}", this.Domain.ToString());
				}
				result = arg + stringBuilder;
			}
			catch
			{
				result = AfaStrings.InvalidField;
			}
			return result;
		}
	}
}
