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
	public class CircularArc : Segment
	{
		private Point centerPointField;

		private double fromAngleField;

		private bool fromAngleFieldSpecified;

		private double toAngleField;

		private bool toAngleFieldSpecified;

		private bool isCounterClockwiseField;

		private bool isMinorField;

		private bool isLineField;

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
		public double FromAngle
		{
			get
			{
				return this.fromAngleField;
			}
			set
			{
				this.fromAngleField = value;
			}
		}

		[XmlIgnore]
		public bool FromAngleSpecified
		{
			get
			{
				return this.fromAngleFieldSpecified;
			}
			set
			{
				this.fromAngleFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double ToAngle
		{
			get
			{
				return this.toAngleField;
			}
			set
			{
				this.toAngleField = value;
			}
		}

		[XmlIgnore]
		public bool ToAngleSpecified
		{
			get
			{
				return this.toAngleFieldSpecified;
			}
			set
			{
				this.toAngleFieldSpecified = value;
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

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool IsLine
		{
			get
			{
				return this.isLineField;
			}
			set
			{
				this.isLineField = value;
			}
		}
	}
}
