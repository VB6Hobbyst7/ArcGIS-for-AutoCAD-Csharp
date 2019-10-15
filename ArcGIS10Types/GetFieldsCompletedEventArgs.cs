using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough]
	public class GetFieldsCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public Fields Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Fields)this.results[0];
			}
		}

		internal GetFieldsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
}
