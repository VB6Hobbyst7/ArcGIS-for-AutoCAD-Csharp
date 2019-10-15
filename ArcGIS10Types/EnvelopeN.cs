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
	public class EnvelopeN : Envelope
	{
		private double xMinField;

		private double yMinField;

		private double xMaxField;

		private double yMaxField;

		private double zMinField;

		private bool zMinFieldSpecified;

		private double zMaxField;

		private bool zMaxFieldSpecified;

		private double mMinField;

		private bool mMinFieldSpecified;

		private double mMaxField;

		private bool mMaxFieldSpecified;

		private SpatialReference spatialReferenceField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double XMin
		{
			get
			{
				return this.xMinField;
			}
			set
			{
				this.xMinField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double YMin
		{
			get
			{
				return this.yMinField;
			}
			set
			{
				this.yMinField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double XMax
		{
			get
			{
				return this.xMaxField;
			}
			set
			{
				this.xMaxField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double YMax
		{
			get
			{
				return this.yMaxField;
			}
			set
			{
				this.yMaxField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double ZMin
		{
			get
			{
				return this.zMinField;
			}
			set
			{
				this.zMinField = value;
			}
		}

		[XmlIgnore]
		public bool ZMinSpecified
		{
			get
			{
				return this.zMinFieldSpecified;
			}
			set
			{
				this.zMinFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double ZMax
		{
			get
			{
				return this.zMaxField;
			}
			set
			{
				this.zMaxField = value;
			}
		}

		[XmlIgnore]
		public bool ZMaxSpecified
		{
			get
			{
				return this.zMaxFieldSpecified;
			}
			set
			{
				this.zMaxFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double MMin
		{
			get
			{
				return this.mMinField;
			}
			set
			{
				this.mMinField = value;
			}
		}

		[XmlIgnore]
		public bool MMinSpecified
		{
			get
			{
				return this.mMinFieldSpecified;
			}
			set
			{
				this.mMinFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double MMax
		{
			get
			{
				return this.mMaxField;
			}
			set
			{
				this.mMaxField = value;
			}
		}

		[XmlIgnore]
		public bool MMaxSpecified
		{
			get
			{
				return this.mMaxFieldSpecified;
			}
			set
			{
				this.mMaxFieldSpecified = value;
			}
		}

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
	}
}
