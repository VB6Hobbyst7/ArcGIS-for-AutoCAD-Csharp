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
	public class SimpleTextSymbol : Symbol
	{
		private Color colorField;

		private Color backgroundColorField;

		private Color outlineColorField;

		private esriSimpleTextVerticalAlignment verticalAlignmentField;

		private bool verticalAlignmentFieldSpecified;

		private esriSimpleTextHorizontalAlignment horizontalAlignmentField;

		private bool horizontalAlignmentFieldSpecified;

		private bool rightToLeftField;

		private double angleField;

		private double xOffsetField;

		private double yOffsetField;

		private double sizeField;

		private bool sizeFieldSpecified;

		private string fontFamilyNameField;

		private esriFontStyle fontStyleField;

		private bool fontStyleFieldSpecified;

		private esriFontWeight fontWeightField;

		private bool fontWeightFieldSpecified;

		private esriFontDecoration fontDecorationField;

		private bool fontDecorationFieldSpecified;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Color Color
		{
			get
			{
				return this.colorField;
			}
			set
			{
				this.colorField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Color BackgroundColor
		{
			get
			{
				return this.backgroundColorField;
			}
			set
			{
				this.backgroundColorField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Color OutlineColor
		{
			get
			{
				return this.outlineColorField;
			}
			set
			{
				this.outlineColorField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriSimpleTextVerticalAlignment VerticalAlignment
		{
			get
			{
				return this.verticalAlignmentField;
			}
			set
			{
				this.verticalAlignmentField = value;
			}
		}

		[XmlIgnore]
		public bool VerticalAlignmentSpecified
		{
			get
			{
				return this.verticalAlignmentFieldSpecified;
			}
			set
			{
				this.verticalAlignmentFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriSimpleTextHorizontalAlignment HorizontalAlignment
		{
			get
			{
				return this.horizontalAlignmentField;
			}
			set
			{
				this.horizontalAlignmentField = value;
			}
		}

		[XmlIgnore]
		public bool HorizontalAlignmentSpecified
		{
			get
			{
				return this.horizontalAlignmentFieldSpecified;
			}
			set
			{
				this.horizontalAlignmentFieldSpecified = value;
			}
		}

		[DefaultValue(false), XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool RightToLeft
		{
			get
			{
				return this.rightToLeftField;
			}
			set
			{
				this.rightToLeftField = value;
			}
		}

		[DefaultValue(0), XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double Angle
		{
			get
			{
				return this.angleField;
			}
			set
			{
				this.angleField = value;
			}
		}

		[DefaultValue(0), XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double XOffset
		{
			get
			{
				return this.xOffsetField;
			}
			set
			{
				this.xOffsetField = value;
			}
		}

		[DefaultValue(0), XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double YOffset
		{
			get
			{
				return this.yOffsetField;
			}
			set
			{
				this.yOffsetField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double Size
		{
			get
			{
				return this.sizeField;
			}
			set
			{
				this.sizeField = value;
			}
		}

		[XmlIgnore]
		public bool SizeSpecified
		{
			get
			{
				return this.sizeFieldSpecified;
			}
			set
			{
				this.sizeFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string FontFamilyName
		{
			get
			{
				return this.fontFamilyNameField;
			}
			set
			{
				this.fontFamilyNameField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriFontStyle FontStyle
		{
			get
			{
				return this.fontStyleField;
			}
			set
			{
				this.fontStyleField = value;
			}
		}

		[XmlIgnore]
		public bool FontStyleSpecified
		{
			get
			{
				return this.fontStyleFieldSpecified;
			}
			set
			{
				this.fontStyleFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriFontWeight FontWeight
		{
			get
			{
				return this.fontWeightField;
			}
			set
			{
				this.fontWeightField = value;
			}
		}

		[XmlIgnore]
		public bool FontWeightSpecified
		{
			get
			{
				return this.fontWeightFieldSpecified;
			}
			set
			{
				this.fontWeightFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriFontDecoration FontDecoration
		{
			get
			{
				return this.fontDecorationField;
			}
			set
			{
				this.fontDecorationField = value;
			}
		}

		[XmlIgnore]
		public bool FontDecorationSpecified
		{
			get
			{
				return this.fontDecorationFieldSpecified;
			}
			set
			{
				this.fontDecorationFieldSpecified = value;
			}
		}

		public SimpleTextSymbol()
		{
			this.rightToLeftField = false;
			this.angleField = 0.0;
			this.xOffsetField = 0.0;
			this.yOffsetField = 0.0;
		}
	}
}
