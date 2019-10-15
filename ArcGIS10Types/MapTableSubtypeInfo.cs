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
	public class MapTableSubtypeInfo
	{
		private int tableIDField;

		private string subtypeFieldNameField;

		private SubtypeInfo[] subtypeInfosField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int TableID
		{
			get
			{
				return this.tableIDField;
			}
			set
			{
				this.tableIDField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string SubtypeFieldName
		{
			get
			{
				return this.subtypeFieldNameField;
			}
			set
			{
				this.subtypeFieldNameField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public SubtypeInfo[] SubtypeInfos
		{
			get
			{
				return this.subtypeInfosField;
			}
			set
			{
				this.subtypeInfosField = value;
			}
		}
	}
}
