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
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, WebServiceBinding(Name = "MapServerBinding", Namespace = "http://www.esri.com/schemas/ArcGIS/10.0"), XmlInclude(typeof(MapServerForceDeriveFromAnyType)), XmlInclude(typeof(Patch)), XmlInclude(typeof(MapTableInfo)), XmlInclude(typeof(Element)), XmlInclude(typeof(PropertySetProperty[]))]
	public class MapServerProxy : SoapHttpClientProtocol
	{
		private SendOrPostCallback GetDocumentInfoOperationCompleted;

		private SendOrPostCallback GetTileImageInfoOperationCompleted;

		private SendOrPostCallback QueryFeatureData2OperationCompleted;

		private SendOrPostCallback QueryAttachmentInfosOperationCompleted;

		private SendOrPostCallback GetCacheControlInfoOperationCompleted;

		private SendOrPostCallback GetDefaultLayerDrawingDescriptionsOperationCompleted;

		private SendOrPostCallback QueryAttachmentDataOperationCompleted;

		private SendOrPostCallback GetSQLSyntaxInfoOperationCompleted;

		private SendOrPostCallback ExportScaleBarOperationCompleted;

		private SendOrPostCallback GetVirtualCacheDirectoryOperationCompleted;

		private SendOrPostCallback QueryRowCountOperationCompleted;

		private SendOrPostCallback GetSupportedImageReturnTypesOperationCompleted;

		private SendOrPostCallback QueryFeatureIDsOperationCompleted;

		private SendOrPostCallback QueryDataOperationCompleted;

		private SendOrPostCallback QueryHTMLPopupsOperationCompleted;

		private SendOrPostCallback GetCacheStorageInfoOperationCompleted;

		private SendOrPostCallback ToMapPointsOperationCompleted;

		private SendOrPostCallback GetMapNameOperationCompleted;

		private SendOrPostCallback GetTileCacheInfoOperationCompleted;

		private SendOrPostCallback QueryRelatedRecordsOperationCompleted;

		private SendOrPostCallback QueryFeatureDataOperationCompleted;

		private SendOrPostCallback QueryHyperlinksOperationCompleted;

		private SendOrPostCallback HasLayerCacheOperationCompleted;

		private SendOrPostCallback QueryFeatureIDs2OperationCompleted;

		private SendOrPostCallback GetLayerTileOperationCompleted;

		private SendOrPostCallback GetServiceConfigurationInfoOperationCompleted;

		private SendOrPostCallback QueryFeatureCount2OperationCompleted;

		private SendOrPostCallback GetCacheDescriptionInfoOperationCompleted;

		private SendOrPostCallback IdentifyOperationCompleted;

		private SendOrPostCallback ComputeDistanceOperationCompleted;

		private SendOrPostCallback GetDefaultMapNameOperationCompleted;

		private SendOrPostCallback IsFixedScaleMapOperationCompleted;

		private SendOrPostCallback GetLegendInfoOperationCompleted;

		private SendOrPostCallback GetMapCountOperationCompleted;

		private SendOrPostCallback HasSingleFusedMapCacheOperationCompleted;

		private SendOrPostCallback GetMapTileOperationCompleted;

		private SendOrPostCallback GetServerInfoOperationCompleted;

		private SendOrPostCallback GetMapTableSubtypeInfosOperationCompleted;

		private SendOrPostCallback QueryFeatureCountOperationCompleted;

		private SendOrPostCallback QueryRasterValueOperationCompleted;

		private SendOrPostCallback FromMapPointsOperationCompleted;

		private SendOrPostCallback QueryRowIDsOperationCompleted;

		private SendOrPostCallback GetCacheNameOperationCompleted;

		private SendOrPostCallback ComputeScaleOperationCompleted;

		private SendOrPostCallback FindOperationCompleted;

		private SendOrPostCallback ExportMapImageOperationCompleted;

		public event GetDocumentInfoCompletedEventHandler GetDocumentInfoCompleted;

		public event GetTileImageInfoCompletedEventHandler GetTileImageInfoCompleted;

		public event QueryFeatureData2CompletedEventHandler QueryFeatureData2Completed;

		public event QueryAttachmentInfosCompletedEventHandler QueryAttachmentInfosCompleted;

		public event GetCacheControlInfoCompletedEventHandler GetCacheControlInfoCompleted;

		public event GetDefaultLayerDrawingDescriptionsCompletedEventHandler GetDefaultLayerDrawingDescriptionsCompleted;

		public event QueryAttachmentDataCompletedEventHandler QueryAttachmentDataCompleted;

		public event GetSQLSyntaxInfoCompletedEventHandler GetSQLSyntaxInfoCompleted;

		public event ExportScaleBarCompletedEventHandler ExportScaleBarCompleted;

		public event GetVirtualCacheDirectoryCompletedEventHandler GetVirtualCacheDirectoryCompleted;

		public event QueryRowCountCompletedEventHandler QueryRowCountCompleted;

		public event GetSupportedImageReturnTypesCompletedEventHandler GetSupportedImageReturnTypesCompleted;

		public event QueryFeatureIDsCompletedEventHandler QueryFeatureIDsCompleted;

		public event QueryDataCompletedEventHandler QueryDataCompleted;

		public event QueryHTMLPopupsCompletedEventHandler QueryHTMLPopupsCompleted;

		public event GetCacheStorageInfoCompletedEventHandler GetCacheStorageInfoCompleted;

		public event ToMapPointsCompletedEventHandler ToMapPointsCompleted;

		public event GetMapNameCompletedEventHandler GetMapNameCompleted;

		public event GetTileCacheInfoCompletedEventHandler GetTileCacheInfoCompleted;

		public event QueryRelatedRecordsCompletedEventHandler QueryRelatedRecordsCompleted;

		public event QueryFeatureDataCompletedEventHandler QueryFeatureDataCompleted;

		public event QueryHyperlinksCompletedEventHandler QueryHyperlinksCompleted;

		public event HasLayerCacheCompletedEventHandler HasLayerCacheCompleted;

		public event QueryFeatureIDs2CompletedEventHandler QueryFeatureIDs2Completed;

		public event GetLayerTileCompletedEventHandler GetLayerTileCompleted;

		public event GetServiceConfigurationInfoCompletedEventHandler GetServiceConfigurationInfoCompleted;

		public event QueryFeatureCount2CompletedEventHandler QueryFeatureCount2Completed;

		public event GetCacheDescriptionInfoCompletedEventHandler GetCacheDescriptionInfoCompleted;

		public event IdentifyCompletedEventHandler IdentifyCompleted;

		public event ComputeDistanceCompletedEventHandler ComputeDistanceCompleted;

		public event GetDefaultMapNameCompletedEventHandler GetDefaultMapNameCompleted;

		public event IsFixedScaleMapCompletedEventHandler IsFixedScaleMapCompleted;

		public event GetLegendInfoCompletedEventHandler GetLegendInfoCompleted;

		public event GetMapCountCompletedEventHandler GetMapCountCompleted;

		public event HasSingleFusedMapCacheCompletedEventHandler HasSingleFusedMapCacheCompleted;

		public event GetMapTileCompletedEventHandler GetMapTileCompleted;

		public event GetServerInfoCompletedEventHandler GetServerInfoCompleted;

		public event GetMapTableSubtypeInfosCompletedEventHandler GetMapTableSubtypeInfosCompleted;

		public event QueryFeatureCountCompletedEventHandler QueryFeatureCountCompleted;

		public event QueryRasterValueCompletedEventHandler QueryRasterValueCompleted;

		public event FromMapPointsCompletedEventHandler FromMapPointsCompleted;

		public event QueryRowIDsCompletedEventHandler QueryRowIDsCompleted;

		public event GetCacheNameCompletedEventHandler GetCacheNameCompleted;

		public event ComputeScaleCompletedEventHandler ComputeScaleCompleted;

		public event FindCompletedEventHandler FindCompleted;

		public event ExportMapImageCompletedEventHandler ExportMapImageCompleted;

		public MapServerProxy()
		{
			base.Url = "http://sampleserver3.arcgisonline.com/arcgis/services/Fire/Sheep/MapServer";
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public PropertySet GetDocumentInfo()
		{
			object[] array = base.Invoke("GetDocumentInfo", new object[0]);
			return (PropertySet)array[0];
		}

		public IAsyncResult BeginGetDocumentInfo(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetDocumentInfo", new object[0], callback, asyncState);
		}

		public PropertySet EndGetDocumentInfo(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (PropertySet)array[0];
		}

		public void GetDocumentInfoAsync()
		{
			this.GetDocumentInfoAsync(null);
		}

		public void GetDocumentInfoAsync(object userState)
		{
			if (this.GetDocumentInfoOperationCompleted == null)
			{
				this.GetDocumentInfoOperationCompleted = new SendOrPostCallback(this.OnGetDocumentInfoOperationCompleted);
			}
			base.InvokeAsync("GetDocumentInfo", new object[0], this.GetDocumentInfoOperationCompleted, userState);
		}

		private void OnGetDocumentInfoOperationCompleted(object arg)
		{
			if (this.GetDocumentInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetDocumentInfoCompleted(this, new GetDocumentInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public TileImageInfo GetTileImageInfo([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName)
		{
			object[] array = base.Invoke("GetTileImageInfo", new object[]
			{
				MapName
			});
			return (TileImageInfo)array[0];
		}

		public IAsyncResult BeginGetTileImageInfo(string MapName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetTileImageInfo", new object[]
			{
				MapName
			}, callback, asyncState);
		}

		public TileImageInfo EndGetTileImageInfo(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (TileImageInfo)array[0];
		}

		public void GetTileImageInfoAsync(string MapName)
		{
			this.GetTileImageInfoAsync(MapName, null);
		}

		public void GetTileImageInfoAsync(string MapName, object userState)
		{
			if (this.GetTileImageInfoOperationCompleted == null)
			{
				this.GetTileImageInfoOperationCompleted = new SendOrPostCallback(this.OnGetTileImageInfoOperationCompleted);
			}
			base.InvokeAsync("GetTileImageInfo", new object[]
			{
				MapName
			}, this.GetTileImageInfoOperationCompleted, userState);
		}

		private void OnGetTileImageInfoOperationCompleted(object arg)
		{
			if (this.GetTileImageInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetTileImageInfoCompleted(this, new GetTileImageInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public QueryResult QueryFeatureData2([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName, [XmlElement(Form = XmlSchemaForm.Unqualified)] LayerDescription LayerDescription, [XmlElement(Form = XmlSchemaForm.Unqualified)] QueryFilter QueryFilter, [XmlElement(Form = XmlSchemaForm.Unqualified)] QueryResultOptions QueryResultOptions)
		{
			object[] array = base.Invoke("QueryFeatureData2", new object[]
			{
				MapName,
				LayerDescription,
				QueryFilter,
				QueryResultOptions
			});
			return (QueryResult)array[0];
		}

		public IAsyncResult BeginQueryFeatureData2(string MapName, LayerDescription LayerDescription, QueryFilter QueryFilter, QueryResultOptions QueryResultOptions, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("QueryFeatureData2", new object[]
			{
				MapName,
				LayerDescription,
				QueryFilter,
				QueryResultOptions
			}, callback, asyncState);
		}

		public QueryResult EndQueryFeatureData2(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (QueryResult)array[0];
		}

		public void QueryFeatureData2Async(string MapName, LayerDescription LayerDescription, QueryFilter QueryFilter, QueryResultOptions QueryResultOptions)
		{
			this.QueryFeatureData2Async(MapName, LayerDescription, QueryFilter, QueryResultOptions, null);
		}

		public void QueryFeatureData2Async(string MapName, LayerDescription LayerDescription, QueryFilter QueryFilter, QueryResultOptions QueryResultOptions, object userState)
		{
			if (this.QueryFeatureData2OperationCompleted == null)
			{
				this.QueryFeatureData2OperationCompleted = new SendOrPostCallback(this.OnQueryFeatureData2OperationCompleted);
			}
			base.InvokeAsync("QueryFeatureData2", new object[]
			{
				MapName,
				LayerDescription,
				QueryFilter,
				QueryResultOptions
			}, this.QueryFeatureData2OperationCompleted, userState);
		}

		private void OnQueryFeatureData2OperationCompleted(object arg)
		{
			if (this.QueryFeatureData2Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.QueryFeatureData2Completed(this, new QueryFeatureData2CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public AttachmentInfo[] QueryAttachmentInfos([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName, [XmlElement(Form = XmlSchemaForm.Unqualified)] int TableID, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)] int[] RowIDs)
		{
			object[] array = base.Invoke("QueryAttachmentInfos", new object[]
			{
				MapName,
				TableID,
				RowIDs
			});
			return (AttachmentInfo[])array[0];
		}

		public IAsyncResult BeginQueryAttachmentInfos(string MapName, int TableID, int[] RowIDs, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("QueryAttachmentInfos", new object[]
			{
				MapName,
				TableID,
				RowIDs
			}, callback, asyncState);
		}

		public AttachmentInfo[] EndQueryAttachmentInfos(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (AttachmentInfo[])array[0];
		}

		public void QueryAttachmentInfosAsync(string MapName, int TableID, int[] RowIDs)
		{
			this.QueryAttachmentInfosAsync(MapName, TableID, RowIDs, null);
		}

		public void QueryAttachmentInfosAsync(string MapName, int TableID, int[] RowIDs, object userState)
		{
			if (this.QueryAttachmentInfosOperationCompleted == null)
			{
				this.QueryAttachmentInfosOperationCompleted = new SendOrPostCallback(this.OnQueryAttachmentInfosOperationCompleted);
			}
			base.InvokeAsync("QueryAttachmentInfos", new object[]
			{
				MapName,
				TableID,
				RowIDs
			}, this.QueryAttachmentInfosOperationCompleted, userState);
		}

		private void OnQueryAttachmentInfosOperationCompleted(object arg)
		{
			if (this.QueryAttachmentInfosCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.QueryAttachmentInfosCompleted(this, new QueryAttachmentInfosCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public CacheControlInfo GetCacheControlInfo([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName)
		{
			object[] array = base.Invoke("GetCacheControlInfo", new object[]
			{
				MapName
			});
			return (CacheControlInfo)array[0];
		}

		public IAsyncResult BeginGetCacheControlInfo(string MapName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetCacheControlInfo", new object[]
			{
				MapName
			}, callback, asyncState);
		}

		public CacheControlInfo EndGetCacheControlInfo(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (CacheControlInfo)array[0];
		}

		public void GetCacheControlInfoAsync(string MapName)
		{
			this.GetCacheControlInfoAsync(MapName, null);
		}

		public void GetCacheControlInfoAsync(string MapName, object userState)
		{
			if (this.GetCacheControlInfoOperationCompleted == null)
			{
				this.GetCacheControlInfoOperationCompleted = new SendOrPostCallback(this.OnGetCacheControlInfoOperationCompleted);
			}
			base.InvokeAsync("GetCacheControlInfo", new object[]
			{
				MapName
			}, this.GetCacheControlInfoOperationCompleted, userState);
		}

		private void OnGetCacheControlInfoOperationCompleted(object arg)
		{
			if (this.GetCacheControlInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetCacheControlInfoCompleted(this, new GetCacheControlInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public LayerDrawingDescription[] GetDefaultLayerDrawingDescriptions([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)] int[] LayerIDs, [XmlElement(Form = XmlSchemaForm.Unqualified)] ServerSymbolOutputOptions SymbolOutputOptions)
		{
			object[] array = base.Invoke("GetDefaultLayerDrawingDescriptions", new object[]
			{
				MapName,
				LayerIDs,
				SymbolOutputOptions
			});
			return (LayerDrawingDescription[])array[0];
		}

		public IAsyncResult BeginGetDefaultLayerDrawingDescriptions(string MapName, int[] LayerIDs, ServerSymbolOutputOptions SymbolOutputOptions, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetDefaultLayerDrawingDescriptions", new object[]
			{
				MapName,
				LayerIDs,
				SymbolOutputOptions
			}, callback, asyncState);
		}

		public LayerDrawingDescription[] EndGetDefaultLayerDrawingDescriptions(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (LayerDrawingDescription[])array[0];
		}

		public void GetDefaultLayerDrawingDescriptionsAsync(string MapName, int[] LayerIDs, ServerSymbolOutputOptions SymbolOutputOptions)
		{
			this.GetDefaultLayerDrawingDescriptionsAsync(MapName, LayerIDs, SymbolOutputOptions, null);
		}

		public void GetDefaultLayerDrawingDescriptionsAsync(string MapName, int[] LayerIDs, ServerSymbolOutputOptions SymbolOutputOptions, object userState)
		{
			if (this.GetDefaultLayerDrawingDescriptionsOperationCompleted == null)
			{
				this.GetDefaultLayerDrawingDescriptionsOperationCompleted = new SendOrPostCallback(this.OnGetDefaultLayerDrawingDescriptionsOperationCompleted);
			}
			base.InvokeAsync("GetDefaultLayerDrawingDescriptions", new object[]
			{
				MapName,
				LayerIDs,
				SymbolOutputOptions
			}, this.GetDefaultLayerDrawingDescriptionsOperationCompleted, userState);
		}

		private void OnGetDefaultLayerDrawingDescriptionsOperationCompleted(object arg)
		{
			if (this.GetDefaultLayerDrawingDescriptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetDefaultLayerDrawingDescriptionsCompleted(this, new GetDefaultLayerDrawingDescriptionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public AttachmentData[] QueryAttachmentData([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName, [XmlElement(Form = XmlSchemaForm.Unqualified)] int TableID, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)] int[] AttachmentIDs, [XmlElement(Form = XmlSchemaForm.Unqualified)] esriTransportType TransportType)
		{
			object[] array = base.Invoke("QueryAttachmentData", new object[]
			{
				MapName,
				TableID,
				AttachmentIDs,
				TransportType
			});
			return (AttachmentData[])array[0];
		}

		public IAsyncResult BeginQueryAttachmentData(string MapName, int TableID, int[] AttachmentIDs, esriTransportType TransportType, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("QueryAttachmentData", new object[]
			{
				MapName,
				TableID,
				AttachmentIDs,
				TransportType
			}, callback, asyncState);
		}

		public AttachmentData[] EndQueryAttachmentData(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (AttachmentData[])array[0];
		}

		public void QueryAttachmentDataAsync(string MapName, int TableID, int[] AttachmentIDs, esriTransportType TransportType)
		{
			this.QueryAttachmentDataAsync(MapName, TableID, AttachmentIDs, TransportType, null);
		}

		public void QueryAttachmentDataAsync(string MapName, int TableID, int[] AttachmentIDs, esriTransportType TransportType, object userState)
		{
			if (this.QueryAttachmentDataOperationCompleted == null)
			{
				this.QueryAttachmentDataOperationCompleted = new SendOrPostCallback(this.OnQueryAttachmentDataOperationCompleted);
			}
			base.InvokeAsync("QueryAttachmentData", new object[]
			{
				MapName,
				TableID,
				AttachmentIDs,
				TransportType
			}, this.QueryAttachmentDataOperationCompleted, userState);
		}

		private void OnQueryAttachmentDataOperationCompleted(object arg)
		{
			if (this.QueryAttachmentDataCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.QueryAttachmentDataCompleted(this, new QueryAttachmentDataCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public SQLSyntaxInfo GetSQLSyntaxInfo([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName, [XmlElement(Form = XmlSchemaForm.Unqualified)] int LayerID)
		{
			object[] array = base.Invoke("GetSQLSyntaxInfo", new object[]
			{
				MapName,
				LayerID
			});
			return (SQLSyntaxInfo)array[0];
		}

		public IAsyncResult BeginGetSQLSyntaxInfo(string MapName, int LayerID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetSQLSyntaxInfo", new object[]
			{
				MapName,
				LayerID
			}, callback, asyncState);
		}

		public SQLSyntaxInfo EndGetSQLSyntaxInfo(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (SQLSyntaxInfo)array[0];
		}

		public void GetSQLSyntaxInfoAsync(string MapName, int LayerID)
		{
			this.GetSQLSyntaxInfoAsync(MapName, LayerID, null);
		}

		public void GetSQLSyntaxInfoAsync(string MapName, int LayerID, object userState)
		{
			if (this.GetSQLSyntaxInfoOperationCompleted == null)
			{
				this.GetSQLSyntaxInfoOperationCompleted = new SendOrPostCallback(this.OnGetSQLSyntaxInfoOperationCompleted);
			}
			base.InvokeAsync("GetSQLSyntaxInfo", new object[]
			{
				MapName,
				LayerID
			}, this.GetSQLSyntaxInfoOperationCompleted, userState);
		}

		private void OnGetSQLSyntaxInfoOperationCompleted(object arg)
		{
			if (this.GetSQLSyntaxInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetSQLSyntaxInfoCompleted(this, new GetSQLSyntaxInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public ImageResult ExportScaleBar([XmlElement(Form = XmlSchemaForm.Unqualified)] ScaleBar ScaleBar, [XmlElement(Form = XmlSchemaForm.Unqualified)] MapDescription MapDescription, [XmlElement(Form = XmlSchemaForm.Unqualified)] ImageDisplay MapDisplay, [XmlElement(Form = XmlSchemaForm.Unqualified)] Color BackGroundColor, [XmlElement(Form = XmlSchemaForm.Unqualified)] ImageDescription ImageDescription)
		{
			object[] array = base.Invoke("ExportScaleBar", new object[]
			{
				ScaleBar,
				MapDescription,
				MapDisplay,
				BackGroundColor,
				ImageDescription
			});
			return (ImageResult)array[0];
		}

		public IAsyncResult BeginExportScaleBar(ScaleBar ScaleBar, MapDescription MapDescription, ImageDisplay MapDisplay, Color BackGroundColor, ImageDescription ImageDescription, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ExportScaleBar", new object[]
			{
				ScaleBar,
				MapDescription,
				MapDisplay,
				BackGroundColor,
				ImageDescription
			}, callback, asyncState);
		}

		public ImageResult EndExportScaleBar(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ImageResult)array[0];
		}

		public void ExportScaleBarAsync(ScaleBar ScaleBar, MapDescription MapDescription, ImageDisplay MapDisplay, Color BackGroundColor, ImageDescription ImageDescription)
		{
			this.ExportScaleBarAsync(ScaleBar, MapDescription, MapDisplay, BackGroundColor, ImageDescription, null);
		}

		public void ExportScaleBarAsync(ScaleBar ScaleBar, MapDescription MapDescription, ImageDisplay MapDisplay, Color BackGroundColor, ImageDescription ImageDescription, object userState)
		{
			if (this.ExportScaleBarOperationCompleted == null)
			{
				this.ExportScaleBarOperationCompleted = new SendOrPostCallback(this.OnExportScaleBarOperationCompleted);
			}
			base.InvokeAsync("ExportScaleBar", new object[]
			{
				ScaleBar,
				MapDescription,
				MapDisplay,
				BackGroundColor,
				ImageDescription
			}, this.ExportScaleBarOperationCompleted, userState);
		}

		private void OnExportScaleBarOperationCompleted(object arg)
		{
			if (this.ExportScaleBarCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ExportScaleBarCompleted(this, new ExportScaleBarCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public string GetVirtualCacheDirectory([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName, [XmlElement(Form = XmlSchemaForm.Unqualified)] int LayerID)
		{
			object[] array = base.Invoke("GetVirtualCacheDirectory", new object[]
			{
				MapName,
				LayerID
			});
			return (string)array[0];
		}

		public IAsyncResult BeginGetVirtualCacheDirectory(string MapName, int LayerID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetVirtualCacheDirectory", new object[]
			{
				MapName,
				LayerID
			}, callback, asyncState);
		}

		public string EndGetVirtualCacheDirectory(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (string)array[0];
		}

		public void GetVirtualCacheDirectoryAsync(string MapName, int LayerID)
		{
			this.GetVirtualCacheDirectoryAsync(MapName, LayerID, null);
		}

		public void GetVirtualCacheDirectoryAsync(string MapName, int LayerID, object userState)
		{
			if (this.GetVirtualCacheDirectoryOperationCompleted == null)
			{
				this.GetVirtualCacheDirectoryOperationCompleted = new SendOrPostCallback(this.OnGetVirtualCacheDirectoryOperationCompleted);
			}
			base.InvokeAsync("GetVirtualCacheDirectory", new object[]
			{
				MapName,
				LayerID
			}, this.GetVirtualCacheDirectoryOperationCompleted, userState);
		}

		private void OnGetVirtualCacheDirectoryOperationCompleted(object arg)
		{
			if (this.GetVirtualCacheDirectoryCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetVirtualCacheDirectoryCompleted(this, new GetVirtualCacheDirectoryCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public int QueryRowCount([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName, [XmlElement(Form = XmlSchemaForm.Unqualified)] MapTableDescription MapTableDescription, [XmlElement(Form = XmlSchemaForm.Unqualified)] QueryFilter QueryFilter)
		{
			object[] array = base.Invoke("QueryRowCount", new object[]
			{
				MapName,
				MapTableDescription,
				QueryFilter
			});
			return (int)array[0];
		}

		public IAsyncResult BeginQueryRowCount(string MapName, MapTableDescription MapTableDescription, QueryFilter QueryFilter, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("QueryRowCount", new object[]
			{
				MapName,
				MapTableDescription,
				QueryFilter
			}, callback, asyncState);
		}

		public int EndQueryRowCount(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (int)array[0];
		}

		public void QueryRowCountAsync(string MapName, MapTableDescription MapTableDescription, QueryFilter QueryFilter)
		{
			this.QueryRowCountAsync(MapName, MapTableDescription, QueryFilter, null);
		}

		public void QueryRowCountAsync(string MapName, MapTableDescription MapTableDescription, QueryFilter QueryFilter, object userState)
		{
			if (this.QueryRowCountOperationCompleted == null)
			{
				this.QueryRowCountOperationCompleted = new SendOrPostCallback(this.OnQueryRowCountOperationCompleted);
			}
			base.InvokeAsync("QueryRowCount", new object[]
			{
				MapName,
				MapTableDescription,
				QueryFilter
			}, this.QueryRowCountOperationCompleted, userState);
		}

		private void OnQueryRowCountOperationCompleted(object arg)
		{
			if (this.QueryRowCountCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.QueryRowCountCompleted(this, new QueryRowCountCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public esriImageReturnType GetSupportedImageReturnTypes()
		{
			object[] array = base.Invoke("GetSupportedImageReturnTypes", new object[0]);
			return (esriImageReturnType)array[0];
		}

		public IAsyncResult BeginGetSupportedImageReturnTypes(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetSupportedImageReturnTypes", new object[0], callback, asyncState);
		}

		public esriImageReturnType EndGetSupportedImageReturnTypes(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (esriImageReturnType)array[0];
		}

		public void GetSupportedImageReturnTypesAsync()
		{
			this.GetSupportedImageReturnTypesAsync(null);
		}

		public void GetSupportedImageReturnTypesAsync(object userState)
		{
			if (this.GetSupportedImageReturnTypesOperationCompleted == null)
			{
				this.GetSupportedImageReturnTypesOperationCompleted = new SendOrPostCallback(this.OnGetSupportedImageReturnTypesOperationCompleted);
			}
			base.InvokeAsync("GetSupportedImageReturnTypes", new object[0], this.GetSupportedImageReturnTypesOperationCompleted, userState);
		}

		private void OnGetSupportedImageReturnTypesOperationCompleted(object arg)
		{
			if (this.GetSupportedImageReturnTypesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetSupportedImageReturnTypesCompleted(this, new GetSupportedImageReturnTypesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public FIDSet QueryFeatureIDs([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName, [XmlElement(Form = XmlSchemaForm.Unqualified)] int LayerID, [XmlElement(Form = XmlSchemaForm.Unqualified)] QueryFilter QueryFilter)
		{
			object[] array = base.Invoke("QueryFeatureIDs", new object[]
			{
				MapName,
				LayerID,
				QueryFilter
			});
			return (FIDSet)array[0];
		}

		public IAsyncResult BeginQueryFeatureIDs(string MapName, int LayerID, QueryFilter QueryFilter, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("QueryFeatureIDs", new object[]
			{
				MapName,
				LayerID,
				QueryFilter
			}, callback, asyncState);
		}

		public FIDSet EndQueryFeatureIDs(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (FIDSet)array[0];
		}

		public void QueryFeatureIDsAsync(string MapName, int LayerID, QueryFilter QueryFilter)
		{
			this.QueryFeatureIDsAsync(MapName, LayerID, QueryFilter, null);
		}

		public void QueryFeatureIDsAsync(string MapName, int LayerID, QueryFilter QueryFilter, object userState)
		{
			if (this.QueryFeatureIDsOperationCompleted == null)
			{
				this.QueryFeatureIDsOperationCompleted = new SendOrPostCallback(this.OnQueryFeatureIDsOperationCompleted);
			}
			base.InvokeAsync("QueryFeatureIDs", new object[]
			{
				MapName,
				LayerID,
				QueryFilter
			}, this.QueryFeatureIDsOperationCompleted, userState);
		}

		private void OnQueryFeatureIDsOperationCompleted(object arg)
		{
			if (this.QueryFeatureIDsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.QueryFeatureIDsCompleted(this, new QueryFeatureIDsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public QueryResult QueryData([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName, [XmlElement(Form = XmlSchemaForm.Unqualified)] MapTableDescription MapTableDescription, [XmlElement(Form = XmlSchemaForm.Unqualified)] QueryFilter QueryFilter, [XmlElement(Form = XmlSchemaForm.Unqualified)] QueryResultOptions QueryResultOptions)
		{
			object[] array = base.Invoke("QueryData", new object[]
			{
				MapName,
				MapTableDescription,
				QueryFilter,
				QueryResultOptions
			});
			return (QueryResult)array[0];
		}

		public IAsyncResult BeginQueryData(string MapName, MapTableDescription MapTableDescription, QueryFilter QueryFilter, QueryResultOptions QueryResultOptions, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("QueryData", new object[]
			{
				MapName,
				MapTableDescription,
				QueryFilter,
				QueryResultOptions
			}, callback, asyncState);
		}

		public QueryResult EndQueryData(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (QueryResult)array[0];
		}

		public void QueryDataAsync(string MapName, MapTableDescription MapTableDescription, QueryFilter QueryFilter, QueryResultOptions QueryResultOptions)
		{
			this.QueryDataAsync(MapName, MapTableDescription, QueryFilter, QueryResultOptions, null);
		}

		public void QueryDataAsync(string MapName, MapTableDescription MapTableDescription, QueryFilter QueryFilter, QueryResultOptions QueryResultOptions, object userState)
		{
			if (this.QueryDataOperationCompleted == null)
			{
				this.QueryDataOperationCompleted = new SendOrPostCallback(this.OnQueryDataOperationCompleted);
			}
			base.InvokeAsync("QueryData", new object[]
			{
				MapName,
				MapTableDescription,
				QueryFilter,
				QueryResultOptions
			}, this.QueryDataOperationCompleted, userState);
		}

		private void OnQueryDataOperationCompleted(object arg)
		{
			if (this.QueryDataCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.QueryDataCompleted(this, new QueryDataCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem("String", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public string[] QueryHTMLPopups([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName, [XmlElement(Form = XmlSchemaForm.Unqualified)] int TableID, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)] int[] RowIDs)
		{
			object[] array = base.Invoke("QueryHTMLPopups", new object[]
			{
				MapName,
				TableID,
				RowIDs
			});
			return (string[])array[0];
		}

		public IAsyncResult BeginQueryHTMLPopups(string MapName, int TableID, int[] RowIDs, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("QueryHTMLPopups", new object[]
			{
				MapName,
				TableID,
				RowIDs
			}, callback, asyncState);
		}

		public string[] EndQueryHTMLPopups(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (string[])array[0];
		}

		public void QueryHTMLPopupsAsync(string MapName, int TableID, int[] RowIDs)
		{
			this.QueryHTMLPopupsAsync(MapName, TableID, RowIDs, null);
		}

		public void QueryHTMLPopupsAsync(string MapName, int TableID, int[] RowIDs, object userState)
		{
			if (this.QueryHTMLPopupsOperationCompleted == null)
			{
				this.QueryHTMLPopupsOperationCompleted = new SendOrPostCallback(this.OnQueryHTMLPopupsOperationCompleted);
			}
			base.InvokeAsync("QueryHTMLPopups", new object[]
			{
				MapName,
				TableID,
				RowIDs
			}, this.QueryHTMLPopupsOperationCompleted, userState);
		}

		private void OnQueryHTMLPopupsOperationCompleted(object arg)
		{
			if (this.QueryHTMLPopupsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.QueryHTMLPopupsCompleted(this, new QueryHTMLPopupsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public CacheStorageInfo GetCacheStorageInfo([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName)
		{
			object[] array = base.Invoke("GetCacheStorageInfo", new object[]
			{
				MapName
			});
			return (CacheStorageInfo)array[0];
		}

		public IAsyncResult BeginGetCacheStorageInfo(string MapName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetCacheStorageInfo", new object[]
			{
				MapName
			}, callback, asyncState);
		}

		public CacheStorageInfo EndGetCacheStorageInfo(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (CacheStorageInfo)array[0];
		}

		public void GetCacheStorageInfoAsync(string MapName)
		{
			this.GetCacheStorageInfoAsync(MapName, null);
		}

		public void GetCacheStorageInfoAsync(string MapName, object userState)
		{
			if (this.GetCacheStorageInfoOperationCompleted == null)
			{
				this.GetCacheStorageInfoOperationCompleted = new SendOrPostCallback(this.OnGetCacheStorageInfoOperationCompleted);
			}
			base.InvokeAsync("GetCacheStorageInfo", new object[]
			{
				MapName
			}, this.GetCacheStorageInfoOperationCompleted, userState);
		}

		private void OnGetCacheStorageInfoOperationCompleted(object arg)
		{
			if (this.GetCacheStorageInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetCacheStorageInfoCompleted(this, new GetCacheStorageInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public Multipoint ToMapPoints([XmlElement(Form = XmlSchemaForm.Unqualified)] MapDescription MapDescription, [XmlElement(Form = XmlSchemaForm.Unqualified)] ImageDisplay MapImageDisplay, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)] int[] ScreenXValues, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)] int[] ScreenYValues)
		{
			object[] array = base.Invoke("ToMapPoints", new object[]
			{
				MapDescription,
				MapImageDisplay,
				ScreenXValues,
				ScreenYValues
			});
			return (Multipoint)array[0];
		}

		public IAsyncResult BeginToMapPoints(MapDescription MapDescription, ImageDisplay MapImageDisplay, int[] ScreenXValues, int[] ScreenYValues, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ToMapPoints", new object[]
			{
				MapDescription,
				MapImageDisplay,
				ScreenXValues,
				ScreenYValues
			}, callback, asyncState);
		}

		public Multipoint EndToMapPoints(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Multipoint)array[0];
		}

		public void ToMapPointsAsync(MapDescription MapDescription, ImageDisplay MapImageDisplay, int[] ScreenXValues, int[] ScreenYValues)
		{
			this.ToMapPointsAsync(MapDescription, MapImageDisplay, ScreenXValues, ScreenYValues, null);
		}

		public void ToMapPointsAsync(MapDescription MapDescription, ImageDisplay MapImageDisplay, int[] ScreenXValues, int[] ScreenYValues, object userState)
		{
			if (this.ToMapPointsOperationCompleted == null)
			{
				this.ToMapPointsOperationCompleted = new SendOrPostCallback(this.OnToMapPointsOperationCompleted);
			}
			base.InvokeAsync("ToMapPoints", new object[]
			{
				MapDescription,
				MapImageDisplay,
				ScreenXValues,
				ScreenYValues
			}, this.ToMapPointsOperationCompleted, userState);
		}

		private void OnToMapPointsOperationCompleted(object arg)
		{
			if (this.ToMapPointsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ToMapPointsCompleted(this, new ToMapPointsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public string GetMapName([XmlElement(Form = XmlSchemaForm.Unqualified)] int Index)
		{
			object[] array = base.Invoke("GetMapName", new object[]
			{
				Index
			});
			return (string)array[0];
		}

		public IAsyncResult BeginGetMapName(int Index, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetMapName", new object[]
			{
				Index
			}, callback, asyncState);
		}

		public string EndGetMapName(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (string)array[0];
		}

		public void GetMapNameAsync(int Index)
		{
			this.GetMapNameAsync(Index, null);
		}

		public void GetMapNameAsync(int Index, object userState)
		{
			if (this.GetMapNameOperationCompleted == null)
			{
				this.GetMapNameOperationCompleted = new SendOrPostCallback(this.OnGetMapNameOperationCompleted);
			}
			base.InvokeAsync("GetMapName", new object[]
			{
				Index
			}, this.GetMapNameOperationCompleted, userState);
		}

		private void OnGetMapNameOperationCompleted(object arg)
		{
			if (this.GetMapNameCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetMapNameCompleted(this, new GetMapNameCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public TileCacheInfo GetTileCacheInfo([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName)
		{
			object[] array = base.Invoke("GetTileCacheInfo", new object[]
			{
				MapName
			});
			return (TileCacheInfo)array[0];
		}

		public IAsyncResult BeginGetTileCacheInfo(string MapName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetTileCacheInfo", new object[]
			{
				MapName
			}, callback, asyncState);
		}

		public TileCacheInfo EndGetTileCacheInfo(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (TileCacheInfo)array[0];
		}

		public void GetTileCacheInfoAsync(string MapName)
		{
			this.GetTileCacheInfoAsync(MapName, null);
		}

		public void GetTileCacheInfoAsync(string MapName, object userState)
		{
			if (this.GetTileCacheInfoOperationCompleted == null)
			{
				this.GetTileCacheInfoOperationCompleted = new SendOrPostCallback(this.OnGetTileCacheInfoOperationCompleted);
			}
			base.InvokeAsync("GetTileCacheInfo", new object[]
			{
				MapName
			}, this.GetTileCacheInfoOperationCompleted, userState);
		}

		private void OnGetTileCacheInfoOperationCompleted(object arg)
		{
			if (this.GetTileCacheInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetTileCacheInfoCompleted(this, new GetTileCacheInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public QueryResult QueryRelatedRecords([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName, [XmlElement(Form = XmlSchemaForm.Unqualified)] int SourceTableID, [XmlElement(Form = XmlSchemaForm.Unqualified)] FIDSet SourceFIDSet, [XmlElement(Form = XmlSchemaForm.Unqualified)] RelateDescription RelateDescription)
		{
			object[] array = base.Invoke("QueryRelatedRecords", new object[]
			{
				MapName,
				SourceTableID,
				SourceFIDSet,
				RelateDescription
			});
			return (QueryResult)array[0];
		}

		public IAsyncResult BeginQueryRelatedRecords(string MapName, int SourceTableID, FIDSet SourceFIDSet, RelateDescription RelateDescription, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("QueryRelatedRecords", new object[]
			{
				MapName,
				SourceTableID,
				SourceFIDSet,
				RelateDescription
			}, callback, asyncState);
		}

		public QueryResult EndQueryRelatedRecords(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (QueryResult)array[0];
		}

		public void QueryRelatedRecordsAsync(string MapName, int SourceTableID, FIDSet SourceFIDSet, RelateDescription RelateDescription)
		{
			this.QueryRelatedRecordsAsync(MapName, SourceTableID, SourceFIDSet, RelateDescription, null);
		}

		public void QueryRelatedRecordsAsync(string MapName, int SourceTableID, FIDSet SourceFIDSet, RelateDescription RelateDescription, object userState)
		{
			if (this.QueryRelatedRecordsOperationCompleted == null)
			{
				this.QueryRelatedRecordsOperationCompleted = new SendOrPostCallback(this.OnQueryRelatedRecordsOperationCompleted);
			}
			base.InvokeAsync("QueryRelatedRecords", new object[]
			{
				MapName,
				SourceTableID,
				SourceFIDSet,
				RelateDescription
			}, this.QueryRelatedRecordsOperationCompleted, userState);
		}

		private void OnQueryRelatedRecordsOperationCompleted(object arg)
		{
			if (this.QueryRelatedRecordsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.QueryRelatedRecordsCompleted(this, new QueryRelatedRecordsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public RecordSet QueryFeatureData([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName, [XmlElement(Form = XmlSchemaForm.Unqualified)] int LayerID, [XmlElement(Form = XmlSchemaForm.Unqualified)] QueryFilter QueryFilter)
		{
			object[] array = base.Invoke("QueryFeatureData", new object[]
			{
				MapName,
				LayerID,
				QueryFilter
			});
			return (RecordSet)array[0];
		}

		public IAsyncResult BeginQueryFeatureData(string MapName, int LayerID, QueryFilter QueryFilter, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("QueryFeatureData", new object[]
			{
				MapName,
				LayerID,
				QueryFilter
			}, callback, asyncState);
		}

		public RecordSet EndQueryFeatureData(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (RecordSet)array[0];
		}

		public void QueryFeatureDataAsync(string MapName, int LayerID, QueryFilter QueryFilter)
		{
			this.QueryFeatureDataAsync(MapName, LayerID, QueryFilter, null);
		}

		public void QueryFeatureDataAsync(string MapName, int LayerID, QueryFilter QueryFilter, object userState)
		{
			if (this.QueryFeatureDataOperationCompleted == null)
			{
				this.QueryFeatureDataOperationCompleted = new SendOrPostCallback(this.OnQueryFeatureDataOperationCompleted);
			}
			base.InvokeAsync("QueryFeatureData", new object[]
			{
				MapName,
				LayerID,
				QueryFilter
			}, this.QueryFeatureDataOperationCompleted, userState);
		}

		private void OnQueryFeatureDataOperationCompleted(object arg)
		{
			if (this.QueryFeatureDataCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.QueryFeatureDataCompleted(this, new QueryFeatureDataCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public MapServerHyperlink[] QueryHyperlinks([XmlElement(Form = XmlSchemaForm.Unqualified)] MapDescription MapDescription, [XmlElement(Form = XmlSchemaForm.Unqualified)] ImageDisplay MapImageDisplay, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)] int[] LayerIDs)
		{
			object[] array = base.Invoke("QueryHyperlinks", new object[]
			{
				MapDescription,
				MapImageDisplay,
				LayerIDs
			});
			return (MapServerHyperlink[])array[0];
		}

		public IAsyncResult BeginQueryHyperlinks(MapDescription MapDescription, ImageDisplay MapImageDisplay, int[] LayerIDs, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("QueryHyperlinks", new object[]
			{
				MapDescription,
				MapImageDisplay,
				LayerIDs
			}, callback, asyncState);
		}

		public MapServerHyperlink[] EndQueryHyperlinks(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (MapServerHyperlink[])array[0];
		}

		public void QueryHyperlinksAsync(MapDescription MapDescription, ImageDisplay MapImageDisplay, int[] LayerIDs)
		{
			this.QueryHyperlinksAsync(MapDescription, MapImageDisplay, LayerIDs, null);
		}

		public void QueryHyperlinksAsync(MapDescription MapDescription, ImageDisplay MapImageDisplay, int[] LayerIDs, object userState)
		{
			if (this.QueryHyperlinksOperationCompleted == null)
			{
				this.QueryHyperlinksOperationCompleted = new SendOrPostCallback(this.OnQueryHyperlinksOperationCompleted);
			}
			base.InvokeAsync("QueryHyperlinks", new object[]
			{
				MapDescription,
				MapImageDisplay,
				LayerIDs
			}, this.QueryHyperlinksOperationCompleted, userState);
		}

		private void OnQueryHyperlinksOperationCompleted(object arg)
		{
			if (this.QueryHyperlinksCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.QueryHyperlinksCompleted(this, new QueryHyperlinksCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public bool HasLayerCache([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName, [XmlElement(Form = XmlSchemaForm.Unqualified)] int LayerID)
		{
			object[] array = base.Invoke("HasLayerCache", new object[]
			{
				MapName,
				LayerID
			});
			return (bool)array[0];
		}

		public IAsyncResult BeginHasLayerCache(string MapName, int LayerID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("HasLayerCache", new object[]
			{
				MapName,
				LayerID
			}, callback, asyncState);
		}

		public bool EndHasLayerCache(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (bool)array[0];
		}

		public void HasLayerCacheAsync(string MapName, int LayerID)
		{
			this.HasLayerCacheAsync(MapName, LayerID, null);
		}

		public void HasLayerCacheAsync(string MapName, int LayerID, object userState)
		{
			if (this.HasLayerCacheOperationCompleted == null)
			{
				this.HasLayerCacheOperationCompleted = new SendOrPostCallback(this.OnHasLayerCacheOperationCompleted);
			}
			base.InvokeAsync("HasLayerCache", new object[]
			{
				MapName,
				LayerID
			}, this.HasLayerCacheOperationCompleted, userState);
		}

		private void OnHasLayerCacheOperationCompleted(object arg)
		{
			if (this.HasLayerCacheCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.HasLayerCacheCompleted(this, new HasLayerCacheCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public FIDSet QueryFeatureIDs2([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName, [XmlElement(Form = XmlSchemaForm.Unqualified)] LayerDescription LayerDescription, [XmlElement(Form = XmlSchemaForm.Unqualified)] QueryFilter QueryFilter)
		{
			object[] array = base.Invoke("QueryFeatureIDs2", new object[]
			{
				MapName,
				LayerDescription,
				QueryFilter
			});
			return (FIDSet)array[0];
		}

		public IAsyncResult BeginQueryFeatureIDs2(string MapName, LayerDescription LayerDescription, QueryFilter QueryFilter, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("QueryFeatureIDs2", new object[]
			{
				MapName,
				LayerDescription,
				QueryFilter
			}, callback, asyncState);
		}

		public FIDSet EndQueryFeatureIDs2(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (FIDSet)array[0];
		}

		public void QueryFeatureIDs2Async(string MapName, LayerDescription LayerDescription, QueryFilter QueryFilter)
		{
			this.QueryFeatureIDs2Async(MapName, LayerDescription, QueryFilter, null);
		}

		public void QueryFeatureIDs2Async(string MapName, LayerDescription LayerDescription, QueryFilter QueryFilter, object userState)
		{
			if (this.QueryFeatureIDs2OperationCompleted == null)
			{
				this.QueryFeatureIDs2OperationCompleted = new SendOrPostCallback(this.OnQueryFeatureIDs2OperationCompleted);
			}
			base.InvokeAsync("QueryFeatureIDs2", new object[]
			{
				MapName,
				LayerDescription,
				QueryFilter
			}, this.QueryFeatureIDs2OperationCompleted, userState);
		}

		private void OnQueryFeatureIDs2OperationCompleted(object arg)
		{
			if (this.QueryFeatureIDs2Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.QueryFeatureIDs2Completed(this, new QueryFeatureIDs2CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified, DataType = "base64Binary")]
		public byte[] GetLayerTile([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName, [XmlElement(Form = XmlSchemaForm.Unqualified)] int LayerID, [XmlElement(Form = XmlSchemaForm.Unqualified)] int Level, [XmlElement(Form = XmlSchemaForm.Unqualified)] int Row, [XmlElement(Form = XmlSchemaForm.Unqualified)] int Column, [XmlElement(Form = XmlSchemaForm.Unqualified)] string Format)
		{
			object[] array = base.Invoke("GetLayerTile", new object[]
			{
				MapName,
				LayerID,
				Level,
				Row,
				Column,
				Format
			});
			return (byte[])array[0];
		}

		public IAsyncResult BeginGetLayerTile(string MapName, int LayerID, int Level, int Row, int Column, string Format, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetLayerTile", new object[]
			{
				MapName,
				LayerID,
				Level,
				Row,
				Column,
				Format
			}, callback, asyncState);
		}

		public byte[] EndGetLayerTile(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (byte[])array[0];
		}

		public void GetLayerTileAsync(string MapName, int LayerID, int Level, int Row, int Column, string Format)
		{
			this.GetLayerTileAsync(MapName, LayerID, Level, Row, Column, Format, null);
		}

		public void GetLayerTileAsync(string MapName, int LayerID, int Level, int Row, int Column, string Format, object userState)
		{
			if (this.GetLayerTileOperationCompleted == null)
			{
				this.GetLayerTileOperationCompleted = new SendOrPostCallback(this.OnGetLayerTileOperationCompleted);
			}
			base.InvokeAsync("GetLayerTile", new object[]
			{
				MapName,
				LayerID,
				Level,
				Row,
				Column,
				Format
			}, this.GetLayerTileOperationCompleted, userState);
		}

		private void OnGetLayerTileOperationCompleted(object arg)
		{
			if (this.GetLayerTileCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetLayerTileCompleted(this, new GetLayerTileCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public PropertySet GetServiceConfigurationInfo()
		{
			object[] array = base.Invoke("GetServiceConfigurationInfo", new object[0]);
			return (PropertySet)array[0];
		}

		public IAsyncResult BeginGetServiceConfigurationInfo(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetServiceConfigurationInfo", new object[0], callback, asyncState);
		}

		public PropertySet EndGetServiceConfigurationInfo(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (PropertySet)array[0];
		}

		public void GetServiceConfigurationInfoAsync()
		{
			this.GetServiceConfigurationInfoAsync(null);
		}

		public void GetServiceConfigurationInfoAsync(object userState)
		{
			if (this.GetServiceConfigurationInfoOperationCompleted == null)
			{
				this.GetServiceConfigurationInfoOperationCompleted = new SendOrPostCallback(this.OnGetServiceConfigurationInfoOperationCompleted);
			}
			base.InvokeAsync("GetServiceConfigurationInfo", new object[0], this.GetServiceConfigurationInfoOperationCompleted, userState);
		}

		private void OnGetServiceConfigurationInfoOperationCompleted(object arg)
		{
			if (this.GetServiceConfigurationInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetServiceConfigurationInfoCompleted(this, new GetServiceConfigurationInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public int QueryFeatureCount2([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName, [XmlElement(Form = XmlSchemaForm.Unqualified)] LayerDescription LayerDescription, [XmlElement(Form = XmlSchemaForm.Unqualified)] QueryFilter QueryFilter)
		{
			object[] array = base.Invoke("QueryFeatureCount2", new object[]
			{
				MapName,
				LayerDescription,
				QueryFilter
			});
			return (int)array[0];
		}

		public IAsyncResult BeginQueryFeatureCount2(string MapName, LayerDescription LayerDescription, QueryFilter QueryFilter, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("QueryFeatureCount2", new object[]
			{
				MapName,
				LayerDescription,
				QueryFilter
			}, callback, asyncState);
		}

		public int EndQueryFeatureCount2(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (int)array[0];
		}

		public void QueryFeatureCount2Async(string MapName, LayerDescription LayerDescription, QueryFilter QueryFilter)
		{
			this.QueryFeatureCount2Async(MapName, LayerDescription, QueryFilter, null);
		}

		public void QueryFeatureCount2Async(string MapName, LayerDescription LayerDescription, QueryFilter QueryFilter, object userState)
		{
			if (this.QueryFeatureCount2OperationCompleted == null)
			{
				this.QueryFeatureCount2OperationCompleted = new SendOrPostCallback(this.OnQueryFeatureCount2OperationCompleted);
			}
			base.InvokeAsync("QueryFeatureCount2", new object[]
			{
				MapName,
				LayerDescription,
				QueryFilter
			}, this.QueryFeatureCount2OperationCompleted, userState);
		}

		private void OnQueryFeatureCount2OperationCompleted(object arg)
		{
			if (this.QueryFeatureCount2Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.QueryFeatureCount2Completed(this, new QueryFeatureCount2CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public CacheDescriptionInfo GetCacheDescriptionInfo([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName)
		{
			object[] array = base.Invoke("GetCacheDescriptionInfo", new object[]
			{
				MapName
			});
			return (CacheDescriptionInfo)array[0];
		}

		public IAsyncResult BeginGetCacheDescriptionInfo(string MapName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetCacheDescriptionInfo", new object[]
			{
				MapName
			}, callback, asyncState);
		}

		public CacheDescriptionInfo EndGetCacheDescriptionInfo(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (CacheDescriptionInfo)array[0];
		}

		public void GetCacheDescriptionInfoAsync(string MapName)
		{
			this.GetCacheDescriptionInfoAsync(MapName, null);
		}

		public void GetCacheDescriptionInfoAsync(string MapName, object userState)
		{
			if (this.GetCacheDescriptionInfoOperationCompleted == null)
			{
				this.GetCacheDescriptionInfoOperationCompleted = new SendOrPostCallback(this.OnGetCacheDescriptionInfoOperationCompleted);
			}
			base.InvokeAsync("GetCacheDescriptionInfo", new object[]
			{
				MapName
			}, this.GetCacheDescriptionInfoOperationCompleted, userState);
		}

		private void OnGetCacheDescriptionInfoOperationCompleted(object arg)
		{
			if (this.GetCacheDescriptionInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetCacheDescriptionInfoCompleted(this, new GetCacheDescriptionInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public MapServerIdentifyResult[] Identify([XmlElement(Form = XmlSchemaForm.Unqualified)] MapDescription MapDescription, [XmlElement(Form = XmlSchemaForm.Unqualified)] ImageDisplay MapImageDisplay, [XmlElement(Form = XmlSchemaForm.Unqualified)] Geometry SearchShape, [XmlElement(Form = XmlSchemaForm.Unqualified)] int Tolerance, [XmlElement(Form = XmlSchemaForm.Unqualified)] esriIdentifyOption IdentifyOption, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)] int[] LayerIDs)
		{
			object[] array = base.Invoke("Identify", new object[]
			{
				MapDescription,
				MapImageDisplay,
				SearchShape,
				Tolerance,
				IdentifyOption,
				LayerIDs
			});
			return (MapServerIdentifyResult[])array[0];
		}

		public IAsyncResult BeginIdentify(MapDescription MapDescription, ImageDisplay MapImageDisplay, Geometry SearchShape, int Tolerance, esriIdentifyOption IdentifyOption, int[] LayerIDs, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Identify", new object[]
			{
				MapDescription,
				MapImageDisplay,
				SearchShape,
				Tolerance,
				IdentifyOption,
				LayerIDs
			}, callback, asyncState);
		}

		public MapServerIdentifyResult[] EndIdentify(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (MapServerIdentifyResult[])array[0];
		}

		public void IdentifyAsync(MapDescription MapDescription, ImageDisplay MapImageDisplay, Geometry SearchShape, int Tolerance, esriIdentifyOption IdentifyOption, int[] LayerIDs)
		{
			this.IdentifyAsync(MapDescription, MapImageDisplay, SearchShape, Tolerance, IdentifyOption, LayerIDs, null);
		}

		public void IdentifyAsync(MapDescription MapDescription, ImageDisplay MapImageDisplay, Geometry SearchShape, int Tolerance, esriIdentifyOption IdentifyOption, int[] LayerIDs, object userState)
		{
			if (this.IdentifyOperationCompleted == null)
			{
				this.IdentifyOperationCompleted = new SendOrPostCallback(this.OnIdentifyOperationCompleted);
			}
			base.InvokeAsync("Identify", new object[]
			{
				MapDescription,
				MapImageDisplay,
				SearchShape,
				Tolerance,
				IdentifyOption,
				LayerIDs
			}, this.IdentifyOperationCompleted, userState);
		}

		private void OnIdentifyOperationCompleted(object arg)
		{
			if (this.IdentifyCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.IdentifyCompleted(this, new IdentifyCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public double ComputeDistance([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName, [XmlElement(Form = XmlSchemaForm.Unqualified)] Point FromPoint, [XmlElement(Form = XmlSchemaForm.Unqualified)] Point ToPoint, [XmlElement(Form = XmlSchemaForm.Unqualified)] esriUnits Units)
		{
			object[] array = base.Invoke("ComputeDistance", new object[]
			{
				MapName,
				FromPoint,
				ToPoint,
				Units
			});
			return (double)array[0];
		}

		public IAsyncResult BeginComputeDistance(string MapName, Point FromPoint, Point ToPoint, esriUnits Units, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ComputeDistance", new object[]
			{
				MapName,
				FromPoint,
				ToPoint,
				Units
			}, callback, asyncState);
		}

		public double EndComputeDistance(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (double)array[0];
		}

		public void ComputeDistanceAsync(string MapName, Point FromPoint, Point ToPoint, esriUnits Units)
		{
			this.ComputeDistanceAsync(MapName, FromPoint, ToPoint, Units, null);
		}

		public void ComputeDistanceAsync(string MapName, Point FromPoint, Point ToPoint, esriUnits Units, object userState)
		{
			if (this.ComputeDistanceOperationCompleted == null)
			{
				this.ComputeDistanceOperationCompleted = new SendOrPostCallback(this.OnComputeDistanceOperationCompleted);
			}
			base.InvokeAsync("ComputeDistance", new object[]
			{
				MapName,
				FromPoint,
				ToPoint,
				Units
			}, this.ComputeDistanceOperationCompleted, userState);
		}

		private void OnComputeDistanceOperationCompleted(object arg)
		{
			if (this.ComputeDistanceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ComputeDistanceCompleted(this, new ComputeDistanceCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public string GetDefaultMapName()
		{
			object[] array = base.Invoke("GetDefaultMapName", new object[0]);
			return (string)array[0];
		}

		public IAsyncResult BeginGetDefaultMapName(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetDefaultMapName", new object[0], callback, asyncState);
		}

		public string EndGetDefaultMapName(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (string)array[0];
		}

		public void GetDefaultMapNameAsync()
		{
			this.GetDefaultMapNameAsync(null);
		}

		public void GetDefaultMapNameAsync(object userState)
		{
			if (this.GetDefaultMapNameOperationCompleted == null)
			{
				this.GetDefaultMapNameOperationCompleted = new SendOrPostCallback(this.OnGetDefaultMapNameOperationCompleted);
			}
			base.InvokeAsync("GetDefaultMapName", new object[0], this.GetDefaultMapNameOperationCompleted, userState);
		}

		private void OnGetDefaultMapNameOperationCompleted(object arg)
		{
			if (this.GetDefaultMapNameCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetDefaultMapNameCompleted(this, new GetDefaultMapNameCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public bool IsFixedScaleMap([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName)
		{
			object[] array = base.Invoke("IsFixedScaleMap", new object[]
			{
				MapName
			});
			return (bool)array[0];
		}

		public IAsyncResult BeginIsFixedScaleMap(string MapName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("IsFixedScaleMap", new object[]
			{
				MapName
			}, callback, asyncState);
		}

		public bool EndIsFixedScaleMap(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (bool)array[0];
		}

		public void IsFixedScaleMapAsync(string MapName)
		{
			this.IsFixedScaleMapAsync(MapName, null);
		}

		public void IsFixedScaleMapAsync(string MapName, object userState)
		{
			if (this.IsFixedScaleMapOperationCompleted == null)
			{
				this.IsFixedScaleMapOperationCompleted = new SendOrPostCallback(this.OnIsFixedScaleMapOperationCompleted);
			}
			base.InvokeAsync("IsFixedScaleMap", new object[]
			{
				MapName
			}, this.IsFixedScaleMapOperationCompleted, userState);
		}

		private void OnIsFixedScaleMapOperationCompleted(object arg)
		{
			if (this.IsFixedScaleMapCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.IsFixedScaleMapCompleted(this, new IsFixedScaleMapCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public MapServerLegendInfo[] GetLegendInfo([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)] int[] LayerIDs, [XmlElement(Form = XmlSchemaForm.Unqualified)] MapServerLegendPatch LegendPatch, [XmlElement(Form = XmlSchemaForm.Unqualified)] ImageType ImageType)
		{
			object[] array = base.Invoke("GetLegendInfo", new object[]
			{
				MapName,
				LayerIDs,
				LegendPatch,
				ImageType
			});
			return (MapServerLegendInfo[])array[0];
		}

		public IAsyncResult BeginGetLegendInfo(string MapName, int[] LayerIDs, MapServerLegendPatch LegendPatch, ImageType ImageType, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetLegendInfo", new object[]
			{
				MapName,
				LayerIDs,
				LegendPatch,
				ImageType
			}, callback, asyncState);
		}

		public MapServerLegendInfo[] EndGetLegendInfo(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (MapServerLegendInfo[])array[0];
		}

		public void GetLegendInfoAsync(string MapName, int[] LayerIDs, MapServerLegendPatch LegendPatch, ImageType ImageType)
		{
			this.GetLegendInfoAsync(MapName, LayerIDs, LegendPatch, ImageType, null);
		}

		public void GetLegendInfoAsync(string MapName, int[] LayerIDs, MapServerLegendPatch LegendPatch, ImageType ImageType, object userState)
		{
			if (this.GetLegendInfoOperationCompleted == null)
			{
				this.GetLegendInfoOperationCompleted = new SendOrPostCallback(this.OnGetLegendInfoOperationCompleted);
			}
			base.InvokeAsync("GetLegendInfo", new object[]
			{
				MapName,
				LayerIDs,
				LegendPatch,
				ImageType
			}, this.GetLegendInfoOperationCompleted, userState);
		}

		private void OnGetLegendInfoOperationCompleted(object arg)
		{
			if (this.GetLegendInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetLegendInfoCompleted(this, new GetLegendInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public int GetMapCount()
		{
			object[] array = base.Invoke("GetMapCount", new object[0]);
			return (int)array[0];
		}

		public IAsyncResult BeginGetMapCount(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetMapCount", new object[0], callback, asyncState);
		}

		public int EndGetMapCount(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (int)array[0];
		}

		public void GetMapCountAsync()
		{
			this.GetMapCountAsync(null);
		}

		public void GetMapCountAsync(object userState)
		{
			if (this.GetMapCountOperationCompleted == null)
			{
				this.GetMapCountOperationCompleted = new SendOrPostCallback(this.OnGetMapCountOperationCompleted);
			}
			base.InvokeAsync("GetMapCount", new object[0], this.GetMapCountOperationCompleted, userState);
		}

		private void OnGetMapCountOperationCompleted(object arg)
		{
			if (this.GetMapCountCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetMapCountCompleted(this, new GetMapCountCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public bool HasSingleFusedMapCache([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName)
		{
			object[] array = base.Invoke("HasSingleFusedMapCache", new object[]
			{
				MapName
			});
			return (bool)array[0];
		}

		public IAsyncResult BeginHasSingleFusedMapCache(string MapName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("HasSingleFusedMapCache", new object[]
			{
				MapName
			}, callback, asyncState);
		}

		public bool EndHasSingleFusedMapCache(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (bool)array[0];
		}

		public void HasSingleFusedMapCacheAsync(string MapName)
		{
			this.HasSingleFusedMapCacheAsync(MapName, null);
		}

		public void HasSingleFusedMapCacheAsync(string MapName, object userState)
		{
			if (this.HasSingleFusedMapCacheOperationCompleted == null)
			{
				this.HasSingleFusedMapCacheOperationCompleted = new SendOrPostCallback(this.OnHasSingleFusedMapCacheOperationCompleted);
			}
			base.InvokeAsync("HasSingleFusedMapCache", new object[]
			{
				MapName
			}, this.HasSingleFusedMapCacheOperationCompleted, userState);
		}

		private void OnHasSingleFusedMapCacheOperationCompleted(object arg)
		{
			if (this.HasSingleFusedMapCacheCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.HasSingleFusedMapCacheCompleted(this, new HasSingleFusedMapCacheCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified, DataType = "base64Binary")]
		public byte[] GetMapTile([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName, [XmlElement(Form = XmlSchemaForm.Unqualified)] int Level, [XmlElement(Form = XmlSchemaForm.Unqualified)] int Row, [XmlElement(Form = XmlSchemaForm.Unqualified)] int Column, [XmlElement(Form = XmlSchemaForm.Unqualified)] string Format)
		{
			object[] array = base.Invoke("GetMapTile", new object[]
			{
				MapName,
				Level,
				Row,
				Column,
				Format
			});
			return (byte[])array[0];
		}

		public IAsyncResult BeginGetMapTile(string MapName, int Level, int Row, int Column, string Format, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetMapTile", new object[]
			{
				MapName,
				Level,
				Row,
				Column,
				Format
			}, callback, asyncState);
		}

		public byte[] EndGetMapTile(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (byte[])array[0];
		}

		public void GetMapTileAsync(string MapName, int Level, int Row, int Column, string Format)
		{
			this.GetMapTileAsync(MapName, Level, Row, Column, Format, null);
		}

		public void GetMapTileAsync(string MapName, int Level, int Row, int Column, string Format, object userState)
		{
			if (this.GetMapTileOperationCompleted == null)
			{
				this.GetMapTileOperationCompleted = new SendOrPostCallback(this.OnGetMapTileOperationCompleted);
			}
			base.InvokeAsync("GetMapTile", new object[]
			{
				MapName,
				Level,
				Row,
				Column,
				Format
			}, this.GetMapTileOperationCompleted, userState);
		}

		private void OnGetMapTileOperationCompleted(object arg)
		{
			if (this.GetMapTileCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetMapTileCompleted(this, new GetMapTileCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public MapServerInfo GetServerInfo([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName)
		{
			object[] array = base.Invoke("GetServerInfo", new object[]
			{
				MapName
			});
			return (MapServerInfo)array[0];
		}

		public IAsyncResult BeginGetServerInfo(string MapName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetServerInfo", new object[]
			{
				MapName
			}, callback, asyncState);
		}

		public MapServerInfo EndGetServerInfo(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (MapServerInfo)array[0];
		}

		public void GetServerInfoAsync(string MapName)
		{
			this.GetServerInfoAsync(MapName, null);
		}

		public void GetServerInfoAsync(string MapName, object userState)
		{
			if (this.GetServerInfoOperationCompleted == null)
			{
				this.GetServerInfoOperationCompleted = new SendOrPostCallback(this.OnGetServerInfoOperationCompleted);
			}
			base.InvokeAsync("GetServerInfo", new object[]
			{
				MapName
			}, this.GetServerInfoOperationCompleted, userState);
		}

		private void OnGetServerInfoOperationCompleted(object arg)
		{
			if (this.GetServerInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetServerInfoCompleted(this, new GetServerInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public MapTableSubtypeInfo[] GetMapTableSubtypeInfos([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)] int[] TableIDs)
		{
			object[] array = base.Invoke("GetMapTableSubtypeInfos", new object[]
			{
				MapName,
				TableIDs
			});
			return (MapTableSubtypeInfo[])array[0];
		}

		public IAsyncResult BeginGetMapTableSubtypeInfos(string MapName, int[] TableIDs, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetMapTableSubtypeInfos", new object[]
			{
				MapName,
				TableIDs
			}, callback, asyncState);
		}

		public MapTableSubtypeInfo[] EndGetMapTableSubtypeInfos(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (MapTableSubtypeInfo[])array[0];
		}

		public void GetMapTableSubtypeInfosAsync(string MapName, int[] TableIDs)
		{
			this.GetMapTableSubtypeInfosAsync(MapName, TableIDs, null);
		}

		public void GetMapTableSubtypeInfosAsync(string MapName, int[] TableIDs, object userState)
		{
			if (this.GetMapTableSubtypeInfosOperationCompleted == null)
			{
				this.GetMapTableSubtypeInfosOperationCompleted = new SendOrPostCallback(this.OnGetMapTableSubtypeInfosOperationCompleted);
			}
			base.InvokeAsync("GetMapTableSubtypeInfos", new object[]
			{
				MapName,
				TableIDs
			}, this.GetMapTableSubtypeInfosOperationCompleted, userState);
		}

		private void OnGetMapTableSubtypeInfosOperationCompleted(object arg)
		{
			if (this.GetMapTableSubtypeInfosCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetMapTableSubtypeInfosCompleted(this, new GetMapTableSubtypeInfosCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public int QueryFeatureCount([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName, [XmlElement(Form = XmlSchemaForm.Unqualified)] int LayerID, [XmlElement(Form = XmlSchemaForm.Unqualified)] QueryFilter QueryFilter)
		{
			object[] array = base.Invoke("QueryFeatureCount", new object[]
			{
				MapName,
				LayerID,
				QueryFilter
			});
			return (int)array[0];
		}

		public IAsyncResult BeginQueryFeatureCount(string MapName, int LayerID, QueryFilter QueryFilter, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("QueryFeatureCount", new object[]
			{
				MapName,
				LayerID,
				QueryFilter
			}, callback, asyncState);
		}

		public int EndQueryFeatureCount(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (int)array[0];
		}

		public void QueryFeatureCountAsync(string MapName, int LayerID, QueryFilter QueryFilter)
		{
			this.QueryFeatureCountAsync(MapName, LayerID, QueryFilter, null);
		}

		public void QueryFeatureCountAsync(string MapName, int LayerID, QueryFilter QueryFilter, object userState)
		{
			if (this.QueryFeatureCountOperationCompleted == null)
			{
				this.QueryFeatureCountOperationCompleted = new SendOrPostCallback(this.OnQueryFeatureCountOperationCompleted);
			}
			base.InvokeAsync("QueryFeatureCount", new object[]
			{
				MapName,
				LayerID,
				QueryFilter
			}, this.QueryFeatureCountOperationCompleted, userState);
		}

		private void OnQueryFeatureCountOperationCompleted(object arg)
		{
			if (this.QueryFeatureCountCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.QueryFeatureCountCompleted(this, new QueryFeatureCountCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public ImageResult[] QueryRasterValue([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName, [XmlElement(Form = XmlSchemaForm.Unqualified)] int SourceTableID, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)] int[] RowIDs, [XmlElement(Form = XmlSchemaForm.Unqualified)] string FieldName, [XmlElement(Form = XmlSchemaForm.Unqualified)] ImageType ImageType)
		{
			object[] array = base.Invoke("QueryRasterValue", new object[]
			{
				MapName,
				SourceTableID,
				RowIDs,
				FieldName,
				ImageType
			});
			return (ImageResult[])array[0];
		}

		public IAsyncResult BeginQueryRasterValue(string MapName, int SourceTableID, int[] RowIDs, string FieldName, ImageType ImageType, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("QueryRasterValue", new object[]
			{
				MapName,
				SourceTableID,
				RowIDs,
				FieldName,
				ImageType
			}, callback, asyncState);
		}

		public ImageResult[] EndQueryRasterValue(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ImageResult[])array[0];
		}

		public void QueryRasterValueAsync(string MapName, int SourceTableID, int[] RowIDs, string FieldName, ImageType ImageType)
		{
			this.QueryRasterValueAsync(MapName, SourceTableID, RowIDs, FieldName, ImageType, null);
		}

		public void QueryRasterValueAsync(string MapName, int SourceTableID, int[] RowIDs, string FieldName, ImageType ImageType, object userState)
		{
			if (this.QueryRasterValueOperationCompleted == null)
			{
				this.QueryRasterValueOperationCompleted = new SendOrPostCallback(this.OnQueryRasterValueOperationCompleted);
			}
			base.InvokeAsync("QueryRasterValue", new object[]
			{
				MapName,
				SourceTableID,
				RowIDs,
				FieldName,
				ImageType
			}, this.QueryRasterValueOperationCompleted, userState);
		}

		private void OnQueryRasterValueOperationCompleted(object arg)
		{
			if (this.QueryRasterValueCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.QueryRasterValueCompleted(this, new QueryRasterValueCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("ScreenXValues", Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public int[] FromMapPoints([XmlElement(Form = XmlSchemaForm.Unqualified)] MapDescription MapDescription, [XmlElement(Form = XmlSchemaForm.Unqualified)] ImageDisplay MapImageDisplay, [XmlElement(Form = XmlSchemaForm.Unqualified)] Multipoint MapPoints, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)] out int[] ScreenYValues)
		{
			object[] array = base.Invoke("FromMapPoints", new object[]
			{
				MapDescription,
				MapImageDisplay,
				MapPoints
			});
			ScreenYValues = (int[])array[1];
			return (int[])array[0];
		}

		public IAsyncResult BeginFromMapPoints(MapDescription MapDescription, ImageDisplay MapImageDisplay, Multipoint MapPoints, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("FromMapPoints", new object[]
			{
				MapDescription,
				MapImageDisplay,
				MapPoints
			}, callback, asyncState);
		}

		public int[] EndFromMapPoints(IAsyncResult asyncResult, out int[] ScreenYValues)
		{
			object[] array = base.EndInvoke(asyncResult);
			ScreenYValues = (int[])array[1];
			return (int[])array[0];
		}

		public void FromMapPointsAsync(MapDescription MapDescription, ImageDisplay MapImageDisplay, Multipoint MapPoints)
		{
			this.FromMapPointsAsync(MapDescription, MapImageDisplay, MapPoints, null);
		}

		public void FromMapPointsAsync(MapDescription MapDescription, ImageDisplay MapImageDisplay, Multipoint MapPoints, object userState)
		{
			if (this.FromMapPointsOperationCompleted == null)
			{
				this.FromMapPointsOperationCompleted = new SendOrPostCallback(this.OnFromMapPointsOperationCompleted);
			}
			base.InvokeAsync("FromMapPoints", new object[]
			{
				MapDescription,
				MapImageDisplay,
				MapPoints
			}, this.FromMapPointsOperationCompleted, userState);
		}

		private void OnFromMapPointsOperationCompleted(object arg)
		{
			if (this.FromMapPointsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.FromMapPointsCompleted(this, new FromMapPointsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public int[] QueryRowIDs([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName, [XmlElement(Form = XmlSchemaForm.Unqualified)] MapTableDescription MapTableDescription, [XmlElement(Form = XmlSchemaForm.Unqualified)] QueryFilter QueryFilter)
		{
			object[] array = base.Invoke("QueryRowIDs", new object[]
			{
				MapName,
				MapTableDescription,
				QueryFilter
			});
			return (int[])array[0];
		}

		public IAsyncResult BeginQueryRowIDs(string MapName, MapTableDescription MapTableDescription, QueryFilter QueryFilter, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("QueryRowIDs", new object[]
			{
				MapName,
				MapTableDescription,
				QueryFilter
			}, callback, asyncState);
		}

		public int[] EndQueryRowIDs(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (int[])array[0];
		}

		public void QueryRowIDsAsync(string MapName, MapTableDescription MapTableDescription, QueryFilter QueryFilter)
		{
			this.QueryRowIDsAsync(MapName, MapTableDescription, QueryFilter, null);
		}

		public void QueryRowIDsAsync(string MapName, MapTableDescription MapTableDescription, QueryFilter QueryFilter, object userState)
		{
			if (this.QueryRowIDsOperationCompleted == null)
			{
				this.QueryRowIDsOperationCompleted = new SendOrPostCallback(this.OnQueryRowIDsOperationCompleted);
			}
			base.InvokeAsync("QueryRowIDs", new object[]
			{
				MapName,
				MapTableDescription,
				QueryFilter
			}, this.QueryRowIDsOperationCompleted, userState);
		}

		private void OnQueryRowIDsOperationCompleted(object arg)
		{
			if (this.QueryRowIDsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.QueryRowIDsCompleted(this, new QueryRowIDsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public string GetCacheName([XmlElement(Form = XmlSchemaForm.Unqualified)] string MapName, [XmlElement(Form = XmlSchemaForm.Unqualified)] int LayerID)
		{
			object[] array = base.Invoke("GetCacheName", new object[]
			{
				MapName,
				LayerID
			});
			return (string)array[0];
		}

		public IAsyncResult BeginGetCacheName(string MapName, int LayerID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetCacheName", new object[]
			{
				MapName,
				LayerID
			}, callback, asyncState);
		}

		public string EndGetCacheName(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (string)array[0];
		}

		public void GetCacheNameAsync(string MapName, int LayerID)
		{
			this.GetCacheNameAsync(MapName, LayerID, null);
		}

		public void GetCacheNameAsync(string MapName, int LayerID, object userState)
		{
			if (this.GetCacheNameOperationCompleted == null)
			{
				this.GetCacheNameOperationCompleted = new SendOrPostCallback(this.OnGetCacheNameOperationCompleted);
			}
			base.InvokeAsync("GetCacheName", new object[]
			{
				MapName,
				LayerID
			}, this.GetCacheNameOperationCompleted, userState);
		}

		private void OnGetCacheNameOperationCompleted(object arg)
		{
			if (this.GetCacheNameCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetCacheNameCompleted(this, new GetCacheNameCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public double ComputeScale([XmlElement(Form = XmlSchemaForm.Unqualified)] MapDescription MapDescription, [XmlElement(Form = XmlSchemaForm.Unqualified)] ImageDisplay MapImageDisplay)
		{
			object[] array = base.Invoke("ComputeScale", new object[]
			{
				MapDescription,
				MapImageDisplay
			});
			return (double)array[0];
		}

		public IAsyncResult BeginComputeScale(MapDescription MapDescription, ImageDisplay MapImageDisplay, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ComputeScale", new object[]
			{
				MapDescription,
				MapImageDisplay
			}, callback, asyncState);
		}

		public double EndComputeScale(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (double)array[0];
		}

		public void ComputeScaleAsync(MapDescription MapDescription, ImageDisplay MapImageDisplay)
		{
			this.ComputeScaleAsync(MapDescription, MapImageDisplay, null);
		}

		public void ComputeScaleAsync(MapDescription MapDescription, ImageDisplay MapImageDisplay, object userState)
		{
			if (this.ComputeScaleOperationCompleted == null)
			{
				this.ComputeScaleOperationCompleted = new SendOrPostCallback(this.OnComputeScaleOperationCompleted);
			}
			base.InvokeAsync("ComputeScale", new object[]
			{
				MapDescription,
				MapImageDisplay
			}, this.ComputeScaleOperationCompleted, userState);
		}

		private void OnComputeScaleOperationCompleted(object arg)
		{
			if (this.ComputeScaleCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ComputeScaleCompleted(this, new ComputeScaleCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public MapServerFindResult[] Find([XmlElement(Form = XmlSchemaForm.Unqualified)] MapDescription MapDescription, [XmlElement(Form = XmlSchemaForm.Unqualified)] ImageDisplay MapImageDisplay, [XmlElement(Form = XmlSchemaForm.Unqualified)] string SearchString, [XmlElement(Form = XmlSchemaForm.Unqualified)] bool Contains, [XmlElement(Form = XmlSchemaForm.Unqualified)] string SearchFields, [XmlElement(Form = XmlSchemaForm.Unqualified)] esriFindOption FindOption, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)] int[] LayerIDs)
		{
			object[] array = base.Invoke("Find", new object[]
			{
				MapDescription,
				MapImageDisplay,
				SearchString,
				Contains,
				SearchFields,
				FindOption,
				LayerIDs
			});
			return (MapServerFindResult[])array[0];
		}

		public IAsyncResult BeginFind(MapDescription MapDescription, ImageDisplay MapImageDisplay, string SearchString, bool Contains, string SearchFields, esriFindOption FindOption, int[] LayerIDs, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Find", new object[]
			{
				MapDescription,
				MapImageDisplay,
				SearchString,
				Contains,
				SearchFields,
				FindOption,
				LayerIDs
			}, callback, asyncState);
		}

		public MapServerFindResult[] EndFind(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (MapServerFindResult[])array[0];
		}

		public void FindAsync(MapDescription MapDescription, ImageDisplay MapImageDisplay, string SearchString, bool Contains, string SearchFields, esriFindOption FindOption, int[] LayerIDs)
		{
			this.FindAsync(MapDescription, MapImageDisplay, SearchString, Contains, SearchFields, FindOption, LayerIDs, null);
		}

		public void FindAsync(MapDescription MapDescription, ImageDisplay MapImageDisplay, string SearchString, bool Contains, string SearchFields, esriFindOption FindOption, int[] LayerIDs, object userState)
		{
			if (this.FindOperationCompleted == null)
			{
				this.FindOperationCompleted = new SendOrPostCallback(this.OnFindOperationCompleted);
			}
			base.InvokeAsync("Find", new object[]
			{
				MapDescription,
				MapImageDisplay,
				SearchString,
				Contains,
				SearchFields,
				FindOption,
				LayerIDs
			}, this.FindOperationCompleted, userState);
		}

		private void OnFindOperationCompleted(object arg)
		{
			if (this.FindCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.FindCompleted(this, new FindCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public MapImage ExportMapImage([XmlElement(Form = XmlSchemaForm.Unqualified)] MapDescription MapDescription, [XmlElement(Form = XmlSchemaForm.Unqualified)] ImageDescription ImageDescription)
		{
			object[] array = base.Invoke("ExportMapImage", new object[]
			{
				MapDescription,
				ImageDescription
			});
			return (MapImage)array[0];
		}

		public IAsyncResult BeginExportMapImage(MapDescription MapDescription, ImageDescription ImageDescription, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ExportMapImage", new object[]
			{
				MapDescription,
				ImageDescription
			}, callback, asyncState);
		}

		public MapImage EndExportMapImage(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (MapImage)array[0];
		}

		public void ExportMapImageAsync(MapDescription MapDescription, ImageDescription ImageDescription)
		{
			this.ExportMapImageAsync(MapDescription, ImageDescription, null);
		}

		public void ExportMapImageAsync(MapDescription MapDescription, ImageDescription ImageDescription, object userState)
		{
			if (this.ExportMapImageOperationCompleted == null)
			{
				this.ExportMapImageOperationCompleted = new SendOrPostCallback(this.OnExportMapImageOperationCompleted);
			}
			base.InvokeAsync("ExportMapImage", new object[]
			{
				MapDescription,
				ImageDescription
			}, this.ExportMapImageOperationCompleted, userState);
		}

		private void OnExportMapImageOperationCompleted(object arg)
		{
			if (this.ExportMapImageCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ExportMapImageCompleted(this, new ExportMapImageCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}
	}
}
