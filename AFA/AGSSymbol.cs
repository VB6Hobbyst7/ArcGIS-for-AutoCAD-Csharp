using System.Collections.Generic;
using System.Text;

namespace AFA
{
    public class AGSSymbol
	{
		private string Type
		{
			get;
			set;
		}

		private string Style
		{
			get;
			set;
		}

		private AGSColor Color
		{
			get;
			set;
		}

		public void Initialize(IDictionary<string, object> dict)
		{
			if (dict == null)
			{
				return;
			}
			object obj;
			if (dict.TryGetValue("type", out obj))
			{
				this.Type = (obj as string);
			}
			if (dict.TryGetValue("style", out obj))
			{
				this.Style = (obj as string);
			}
			if (dict.TryGetValue("color", out obj))
			{
				this.Color = new AGSColor(obj as object[]);
			}
		}

		public override string ToString()
		{
			string arg = "";
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Type: {0}, Style: {1}, Color: {2}", this.Type, this.Style, this.Color);
			return arg + stringBuilder;
		}
	}
}
