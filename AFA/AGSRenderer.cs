using System.Collections.Generic;
using System.Text;

namespace AFA
{
    public abstract class AGSRenderer
	{
		private string RendererType
		{
			get;
			set;
		}

		private string Label
		{
			get;
			set;
		}

		private string Description
		{
			get;
			set;
		}

		public AGSSymbol DefaultSymbol
		{
			get;
			set;
		}

		public void Initialize(IDictionary<string, object> dict)
		{
			this.RendererType = "";
			this.Label = "";
			this.Description = "";
			this.DefaultSymbol = null;
			foreach (KeyValuePair<string, object> current in dict)
			{
				if (current.Key == "type")
				{
					this.RendererType = (current.Value as string);
				}
				else if (current.Key == "label")
				{
					this.Label = (current.Value as string);
				}
				else if (current.Key == "description")
				{
					this.Description = (current.Value as string);
				}
				else if (current.Key == "defaultsymbol")
				{
					this.DefaultSymbol = new AGSSymbol();
					this.DefaultSymbol.Initialize(current.Value as IDictionary<string, object>);
				}
			}
		}

		public override string ToString()
		{
			string arg = "";
			StringBuilder stringBuilder = new StringBuilder();
			if (this.RendererType.Length > 0)
			{
				stringBuilder.AppendFormat("Type = {0}", this.RendererType);
			}
			if (this.Label.Length > 0)
			{
				stringBuilder.AppendFormat(", Label = {0}", this.Label);
			}
			if (this.Description.Length > 0)
			{
				stringBuilder.AppendFormat(", Desc = {0}", this.Description);
			}
			return arg + stringBuilder;
		}
	}
}
