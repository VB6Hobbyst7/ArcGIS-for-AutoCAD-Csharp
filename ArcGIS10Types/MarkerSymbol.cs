using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlInclude(typeof(CartographicMarkerSymbol)), XmlInclude(typeof(PictureMarkerSymbol)), XmlInclude(typeof(CharacterMarkerSymbol)), XmlInclude(typeof(SimpleMarkerSymbol)), XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	[Serializable]
	public abstract class MarkerSymbol : Symbol
	{
		private double angleField;

		private Color colorField;

		private double sizeField;

		private double xOffsetField;

		private double yOffsetField;

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
		public double Size
		{
			get
			{
				return this.sizeField;
			}
			set
			{
				this.sizeField = value;
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
	}
}
