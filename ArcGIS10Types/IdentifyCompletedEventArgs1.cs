using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough]
	public class IdentifyCompletedEventArgs1 : AsyncCompletedEventArgs
	{
		private object[] results;

		public ImageServerIdentifyResult Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ImageServerIdentifyResult)this.results[0];
			}
		}

		internal IdentifyCompletedEventArgs1(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
}
