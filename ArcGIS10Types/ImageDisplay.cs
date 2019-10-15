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
	public class ImageDisplay
	{
		private int imageHeightField;

		private int imageWidthField;

		private double imageDPIField;

		private Color transparentColorField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int ImageHeight
		{
			get
			{
				return this.imageHeightField;
			}
			set
			{
				this.imageHeightField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int ImageWidth
		{
			get
			{
				return this.imageWidthField;
			}
			set
			{
				this.imageWidthField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double ImageDPI
		{
			get
			{
				return this.imageDPIField;
			}
			set
			{
				this.imageDPIField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Color TransparentColor
		{
			get
			{
				return this.transparentColorField;
			}
			set
			{
				this.transparentColorField = value;
			}
		}
	}
}
