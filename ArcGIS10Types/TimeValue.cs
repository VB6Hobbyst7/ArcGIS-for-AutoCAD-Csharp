using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlInclude(typeof(TimeExtent)), XmlInclude(typeof(TimeInstant)), XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	[Serializable]
	public class TimeValue
	{
		private TimeReference timeReferenceField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TimeReference TimeReference
		{
			get
			{
				return this.timeReferenceField;
			}
			set
			{
				this.timeReferenceField = value;
			}
		}
	}
}
