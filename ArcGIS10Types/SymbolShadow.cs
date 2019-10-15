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
	public class SymbolShadow : Shadow
	{
		private LineSymbol symbolField;

		private short cornerRoundingField;

		private double horizontalOffsetField;

		private double verticalOffsetField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public LineSymbol Symbol
		{
			get
			{
				return this.symbolField;
			}
			set
			{
				this.symbolField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public short CornerRounding
		{
			get
			{
				return this.cornerRoundingField;
			}
			set
			{
				this.cornerRoundingField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double HorizontalOffset
		{
			get
			{
				return this.horizontalOffsetField;
			}
			set
			{
				this.horizontalOffsetField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double VerticalOffset
		{
			get
			{
				return this.verticalOffsetField;
			}
			set
			{
				this.verticalOffsetField = value;
			}
		}
	}
}
