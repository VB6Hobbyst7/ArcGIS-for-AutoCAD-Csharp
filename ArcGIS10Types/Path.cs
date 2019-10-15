using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlInclude(typeof(Ring)), XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	[Serializable]
	public class Path : Curve
	{
		private Point[] pointArrayField;

		private Segment[] segmentArrayField;

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public Point[] PointArray
		{
			get
			{
				return this.pointArrayField;
			}
			set
			{
				this.pointArrayField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public Segment[] SegmentArray
		{
			get
			{
				return this.segmentArrayField;
			}
			set
			{
				this.segmentArrayField = value;
			}
		}
	}
}
