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
	public class CharacterMarkerSymbol : CartographicMarkerSymbol
	{
		private int characterIndexField;

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

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int CharacterIndex
		{
			get
			{
				return this.characterIndexField;
			}
			set
			{
				this.characterIndexField = value;
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
	}
}
