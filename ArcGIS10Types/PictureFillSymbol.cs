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
	public class PictureFillSymbol : FillSymbol
	{
		private byte[] pictureField;

		private string pictureUriField;

		private double widthField;

		private bool widthFieldSpecified;

		private double heightField;

		private bool heightFieldSpecified;

		private Color bgColorField;

		private Color fgColorField;

		private Color bitmapTransColorField;

		private double xSeparationField;

		private bool xSeparationFieldSpecified;

		private double ySeparationField;

		private bool ySeparationFieldSpecified;

		private bool swap1BitColorField;

		private bool swap1BitColorFieldSpecified;

		private double angleField;

		private bool angleFieldSpecified;

		private double xOffsetField;

		private bool xOffsetFieldSpecified;

		private double yOffsetField;

		private bool yOffsetFieldSpecified;

		private double xScaleField;

		private double yScaleField;

		[XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "base64Binary")]
		public byte[] Picture
		{
			get
			{
				return this.pictureField;
			}
			set
			{
				this.pictureField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string PictureUri
		{
			get
			{
				return this.pictureUriField;
			}
			set
			{
				this.pictureUriField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double Width
		{
			get
			{
				return this.widthField;
			}
			set
			{
				this.widthField = value;
			}
		}

		[XmlIgnore]
		public bool WidthSpecified
		{
			get
			{
				return this.widthFieldSpecified;
			}
			set
			{
				this.widthFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double Height
		{
			get
			{
				return this.heightField;
			}
			set
			{
				this.heightField = value;
			}
		}

		[XmlIgnore]
		public bool HeightSpecified
		{
			get
			{
				return this.heightFieldSpecified;
			}
			set
			{
				this.heightFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Color BgColor
		{
			get
			{
				return this.bgColorField;
			}
			set
			{
				this.bgColorField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Color FgColor
		{
			get
			{
				return this.fgColorField;
			}
			set
			{
				this.fgColorField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Color BitmapTransColor
		{
			get
			{
				return this.bitmapTransColorField;
			}
			set
			{
				this.bitmapTransColorField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double XSeparation
		{
			get
			{
				return this.xSeparationField;
			}
			set
			{
				this.xSeparationField = value;
			}
		}

		[XmlIgnore]
		public bool XSeparationSpecified
		{
			get
			{
				return this.xSeparationFieldSpecified;
			}
			set
			{
				this.xSeparationFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double YSeparation
		{
			get
			{
				return this.ySeparationField;
			}
			set
			{
				this.ySeparationField = value;
			}
		}

		[XmlIgnore]
		public bool YSeparationSpecified
		{
			get
			{
				return this.ySeparationFieldSpecified;
			}
			set
			{
				this.ySeparationFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool Swap1BitColor
		{
			get
			{
				return this.swap1BitColorField;
			}
			set
			{
				this.swap1BitColorField = value;
			}
		}

		[XmlIgnore]
		public bool Swap1BitColorSpecified
		{
			get
			{
				return this.swap1BitColorFieldSpecified;
			}
			set
			{
				this.swap1BitColorFieldSpecified = value;
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

		[XmlIgnore]
		public bool AngleSpecified
		{
			get
			{
				return this.angleFieldSpecified;
			}
			set
			{
				this.angleFieldSpecified = value;
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

		[XmlIgnore]
		public bool XOffsetSpecified
		{
			get
			{
				return this.xOffsetFieldSpecified;
			}
			set
			{
				this.xOffsetFieldSpecified = value;
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

		[XmlIgnore]
		public bool YOffsetSpecified
		{
			get
			{
				return this.yOffsetFieldSpecified;
			}
			set
			{
				this.yOffsetFieldSpecified = value;
			}
		}

		[DefaultValue(1), XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double XScale
		{
			get
			{
				return this.xScaleField;
			}
			set
			{
				this.xScaleField = value;
			}
		}

		[DefaultValue(1), XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double YScale
		{
			get
			{
				return this.yScaleField;
			}
			set
			{
				this.yScaleField = value;
			}
		}

		public PictureFillSymbol()
		{
			this.xScaleField = 1.0;
			this.yScaleField = 1.0;
		}
	}
}
