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
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, WebServiceBinding(Name = "FeatureServerBinding", Namespace = "http://www.esri.com/schemas/ArcGIS/10.0"), XmlInclude(typeof(FeatureServerForceDeriveFromAnyType)), XmlInclude(typeof(GraphicFeatureLayer[])), XmlInclude(typeof(DataObjectType[]))]
	public class FeatureServiceProxy : SoapHttpClientProtocol
	{
		private SendOrPostCallback QueryAttachmentInfosOperationCompleted;

		private SendOrPostCallback GetServiceObjectCountOperationCompleted;

		private SendOrPostCallback AddAttachmentsOperationCompleted;

		private SendOrPostCallback UpdateOperationCompleted;

		private SendOrPostCallback ApplyEditsOperationCompleted;

		private SendOrPostCallback GetLayersOperationCompleted;

		private SendOrPostCallback QueryOperationCompleted;

		private SendOrPostCallback QueryHTMLPopupsOperationCompleted;

		private SendOrPostCallback DeleteOperationCompleted;

		private SendOrPostCallback GetTablesOperationCompleted;

		private SendOrPostCallback GetCountOperationCompleted;

		private SendOrPostCallback AddOperationCompleted;

		private SendOrPostCallback DeleteAttachmentsOperationCompleted;

		private SendOrPostCallback QueryFromServiceOperationCompleted;

		private SendOrPostCallback DeleteByIDOperationCompleted;

		private SendOrPostCallback UpdateAttachmentsOperationCompleted;

		private SendOrPostCallback QueryRelatedObjectsOperationCompleted;

		private SendOrPostCallback QueryAttachmentDataOperationCompleted;

		private SendOrPostCallback QueryIDsOperationCompleted;

		public event QueryAttachmentInfosCompletedEventHandler1 QueryAttachmentInfosCompleted;

		public event GetServiceObjectCountCompletedEventHandler GetServiceObjectCountCompleted;

		public event AddAttachmentsCompletedEventHandler AddAttachmentsCompleted;

		public event UpdateCompletedEventHandler UpdateCompleted;

		public event ApplyEditsCompletedEventHandler ApplyEditsCompleted;

		public event GetLayersCompletedEventHandler GetLayersCompleted;

		public event QueryCompletedEventHandler QueryCompleted;

		public event QueryHTMLPopupsCompletedEventHandler1 QueryHTMLPopupsCompleted;

		public event DeleteCompletedEventHandler DeleteCompleted;

		public event GetTablesCompletedEventHandler GetTablesCompleted;

		public event GetCountCompletedEventHandler GetCountCompleted;

		public event AddCompletedEventHandler AddCompleted;

		public event DeleteAttachmentsCompletedEventHandler DeleteAttachmentsCompleted;

		public event QueryFromServiceCompletedEventHandler QueryFromServiceCompleted;

		public event DeleteByIDCompletedEventHandler DeleteByIDCompleted;

		public event UpdateAttachmentsCompletedEventHandler UpdateAttachmentsCompleted;

		public event QueryRelatedObjectsCompletedEventHandler QueryRelatedObjectsCompleted;

		public event QueryAttachmentDataCompletedEventHandler1 QueryAttachmentDataCompleted;

		public event QueryIDsCompletedEventHandler QueryIDsCompleted;

		public FeatureServiceProxy()
		{
			base.Url = "http://sampleserver3.arcgisonline.com/arcgis/services/Fire/Sheep/MapServer/FeatureServer";
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public AttachmentInfo[] QueryAttachmentInfos([XmlElement(Form = XmlSchemaForm.Unqualified)] int LayerOrTableID, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)] int[] ObjectIDs)
		{
			object[] array = base.Invoke("QueryAttachmentInfos", new object[]
			{
				LayerOrTableID,
				ObjectIDs
			});
			return (AttachmentInfo[])array[0];
		}

		public IAsyncResult BeginQueryAttachmentInfos(int LayerOrTableID, int[] ObjectIDs, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("QueryAttachmentInfos", new object[]
			{
				LayerOrTableID,
				ObjectIDs
			}, callback, asyncState);
		}

		public AttachmentInfo[] EndQueryAttachmentInfos(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (AttachmentInfo[])array[0];
		}

		public void QueryAttachmentInfosAsync(int LayerOrTableID, int[] ObjectIDs)
		{
			this.QueryAttachmentInfosAsync(LayerOrTableID, ObjectIDs, null);
		}

		public void QueryAttachmentInfosAsync(int LayerOrTableID, int[] ObjectIDs, object userState)
		{
			if (this.QueryAttachmentInfosOperationCompleted == null)
			{
				this.QueryAttachmentInfosOperationCompleted = new SendOrPostCallback(this.OnQueryAttachmentInfosOperationCompleted);
			}
			base.InvokeAsync("QueryAttachmentInfos", new object[]
			{
				LayerOrTableID,
				ObjectIDs
			}, this.QueryAttachmentInfosOperationCompleted, userState);
		}

		private void OnQueryAttachmentInfosOperationCompleted(object arg)
		{
			if (this.QueryAttachmentInfosCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.QueryAttachmentInfosCompleted(this, new QueryAttachmentInfosCompletedEventArgs1(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public int GetServiceObjectCount([XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] GFSTableDescription[] LayerOrTableDescriptions, [XmlElement(Form = XmlSchemaForm.Unqualified)] Geometry Geometry)
		{
			object[] array = base.Invoke("GetServiceObjectCount", new object[]
			{
				LayerOrTableDescriptions,
				Geometry
			});
			return (int)array[0];
		}

		public IAsyncResult BeginGetServiceObjectCount(GFSTableDescription[] LayerOrTableDescriptions, Geometry Geometry, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetServiceObjectCount", new object[]
			{
				LayerOrTableDescriptions,
				Geometry
			}, callback, asyncState);
		}

		public int EndGetServiceObjectCount(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (int)array[0];
		}

		public void GetServiceObjectCountAsync(GFSTableDescription[] LayerOrTableDescriptions, Geometry Geometry)
		{
			this.GetServiceObjectCountAsync(LayerOrTableDescriptions, Geometry, null);
		}

		public void GetServiceObjectCountAsync(GFSTableDescription[] LayerOrTableDescriptions, Geometry Geometry, object userState)
		{
			if (this.GetServiceObjectCountOperationCompleted == null)
			{
				this.GetServiceObjectCountOperationCompleted = new SendOrPostCallback(this.OnGetServiceObjectCountOperationCompleted);
			}
			base.InvokeAsync("GetServiceObjectCount", new object[]
			{
				LayerOrTableDescriptions,
				Geometry
			}, this.GetServiceObjectCountOperationCompleted, userState);
		}

		private void OnGetServiceObjectCountOperationCompleted(object arg)
		{
			if (this.GetServiceObjectCountCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetServiceObjectCountCompleted(this, new GetServiceObjectCountCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public EditResult[] AddAttachments([XmlElement(Form = XmlSchemaForm.Unqualified)] int LayerOrTableID, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] AttachmentData[] AttachmentDataArray)
		{
			object[] array = base.Invoke("AddAttachments", new object[]
			{
				LayerOrTableID,
				AttachmentDataArray
			});
			return (EditResult[])array[0];
		}

		public IAsyncResult BeginAddAttachments(int LayerOrTableID, AttachmentData[] AttachmentDataArray, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AddAttachments", new object[]
			{
				LayerOrTableID,
				AttachmentDataArray
			}, callback, asyncState);
		}

		public EditResult[] EndAddAttachments(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (EditResult[])array[0];
		}

		public void AddAttachmentsAsync(int LayerOrTableID, AttachmentData[] AttachmentDataArray)
		{
			this.AddAttachmentsAsync(LayerOrTableID, AttachmentDataArray, null);
		}

		public void AddAttachmentsAsync(int LayerOrTableID, AttachmentData[] AttachmentDataArray, object userState)
		{
			if (this.AddAttachmentsOperationCompleted == null)
			{
				this.AddAttachmentsOperationCompleted = new SendOrPostCallback(this.OnAddAttachmentsOperationCompleted);
			}
			base.InvokeAsync("AddAttachments", new object[]
			{
				LayerOrTableID,
				AttachmentDataArray
			}, this.AddAttachmentsOperationCompleted, userState);
		}

		private void OnAddAttachmentsOperationCompleted(object arg)
		{
			if (this.AddAttachmentsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AddAttachmentsCompleted(this, new AddAttachmentsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public EditResult[] Update([XmlElement(Form = XmlSchemaForm.Unqualified)] int LayerOrTableID, [XmlElement(Form = XmlSchemaForm.Unqualified)] DataObjects DataObjects)
		{
			object[] array = base.Invoke("Update", new object[]
			{
				LayerOrTableID,
				DataObjects
			});
			return (EditResult[])array[0];
		}

		public IAsyncResult BeginUpdate(int LayerOrTableID, DataObjects DataObjects, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Update", new object[]
			{
				LayerOrTableID,
				DataObjects
			}, callback, asyncState);
		}

		public EditResult[] EndUpdate(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (EditResult[])array[0];
		}

		public void UpdateAsync(int LayerOrTableID, DataObjects DataObjects)
		{
			this.UpdateAsync(LayerOrTableID, DataObjects, null);
		}

		public void UpdateAsync(int LayerOrTableID, DataObjects DataObjects, object userState)
		{
			if (this.UpdateOperationCompleted == null)
			{
				this.UpdateOperationCompleted = new SendOrPostCallback(this.OnUpdateOperationCompleted);
			}
			base.InvokeAsync("Update", new object[]
			{
				LayerOrTableID,
				DataObjects
			}, this.UpdateOperationCompleted, userState);
		}

		private void OnUpdateOperationCompleted(object arg)
		{
			if (this.UpdateCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateCompleted(this, new UpdateCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public TableEditResult[] ApplyEdits([XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] TableEdit[] TableEdits)
		{
			object[] array = base.Invoke("ApplyEdits", new object[]
			{
				TableEdits
			});
			return (TableEditResult[])array[0];
		}

		public IAsyncResult BeginApplyEdits(TableEdit[] TableEdits, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ApplyEdits", new object[]
			{
				TableEdits
			}, callback, asyncState);
		}

		public TableEditResult[] EndApplyEdits(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (TableEditResult[])array[0];
		}

		public void ApplyEditsAsync(TableEdit[] TableEdits)
		{
			this.ApplyEditsAsync(TableEdits, null);
		}

		public void ApplyEditsAsync(TableEdit[] TableEdits, object userState)
		{
			if (this.ApplyEditsOperationCompleted == null)
			{
				this.ApplyEditsOperationCompleted = new SendOrPostCallback(this.OnApplyEditsOperationCompleted);
			}
			base.InvokeAsync("ApplyEdits", new object[]
			{
				TableEdits
			}, this.ApplyEditsOperationCompleted, userState);
		}

		private void OnApplyEditsOperationCompleted(object arg)
		{
			if (this.ApplyEditsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ApplyEditsCompleted(this, new ApplyEditsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public GraphicFeatureLayer[] GetLayers([XmlElement(Form = XmlSchemaForm.Unqualified)] ServerSymbolOutputOptions SymbolOutputOptions)
		{
			object[] array = base.Invoke("GetLayers", new object[]
			{
				SymbolOutputOptions
			});
			return (GraphicFeatureLayer[])array[0];
		}

		public IAsyncResult BeginGetLayers(ServerSymbolOutputOptions SymbolOutputOptions, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetLayers", new object[]
			{
				SymbolOutputOptions
			}, callback, asyncState);
		}

		public GraphicFeatureLayer[] EndGetLayers(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (GraphicFeatureLayer[])array[0];
		}

		public void GetLayersAsync(ServerSymbolOutputOptions SymbolOutputOptions)
		{
			this.GetLayersAsync(SymbolOutputOptions, null);
		}

		public void GetLayersAsync(ServerSymbolOutputOptions SymbolOutputOptions, object userState)
		{
			if (this.GetLayersOperationCompleted == null)
			{
				this.GetLayersOperationCompleted = new SendOrPostCallback(this.OnGetLayersOperationCompleted);
			}
			base.InvokeAsync("GetLayers", new object[]
			{
				SymbolOutputOptions
			}, this.GetLayersOperationCompleted, userState);
		}

		private void OnGetLayersOperationCompleted(object arg)
		{
			if (this.GetLayersCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetLayersCompleted(this, new GetLayersCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public ServiceData Query([XmlElement(Form = XmlSchemaForm.Unqualified)] int LayerOrTableID, [XmlElement(Form = XmlSchemaForm.Unqualified)] string DefinitionExpression, [XmlElement(Form = XmlSchemaForm.Unqualified)] QueryFilter QueryFilter, [XmlElement(Form = XmlSchemaForm.Unqualified)] ServiceDataOptions ServiceDataOptions)
		{
			object[] array = base.Invoke("Query", new object[]
			{
				LayerOrTableID,
				DefinitionExpression,
				QueryFilter,
				ServiceDataOptions
			});
			return (ServiceData)array[0];
		}

		public IAsyncResult BeginQuery(int LayerOrTableID, string DefinitionExpression, QueryFilter QueryFilter, ServiceDataOptions ServiceDataOptions, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Query", new object[]
			{
				LayerOrTableID,
				DefinitionExpression,
				QueryFilter,
				ServiceDataOptions
			}, callback, asyncState);
		}

		public ServiceData EndQuery(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ServiceData)array[0];
		}

		public void QueryAsync(int LayerOrTableID, string DefinitionExpression, QueryFilter QueryFilter, ServiceDataOptions ServiceDataOptions)
		{
			this.QueryAsync(LayerOrTableID, DefinitionExpression, QueryFilter, ServiceDataOptions, null);
		}

		public void QueryAsync(int LayerOrTableID, string DefinitionExpression, QueryFilter QueryFilter, ServiceDataOptions ServiceDataOptions, object userState)
		{
			if (this.QueryOperationCompleted == null)
			{
				this.QueryOperationCompleted = new SendOrPostCallback(this.OnQueryOperationCompleted);
			}
			base.InvokeAsync("Query", new object[]
			{
				LayerOrTableID,
				DefinitionExpression,
				QueryFilter,
				ServiceDataOptions
			}, this.QueryOperationCompleted, userState);
		}

		private void OnQueryOperationCompleted(object arg)
		{
			if (this.QueryCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.QueryCompleted(this, new QueryCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem("String", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public string[] QueryHTMLPopups([XmlElement(Form = XmlSchemaForm.Unqualified)] int LayerOrTableID, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)] int[] ObjectIDs)
		{
			object[] array = base.Invoke("QueryHTMLPopups", new object[]
			{
				LayerOrTableID,
				ObjectIDs
			});
			return (string[])array[0];
		}

		public IAsyncResult BeginQueryHTMLPopups(int LayerOrTableID, int[] ObjectIDs, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("QueryHTMLPopups", new object[]
			{
				LayerOrTableID,
				ObjectIDs
			}, callback, asyncState);
		}

		public string[] EndQueryHTMLPopups(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (string[])array[0];
		}

		public void QueryHTMLPopupsAsync(int LayerOrTableID, int[] ObjectIDs)
		{
			this.QueryHTMLPopupsAsync(LayerOrTableID, ObjectIDs, null);
		}

		public void QueryHTMLPopupsAsync(int LayerOrTableID, int[] ObjectIDs, object userState)
		{
			if (this.QueryHTMLPopupsOperationCompleted == null)
			{
				this.QueryHTMLPopupsOperationCompleted = new SendOrPostCallback(this.OnQueryHTMLPopupsOperationCompleted);
			}
			base.InvokeAsync("QueryHTMLPopups", new object[]
			{
				LayerOrTableID,
				ObjectIDs
			}, this.QueryHTMLPopupsOperationCompleted, userState);
		}

		private void OnQueryHTMLPopupsOperationCompleted(object arg)
		{
			if (this.QueryHTMLPopupsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.QueryHTMLPopupsCompleted(this, new QueryHTMLPopupsCompletedEventArgs1(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void Delete([XmlElement(Form = XmlSchemaForm.Unqualified)] int LayerOrTableID, [XmlElement(Form = XmlSchemaForm.Unqualified)] string DefinitionExpression, [XmlElement(Form = XmlSchemaForm.Unqualified)] QueryFilter QueryFilter)
		{
			base.Invoke("Delete", new object[]
			{
				LayerOrTableID,
				DefinitionExpression,
				QueryFilter
			});
		}

		public IAsyncResult BeginDelete(int LayerOrTableID, string DefinitionExpression, QueryFilter QueryFilter, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Delete", new object[]
			{
				LayerOrTableID,
				DefinitionExpression,
				QueryFilter
			}, callback, asyncState);
		}

		public void EndDelete(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		public void DeleteAsync(int LayerOrTableID, string DefinitionExpression, QueryFilter QueryFilter)
		{
			this.DeleteAsync(LayerOrTableID, DefinitionExpression, QueryFilter, null);
		}

		public void DeleteAsync(int LayerOrTableID, string DefinitionExpression, QueryFilter QueryFilter, object userState)
		{
			if (this.DeleteOperationCompleted == null)
			{
				this.DeleteOperationCompleted = new SendOrPostCallback(this.OnDeleteOperationCompleted);
			}
			base.InvokeAsync("Delete", new object[]
			{
				LayerOrTableID,
				DefinitionExpression,
				QueryFilter
			}, this.DeleteOperationCompleted, userState);
		}

		private void OnDeleteOperationCompleted(object arg)
		{
			if (this.DeleteCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public DataObjectTable[] GetTables([XmlElement(Form = XmlSchemaForm.Unqualified)] ServerSymbolOutputOptions SymbolOutputOptions, [XmlElement(Form = XmlSchemaForm.Unqualified)] bool IgnoreLayers)
		{
			object[] array = base.Invoke("GetTables", new object[]
			{
				SymbolOutputOptions,
				IgnoreLayers
			});
			return (DataObjectTable[])array[0];
		}

		public IAsyncResult BeginGetTables(ServerSymbolOutputOptions SymbolOutputOptions, bool IgnoreLayers, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetTables", new object[]
			{
				SymbolOutputOptions,
				IgnoreLayers
			}, callback, asyncState);
		}

		public DataObjectTable[] EndGetTables(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (DataObjectTable[])array[0];
		}

		public void GetTablesAsync(ServerSymbolOutputOptions SymbolOutputOptions, bool IgnoreLayers)
		{
			this.GetTablesAsync(SymbolOutputOptions, IgnoreLayers, null);
		}

		public void GetTablesAsync(ServerSymbolOutputOptions SymbolOutputOptions, bool IgnoreLayers, object userState)
		{
			if (this.GetTablesOperationCompleted == null)
			{
				this.GetTablesOperationCompleted = new SendOrPostCallback(this.OnGetTablesOperationCompleted);
			}
			base.InvokeAsync("GetTables", new object[]
			{
				SymbolOutputOptions,
				IgnoreLayers
			}, this.GetTablesOperationCompleted, userState);
		}

		private void OnGetTablesOperationCompleted(object arg)
		{
			if (this.GetTablesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetTablesCompleted(this, new GetTablesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public int GetCount([XmlElement(Form = XmlSchemaForm.Unqualified)] int LayerOrTableID, [XmlElement(Form = XmlSchemaForm.Unqualified)] string DefinitionExpression, [XmlElement(Form = XmlSchemaForm.Unqualified)] QueryFilter QueryFilter)
		{
			object[] array = base.Invoke("GetCount", new object[]
			{
				LayerOrTableID,
				DefinitionExpression,
				QueryFilter
			});
			return (int)array[0];
		}

		public IAsyncResult BeginGetCount(int LayerOrTableID, string DefinitionExpression, QueryFilter QueryFilter, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetCount", new object[]
			{
				LayerOrTableID,
				DefinitionExpression,
				QueryFilter
			}, callback, asyncState);
		}

		public int EndGetCount(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (int)array[0];
		}

		public void GetCountAsync(int LayerOrTableID, string DefinitionExpression, QueryFilter QueryFilter)
		{
			this.GetCountAsync(LayerOrTableID, DefinitionExpression, QueryFilter, null);
		}

		public void GetCountAsync(int LayerOrTableID, string DefinitionExpression, QueryFilter QueryFilter, object userState)
		{
			if (this.GetCountOperationCompleted == null)
			{
				this.GetCountOperationCompleted = new SendOrPostCallback(this.OnGetCountOperationCompleted);
			}
			base.InvokeAsync("GetCount", new object[]
			{
				LayerOrTableID,
				DefinitionExpression,
				QueryFilter
			}, this.GetCountOperationCompleted, userState);
		}

		private void OnGetCountOperationCompleted(object arg)
		{
			if (this.GetCountCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetCountCompleted(this, new GetCountCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public EditResult[] Add([XmlElement(Form = XmlSchemaForm.Unqualified)] int LayerOrTableID, [XmlElement(Form = XmlSchemaForm.Unqualified)] DataObjects DataObjects)
		{
			object[] array = base.Invoke("Add", new object[]
			{
				LayerOrTableID,
				DataObjects
			});
			return (EditResult[])array[0];
		}

		public IAsyncResult BeginAdd(int LayerOrTableID, DataObjects DataObjects, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Add", new object[]
			{
				LayerOrTableID,
				DataObjects
			}, callback, asyncState);
		}

		public EditResult[] EndAdd(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (EditResult[])array[0];
		}

		public void AddAsync(int LayerOrTableID, DataObjects DataObjects)
		{
			this.AddAsync(LayerOrTableID, DataObjects, null);
		}

		public void AddAsync(int LayerOrTableID, DataObjects DataObjects, object userState)
		{
			if (this.AddOperationCompleted == null)
			{
				this.AddOperationCompleted = new SendOrPostCallback(this.OnAddOperationCompleted);
			}
			base.InvokeAsync("Add", new object[]
			{
				LayerOrTableID,
				DataObjects
			}, this.AddOperationCompleted, userState);
		}

		private void OnAddOperationCompleted(object arg)
		{
			if (this.AddCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AddCompleted(this, new AddCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public EditResult[] DeleteAttachments([XmlElement(Form = XmlSchemaForm.Unqualified)] int LayerOrTableID, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)] int[] IDsOfAttachmentsToDelete)
		{
			object[] array = base.Invoke("DeleteAttachments", new object[]
			{
				LayerOrTableID,
				IDsOfAttachmentsToDelete
			});
			return (EditResult[])array[0];
		}

		public IAsyncResult BeginDeleteAttachments(int LayerOrTableID, int[] IDsOfAttachmentsToDelete, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteAttachments", new object[]
			{
				LayerOrTableID,
				IDsOfAttachmentsToDelete
			}, callback, asyncState);
		}

		public EditResult[] EndDeleteAttachments(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (EditResult[])array[0];
		}

		public void DeleteAttachmentsAsync(int LayerOrTableID, int[] IDsOfAttachmentsToDelete)
		{
			this.DeleteAttachmentsAsync(LayerOrTableID, IDsOfAttachmentsToDelete, null);
		}

		public void DeleteAttachmentsAsync(int LayerOrTableID, int[] IDsOfAttachmentsToDelete, object userState)
		{
			if (this.DeleteAttachmentsOperationCompleted == null)
			{
				this.DeleteAttachmentsOperationCompleted = new SendOrPostCallback(this.OnDeleteAttachmentsOperationCompleted);
			}
			base.InvokeAsync("DeleteAttachments", new object[]
			{
				LayerOrTableID,
				IDsOfAttachmentsToDelete
			}, this.DeleteAttachmentsOperationCompleted, userState);
		}

		private void OnDeleteAttachmentsOperationCompleted(object arg)
		{
			if (this.DeleteAttachmentsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteAttachmentsCompleted(this, new DeleteAttachmentsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public ServiceData QueryFromService([XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] GFSTableDescription[] LayerOrTableDescriptions, [XmlElement(Form = XmlSchemaForm.Unqualified)] Geometry Geometry, [XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference OutSR, [XmlElement(Form = XmlSchemaForm.Unqualified)] TimeReference OutTR, [XmlElement(Form = XmlSchemaForm.Unqualified)] ServiceDataOptions ServiceDataOptions)
		{
			object[] array = base.Invoke("QueryFromService", new object[]
			{
				LayerOrTableDescriptions,
				Geometry,
				OutSR,
				OutTR,
				ServiceDataOptions
			});
			return (ServiceData)array[0];
		}

		public IAsyncResult BeginQueryFromService(GFSTableDescription[] LayerOrTableDescriptions, Geometry Geometry, SpatialReference OutSR, TimeReference OutTR, ServiceDataOptions ServiceDataOptions, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("QueryFromService", new object[]
			{
				LayerOrTableDescriptions,
				Geometry,
				OutSR,
				OutTR,
				ServiceDataOptions
			}, callback, asyncState);
		}

		public ServiceData EndQueryFromService(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ServiceData)array[0];
		}

		public void QueryFromServiceAsync(GFSTableDescription[] LayerOrTableDescriptions, Geometry Geometry, SpatialReference OutSR, TimeReference OutTR, ServiceDataOptions ServiceDataOptions)
		{
			this.QueryFromServiceAsync(LayerOrTableDescriptions, Geometry, OutSR, OutTR, ServiceDataOptions, null);
		}

		public void QueryFromServiceAsync(GFSTableDescription[] LayerOrTableDescriptions, Geometry Geometry, SpatialReference OutSR, TimeReference OutTR, ServiceDataOptions ServiceDataOptions, object userState)
		{
			if (this.QueryFromServiceOperationCompleted == null)
			{
				this.QueryFromServiceOperationCompleted = new SendOrPostCallback(this.OnQueryFromServiceOperationCompleted);
			}
			base.InvokeAsync("QueryFromService", new object[]
			{
				LayerOrTableDescriptions,
				Geometry,
				OutSR,
				OutTR,
				ServiceDataOptions
			}, this.QueryFromServiceOperationCompleted, userState);
		}

		private void OnQueryFromServiceOperationCompleted(object arg)
		{
			if (this.QueryFromServiceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.QueryFromServiceCompleted(this, new QueryFromServiceCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public EditResult[] DeleteByID([XmlElement(Form = XmlSchemaForm.Unqualified)] int LayerOrTableID, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)] int[] IDsOfObjectsToDelete)
		{
			object[] array = base.Invoke("DeleteByID", new object[]
			{
				LayerOrTableID,
				IDsOfObjectsToDelete
			});
			return (EditResult[])array[0];
		}

		public IAsyncResult BeginDeleteByID(int LayerOrTableID, int[] IDsOfObjectsToDelete, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteByID", new object[]
			{
				LayerOrTableID,
				IDsOfObjectsToDelete
			}, callback, asyncState);
		}

		public EditResult[] EndDeleteByID(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (EditResult[])array[0];
		}

		public void DeleteByIDAsync(int LayerOrTableID, int[] IDsOfObjectsToDelete)
		{
			this.DeleteByIDAsync(LayerOrTableID, IDsOfObjectsToDelete, null);
		}

		public void DeleteByIDAsync(int LayerOrTableID, int[] IDsOfObjectsToDelete, object userState)
		{
			if (this.DeleteByIDOperationCompleted == null)
			{
				this.DeleteByIDOperationCompleted = new SendOrPostCallback(this.OnDeleteByIDOperationCompleted);
			}
			base.InvokeAsync("DeleteByID", new object[]
			{
				LayerOrTableID,
				IDsOfObjectsToDelete
			}, this.DeleteByIDOperationCompleted, userState);
		}

		private void OnDeleteByIDOperationCompleted(object arg)
		{
			if (this.DeleteByIDCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteByIDCompleted(this, new DeleteByIDCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public EditResult[] UpdateAttachments([XmlElement(Form = XmlSchemaForm.Unqualified)] int LayerOrTableID, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] AttachmentData[] AttachmentDataArray)
		{
			object[] array = base.Invoke("UpdateAttachments", new object[]
			{
				LayerOrTableID,
				AttachmentDataArray
			});
			return (EditResult[])array[0];
		}

		public IAsyncResult BeginUpdateAttachments(int LayerOrTableID, AttachmentData[] AttachmentDataArray, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateAttachments", new object[]
			{
				LayerOrTableID,
				AttachmentDataArray
			}, callback, asyncState);
		}

		public EditResult[] EndUpdateAttachments(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (EditResult[])array[0];
		}

		public void UpdateAttachmentsAsync(int LayerOrTableID, AttachmentData[] AttachmentDataArray)
		{
			this.UpdateAttachmentsAsync(LayerOrTableID, AttachmentDataArray, null);
		}

		public void UpdateAttachmentsAsync(int LayerOrTableID, AttachmentData[] AttachmentDataArray, object userState)
		{
			if (this.UpdateAttachmentsOperationCompleted == null)
			{
				this.UpdateAttachmentsOperationCompleted = new SendOrPostCallback(this.OnUpdateAttachmentsOperationCompleted);
			}
			base.InvokeAsync("UpdateAttachments", new object[]
			{
				LayerOrTableID,
				AttachmentDataArray
			}, this.UpdateAttachmentsOperationCompleted, userState);
		}

		private void OnUpdateAttachmentsOperationCompleted(object arg)
		{
			if (this.UpdateAttachmentsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateAttachmentsCompleted(this, new UpdateAttachmentsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public ServiceData QueryRelatedObjects([XmlElement(Form = XmlSchemaForm.Unqualified)] int SourceLayerOrTableID, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)] int[] ObjectIDsInSource, [XmlElement(Form = XmlSchemaForm.Unqualified)] int RelationshipID, [XmlElement(Form = XmlSchemaForm.Unqualified)] string TargetDefinitionExpression, [XmlElement(Form = XmlSchemaForm.Unqualified)] string TargetTableProps, [XmlElement(Form = XmlSchemaForm.Unqualified)] bool GroupBySourceOIDs, [XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference OutSR, [XmlElement(Form = XmlSchemaForm.Unqualified)] TimeReference OutTR, [XmlElement(Form = XmlSchemaForm.Unqualified)] ServiceDataOptions ServiceDataOptions)
		{
			object[] array = base.Invoke("QueryRelatedObjects", new object[]
			{
				SourceLayerOrTableID,
				ObjectIDsInSource,
				RelationshipID,
				TargetDefinitionExpression,
				TargetTableProps,
				GroupBySourceOIDs,
				OutSR,
				OutTR,
				ServiceDataOptions
			});
			return (ServiceData)array[0];
		}

		public IAsyncResult BeginQueryRelatedObjects(int SourceLayerOrTableID, int[] ObjectIDsInSource, int RelationshipID, string TargetDefinitionExpression, string TargetTableProps, bool GroupBySourceOIDs, SpatialReference OutSR, TimeReference OutTR, ServiceDataOptions ServiceDataOptions, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("QueryRelatedObjects", new object[]
			{
				SourceLayerOrTableID,
				ObjectIDsInSource,
				RelationshipID,
				TargetDefinitionExpression,
				TargetTableProps,
				GroupBySourceOIDs,
				OutSR,
				OutTR,
				ServiceDataOptions
			}, callback, asyncState);
		}

		public ServiceData EndQueryRelatedObjects(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ServiceData)array[0];
		}

		public void QueryRelatedObjectsAsync(int SourceLayerOrTableID, int[] ObjectIDsInSource, int RelationshipID, string TargetDefinitionExpression, string TargetTableProps, bool GroupBySourceOIDs, SpatialReference OutSR, TimeReference OutTR, ServiceDataOptions ServiceDataOptions)
		{
			this.QueryRelatedObjectsAsync(SourceLayerOrTableID, ObjectIDsInSource, RelationshipID, TargetDefinitionExpression, TargetTableProps, GroupBySourceOIDs, OutSR, OutTR, ServiceDataOptions, null);
		}

		public void QueryRelatedObjectsAsync(int SourceLayerOrTableID, int[] ObjectIDsInSource, int RelationshipID, string TargetDefinitionExpression, string TargetTableProps, bool GroupBySourceOIDs, SpatialReference OutSR, TimeReference OutTR, ServiceDataOptions ServiceDataOptions, object userState)
		{
			if (this.QueryRelatedObjectsOperationCompleted == null)
			{
				this.QueryRelatedObjectsOperationCompleted = new SendOrPostCallback(this.OnQueryRelatedObjectsOperationCompleted);
			}
			base.InvokeAsync("QueryRelatedObjects", new object[]
			{
				SourceLayerOrTableID,
				ObjectIDsInSource,
				RelationshipID,
				TargetDefinitionExpression,
				TargetTableProps,
				GroupBySourceOIDs,
				OutSR,
				OutTR,
				ServiceDataOptions
			}, this.QueryRelatedObjectsOperationCompleted, userState);
		}

		private void OnQueryRelatedObjectsOperationCompleted(object arg)
		{
			if (this.QueryRelatedObjectsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.QueryRelatedObjectsCompleted(this, new QueryRelatedObjectsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public AttachmentData[] QueryAttachmentData([XmlElement(Form = XmlSchemaForm.Unqualified)] int LayerOrTableID, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)] int[] AttachmentIDs, [XmlElement(Form = XmlSchemaForm.Unqualified)] esriTransportType TransportType)
		{
			object[] array = base.Invoke("QueryAttachmentData", new object[]
			{
				LayerOrTableID,
				AttachmentIDs,
				TransportType
			});
			return (AttachmentData[])array[0];
		}

		public IAsyncResult BeginQueryAttachmentData(int LayerOrTableID, int[] AttachmentIDs, esriTransportType TransportType, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("QueryAttachmentData", new object[]
			{
				LayerOrTableID,
				AttachmentIDs,
				TransportType
			}, callback, asyncState);
		}

		public AttachmentData[] EndQueryAttachmentData(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (AttachmentData[])array[0];
		}

		public void QueryAttachmentDataAsync(int LayerOrTableID, int[] AttachmentIDs, esriTransportType TransportType)
		{
			this.QueryAttachmentDataAsync(LayerOrTableID, AttachmentIDs, TransportType, null);
		}

		public void QueryAttachmentDataAsync(int LayerOrTableID, int[] AttachmentIDs, esriTransportType TransportType, object userState)
		{
			if (this.QueryAttachmentDataOperationCompleted == null)
			{
				this.QueryAttachmentDataOperationCompleted = new SendOrPostCallback(this.OnQueryAttachmentDataOperationCompleted);
			}
			base.InvokeAsync("QueryAttachmentData", new object[]
			{
				LayerOrTableID,
				AttachmentIDs,
				TransportType
			}, this.QueryAttachmentDataOperationCompleted, userState);
		}

		private void OnQueryAttachmentDataOperationCompleted(object arg)
		{
			if (this.QueryAttachmentDataCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.QueryAttachmentDataCompleted(this, new QueryAttachmentDataCompletedEventArgs1(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public int[] QueryIDs([XmlElement(Form = XmlSchemaForm.Unqualified)] int LayerOrTableID, [XmlElement(Form = XmlSchemaForm.Unqualified)] string DefinitionExpression, [XmlElement(Form = XmlSchemaForm.Unqualified)] QueryFilter QueryFilter)
		{
			object[] array = base.Invoke("QueryIDs", new object[]
			{
				LayerOrTableID,
				DefinitionExpression,
				QueryFilter
			});
			return (int[])array[0];
		}

		public IAsyncResult BeginQueryIDs(int LayerOrTableID, string DefinitionExpression, QueryFilter QueryFilter, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("QueryIDs", new object[]
			{
				LayerOrTableID,
				DefinitionExpression,
				QueryFilter
			}, callback, asyncState);
		}

		public int[] EndQueryIDs(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (int[])array[0];
		}

		public void QueryIDsAsync(int LayerOrTableID, string DefinitionExpression, QueryFilter QueryFilter)
		{
			this.QueryIDsAsync(LayerOrTableID, DefinitionExpression, QueryFilter, null);
		}

		public void QueryIDsAsync(int LayerOrTableID, string DefinitionExpression, QueryFilter QueryFilter, object userState)
		{
			if (this.QueryIDsOperationCompleted == null)
			{
				this.QueryIDsOperationCompleted = new SendOrPostCallback(this.OnQueryIDsOperationCompleted);
			}
			base.InvokeAsync("QueryIDs", new object[]
			{
				LayerOrTableID,
				DefinitionExpression,
				QueryFilter
			}, this.QueryIDsOperationCompleted, userState);
		}

		private void OnQueryIDsOperationCompleted(object arg)
		{
			if (this.QueryIDsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.QueryIDsCompleted(this, new QueryIDsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}
	}
}
