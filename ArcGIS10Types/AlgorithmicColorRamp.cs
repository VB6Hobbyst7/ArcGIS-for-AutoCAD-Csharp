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
	public class AlgorithmicColorRamp : ColorRamp
	{
		private string algorithmField;

		private HsvColor fromColorField;

		private HsvColor toColorField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Algorithm
		{
			get
			{
				return this.algorithmField;
			}
			set
			{
				this.algorithmField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public HsvColor FromColor
		{
			get
			{
				return this.fromColorField;
			}
			set
			{
				this.fromColorField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public HsvColor ToColor
		{
			get
			{
				return this.toColorField;
			}
			set
			{
				this.toColorField = value;
			}
		}
	}
}
