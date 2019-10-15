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
	public class TemplateInfo
	{
		private string descriptionField;

		private string nameField;

		private DataObject prototypeField;

		private esriFeatureEditTool defaultToolField;

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
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public DataObject Prototype
		{
			get
			{
				return this.prototypeField;
			}
			set
			{
				this.prototypeField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriFeatureEditTool DefaultTool
		{
			get
			{
				return this.defaultToolField;
			}
			set
			{
				this.defaultToolField = value;
			}
		}
	}
}
