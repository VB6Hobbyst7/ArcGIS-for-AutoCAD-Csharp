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
	public class RenderingRule
	{
		private string nameField;

		private string descriptionField;

		private RasterFunction functionField;

		private RasterFunctionArguments argumentsField;

		private string variableNameField;

		private RasterRenderer rendererField;

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
		public RasterFunction Function
		{
			get
			{
				return this.functionField;
			}
			set
			{
				this.functionField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public RasterFunctionArguments Arguments
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

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string VariableName
		{
			get
			{
				return this.variableNameField;
			}
			set
			{
				this.variableNameField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public RasterRenderer Renderer
		{
			get
			{
				return this.rendererField;
			}
			set
			{
				this.rendererField = value;
			}
		}
	}
}
