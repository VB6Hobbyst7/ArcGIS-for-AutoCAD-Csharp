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
	public class FeatureServerForceDeriveFromAnyType
	{
		private DataObjects dataObjectsField;

		private DataObjectGroups dataObjectGroupsField;

		private DomainInfo domainInfoField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public DataObjects DataObjects
		{
			get
			{
				return this.dataObjectsField;
			}
			set
			{
				this.dataObjectsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public DataObjectGroups DataObjectGroups
		{
			get
			{
				return this.dataObjectGroupsField;
			}
			set
			{
				this.dataObjectGroupsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public DomainInfo DomainInfo
		{
			get
			{
				return this.domainInfoField;
			}
			set
			{
				this.domainInfoField = value;
			}
		}
	}
}
