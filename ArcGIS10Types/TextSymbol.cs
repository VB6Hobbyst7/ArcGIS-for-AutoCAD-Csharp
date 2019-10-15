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
	public class TextSymbol : Symbol
	{
		private Color colorField;

		private int breakCharIndexField;

		private esriTextVerticalAlignment verticalAlignmentField;

		private esriTextHorizontalAlignment horizontalAlignmentField;

		private bool clipField;

		private bool rightToLeftField;

		private double angleField;

		private double xOffsetField;

		private double yOffsetField;

		private Color shadowColorField;

		private double shadowXOffsetField;

		private double shadowYOffsetField;

		private esriTextPosition textPositionField;

		private esriTextCase textCaseField;

		private double characterSpacingField;

		private double characterWidthField;

		private double wordSpacingField;

		private bool kerningField;

		private double leadingField;

		private esriTextDirection textDirectionField;

		private double flipAngleField;

		private bool typeSettingField;

		private string textPathClassField;

		private Symbol fillSymbolField;

		private string textField;

		private double sizeField;

		private esriMaskStyle maskStyleField;

		private double maskSizeField;

		private Symbol maskSymbolField;

		private string fontNameField;

		private bool fontItalicField;

		private bool fontItalicFieldSpecified;

		private bool fontUnderlineField;

		private bool fontUnderlineFieldSpecified;

		private bool fontStrikethroughField;

		private bool fontStrikethroughFieldSpecified;

		private int fontWeightField;

		private bool fontWeightFieldSpecified;

		private int fontCharsetField;

		private bool fontCharsetFieldSpecified;

		private int fontSizeHiField;

		private bool fontSizeHiFieldSpecified;

		private int fontSizeLoField;

		private bool fontSizeLoFieldSpecified;

		private string textParserClassField;

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
		public int BreakCharIndex
		{
			get
			{
				return this.breakCharIndexField;
			}
			set
			{
				this.breakCharIndexField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriTextVerticalAlignment VerticalAlignment
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

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriTextHorizontalAlignment HorizontalAlignment
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

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool Clip
		{
			get
			{
				return this.clipField;
			}
			set
			{
				this.clipField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
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

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
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

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
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

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
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
		public Color ShadowColor
		{
			get
			{
				return this.shadowColorField;
			}
			set
			{
				this.shadowColorField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double ShadowXOffset
		{
			get
			{
				return this.shadowXOffsetField;
			}
			set
			{
				this.shadowXOffsetField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double ShadowYOffset
		{
			get
			{
				return this.shadowYOffsetField;
			}
			set
			{
				this.shadowYOffsetField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriTextPosition TextPosition
		{
			get
			{
				return this.textPositionField;
			}
			set
			{
				this.textPositionField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriTextCase TextCase
		{
			get
			{
				return this.textCaseField;
			}
			set
			{
				this.textCaseField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double CharacterSpacing
		{
			get
			{
				return this.characterSpacingField;
			}
			set
			{
				this.characterSpacingField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double CharacterWidth
		{
			get
			{
				return this.characterWidthField;
			}
			set
			{
				this.characterWidthField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double WordSpacing
		{
			get
			{
				return this.wordSpacingField;
			}
			set
			{
				this.wordSpacingField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool Kerning
		{
			get
			{
				return this.kerningField;
			}
			set
			{
				this.kerningField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double Leading
		{
			get
			{
				return this.leadingField;
			}
			set
			{
				this.leadingField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriTextDirection TextDirection
		{
			get
			{
				return this.textDirectionField;
			}
			set
			{
				this.textDirectionField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double FlipAngle
		{
			get
			{
				return this.flipAngleField;
			}
			set
			{
				this.flipAngleField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool TypeSetting
		{
			get
			{
				return this.typeSettingField;
			}
			set
			{
				this.typeSettingField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string TextPathClass
		{
			get
			{
				return this.textPathClassField;
			}
			set
			{
				this.textPathClassField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Symbol FillSymbol
		{
			get
			{
				return this.fillSymbolField;
			}
			set
			{
				this.fillSymbolField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Text
		{
			get
			{
				return this.textField;
			}
			set
			{
				this.textField = value;
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

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriMaskStyle MaskStyle
		{
			get
			{
				return this.maskStyleField;
			}
			set
			{
				this.maskStyleField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double MaskSize
		{
			get
			{
				return this.maskSizeField;
			}
			set
			{
				this.maskSizeField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Symbol MaskSymbol
		{
			get
			{
				return this.maskSymbolField;
			}
			set
			{
				this.maskSymbolField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string FontName
		{
			get
			{
				return this.fontNameField;
			}
			set
			{
				this.fontNameField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool FontItalic
		{
			get
			{
				return this.fontItalicField;
			}
			set
			{
				this.fontItalicField = value;
			}
		}

		[XmlIgnore]
		public bool FontItalicSpecified
		{
			get
			{
				return this.fontItalicFieldSpecified;
			}
			set
			{
				this.fontItalicFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool FontUnderline
		{
			get
			{
				return this.fontUnderlineField;
			}
			set
			{
				this.fontUnderlineField = value;
			}
		}

		[XmlIgnore]
		public bool FontUnderlineSpecified
		{
			get
			{
				return this.fontUnderlineFieldSpecified;
			}
			set
			{
				this.fontUnderlineFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool FontStrikethrough
		{
			get
			{
				return this.fontStrikethroughField;
			}
			set
			{
				this.fontStrikethroughField = value;
			}
		}

		[XmlIgnore]
		public bool FontStrikethroughSpecified
		{
			get
			{
				return this.fontStrikethroughFieldSpecified;
			}
			set
			{
				this.fontStrikethroughFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int FontWeight
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
		public int FontCharset
		{
			get
			{
				return this.fontCharsetField;
			}
			set
			{
				this.fontCharsetField = value;
			}
		}

		[XmlIgnore]
		public bool FontCharsetSpecified
		{
			get
			{
				return this.fontCharsetFieldSpecified;
			}
			set
			{
				this.fontCharsetFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int FontSizeHi
		{
			get
			{
				return this.fontSizeHiField;
			}
			set
			{
				this.fontSizeHiField = value;
			}
		}

		[XmlIgnore]
		public bool FontSizeHiSpecified
		{
			get
			{
				return this.fontSizeHiFieldSpecified;
			}
			set
			{
				this.fontSizeHiFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int FontSizeLo
		{
			get
			{
				return this.fontSizeLoField;
			}
			set
			{
				this.fontSizeLoField = value;
			}
		}

		[XmlIgnore]
		public bool FontSizeLoSpecified
		{
			get
			{
				return this.fontSizeLoFieldSpecified;
			}
			set
			{
				this.fontSizeLoFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string TextParserClass
		{
			get
			{
				return this.textParserClassField;
			}
			set
			{
				this.textParserClassField = value;
			}
		}

		public TextSymbol()
		{
			this.characterWidthField = 100.0;
			this.wordSpacingField = 100.0;
		}
	}
}
