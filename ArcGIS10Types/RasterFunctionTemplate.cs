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
	public class RasterFunctionTemplate : RasterFunction
	{
		private RasterFunction functionField;

		private RasterFunctionArguments argumentsField;

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
	}
}
