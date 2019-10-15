using System;
using System.Windows.Forms;

namespace AFA.UI
{
	public class CommandStartEventArgs : EventArgs
	{
		public string WindowTitle
		{
			get;
			set;
		}

		public int ProgressMinValue
		{
			get;
			set;
		}

		public int ProgressMaxValue
		{
			get;
			set;
		}

		public int PrograssInitValue
		{
			get;
			set;
		}

		public ProgressBarStyle ProgressBarStyle
		{
			get;
			set;
		}

		public CommandStartEventArgs()
		{
			this.WindowTitle = "Command in Progress";
			this.ProgressMinValue = 0;
			this.ProgressMaxValue = 0;
		}
	}
}
