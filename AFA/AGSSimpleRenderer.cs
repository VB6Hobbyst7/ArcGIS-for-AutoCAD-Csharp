using System.Collections.Generic;
using System.Text;

namespace AFA
{
    public class AGSSimpleRenderer : AGSRenderer
	{
		private AGSSymbol Symbol
		{
			get;
			set;
		}

		public AGSSimpleRenderer(IDictionary<string, object> dict)
		{
			base.Initialize(dict);
			this.Symbol = null;
			object obj;
			if (dict.TryGetValue("symbol", out obj))
			{
				IDictionary<string, object> dictionary = obj as IDictionary<string, object>;
				object obj2;
				if (dictionary.TryGetValue("type", out obj2))
				{
					string a = obj2 as string;
					if (a == "esriSMS")
					{
						this.Symbol = new AGSSimpleMarkerSymbol(dictionary);
						return;
					}
					if (a == "esriSLS")
					{
						this.Symbol = new AGSSimpleLineSymbol(dictionary);
						return;
					}
					if (a == "esriSFS")
					{
						this.Symbol = new AGSSimpleFillSymbol(dictionary);
						return;
					}
					if (a == "esriPMS")
					{
						this.Symbol = new AGSPictureMarkerSymbol(dictionary);
						return;
					}
					if (a == "esriPFS")
					{
						this.Symbol = new AGSPictureFillSymbol(dictionary);
						return;
					}
					if (a == "esriTS")
					{
						this.Symbol = null;
					}
				}
			}
		}

		public override string ToString()
		{
			string arg = base.ToString();
			StringBuilder stringBuilder = new StringBuilder();
			if (this.Symbol != null)
			{
				stringBuilder.AppendFormat(", Symbol = {0}", this.Symbol);
			}
			return arg + stringBuilder;
		}
	}
}
