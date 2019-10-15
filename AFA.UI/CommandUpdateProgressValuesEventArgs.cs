using System;

namespace AFA.UI
{
	public class CommandUpdateProgressValuesEventArgs : EventArgs
	{
		public int MinValue
		{
			get;
			set;
		}

		public int MaxValue
		{
			get;
			set;
		}

		public CommandUpdateProgressValuesEventArgs()
		{
			this.MinValue = 0;
			this.MaxValue = 0;
		}

		public CommandUpdateProgressValuesEventArgs(int min, int max)
		{
			this.MinValue = min;
			this.MaxValue = max;
		}
	}
}
