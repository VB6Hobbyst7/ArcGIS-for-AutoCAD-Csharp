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
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, WebServiceBinding(Name = "GeometryServerBinding", Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	public class Geometry_GeometryServer : SoapHttpClientProtocol
	{
		private SendOrPostCallback GetAreasAndLengthsOperationCompleted;

		private SendOrPostCallback FindUnitsByWKIDOperationCompleted;

		private SendOrPostCallback ReshapeOperationCompleted;

		private SendOrPostCallback GetLengthsGeodesicOperationCompleted;

		private SendOrPostCallback GeneralizeOperationCompleted;

		private SendOrPostCallback TrimExtendOperationCompleted;

		private SendOrPostCallback ConvexHullOperationCompleted;

		private SendOrPostCallback GetLabelPointsOperationCompleted;

		private SendOrPostCallback UnionOperationCompleted;

		private SendOrPostCallback ProjectOperationCompleted;

		private SendOrPostCallback DensifyGeodesicOperationCompleted;

		private SendOrPostCallback FindSRByWKIDOperationCompleted;

		private SendOrPostCallback GetAreasAndLengths2OperationCompleted;

		private SendOrPostCallback BufferOperationCompleted;

		private SendOrPostCallback DensifyOperationCompleted;

		private SendOrPostCallback GetLengths2OperationCompleted;

		private SendOrPostCallback RelationOperationCompleted;

		private SendOrPostCallback AutoCompleteOperationCompleted;

		private SendOrPostCallback FindSRByWKTOperationCompleted;

		private SendOrPostCallback OffsetOperationCompleted;

		private SendOrPostCallback CutOperationCompleted;

		private SendOrPostCallback IntersectOperationCompleted;

		private SendOrPostCallback GetDistanceGeodesicOperationCompleted;

		private SendOrPostCallback SimplifyOperationCompleted;

		private SendOrPostCallback DifferenceOperationCompleted;

		private SendOrPostCallback FindUnitsByWKTOperationCompleted;

		private SendOrPostCallback GetDistanceOperationCompleted;

		private SendOrPostCallback GetLengthsOperationCompleted;

		public event GetAreasAndLengthsCompletedEventHandler GetAreasAndLengthsCompleted;

		public event FindUnitsByWKIDCompletedEventHandler FindUnitsByWKIDCompleted;

		public event ReshapeCompletedEventHandler ReshapeCompleted;

		public event GetLengthsGeodesicCompletedEventHandler GetLengthsGeodesicCompleted;

		public event GeneralizeCompletedEventHandler GeneralizeCompleted;

		public event TrimExtendCompletedEventHandler TrimExtendCompleted;

		public event ConvexHullCompletedEventHandler ConvexHullCompleted;

		public event GetLabelPointsCompletedEventHandler GetLabelPointsCompleted;

		public event UnionCompletedEventHandler UnionCompleted;

		public event ProjectCompletedEventHandler ProjectCompleted;

		public event DensifyGeodesicCompletedEventHandler DensifyGeodesicCompleted;

		public event FindSRByWKIDCompletedEventHandler FindSRByWKIDCompleted;

		public event GetAreasAndLengths2CompletedEventHandler GetAreasAndLengths2Completed;

		public event BufferCompletedEventHandler BufferCompleted;

		public event DensifyCompletedEventHandler DensifyCompleted;

		public event GetLengths2CompletedEventHandler GetLengths2Completed;

		public event RelationCompletedEventHandler RelationCompleted;

		public event AutoCompleteCompletedEventHandler AutoCompleteCompleted;

		public event FindSRByWKTCompletedEventHandler FindSRByWKTCompleted;

		public event OffsetCompletedEventHandler OffsetCompleted;

		public event CutCompletedEventHandler CutCompleted;

		public event IntersectCompletedEventHandler IntersectCompleted;

		public event GetDistanceGeodesicCompletedEventHandler GetDistanceGeodesicCompleted;

		public event SimplifyCompletedEventHandler SimplifyCompleted;

		public event DifferenceCompletedEventHandler DifferenceCompleted;

		public event FindUnitsByWKTCompletedEventHandler FindUnitsByWKTCompleted;

		public event GetDistanceCompletedEventHandler GetDistanceCompleted;

		public event GetLengthsCompletedEventHandler GetLengthsCompleted;

		public Geometry_GeometryServer()
		{
			base.Url = "http://sampleserver3.arcgisonline.com/arcgis/services/Geometry/GeometryServer";
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Areas", Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public double[] GetAreasAndLengths([XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference SpatialReference, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] Polygon[] InPolygonArray, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)] out double[] Lengths)
		{
			object[] array = base.Invoke("GetAreasAndLengths", new object[]
			{
				SpatialReference,
				InPolygonArray
			});
			Lengths = (double[])array[1];
			return (double[])array[0];
		}

		public IAsyncResult BeginGetAreasAndLengths(SpatialReference SpatialReference, Polygon[] InPolygonArray, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetAreasAndLengths", new object[]
			{
				SpatialReference,
				InPolygonArray
			}, callback, asyncState);
		}

		public double[] EndGetAreasAndLengths(IAsyncResult asyncResult, out double[] Lengths)
		{
			object[] array = base.EndInvoke(asyncResult);
			Lengths = (double[])array[1];
			return (double[])array[0];
		}

		public void GetAreasAndLengthsAsync(SpatialReference SpatialReference, Polygon[] InPolygonArray)
		{
			this.GetAreasAndLengthsAsync(SpatialReference, InPolygonArray, null);
		}

		public void GetAreasAndLengthsAsync(SpatialReference SpatialReference, Polygon[] InPolygonArray, object userState)
		{
			if (this.GetAreasAndLengthsOperationCompleted == null)
			{
				this.GetAreasAndLengthsOperationCompleted = new SendOrPostCallback(this.OnGetAreasAndLengthsOperationCompleted);
			}
			base.InvokeAsync("GetAreasAndLengths", new object[]
			{
				SpatialReference,
				InPolygonArray
			}, this.GetAreasAndLengthsOperationCompleted, userState);
		}

		private void OnGetAreasAndLengthsOperationCompleted(object arg)
		{
			if (this.GetAreasAndLengthsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetAreasAndLengthsCompleted(this, new GetAreasAndLengthsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public Unit FindUnitsByWKID([XmlElement(Form = XmlSchemaForm.Unqualified)] string authority, [XmlElement(Form = XmlSchemaForm.Unqualified)] int WKID)
		{
			object[] array = base.Invoke("FindUnitsByWKID", new object[]
			{
				authority,
				WKID
			});
			return (Unit)array[0];
		}

		public IAsyncResult BeginFindUnitsByWKID(string authority, int WKID, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("FindUnitsByWKID", new object[]
			{
				authority,
				WKID
			}, callback, asyncState);
		}

		public Unit EndFindUnitsByWKID(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Unit)array[0];
		}

		public void FindUnitsByWKIDAsync(string authority, int WKID)
		{
			this.FindUnitsByWKIDAsync(authority, WKID, null);
		}

		public void FindUnitsByWKIDAsync(string authority, int WKID, object userState)
		{
			if (this.FindUnitsByWKIDOperationCompleted == null)
			{
				this.FindUnitsByWKIDOperationCompleted = new SendOrPostCallback(this.OnFindUnitsByWKIDOperationCompleted);
			}
			base.InvokeAsync("FindUnitsByWKID", new object[]
			{
				authority,
				WKID
			}, this.FindUnitsByWKIDOperationCompleted, userState);
		}

		private void OnFindUnitsByWKIDOperationCompleted(object arg)
		{
			if (this.FindUnitsByWKIDCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.FindUnitsByWKIDCompleted(this, new FindUnitsByWKIDCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public Geometry Reshape([XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference SpatialReference, [XmlElement(Form = XmlSchemaForm.Unqualified)] Geometry Target, [XmlElement(Form = XmlSchemaForm.Unqualified)] Polyline Reshaper)
		{
			object[] array = base.Invoke("Reshape", new object[]
			{
				SpatialReference,
				Target,
				Reshaper
			});
			return (Geometry)array[0];
		}

		public IAsyncResult BeginReshape(SpatialReference SpatialReference, Geometry Target, Polyline Reshaper, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Reshape", new object[]
			{
				SpatialReference,
				Target,
				Reshaper
			}, callback, asyncState);
		}

		public Geometry EndReshape(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Geometry)array[0];
		}

		public void ReshapeAsync(SpatialReference SpatialReference, Geometry Target, Polyline Reshaper)
		{
			this.ReshapeAsync(SpatialReference, Target, Reshaper, null);
		}

		public void ReshapeAsync(SpatialReference SpatialReference, Geometry Target, Polyline Reshaper, object userState)
		{
			if (this.ReshapeOperationCompleted == null)
			{
				this.ReshapeOperationCompleted = new SendOrPostCallback(this.OnReshapeOperationCompleted);
			}
			base.InvokeAsync("Reshape", new object[]
			{
				SpatialReference,
				Target,
				Reshaper
			}, this.ReshapeOperationCompleted, userState);
		}

		private void OnReshapeOperationCompleted(object arg)
		{
			if (this.ReshapeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ReshapeCompleted(this, new ReshapeCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public double[] GetLengthsGeodesic([XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference SpatialReference, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] Polyline[] InPolylineArray, [XmlElement(Form = XmlSchemaForm.Unqualified)] LinearUnit LengthUnit)
		{
			object[] array = base.Invoke("GetLengthsGeodesic", new object[]
			{
				SpatialReference,
				InPolylineArray,
				LengthUnit
			});
			return (double[])array[0];
		}

		public IAsyncResult BeginGetLengthsGeodesic(SpatialReference SpatialReference, Polyline[] InPolylineArray, LinearUnit LengthUnit, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetLengthsGeodesic", new object[]
			{
				SpatialReference,
				InPolylineArray,
				LengthUnit
			}, callback, asyncState);
		}

		public double[] EndGetLengthsGeodesic(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (double[])array[0];
		}

		public void GetLengthsGeodesicAsync(SpatialReference SpatialReference, Polyline[] InPolylineArray, LinearUnit LengthUnit)
		{
			this.GetLengthsGeodesicAsync(SpatialReference, InPolylineArray, LengthUnit, null);
		}

		public void GetLengthsGeodesicAsync(SpatialReference SpatialReference, Polyline[] InPolylineArray, LinearUnit LengthUnit, object userState)
		{
			if (this.GetLengthsGeodesicOperationCompleted == null)
			{
				this.GetLengthsGeodesicOperationCompleted = new SendOrPostCallback(this.OnGetLengthsGeodesicOperationCompleted);
			}
			base.InvokeAsync("GetLengthsGeodesic", new object[]
			{
				SpatialReference,
				InPolylineArray,
				LengthUnit
			}, this.GetLengthsGeodesicOperationCompleted, userState);
		}

		private void OnGetLengthsGeodesicOperationCompleted(object arg)
		{
			if (this.GetLengthsGeodesicCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetLengthsGeodesicCompleted(this, new GetLengthsGeodesicCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public Geometry[] Generalize([XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference SpatialReference, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] Geometry[] InGeometryArray, [XmlElement(Form = XmlSchemaForm.Unqualified)] double MaxDeviation, [XmlElement(Form = XmlSchemaForm.Unqualified)] LinearUnit DeviationUnit)
		{
			object[] array = base.Invoke("Generalize", new object[]
			{
				SpatialReference,
				InGeometryArray,
				MaxDeviation,
				DeviationUnit
			});
			return (Geometry[])array[0];
		}

		public IAsyncResult BeginGeneralize(SpatialReference SpatialReference, Geometry[] InGeometryArray, double MaxDeviation, LinearUnit DeviationUnit, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Generalize", new object[]
			{
				SpatialReference,
				InGeometryArray,
				MaxDeviation,
				DeviationUnit
			}, callback, asyncState);
		}

		public Geometry[] EndGeneralize(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Geometry[])array[0];
		}

		public void GeneralizeAsync(SpatialReference SpatialReference, Geometry[] InGeometryArray, double MaxDeviation, LinearUnit DeviationUnit)
		{
			this.GeneralizeAsync(SpatialReference, InGeometryArray, MaxDeviation, DeviationUnit, null);
		}

		public void GeneralizeAsync(SpatialReference SpatialReference, Geometry[] InGeometryArray, double MaxDeviation, LinearUnit DeviationUnit, object userState)
		{
			if (this.GeneralizeOperationCompleted == null)
			{
				this.GeneralizeOperationCompleted = new SendOrPostCallback(this.OnGeneralizeOperationCompleted);
			}
			base.InvokeAsync("Generalize", new object[]
			{
				SpatialReference,
				InGeometryArray,
				MaxDeviation,
				DeviationUnit
			}, this.GeneralizeOperationCompleted, userState);
		}

		private void OnGeneralizeOperationCompleted(object arg)
		{
			if (this.GeneralizeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GeneralizeCompleted(this, new GeneralizeCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public Polyline[] TrimExtend([XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference SpatialReference, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] Polyline[] InPolylineArray, [XmlElement(Form = XmlSchemaForm.Unqualified)] Polyline TrimExtendTo, [XmlElement(Form = XmlSchemaForm.Unqualified)] int ExtendHow)
		{
			object[] array = base.Invoke("TrimExtend", new object[]
			{
				SpatialReference,
				InPolylineArray,
				TrimExtendTo,
				ExtendHow
			});
			return (Polyline[])array[0];
		}

		public IAsyncResult BeginTrimExtend(SpatialReference SpatialReference, Polyline[] InPolylineArray, Polyline TrimExtendTo, int ExtendHow, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("TrimExtend", new object[]
			{
				SpatialReference,
				InPolylineArray,
				TrimExtendTo,
				ExtendHow
			}, callback, asyncState);
		}

		public Polyline[] EndTrimExtend(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Polyline[])array[0];
		}

		public void TrimExtendAsync(SpatialReference SpatialReference, Polyline[] InPolylineArray, Polyline TrimExtendTo, int ExtendHow)
		{
			this.TrimExtendAsync(SpatialReference, InPolylineArray, TrimExtendTo, ExtendHow, null);
		}

		public void TrimExtendAsync(SpatialReference SpatialReference, Polyline[] InPolylineArray, Polyline TrimExtendTo, int ExtendHow, object userState)
		{
			if (this.TrimExtendOperationCompleted == null)
			{
				this.TrimExtendOperationCompleted = new SendOrPostCallback(this.OnTrimExtendOperationCompleted);
			}
			base.InvokeAsync("TrimExtend", new object[]
			{
				SpatialReference,
				InPolylineArray,
				TrimExtendTo,
				ExtendHow
			}, this.TrimExtendOperationCompleted, userState);
		}

		private void OnTrimExtendOperationCompleted(object arg)
		{
			if (this.TrimExtendCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.TrimExtendCompleted(this, new TrimExtendCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public Geometry ConvexHull([XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference SpatialReference, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] Geometry[] InGeometryArray)
		{
			object[] array = base.Invoke("ConvexHull", new object[]
			{
				SpatialReference,
				InGeometryArray
			});
			return (Geometry)array[0];
		}

		public IAsyncResult BeginConvexHull(SpatialReference SpatialReference, Geometry[] InGeometryArray, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ConvexHull", new object[]
			{
				SpatialReference,
				InGeometryArray
			}, callback, asyncState);
		}

		public Geometry EndConvexHull(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Geometry)array[0];
		}

		public void ConvexHullAsync(SpatialReference SpatialReference, Geometry[] InGeometryArray)
		{
			this.ConvexHullAsync(SpatialReference, InGeometryArray, null);
		}

		public void ConvexHullAsync(SpatialReference SpatialReference, Geometry[] InGeometryArray, object userState)
		{
			if (this.ConvexHullOperationCompleted == null)
			{
				this.ConvexHullOperationCompleted = new SendOrPostCallback(this.OnConvexHullOperationCompleted);
			}
			base.InvokeAsync("ConvexHull", new object[]
			{
				SpatialReference,
				InGeometryArray
			}, this.ConvexHullOperationCompleted, userState);
		}

		private void OnConvexHullOperationCompleted(object arg)
		{
			if (this.ConvexHullCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ConvexHullCompleted(this, new ConvexHullCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public Point[] GetLabelPoints([XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference SpatialReference, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] Polygon[] InPolygonArray)
		{
			object[] array = base.Invoke("GetLabelPoints", new object[]
			{
				SpatialReference,
				InPolygonArray
			});
			return (Point[])array[0];
		}

		public IAsyncResult BeginGetLabelPoints(SpatialReference SpatialReference, Polygon[] InPolygonArray, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetLabelPoints", new object[]
			{
				SpatialReference,
				InPolygonArray
			}, callback, asyncState);
		}

		public Point[] EndGetLabelPoints(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Point[])array[0];
		}

		public void GetLabelPointsAsync(SpatialReference SpatialReference, Polygon[] InPolygonArray)
		{
			this.GetLabelPointsAsync(SpatialReference, InPolygonArray, null);
		}

		public void GetLabelPointsAsync(SpatialReference SpatialReference, Polygon[] InPolygonArray, object userState)
		{
			if (this.GetLabelPointsOperationCompleted == null)
			{
				this.GetLabelPointsOperationCompleted = new SendOrPostCallback(this.OnGetLabelPointsOperationCompleted);
			}
			base.InvokeAsync("GetLabelPoints", new object[]
			{
				SpatialReference,
				InPolygonArray
			}, this.GetLabelPointsOperationCompleted, userState);
		}

		private void OnGetLabelPointsOperationCompleted(object arg)
		{
			if (this.GetLabelPointsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetLabelPointsCompleted(this, new GetLabelPointsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public Geometry Union([XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference SpatialReference, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] Geometry[] InGeometryArray)
		{
			object[] array = base.Invoke("Union", new object[]
			{
				SpatialReference,
				InGeometryArray
			});
			return (Geometry)array[0];
		}

		public IAsyncResult BeginUnion(SpatialReference SpatialReference, Geometry[] InGeometryArray, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Union", new object[]
			{
				SpatialReference,
				InGeometryArray
			}, callback, asyncState);
		}

		public Geometry EndUnion(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Geometry)array[0];
		}

		public void UnionAsync(SpatialReference SpatialReference, Geometry[] InGeometryArray)
		{
			this.UnionAsync(SpatialReference, InGeometryArray, null);
		}

		public void UnionAsync(SpatialReference SpatialReference, Geometry[] InGeometryArray, object userState)
		{
			if (this.UnionOperationCompleted == null)
			{
				this.UnionOperationCompleted = new SendOrPostCallback(this.OnUnionOperationCompleted);
			}
			base.InvokeAsync("Union", new object[]
			{
				SpatialReference,
				InGeometryArray
			}, this.UnionOperationCompleted, userState);
		}

		private void OnUnionOperationCompleted(object arg)
		{
			if (this.UnionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UnionCompleted(this, new UnionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public Geometry[] Project([XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference InSpatialReference, [XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference OutSpatialReference, [XmlElement(Form = XmlSchemaForm.Unqualified)] bool TransformForward, [XmlElement(Form = XmlSchemaForm.Unqualified)] GeoTransformation Transformation, [XmlElement(Form = XmlSchemaForm.Unqualified)] Envelope Extent, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] Geometry[] InGeometryArray)
		{
			object[] array = base.Invoke("Project", new object[]
			{
				InSpatialReference,
				OutSpatialReference,
				TransformForward,
				Transformation,
				Extent,
				InGeometryArray
			});
			return (Geometry[])array[0];
		}

		public IAsyncResult BeginProject(SpatialReference InSpatialReference, SpatialReference OutSpatialReference, bool TransformForward, GeoTransformation Transformation, Envelope Extent, Geometry[] InGeometryArray, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Project", new object[]
			{
				InSpatialReference,
				OutSpatialReference,
				TransformForward,
				Transformation,
				Extent,
				InGeometryArray
			}, callback, asyncState);
		}

		public Geometry[] EndProject(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Geometry[])array[0];
		}

		public void ProjectAsync(SpatialReference InSpatialReference, SpatialReference OutSpatialReference, bool TransformForward, GeoTransformation Transformation, Envelope Extent, Geometry[] InGeometryArray)
		{
			this.ProjectAsync(InSpatialReference, OutSpatialReference, TransformForward, Transformation, Extent, InGeometryArray, null);
		}

		public void ProjectAsync(SpatialReference InSpatialReference, SpatialReference OutSpatialReference, bool TransformForward, GeoTransformation Transformation, Envelope Extent, Geometry[] InGeometryArray, object userState)
		{
			if (this.ProjectOperationCompleted == null)
			{
				this.ProjectOperationCompleted = new SendOrPostCallback(this.OnProjectOperationCompleted);
			}
			base.InvokeAsync("Project", new object[]
			{
				InSpatialReference,
				OutSpatialReference,
				TransformForward,
				Transformation,
				Extent,
				InGeometryArray
			}, this.ProjectOperationCompleted, userState);
		}

		private void OnProjectOperationCompleted(object arg)
		{
			if (this.ProjectCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ProjectCompleted(this, new ProjectCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public Geometry[] DensifyGeodesic([XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference SpatialReference, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] Geometry[] InGeometryArray, [XmlElement(Form = XmlSchemaForm.Unqualified)] double MaxSegmentLength, [XmlElement(Form = XmlSchemaForm.Unqualified)] LinearUnit LengthUnit)
		{
			object[] array = base.Invoke("DensifyGeodesic", new object[]
			{
				SpatialReference,
				InGeometryArray,
				MaxSegmentLength,
				LengthUnit
			});
			return (Geometry[])array[0];
		}

		public IAsyncResult BeginDensifyGeodesic(SpatialReference SpatialReference, Geometry[] InGeometryArray, double MaxSegmentLength, LinearUnit LengthUnit, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DensifyGeodesic", new object[]
			{
				SpatialReference,
				InGeometryArray,
				MaxSegmentLength,
				LengthUnit
			}, callback, asyncState);
		}

		public Geometry[] EndDensifyGeodesic(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Geometry[])array[0];
		}

		public void DensifyGeodesicAsync(SpatialReference SpatialReference, Geometry[] InGeometryArray, double MaxSegmentLength, LinearUnit LengthUnit)
		{
			this.DensifyGeodesicAsync(SpatialReference, InGeometryArray, MaxSegmentLength, LengthUnit, null);
		}

		public void DensifyGeodesicAsync(SpatialReference SpatialReference, Geometry[] InGeometryArray, double MaxSegmentLength, LinearUnit LengthUnit, object userState)
		{
			if (this.DensifyGeodesicOperationCompleted == null)
			{
				this.DensifyGeodesicOperationCompleted = new SendOrPostCallback(this.OnDensifyGeodesicOperationCompleted);
			}
			base.InvokeAsync("DensifyGeodesic", new object[]
			{
				SpatialReference,
				InGeometryArray,
				MaxSegmentLength,
				LengthUnit
			}, this.DensifyGeodesicOperationCompleted, userState);
		}

		private void OnDensifyGeodesicOperationCompleted(object arg)
		{
			if (this.DensifyGeodesicCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DensifyGeodesicCompleted(this, new DensifyGeodesicCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public SpatialReference FindSRByWKID([XmlElement(Form = XmlSchemaForm.Unqualified)] string authority, [XmlElement(Form = XmlSchemaForm.Unqualified)] int WKID, [XmlElement(Form = XmlSchemaForm.Unqualified)] int WKID_Z, [XmlElement(Form = XmlSchemaForm.Unqualified)] bool DefaultXYResolution, [XmlElement(Form = XmlSchemaForm.Unqualified)] bool DefaultXYTolerance)
		{
			object[] array = base.Invoke("FindSRByWKID", new object[]
			{
				authority,
				WKID,
				WKID_Z,
				DefaultXYResolution,
				DefaultXYTolerance
			});
			return (SpatialReference)array[0];
		}

		public IAsyncResult BeginFindSRByWKID(string authority, int WKID, int WKID_Z, bool DefaultXYResolution, bool DefaultXYTolerance, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("FindSRByWKID", new object[]
			{
				authority,
				WKID,
				WKID_Z,
				DefaultXYResolution,
				DefaultXYTolerance
			}, callback, asyncState);
		}

		public SpatialReference EndFindSRByWKID(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (SpatialReference)array[0];
		}

		public void FindSRByWKIDAsync(string authority, int WKID, int WKID_Z, bool DefaultXYResolution, bool DefaultXYTolerance)
		{
			this.FindSRByWKIDAsync(authority, WKID, WKID_Z, DefaultXYResolution, DefaultXYTolerance, null);
		}

		public void FindSRByWKIDAsync(string authority, int WKID, int WKID_Z, bool DefaultXYResolution, bool DefaultXYTolerance, object userState)
		{
			if (this.FindSRByWKIDOperationCompleted == null)
			{
				this.FindSRByWKIDOperationCompleted = new SendOrPostCallback(this.OnFindSRByWKIDOperationCompleted);
			}
			base.InvokeAsync("FindSRByWKID", new object[]
			{
				authority,
				WKID,
				WKID_Z,
				DefaultXYResolution,
				DefaultXYTolerance
			}, this.FindSRByWKIDOperationCompleted, userState);
		}

		private void OnFindSRByWKIDOperationCompleted(object arg)
		{
			if (this.FindSRByWKIDCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.FindSRByWKIDCompleted(this, new FindSRByWKIDCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Areas", Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public double[] GetAreasAndLengths2([XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference SpatialReference, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] Polygon[] InPolygonArray, [XmlElement(Form = XmlSchemaForm.Unqualified)] LinearUnit LengthUnit, [XmlElement(Form = XmlSchemaForm.Unqualified)] LinearUnit AreaUnit, [XmlElement(Form = XmlSchemaForm.Unqualified)] esriAreaUnits AreaUnitEnum, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)] out double[] Lengths)
		{
			object[] array = base.Invoke("GetAreasAndLengths2", new object[]
			{
				SpatialReference,
				InPolygonArray,
				LengthUnit,
				AreaUnit,
				AreaUnitEnum
			});
			Lengths = (double[])array[1];
			return (double[])array[0];
		}

		public IAsyncResult BeginGetAreasAndLengths2(SpatialReference SpatialReference, Polygon[] InPolygonArray, LinearUnit LengthUnit, LinearUnit AreaUnit, esriAreaUnits AreaUnitEnum, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetAreasAndLengths2", new object[]
			{
				SpatialReference,
				InPolygonArray,
				LengthUnit,
				AreaUnit,
				AreaUnitEnum
			}, callback, asyncState);
		}

		public double[] EndGetAreasAndLengths2(IAsyncResult asyncResult, out double[] Lengths)
		{
			object[] array = base.EndInvoke(asyncResult);
			Lengths = (double[])array[1];
			return (double[])array[0];
		}

		public void GetAreasAndLengths2Async(SpatialReference SpatialReference, Polygon[] InPolygonArray, LinearUnit LengthUnit, LinearUnit AreaUnit, esriAreaUnits AreaUnitEnum)
		{
			this.GetAreasAndLengths2Async(SpatialReference, InPolygonArray, LengthUnit, AreaUnit, AreaUnitEnum, null);
		}

		public void GetAreasAndLengths2Async(SpatialReference SpatialReference, Polygon[] InPolygonArray, LinearUnit LengthUnit, LinearUnit AreaUnit, esriAreaUnits AreaUnitEnum, object userState)
		{
			if (this.GetAreasAndLengths2OperationCompleted == null)
			{
				this.GetAreasAndLengths2OperationCompleted = new SendOrPostCallback(this.OnGetAreasAndLengths2OperationCompleted);
			}
			base.InvokeAsync("GetAreasAndLengths2", new object[]
			{
				SpatialReference,
				InPolygonArray,
				LengthUnit,
				AreaUnit,
				AreaUnitEnum
			}, this.GetAreasAndLengths2OperationCompleted, userState);
		}

		private void OnGetAreasAndLengths2OperationCompleted(object arg)
		{
			if (this.GetAreasAndLengths2Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetAreasAndLengths2Completed(this, new GetAreasAndLengths2CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public Geometry[] Buffer([XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference InSpatialReference, [XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference BufferSpatialReference, [XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference OutSpatialReference, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)] double[] Distances, [XmlElement(Form = XmlSchemaForm.Unqualified)] Unit Unit, [XmlElement(Form = XmlSchemaForm.Unqualified)] bool UnionResults, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] Geometry[] InGeometryArray)
		{
			object[] array = base.Invoke("Buffer", new object[]
			{
				InSpatialReference,
				BufferSpatialReference,
				OutSpatialReference,
				Distances,
				Unit,
				UnionResults,
				InGeometryArray
			});
			return (Geometry[])array[0];
		}

		public IAsyncResult BeginBuffer(SpatialReference InSpatialReference, SpatialReference BufferSpatialReference, SpatialReference OutSpatialReference, double[] Distances, Unit Unit, bool UnionResults, Geometry[] InGeometryArray, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Buffer", new object[]
			{
				InSpatialReference,
				BufferSpatialReference,
				OutSpatialReference,
				Distances,
				Unit,
				UnionResults,
				InGeometryArray
			}, callback, asyncState);
		}

		public Geometry[] EndBuffer(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Geometry[])array[0];
		}

		public void BufferAsync(SpatialReference InSpatialReference, SpatialReference BufferSpatialReference, SpatialReference OutSpatialReference, double[] Distances, Unit Unit, bool UnionResults, Geometry[] InGeometryArray)
		{
			this.BufferAsync(InSpatialReference, BufferSpatialReference, OutSpatialReference, Distances, Unit, UnionResults, InGeometryArray, null);
		}

		public void BufferAsync(SpatialReference InSpatialReference, SpatialReference BufferSpatialReference, SpatialReference OutSpatialReference, double[] Distances, Unit Unit, bool UnionResults, Geometry[] InGeometryArray, object userState)
		{
			if (this.BufferOperationCompleted == null)
			{
				this.BufferOperationCompleted = new SendOrPostCallback(this.OnBufferOperationCompleted);
			}
			base.InvokeAsync("Buffer", new object[]
			{
				InSpatialReference,
				BufferSpatialReference,
				OutSpatialReference,
				Distances,
				Unit,
				UnionResults,
				InGeometryArray
			}, this.BufferOperationCompleted, userState);
		}

		private void OnBufferOperationCompleted(object arg)
		{
			if (this.BufferCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.BufferCompleted(this, new BufferCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public Geometry[] Densify([XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference SpatialReference, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] Geometry[] InGeometryArray, [XmlElement(Form = XmlSchemaForm.Unqualified)] double MaxSegmentLength, [XmlElement(Form = XmlSchemaForm.Unqualified)] bool UseDeviationDensification, [XmlElement(Form = XmlSchemaForm.Unqualified)] double DensificationParameter)
		{
			object[] array = base.Invoke("Densify", new object[]
			{
				SpatialReference,
				InGeometryArray,
				MaxSegmentLength,
				UseDeviationDensification,
				DensificationParameter
			});
			return (Geometry[])array[0];
		}

		public IAsyncResult BeginDensify(SpatialReference SpatialReference, Geometry[] InGeometryArray, double MaxSegmentLength, bool UseDeviationDensification, double DensificationParameter, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Densify", new object[]
			{
				SpatialReference,
				InGeometryArray,
				MaxSegmentLength,
				UseDeviationDensification,
				DensificationParameter
			}, callback, asyncState);
		}

		public Geometry[] EndDensify(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Geometry[])array[0];
		}

		public void DensifyAsync(SpatialReference SpatialReference, Geometry[] InGeometryArray, double MaxSegmentLength, bool UseDeviationDensification, double DensificationParameter)
		{
			this.DensifyAsync(SpatialReference, InGeometryArray, MaxSegmentLength, UseDeviationDensification, DensificationParameter, null);
		}

		public void DensifyAsync(SpatialReference SpatialReference, Geometry[] InGeometryArray, double MaxSegmentLength, bool UseDeviationDensification, double DensificationParameter, object userState)
		{
			if (this.DensifyOperationCompleted == null)
			{
				this.DensifyOperationCompleted = new SendOrPostCallback(this.OnDensifyOperationCompleted);
			}
			base.InvokeAsync("Densify", new object[]
			{
				SpatialReference,
				InGeometryArray,
				MaxSegmentLength,
				UseDeviationDensification,
				DensificationParameter
			}, this.DensifyOperationCompleted, userState);
		}

		private void OnDensifyOperationCompleted(object arg)
		{
			if (this.DensifyCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DensifyCompleted(this, new DensifyCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public double[] GetLengths2([XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference SpatialReference, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] Polyline[] InPolylineArray, [XmlElement(Form = XmlSchemaForm.Unqualified)] LinearUnit LengthUnit)
		{
			object[] array = base.Invoke("GetLengths2", new object[]
			{
				SpatialReference,
				InPolylineArray,
				LengthUnit
			});
			return (double[])array[0];
		}

		public IAsyncResult BeginGetLengths2(SpatialReference SpatialReference, Polyline[] InPolylineArray, LinearUnit LengthUnit, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetLengths2", new object[]
			{
				SpatialReference,
				InPolylineArray,
				LengthUnit
			}, callback, asyncState);
		}

		public double[] EndGetLengths2(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (double[])array[0];
		}

		public void GetLengths2Async(SpatialReference SpatialReference, Polyline[] InPolylineArray, LinearUnit LengthUnit)
		{
			this.GetLengths2Async(SpatialReference, InPolylineArray, LengthUnit, null);
		}

		public void GetLengths2Async(SpatialReference SpatialReference, Polyline[] InPolylineArray, LinearUnit LengthUnit, object userState)
		{
			if (this.GetLengths2OperationCompleted == null)
			{
				this.GetLengths2OperationCompleted = new SendOrPostCallback(this.OnGetLengths2OperationCompleted);
			}
			base.InvokeAsync("GetLengths2", new object[]
			{
				SpatialReference,
				InPolylineArray,
				LengthUnit
			}, this.GetLengths2OperationCompleted, userState);
		}

		private void OnGetLengths2OperationCompleted(object arg)
		{
			if (this.GetLengths2Completed != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetLengths2Completed(this, new GetLengths2CompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public RelationResult[] Relation([XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference SpatialReference, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] Geometry[] InGeometryArray1, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] Geometry[] InGeometryArray2, [XmlElement(Form = XmlSchemaForm.Unqualified)] esriGeometryRelationEnum RelationName, [XmlElement(Form = XmlSchemaForm.Unqualified)] string RelationParameter)
		{
			object[] array = base.Invoke("Relation", new object[]
			{
				SpatialReference,
				InGeometryArray1,
				InGeometryArray2,
				RelationName,
				RelationParameter
			});
			return (RelationResult[])array[0];
		}

		public IAsyncResult BeginRelation(SpatialReference SpatialReference, Geometry[] InGeometryArray1, Geometry[] InGeometryArray2, esriGeometryRelationEnum RelationName, string RelationParameter, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Relation", new object[]
			{
				SpatialReference,
				InGeometryArray1,
				InGeometryArray2,
				RelationName,
				RelationParameter
			}, callback, asyncState);
		}

		public RelationResult[] EndRelation(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (RelationResult[])array[0];
		}

		public void RelationAsync(SpatialReference SpatialReference, Geometry[] InGeometryArray1, Geometry[] InGeometryArray2, esriGeometryRelationEnum RelationName, string RelationParameter)
		{
			this.RelationAsync(SpatialReference, InGeometryArray1, InGeometryArray2, RelationName, RelationParameter, null);
		}

		public void RelationAsync(SpatialReference SpatialReference, Geometry[] InGeometryArray1, Geometry[] InGeometryArray2, esriGeometryRelationEnum RelationName, string RelationParameter, object userState)
		{
			if (this.RelationOperationCompleted == null)
			{
				this.RelationOperationCompleted = new SendOrPostCallback(this.OnRelationOperationCompleted);
			}
			base.InvokeAsync("Relation", new object[]
			{
				SpatialReference,
				InGeometryArray1,
				InGeometryArray2,
				RelationName,
				RelationParameter
			}, this.RelationOperationCompleted, userState);
		}

		private void OnRelationOperationCompleted(object arg)
		{
			if (this.RelationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RelationCompleted(this, new RelationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public Polygon[] AutoComplete([XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference SpatialReference, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] Polygon[] InPolygons, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] Polyline[] InCompletionLines)
		{
			object[] array = base.Invoke("AutoComplete", new object[]
			{
				SpatialReference,
				InPolygons,
				InCompletionLines
			});
			return (Polygon[])array[0];
		}

		public IAsyncResult BeginAutoComplete(SpatialReference SpatialReference, Polygon[] InPolygons, Polyline[] InCompletionLines, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AutoComplete", new object[]
			{
				SpatialReference,
				InPolygons,
				InCompletionLines
			}, callback, asyncState);
		}

		public Polygon[] EndAutoComplete(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Polygon[])array[0];
		}

		public void AutoCompleteAsync(SpatialReference SpatialReference, Polygon[] InPolygons, Polyline[] InCompletionLines)
		{
			this.AutoCompleteAsync(SpatialReference, InPolygons, InCompletionLines, null);
		}

		public void AutoCompleteAsync(SpatialReference SpatialReference, Polygon[] InPolygons, Polyline[] InCompletionLines, object userState)
		{
			if (this.AutoCompleteOperationCompleted == null)
			{
				this.AutoCompleteOperationCompleted = new SendOrPostCallback(this.OnAutoCompleteOperationCompleted);
			}
			base.InvokeAsync("AutoComplete", new object[]
			{
				SpatialReference,
				InPolygons,
				InCompletionLines
			}, this.AutoCompleteOperationCompleted, userState);
		}

		private void OnAutoCompleteOperationCompleted(object arg)
		{
			if (this.AutoCompleteCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AutoCompleteCompleted(this, new AutoCompleteCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public SpatialReference FindSRByWKT([XmlElement(Form = XmlSchemaForm.Unqualified)] string WKT, [XmlElement(Form = XmlSchemaForm.Unqualified)] string WKT_Z, [XmlElement(Form = XmlSchemaForm.Unqualified)] bool DefaultXYResolution, [XmlElement(Form = XmlSchemaForm.Unqualified)] bool DefaultXYTolerance)
		{
			object[] array = base.Invoke("FindSRByWKT", new object[]
			{
				WKT,
				WKT_Z,
				DefaultXYResolution,
				DefaultXYTolerance
			});
			return (SpatialReference)array[0];
		}

		public IAsyncResult BeginFindSRByWKT(string WKT, string WKT_Z, bool DefaultXYResolution, bool DefaultXYTolerance, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("FindSRByWKT", new object[]
			{
				WKT,
				WKT_Z,
				DefaultXYResolution,
				DefaultXYTolerance
			}, callback, asyncState);
		}

		public SpatialReference EndFindSRByWKT(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (SpatialReference)array[0];
		}

		public void FindSRByWKTAsync(string WKT, string WKT_Z, bool DefaultXYResolution, bool DefaultXYTolerance)
		{
			this.FindSRByWKTAsync(WKT, WKT_Z, DefaultXYResolution, DefaultXYTolerance, null);
		}

		public void FindSRByWKTAsync(string WKT, string WKT_Z, bool DefaultXYResolution, bool DefaultXYTolerance, object userState)
		{
			if (this.FindSRByWKTOperationCompleted == null)
			{
				this.FindSRByWKTOperationCompleted = new SendOrPostCallback(this.OnFindSRByWKTOperationCompleted);
			}
			base.InvokeAsync("FindSRByWKT", new object[]
			{
				WKT,
				WKT_Z,
				DefaultXYResolution,
				DefaultXYTolerance
			}, this.FindSRByWKTOperationCompleted, userState);
		}

		private void OnFindSRByWKTOperationCompleted(object arg)
		{
			if (this.FindSRByWKTCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.FindSRByWKTCompleted(this, new FindSRByWKTCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public Geometry[] Offset([XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference SpatialReference, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] Geometry[] InGeometryArray, [XmlElement(Form = XmlSchemaForm.Unqualified)] double OffsetDistance, [XmlElement(Form = XmlSchemaForm.Unqualified)] LinearUnit OffsetUnit, [XmlElement(Form = XmlSchemaForm.Unqualified)] esriGeometryOffsetEnum OffsetHow, [XmlElement(Form = XmlSchemaForm.Unqualified)] double BevelRatio, [XmlElement(Form = XmlSchemaForm.Unqualified)] bool SimplifyResult)
		{
			object[] array = base.Invoke("Offset", new object[]
			{
				SpatialReference,
				InGeometryArray,
				OffsetDistance,
				OffsetUnit,
				OffsetHow,
				BevelRatio,
				SimplifyResult
			});
			return (Geometry[])array[0];
		}

		public IAsyncResult BeginOffset(SpatialReference SpatialReference, Geometry[] InGeometryArray, double OffsetDistance, LinearUnit OffsetUnit, esriGeometryOffsetEnum OffsetHow, double BevelRatio, bool SimplifyResult, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Offset", new object[]
			{
				SpatialReference,
				InGeometryArray,
				OffsetDistance,
				OffsetUnit,
				OffsetHow,
				BevelRatio,
				SimplifyResult
			}, callback, asyncState);
		}

		public Geometry[] EndOffset(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Geometry[])array[0];
		}

		public void OffsetAsync(SpatialReference SpatialReference, Geometry[] InGeometryArray, double OffsetDistance, LinearUnit OffsetUnit, esriGeometryOffsetEnum OffsetHow, double BevelRatio, bool SimplifyResult)
		{
			this.OffsetAsync(SpatialReference, InGeometryArray, OffsetDistance, OffsetUnit, OffsetHow, BevelRatio, SimplifyResult, null);
		}

		public void OffsetAsync(SpatialReference SpatialReference, Geometry[] InGeometryArray, double OffsetDistance, LinearUnit OffsetUnit, esriGeometryOffsetEnum OffsetHow, double BevelRatio, bool SimplifyResult, object userState)
		{
			if (this.OffsetOperationCompleted == null)
			{
				this.OffsetOperationCompleted = new SendOrPostCallback(this.OnOffsetOperationCompleted);
			}
			base.InvokeAsync("Offset", new object[]
			{
				SpatialReference,
				InGeometryArray,
				OffsetDistance,
				OffsetUnit,
				OffsetHow,
				BevelRatio,
				SimplifyResult
			}, this.OffsetOperationCompleted, userState);
		}

		private void OnOffsetOperationCompleted(object arg)
		{
			if (this.OffsetCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.OffsetCompleted(this, new OffsetCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public Geometry[] Cut([XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference SpatialReference, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] Geometry[] Targets, [XmlElement(Form = XmlSchemaForm.Unqualified)] Polyline Cutter, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)] out int[] CutIndexResult)
		{
			object[] array = base.Invoke("Cut", new object[]
			{
				SpatialReference,
				Targets,
				Cutter
			});
			CutIndexResult = (int[])array[1];
			return (Geometry[])array[0];
		}

		public IAsyncResult BeginCut(SpatialReference SpatialReference, Geometry[] Targets, Polyline Cutter, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Cut", new object[]
			{
				SpatialReference,
				Targets,
				Cutter
			}, callback, asyncState);
		}

		public Geometry[] EndCut(IAsyncResult asyncResult, out int[] CutIndexResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			CutIndexResult = (int[])array[1];
			return (Geometry[])array[0];
		}

		public void CutAsync(SpatialReference SpatialReference, Geometry[] Targets, Polyline Cutter)
		{
			this.CutAsync(SpatialReference, Targets, Cutter, null);
		}

		public void CutAsync(SpatialReference SpatialReference, Geometry[] Targets, Polyline Cutter, object userState)
		{
			if (this.CutOperationCompleted == null)
			{
				this.CutOperationCompleted = new SendOrPostCallback(this.OnCutOperationCompleted);
			}
			base.InvokeAsync("Cut", new object[]
			{
				SpatialReference,
				Targets,
				Cutter
			}, this.CutOperationCompleted, userState);
		}

		private void OnCutOperationCompleted(object arg)
		{
			if (this.CutCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CutCompleted(this, new CutCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public Geometry[] Intersect([XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference SpatialReference, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] Geometry[] InGeometryArray1, [XmlElement(Form = XmlSchemaForm.Unqualified)] Geometry InGeometry2)
		{
			object[] array = base.Invoke("Intersect", new object[]
			{
				SpatialReference,
				InGeometryArray1,
				InGeometry2
			});
			return (Geometry[])array[0];
		}

		public IAsyncResult BeginIntersect(SpatialReference SpatialReference, Geometry[] InGeometryArray1, Geometry InGeometry2, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Intersect", new object[]
			{
				SpatialReference,
				InGeometryArray1,
				InGeometry2
			}, callback, asyncState);
		}

		public Geometry[] EndIntersect(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Geometry[])array[0];
		}

		public void IntersectAsync(SpatialReference SpatialReference, Geometry[] InGeometryArray1, Geometry InGeometry2)
		{
			this.IntersectAsync(SpatialReference, InGeometryArray1, InGeometry2, null);
		}

		public void IntersectAsync(SpatialReference SpatialReference, Geometry[] InGeometryArray1, Geometry InGeometry2, object userState)
		{
			if (this.IntersectOperationCompleted == null)
			{
				this.IntersectOperationCompleted = new SendOrPostCallback(this.OnIntersectOperationCompleted);
			}
			base.InvokeAsync("Intersect", new object[]
			{
				SpatialReference,
				InGeometryArray1,
				InGeometry2
			}, this.IntersectOperationCompleted, userState);
		}

		private void OnIntersectOperationCompleted(object arg)
		{
			if (this.IntersectCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.IntersectCompleted(this, new IntersectCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public double GetDistanceGeodesic([XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference SpatialReference, [XmlElement(Form = XmlSchemaForm.Unqualified)] Geometry G1, [XmlElement(Form = XmlSchemaForm.Unqualified)] Geometry G2, [XmlElement(Form = XmlSchemaForm.Unqualified)] LinearUnit DistanceUnit)
		{
			object[] array = base.Invoke("GetDistanceGeodesic", new object[]
			{
				SpatialReference,
				G1,
				G2,
				DistanceUnit
			});
			return (double)array[0];
		}

		public IAsyncResult BeginGetDistanceGeodesic(SpatialReference SpatialReference, Geometry G1, Geometry G2, LinearUnit DistanceUnit, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetDistanceGeodesic", new object[]
			{
				SpatialReference,
				G1,
				G2,
				DistanceUnit
			}, callback, asyncState);
		}

		public double EndGetDistanceGeodesic(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (double)array[0];
		}

		public void GetDistanceGeodesicAsync(SpatialReference SpatialReference, Geometry G1, Geometry G2, LinearUnit DistanceUnit)
		{
			this.GetDistanceGeodesicAsync(SpatialReference, G1, G2, DistanceUnit, null);
		}

		public void GetDistanceGeodesicAsync(SpatialReference SpatialReference, Geometry G1, Geometry G2, LinearUnit DistanceUnit, object userState)
		{
			if (this.GetDistanceGeodesicOperationCompleted == null)
			{
				this.GetDistanceGeodesicOperationCompleted = new SendOrPostCallback(this.OnGetDistanceGeodesicOperationCompleted);
			}
			base.InvokeAsync("GetDistanceGeodesic", new object[]
			{
				SpatialReference,
				G1,
				G2,
				DistanceUnit
			}, this.GetDistanceGeodesicOperationCompleted, userState);
		}

		private void OnGetDistanceGeodesicOperationCompleted(object arg)
		{
			if (this.GetDistanceGeodesicCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetDistanceGeodesicCompleted(this, new GetDistanceGeodesicCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public Geometry[] Simplify([XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference SpatialReference, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] Geometry[] InGeometryArray)
		{
			object[] array = base.Invoke("Simplify", new object[]
			{
				SpatialReference,
				InGeometryArray
			});
			return (Geometry[])array[0];
		}

		public IAsyncResult BeginSimplify(SpatialReference SpatialReference, Geometry[] InGeometryArray, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Simplify", new object[]
			{
				SpatialReference,
				InGeometryArray
			}, callback, asyncState);
		}

		public Geometry[] EndSimplify(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Geometry[])array[0];
		}

		public void SimplifyAsync(SpatialReference SpatialReference, Geometry[] InGeometryArray)
		{
			this.SimplifyAsync(SpatialReference, InGeometryArray, null);
		}

		public void SimplifyAsync(SpatialReference SpatialReference, Geometry[] InGeometryArray, object userState)
		{
			if (this.SimplifyOperationCompleted == null)
			{
				this.SimplifyOperationCompleted = new SendOrPostCallback(this.OnSimplifyOperationCompleted);
			}
			base.InvokeAsync("Simplify", new object[]
			{
				SpatialReference,
				InGeometryArray
			}, this.SimplifyOperationCompleted, userState);
		}

		private void OnSimplifyOperationCompleted(object arg)
		{
			if (this.SimplifyCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SimplifyCompleted(this, new SimplifyCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public Geometry[] Difference([XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference SpatialReference, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] Geometry[] InGeometryArray1, [XmlElement(Form = XmlSchemaForm.Unqualified)] Geometry InGeometry2)
		{
			object[] array = base.Invoke("Difference", new object[]
			{
				SpatialReference,
				InGeometryArray1,
				InGeometry2
			});
			return (Geometry[])array[0];
		}

		public IAsyncResult BeginDifference(SpatialReference SpatialReference, Geometry[] InGeometryArray1, Geometry InGeometry2, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Difference", new object[]
			{
				SpatialReference,
				InGeometryArray1,
				InGeometry2
			}, callback, asyncState);
		}

		public Geometry[] EndDifference(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Geometry[])array[0];
		}

		public void DifferenceAsync(SpatialReference SpatialReference, Geometry[] InGeometryArray1, Geometry InGeometry2)
		{
			this.DifferenceAsync(SpatialReference, InGeometryArray1, InGeometry2, null);
		}

		public void DifferenceAsync(SpatialReference SpatialReference, Geometry[] InGeometryArray1, Geometry InGeometry2, object userState)
		{
			if (this.DifferenceOperationCompleted == null)
			{
				this.DifferenceOperationCompleted = new SendOrPostCallback(this.OnDifferenceOperationCompleted);
			}
			base.InvokeAsync("Difference", new object[]
			{
				SpatialReference,
				InGeometryArray1,
				InGeometry2
			}, this.DifferenceOperationCompleted, userState);
		}

		private void OnDifferenceOperationCompleted(object arg)
		{
			if (this.DifferenceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DifferenceCompleted(this, new DifferenceCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public Unit FindUnitsByWKT([XmlElement(Form = XmlSchemaForm.Unqualified)] string WKT)
		{
			object[] array = base.Invoke("FindUnitsByWKT", new object[]
			{
				WKT
			});
			return (Unit)array[0];
		}

		public IAsyncResult BeginFindUnitsByWKT(string WKT, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("FindUnitsByWKT", new object[]
			{
				WKT
			}, callback, asyncState);
		}

		public Unit EndFindUnitsByWKT(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Unit)array[0];
		}

		public void FindUnitsByWKTAsync(string WKT)
		{
			this.FindUnitsByWKTAsync(WKT, null);
		}

		public void FindUnitsByWKTAsync(string WKT, object userState)
		{
			if (this.FindUnitsByWKTOperationCompleted == null)
			{
				this.FindUnitsByWKTOperationCompleted = new SendOrPostCallback(this.OnFindUnitsByWKTOperationCompleted);
			}
			base.InvokeAsync("FindUnitsByWKT", new object[]
			{
				WKT
			}, this.FindUnitsByWKTOperationCompleted, userState);
		}

		private void OnFindUnitsByWKTOperationCompleted(object arg)
		{
			if (this.FindUnitsByWKTCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.FindUnitsByWKTCompleted(this, new FindUnitsByWKTCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("Result", Form = XmlSchemaForm.Unqualified)]
		public double GetDistance([XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference SpatialReference, [XmlElement(Form = XmlSchemaForm.Unqualified)] Geometry G1, [XmlElement(Form = XmlSchemaForm.Unqualified)] Geometry G2, [XmlElement(Form = XmlSchemaForm.Unqualified)] LinearUnit DistanceUnit)
		{
			object[] array = base.Invoke("GetDistance", new object[]
			{
				SpatialReference,
				G1,
				G2,
				DistanceUnit
			});
			return (double)array[0];
		}

		public IAsyncResult BeginGetDistance(SpatialReference SpatialReference, Geometry G1, Geometry G2, LinearUnit DistanceUnit, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetDistance", new object[]
			{
				SpatialReference,
				G1,
				G2,
				DistanceUnit
			}, callback, asyncState);
		}

		public double EndGetDistance(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (double)array[0];
		}

		public void GetDistanceAsync(SpatialReference SpatialReference, Geometry G1, Geometry G2, LinearUnit DistanceUnit)
		{
			this.GetDistanceAsync(SpatialReference, G1, G2, DistanceUnit, null);
		}

		public void GetDistanceAsync(SpatialReference SpatialReference, Geometry G1, Geometry G2, LinearUnit DistanceUnit, object userState)
		{
			if (this.GetDistanceOperationCompleted == null)
			{
				this.GetDistanceOperationCompleted = new SendOrPostCallback(this.OnGetDistanceOperationCompleted);
			}
			base.InvokeAsync("GetDistance", new object[]
			{
				SpatialReference,
				G1,
				G2,
				DistanceUnit
			}, this.GetDistanceOperationCompleted, userState);
		}

		private void OnGetDistanceOperationCompleted(object arg)
		{
			if (this.GetDistanceCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetDistanceCompleted(this, new GetDistanceCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", ResponseNamespace = "http://www.esri.com/schemas/ArcGIS/10.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlArray("Result", Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public double[] GetLengths([XmlElement(Form = XmlSchemaForm.Unqualified)] SpatialReference SpatialReference, [XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)] Polyline[] InPolylineArray)
		{
			object[] array = base.Invoke("GetLengths", new object[]
			{
				SpatialReference,
				InPolylineArray
			});
			return (double[])array[0];
		}

		public IAsyncResult BeginGetLengths(SpatialReference SpatialReference, Polyline[] InPolylineArray, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetLengths", new object[]
			{
				SpatialReference,
				InPolylineArray
			}, callback, asyncState);
		}

		public double[] EndGetLengths(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (double[])array[0];
		}

		public void GetLengthsAsync(SpatialReference SpatialReference, Polyline[] InPolylineArray)
		{
			this.GetLengthsAsync(SpatialReference, InPolylineArray, null);
		}

		public void GetLengthsAsync(SpatialReference SpatialReference, Polyline[] InPolylineArray, object userState)
		{
			if (this.GetLengthsOperationCompleted == null)
			{
				this.GetLengthsOperationCompleted = new SendOrPostCallback(this.OnGetLengthsOperationCompleted);
			}
			base.InvokeAsync("GetLengths", new object[]
			{
				SpatialReference,
				InPolylineArray
			}, this.GetLengthsOperationCompleted, userState);
		}

		private void OnGetLengthsOperationCompleted(object arg)
		{
			if (this.GetLengthsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetLengthsCompleted(this, new GetLengthsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}
	}
}
