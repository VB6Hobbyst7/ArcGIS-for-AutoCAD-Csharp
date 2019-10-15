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
	public class EllipticArc : Segment
	{
		private bool ellipseStdField;

		private Point centerPointField;

		private double rotationField;

		private double minorMajorRatioField;

		private bool isCounterClockwiseField;

		private bool isMinorField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool EllipseStd
		{
			get
			{
				return this.ellipseStdField;
			}
			set
			{
				this.ellipseStdField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Point CenterPoint
		{
			get
			{
				return this.centerPointField;
			}
			set
			{
				this.centerPointField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double Rotation
		{
			get
			{
				return this.rotationField;
			}
			set
			{
				this.rotationField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double MinorMajorRatio
		{
			get
			{
				return this.minorMajorRatioField;
			}
			set
			{
				this.minorMajorRatioField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool IsCounterClockwise
		{
			get
			{
				return this.isCounterClockwiseField;
			}
			set
			{
				this.isCounterClockwiseField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool IsMinor
		{
			get
			{
				return this.isMinorField;
			}
			set
			{
				this.isMinorField = value;
			}
		}
	}
}
