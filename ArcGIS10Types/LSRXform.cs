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
	public class LSRXform : GeodataXform
	{
		private SpatialReference spatialReferenceField;

		private Point perspectiveCenterField;

		private double[] rotationMatrixField;

		private double flatteningField;

		private double equatorialRadiusField;

		private bool equatorialRadiusFieldSpecified;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public SpatialReference SpatialReference
		{
			get
			{
				return this.spatialReferenceField;
			}
			set
			{
				this.spatialReferenceField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Point PerspectiveCenter
		{
			get
			{
				return this.perspectiveCenterField;
			}
			set
			{
				this.perspectiveCenterField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public double[] RotationMatrix
		{
			get
			{
				return this.rotationMatrixField;
			}
			set
			{
				this.rotationMatrixField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double Flattening
		{
			get
			{
				return this.flatteningField;
			}
			set
			{
				this.flatteningField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double EquatorialRadius
		{
			get
			{
				return this.equatorialRadiusField;
			}
			set
			{
				this.equatorialRadiusField = value;
			}
		}

		[XmlIgnore]
		public bool EquatorialRadiusSpecified
		{
			get
			{
				return this.equatorialRadiusFieldSpecified;
			}
			set
			{
				this.equatorialRadiusFieldSpecified = value;
			}
		}
	}
}
