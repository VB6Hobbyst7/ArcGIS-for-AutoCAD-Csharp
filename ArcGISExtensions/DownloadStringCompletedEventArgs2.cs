using System;
using System.ComponentModel;

namespace ArcGISExtensions
{
	public class DownloadStringCompletedEventArgs2 : AsyncCompletedEventArgs
	{
		private string m_Result;

		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		internal DownloadStringCompletedEventArgs2(string result, Exception exception, bool cancelled, object userToken) : base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}
	}
}
