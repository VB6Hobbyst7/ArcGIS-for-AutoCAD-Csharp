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
	public class NumericFormat
	{
		private esriRoundingOptionEnum roundingOptionField;

		private bool roundingOptionFieldSpecified;

		private int roundingValueField;

		private bool roundingValueFieldSpecified;

		private esriNumericAlignmentEnum alignmentOptionField;

		private bool alignmentOptionFieldSpecified;

		private int alignmentWidthField;

		private bool alignmentWidthFieldSpecified;

		private bool useSeparatorField;

		private bool useSeparatorFieldSpecified;

		private bool zeroPadField;

		private bool zeroPadFieldSpecified;

		private bool showPlusField;

		private bool showPlusFieldSpecified;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriRoundingOptionEnum RoundingOption
		{
			get
			{
				return this.roundingOptionField;
			}
			set
			{
				this.roundingOptionField = value;
			}
		}

		[XmlIgnore]
		public bool RoundingOptionSpecified
		{
			get
			{
				return this.roundingOptionFieldSpecified;
			}
			set
			{
				this.roundingOptionFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int RoundingValue
		{
			get
			{
				return this.roundingValueField;
			}
			set
			{
				this.roundingValueField = value;
			}
		}

		[XmlIgnore]
		public bool RoundingValueSpecified
		{
			get
			{
				return this.roundingValueFieldSpecified;
			}
			set
			{
				this.roundingValueFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriNumericAlignmentEnum AlignmentOption
		{
			get
			{
				return this.alignmentOptionField;
			}
			set
			{
				this.alignmentOptionField = value;
			}
		}

		[XmlIgnore]
		public bool AlignmentOptionSpecified
		{
			get
			{
				return this.alignmentOptionFieldSpecified;
			}
			set
			{
				this.alignmentOptionFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int AlignmentWidth
		{
			get
			{
				return this.alignmentWidthField;
			}
			set
			{
				this.alignmentWidthField = value;
			}
		}

		[XmlIgnore]
		public bool AlignmentWidthSpecified
		{
			get
			{
				return this.alignmentWidthFieldSpecified;
			}
			set
			{
				this.alignmentWidthFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool UseSeparator
		{
			get
			{
				return this.useSeparatorField;
			}
			set
			{
				this.useSeparatorField = value;
			}
		}

		[XmlIgnore]
		public bool UseSeparatorSpecified
		{
			get
			{
				return this.useSeparatorFieldSpecified;
			}
			set
			{
				this.useSeparatorFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool ZeroPad
		{
			get
			{
				return this.zeroPadField;
			}
			set
			{
				this.zeroPadField = value;
			}
		}

		[XmlIgnore]
		public bool ZeroPadSpecified
		{
			get
			{
				return this.zeroPadFieldSpecified;
			}
			set
			{
				this.zeroPadFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool ShowPlus
		{
			get
			{
				return this.showPlusField;
			}
			set
			{
				this.showPlusField = value;
			}
		}

		[XmlIgnore]
		public bool ShowPlusSpecified
		{
			get
			{
				return this.showPlusFieldSpecified;
			}
			set
			{
				this.showPlusFieldSpecified = value;
			}
		}
	}
}
