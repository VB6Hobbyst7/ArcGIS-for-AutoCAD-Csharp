using System;

namespace AFA.UI
{
	public class CommandProgressEventArgs : EventArgs
	{
		public int ProgressValue
		{
			get;
			set;
		}

		public string ProgressTitle
		{
			get;
			set;
		}

		public string ProgressMessage
		{
			get;
			set;
		}

		public CommandProgressEventArgs()
		{
			this.ProgressValue = 0;
			this.ProgressTitle = "";
			this.ProgressMessage = "";
		}
	}
}
