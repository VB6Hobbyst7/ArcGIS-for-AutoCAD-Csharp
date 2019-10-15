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
	public class MapServerForceDeriveFromAnyType
	{
		private RelatedRecordSet relatedRecordSetField;

		private FieldDomainInfo fieldDomainInfoField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public RelatedRecordSet RelatedRecordSet
		{
			get
			{
				return this.relatedRecordSetField;
			}
			set
			{
				this.relatedRecordSetField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public FieldDomainInfo FieldDomainInfo
		{
			get
			{
				return this.fieldDomainInfoField;
			}
			set
			{
				this.fieldDomainInfoField = value;
			}
		}
	}
}
