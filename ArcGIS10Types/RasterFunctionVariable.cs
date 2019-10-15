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
	public class RasterFunctionVariable
	{
		private string nameField;

		private string descriptionField;

		private object valueField;

		private string[] aliasesField;

		private bool isDatasetField;

		private bool isDatasetFieldSpecified;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Description
		{
			get
			{
				return this.descriptionField;
			}
			set
			{
				this.descriptionField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public object Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("String", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public string[] Aliases
		{
			get
			{
				return this.aliasesField;
			}
			set
			{
				this.aliasesField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool IsDataset
		{
			get
			{
				return this.isDatasetField;
			}
			set
			{
				this.isDatasetField = value;
			}
		}

		[XmlIgnore]
		public bool IsDatasetSpecified
		{
			get
			{
				return this.isDatasetFieldSpecified;
			}
			set
			{
				this.isDatasetFieldSpecified = value;
			}
		}
	}
}
