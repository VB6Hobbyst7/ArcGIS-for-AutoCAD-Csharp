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
	public class RecordSet
	{
		private Fields fieldsField;

		private Record[] recordsField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Fields Fields
		{
			get
			{
				return this.fieldsField;
			}
			set
			{
				this.fieldsField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public Record[] Records
		{
			get
			{
				return this.recordsField;
			}
			set
			{
				this.recordsField = value;
			}
		}
	}
}
