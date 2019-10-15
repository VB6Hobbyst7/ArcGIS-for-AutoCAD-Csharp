using System.Collections.Generic;

namespace AFA
{
    public interface IAGSFolder
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

		AGSType Type
		{
			get;
			set;
		}

		string URL
		{
			get;
			set;
		}

		string ErrMsg
		{
			get;
			set;
		}

		IList<object> Children
		{
			get;
		}

		IList<object> Items
		{
			get;
		}

		void Clear();

		bool LoadChildren();
	}
}
