using System.Collections.Generic;
using System.Text;

namespace AFA
{
    public class AGSSimpleFillSymbol : AGSSymbol
	{
		private AGSSimpleLineSymbol outline
		{
			get;
			set;
		}

		public AGSSimpleFillSymbol(IDictionary<string, object> dict)
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
