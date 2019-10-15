using System.Collections.Generic;
using System.Text;

namespace AFA
{
    public class AGSSimpleMarkerSymbol : AGSSymbol
	{
		private int Size
		{
			get;
			set;
		}

		private int Angle
		{
			get;
			set;
		}

		private decimal XOffset
		{
			get;
			set;
		}

		private decimal YOffset
		{
			get;
			set;
		}

		private AGSColor OutlineColor
		{
			get;
			set;
		}

		private decimal OutlineWidth
		{
			get;
			set;
		}

		public AGSSimpleMarkerSymbol(IDictionary<string, object> dict)
		{
			base.Initialize(dict);
		}

		public override string ToString()
		{
			string arg = base.ToString();
			StringBuilder arg2 = new StringBuilder();
			return arg + arg2;
		}
	}
}
