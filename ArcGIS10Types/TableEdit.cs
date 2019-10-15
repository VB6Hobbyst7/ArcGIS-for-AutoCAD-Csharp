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
	public class TableEdit
	{
		private DataObjects addsField;

		private int[] deletesField;

		private int layerOrTableIDField;

		private DataObjects updatesField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public DataObjects Adds
		{
			get
			{
				return this.addsField;
			}
			set
			{
				this.addsField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public int[] Deletes
		{
			get
			{
				return this.deletesField;
			}
			set
			{
				this.deletesField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int LayerOrTableID
		{
			get
			{
				return this.layerOrTableIDField;
			}
			set
			{
				this.layerOrTableIDField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public DataObjects Updates
		{
			get
			{
				return this.updatesField;
			}
			set
			{
				this.updatesField = value;
			}
		}
	}
}
