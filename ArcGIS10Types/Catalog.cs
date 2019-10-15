using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, WebServiceBinding(Name = "ServiceCatalogBinding", Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	public class Catalog : SoapHttpClientProtocol
	{
		private SendOrPostCallback GetMessageVersionOperationCompleted;

		private SendOrPostCallback GetMessageFormatsOperationCompleted;

		private SendOrPostCallback GetTokenServiceURLOperationCompleted;

		private SendOrPostCallback GetFoldersOperationCompleted;

		private SendOrPostCallback GetServiceDescriptionsOperationCompleted;

		private SendOrPostCallback RequiresTokensOperationCompleted;

		private SendOrPostCallback GetServiceDescriptionsExOperationCompleted;

		public event GetMessageVersionCompletedEventHandler GetMessageVersionCompleted;

		public event GetMessageFormatsCompletedEventHandler GetMessageFormatsCompleted;

		public event GetTokenServiceURLCompletedEventHandler GetTokenServiceURLCompleted;

		public event GetFoldersCompletedEventHandler GetFoldersCompleted;

		public event GetServiceDescriptionsCompletedEventHandler GetServiceDescriptionsCompleted;

		public event RequiresTokensCompletedEventHandler RequiresTokensCompleted;

		public event GetServiceDescriptionsExCompletedEventHandler GetServiceDescriptionsExCompleted;

		public Catalog()
		{
			base.Url = "http://localhost/ArcGIS/Services";
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("MessageVersion", Form = XmlSchemaForm.Unqualified)]
		public esriArcGISVersion GetMessageVersion()
		{
			object[] array = base.Invoke("GetMessageVersion", new object[0]);
			return (esriArcGISVersion)array[0];
		}

		public IAsyncResult BeginGetMessageVersion(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetMessageVersion", new object[0], callback, asyncState);
		}

		public esriArcGISVersion EndGetMessageVersion(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (esriArcGISVersion)array[0];
		}

		public void GetMessageVersionAsync()
		{
			this.GetMessageVersionAsync(null);
		}

		public void GetMessageVersionAsync(object userState)
		{
			if (this.GetMessageVersionOperationCompleted == null)
			{
				this.GetMessageVersionOperationCompleted = new SendOrPostCallback(this.OnGetMessageVersionOperationCompleted);
			}
			base.InvokeAsync("GetMessageVersion", new object[0], this.GetMessageVersionOperationCompleted, userState);
		}

		private void OnGetMessageVersionOperationCompleted(object arg)
		{
			if (this.GetMessageVersionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetMessageVersionCompleted(this, new GetMessageVersionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("MessageFormats", Form = XmlSchemaForm.Unqualified)]
		public esriServiceCatalogMessageFormat GetMessageFormats()
		{
			object[] array = base.Invoke("GetMessageFormats", new object[0]);
			return (esriServiceCatalogMessageFormat)array[0];
		}

		public IAsyncResult BeginGetMessageFormats(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetMessageFormats", new object[0], callback, asyncState);
		}

		public esriServiceCatalogMessageFormat EndGetMessageFormats(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (esriServiceCatalogMessageFormat)array[0];
		}

		public void GetMessageFormatsAsync()
		{
			this.GetMessageFormatsAsync(null);
		}

		public void GetMessageFormatsAsync(object userState)
		{
			if (this.GetMessageFormatsOperationCompleted == null)
			{
				this.GetMessageFormatsOperationCompleted = new SendOrPostCallback(this.OnGetMessageFormatsOperationCompleted);
			}
			base.InvokeAsync("GetMessageFormats", new object[0], this.GetMessageFormatsOperationCompleted, userState);
		}

		private void OnGetMessageFormatsOperationCompleted(object arg)
		{
			if (this.GetMessageFormatsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetMessageFormatsCompleted(this, new GetMessageFormatsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("TokenServiceURL", Form = XmlSchemaForm.Unqualified)]
		public string GetTokenServiceURL()
		{
			object[] array = base.Invoke("GetTokenServiceURL", new object[0]);
			return (string)array[0];
		}

		public IAsyncResult BeginGetTokenServiceURL(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetTokenServiceURL", new object[0], callback, asyncState);
		}

		public string EndGetTokenServiceURL(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (string)array[0];
		}

		public void GetTokenServiceURLAsync()
		{
			this.GetTokenServiceURLAsync(null);
		}

		public void GetTokenServiceURLAsync(object userState)
		{
			if (this.GetTokenServiceURLOperationCompleted == null)
			{
				this.GetTokenServiceURLOperationCompleted = new SendOrPostCallback(this.OnGetTokenServiceURLOperationCompleted);
			}
			base.InvokeAsync("GetTokenServiceURL", new object[0], this.GetTokenServiceURLOperationCompleted, userState);
		}

		private void OnGetTokenServiceURLOperationCompleted(object arg)
		{
			if (this.GetTokenServiceURLCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetTokenServiceURLCompleted(this, new GetTokenServiceURLCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("FolderNames", Form = XmlSchemaForm.Unqualified), XmlArrayItem("String", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public string[] GetFolders()
		{
			object[] array = base.Invoke("GetFolders", new object[0]);
			return (string[])array[0];
		}

		public IAsyncResult BeginGetFolders(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetFolders", new object[0], callback, asyncState);
		}

		public string[] EndGetFolders(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (string[])array[0];
		}

		public void GetFoldersAsync()
		{
			this.GetFoldersAsync(null);
		}

		public void GetFoldersAsync(object userState)
		{
			if (this.GetFoldersOperationCompleted == null)
			{
				this.GetFoldersOperationCompleted = new SendOrPostCallback(this.OnGetFoldersOperationCompleted);
			}
			base.InvokeAsync("GetFolders", new object[0], this.GetFoldersOperationCompleted, userState);
		}

		private void OnGetFoldersOperationCompleted(object arg)
		{
			if (this.GetFoldersCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetFoldersCompleted(this, new GetFoldersCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("ServiceDescriptions", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public ServiceDescription[] GetServiceDescriptions()
		{
			object[] array = base.Invoke("GetServiceDescriptions", new object[0]);
			return (ServiceDescription[])array[0];
		}

		public IAsyncResult BeginGetServiceDescriptions(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetServiceDescriptions", new object[0], callback, asyncState);
		}

		public ServiceDescription[] EndGetServiceDescriptions(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ServiceDescription[])array[0];
		}

		public void GetServiceDescriptionsAsync()
		{
			this.GetServiceDescriptionsAsync(null);
		}

		public void GetServiceDescriptionsAsync(object userState)
		{
			if (this.GetServiceDescriptionsOperationCompleted == null)
			{
				this.GetServiceDescriptionsOperationCompleted = new SendOrPostCallback(this.OnGetServiceDescriptionsOperationCompleted);
			}
			base.InvokeAsync("GetServiceDescriptions", new object[0], this.GetServiceDescriptionsOperationCompleted, userState);
		}

		private void OnGetServiceDescriptionsOperationCompleted(object arg)
		{
			if (this.GetServiceDescriptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetServiceDescriptionsCompleted(this, new GetServiceDescriptionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public bool RequiresTokens()
		{
			object[] array = base.Invoke("RequiresTokens", new object[0]);
			return (bool)array[0];
		}

		public IAsyncResult BeginRequiresTokens(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("RequiresTokens", new object[0], callback, asyncState);
		}

		public bool EndRequiresTokens(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (bool)array[0];
		}

		public void RequiresTokensAsync()
		{
			this.RequiresTokensAsync(null);
		}

		public void RequiresTokensAsync(object userState)
		{
			if (this.RequiresTokensOperationCompleted == null)
			{
				this.RequiresTokensOperationCompleted = new SendOrPostCallback(this.OnRequiresTokensOperationCompleted);
			}
			base.InvokeAsync("RequiresTokens", new object[0], this.RequiresTokensOperationCompleted, userState);
		}

		private void OnRequiresTokensOperationCompleted(object arg)
		{
			if (this.RequiresTokensCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RequiresTokensCompleted(this, new RequiresTokensCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("ServiceDescriptions", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public ServiceDescription[] GetServiceDescriptionsEx([XmlElement(Form = XmlSchemaForm.Unqualified)] string FolderName)
		{
			object[] array = base.Invoke("GetServiceDescriptionsEx", new object[]
			{
				FolderName
			});
			return (ServiceDescription[])array[0];
		}

		public IAsyncResult BeginGetServiceDescriptionsEx(string FolderName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetServiceDescriptionsEx", new object[]
			{
				FolderName
			}, callback, asyncState);
		}

		public ServiceDescription[] EndGetServiceDescriptionsEx(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ServiceDescription[])array[0];
		}

		public void GetServiceDescriptionsExAsync(string FolderName)
		{
			this.GetServiceDescriptionsExAsync(FolderName, null);
		}

		public void GetServiceDescriptionsExAsync(string FolderName, object userState)
		{
			if (this.GetServiceDescriptionsExOperationCompleted == null)
			{
				this.GetServiceDescriptionsExOperationCompleted = new SendOrPostCallback(this.OnGetServiceDescriptionsExOperationCompleted);
			}
			base.InvokeAsync("GetServiceDescriptionsEx", new object[]
			{
				FolderName
			}, this.GetServiceDescriptionsExOperationCompleted, userState);
		}

		private void OnGetServiceDescriptionsExOperationCompleted(object arg)
		{
			if (this.GetServiceDescriptionsExCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetServiceDescriptionsExCompleted(this, new GetServiceDescriptionsExCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}
	}
}
