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
	public class AISRequest
	{
		private string nameField;

		private GeoImageDescription geoImageDescriptionField;

		private string[] argumentsField;

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
		public GeoImageDescription GeoImageDescription
		{
			get
			{
				return this.geoImageDescriptionField;
			}
			set
			{
				this.geoImageDescriptionField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("String", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public string[] Arguments
		{
			get
			{
				return this.argumentsField;
			}
			set
			{
				this.argumentsField = value;
			}
		}
	}
}
