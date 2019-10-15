using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlInclude(typeof(CmykColor)), XmlInclude(typeof(HsvColor)), XmlInclude(typeof(HlsColor)), XmlInclude(typeof(RgbColor)), XmlInclude(typeof(GrayColor)), XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	[Serializable]
	public abstract class Color
	{
		private bool useWindowsDitheringField;

		private bool useWindowsDitheringFieldSpecified;

		private byte alphaValueField;

		private bool alphaValueFieldSpecified;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool UseWindowsDithering
		{
			get
			{
				return this.useWindowsDitheringField;
			}
			set
			{
				this.useWindowsDitheringField = value;
			}
		}

		[XmlIgnore]
		public bool UseWindowsDitheringSpecified
		{
			get
			{
				return this.useWindowsDitheringFieldSpecified;
			}
			set
			{
				this.useWindowsDitheringFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public byte AlphaValue
		{
			get
			{
				return this.alphaValueField;
			}
			set
			{
				this.alphaValueField = value;
			}
		}

		[XmlIgnore]
		public bool AlphaValueSpecified
		{
			get
			{
				return this.alphaValueFieldSpecified;
			}
			set
			{
				this.alphaValueFieldSpecified = value;
			}
		}
	}
}
