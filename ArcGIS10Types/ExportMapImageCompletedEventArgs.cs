using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough]
	public class ExportMapImageCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public MapImage Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (MapImage)this.results[0];
			}
		}

		internal ExportMapImageCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
}
