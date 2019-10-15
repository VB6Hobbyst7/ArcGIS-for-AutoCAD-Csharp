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
	public class DomainInfo
	{
		private Domain domainField;

		private bool inheritedField;

		private bool inheritedFieldSpecified;

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

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool Inherited
		{
			get
			{
				return this.inheritedField;
			}
			set
			{
				this.inheritedField = value;
			}
		}

		[XmlIgnore]
		public bool InheritedSpecified
		{
			get
			{
				return this.inheritedFieldSpecified;
			}
			set
			{
				this.inheritedFieldSpecified = value;
			}
		}
	}
}
