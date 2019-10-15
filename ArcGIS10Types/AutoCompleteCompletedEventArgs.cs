using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough]
	public class AutoCompleteCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public Polygon[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Polygon[])this.results[0];
			}
		}

		internal AutoCompleteCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
}
