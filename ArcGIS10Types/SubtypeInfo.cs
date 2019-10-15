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
	public class SubtypeInfo
	{
		private int subtypeCodeField;

		private string subtypeNameField;

		private FieldDomainInfo[] fieldDomainInfosField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int SubtypeCode
		{
			get
			{
				return this.subtypeCodeField;
			}
			set
			{
				this.subtypeCodeField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string SubtypeName
		{
			get
			{
				return this.subtypeNameField;
			}
			set
			{
				this.subtypeNameField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public FieldDomainInfo[] FieldDomainInfos
		{
			get
			{
				return this.fieldDomainInfosField;
			}
			set
			{
				this.fieldDomainInfosField = value;
			}
		}
	}
}
