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
	public class RasterHistogram
	{
		private int sizeField;

		private double minField;

		private double maxField;

		private object countsField;

		private double[] binsField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int Size
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
		public double Min
		{
			get
			{
				return this.minField;
			}
			set
			{
				this.minField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double Max
		{
			get
			{
				return this.maxField;
			}
			set
			{
				this.maxField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public object Counts
		{
			get
			{
				return this.countsField;
			}
			set
			{
				this.countsField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public double[] Bins
		{
			get
			{
				return this.binsField;
			}
			set
			{
				this.binsField = value;
			}
		}
	}
}
