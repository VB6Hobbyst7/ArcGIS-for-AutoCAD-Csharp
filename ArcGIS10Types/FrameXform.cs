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
	public class FrameXform : GeodataXform
	{
		private SpatialReference spatialReferenceField;

		private double constantZField;

		private bool constantZFieldSpecified;

		private double zFactorField;

		private bool zFactorFieldSpecified;

		private double zOffsetField;

		private bool zOffsetFieldSpecified;

		private bool correctGeoidField;

		private bool correctGeoidFieldSpecified;

		private GeodataXform interiorOrientationField;

		private bool konradyField;

		private bool konradyFieldSpecified;

		private string konradyTypeField;

		private double[] konradyParametersField;

		private bool curvatureAndRefractionField;

		private bool curvatureAndRefractionFieldSpecified;

		private double earthRadiusField;

		private bool earthRadiusFieldSpecified;

		private double averageZField;

		private bool averageZFieldSpecified;

		private Point principlePointField;

		private double focalLengthField;

		private bool focalLengthFieldSpecified;

		private double polarityField;

		private bool polarityFieldSpecified;

		private Point sensorPositionField;

		private double[] exteriorOrientationField;

		private bool clockwiseField;

		private bool clockwiseFieldSpecified;

		private GeodataXform lSRField;

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
		public double ConstantZ
		{
			get
			{
				return this.constantZField;
			}
			set
			{
				this.constantZField = value;
			}
		}

		[XmlIgnore]
		public bool ConstantZSpecified
		{
			get
			{
				return this.constantZFieldSpecified;
			}
			set
			{
				this.constantZFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double ZFactor
		{
			get
			{
				return this.zFactorField;
			}
			set
			{
				this.zFactorField = value;
			}
		}

		[XmlIgnore]
		public bool ZFactorSpecified
		{
			get
			{
				return this.zFactorFieldSpecified;
			}
			set
			{
				this.zFactorFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double ZOffset
		{
			get
			{
				return this.zOffsetField;
			}
			set
			{
				this.zOffsetField = value;
			}
		}

		[XmlIgnore]
		public bool ZOffsetSpecified
		{
			get
			{
				return this.zOffsetFieldSpecified;
			}
			set
			{
				this.zOffsetFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool CorrectGeoid
		{
			get
			{
				return this.correctGeoidField;
			}
			set
			{
				this.correctGeoidField = value;
			}
		}

		[XmlIgnore]
		public bool CorrectGeoidSpecified
		{
			get
			{
				return this.correctGeoidFieldSpecified;
			}
			set
			{
				this.correctGeoidFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public GeodataXform InteriorOrientation
		{
			get
			{
				return this.interiorOrientationField;
			}
			set
			{
				this.interiorOrientationField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool Konrady
		{
			get
			{
				return this.konradyField;
			}
			set
			{
				this.konradyField = value;
			}
		}

		[XmlIgnore]
		public bool KonradySpecified
		{
			get
			{
				return this.konradyFieldSpecified;
			}
			set
			{
				this.konradyFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string KonradyType
		{
			get
			{
				return this.konradyTypeField;
			}
			set
			{
				this.konradyTypeField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public double[] KonradyParameters
		{
			get
			{
				return this.konradyParametersField;
			}
			set
			{
				this.konradyParametersField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool CurvatureAndRefraction
		{
			get
			{
				return this.curvatureAndRefractionField;
			}
			set
			{
				this.curvatureAndRefractionField = value;
			}
		}

		[XmlIgnore]
		public bool CurvatureAndRefractionSpecified
		{
			get
			{
				return this.curvatureAndRefractionFieldSpecified;
			}
			set
			{
				this.curvatureAndRefractionFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double EarthRadius
		{
			get
			{
				return this.earthRadiusField;
			}
			set
			{
				this.earthRadiusField = value;
			}
		}

		[XmlIgnore]
		public bool EarthRadiusSpecified
		{
			get
			{
				return this.earthRadiusFieldSpecified;
			}
			set
			{
				this.earthRadiusFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double AverageZ
		{
			get
			{
				return this.averageZField;
			}
			set
			{
				this.averageZField = value;
			}
		}

		[XmlIgnore]
		public bool AverageZSpecified
		{
			get
			{
				return this.averageZFieldSpecified;
			}
			set
			{
				this.averageZFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Point PrinciplePoint
		{
			get
			{
				return this.principlePointField;
			}
			set
			{
				this.principlePointField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double FocalLength
		{
			get
			{
				return this.focalLengthField;
			}
			set
			{
				this.focalLengthField = value;
			}
		}

		[XmlIgnore]
		public bool FocalLengthSpecified
		{
			get
			{
				return this.focalLengthFieldSpecified;
			}
			set
			{
				this.focalLengthFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double Polarity
		{
			get
			{
				return this.polarityField;
			}
			set
			{
				this.polarityField = value;
			}
		}

		[XmlIgnore]
		public bool PolaritySpecified
		{
			get
			{
				return this.polarityFieldSpecified;
			}
			set
			{
				this.polarityFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Point SensorPosition
		{
			get
			{
				return this.sensorPositionField;
			}
			set
			{
				this.sensorPositionField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public double[] ExteriorOrientation
		{
			get
			{
				return this.exteriorOrientationField;
			}
			set
			{
				this.exteriorOrientationField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool Clockwise
		{
			get
			{
				return this.clockwiseField;
			}
			set
			{
				this.clockwiseField = value;
			}
		}

		[XmlIgnore]
		public bool ClockwiseSpecified
		{
			get
			{
				return this.clockwiseFieldSpecified;
			}
			set
			{
				this.clockwiseFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public GeodataXform LSR
		{
			get
			{
				return this.lSRField;
			}
			set
			{
				this.lSRField = value;
			}
		}
	}
}
