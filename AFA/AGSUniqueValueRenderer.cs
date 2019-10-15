using System.Collections.Generic;

namespace AFA
{
    public class AGSUniqueValueRenderer : AGSRenderer
	{
		public AGSUniqueValueRenderer(IDictionary<string, object> dict)
		{
			base.Initialize(dict);
			object obj;
			if (dict.TryGetValue("defaultSymbol", out obj))
			{
				base.DefaultSymbol = new AGSSymbol();
				base.DefaultSymbol.Initialize(obj as IDictionary<string, object>);
			}
		}
	}
}
