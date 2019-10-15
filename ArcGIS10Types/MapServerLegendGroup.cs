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
	public class MapServerLegendGroup
	{
		private string headingField;

		private MapServerLegendClass[] legendClassesField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Heading
		{
			get
			{
				return this.headingField;
			}
			set
			{
				this.headingField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public MapServerLegendClass[] LegendClasses
		{
			get
			{
				return this.legendClassesField;
			}
			set
			{
				this.legendClassesField = value;
			}
		}
	}
}
