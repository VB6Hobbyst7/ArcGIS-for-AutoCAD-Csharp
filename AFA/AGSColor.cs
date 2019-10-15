using System;
using System.Text;

namespace AFA
{
	public class AGSColor
	{
		public byte Red
		{
			get;
			set;
		}

		public byte Green
		{
			get;
			set;
		}

		public byte Blue
		{
			get;
			set;
		}

		public byte Alpha
		{
			get;
			set;
		}

		public AGSColor(byte r, byte g, byte b, byte a)
		{
			this.Red = Convert.ToByte(r);
			this.Green = Convert.ToByte(g);
			this.Blue = Convert.ToByte(b);
			this.Alpha = Convert.ToByte(a);
		}

		public AGSColor(int r, int g, int b, int a)
		{
			this.Red = Convert.ToByte(r);
			this.Green = Convert.ToByte(g);
			this.Blue = Convert.ToByte(b);
			this.Alpha = Convert.ToByte(a);
		}

		public AGSColor(object[] list)
		{
			if (list != null)
			{
				this.Red = Convert.ToByte(list[0]);
				this.Green = Convert.ToByte(list[1]);
				this.Blue = Convert.ToByte(list[2]);
				this.Alpha = Convert.ToByte(list[3]);
			}
		}

		public override string ToString()
		{
			string arg = "";
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("[{0}, {1}, {2}, {3}]", new object[]
			{
				this.Red,
				this.Green,
				this.Blue,
				this.Alpha
			});
			return arg + stringBuilder;
		}
	}
}
