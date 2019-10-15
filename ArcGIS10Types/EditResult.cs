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
	public class EditResult
	{
		private int codeField;

		private string descriptionField;

		private string globalIDField;

		private int oIDField;

		private bool succeededField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int Code
		{
			get
			{
				return this.codeField;
			}
			set
			{
				this.codeField = value;
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
		public string GlobalID
		{
			get
			{
				return this.globalIDField;
			}
			set
			{
				this.globalIDField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int OID
		{
			get
			{
				return this.oIDField;
			}
			set
			{
				this.oIDField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool Succeeded
		{
			get
			{
				return this.succeededField;
			}
			set
			{
				this.succeededField = value;
			}
		}
	}
}
