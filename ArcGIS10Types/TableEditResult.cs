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
	public class TableEditResult
	{
		private EditResult[] addResultsField;

		private EditResult[] deleteResultsField;

		private int layerOrTableIDField;

		private EditResult[] updateResultsField;

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public EditResult[] AddResults
		{
			get
			{
				return this.addResultsField;
			}
			set
			{
				this.addResultsField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public EditResult[] DeleteResults
		{
			get
			{
				return this.deleteResultsField;
			}
			set
			{
				this.deleteResultsField = value;
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

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public EditResult[] UpdateResults
		{
			get
			{
				return this.updateResultsField;
			}
			set
			{
				this.updateResultsField = value;
			}
		}
	}
}
