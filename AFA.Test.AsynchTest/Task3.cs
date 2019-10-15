using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AFA.Test.AsynchTest
{
    public class Task3 : ITask
	{
		private StringBuilder _errMsg = new StringBuilder();

		private bool _successful;

		private List<string> _returnData;

		public string TaskName
		{
			get
			{
				return "Retrieve Google Map Data";
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
				return this._returnData;
			}
		}

		public void Execute()
		{
			this._errMsg.Length = 0;
			this._successful = true;
			this._returnData = new List<string>();
			this.DoTask();
		}

		private void DoTask()
		{
			Thread.Sleep(10000);
			this._errMsg.Append("This startup task has been completed successfully!");
			this._returnData.AddRange(new string[]
			{
				"AAAA",
				"BBBB",
				"CCCC"
			});
		}
	}
}
