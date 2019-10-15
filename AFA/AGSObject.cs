using System.Collections.Generic;

namespace AFA
{
    public abstract class AGSObject : IAGSObject
	{
		public string Name
		{
			get;
			set;
		}

		public string FullName
		{
			get;
			set;
		}

		public AGSConnection Parent
		{
			get;
			set;
		}

		public bool IsValid
		{
			get;
			set;
		}

		public AGSType Type
		{
			get;
			set;
		}

		public IDictionary<string, object> Properties
		{
			get;
			set;
		}

		public virtual AGSExportOptions ExportOptions
		{
			get;
			set;
		}

		public bool HasChildren()
		{
			return false;
		}

		public IEnumerable<object> GetChildren()
		{
			return null;
		}

		public abstract string GetWKT();

		public override string ToString()
		{
			return this.Name;
		}
	}
}
