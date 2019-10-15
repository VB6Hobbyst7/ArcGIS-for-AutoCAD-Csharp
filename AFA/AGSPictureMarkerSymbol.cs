using System.Collections.Generic;
using System.Text;

namespace AFA
{
    public class AGSPictureMarkerSymbol : AGSSymbol
	{
		private string URL
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

		private int Angle
		{
			get;
			set;
		}

		private int Width
		{
			get;
			set;
		}

		private int Height
		{
			get;
			set;
		}

		private string ContentType
		{
			get;
			set;
		}

		private byte[] ImageData
		{
			get;
			set;
		}

		public AGSPictureMarkerSymbol(IDictionary<string, object> dict)
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
