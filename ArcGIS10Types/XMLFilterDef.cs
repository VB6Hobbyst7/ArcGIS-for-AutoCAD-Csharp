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
	public class XMLFilterDef : FilterDef
	{
		private string fieldNameField;

		private string expressionField;

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
		public string Expression
		{
			get
			{
				return this.expressionField;
			}
			set
			{
				this.expressionField = value;
			}
		}
	}
}
