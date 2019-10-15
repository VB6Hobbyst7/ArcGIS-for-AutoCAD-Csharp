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
	public class RelationResult
	{
		private int leftIndexField;

		private int rightIndexField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int leftIndex
		{
			get
			{
				return this.leftIndexField;
			}
			set
			{
				this.leftIndexField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int rightIndex
		{
			get
			{
				return this.rightIndexField;
			}
			set
			{
				this.rightIndexField = value;
			}
		}
	}
}
