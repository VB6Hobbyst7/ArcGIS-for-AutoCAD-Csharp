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
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, WebServiceBinding(Name = "ImageServerBinding", Namespace = "http://www.esri.com/schemas/ArcGIS/10.0"), XmlInclude(typeof(ImageServerForceDeriveFromAnyType)), XmlInclude(typeof(RasterInfo[]))]
	public class Temperature_ImageServer : SoapHttpClientProtocol
	{
		private SendOrPostCallback DownloadOperationCompleted;

		private SendOrPostCallback GetFieldsOperationCompleted;

		private SendOrPostCallback GetNativeRasterInfoOperationCompleted;

		private SendOrPostCallback GetThumbnailOperationCompleted;

		private SendOrPostCallback GetMetadataOperationCompleted;

		private SendOrPostCallback GetNativePixelBlockOperationCompleted;

		private SendOrPostCallback GetServiceInfoOperationCompleted;

		private SendOrPostCallback GetCatalogItemsOperationCompleted;

		private SendOrPostCallback GetVersionOperationCompleted;

		private SendOrPostCallback ExportScaledImageOperationCompleted;

		private SendOrPostCallback GetCatalogItemIDsOperationCompleted;

		private SendOrPostCallback GetFileOperationCompleted;

		private SendOrPostCallback GetPixelBlockOperationCompleted;

		private SendOrPostCallback GetCatalogItemCountOperationCompleted;

		private SendOrPostCallback GetRasterMetadataOperationCompleted;

		private SendOrPostCallback GetRasterInfoOperationCompleted;

		private SendOrPostCallback IdentifyOperationCompleted;

		private SendOrPostCallback ExecuteOperationCompleted;

		private SendOrPostCallback ExportImageOperationCompleted;

		private SendOrPostCallback GetImageOperationCompleted;

		private SendOrPostCallback GenerateServiceInfoOperationCompleted;

		public event DownloadCompletedEventHandler DownloadCompleted;

		public event GetFieldsCompletedEventHandler GetFieldsCompleted;

		public event GetNativeRasterInfoCompletedEventHandler GetNativeRasterInfoCompleted;

		public event GetThumbnailCompletedEventHandler GetThumbnailCompleted;

		public event GetMetadataCompletedEventHandler GetMetadataCompleted;

		public event GetNativePixelBlockCompletedEventHandler GetNativePixelBlockCompleted;

		public event GetServiceInfoCompletedEventHandler GetServiceInfoCompleted;

		public event GetCatalogItemsCompletedEventHandler GetCatalogItemsCompleted;

		public event GetVersionCompletedEventHandler GetVersionCompleted;

		public event ExportScaledImageCompletedEventHandler ExportScaledImageCompleted;

		public event GetCatalogItemIDsCompletedEventHandler GetCatalogItemIDsCompleted;

		public event GetFileCompletedEventHandler GetFileCompleted;

		public event GetPixelBlockCompletedEventHandler GetPixelBlockCompleted;

		public event GetCatalogItemCountCompletedEventHandler GetCatalogItemCountCompleted;

		public event GetRasterMetadataCompletedEventHandler GetRasterMetadataCompleted;

		public event GetRasterInfoCompletedEventHandler GetRasterInfoCompleted;

		public event IdentifyCompletedEventHandler1 IdentifyCompleted;

		public event ExecuteCompletedEventHandler ExecuteCompleted;

		public event ExportImageCompletedEventHandler ExportImageCompleted;

		public event GetImageCompletedEventHandler GetImageCompleted;

		public event GenerateServiceInfoCompletedEventHandler GenerateServiceInfoCompleted;

		public Temperature_ImageServer()
		{
			base.Url = "http://sampleserver3.arcgisonline.com/arcgis/services/World/Temperature/ImageServer";
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public ImageServerDownloadResult[] Download([XmlElement(Form = XmlSchemaForm.Unqualified)] FIDSet FIDs, [XmlElement(Form = XmlSchemaForm.Unqualified)] Geometry Geometry, [XmlElement(Form = XmlSchemaForm.Unqualified)] string Format)
		{
			object[] array = base.Invoke("Download", new object[]
			{
				FIDs,
				Geometry,
				Format
			});
			return (ImageServerDownloadResult[])array[0];
		}

		public IAsyncResult BeginDownload(FIDSet FIDs, Geometry Geometry, string Format, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Download", new object[]
			{
				FIDs,
				Geometry,
				Format
			}, callback, asyncState);
		}

		public ImageServerDownloadResult[] EndDownload(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ImageServerDownloadResult[])array[0];
		}

		public void DownloadAsync(FIDSet FIDs, Geometry Geometry, string Format)
		{
			this.DownloadAsync(FIDs, Geometry, Format, null);
		}

		public void DownloadAsync(FIDSet FIDs, Geometry Geometry, string Format, object userState)
		{
			if (this.DownloadOperationCompleted == null)
			{
				this.DownloadOperationCompleted = new SendOrPostCallback(this.OnDownloadOperationCompleted);
			}
			base.InvokeAsync("Download", new object[]
			{
				FIDs,
				Geometry,
				Format
			}, this.DownloadOperationCompleted, userState);
		}

		private void OnDownloadOperationCompleted(object arg)
		{
			if (this.DownloadCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DownloadCompleted(this, new DownloadCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public Fields GetFields()
		{
			object[] array = base.Invoke("GetFields", new object[0]);
			return (Fields)array[0];
		}

		public IAsyncResult BeginGetFields(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetFields", new object[0], callback, asyncState);
		}

		public Fields EndGetFields(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Fields)array[0];
		}

		public void GetFieldsAsync()
		{
			this.GetFieldsAsync(null);
		}

		public void GetFieldsAsync(object userState)
		{
			if (this.GetFieldsOperationCompleted == null)
			{
				this.GetFieldsOperationCompleted = new SendOrPostCallback(this.OnGetFieldsOperationCompleted);
			}
			base.InvokeAsync("GetFields", new object[0], this.GetFieldsOperationCompleted, userState);
		}

		private void OnGetFieldsOperationCompleted(object arg)
		{
			if (this.GetFieldsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetFieldsCompleted(this, new GetFieldsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public RasterInfo[] GetNativeRasterInfo([XmlElement(Form = XmlSchemaForm.Unqualified)] int RID)
		{
			object[] array = base.Invoke("GetNativeRasterInfo", new object[]
			{
				RID
			});
			return (RasterInfo[])array[0];
		}

		public IAsyncResult BeginGetNativeRasterInfo(int RID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetNativeRasterInfo", new object[]
			{
				RID
			}, callback, asyncState);
		}

		public RasterInfo[] EndGetNativeRasterInfo(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (RasterInfo[])array[0];
		}

		public void GetNativeRasterInfoAsync(int RID)
		{
			this.GetNativeRasterInfoAsync(RID, null);
		}

		public void GetNativeRasterInfoAsync(int RID, object userState)
		{
			if (this.GetNativeRasterInfoOperationCompleted == null)
			{
				this.GetNativeRasterInfoOperationCompleted = new SendOrPostCallback(this.OnGetNativeRasterInfoOperationCompleted);
			}
			base.InvokeAsync("GetNativeRasterInfo", new object[]
			{
				RID
			}, this.GetNativeRasterInfoOperationCompleted, userState);
		}

		private void OnGetNativeRasterInfoOperationCompleted(object arg)
		{
			if (this.GetNativeRasterInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetNativeRasterInfoCompleted(this, new GetNativeRasterInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public ImageResult GetThumbnail([XmlElement(Form = XmlSchemaForm.Unqualified)] int RID)
		{
			object[] array = base.Invoke("GetThumbnail", new object[]
			{
				RID
			});
			return (ImageResult)array[0];
		}

		public IAsyncResult BeginGetThumbnail(int RID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetThumbnail", new object[]
			{
				RID
			}, callback, asyncState);
		}

		public ImageResult EndGetThumbnail(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ImageResult)array[0];
		}

		public void GetThumbnailAsync(int RID)
		{
			this.GetThumbnailAsync(RID, null);
		}

		public void GetThumbnailAsync(int RID, object userState)
		{
			if (this.GetThumbnailOperationCompleted == null)
			{
				this.GetThumbnailOperationCompleted = new SendOrPostCallback(this.OnGetThumbnailOperationCompleted);
			}
			base.InvokeAsync("GetThumbnail", new object[]
			{
				RID
			}, this.GetThumbnailOperationCompleted, userState);
		}

		private void OnGetThumbnailOperationCompleted(object arg)
		{
			if (this.GetThumbnailCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetThumbnailCompleted(this, new GetThumbnailCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public string GetMetadata()
		{
			object[] array = base.Invoke("GetMetadata", new object[0]);
			return (string)array[0];
		}

		public IAsyncResult BeginGetMetadata(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetMetadata", new object[0], callback, asyncState);
		}

		public string EndGetMetadata(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (string)array[0];
		}

		public void GetMetadataAsync()
		{
			this.GetMetadataAsync(null);
		}

		public void GetMetadataAsync(object userState)
		{
			if (this.GetMetadataOperationCompleted == null)
			{
				this.GetMetadataOperationCompleted = new SendOrPostCallback(this.OnGetMetadataOperationCompleted);
			}
			base.InvokeAsync("GetMetadata", new object[0], this.GetMetadataOperationCompleted, userState);
		}

		private void OnGetMetadataOperationCompleted(object arg)
		{
			if (this.GetMetadataCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetMetadataCompleted(this, new GetMetadataCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified, DataType = "base64Binary")]
		public byte[] GetNativePixelBlock([XmlElement(Form = XmlSchemaForm.Unqualified)] int RID, [XmlElement(Form = XmlSchemaForm.Unqualified)] int IID, [XmlElement(Form = XmlSchemaForm.Unqualified)] int Tx, [XmlElement(Form = XmlSchemaForm.Unqualified)] int Ty, [XmlElement(Form = XmlSchemaForm.Unqualified)] int Level)
		{
			object[] array = base.Invoke("GetNativePixelBlock", new object[]
			{
				RID,
				IID,
				Tx,
				Ty,
				Level
			});
			return (byte[])array[0];
		}

		public IAsyncResult BeginGetNativePixelBlock(int RID, int IID, int Tx, int Ty, int Level, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetNativePixelBlock", new object[]
			{
				RID,
				IID,
				Tx,
				Ty,
				Level
			}, callback, asyncState);
		}

		public byte[] EndGetNativePixelBlock(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (byte[])array[0];
		}

		public void GetNativePixelBlockAsync(int RID, int IID, int Tx, int Ty, int Level)
		{
			this.GetNativePixelBlockAsync(RID, IID, Tx, Ty, Level, null);
		}

		public void GetNativePixelBlockAsync(int RID, int IID, int Tx, int Ty, int Level, object userState)
		{
			if (this.GetNativePixelBlockOperationCompleted == null)
			{
				this.GetNativePixelBlockOperationCompleted = new SendOrPostCallback(this.OnGetNativePixelBlockOperationCompleted);
			}
			base.InvokeAsync("GetNativePixelBlock", new object[]
			{
				RID,
				IID,
				Tx,
				Ty,
				Level
			}, this.GetNativePixelBlockOperationCompleted, userState);
		}

		private void OnGetNativePixelBlockOperationCompleted(object arg)
		{
			if (this.GetNativePixelBlockCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetNativePixelBlockCompleted(this, new GetNativePixelBlockCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public ImageServiceInfo GetServiceInfo()
		{
			object[] array = base.Invoke("GetServiceInfo", new object[0]);
			return (ImageServiceInfo)array[0];
		}

		public IAsyncResult BeginGetServiceInfo(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetServiceInfo", new object[0], callback, asyncState);
		}

		public ImageServiceInfo EndGetServiceInfo(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ImageServiceInfo)array[0];
		}

		public void GetServiceInfoAsync()
		{
			this.GetServiceInfoAsync(null);
		}

		public void GetServiceInfoAsync(object userState)
		{
			if (this.GetServiceInfoOperationCompleted == null)
			{
				this.GetServiceInfoOperationCompleted = new SendOrPostCallback(this.OnGetServiceInfoOperationCompleted);
			}
			base.InvokeAsync("GetServiceInfo", new object[0], this.GetServiceInfoOperationCompleted, userState);
		}

		private void OnGetServiceInfoOperationCompleted(object arg)
		{
			if (this.GetServiceInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetServiceInfoCompleted(this, new GetServiceInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public RecordSet GetCatalogItems([XmlElement(Form = XmlSchemaForm.Unqualified)] QueryFilter QueryFilter)
		{
			object[] array = base.Invoke("GetCatalogItems", new object[]
			{
				QueryFilter
			});
			return (RecordSet)array[0];
		}

		public IAsyncResult BeginGetCatalogItems(QueryFilter QueryFilter, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetCatalogItems", new object[]
			{
				QueryFilter
			}, callback, asyncState);
		}

		public RecordSet EndGetCatalogItems(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (RecordSet)array[0];
		}

		public void GetCatalogItemsAsync(QueryFilter QueryFilter)
		{
			this.GetCatalogItemsAsync(QueryFilter, null);
		}

		public void GetCatalogItemsAsync(QueryFilter QueryFilter, object userState)
		{
			if (this.GetCatalogItemsOperationCompleted == null)
			{
				this.GetCatalogItemsOperationCompleted = new SendOrPostCallback(this.OnGetCatalogItemsOperationCompleted);
			}
			base.InvokeAsync("GetCatalogItems", new object[]
			{
				QueryFilter
			}, this.GetCatalogItemsOperationCompleted, userState);
		}

		private void OnGetCatalogItemsOperationCompleted(object arg)
		{
			if (this.GetCatalogItemsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetCatalogItemsCompleted(this, new GetCatalogItemsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public decimal GetVersion()
		{
			object[] array = base.Invoke("GetVersion", new object[0]);
			return (decimal)array[0];
		}

		public IAsyncResult BeginGetVersion(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetVersion", new object[0], callback, asyncState);
		}

		public decimal EndGetVersion(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (decimal)array[0];
		}

		public void GetVersionAsync()
		{
			this.GetVersionAsync(null);
		}

		public void GetVersionAsync(object userState)
		{
			if (this.GetVersionOperationCompleted == null)
			{
				this.GetVersionOperationCompleted = new SendOrPostCallback(this.OnGetVersionOperationCompleted);
			}
			base.InvokeAsync("GetVersion", new object[0], this.GetVersionOperationCompleted, userState);
		}

		private void OnGetVersionOperationCompleted(object arg)
		{
			if (this.GetVersionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetVersionCompleted(this, new GetVersionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public MapImage ExportScaledImage([XmlElement(Form = XmlSchemaForm.Unqualified)] GeoImageDescription ImageDescription, [XmlElement(Form = XmlSchemaForm.Unqualified)] ImageType ImageType)
		{
			object[] array = base.Invoke("ExportScaledImage", new object[]
			{
				ImageDescription,
				ImageType
			});
			return (MapImage)array[0];
		}

		public IAsyncResult BeginExportScaledImage(GeoImageDescription ImageDescription, ImageType ImageType, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ExportScaledImage", new object[]
			{
				ImageDescription,
				ImageType
			}, callback, asyncState);
		}

		public MapImage EndExportScaledImage(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (MapImage)array[0];
		}

		public void ExportScaledImageAsync(GeoImageDescription ImageDescription, ImageType ImageType)
		{
			this.ExportScaledImageAsync(ImageDescription, ImageType, null);
		}

		public void ExportScaledImageAsync(GeoImageDescription ImageDescription, ImageType ImageType, object userState)
		{
			if (this.ExportScaledImageOperationCompleted == null)
			{
				this.ExportScaledImageOperationCompleted = new SendOrPostCallback(this.OnExportScaledImageOperationCompleted);
			}
			base.InvokeAsync("ExportScaledImage", new object[]
			{
				ImageDescription,
				ImageType
			}, this.ExportScaledImageOperationCompleted, userState);
		}

		private void OnExportScaledImageOperationCompleted(object arg)
		{
			if (this.ExportScaledImageCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ExportScaledImageCompleted(this, new ExportScaledImageCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public FIDSet GetCatalogItemIDs([XmlElement(Form = XmlSchemaForm.Unqualified)] QueryFilter QueryFilter)
		{
			object[] array = base.Invoke("GetCatalogItemIDs", new object[]
			{
				QueryFilter
			});
			return (FIDSet)array[0];
		}

		public IAsyncResult BeginGetCatalogItemIDs(QueryFilter QueryFilter, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetCatalogItemIDs", new object[]
			{
				QueryFilter
			}, callback, asyncState);
		}

		public FIDSet EndGetCatalogItemIDs(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (FIDSet)array[0];
		}

		public void GetCatalogItemIDsAsync(QueryFilter QueryFilter)
		{
			this.GetCatalogItemIDsAsync(QueryFilter, null);
		}

		public void GetCatalogItemIDsAsync(QueryFilter QueryFilter, object userState)
		{
			if (this.GetCatalogItemIDsOperationCompleted == null)
			{
				this.GetCatalogItemIDsOperationCompleted = new SendOrPostCallback(this.OnGetCatalogItemIDsOperationCompleted);
			}
			base.InvokeAsync("GetCatalogItemIDs", new object[]
			{
				QueryFilter
			}, this.GetCatalogItemIDsOperationCompleted, userState);
		}

		private void OnGetCatalogItemIDsOperationCompleted(object arg)
		{
			if (this.GetCatalogItemIDsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetCatalogItemIDsCompleted(this, new GetCatalogItemIDsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public string GetFile([XmlElement(Form = XmlSchemaForm.Unqualified)] ImageServerDownloadResult File)
		{
			object[] array = base.Invoke("GetFile", new object[]
			{
				File
			});
			return (string)array[0];
		}

		public IAsyncResult BeginGetFile(ImageServerDownloadResult File, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetFile", new object[]
			{
				File
			}, callback, asyncState);
		}

		public string EndGetFile(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (string)array[0];
		}

		public void GetFileAsync(ImageServerDownloadResult File)
		{
			this.GetFileAsync(File, null);
		}

		public void GetFileAsync(ImageServerDownloadResult File, object userState)
		{
			if (this.GetFileOperationCompleted == null)
			{
				this.GetFileOperationCompleted = new SendOrPostCallback(this.OnGetFileOperationCompleted);
			}
			base.InvokeAsync("GetFile", new object[]
			{
				File
			}, this.GetFileOperationCompleted, userState);
		}

		private void OnGetFileOperationCompleted(object arg)
		{
			if (this.GetFileCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetFileCompleted(this, new GetFileCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified, DataType = "base64Binary")]
		public byte[] GetPixelBlock([XmlElement(Form = XmlSchemaForm.Unqualified)] int RID, [XmlElement(Form = XmlSchemaForm.Unqualified)] int Tx, [XmlElement(Form = XmlSchemaForm.Unqualified)] int Ty, [XmlElement(Form = XmlSchemaForm.Unqualified)] int Level)
		{
			object[] array = base.Invoke("GetPixelBlock", new object[]
			{
				RID,
				Tx,
				Ty,
				Level
			});
			return (byte[])array[0];
		}

		public IAsyncResult BeginGetPixelBlock(int RID, int Tx, int Ty, int Level, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetPixelBlock", new object[]
			{
				RID,
				Tx,
				Ty,
				Level
			}, callback, asyncState);
		}

		public byte[] EndGetPixelBlock(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (byte[])array[0];
		}

		public void GetPixelBlockAsync(int RID, int Tx, int Ty, int Level)
		{
			this.GetPixelBlockAsync(RID, Tx, Ty, Level, null);
		}

		public void GetPixelBlockAsync(int RID, int Tx, int Ty, int Level, object userState)
		{
			if (this.GetPixelBlockOperationCompleted == null)
			{
				this.GetPixelBlockOperationCompleted = new SendOrPostCallback(this.OnGetPixelBlockOperationCompleted);
			}
			base.InvokeAsync("GetPixelBlock", new object[]
			{
				RID,
				Tx,
				Ty,
				Level
			}, this.GetPixelBlockOperationCompleted, userState);
		}

		private void OnGetPixelBlockOperationCompleted(object arg)
		{
			if (this.GetPixelBlockCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetPixelBlockCompleted(this, new GetPixelBlockCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public int GetCatalogItemCount([XmlElement(Form = XmlSchemaForm.Unqualified)] QueryFilter QueryFilter)
		{
			object[] array = base.Invoke("GetCatalogItemCount", new object[]
			{
				QueryFilter
			});
			return (int)array[0];
		}

		public IAsyncResult BeginGetCatalogItemCount(QueryFilter QueryFilter, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetCatalogItemCount", new object[]
			{
				QueryFilter
			}, callback, asyncState);
		}

		public int EndGetCatalogItemCount(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (int)array[0];
		}

		public void GetCatalogItemCountAsync(QueryFilter QueryFilter)
		{
			this.GetCatalogItemCountAsync(QueryFilter, null);
		}

		public void GetCatalogItemCountAsync(QueryFilter QueryFilter, object userState)
		{
			if (this.GetCatalogItemCountOperationCompleted == null)
			{
				this.GetCatalogItemCountOperationCompleted = new SendOrPostCallback(this.OnGetCatalogItemCountOperationCompleted);
			}
			base.InvokeAsync("GetCatalogItemCount", new object[]
			{
				QueryFilter
			}, this.GetCatalogItemCountOperationCompleted, userState);
		}

		private void OnGetCatalogItemCountOperationCompleted(object arg)
		{
			if (this.GetCatalogItemCountCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetCatalogItemCountCompleted(this, new GetCatalogItemCountCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public string GetRasterMetadata([XmlElement(Form = XmlSchemaForm.Unqualified)] int RID)
		{
			object[] array = base.Invoke("GetRasterMetadata", new object[]
			{
				RID
			});
			return (string)array[0];
		}

		public IAsyncResult BeginGetRasterMetadata(int RID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetRasterMetadata", new object[]
			{
				RID
			}, callback, asyncState);
		}

		public string EndGetRasterMetadata(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (string)array[0];
		}

		public void GetRasterMetadataAsync(int RID)
		{
			this.GetRasterMetadataAsync(RID, null);
		}

		public void GetRasterMetadataAsync(int RID, object userState)
		{
			if (this.GetRasterMetadataOperationCompleted == null)
			{
				this.GetRasterMetadataOperationCompleted = new SendOrPostCallback(this.OnGetRasterMetadataOperationCompleted);
			}
			base.InvokeAsync("GetRasterMetadata", new object[]
			{
				RID
			}, this.GetRasterMetadataOperationCompleted, userState);
		}

		private void OnGetRasterMetadataOperationCompleted(object arg)
		{
			if (this.GetRasterMetadataCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetRasterMetadataCompleted(this, new GetRasterMetadataCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public RasterInfo GetRasterInfo([XmlElement(Form = XmlSchemaForm.Unqualified)] int RID)
		{
			object[] array = base.Invoke("GetRasterInfo", new object[]
			{
				RID
			});
			return (RasterInfo)array[0];
		}

		public IAsyncResult BeginGetRasterInfo(int RID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetRasterInfo", new object[]
			{
				RID
			}, callback, asyncState);
		}

		public RasterInfo EndGetRasterInfo(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (RasterInfo)array[0];
		}

		public void GetRasterInfoAsync(int RID)
		{
			this.GetRasterInfoAsync(RID, null);
		}

		public void GetRasterInfoAsync(int RID, object userState)
		{
			if (this.GetRasterInfoOperationCompleted == null)
			{
				this.GetRasterInfoOperationCompleted = new SendOrPostCallback(this.OnGetRasterInfoOperationCompleted);
			}
			base.InvokeAsync("GetRasterInfo", new object[]
			{
				RID
			}, this.GetRasterInfoOperationCompleted, userState);
		}

		private void OnGetRasterInfoOperationCompleted(object arg)
		{
			if (this.GetRasterInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetRasterInfoCompleted(this, new GetRasterInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public ImageServerIdentifyResult Identify([XmlElement(Form = XmlSchemaForm.Unqualified)] Geometry Location, [XmlElement(Form = XmlSchemaForm.Unqualified)] MosaicRule MosaicRule, [XmlElement(Form = XmlSchemaForm.Unqualified)] Point PixelSize)
		{
			object[] array = base.Invoke("Identify", new object[]
			{
				Location,
				MosaicRule,
				PixelSize
			});
			return (ImageServerIdentifyResult)array[0];
		}

		public IAsyncResult BeginIdentify(Geometry Location, MosaicRule MosaicRule, Point PixelSize, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Identify", new object[]
			{
				Location,
				MosaicRule,
				PixelSize
			}, callback, asyncState);
		}

		public ImageServerIdentifyResult EndIdentify(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ImageServerIdentifyResult)array[0];
		}

		public void IdentifyAsync(Geometry Location, MosaicRule MosaicRule, Point PixelSize)
		{
			this.IdentifyAsync(Location, MosaicRule, PixelSize, null);
		}

		public void IdentifyAsync(Geometry Location, MosaicRule MosaicRule, Point PixelSize, object userState)
		{
			if (this.IdentifyOperationCompleted == null)
			{
				this.IdentifyOperationCompleted = new SendOrPostCallback(this.OnIdentifyOperationCompleted);
			}
			base.InvokeAsync("Identify", new object[]
			{
				Location,
				MosaicRule,
				PixelSize
			}, this.IdentifyOperationCompleted, userState);
		}

		private void OnIdentifyOperationCompleted(object arg)
		{
			if (this.IdentifyCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.IdentifyCompleted(this, new IdentifyCompletedEventArgs1(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public string Execute([XmlElement(Form = XmlSchemaForm.Unqualified)] AISRequest Request)
		{
			object[] array = base.Invoke("Execute", new object[]
			{
				Request
			});
			return (string)array[0];
		}

		public IAsyncResult BeginExecute(AISRequest Request, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Execute", new object[]
			{
				Request
			}, callback, asyncState);
		}

		public string EndExecute(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (string)array[0];
		}

		public void ExecuteAsync(AISRequest Request)
		{
			this.ExecuteAsync(Request, null);
		}

		public void ExecuteAsync(AISRequest Request, object userState)
		{
			if (this.ExecuteOperationCompleted == null)
			{
				this.ExecuteOperationCompleted = new SendOrPostCallback(this.OnExecuteOperationCompleted);
			}
			base.InvokeAsync("Execute", new object[]
			{
				Request
			}, this.ExecuteOperationCompleted, userState);
		}

		private void OnExecuteOperationCompleted(object arg)
		{
			if (this.ExecuteCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ExecuteCompleted(this, new ExecuteCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public ImageResult ExportImage([XmlElement(Form = XmlSchemaForm.Unqualified)] GeoImageDescription ImageDescription, [XmlElement(Form = XmlSchemaForm.Unqualified)] ImageType ImageType)
		{
			object[] array = base.Invoke("ExportImage", new object[]
			{
				ImageDescription,
				ImageType
			});
			return (ImageResult)array[0];
		}

		public IAsyncResult BeginExportImage(GeoImageDescription ImageDescription, ImageType ImageType, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ExportImage", new object[]
			{
				ImageDescription,
				ImageType
			}, callback, asyncState);
		}

		public ImageResult EndExportImage(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ImageResult)array[0];
		}

		public void ExportImageAsync(GeoImageDescription ImageDescription, ImageType ImageType)
		{
			this.ExportImageAsync(ImageDescription, ImageType, null);
		}

		public void ExportImageAsync(GeoImageDescription ImageDescription, ImageType ImageType, object userState)
		{
			if (this.ExportImageOperationCompleted == null)
			{
				this.ExportImageOperationCompleted = new SendOrPostCallback(this.OnExportImageOperationCompleted);
			}
			base.InvokeAsync("ExportImage", new object[]
			{
				ImageDescription,
				ImageType
			}, this.ExportImageOperationCompleted, userState);
		}

		private void OnExportImageOperationCompleted(object arg)
		{
			if (this.ExportImageCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ExportImageCompleted(this, new ExportImageCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified, DataType = "base64Binary")]
		public byte[] GetImage([XmlElement(Form = XmlSchemaForm.Unqualified)] GeoImageDescription ImageDescription)
		{
			object[] array = base.Invoke("GetImage", new object[]
			{
				ImageDescription
			});
			return (byte[])array[0];
		}

		public IAsyncResult BeginGetImage(GeoImageDescription ImageDescription, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetImage", new object[]
			{
				ImageDescription
			}, callback, asyncState);
		}

		public byte[] EndGetImage(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (byte[])array[0];
		}

		public void GetImageAsync(GeoImageDescription ImageDescription)
		{
			this.GetImageAsync(ImageDescription, null);
		}

		public void GetImageAsync(GeoImageDescription ImageDescription, object userState)
		{
			if (this.GetImageOperationCompleted == null)
			{
				this.GetImageOperationCompleted = new SendOrPostCallback(this.OnGetImageOperationCompleted);
			}
			base.InvokeAsync("GetImage", new object[]
			{
				ImageDescription
			}, this.GetImageOperationCompleted, userState);
		}

		private void OnGetImageOperationCompleted(object arg)
		{
			if (this.GetImageCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetImageCompleted(this, new GetImageCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public ImageServiceInfo GenerateServiceInfo([XmlElement(Form = XmlSchemaForm.Unqualified)] RenderingRule RenderingRule)
		{
			object[] array = base.Invoke("GenerateServiceInfo", new object[]
			{
				RenderingRule
			});
			return (ImageServiceInfo)array[0];
		}

		public IAsyncResult BeginGenerateServiceInfo(RenderingRule RenderingRule, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GenerateServiceInfo", new object[]
			{
				RenderingRule
			}, callback, asyncState);
		}

		public ImageServiceInfo EndGenerateServiceInfo(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ImageServiceInfo)array[0];
		}

		public void GenerateServiceInfoAsync(RenderingRule RenderingRule)
		{
			this.GenerateServiceInfoAsync(RenderingRule, null);
		}

		public void GenerateServiceInfoAsync(RenderingRule RenderingRule, object userState)
		{
			if (this.GenerateServiceInfoOperationCompleted == null)
			{
				this.GenerateServiceInfoOperationCompleted = new SendOrPostCallback(this.OnGenerateServiceInfoOperationCompleted);
			}
			base.InvokeAsync("GenerateServiceInfo", new object[]
			{
				RenderingRule
			}, this.GenerateServiceInfoOperationCompleted, userState);
		}

		private void OnGenerateServiceInfoOperationCompleted(object arg)
		{
			if (this.GenerateServiceInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GenerateServiceInfoCompleted(this, new GenerateServiceInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}
	}
}
