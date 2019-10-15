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
	public class RandomColorRamp : ColorRamp
	{
		private int numColorsField;

		private bool numColorsFieldSpecified;

		private bool useSeedField;

		private bool useSeedFieldSpecified;

		private int seedField;

		private bool seedFieldSpecified;

		private short minValueField;

		private bool minValueFieldSpecified;

		private short maxValueField;

		private bool maxValueFieldSpecified;

		private short minSaturationField;

		private bool minSaturationFieldSpecified;

		private short maxSaturationField;

		private bool maxSaturationFieldSpecified;

		private short startHueField;

		private bool startHueFieldSpecified;

		private short endHueField;

		private bool endHueFieldSpecified;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int NumColors
		{
			get
			{
				return this.numColorsField;
			}
			set
			{
				this.numColorsField = value;
			}
		}

		[XmlIgnore]
		public bool NumColorsSpecified
		{
			get
			{
				return this.numColorsFieldSpecified;
			}
			set
			{
				this.numColorsFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool UseSeed
		{
			get
			{
				return this.useSeedField;
			}
			set
			{
				this.useSeedField = value;
			}
		}

		[XmlIgnore]
		public bool UseSeedSpecified
		{
			get
			{
				return this.useSeedFieldSpecified;
			}
			set
			{
				this.useSeedFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int Seed
		{
			get
			{
				return this.seedField;
			}
			set
			{
				this.seedField = value;
			}
		}

		[XmlIgnore]
		public bool SeedSpecified
		{
			get
			{
				return this.seedFieldSpecified;
			}
			set
			{
				this.seedFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public short MinValue
		{
			get
			{
				return this.minValueField;
			}
			set
			{
				this.minValueField = value;
			}
		}

		[XmlIgnore]
		public bool MinValueSpecified
		{
			get
			{
				return this.minValueFieldSpecified;
			}
			set
			{
				this.minValueFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public short MaxValue
		{
			get
			{
				return this.maxValueField;
			}
			set
			{
				this.maxValueField = value;
			}
		}

		[XmlIgnore]
		public bool MaxValueSpecified
		{
			get
			{
				return this.maxValueFieldSpecified;
			}
			set
			{
				this.maxValueFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public short MinSaturation
		{
			get
			{
				return this.minSaturationField;
			}
			set
			{
				this.minSaturationField = value;
			}
		}

		[XmlIgnore]
		public bool MinSaturationSpecified
		{
			get
			{
				return this.minSaturationFieldSpecified;
			}
			set
			{
				this.minSaturationFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public short MaxSaturation
		{
			get
			{
				return this.maxSaturationField;
			}
			set
			{
				this.maxSaturationField = value;
			}
		}

		[XmlIgnore]
		public bool MaxSaturationSpecified
		{
			get
			{
				return this.maxSaturationFieldSpecified;
			}
			set
			{
				this.maxSaturationFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public short StartHue
		{
			get
			{
				return this.startHueField;
			}
			set
			{
				this.startHueField = value;
			}
		}

		[XmlIgnore]
		public bool StartHueSpecified
		{
			get
			{
				return this.startHueFieldSpecified;
			}
			set
			{
				this.startHueFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public short EndHue
		{
			get
			{
				return this.endHueField;
			}
			set
			{
				this.endHueField = value;
			}
		}

		[XmlIgnore]
		public bool EndHueSpecified
		{
			get
			{
				return this.endHueFieldSpecified;
			}
			set
			{
				this.endHueFieldSpecified = value;
			}
		}
	}
}
