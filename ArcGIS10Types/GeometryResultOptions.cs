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
	public class GeometryResultOptions
	{
		private bool densifyGeometriesField;

		private double maximumSegmentLengthField;

		private double maximumDeviationField;

		private bool generalizeGeometriesField;

		private double maximumAllowableOffsetField;

		[DefaultValue(false), XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool DensifyGeometries
		{
			get
			{
				return this.densifyGeometriesField;
			}
			set
			{
				this.densifyGeometriesField = value;
			}
		}

		[DefaultValue(-1), XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double MaximumSegmentLength
		{
			get
			{
				return this.maximumSegmentLengthField;
			}
			set
			{
				this.maximumSegmentLengthField = value;
			}
		}

		[DefaultValue(0), XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double MaximumDeviation
		{
			get
			{
				return this.maximumDeviationField;
			}
			set
			{
				this.maximumDeviationField = value;
			}
		}

		[DefaultValue(false), XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool GeneralizeGeometries
		{
			get
			{
				return this.generalizeGeometriesField;
			}
			set
			{
				this.generalizeGeometriesField = value;
			}
		}

		[DefaultValue(0), XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double MaximumAllowableOffset
		{
			get
			{
				return this.maximumAllowableOffsetField;
			}
			set
			{
				this.maximumAllowableOffsetField = value;
			}
		}

		public GeometryResultOptions()
		{
			this.densifyGeometriesField = false;
			this.maximumSegmentLengthField = -1.0;
			this.maximumDeviationField = 0.0;
			this.generalizeGeometriesField = false;
			this.maximumAllowableOffsetField = 0.0;
		}
	}
}
