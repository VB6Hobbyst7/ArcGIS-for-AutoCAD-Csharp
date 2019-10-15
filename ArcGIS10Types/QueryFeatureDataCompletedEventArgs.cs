using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough]
	public class QueryFeatureDataCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public RecordSet Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (RecordSet)this.results[0];
			}
		}

		internal QueryFeatureDataCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
}
