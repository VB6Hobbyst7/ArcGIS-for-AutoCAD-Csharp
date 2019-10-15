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
	public class RgbColor : Color
	{
		private byte redField;

		private byte greenField;

		private byte blueField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public byte Red
		{
			get
			{
				return this.redField;
			}
			set
			{
				this.redField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public byte Green
		{
			get
			{
				return this.greenField;
			}
			set
			{
				this.greenField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public byte Blue
		{
			get
			{
				return this.blueField;
			}
			set
			{
				this.blueField = value;
			}
		}
	}
}
