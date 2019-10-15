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
	public class RelatedRecordSet
	{
		private Fields relatedRecordFieldsField;

		private RelatedRecordGroup[] relatedRecordGroupsField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Fields RelatedRecordFields
		{
			get
			{
				return this.relatedRecordFieldsField;
			}
			set
			{
				this.relatedRecordFieldsField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public RelatedRecordGroup[] RelatedRecordGroups
		{
			get
			{
				return this.relatedRecordGroupsField;
			}
			set
			{
				this.relatedRecordGroupsField = value;
			}
		}
	}
}
