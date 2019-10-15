using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough]
	public class GetAreasAndLengthsCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public double[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (double[])this.results[0];
			}
		}

		public double[] Lengths
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (double[])this.results[1];
			}
		}

		internal GetAreasAndLengthsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
}
