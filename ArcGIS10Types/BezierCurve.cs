using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	[Serializable]
	public class BezierCurve : Segment
	{
		private int degreeField;

		private Point[] controlPointArrayField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int Degree
		{
			get
			{
				return this.degreeField;
			}
			set
			{
				this.degreeField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public Point[] ControlPointArray
		{
			get
			{
				return this.controlPointArrayField;
			}
			set
			{
				this.controlPointArrayField = value;
			}
		}
	}
}
