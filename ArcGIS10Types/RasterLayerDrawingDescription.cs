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
	public class RasterLayerDrawingDescription : LayerDrawingDescription
	{
		private RasterRenderer rasterRendererField;

		private short transparencyField;

		private bool transparencyFieldSpecified;

		private short brightnessField;

		private bool brightnessFieldSpecified;

		private short contrastField;

		private bool contrastFieldSpecified;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public RasterRenderer RasterRenderer
		{
			get
			{
				return this.rasterRendererField;
			}
			set
			{
				this.rasterRendererField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public short Transparency
		{
			get
			{
				return this.transparencyField;
			}
			set
			{
				this.transparencyField = value;
			}
		}

		[XmlIgnore]
		public bool TransparencySpecified
		{
			get
			{
				return this.transparencyFieldSpecified;
			}
			set
			{
				this.transparencyFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public short Brightness
		{
			get
			{
				return this.brightnessField;
			}
			set
			{
				this.brightnessField = value;
			}
		}

		[XmlIgnore]
		public bool BrightnessSpecified
		{
			get
			{
				return this.brightnessFieldSpecified;
			}
			set
			{
				this.brightnessFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public short Contrast
		{
			get
			{
				return this.contrastField;
			}
			set
			{
				this.contrastField = value;
			}
		}

		[XmlIgnore]
		public bool ContrastSpecified
		{
			get
			{
				return this.contrastFieldSpecified;
			}
			set
			{
				this.contrastFieldSpecified = value;
			}
		}
	}
}
