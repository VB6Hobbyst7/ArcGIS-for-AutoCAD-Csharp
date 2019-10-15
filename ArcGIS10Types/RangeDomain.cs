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
	public class RangeDomain : Domain
	{
		private object maxValueField;

		private object minValueField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public object MaxValue
		{
			get
			{
				return this.maxValueField;
			}
			set
			{
				this.maxValueField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public object MinValue
		{
			get
			{
				return this.minValueField;
			}
			set
			{
				this.minValueField = value;
			}
		}
	}
}
