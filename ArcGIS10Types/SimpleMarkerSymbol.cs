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
	public class SimpleMarkerSymbol : MarkerSymbol
	{
		private bool outlineField;

		private double outlineSizeField;

		private Color outlineColorField;

		private esriSimpleMarkerStyle styleField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool Outline
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

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double OutlineSize
		{
			get
			{
				return this.outlineSizeField;
			}
			set
			{
				this.outlineSizeField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Color OutlineColor
		{
			get
			{
				return this.outlineColorField;
			}
			set
			{
				this.outlineColorField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriSimpleMarkerStyle Style
		{
			get
			{
				return this.styleField;
			}
			set
			{
				this.styleField = value;
			}
		}
	}
}
