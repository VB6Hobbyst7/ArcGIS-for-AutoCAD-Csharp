using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough]
	public class FromMapPointsCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public int[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int[])this.results[0];
			}
		}

		public int[] ScreenYValues
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int[])this.results[1];
			}
		}

		internal FromMapPointsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
}
