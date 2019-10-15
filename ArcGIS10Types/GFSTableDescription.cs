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
	public class GFSTableDescription
	{
		private int idField;

		private string definitionExpressionField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int ID
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string DefinitionExpression
		{
			get
			{
				return this.definitionExpressionField;
			}
			set
			{
				this.definitionExpressionField = value;
			}
		}
	}
}
