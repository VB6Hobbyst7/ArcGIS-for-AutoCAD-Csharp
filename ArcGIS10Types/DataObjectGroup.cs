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
	public class DataObjectGroup
	{
		private DataObject[] dataObjectArrayField;

		private int groupIDField;

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public DataObject[] DataObjectArray
		{
			get
			{
				return this.dataObjectArrayField;
			}
			set
			{
				this.dataObjectArrayField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int GroupID
		{
			get
			{
				return this.groupIDField;
			}
			set
			{
				this.groupIDField = value;
			}
		}
	}
}
