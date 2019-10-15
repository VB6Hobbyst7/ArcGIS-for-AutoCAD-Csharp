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
	public class RasterColormap
	{
		private int colormapSizeField;

		private int[] valuesField;

		private int[] colorsField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int ColormapSize
		{
			get
			{
				return this.colormapSizeField;
			}
			set
			{
				this.colormapSizeField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public int[] Values
		{
			get
			{
				return this.valuesField;
			}
			set
			{
				this.valuesField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public int[] Colors
		{
			get
			{
				return this.colorsField;
			}
			set
			{
				this.colorsField = value;
			}
		}
	}
}
