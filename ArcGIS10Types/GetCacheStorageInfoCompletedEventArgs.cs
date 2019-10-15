using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough]
	public class GetCacheStorageInfoCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public CacheStorageInfo Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CacheStorageInfo)this.results[0];
			}
		}

		internal GetCacheStorageInfoCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
}
