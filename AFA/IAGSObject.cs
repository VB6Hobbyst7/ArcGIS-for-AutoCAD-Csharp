using System.Collections.Generic;

namespace AFA
{
    public interface IAGSObject
	{
		string Name
		{
			get;
			set;
		}

		string FullName
		{
			get;
			set;
		}

		AGSConnection Parent
		{
			get;
			set;
		}

		bool IsValid
		{
			get;
			set;
		}

		AGSType Type
		{
			get;
			set;
		}

		IDictionary<string, object> Properties
		{
			get;
			set;
		}

		bool HasChildren();

		IEnumerable<object> GetChildren();
	}
}
