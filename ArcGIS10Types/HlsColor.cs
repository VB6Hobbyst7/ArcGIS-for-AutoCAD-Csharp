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
	public class HlsColor : Color
	{
		private short hueField;

		private byte lightnessField;

		private byte saturationField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public short Hue
		{
			get
			{
				return this.hueField;
			}
			set
			{
				this.hueField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public byte Lightness
		{
			get
			{
				return this.lightnessField;
			}
			set
			{
				this.lightnessField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public byte Saturation
		{
			get
			{
				return this.saturationField;
			}
			set
			{
				this.saturationField = value;
			}
		}
	}
}
