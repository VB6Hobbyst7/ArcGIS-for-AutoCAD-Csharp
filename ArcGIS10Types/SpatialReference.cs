using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlInclude(typeof(GeographicCoordinateSystem)), XmlInclude(typeof(ProjectedCoordinateSystem)), XmlInclude(typeof(UnknownCoordinateSystem)), XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	[Serializable]
	public abstract class SpatialReference
	{
		private string wKTField;

		private double xOriginField;

		private bool xOriginFieldSpecified;

		private double yOriginField;

		private bool yOriginFieldSpecified;

		private double xYScaleField;

		private bool xYScaleFieldSpecified;

		private double zOriginField;

		private bool zOriginFieldSpecified;

		private double zScaleField;

		private bool zScaleFieldSpecified;

		private double mOriginField;

		private bool mOriginFieldSpecified;

		private double mScaleField;

		private bool mScaleFieldSpecified;

		private double xYToleranceField;

		private bool xYToleranceFieldSpecified;

		private double zToleranceField;

		private bool zToleranceFieldSpecified;

		private double mToleranceField;

		private bool mToleranceFieldSpecified;

		private bool highPrecisionField;

		private bool highPrecisionFieldSpecified;

		private double leftLongitudeField;

		private bool leftLongitudeFieldSpecified;

		private int wKIDField;

		private bool wKIDFieldSpecified;

		private int vCSWKIDField;

		private bool vCSWKIDFieldSpecified;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string WKT
		{
			get
			{
				return this.wKTField;
			}
			set
			{
				this.wKTField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double XOrigin
		{
			get
			{
				return this.xOriginField;
			}
			set
			{
				this.xOriginField = value;
			}
		}

		[XmlIgnore]
		public bool XOriginSpecified
		{
			get
			{
				return this.xOriginFieldSpecified;
			}
			set
			{
				this.xOriginFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double YOrigin
		{
			get
			{
				return this.yOriginField;
			}
			set
			{
				this.yOriginField = value;
			}
		}

		[XmlIgnore]
		public bool YOriginSpecified
		{
			get
			{
				return this.yOriginFieldSpecified;
			}
			set
			{
				this.yOriginFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double XYScale
		{
			get
			{
				return this.xYScaleField;
			}
			set
			{
				this.xYScaleField = value;
			}
		}

		[XmlIgnore]
		public bool XYScaleSpecified
		{
			get
			{
				return this.xYScaleFieldSpecified;
			}
			set
			{
				this.xYScaleFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double ZOrigin
		{
			get
			{
				return this.zOriginField;
			}
			set
			{
				this.zOriginField = value;
			}
		}

		[XmlIgnore]
		public bool ZOriginSpecified
		{
			get
			{
				return this.zOriginFieldSpecified;
			}
			set
			{
				this.zOriginFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double ZScale
		{
			get
			{
				return this.zScaleField;
			}
			set
			{
				this.zScaleField = value;
			}
		}

		[XmlIgnore]
		public bool ZScaleSpecified
		{
			get
			{
				return this.zScaleFieldSpecified;
			}
			set
			{
				this.zScaleFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double MOrigin
		{
			get
			{
				return this.mOriginField;
			}
			set
			{
				this.mOriginField = value;
			}
		}

		[XmlIgnore]
		public bool MOriginSpecified
		{
			get
			{
				return this.mOriginFieldSpecified;
			}
			set
			{
				this.mOriginFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double MScale
		{
			get
			{
				return this.mScaleField;
			}
			set
			{
				this.mScaleField = value;
			}
		}

		[XmlIgnore]
		public bool MScaleSpecified
		{
			get
			{
				return this.mScaleFieldSpecified;
			}
			set
			{
				this.mScaleFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double XYTolerance
		{
			get
			{
				return this.xYToleranceField;
			}
			set
			{
				this.xYToleranceField = value;
			}
		}

		[XmlIgnore]
		public bool XYToleranceSpecified
		{
			get
			{
				return this.xYToleranceFieldSpecified;
			}
			set
			{
				this.xYToleranceFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double ZTolerance
		{
			get
			{
				return this.zToleranceField;
			}
			set
			{
				this.zToleranceField = value;
			}
		}

		[XmlIgnore]
		public bool ZToleranceSpecified
		{
			get
			{
				return this.zToleranceFieldSpecified;
			}
			set
			{
				this.zToleranceFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double MTolerance
		{
			get
			{
				return this.mToleranceField;
			}
			set
			{
				this.mToleranceField = value;
			}
		}

		[XmlIgnore]
		public bool MToleranceSpecified
		{
			get
			{
				return this.mToleranceFieldSpecified;
			}
			set
			{
				this.mToleranceFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool HighPrecision
		{
			get
			{
				return this.highPrecisionField;
			}
			set
			{
				this.highPrecisionField = value;
			}
		}

		[XmlIgnore]
		public bool HighPrecisionSpecified
		{
			get
			{
				return this.highPrecisionFieldSpecified;
			}
			set
			{
				this.highPrecisionFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double LeftLongitude
		{
			get
			{
				return this.leftLongitudeField;
			}
			set
			{
				this.leftLongitudeField = value;
			}
		}

		[XmlIgnore]
		public bool LeftLongitudeSpecified
		{
			get
			{
				return this.leftLongitudeFieldSpecified;
			}
			set
			{
				this.leftLongitudeFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int WKID
		{
			get
			{
				return this.wKIDField;
			}
			set
			{
				this.wKIDField = value;
			}
		}

		[XmlIgnore]
		public bool WKIDSpecified
		{
			get
			{
				return this.wKIDFieldSpecified;
			}
			set
			{
				this.wKIDFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int VCSWKID
		{
			get
			{
				return this.vCSWKIDField;
			}
			set
			{
				this.vCSWKIDField = value;
			}
		}

		[XmlIgnore]
		public bool VCSWKIDSpecified
		{
			get
			{
				return this.vCSWKIDFieldSpecified;
			}
			set
			{
				this.vCSWKIDFieldSpecified = value;
			}
		}
	}
}
