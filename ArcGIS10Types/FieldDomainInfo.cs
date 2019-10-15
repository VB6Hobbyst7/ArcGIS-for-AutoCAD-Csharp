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
	public class FieldDomainInfo
	{
		private string fieldNameField;

		private Domain domainField;

		private bool isInheritedField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string FieldName
		{
			get
			{
				return this.fieldNameField;
			}
			set
			{
				this.fieldNameField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Domain Domain
		{
			get
			{
				return this.domainField;
			}
			set
			{
				this.domainField = value;
			}
		}

		[DefaultValue(false), XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool IsInherited
		{
			get
			{
				return this.isInheritedField;
			}
			set
			{
				this.isInheritedField = value;
			}
		}

		public FieldDomainInfo()
		{
			this.isInheritedField = false;
		}
	}
}
