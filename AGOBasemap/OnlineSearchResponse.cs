using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AGOBasemap
{
    [DataContract]
	public class OnlineSearchResponse
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

		public List<OnlineSearchItem> Results
		{
			get;
			set;
		}
	}
}
