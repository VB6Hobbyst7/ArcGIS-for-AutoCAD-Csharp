using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough]
	public class GetRasterInfoCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public RasterInfo Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (RasterInfo)this.results[0];
			}
		}

		internal GetRasterInfoCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
}
