using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlInclude(typeof(CircularArc)), XmlInclude(typeof(EllipticArc)), XmlInclude(typeof(BezierCurve)), XmlInclude(typeof(Line)), XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	[Serializable]
	public abstract class Segment : Curve
	{
		private Point fromPointField;

		private Point toPointField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Point FromPoint
		{
			get
			{
				return this.fromPointField;
			}
			set
			{
				this.fromPointField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Point ToPoint
		{
			get
			{
				return this.toPointField;
			}
			set
			{
				this.toPointField = value;
			}
		}
	}
}
