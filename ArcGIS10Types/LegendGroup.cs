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
	public class LegendGroup
	{
		private bool visibleField;

		private bool editableField;

		private string headingField;

		private LegendClass[] legendClassesField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool Visible
		{
			get
			{
				return this.visibleField;
			}
			set
			{
				this.visibleField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool Editable
		{
			get
			{
				return this.editableField;
			}
			set
			{
				this.editableField = value;
			}
		}

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
		public LegendClass[] LegendClasses
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
