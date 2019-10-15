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
	public class DataObjectType
	{
		private string nameField;

		private PropertySet propDomainsField;

		private TemplateInfo[] templatesField;

		private object typeIDField;

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
		public PropertySet PropDomains
		{
			get
			{
				return this.propDomainsField;
			}
			set
			{
				this.propDomainsField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public TemplateInfo[] Templates
		{
			get
			{
				return this.templatesField;
			}
			set
			{
				this.templatesField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public object TypeID
		{
			get
			{
				return this.typeIDField;
			}
			set
			{
				this.typeIDField = value;
			}
		}
	}
}
