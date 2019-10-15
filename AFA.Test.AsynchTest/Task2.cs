using System.Text;
using System.Threading;

namespace AFA.Test.AsynchTest
{
    public class Task2 : ITask
	{
		private StringBuilder _errMsg = new StringBuilder();

		private bool _successful;

		public string TaskName
		{
			get
			{
				return "Send Data To XXX Service";
			}
		}

		public string CompletionMessage
		{
			get
			{
				return this._errMsg.ToString();
			}
		}

		public bool Successful
		{
			get
			{
				return this._successful;
			}
		}

		public object ResultData
		{
			get
			{
				return null;
			}
		}

		public void Execute()
		{
			this._errMsg.Length = 0;
			this._successful = true;
			this.DoTask();
		}

		private void DoTask()
		{
			Thread.Sleep(5000);
			this._errMsg.Append("This startup task has been completed successfully!");
		}
	}
}
