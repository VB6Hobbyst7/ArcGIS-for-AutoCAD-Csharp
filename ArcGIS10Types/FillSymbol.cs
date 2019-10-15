using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlInclude(typeof(SimpleFillSymbol)), XmlInclude(typeof(XMLBinaryFillSymbol)), XmlInclude(typeof(PictureFillSymbol)), XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	[Serializable]
	public abstract class FillSymbol : Symbol
	{
		private Color colorField;

		private LineSymbol outlineField;

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
		public LineSymbol Outline
		{
			get
			{
				return this.outlineField;
			}
			set
			{
				this.outlineField = value;
			}
		}
	}
}
