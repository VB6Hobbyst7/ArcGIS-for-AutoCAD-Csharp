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
	public class SymbolBackground : Background
	{
		private double horizontalGapField;

		private short cornerRoundingField;

		private double verticalGapField;

		private FillSymbol symbolField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double HorizontalGap
		{
			get
			{
				return this.horizontalGapField;
			}
			set
			{
				this.horizontalGapField = value;
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
		public double VerticalGap
		{
			get
			{
				return this.verticalGapField;
			}
			set
			{
				this.verticalGapField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public FillSymbol Symbol
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
	}
}
