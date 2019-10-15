using System.Collections.Generic;
using System.Text;

namespace AFA
{
    public class AGSSimpleLineSymbol : AGSSymbol
	{
		private decimal Width
		{
			get;
			set;
		}

		public AGSSimpleLineSymbol(IDictionary<string, object> dict)
		{
			base.Initialize(dict);
			object obj;
			if (dict.TryGetValue("width", out obj))
			{
				this.Width = decimal.Parse(obj.ToString());
			}
		}

		public override string ToString()
		{
			string arg = base.ToString();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(", Width: {0}", this.Width);
			return arg + stringBuilder;
		}
	}
}
