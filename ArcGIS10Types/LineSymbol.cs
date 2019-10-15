using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlInclude(typeof(SimpleLineSymbol)), XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	[Serializable]
	public abstract class LineSymbol : Symbol
	{
		private Color colorField;

		private double widthField;

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
	}
}
