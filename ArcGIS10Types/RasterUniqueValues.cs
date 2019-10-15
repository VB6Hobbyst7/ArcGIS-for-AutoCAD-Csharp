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
	public class RasterUniqueValues
	{
		private int uniqueValuesSizeField;

		private bool uniqueValuesSizeFieldSpecified;

		private object[] valuesField;

		private int[] countsField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int UniqueValuesSize
		{
			get
			{
				return this.uniqueValuesSizeField;
			}
			set
			{
				this.uniqueValuesSizeField = value;
			}
		}

		[XmlIgnore]
		public bool UniqueValuesSizeSpecified
		{
			get
			{
				return this.uniqueValuesSizeFieldSpecified;
			}
			set
			{
				this.uniqueValuesSizeFieldSpecified = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Value", Form = XmlSchemaForm.Unqualified)]
		public object[] Values
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
		public int[] Counts
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
	}
}
