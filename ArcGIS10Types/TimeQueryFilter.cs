using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlInclude(typeof(ImageQueryFilter)), XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	[Serializable]
	public class TimeQueryFilter : SpatialFilter
	{
		private TimeValue timeValueField;

		private TimeReference outputTimeReferenceField;

		private esriTimeRelation timeRelationField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TimeValue TimeValue
		{
			get
			{
				return this.timeValueField;
			}
			set
			{
				this.timeValueField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TimeReference OutputTimeReference
		{
			get
			{
				return this.outputTimeReferenceField;
			}
			set
			{
				this.outputTimeReferenceField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriTimeRelation TimeRelation
		{
			get
			{
				return this.timeRelationField;
			}
			set
			{
				this.timeRelationField = value;
			}
		}
	}
}
