using System;

namespace AFA
{
	public class TaskCompletedEventArgs : EventArgs
	{
		private object _outputData;

		private string _taskName;

		public string TaskName
		{
			get
			{
				return this._taskName;
			}
		}

		public object OutputData
		{
			get
			{
				return this._outputData;
			}
		}

		public TaskCompletedEventArgs(string taskName, object outputData)
		{
			this._taskName = taskName;
			this._outputData = outputData;
		}
	}
}
