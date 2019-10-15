using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlInclude(typeof(PolygonB)), XmlInclude(typeof(Polygon)), XmlInclude(typeof(PolygonN)), XmlInclude(typeof(Segment)), XmlInclude(typeof(BezierCurve)), XmlInclude(typeof(CircularArc)), XmlInclude(typeof(EllipticArc)), XmlInclude(typeof(Line)), XmlInclude(typeof(Polyline)), XmlInclude(typeof(Polycurve)), XmlInclude(typeof(PolylineB)), XmlInclude(typeof(PolylineN)), XmlInclude(typeof(Path)), XmlInclude(typeof(Ring)), XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	[Serializable]
	public abstract class Curve : Geometry
	{
	}
}
