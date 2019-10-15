using System.Collections.Generic;

namespace AGOBasemap
{
    public class OnlineSearchGroupsResponse
	{
		public string Query
		{
			get;
			set;
		}

		public int Total
		{
			get;
			set;
		}

		public int Start
		{
			get;
			set;
		}

		public int Num
		{
			get;
			set;
		}

		public int NextStart
		{
			get;
			set;
		}

		public List<OnlineSearchGroupItem> Results
		{
			get;
			set;
		}
	}
}
